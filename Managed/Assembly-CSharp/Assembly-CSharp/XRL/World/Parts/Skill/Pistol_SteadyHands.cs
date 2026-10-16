using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200136D RID: 4973
	[Serializable]
	public class Pistol_SteadyHands : BaseSkill
	{
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600C162 RID: 49506 RVA: 0x0046B9C8 File Offset: 0x00469BC8
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
