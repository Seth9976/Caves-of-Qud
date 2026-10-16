using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001337 RID: 4919
	[Serializable]
	public class Customs : BaseSkill
	{
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600C033 RID: 49203 RVA: 0x00467229 File Offset: 0x00465429
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
