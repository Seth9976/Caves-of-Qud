using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200014A RID: 330
	internal class AlwaysAssignedWalker : AbstractRegionDataFlowPass
	{
		// Token: 0x06001AB5 RID: 6837 RVA: 0x0008BB78 File Offset: 0x00089D78
		private AlwaysAssignedWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion, null, null, false)
		{
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0008BBA0 File Offset: 0x00089DA0
		internal static IEnumerable<Symbol> Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
		{
			AlwaysAssignedWalker alwaysAssignedWalker = new AlwaysAssignedWalker(compilation, member, node, firstInRegion, lastInRegion);
			bool flag = false;
			IEnumerable<Symbol> enumerable;
			try
			{
				List<Symbol> list = alwaysAssignedWalker.Analyze(ref flag);
				IEnumerable<Symbol> enumerable2;
				if (!flag)
				{
					enumerable = list;
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = SpecializedCollections.EmptyEnumerable<Symbol>();
				}
				enumerable = enumerable2;
			}
			finally
			{
				alwaysAssignedWalker.Free();
			}
			return enumerable;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0008BBEC File Offset: 0x00089DEC
		private List<Symbol> Analyze(ref bool badRegion)
		{
			base.Analyze(ref badRegion, null);
			List<Symbol> list = new List<Symbol>();
			if (this._endOfRegionState.Reachable)
			{
				foreach (int num in this._endOfRegionState.Assigned.TrueBits())
				{
					if (num < this.variableBySlot.Count)
					{
						LocalDataFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.VariableIdentifier variableIdentifier = this.variableBySlot[num];
						if (variableIdentifier.Exists && !(variableIdentifier.Symbol is FieldSymbol))
						{
							list.Add(variableIdentifier.Symbol);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0008BC98 File Offset: 0x00089E98
		protected override void WriteArgument(BoundExpression arg, RefKind refKind, MethodSymbol method)
		{
			if (refKind == RefKind.Out)
			{
				base.Assign(arg, null, false, true);
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0008BCA8 File Offset: 0x00089EA8
		protected override void ResolveBranch([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pending, LabelSymbol label, BoundStatement target, ref bool labelStateChanged)
		{
			if (base.IsInside && pending.Branch != null && !base.RegionContains(pending.Branch.Syntax.Span))
			{
				pending.State = (pending.State.Reachable ? this.TopState() : this.UnreachableState());
			}
			base.ResolveBranch(pending, label, target, ref labelStateChanged);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0008BD09 File Offset: 0x00089F09
		public override BoundNode VisitLabel(BoundLabel node)
		{
			this.ResolveLabel(node, node.Label);
			return base.VisitLabel(node);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0008BD1F File Offset: 0x00089F1F
		public override BoundNode VisitLabeledStatement(BoundLabeledStatement node)
		{
			this.ResolveLabel(node, node.Label);
			return base.VisitLabeledStatement(node);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0008BD35 File Offset: 0x00089F35
		private void ResolveLabel(BoundNode node, LabelSymbol label)
		{
			if (node.Syntax != null && base.RegionContains(node.Syntax.Span))
			{
				this._labelsInside.Add(label);
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0008BD5F File Offset: 0x00089F5F
		protected override void EnterRegion()
		{
			this.State = this.TopState();
			base.EnterRegion();
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0008BD74 File Offset: 0x00089F74
		protected override void LeaveRegion()
		{
			if (this.IsConditionalState)
			{
				this._endOfRegionState = this.StateWhenTrue.Clone();
				this.Join(ref this._endOfRegionState, ref this.StateWhenFalse);
			}
			else
			{
				this._endOfRegionState = this.State.Clone();
			}
			foreach (AbstractFlowPass<DefiniteAssignmentPass.LocalState, DefiniteAssignmentPass.LocalFunctionState>.PendingBranch pendingBranch in base.PendingBranches)
			{
				if (pendingBranch.Branch != null && base.RegionContains(pendingBranch.Branch.Syntax.Span) && !this._labelsInside.Contains(pendingBranch.Label))
				{
					this.Join(ref this._endOfRegionState, ref pendingBranch.State);
				}
			}
			base.LeaveRegion();
		}

		// Token: 0x04000E03 RID: 3587
		private DefiniteAssignmentPass.LocalState _endOfRegionState;

		// Token: 0x04000E04 RID: 3588
		private readonly HashSet<LabelSymbol> _labelsInside = new HashSet<LabelSymbol>();
	}
}
