using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA4 RID: 2724
	public class JoppaOutskirtsRuins : ZoneBuilderSandbox
	{
		// Token: 0x06006F24 RID: 28452 RVA: 0x002690B0 File Offset: 0x002672B0
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("Dirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			NoiseMap noiseMap = new NoiseMap(Z.Width, Z.Height, 10, 1, 1, Stat.Random(30, 60), Stat.Random(50, 70), Stat.Random(125, 135), 0, 10, 0, 1, list, 5);
			if (Watervine.WatervineNoise == null)
			{
				Random seededRand = base.GetSeededRand("WatervineNoise");
				Watervine.WatervineNoise = PerlinNoise2Df.sumNoiseFunctions(1200, 375, 0, 0, new List<PerlinNoise2Df>
				{
					new PerlinNoise2Df(8, 0.86f, seededRand),
					new PerlinNoise2Df(32, 0.72f, seededRand),
					new PerlinNoise2Df(64, 0.6f, seededRand),
					new PerlinNoise2Df(128, 0.48f, seededRand),
					new PerlinNoise2Df(300, 0.36f, seededRand),
					new PerlinNoise2Df(600, 0.12f, seededRand),
					new PerlinNoise2Df(1200, 0.06f, seededRand)
				}, 0.5f);
				Watervine.WatervineNoise = PerlinNoise2Df.Smooth(Watervine.WatervineNoise, 1200, 375, 1);
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
					for (int l2 = 0; l2 < 10; l2++)
					{
						array[l2, k] = -1.0 + 0.05 * (double)l2;
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
					double num11 = (double)Watervine.WatervineNoise[num10 + num, num9 + num2] + array[num10, num9];
					if (noiseMap.Noise[num10, num9] >= 5)
					{
						Z.GetCell(num10, num9).AddObject(GameObjectFactory.Factory.CreateObject("SaltyWaterPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					else if ((double)noiseMap.Noise[num10, num9] >= 3.5)
					{
						Z.GetCell(num10, num9).AddObject(GameObjectFactory.Factory.CreateObject("Watervine"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					else if (num11 >= 0.8)
					{
						Z.GetCell(num10, num9).AddObject(GameObjectFactory.Factory.CreateObject("SaltyWaterPuddle"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					else if (num11 >= 0.7)
					{
						if (Stat.Random(1, 100) <= 98)
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
			if (Z.X == 1 && Z.Y == 0)
			{
				InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(Z, null, InfluenceMapSeedStrategy.RandomPointFurtherThan4, 600, null, false, null);
				if (influenceMap.Regions.Count > 0)
				{
					InfluenceMapRegion randomElement = influenceMap.Regions.GetRandomElement<InfluenceMapRegion>(null);
					Rect2D rect2D = GridTools.MaxRectByArea(randomElement.GetGrid(), false, 0, 0).Translate(randomElement.BoundingBox.UpperLeft);
					if (rect2D.x1 == 0)
					{
						rect2D = new Rect2D(1, rect2D.y1, rect2D.x2, rect2D.y2);
					}
					if (rect2D.x2 == Z.Width - 1)
					{
						rect2D = new Rect2D(rect2D.x1, rect2D.y1, Z.Width - 2, rect2D.y2);
					}
					if (rect2D.y1 == 0)
					{
						rect2D = new Rect2D(rect2D.x1, 1, rect2D.x2, rect2D.y2);
					}
					if (rect2D.y2 == Z.Height - 1)
					{
						rect2D = new Rect2D(rect2D.x1, rect2D.y1, rect2D.x2, Z.Height - 2);
					}
					string text = "?";
					if (randomElement.Center.X <= 40)
					{
						text = "E";
					}
					if (randomElement.Center.X >= 40)
					{
						text = "W";
					}
					if (rect2D.y2 > 16)
					{
						text = "N";
					}
					if (rect2D.y1 < 8)
					{
						text = "S";
					}
					if (rect2D.y1 <= 0)
					{
						text = "S";
					}
					if (rect2D.y2 >= 24)
					{
						text = "N";
					}
					if (rect2D.y1 <= 0 && rect2D.y2 >= 24)
					{
						if (randomElement.Center.X <= 40)
						{
							text = "E";
						}
						if (randomElement.Center.X >= 40)
						{
							text = "W";
						}
					}
					Point2D randomDoorCell = rect2D.GetRandomDoorCell(text, 2);
					string cellSide = rect2D.GetCellSide(randomDoorCell);
					Rect2D rect2D2 = rect2D.ReduceBy(0, 0);
					int num12 = 0;
					if (cellSide == "N")
					{
						num12 = ((Stat.Random(0, 1) == 0) ? 2 : 3);
					}
					if (cellSide == "S")
					{
						num12 = ((Stat.Random(0, 1) == 0) ? 0 : 1);
					}
					if (cellSide == "E")
					{
						num12 = ((Stat.Random(0, 1) == 0) ? 0 : 3);
					}
					if (cellSide == "W")
					{
						num12 = ((Stat.Random(0, 1) == 0) ? 1 : 2);
					}
					if (num12 == 0 || num12 == 1)
					{
						rect2D2.y2 = rect2D2.y1 + 3;
					}
					else
					{
						rect2D2.y1 = rect2D2.y2 - 3;
					}
					if (num12 == 0 || num12 == 3)
					{
						rect2D2.x2 = rect2D2.x1 + 3;
					}
					else
					{
						rect2D2.x1 = rect2D2.x2 - 3;
					}
					base.ClearRect(Z, rect2D2);
					ZoneBuilderSandbox.PlaceObjectOnRect(Z, "BrinestalkWall", rect2D2, false);
					Point2D randomDoorCell2 = rect2D2.GetRandomDoorCell(cellSide, 1);
					Z.GetCell(randomDoorCell2).Clear(null, false, false, null);
					Z.GetCell(randomDoorCell2).AddObject("Door", null, null, null, null);
					int num13 = Stat.Random(3, 10);
					for (int num14 = 0; num14 < num13; num14++)
					{
						ZoneBuilderSandbox.PlaceObjectInRect(Z, rect2D.ReduceBy(1, 1), "Tombstone", null, 0, null);
					}
					rect2D2.ForEachLocation(delegate(Location2D l)
					{
						if (90.in100())
						{
							Z.GetCell(l).ClearWalls();
						}
					});
					int prefabWidth = 5;
					int prefabHeight = 3;
					List<Cell> cells = Z.GetCells(delegate(Cell c)
					{
						if (c.X > 75)
						{
							return false;
						}
						if (c.Y > 20)
						{
							return false;
						}
						if (!c.HasOpenLiquidVolume())
						{
							return false;
						}
						for (int num17 = 0; num17 < prefabHeight; num17++)
						{
							for (int num18 = 0; num18 < prefabWidth; num18++)
							{
								Cell cellFromOffset = c.GetCellFromOffset(num18, num17);
								if (cellFromOffset == null || !cellFromOffset.HasOpenLiquidVolume())
								{
									return false;
								}
							}
						}
						return true;
					});
					if (cells.Count <= 0)
					{
						cells.Add(Z.GetRandomCell(Math.Max(prefabWidth, prefabHeight)));
					}
					Cell randomElement2 = cells.GetRandomElement<Cell>(null);
					for (int num15 = 0; num15 < prefabHeight; num15++)
					{
						for (int num16 = 0; num16 < prefabWidth; num16++)
						{
							randomElement2.GetCellFromOffset(num16, num15).Clear(null, false, false, null);
						}
					}
					randomElement2.AddObject("JoppaSultanShrine_5x3_ruined", null, null, null, null);
				}
			}
			Z.ClearReachableMap();
			return Z.BuildReachableMap(0, 0) >= 400;
		}
	}
}
