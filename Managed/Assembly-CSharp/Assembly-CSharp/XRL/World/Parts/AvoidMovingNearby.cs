using System;

namespace XRL.World.Parts
{
	// Token: 0x020011E5 RID: 4581
	[Serializable]
	public class AvoidMovingNearby : IPart
	{
		// Token: 0x0600B681 RID: 46721 RVA: 0x0042979C File Offset: 0x0042799C
		public override bool SameAs(IPart p)
		{
			AvoidMovingNearby avoidMovingNearby = p as AvoidMovingNearby;
			return avoidMovingNearby.Weight == this.Weight && avoidMovingNearby.RespectPhase == this.RespectPhase && base.SameAs(p);
		}

		// Token: 0x0600B682 RID: 46722 RVA: 0x004297D7 File Offset: 0x004279D7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600B683 RID: 46723 RVA: 0x004297F5 File Offset: 0x004279F5
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!this.RespectPhase || E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(this.Weight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B684 RID: 46724 RVA: 0x00429820 File Offset: 0x00427A20
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!this.RespectPhase || E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(this.Weight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400499E RID: 18846
		public int Weight = 2;

		// Token: 0x0400499F RID: 18847
		public bool RespectPhase = true;
	}
}
