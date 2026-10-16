using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001342 RID: 4930
	[Serializable]
	public class Endurance_Weathered : BaseSkill
	{
		// Token: 0x0600C068 RID: 49256 RVA: 0x00467B52 File Offset: 0x00465D52
		public override bool AddSkill(GameObject GO)
		{
			base.StatShifter.SetStatShift("HeatResistance", 15, false);
			base.StatShifter.SetStatShift("ColdResistance", 15, false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C069 RID: 49257 RVA: 0x00467B83 File Offset: 0x00465D83
		public override bool RemoveSkill(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.RemoveSkill(GO);
		}
	}
}
