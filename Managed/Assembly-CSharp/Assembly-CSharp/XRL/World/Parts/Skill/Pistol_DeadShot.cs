using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001367 RID: 4967
	[Serializable]
	public class Pistol_DeadShot : BaseSkill
	{
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600C14C RID: 49484 RVA: 0x0046B6BD File Offset: 0x004698BD
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
