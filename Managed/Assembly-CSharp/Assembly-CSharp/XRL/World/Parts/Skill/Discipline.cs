using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200137B RID: 4987
	[Serializable]
	public class Discipline : BaseSkill
	{
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600C19D RID: 49565 RVA: 0x0046C7D7 File Offset: 0x0046A9D7
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
