using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001346 RID: 4934
	[Serializable]
	public class HeavyWeapons_Tank : BaseSkill
	{
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600C07F RID: 49279 RVA: 0x00468038 File Offset: 0x00466238
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
