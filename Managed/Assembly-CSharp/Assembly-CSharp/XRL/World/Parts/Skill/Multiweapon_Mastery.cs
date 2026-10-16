using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001355 RID: 4949
	[Serializable]
	public class Multiweapon_Mastery : BaseSkill
	{
		// Token: 0x0600C0CB RID: 49355 RVA: 0x00468A87 File Offset: 0x00466C87
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600C0CC RID: 49356 RVA: 0x00468A9D File Offset: 0x00466C9D
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary)
			{
				E.Chance += 15;
			}
			return base.HandleEvent(E);
		}
	}
}
