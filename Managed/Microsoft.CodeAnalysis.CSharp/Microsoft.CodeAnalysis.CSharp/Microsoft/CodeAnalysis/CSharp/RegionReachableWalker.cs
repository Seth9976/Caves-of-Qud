using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200015A RID: 346
	internal class RegionReachableWalker : AbstractRegionControlFlowPass
	{
		// Token: 0x06001D7B RID: 7547 RVA: 0x0009F33C File Offset: 0x0009D53C
		internal static void Analyze(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion, out bool startPointIsReachable, out bool endPointIsReachable)
		{
			RegionReachableWalker regionReachableWalker = new RegionReachableWalker(compilation, member, node, firstInRegion, lastInRegion);
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			bool flag = false;
			try
			{
				regionReachableWalker.Analyze(ref flag, instance);
				startPointIsReachable = flag || regionReachableWalker._regionStartPointIsReachable.GetValueOrDefault(true);
				endPointIsReachable = flag || regionReachableWalker._regionEndPointIsReachable.GetValueOrDefault(regionReachableWalker.State.Alive);
			}
			finally
			{
				instance.Free();
				regionReachableWalker.Free();
			}
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0009F3BC File Offset: 0x0009D5BC
		private RegionReachableWalker(CSharpCompilation compilation, Symbol member, BoundNode node, BoundNode firstInRegion, BoundNode lastInRegion)
			: base(compilation, member, node, firstInRegion, lastInRegion)
		{
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0009F3CB File Offset: 0x0009D5CB
		protected override void EnterRegion()
		{
			this._regionStartPointIsReachable = new bool?(this.State.Alive);
			base.EnterRegion();
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0009F3E9 File Offset: 0x0009D5E9
		protected override void LeaveRegion()
		{
			this._regionEndPointIsReachable = new bool?(this.State.Alive);
			base.LeaveRegion();
		}

		// Token: 0x04000E64 RID: 3684
		private bool? _regionStartPointIsReachable;

		// Token: 0x04000E65 RID: 3685
		private bool? _regionEndPointIsReachable;
	}
}
