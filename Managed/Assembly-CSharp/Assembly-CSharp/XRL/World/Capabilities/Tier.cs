using System;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F9 RID: 2553
	public static class Tier
	{
		// Token: 0x06006CDB RID: 27867 RVA: 0x00236DFC File Offset: 0x00234FFC
		public static int Constrain(int Tier)
		{
			return Math.Min(Math.Max(Tier, 1), 8);
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x00236E0B File Offset: 0x0023500B
		public static void Constrain(ref int Tier)
		{
			Tier = Tier.Constrain(Tier);
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x00236E16 File Offset: 0x00235016
		public static int Fuzz(int Tier, int Chance = 5)
		{
			while (Chance.in100())
			{
				Tier--;
			}
			Tier.Constrain(ref Tier);
			while (Chance.in100())
			{
				Tier++;
			}
			Tier.Constrain(ref Tier);
			return Tier;
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x00236E45 File Offset: 0x00235045
		public static void Fuzz(ref int Tier, int Chance = 5)
		{
			Tier = Tier.Fuzz(Tier, 5);
		}

		// Token: 0x040031DA RID: 12762
		public const int MINIMUM = 1;

		// Token: 0x040031DB RID: 12763
		public const int MAXIMUM = 8;
	}
}
