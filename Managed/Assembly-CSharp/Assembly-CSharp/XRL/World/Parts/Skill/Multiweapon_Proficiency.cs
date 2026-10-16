using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001356 RID: 4950
	[Serializable]
	public class Multiweapon_Proficiency : BaseSkill
	{
		// Token: 0x0600C0CE RID: 49358 RVA: 0x00468ACD File Offset: 0x00466CCD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600C0CF RID: 49359 RVA: 0x00468AE3 File Offset: 0x00466CE3
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary)
			{
				E.Chance += 20;
			}
			return base.HandleEvent(E);
		}
	}
}
