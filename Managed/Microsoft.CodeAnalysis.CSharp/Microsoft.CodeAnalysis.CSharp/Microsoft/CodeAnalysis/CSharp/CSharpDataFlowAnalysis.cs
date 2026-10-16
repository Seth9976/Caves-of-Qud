using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200014D RID: 333
	internal class CSharpDataFlowAnalysis : DataFlowAnalysis
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x0008C884 File Offset: 0x0008AA84
		internal CSharpDataFlowAnalysis(RegionAnalysisContext context)
		{
			this._context = context;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0008C894 File Offset: 0x0008AA94
		public override ImmutableArray<ISymbol> VariablesDeclared
		{
			get
			{
				if (this._variablesDeclared.IsDefault)
				{
					ImmutableArray<ISymbol> immutableArray = (this.Succeeded ? CSharpDataFlowAnalysis.Normalize(VariablesDeclaredWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion)) : ImmutableArray<ISymbol>.Empty);
					ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._variablesDeclared, immutableArray);
				}
				return this._variablesDeclared;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0008C914 File Offset: 0x0008AB14
		private HashSet<Symbol> UnassignedVariables
		{
			get
			{
				if (this._unassignedVariables == null)
				{
					HashSet<Symbol> hashSet = (this.Succeeded ? UnassignedVariablesWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, false) : new HashSet<Symbol>());
					Interlocked.CompareExchange<HashSet<Symbol>>(ref this._unassignedVariables, hashSet, null);
				}
				return this._unassignedVariables;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0008C974 File Offset: 0x0008AB74
		public override ImmutableArray<ISymbol> DataFlowsIn
		{
			get
			{
				if (this._dataFlowsIn.IsDefault)
				{
					this._succeeded = new bool?(!this._context.Failed);
					ImmutableArray<ISymbol> immutableArray = (this._context.Failed ? ImmutableArray<ISymbol>.Empty : CSharpDataFlowAnalysis.Normalize(DataFlowsInWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion, this.UnassignedVariables, this.UnassignedVariableAddressOfSyntaxes, out this._succeeded)));
					ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._dataFlowsIn, immutableArray);
				}
				return this._dataFlowsIn;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0008CA25 File Offset: 0x0008AC25
		public override ImmutableArray<ISymbol> DefinitelyAssignedOnEntry
		{
			get
			{
				return this.ComputeDefinitelyAssignedValues().Item1;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0008CA32 File Offset: 0x0008AC32
		public override ImmutableArray<ISymbol> DefinitelyAssignedOnExit
		{
			get
			{
				return this.ComputeDefinitelyAssignedValues().Item2;
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0008CA40 File Offset: 0x0008AC40
		[return: TupleElementNames(new string[] { "onEntry", "onExit" })]
		private ValueTuple<ImmutableArray<ISymbol>, ImmutableArray<ISymbol>> ComputeDefinitelyAssignedValues()
		{
			if (this._definitelyAssignedOnExit.IsDefault)
			{
				ImmutableArray<ISymbol> immutableArray = ImmutableArray<ISymbol>.Empty;
				ImmutableArray<ISymbol> immutableArray2 = ImmutableArray<ISymbol>.Empty;
				if (this.Succeeded)
				{
					ValueTuple<HashSet<Symbol>, HashSet<Symbol>> valueTuple = DefinitelyAssignedWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion);
					HashSet<Symbol> item = valueTuple.Item1;
					IEnumerable<Symbol> item2 = valueTuple.Item2;
					immutableArray = CSharpDataFlowAnalysis.Normalize(item);
					immutableArray2 = CSharpDataFlowAnalysis.Normalize(item2);
				}
				ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._definitelyAssignedOnEntry, immutableArray);
				ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._definitelyAssignedOnExit, immutableArray2);
			}
			return new ValueTuple<ImmutableArray<ISymbol>, ImmutableArray<ISymbol>>(this._definitelyAssignedOnEntry, this._definitelyAssignedOnExit);
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0008CAF4 File Offset: 0x0008ACF4
		public override ImmutableArray<ISymbol> DataFlowsOut
		{
			get
			{
				ImmutableArray<ISymbol> dataFlowsIn = this.DataFlowsIn;
				if (this._dataFlowsOut.IsDefault)
				{
					ImmutableArray<ISymbol> immutableArray = (this.Succeeded ? CSharpDataFlowAnalysis.Normalize(DataFlowsOutWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion, this.UnassignedVariables, this._dataFlowsIn)) : ImmutableArray<ISymbol>.Empty);
					ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._dataFlowsOut, immutableArray);
				}
				return this._dataFlowsOut;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x0008CB88 File Offset: 0x0008AD88
		public override ImmutableArray<ISymbol> AlwaysAssigned
		{
			get
			{
				if (this._alwaysAssigned.IsDefault)
				{
					ImmutableArray<ISymbol> immutableArray = (this.Succeeded ? CSharpDataFlowAnalysis.Normalize(AlwaysAssignedWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion)) : ImmutableArray<ISymbol>.Empty);
					ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._alwaysAssigned, immutableArray);
				}
				return this._alwaysAssigned;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0008CC06 File Offset: 0x0008AE06
		public override ImmutableArray<ISymbol> ReadInside
		{
			get
			{
				if (this._readInside.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._readInside;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x0008CC21 File Offset: 0x0008AE21
		public override ImmutableArray<ISymbol> WrittenInside
		{
			get
			{
				if (this._writtenInside.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._writtenInside;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0008CC3C File Offset: 0x0008AE3C
		public override ImmutableArray<ISymbol> ReadOutside
		{
			get
			{
				if (this._readOutside.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._readOutside;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x0008CC57 File Offset: 0x0008AE57
		public override ImmutableArray<ISymbol> WrittenOutside
		{
			get
			{
				if (this._writtenOutside.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._writtenOutside;
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0008CC74 File Offset: 0x0008AE74
		private void AnalyzeReadWrite()
		{
			IEnumerable<Symbol> enumerable;
			IEnumerable<Symbol> enumerable2;
			IEnumerable<Symbol> enumerable3;
			IEnumerable<Symbol> enumerable4;
			IEnumerable<Symbol> enumerable5;
			IEnumerable<Symbol> enumerable6;
			IEnumerable<Symbol> enumerable7;
			IEnumerable<Symbol> enumerable8;
			IEnumerable<MethodSymbol> enumerable9;
			if (this.Succeeded)
			{
				ReadWriteWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode, this._context.FirstInRegion, this._context.LastInRegion, this.UnassignedVariableAddressOfSyntaxes, out enumerable, out enumerable2, out enumerable3, out enumerable4, out enumerable5, out enumerable6, out enumerable7, out enumerable8, out enumerable9);
			}
			else
			{
				enumerable2 = (enumerable = (enumerable3 = (enumerable4 = (enumerable5 = (enumerable6 = (enumerable7 = (enumerable8 = Enumerable.Empty<Symbol>())))))));
				enumerable9 = Enumerable.Empty<MethodSymbol>();
			}
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._readInside, CSharpDataFlowAnalysis.Normalize(enumerable));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._writtenInside, CSharpDataFlowAnalysis.Normalize(enumerable2));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._readOutside, CSharpDataFlowAnalysis.Normalize(enumerable3));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._writtenOutside, CSharpDataFlowAnalysis.Normalize(enumerable4));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._captured, CSharpDataFlowAnalysis.Normalize(enumerable5));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._capturedInside, CSharpDataFlowAnalysis.Normalize(enumerable7));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._capturedOutside, CSharpDataFlowAnalysis.Normalize(enumerable8));
			ImmutableInterlocked.InterlockedInitialize<ISymbol>(ref this._unsafeAddressTaken, CSharpDataFlowAnalysis.Normalize(enumerable6));
			ImmutableInterlocked.InterlockedInitialize<IMethodSymbol>(ref this._usedLocalFunctions, CSharpDataFlowAnalysis.Normalize(enumerable9));
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x0008CDA5 File Offset: 0x0008AFA5
		public override ImmutableArray<ISymbol> Captured
		{
			get
			{
				if (this._captured.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._captured;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0008CDC0 File Offset: 0x0008AFC0
		public override ImmutableArray<ISymbol> CapturedInside
		{
			get
			{
				if (this._capturedInside.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._capturedInside;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x0008CDDB File Offset: 0x0008AFDB
		public override ImmutableArray<ISymbol> CapturedOutside
		{
			get
			{
				if (this._capturedOutside.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._capturedOutside;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0008CDF6 File Offset: 0x0008AFF6
		public override ImmutableArray<ISymbol> UnsafeAddressTaken
		{
			get
			{
				if (this._unsafeAddressTaken.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._unsafeAddressTaken;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x0008CE11 File Offset: 0x0008B011
		public override ImmutableArray<IMethodSymbol> UsedLocalFunctions
		{
			get
			{
				if (this._usedLocalFunctions.IsDefault)
				{
					this.AnalyzeReadWrite();
				}
				return this._usedLocalFunctions;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0008CE2C File Offset: 0x0008B02C
		private HashSet<PrefixUnaryExpressionSyntax> UnassignedVariableAddressOfSyntaxes
		{
			get
			{
				if (this._unassignedVariableAddressOfSyntaxes == null)
				{
					HashSet<PrefixUnaryExpressionSyntax> hashSet = (this.Succeeded ? UnassignedAddressTakenVariablesWalker.Analyze(this._context.Compilation, this._context.Member, this._context.BoundNode) : new HashSet<PrefixUnaryExpressionSyntax>());
					Interlocked.CompareExchange<HashSet<PrefixUnaryExpressionSyntax>>(ref this._unassignedVariableAddressOfSyntaxes, hashSet, null);
				}
				return this._unassignedVariableAddressOfSyntaxes;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0008CE8B File Offset: 0x0008B08B
		public sealed override bool Succeeded
		{
			get
			{
				if (this._succeeded == null)
				{
					ImmutableArray<ISymbol> dataFlowsIn = this.DataFlowsIn;
				}
				return this._succeeded.Value;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0008CEAC File Offset: 0x0008B0AC
		private static ImmutableArray<ISymbol> Normalize(IEnumerable<Symbol> data)
		{
			return ImmutableArray.CreateRange<ISymbol>(Enumerable.OrderBy<Symbol, Symbol>(Enumerable.Where<Symbol>(data, (Symbol s) => s.CanBeReferencedByName), (Symbol s) => s, LexicalOrderSymbolComparer.Instance).GetPublicSymbols());
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0008CF14 File Offset: 0x0008B114
		private static ImmutableArray<IMethodSymbol> Normalize(IEnumerable<MethodSymbol> data)
		{
			return ImmutableArray.CreateRange<IMethodSymbol>(Enumerable.Select<MethodSymbol, IMethodSymbol>(Enumerable.OrderBy<MethodSymbol, Symbol>(Enumerable.Where<MethodSymbol>(data, (MethodSymbol s) => s.CanBeReferencedByName), (MethodSymbol s) => s, LexicalOrderSymbolComparer.Instance), (MethodSymbol p) => p.GetPublicSymbol()));
		}

		// Token: 0x04000E10 RID: 3600
		private readonly RegionAnalysisContext _context;

		// Token: 0x04000E11 RID: 3601
		private ImmutableArray<ISymbol> _variablesDeclared;

		// Token: 0x04000E12 RID: 3602
		private HashSet<Symbol> _unassignedVariables;

		// Token: 0x04000E13 RID: 3603
		private ImmutableArray<ISymbol> _dataFlowsIn;

		// Token: 0x04000E14 RID: 3604
		private ImmutableArray<ISymbol> _dataFlowsOut;

		// Token: 0x04000E15 RID: 3605
		private ImmutableArray<ISymbol> _definitelyAssignedOnEntry;

		// Token: 0x04000E16 RID: 3606
		private ImmutableArray<ISymbol> _definitelyAssignedOnExit;

		// Token: 0x04000E17 RID: 3607
		private ImmutableArray<ISymbol> _alwaysAssigned;

		// Token: 0x04000E18 RID: 3608
		private ImmutableArray<ISymbol> _readInside;

		// Token: 0x04000E19 RID: 3609
		private ImmutableArray<ISymbol> _writtenInside;

		// Token: 0x04000E1A RID: 3610
		private ImmutableArray<ISymbol> _readOutside;

		// Token: 0x04000E1B RID: 3611
		private ImmutableArray<ISymbol> _writtenOutside;

		// Token: 0x04000E1C RID: 3612
		private ImmutableArray<ISymbol> _captured;

		// Token: 0x04000E1D RID: 3613
		private ImmutableArray<IMethodSymbol> _usedLocalFunctions;

		// Token: 0x04000E1E RID: 3614
		private ImmutableArray<ISymbol> _capturedInside;

		// Token: 0x04000E1F RID: 3615
		private ImmutableArray<ISymbol> _capturedOutside;

		// Token: 0x04000E20 RID: 3616
		private ImmutableArray<ISymbol> _unsafeAddressTaken;

		// Token: 0x04000E21 RID: 3617
		private HashSet<PrefixUnaryExpressionSyntax> _unassignedVariableAddressOfSyntaxes;

		// Token: 0x04000E22 RID: 3618
		private bool? _succeeded;
	}
}
