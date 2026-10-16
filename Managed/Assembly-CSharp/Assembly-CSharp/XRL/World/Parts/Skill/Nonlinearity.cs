using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001357 RID: 4951
	[Serializable]
	public class Nonlinearity : BaseSkill
	{
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600C0D1 RID: 49361 RVA: 0x00468B13 File Offset: 0x00466D13
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
