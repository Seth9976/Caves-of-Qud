using System;

namespace XRL.World.Parts
{
	// Token: 0x020011E6 RID: 4582
	[Serializable]
	public class AvoidMovingOnto : IPart
	{
		// Token: 0x0600B686 RID: 46726 RVA: 0x00429864 File Offset: 0x00427A64
		public override bool SameAs(IPart p)
		{
			AvoidMovingOnto avoidMovingOnto = p as AvoidMovingOnto;
			return avoidMovingOnto.Weight == this.Weight && avoidMovingOnto.RespectPhase == this.RespectPhase && base.SameAs(p);
		}

		// Token: 0x0600B687 RID: 46727 RVA: 0x0042989F File Offset: 0x00427A9F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600B688 RID: 46728 RVA: 0x004298B5 File Offset: 0x00427AB5
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!this.RespectPhase || E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(this.Weight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040049A0 RID: 18848
		public int Weight = 1;

		// Token: 0x040049A1 RID: 18849
		public bool RespectPhase = true;
	}
}
