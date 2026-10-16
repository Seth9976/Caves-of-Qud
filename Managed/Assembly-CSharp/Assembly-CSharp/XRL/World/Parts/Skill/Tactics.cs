using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013AA RID: 5034
	[Serializable]
	public class Tactics : BaseSkill
	{
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600C28D RID: 49805 RVA: 0x004717AE File Offset: 0x0046F9AE
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
