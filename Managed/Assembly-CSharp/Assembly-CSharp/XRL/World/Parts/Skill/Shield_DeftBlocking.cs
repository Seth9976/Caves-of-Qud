using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001382 RID: 4994
	[Serializable]
	public class Shield_DeftBlocking : BaseSkill
	{
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600C1BB RID: 49595 RVA: 0x0046CC7F File Offset: 0x0046AE7F
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
