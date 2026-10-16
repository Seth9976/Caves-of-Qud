using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001398 RID: 5016
	[Serializable]
	public class Survival : BaseSkill
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600C22F RID: 49711 RVA: 0x0046EFB8 File Offset: 0x0046D1B8
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
