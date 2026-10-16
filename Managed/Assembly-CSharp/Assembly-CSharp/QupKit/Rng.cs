using System;

namespace QupKit
{
	// Token: 0x020003A7 RID: 935
	public static class Rng
	{
		// Token: 0x06002300 RID: 8960 RVA: 0x000B1EE0 File Offset: 0x000B00E0
		public static string GetRandomDirection()
		{
			int num = Rng.Next(0, 3);
			if (num == 0)
			{
				return "N";
			}
			if (num == 1)
			{
				return "S";
			}
			if (num == 2)
			{
				return "E";
			}
			if (num == 3)
			{
				return "W";
			}
			return "N";
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000B1F21 File Offset: 0x000B0121
		public static int Next(int Low, int High)
		{
			if (Rng.R == null)
			{
				Rng.R = new Random();
			}
			return Rng.R.Next(Low, High + 1);
		}

		// Token: 0x040011A3 RID: 4515
		private static Random R;
	}
}
