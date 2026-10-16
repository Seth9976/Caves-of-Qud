using System;

namespace XRL.World.Capabilities
{
	// Token: 0x020009EE RID: 2542
	public static class MatterPhase
	{
		// Token: 0x06006C80 RID: 27776 RVA: 0x00233940 File Offset: 0x00231B40
		public static string getName(int matterPhase)
		{
			switch (matterPhase)
			{
			case 1:
				return "solid";
			case 2:
				return "liquid";
			case 3:
				return "gas";
			case 4:
				return "plasma";
			default:
				throw new Exception("invalid matter phase " + matterPhase.ToString());
			}
		}

		// Token: 0x06006C81 RID: 27777 RVA: 0x00233995 File Offset: 0x00231B95
		public static int getMatterPhase(GameObject obj)
		{
			return GetMatterPhaseEvent.GetFor(obj, 1);
		}

		// Token: 0x040031B7 RID: 12727
		public const int SOLID = 1;

		// Token: 0x040031B8 RID: 12728
		public const int LIQUID = 2;

		// Token: 0x040031B9 RID: 12729
		public const int GAS = 3;

		// Token: 0x040031BA RID: 12730
		public const int PLASMA = 4;
	}
}
