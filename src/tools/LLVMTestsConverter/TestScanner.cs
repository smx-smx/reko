/*
 * Copyright 2018 Stefano Moioli<smxdev4@gmail.com>
 **/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LLVMTestsConverter
{
	public class TestScanner {

		public delegate void PrintUnitTestDelegate(string assembly, string encoding);

		public static void PrintDummyUnitTest(string assembly, string encoding) {
			Console.WriteLine($"//TODO: {assembly} => {encoding}");
		}

		private static byte[] ByteSwap(dynamic value) {
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes);
			return bytes;
		}

		private static UInt32 ToValueBe(string encoding) {
			return Convert.ToUInt32(encoding.Replace(",", "").Replace("0x", ""), 16);
		}

		private static UInt32 ToValueLe(string encoding) {
			return BitConverter.ToUInt32(
				ByteSwap(ToValueBe(encoding)
			), 0);
		}

		private static void PrintAArch64UnitTest(string assembly, string encoding) {
			StringBuilder sb = new StringBuilder();

			UInt32 valueBe = ToValueBe(encoding);
			UInt32 valueLe = ToValueLe(encoding);

			string template = @"	// Little-endian: {0:X8}
	public void AArch64Dis_0x{1:X8}() {{
		Given_Instruction(0x{1:X8});
		Expect_Code(""{2}"");
	}}";
			Console.WriteLine(string.Format(template, valueLe, valueBe, assembly));
		}

		private static void PrintPowerPcUnitTest(string assembly, string encoding) {
			UInt32 valueBe = ToValueBe(encoding);

			string template = @"	[Test]
	public void PPCDis_{1:X8}() {{
			var instr = DisassembleWord(0x{1:X8});
			Assert.AreEqual(""{0}"", instr.ToString());
	}}";
			Console.WriteLine(string.Format(template, assembly, valueBe));
		}

		private static void PrintX86UnitTest(string assembly, string encoding) {
			string template = @"	[Test]
	public void Dis_{0}() {{
		var instr = Disassemble64({1});
		Assert.AreEqual(""{2}"", instr.ToString());
	}}";

			string testName = Regex.Replace(assembly, @"\s", "");
			Console.WriteLine(string.Format(template, testName, encoding, assembly));
		}

		private readonly Dictionary<String, PrintUnitTestDelegate> arches = new Dictionary<string, PrintUnitTestDelegate>() {
			{ "AArch64", new PrintUnitTestDelegate(PrintAArch64UnitTest) },
			{ "ARM", new PrintUnitTestDelegate(PrintDummyUnitTest) },
			{ "Mips", new PrintUnitTestDelegate(PrintDummyUnitTest) },
			{ "PowerPC", new PrintUnitTestDelegate(PrintPowerPcUnitTest) },
			{ "Sparc", new PrintUnitTestDelegate(PrintDummyUnitTest) },
			{ "X86", new PrintUnitTestDelegate(PrintX86UnitTest) },
		};

		private string dirPath;
		public TestScanner(string dirPath) {
			this.dirPath = dirPath;
		}

		private void ProcessDir(string dir, PrintUnitTestDelegate unitTestPrinter) {
			foreach(string file in Directory.GetFiles(dir, "*.s", SearchOption.AllDirectories)) {
				Console.WriteLine($"  FILE => {file}");
				using (StreamReader rdr = File.OpenText(file)) {

					if(unitTestPrinter == PrintAArch64UnitTest) {
						unitTestPrinter.ToString();
					}
					
					var asmFile = new LLVMTestConverter(rdr, unitTestPrinter, out bool success);
					if (!success)
						continue;
				}
			}
		}

		public void Work() {
			string[] dirs = Directory.GetDirectories(dirPath);
			foreach(string dir in dirs) {
				var match = arches.Where((pair) => pair.Key.Contains(Path.GetFileName(dir))).FirstOrDefault();
				if(match.Value != null) {
					Console.WriteLine($" DIR => {dir}");
					ProcessDir(dir, match.Value);
				}
			}
		}
	}
}
