using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200134C RID: 4940
	[Serializable]
	public class LongBladesImprovedDefensiveStance : LongBladesSkillBase
	{
		// Token: 0x0600C09F RID: 49311 RVA: 0x0046838C File Offset: 0x0046658C
		public override bool AddSkill(GameObject GO)
		{
			GO.FireEvent("LongBlades.UpdateStance");
			return base.AddSkill(GO);
		}

		// Token: 0x0600C0A0 RID: 49312 RVA: 0x004683A1 File Offset: 0x004665A1
		public override bool RemoveSkill(GameObject GO)
		{
			GO.FireEvent("LongBlades.UpdateStance");
			return base.RemoveSkill(GO);
		}
	}
}
