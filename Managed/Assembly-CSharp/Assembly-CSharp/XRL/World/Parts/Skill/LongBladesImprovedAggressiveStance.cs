using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200134B RID: 4939
	[Serializable]
	public class LongBladesImprovedAggressiveStance : LongBladesSkillBase
	{
		// Token: 0x0600C09C RID: 49308 RVA: 0x0046835A File Offset: 0x0046655A
		public override bool AddSkill(GameObject GO)
		{
			GO.FireEvent("LongBlades.UpdateStance");
			return base.AddSkill(GO);
		}

		// Token: 0x0600C09D RID: 49309 RVA: 0x0046836F File Offset: 0x0046656F
		public override bool RemoveSkill(GameObject GO)
		{
			GO.FireEvent("LongBlades.UpdateStance");
			return base.RemoveSkill(GO);
		}
	}
}
