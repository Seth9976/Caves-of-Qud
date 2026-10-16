using System;

namespace XRL.World.AI
{
	// Token: 0x02000B69 RID: 2921
	public abstract class IOpinionCombat : IOpinionObject
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600752F RID: 29999 RVA: 0x002BF2CE File Offset: 0x002BD4CE
		public override int Cooldown
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06007530 RID: 30000 RVA: 0x002BF2D1 File Offset: 0x002BD4D1
		public override float Limit
		{
			get
			{
				return 5f;
			}
		}
	}
}
