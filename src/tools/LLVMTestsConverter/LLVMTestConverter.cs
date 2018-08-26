/*
 * Copyright 2018 Stefano Moioli <smxdev4@gmail.com>
 **/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static LLVMTestsConverter.TestScanner;

namespace LLVMTestsConverter
{
	public enum CommentType
	{
		Invalid,
		Run,
		Check,
		Encoding
	}

	public enum LineType
	{
		Invalid,
		Comment,
		Assembly
	}

	public enum CommentStyle
	{
		Invalid,
		OneLine,
		TwoLines
	}

	public class LLVMTestConverter
	{
		private static readonly string[] commentTokens = new string[] {
			"//", ";", "#", "!"
		};

		private StreamReader rdr;

		private string fileCommentToken;
		private CommentStyle fileCommentStyle;

		private bool stopHandling = false;

		private bool runVerified = false;

		private bool encodingTypoPresent = false;

		private string Check;
		private string Encoding;

		private string RemoveCommentToken(string line) {
			if (line.Length < fileCommentToken.Length)
				return line;
			return line.Substring(fileCommentToken.Length).Trim();
		}

		/// <summary>
		/// Reads a line from a .s file and determines the line type
		/// </summary>
		/// <param name="lineType"></param>
		/// <returns></returns>
		private string ReadLine(out LineType lineType) {
			string line = rdr.ReadLine();
			if (line == null) {
				lineType = LineType.Invalid;
				return line;
			}

			line = line.Trim();
			foreach (string token in commentTokens) {
				if (line.StartsWith(token)) {
					lineType = LineType.Comment;
					return line;
				}
			}

			lineType = LineType.Assembly;
			return line;
		}

		/// <summary>
		/// Scans the file to determine the comment separator
		/// It additionally determines if encoding directives are located on 1 or 2 lines
		/// </summary>
		/// <returns></returns>
		private string DetectCommentStyle() {
			fileCommentStyle = CommentStyle.Invalid;

			long pos = rdr.BaseStream.Position;
			rdr.BaseStream.Seek(0, SeekOrigin.Begin);

			string line;
			while ((line = ReadLine(out LineType lineType)) != null) {
				if (lineType != LineType.Comment)
					continue;

				if (fileCommentToken == null) {
					foreach (string token in commentTokens) {
						if (line.StartsWith(token)) {
							fileCommentToken = token;
							break;
						}
					}
				}

				DetectCommment(RemoveCommentToken(line), out CommentType commentType);
				if (commentType != CommentType.Check)
					continue;


				string[] parts = line.Split(new string[] { fileCommentToken }, StringSplitOptions.RemoveEmptyEntries);
				switch (parts.Length) {
					case 1:
						long curPos = rdr.BaseStream.Position;
						string line2 = ReadLine(out LineType line2Type);
						if(line2 == null) {
							fileCommentStyle = CommentStyle.Invalid;
							stopHandling = true;
							goto end_ret;
						}
						if(line2Type != LineType.Comment) {
							continue;
						}
						line2 = DetectCommment(RemoveCommentToken(line2), out CommentType comment2Type);
						if (comment2Type == CommentType.Check) {
							DetectCommment(line2, out comment2Type);
							if(comment2Type == CommentType.Encoding) {
								fileCommentStyle = CommentStyle.TwoLines;
							} else {
								fileCommentStyle = CommentStyle.Invalid;
								goto end_ret;
							}
						} else {
							fileCommentStyle = CommentStyle.Invalid;
							goto end_ret;
						}
						break;
					case 2:
						fileCommentStyle = CommentStyle.OneLine;
						break;
					case 0:
						goto throw_ex;
					default:
						if (parts[1].Trim() == "<MCInst")
							goto case 2;

						throw_ex:
						throw new NotSupportedException($"Comment '{line}' contains {parts.Length} pieces");
				}

				if (fileCommentToken != null)
					break;
			}

			end_ret:
			rdr.BaseStream.Seek(pos, SeekOrigin.Begin);
			rdr.DiscardBufferedData();
			return fileCommentToken;
		}

		/// <summary>
		/// Checks and sanitizes assembly and encoding
		/// </summary>
		/// <param name="check"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		private bool Parse_CheckEncoding(string check, string encoding) {
			//interestingly, there's at least 1 broken LLVM test 
			//arm64-aliases.S -> ; CHECK: tlbi alle1                 ; encoding: [0x9f,0x87,0x0c,0xd5
			//someone forgot to add a closing "]"
			if (!encoding.StartsWith("[")/* || !encoding.EndsWith("]")*/) {
				throw new InvalidDataException($"Invalid syntax for encoding => '{encoding}'");
			}
			if (!encoding.EndsWith("]")) {
				encodingTypoPresent = true;
			}
			encoding = encoding.Substring(1, encoding.Length - 2);
			if(encoding == string.Empty)
				return false;

			if (encoding.Contains("0b") || encoding.Contains("'"))
				return false;

			this.Check = check;
			this.Encoding = encoding;
			return true;
		}

		/// <summary>
		/// Handles a comment line
		/// </summary>
		/// <param name="currentLine"></param>
		/// <returns></returns>
		private bool HandleComment(string currentLine) {
			/*
			 * We want to have the following layout
			 * lines[0] -> CHECK
			 * lines[1] -> CHECK encoding
			 * So if the comment style is OneLine, we split the line
			 * If the comment style is TwoLines, we read and process the next line
			 */
			List<string> lines = new List<string>() { currentLine };

			string commentData = DetectCommment(RemoveCommentToken(currentLine), out CommentType commentType);
			if (!runVerified) {
				if (commentType == CommentType.Run) {
					if (!commentData.Contains("-show-encoding")) {
						stopHandling = true;
						return false;
					}
				}
				runVerified = true;
			}

			if (fileCommentStyle == CommentStyle.TwoLines) {
				string line2 = ReadLine(out LineType lineType);
				if(lineType != LineType.Comment) {
					commentType = CommentType.Invalid;
					return false;
				}
				line2 = DetectCommment(RemoveCommentToken(line2), out CommentType line2Comment);
				if(line2Comment != CommentType.Check) {
					commentType = CommentType.Invalid;
					return false;
				}
				line2 = DetectCommment(line2, out line2Comment);
				if(line2Comment != CommentType.Encoding) {
					commentType = CommentType.Invalid;
					return false;
				}

				string line = lines[0];
				line = DetectCommment(RemoveCommentToken(line), out CommentType lineComment);
				if(lineComment != CommentType.Check) {
					throw new InvalidDataException("This was supposed to be a CHECK comment");
				}

				lines[0] = line;
				lines.Add(line2);
			} else {
				string line = lines[0];
				string[] parts = line.Split(new string[] { fileCommentToken }, StringSplitOptions.RemoveEmptyEntries);

				if(parts.Length < 2) {
					commentType = CommentType.Invalid;
					return false;
				}

				string check = parts[0].Trim();
				string encoding = parts[1].Trim();

				check = DetectCommment(check, out CommentType checkType);
				encoding = DetectCommment(encoding, out CommentType encodingType);
				if (checkType != CommentType.Check || encodingType != CommentType.Encoding) {
					commentType = CommentType.Invalid;
					return false;
				}

				lines[0] = check;
				lines.Add(encoding);
			}

			/*
			Console.WriteLine($"Check => {lines[0]}");
			Console.WriteLine($"Encoding => {lines[1]}");
			*/
			return Parse_CheckEncoding(lines[0], lines[1]);
		}

		/// <summary>
		/// Detects the type of comments for a given comment line
		/// </summary>
		/// <param name="line"></param>
		/// <param name="type"></param>
		/// <returns>The remaining part of the comment, without the leading tag (e.g CHECK)</returns>
		private string DetectCommment(string line, out CommentType type) {
			string[] parts = line.Split(':');

			if(parts.Length <= 1) {
				type = CommentType.Invalid;
				return line;
			}


			string commentType = parts[0];
			line = string.Join(":", parts.Skip(1)).Trim();

			switch (commentType) {
				case "RUN":
					type = CommentType.Run;
					break;
				case "CHECK":
				case "CHECK-SHA":
				case "EMBEDDED":
					type = CommentType.Check;
					break;
				case "encoding":
					type = CommentType.Encoding;
					break;
				default:
					type = CommentType.Invalid;
					break;
			}

			return line;
		}

		public LLVMTestConverter(StreamReader rdr, PrintUnitTestDelegate unitTestPrinter, out bool success) {
			this.rdr = rdr;

			rdr.BaseStream.Seek(0, SeekOrigin.Begin);
			rdr.DiscardBufferedData();

			DetectCommentStyle();

			while (!stopHandling) {
				string line = ReadLine(out LineType type);
				if (line == null)
					break;

				switch (type) {
					case LineType.Comment:
						if (HandleComment(line)) {
							unitTestPrinter.Invoke(Check, Encoding);
						}
						break;
					case LineType.Assembly:
						break;
					case LineType.Invalid:
						throw new InvalidDataException("Failed to load source");
				}
			}

			success = !stopHandling;
		}
	}
}
