#region License
/* 
 * Copyright (C) 1999-2012 John K�ll�n.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2, or (at your option)
 * any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; see the file COPYING.  If not, write to
 * the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
 */
#endregion

using Decompiler.Core;
using Decompiler.Core.Code;
using Decompiler.Core.Lib;
using Decompiler.Core.Output;
using Decompiler.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Decompiler.Analysis
{
	/// <summary>
	/// We are keenly interested in discovering the register linkage 
	/// between procedures, i.e. what registers are used by a called 
	/// procedure, and what modified registers are used by a calling 
	/// procedure. Once these registers have been discovered, we can
	/// separate the procedures from each other and proceed with the
	/// decompilation.
	/// </summary>
	public class DataFlowAnalysis
	{
		private Program prog;
		private DecompilerEventListener eventListener;
		private ProgramDataFlow flow;

        public DataFlowAnalysis(Program prog, DecompilerEventListener eventListener)
		{
			this.prog = prog;
            this.eventListener = eventListener;
			this.flow = new ProgramDataFlow(prog);
		}

		public void AnalyzeProgram()
		{
			UntangleProcedures();
			BuildExpressionTrees();
		}

        /// <summary>
        /// Processes procedures individually, building complex expression trees out
        /// of the simple, close-to-the-machine code generated by the disassembly.
        /// </summary>
        /// <param name="rl"></param>
		public void BuildExpressionTrees()
		{
            int i = 0;
			foreach (Procedure proc in prog.Procedures.Values)
			{
                eventListener.ShowProgress("Building complex expressions.", i, prog.Procedures.Values.Count);
                ++i;

                LongAddRewriter larw = new LongAddRewriter(proc, prog.Architecture);
                larw.Transform();

				Aliases alias = new Aliases(proc, prog.Architecture, flow);
				alias.Transform();
                var doms = new DominatorGraph<Block>(proc.ControlGraph, proc.EntryBlock);
                var sst = new SsaTransform(proc, doms);
				var ssa = sst.SsaState;

                var cce = new ConditionCodeEliminator(ssa.Identifiers, prog.Architecture);
				cce.Transform();
				DeadCode.Eliminate(proc, ssa);

				var vp = new ValuePropagator(ssa.Identifiers, proc);
				vp.Transform();
				DeadCode.Eliminate(proc, ssa);

				// Build expressions. A definition with a single use can be subsumed
				// into the using expression. 

				var coa = new Coalescer(proc, ssa);
				coa.Transform();
				DeadCode.Eliminate(proc, ssa);

                var liv = new LinearInductionVariableFinder(
                    proc, 
                    ssa.Identifiers, 
                    new BlockDominatorGraph(proc.ControlGraph, proc.EntryBlock));
                liv.Find();
     
                foreach (KeyValuePair<LinearInductionVariable, LinearInductionVariableContext> de in liv.Contexts)
                {
                    var str = new StrengthReduction(ssa, de.Key, de.Value);
                    str.ClassifyUses();
                    str.ModifyUses();
                }
				var opt = new OutParameterTransformer(proc, ssa.Identifiers);
				opt.Transform();
				DeadCode.Eliminate(proc, ssa);

                // Definitions with multiple uses and variables joined by PHI functions become webs.
                var web = new WebBuilder(proc, ssa.Identifiers, prog.InductionVariables);
				web.Transform();
				ssa.ConvertBack(false);
			} 
		}

		public void DumpProgram()
		{
			foreach (Procedure proc in prog.Procedures.Values)
			{
				StringWriter output = new StringWriter();
				ProcedureFlow pf= this.flow[proc];
                Formatter f = new Formatter(output);
				if (pf.Signature != null)
					pf.Signature.Emit(proc.Name, ProcedureSignature.EmitFlags.None, f);
				else if (proc.Signature != null)
					proc.Signature.Emit(proc.Name, ProcedureSignature.EmitFlags.None, f);
				else
					output.Write("Warning: no signature found for {0}", proc.Name);
				output.WriteLine();
				pf.Emit(prog.Architecture, output);

				output.WriteLine("// {0}", proc.Name);
				proc.Signature.Emit(proc.Name, ProcedureSignature.EmitFlags.None, f);
				output.WriteLine();
				foreach (Block block in proc.ControlGraph.Blocks)
				{
					if (block != null)
					{
						BlockFlow bf = this.flow[block];
						bf.Emit(prog.Architecture, output);
						output.WriteLine();
						block.Write(output);
					}
				}
				Debug.WriteLine(output.ToString());
			}
		}

		public ProgramDataFlow ProgramDataFlow
		{
			get { return flow; }
		}

		/// <summary>
		/// Finds all interprocedural register dependencies (in- and out-parameters) and
		/// abstracts them away by rewriting as calls.
		/// </summary>
        /// <returns>A RegisterLiveness object that summarizes the interprocedural register
        /// liveness analysis. This information can be used to generate SSA form.
        /// </returns>
		public void UntangleProcedures()
		{
            eventListener.ShowStatus("Finding terminating procedures.");
            var term = new TerminationAnalysis(flow);
            term.Analyze(prog);
			eventListener.ShowStatus("Finding trashed registers.");
            var trf = new TrashedRegisterFinder(prog, prog.Procedures.Values, flow, eventListener);
			trf.Compute();
            eventListener.ShowStatus("Rewriting affine expressions.");
            trf.RewriteBasicBlocks();
            eventListener.ShowStatus("Computing register liveness.");
            var rl = RegisterLiveness.Compute(prog, flow, eventListener);
            eventListener.ShowStatus("Rewriting calls.");
			GlobalCallRewriter.Rewrite(prog, flow);
		}
	}
}
