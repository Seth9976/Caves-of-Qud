using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200014F RID: 335
	internal class DataFlowsOutWalker : AbstractRegionDataFlowPass
	{
		// Token: 0x06001B02 RID: 6914 RVA: 0x0008D18C File Offset: 0x0008B38C
		private DataFlowsOutWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<Symbol> unassignedVariables, ImmutableArray<ISymbol> dataFlowsIn)
			: base(compilation, member, node, firstInRegion, lastInRegion, unassignedVariables, null, true)
		{
			this._dataFlowsIn = dataFlowsIn;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0008D1C0 File Offset: 0x0008B3C0
		internal static HashSet<Symbol> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, HashSet<Symbol> unassignedVariables, ImmutableArray<ISymbol> dataFlowsIn)
		{
			DataFlowsOutWalker dataFlowsOutWalker = new DataFlowsOutWalker(compilation, member, node, firstInRegion, lastInRegion, unassignedVariables, dataFlowsIn);
			HashSet<Symbol> hashSet2;
			try
			{
				bool flag = false;
				HashSet<Symbol> hashSet = dataFlowsOutWalker.Analyze(ref flag);
				hashSet2 = (flag ? new HashSet<Symbol>() : hashSet);
			}
			finally
			{
				dataFlowsOutWalker.Free();
			}
			return hashSet2;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0008D210 File Offset: 0x0008B410
		private HashSet<Symbol> Analyze(ref bool badRegion)
		{
			base.Analyze(ref badRegion, null);
			return this._dataFlowsOut;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0008D220 File Offset: 0x0008B420
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected override ImmutableArray<AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch> Scan(ref bool badRegion)
		{
			this._dataFlowsOut.Clear();
			return base.Scan(ref badRegion);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0008D234 File Offset: 0x0008B434
		protected override void EnterRegion()
		{
			foreach (ISymbol symbol in this._dataFlowsIn)
			{
				Symbol symbol2 = symbol.GetSymbol();
				int orCreateSlot = this.GetOrCreateSlot(symbol2, 0, false, true);
				if (orCreateSlot > 0 && !this.State.IsAssigned(orCreateSlot))
				{
					this._dataFlowsOut.Add(symbol2);
				}
			}
			base.EnterRegion();
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0008D298 File Offset: 0x0008B498
		protected override void NoteWrite(Symbol variable, BoundExpression value, bool read)
		{
			if (this.State.Reachable && base.IsInside)
			{
				ParameterSymbol parameterSymbol = variable as ParameterSymbol;
				if (this.FlowsOut(parameterSymbol))
				{
					this._dataFlowsOut.Add(parameterSymbol);
				}
			}
			base.NoteWrite(variable, value, read);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0008D2E0 File Offset: 0x0008B4E0
		protected override void AssignImpl(BoundNode node, BoundExpression value, bool isRef, bool written, bool read)
		{
			if (base.IsInside)
			{
				written = false;
				if (this.State.Reachable)
				{
					ParameterSymbol parameterSymbol = this.Param(node);
					if (this.FlowsOut(parameterSymbol))
					{
						this._dataFlowsOut.Add(parameterSymbol);
					}
				}
			}
			base.AssignImpl(node, value, isRef, written, read);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0008D330 File Offset: 0x0008B530
		private bool FlowsOut(ParameterSymbol param)
		{
			return param != null && param.RefKind != RefKind.None && !param.IsImplicitlyDeclared && !base.RegionContains(param.Locations[0].SourceSpan);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0008D370 File Offset: 0x0008B570
		private ParameterSymbol Param(BoundNode node)
		{
			BoundKind kind = node.Kind;
			if (kind == BoundKind.ThisReference)
			{
				return base.MethodThisParameter;
			}
			if (kind == BoundKind.Parameter)
			{
				return ((BoundParameter)node).ParameterSymbol;
			}
			return null;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0008D3A2 File Offset: 0x0008B5A2
		public override BoundNode VisitQueryClause(BoundQueryClause node)
		{
			return base.VisitQueryClause(node);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0008D3AB File Offset: 0x0008B5AB
		protected override void ReportUnassigned(Symbol symbol, SyntaxNode node, int slot, bool skipIfUseBeforeDeclaration)
		{
			if (!base.IsInside)
			{
				this._dataFlowsOut.Add((symbol.Kind == SymbolKind.Field) ? base.GetNonMemberSymbol(slot) : symbol);
			}
			base.ReportUnassigned(symbol, node, slot, skipIfUseBeforeDeclaration);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0008D3DF File Offset: 0x0008B5DF
		protected override void ReportUnassignedOutParameter(ParameterSymbol parameter, SyntaxNode node, Location location)
		{
			if (!this._dataFlowsOut.Contains(parameter) && (node == null || node is ReturnStatementSyntax))
			{
				this._dataFlowsOut.Add(parameter);
			}
			base.ReportUnassignedOutParameter(parameter, node, location);
		}

		// Token: 0x04000E24 RID: 3620
		private readonly ImmutableArray<ISymbol> _dataFlowsIn;

		// Token: 0x04000E25 RID: 3621
		private readonly HashSet<Symbol> _dataFlowsOut = new HashSet<Symbol>();
	}
}
