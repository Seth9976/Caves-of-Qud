using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200134D RID: 4941
	[Serializable]
	public class LongBladesImprovedDuelistStance : LongBladesSkillBase
	{
		// Token: 0x0600C0A2 RID: 49314 RVA: 0x004683BE File Offset: 0x004665BE
		public override bool AddSkill(GameObject GO)
		{
			GO.FireEvent("LongBlades.UpdateStance");
			return base.AddSkill(GO);
		}

		// Token: 0x0600C0A3 RID: 49315 RVA: 0x004683D3 File Offset: 0x004665D3
		public override bool RemoveSkill(GameObject GO)
		{
			GO.FireEvent("LongBlades.UpdateStance");
			return base.RemoveSkill(GO);
		}
	}
}
