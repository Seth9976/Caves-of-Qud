using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001316 RID: 4886
	[Serializable]
	public class Acrobatics : BaseSkill
	{
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600BF11 RID: 48913 RVA: 0x0045F92D File Offset: 0x0045DB2D
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
