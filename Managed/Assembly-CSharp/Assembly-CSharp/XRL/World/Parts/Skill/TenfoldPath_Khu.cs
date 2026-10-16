using System;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B1 RID: 5041
	[Serializable]
	public class TenfoldPath_Khu : BaseInitiatorySkill
	{
		// Token: 0x0600C2AE RID: 49838 RVA: 0x00471D31 File Offset: 0x0046FF31
		public override bool AddSkill(GameObject Object)
		{
			base.StatShifter.SetStatShift("MA", 2, false);
			return base.AddSkill(Object);
		}

		// Token: 0x0600C2AF RID: 49839 RVA: 0x00471D4D File Offset: 0x0046FF4D
		public override bool RemoveSkill(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			return base.RemoveSkill(Object);
		}
	}
}
