using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B11 RID: 2833
	public class WatervineCaravanserai : ZoneBuilderSandbox
	{
		// Token: 0x060071AB RID: 29099 RVA: 0x002A785B File Offset: 0x002A5A5B
		public static void Save(SerializationWriter Writer)
		{
			Writer.Write(0);
		}

		// Token: 0x060071AC RID: 29100 RVA: 0x002A7864 File Offset: 0x002A5A64
		public static void Load(SerializationReader Reader)
		{
			if (Reader.ReadInt32() == 0)
			{
				WatervineCaravanserai.WatervineNoise = null;
				return;
			}
			WatervineCaravanserai.WatervineNoise = null;
			for (int i = 0; i < 375; i++)
			{
				for (int j = 0; j < 1200; j++)
				{
					Reader.ReadDouble();
				}
			}
		}

		// Token: 0x060071AD RID: 29101 RVA: 0x002A78AD File Offset: 0x002A5AAD
		public static bool InBlackoutArea(int x, int y)
		{
			return (x >= 42 && x <= 55) || (y >= 9 && y <= 16);
		}

		// Token: 0x060071AE RID: 29102 RVA: 0x002A78C8 File Offset: 0x002A5AC8
		public bool BuildZone(Zone Z)
		{
			if (!this.Underground)
			{
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("Dirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			NoiseMap noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, Stat.Random(30, 60), Stat.Random(50, 70), Stat.Random(125, 135), 0, 10, 0, 1, list, 5);
			if (WatervineCaravanserai.WatervineNoise == null)
			{
				Random seededRand = base.GetSeededRand("WatervineNoise");
				WatervineCaravanserai.WatervineNoise = PerlinNoise2Df.sumNoiseFunctions(1200, 375, 0, 0, new List<PerlinNoise2Df>
				{
					new PerlinNoise2Df(8, 0.86f, seededRand),
					new PerlinNoise2Df(32, 0.72f, seededRand),
					new PerlinNoise2Df(64, 0.6f, seededRand),
					new PerlinNoise2Df(128, 0.48f, seededRand),
					new PerlinNoise2Df(300, 0.36f, seededRand),
					new PerlinNoise2Df(600, 0.12f, seededRand),
					new PerlinNoise2Df(1200, 0.06f, seededRand)
				}, 0.5f);
				WatervineCaravanserai.WatervineNoise = PerlinNoise2Df.Smooth(WatervineCaravanserai.WatervineNoise, 1200, 375, 1);
			}
			int num = Z.wX * 240 + Z.X * 80;
			int num2 = Z.wY * 75 + Z.Y * 25;
			num %= 1200;
			num2 %= 375;
			double[,] array = new double[Z.Width, Z.Height];
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					array[j, i] = 0.0;
				}
			}
			if (Z.X == 0 && Z.wX > 0 && !ZoneManager.GetObjectTypeForZone(Z.wX - 1, Z.wY, Z.GetZoneWorld()).Contains("TerrainSaltmarsh"))
			{
				for (int k = 0; k < Z.Height; k++)
				{
					for (int l = 0; l < 10; l++)
					{
						array[l, k] = -1.0 + 0.05 * (double)l;
					}
				}
			}
			if (Z.X == 2 && Z.wX < 79)
			{
				string objectTypeForZone = ZoneManager.GetObjectTypeForZone(Z.wX + 1, Z.wY, Z.GetZoneWorld());
				if (!objectTypeForZone.Contains("TerrainSaltmarsh") && !objectTypeForZone.Contains("TerrainWatervine"))
				{
					for (int m = 0; m < Z.Height; m++)
					{
						for (int n = Z.Width - 10; n < Z.Width; n++)
						{
							array[n, m] = -1.0 + 0.05 * (double)(Z.Width - n);
						}
					}
				}
			}
			if (Z.Y == 0 && Z.wY > 0)
			{
				string objectTypeForZone2 = ZoneManager.GetObjectTypeForZone(Z.wX, Z.wY - 1, Z.GetZoneWorld());
				if (!objectTypeForZone2.Contains("TerrainSaltmarsh") && !objectTypeForZone2.Contains("TerrainWatervine"))
				{
					for (int num3 = 0; num3 < 10; num3++)
					{
						for (int num4 = 0; num4 < Z.Width; num4++)
						{
							array[num4, num3] = -1.0 + 0.05 * (double)num3;
						}
					}
				}
			}
			if (Z.Y == 2 && Z.wY < 24)
			{
				string objectTypeForZone3 = ZoneManager.GetObjectTypeForZone(Z.wX, Z.wY + 1, Z.GetZoneWorld());
				if (!objectTypeForZone3.Contains("TerrainSaltmarsh") && !objectTypeForZone3.Contains("TerrainWatervine"))
				{
					for (int num5 = Z.Height - 10; num5 < Z.Height; num5++)
					{
						for (int num6 = 0; num6 < Z.Width; num6++)
						{
							array[num6, num5] = -1.0 + 0.05 * (double)(Z.Height - num5);
						}
					}
				}
			}
			if (Z.X == 0 && Z.wX > 0)
			{
				string objectTypeForZone4 = ZoneManager.GetObjectTypeForZone(Z.wX - 1, Z.wY, Z.GetZoneWorld());
				if (!objectTypeForZone4.Contains("TerrainSaltmarsh") && !objectTypeForZone4.Contains("TerrainWatervine"))
				{
					for (int num7 = 0; num7 < Z.Height; num7++)
					{
						for (int num8 = 0; num8 < 10; num8++)
						{
							array[num8, num7] = -1.0 + 0.1 * (double)num8;
						}
					}
				}
			}
			for (int num9 = 0; num9 < Z.Height; num9++)
			{
				for (int num10 = 0; num10 < Z.Width; num10++)
				{
					if (!this.Underground || Z.GetCell(num10, num9).IsPassable(null, true))
					{
						double num11 = (double)WatervineCaravanserai.WatervineNoise[num10 + num, num9 + num2] + array[num10, num9];
						if (noiseMap.Noise[num10, num9] > 3 && !WatervineCaravanserai.InBlackoutArea(num10, num9))
						{
							Z.GetCell(num10, num9).AddObject(GameObjectFactory.Factory.CreateObject("SaltyWaterPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						else if (num11 >= 0.7 && !WatervineCaravanserai.InBlackoutArea(num10, num9))
						{
							Z.GetCell(num10, num9).AddObject(GameObjectFactory.Factory.CreateObject("SaltyWaterPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						else if (num11 >= 0.6)
						{
							if (Stat.Random(1, 100) <= 80 && !WatervineCaravanserai.InBlackoutArea(num10, num9))
							{
								Z.GetCell(num10, num9).AddObject(GameObjectFactory.Factory.CreateObject("Watervine"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else
						{
							Z.GetCell(num10, num9).SetReachable(true);
						}
					}
				}
			}
			if (!this.Underground)
			{
				Z.ClearReachableMap();
				if (Z.BuildReachableMap(0, 0) < 400)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040034C4 RID: 13508
		public static float[,] WatervineNoise;

		// Token: 0x040034C5 RID: 13509
		public const int MaxWidth = 1200;

		// Token: 0x040034C6 RID: 13510
		public const int MaxHeight = 375;

		// Token: 0x040034C7 RID: 13511
		public const int XBlackoutMin = 42;

		// Token: 0x040034C8 RID: 13512
		public const int XBlackoutMax = 55;

		// Token: 0x040034C9 RID: 13513
		public const int YBlackoutMin = 9;

		// Token: 0x040034CA RID: 13514
		public const int YBlackoutMax = 16;

		// Token: 0x040034CB RID: 13515
		public bool Underground;
	}
}
