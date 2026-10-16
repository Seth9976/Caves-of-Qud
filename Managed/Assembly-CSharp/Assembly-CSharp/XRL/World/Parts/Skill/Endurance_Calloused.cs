using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200133B RID: 4923
	[Serializable]
	public class Endurance_Calloused : BaseSkill
	{
		// Token: 0x0600C04B RID: 49227 RVA: 0x004675B2 File Offset: 0x004657B2
		public override bool AddSkill(GameObject GO)
		{
			base.StatShifter.SetStatShift("AV", 1, true);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C04C RID: 49228 RVA: 0x004675CE File Offset: 0x004657CE
		public override bool RemoveSkill(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.RemoveSkill(GO);
		}
	}
}
