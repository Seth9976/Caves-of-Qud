using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001317 RID: 4887
	[Serializable]
	public class Acrobatics_Dodge : BaseSkill
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600BF13 RID: 48915 RVA: 0x0045F93C File Offset: 0x0045DB3C
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0600BF14 RID: 48916 RVA: 0x0045F943 File Offset: 0x0045DB43
		public override bool AddSkill(GameObject GO)
		{
			base.StatShifter.SetStatShift("DV", 2, false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600BF15 RID: 48917 RVA: 0x0045F95F File Offset: 0x0045DB5F
		public override bool RemoveSkill(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.RemoveSkill(GO);
		}
	}
}
