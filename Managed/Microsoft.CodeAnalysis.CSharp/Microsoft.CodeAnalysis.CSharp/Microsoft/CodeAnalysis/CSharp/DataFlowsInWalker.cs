using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200014E RID: 334
	internal class DataFlowsInWalker : AbstractRegionDataFlowPass
	{
		// Token: 0x06001AF9 RID: 6905 RVA: 0x0008CF98 File Offset: 0x0008B198
		private DataFlowsInWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<Symbol> unassignedVariables, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes)
			: base(compilation, member, node, firstInRegion, lastInRegion, unassignedVariables, unassignedVariableAddressOfSyntaxes, false)
		{
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0008CFC4 File Offset: 0x0008B1C4
		internal static HashSet<Symbol> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<Symbol> unassignedVariables, HashSet<PrefixUnaryExpressionSyntax> unassignedVariableAddressOfSyntaxes, out bool? succeeded)
		{
			DataFlowsInWalker dataFlowsInWalker = new DataFlowsInWalker(compilation, member, node, firstInRegion, lastInRegion, unassignedVariables, unassignedVariableAddressOfSyntaxes);
			HashSet<Symbol> hashSet2;
			try
			{
				bool flag = false;
				HashSet<Symbol> hashSet = dataFlowsInWalker.Analyze(ref flag);
				succeeded = new bool?(!flag);
				hashSet2 = (flag ? new HashSet<Symbol>() : hashSet);
			}
			finally
			{
				dataFlowsInWalker.Free();
			}
			return hashSet2;
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0008D024 File Offset: 0x0008B224
		private HashSet<Symbol> Analyze(ref bool badRegion)
		{
			base.Analyze(ref badRegion, null);
			return this._dataFlowsIn;
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x0008D034 File Offset: 0x0008B234
		private DefiniteAssignmentPass.LocalState ResetState(DefiniteAssignmentPass.LocalState state)
		{
			bool flag = !state.Reachable;
			state = this.TopState();
			if (flag)
			{
				state.Assign(0);
			}
			return state;
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0008D053 File Offset: 0x0008B253
		protected override void EnterRegion()
		{
			this.State = this.ResetState(this.State);
			this._dataFlowsIn.Clear();
			base.EnterRegion();
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0008D078 File Offset: 0x0008B278
		protected override void NoteBranch([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pending, BoundNode gotoStmt, BoundStatement targetStmt)
		{
			if (!gotoStmt.WasCompilerGenerated && !targetStmt.WasCompilerGenerated && !base.RegionContains(gotoStmt.Syntax.Span) && base.RegionContains(targetStmt.Syntax.Span))
			{
				pending.State = this.ResetState(pending.State);
			}
			base.NoteBranch(pending, gotoStmt, targetStmt);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0008D0D8 File Offset: 0x0008B2D8
		public override BoundNode VisitRangeVariable(BoundRangeVariable node)
		{
			if (base.IsInside && !base.RegionContains(node.RangeVariableSymbol.Locations[0].SourceSpan))
			{
				this._dataFlowsIn.Add(node.RangeVariableSymbol);
			}
			return null;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0008D121 File Offset: 0x0008B321
		protected override void ReportUnassigned(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration)
		{
			if (base.RegionContains(node.Span))
			{
				this._dataFlowsIn.Add((symbol.Kind == SymbolKind.Field) ? base.GetNonMemberSymbol(slot) : symbol);
			}
			base.ReportUnassigned(symbol, node, slot, skipIfUseBeforeDeclaration);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0008D15B File Offset: 0x0008B35B
		protected override void ReportUnassignedOutParameter(ParameterSymbol parameter, SyntaxNode node, Location location)
		{
			if (node != null && node is ReturnStatementSyntax && base.RegionContains(node.Span))
			{
				this._dataFlowsIn.Add(parameter);
			}
			base.ReportUnassignedOutParameter(parameter, node, location);
		}

		// Token: 0x04000E23 RID: 3619
		private readonly HashSet<Symbol> _dataFlowsIn = new HashSet<Symbol>();
	}
}
