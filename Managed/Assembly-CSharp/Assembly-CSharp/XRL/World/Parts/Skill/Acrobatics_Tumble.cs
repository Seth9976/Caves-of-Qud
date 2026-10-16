using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200131A RID: 4890
	[Serializable]
	public class Acrobatics_Tumble : BaseSkill
	{
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600BF3C RID: 48956 RVA: 0x00460E2F File Offset: 0x0045F02F
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0600BF3D RID: 48957 RVA: 0x00460E36 File Offset: 0x0045F036
		public override bool AddSkill(GameObject GO)
		{
			base.StatShifter.SetStatShift("DV", 1, false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BF3E RID: 48958 RVA: 0x00460E52 File Offset: 0x0045F052
		public override bool RemoveSkill(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.RemoveSkill(GO);
		}
	}
}
