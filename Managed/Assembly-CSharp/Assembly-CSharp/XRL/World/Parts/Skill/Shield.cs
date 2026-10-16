using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001383 RID: 4995
	[Serializable]
	public class Shield : BaseSkill
	{
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600C1BD RID: 49597 RVA: 0x0046CC8E File Offset: 0x0046AE8E
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
