using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001381 RID: 4993
	[Serializable]
	public class Shield_Block : BaseSkill
	{
		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600C1B9 RID: 49593 RVA: 0x0046CC70 File Offset: 0x0046AE70
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
