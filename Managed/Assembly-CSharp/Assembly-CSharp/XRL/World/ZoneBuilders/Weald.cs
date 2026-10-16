using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B13 RID: 2835
	public class Weald
	{
		// Token: 0x060071B2 RID: 29106 RVA: 0x002A8ADC File Offset: 0x002A6CDC
		public bool BuildZone(Zone Z)
		{
			if (Weald.WealdNoise == null)
			{
				Weald.NoiseFunctions = new List<PerlinNoise2D>();
				Weald.NoiseFunctions.Add(new PerlinNoise2D(1, 3f, Stat.Rand));
				Weald.NoiseFunctions.Add(new PerlinNoise2D(4, 2f, Stat.Rand));
				Weald.NoiseFunctions.Add(new PerlinNoise2D(8, 1f, Stat.Rand));
				Weald.NoiseFunctions.Add(new PerlinNoise2D(16, 2f, Stat.Rand));
				Weald.NoiseFunctions.Add(new PerlinNoise2D(32, 3f, Stat.Rand));
				Weald.NoiseFunctions.Add(new PerlinNoise2D(128, 4f, Stat.Rand));
				Weald.WealdNoise = PerlinNoise2D.sumNoiseFunctions(1200, 375, 0, 0, Weald.NoiseFunctions);
			}
			int num = Z.wX * 240 + Z.X * 80;
			int num2 = Z.wY * 75 + Z.Y * 25;
			num %= 1200;
			num2 %= 375;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					double num3 = Weald.WealdNoise[j + num, i + num2];
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					if (num3 >= 0.8)
					{
						num4 = 20;
						num5 = 80;
						num6 = 5;
					}
					else if (num3 >= 0.7)
					{
						num4 = 3;
						num5 = 60;
						num6 = 3;
					}
					else if (num3 >= 0.5)
					{
						num4 = 1;
						num5 = 1;
						num6 = 30;
					}
					if (num4.in100())
					{
						Z.GetCell(j, i).AddObject("Starapple Tree", null, null, null, null);
					}
					else if (num5.in100())
					{
						Z.GetCell(j, i).AddObject("Witchwood Tree", null, null, null, null);
					}
					else if (num6.in100())
					{
						Z.GetCell(j, i).AddObject("Yuckwheat", null, null, null, null);
					}
				}
			}
			Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			Z.GetCell(0, 0).AddObject("Grassy", null, null, null, null);
			Z.ClearReachableMap();
			return Z.BuildReachableMap(0, 0) >= 400;
		}

		// Token: 0x040034CC RID: 13516
		private static List<PerlinNoise2D> NoiseFunctions;

		// Token: 0x040034CD RID: 13517
		private static double[,] WealdNoise;
	}
}
