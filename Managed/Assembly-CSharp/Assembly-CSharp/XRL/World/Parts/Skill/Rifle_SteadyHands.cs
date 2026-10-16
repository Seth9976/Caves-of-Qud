using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001376 RID: 4982
	[Serializable]
	public class Rifle_SteadyHands : BaseSkill
	{
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600C18A RID: 49546 RVA: 0x0046C34E File Offset: 0x0046A54E
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
