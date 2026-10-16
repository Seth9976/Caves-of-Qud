using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200136A RID: 4970
	[Serializable]
	public class Pistol_FastestGun : BaseSkill
	{
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600C15C RID: 49500 RVA: 0x0046B971 File Offset: 0x00469B71
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
