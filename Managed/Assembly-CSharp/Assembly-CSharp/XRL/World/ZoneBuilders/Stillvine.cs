using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B0F RID: 2831
	public class Stillvine : ZoneBuilderSandbox
	{
		// Token: 0x060071A5 RID: 29093 RVA: 0x002A7358 File Offset: 0x002A5558
		public static void Save(SerializationWriter Writer)
		{
			Writer.Write(0);
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x002A7364 File Offset: 0x002A5564
		public static void Load(SerializationReader Reader)
		{
			if (Reader.ReadInt32() == 0)
			{
				Stillvine.WatervineNoise = null;
				return;
			}
			Stillvine.WatervineNoise = null;
			for (int i = 0; i < 375; i++)
			{
				for (int j = 0; j < 1200; j++)
				{
					Reader.ReadDouble();
				}
			}
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x002A73B0 File Offset: 0x002A55B0
		public bool BuildZone(Zone Z)
		{
			if (!this.Underground)
			{
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("Dirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			else
			{
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("PaleDirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			NoiseMap noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, Stat.Random(30, 60), Stat.Random(50, 70), Stat.Random(125, 135), 0, 10, 0, 1, list, 5);
			if (Stillvine.WatervineNoise == null)
			{
				Random seededRand = base.GetSeededRand("WatervineNoise");
				Stillvine.WatervineNoise = PerlinNoise2Df.sumNoiseFunctions(1200, 375, 0, 0, new List<PerlinNoise2Df>
				{
					new PerlinNoise2Df(8, 0.86f, seededRand),
					new PerlinNoise2Df(32, 0.72f, seededRand),
					new PerlinNoise2Df(64, 0.6f, seededRand),
					new PerlinNoise2Df(128, 0.48f, seededRand),
					new PerlinNoise2Df(300, 0.36f, seededRand),
					new PerlinNoise2Df(600, 0.12f, seededRand),
					new PerlinNoise2Df(1200, 0.06f, seededRand)
				}, 0.5f);
				Stillvine.WatervineNoise = PerlinNoise2Df.Smooth(Stillvine.WatervineNoise, 1200, 375, 1);
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
			for (int k = 0; k < Z.Height; k++)
			{
				for (int l = 0; l < Z.Width; l++)
				{
					if (!this.Underground || Z.GetCell(l, k).IsPassable(null, true))
					{
						double num3 = (double)Stillvine.WatervineNoise[l + num, k + num2] + array[l, k];
						if (noiseMap.Noise[l, k] > 3)
						{
							Z.GetCell(l, k).AddObject(GameObjectFactory.Factory.CreateObject("SaltyWaterPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						else if (num3 >= 0.7)
						{
							Z.GetCell(l, k).AddObject(GameObjectFactory.Factory.CreateObject("SaltyWaterPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						else if (num3 >= 0.6)
						{
							if (Stat.Random(1, 100) <= 80)
							{
								Z.GetCell(l, k).AddObject(GameObjectFactory.Factory.CreateObject("Stillvine"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else
						{
							Z.GetCell(l, k).SetReachable(true);
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

		// Token: 0x040034C0 RID: 13504
		public static float[,] WatervineNoise;

		// Token: 0x040034C1 RID: 13505
		public const int MaxWidth = 1200;

		// Token: 0x040034C2 RID: 13506
		public const int MaxHeight = 375;

		// Token: 0x040034C3 RID: 13507
		public bool Underground;
	}
}
