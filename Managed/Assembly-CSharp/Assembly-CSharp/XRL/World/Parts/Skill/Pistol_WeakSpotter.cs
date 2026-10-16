using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200136E RID: 4974
	[Serializable]
	public class Pistol_WeakSpotter : BaseSkill
	{
		// Token: 0x0600C164 RID: 49508 RVA: 0x0046B9D7 File Offset: 0x00469BD7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCriticalThresholdEvent>.ID;
		}

		// Token: 0x0600C165 RID: 49509 RVA: 0x0046B9ED File Offset: 0x00469BED
		public override bool HandleEvent(GetCriticalThresholdEvent E)
		{
			if (E.Attacker == this.ParentObject && E.Skill == "Pistol")
			{
				E.Threshold--;
			}
			return base.HandleEvent(E);
		}
	}
}
