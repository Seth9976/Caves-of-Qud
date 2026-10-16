using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001352 RID: 4946
	[Serializable]
	public class Multiweapon_Expertise : BaseSkill
	{
		// Token: 0x0600C0BA RID: 49338 RVA: 0x00468603 File Offset: 0x00466803
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600C0BB RID: 49339 RVA: 0x00468619 File Offset: 0x00466819
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
