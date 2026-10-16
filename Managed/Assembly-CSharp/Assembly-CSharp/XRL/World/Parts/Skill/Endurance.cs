using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200133C RID: 4924
	[Serializable]
	public class Endurance : BaseSkill
	{
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600C04E RID: 49230 RVA: 0x004675EA File Offset: 0x004657EA
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
