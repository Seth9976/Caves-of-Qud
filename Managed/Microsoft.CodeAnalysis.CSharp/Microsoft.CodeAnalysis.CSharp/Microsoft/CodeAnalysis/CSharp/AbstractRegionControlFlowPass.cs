using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000148 RID: 328
	internal abstract class AbstractRegionControlFlowPass : ControlFlowPass
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x0008BA05 File Offset: 0x00089C05
		internal AbstractRegionControlFlowPass(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion)
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0008BA14 File Offset: 0x00089C14
		public override BoundNode Visit(BoundNode node)
		{
			base.VisitAlways(node);
			return null;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0008BA20 File Offset: 0x00089C20
		public override BoundNode VisitLambda(BoundLambda node)
		{
			AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.SavedPending savedPending = base.SavePending();
			ControlFlowPass.LocalState state = this.State;
			this.State = this.TopState();
			AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.SavedPending savedPending2 = base.SavePending();
			base.VisitAlways(node.Body);
			base.RestorePending(savedPending2);
			ImmutableArray<AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch> immutableArray = this.RemoveReturns();
			base.RestorePending(savedPending);
			this.Join(ref state, ref this.State);
			foreach (AbstractFlowPass<ControlFlowPass.LocalState, ControlFlowPass.LocalFunctionState>.PendingBranch pendingBranch in immutableArray)
			{
				this.State = pendingBranch.State;
				this.Join(ref state, ref this.State);
			}
			this.State = state;
			return null;
		}
	}
}
