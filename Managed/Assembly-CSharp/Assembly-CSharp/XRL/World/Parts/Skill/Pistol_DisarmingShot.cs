using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001368 RID: 4968
	[Serializable]
	public class Pistol_DisarmingShot : BaseSkill
	{
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600C14E RID: 49486 RVA: 0x0046B6CC File Offset: 0x004698CC
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
