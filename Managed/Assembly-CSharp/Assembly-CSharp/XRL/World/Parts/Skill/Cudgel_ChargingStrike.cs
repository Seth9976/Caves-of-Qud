using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200132F RID: 4911
	[Serializable]
	public class Cudgel_ChargingStrike : BaseSkill
	{
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600BFF3 RID: 49139 RVA: 0x00464FF4 File Offset: 0x004631F4
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
