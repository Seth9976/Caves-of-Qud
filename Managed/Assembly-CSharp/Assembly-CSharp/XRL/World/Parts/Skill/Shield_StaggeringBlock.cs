using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001386 RID: 4998
	[Serializable]
	public class Shield_StaggeringBlock : BaseSkill
	{
		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600C1D6 RID: 49622 RVA: 0x0046D6BE File Offset: 0x0046B8BE
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
