using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001364 RID: 4964
	[Serializable]
	public class Physic : BaseSkill
	{
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600C13D RID: 49469 RVA: 0x0046B4AF File Offset: 0x004696AF
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
