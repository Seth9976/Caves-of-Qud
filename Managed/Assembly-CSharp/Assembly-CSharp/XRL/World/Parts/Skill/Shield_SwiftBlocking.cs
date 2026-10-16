using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001387 RID: 4999
	[Serializable]
	public class Shield_SwiftBlocking : BaseSkill
	{
		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600C1D8 RID: 49624 RVA: 0x0046D6CD File Offset: 0x0046B8CD
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
