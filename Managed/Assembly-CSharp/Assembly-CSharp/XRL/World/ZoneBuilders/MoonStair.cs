using System;
using System.Collections.Generic;
using Genkit;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AAB RID: 2731
	public class MoonStair : ZoneBuilderSandbox
	{
		// Token: 0x06006F45 RID: 28485 RVA: 0x0026BB0C File Offset: 0x00269D0C
		public static void Save(SerializationWriter Writer)
		{
		}

		// Token: 0x06006F46 RID: 28486 RVA: 0x0026BB0E File Offset: 0x00269D0E
		public static void Load(SerializationReader Reader)
		{
		}

		// Token: 0x06006F47 RID: 28487 RVA: 0x0026BB10 File Offset: 0x00269D10
		public int getSeed(string seed)
		{
			return XRLCore.Core.Game.GetWorldSeed(seed);
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x0026BB24 File Offset: 0x00269D24
		public double sampleSimplexNoise(string type, int x, int y, int z, int amplitude, float frequencyMultiplier = 1f)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.25f * frequencyMultiplier);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			return Math.Ceiling((double)this.fastNoise.GetNoise((float)x, (float)y, (float)z) * (double)amplitude);
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x0026BBB8 File Offset: 0x00269DB8
		public double sampleCellularNoise(string type, int x, int y, int z, float frequencyMultiplier = 1f)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.Cellular);
			this.fastNoise.SetFrequency(0.2f * frequencyMultiplier);
			this.fastNoise.SetCellularDistanceFunction(FastNoise.CellularDistanceFunction.Euclidean);
			this.fastNoise.SetCellularReturnType(FastNoise.CellularReturnType.Distance2Add);
			this.fastNoise.SetCellularDistance2Indicies(0, 1);
			this.fastNoise.SetCellularJitter(0.45f);
			return (double)this.fastNoise.GetNoise((float)x, (float)y, (float)z);
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x0026BC40 File Offset: 0x00269E40
		public double sampleCellularNoise2(string type, int x, int y, int z, float frequencyMultiplier = 1f)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.Cellular);
			this.fastNoise.SetFrequency(0.4f * frequencyMultiplier);
			this.fastNoise.SetCellularDistanceFunction(FastNoise.CellularDistanceFunction.Euclidean);
			this.fastNoise.SetCellularReturnType(FastNoise.CellularReturnType.CellValue);
			this.fastNoise.SetCellularDistance2Indicies(0, 1);
			this.fastNoise.SetCellularJitter(0.45f);
			return (double)this.fastNoise.GetNoise((float)x, (float)y, (float)z);
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x0026BCC8 File Offset: 0x00269EC8
		public void PaintCrystals(Zone Z, int x, int y, string color)
		{
			int num = x / 3;
			int num2 = x / 3 % 2;
			int num3 = y / 2;
			for (int i = num * 3; i < num * 3 + 3; i++)
			{
				for (int j = num3 * 2 + num2; j < num3 * 2 + 2 + num2; j++)
				{
					if (Z.GetCell(i, j) != null && Z.GetCell(i, j).HasObject("CrystalWall"))
					{
						Render render = Z.GetCell(i, j).GetObjects("CrystalWall")[0].Render;
						render.ColorString = "&" + color + "^k";
						render.TileColor = "&" + color + "^k";
					}
				}
			}
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x0026BD80 File Offset: 0x00269F80
		public int sampleZonescaleSimplexNoiseIntRange(string type, int x, int y, int z, int low, int high)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.1f);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			float num = (this.fastNoise.GetNoise((float)x, (float)y, (float)z) * 1.8f + 1f) / 2f;
			num *= (float)high - (float)low;
			num += (float)low;
			return Math.Min(Math.Max(low, (int)Math.Round((double)num)), high);
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x0026BE40 File Offset: 0x0026A040
		public void SpiralZoomie(MoonStair.MoonStairZoomiesDefinition zoomies, Rect2D zoomieArea, Point2D start, string color = null, int depth = 0)
		{
			zoomies.zoomieStarts.Add(start);
			int num = Stat.Random(30, 2800);
			float num2 = 1f;
			float num3 = (float)Stat.Random(1, 6);
			float num4 = (float)Stat.Random(400, 1600) / 800f;
			float num5 = (float)Stat.Random(20, 80) / 800f;
			if (Stat.Random(1, 100) <= 50)
			{
				num5 = -num5;
			}
			int num6 = Stat.Random(1, 5);
			int num7 = start.x;
			int num8 = start.y;
			if (color == null)
			{
				color = MoonStair.ZoomieColor.GetRandomElement<char>(null).ToString();
			}
			HashSet<Point2D> hashSet = new HashSet<Point2D>();
			hashSet.Add(start);
			int i = 0;
			while (i < num)
			{
				int num9 = Convert.ToInt32(start.x) + (int)((double)num2 * Math.Cos((double)num3));
				int num10 = Convert.ToInt32(start.y) + (int)((double)num2 * Math.Sin((double)num3) * 0.66);
				if (zoomieArea.Contains(num9, num10))
				{
					foreach (Pair pair in MissileWeapon.ListOfVisitedSquares(num7, num8, num9, num10))
					{
						if (!hashSet.Contains(new Point2D(pair.x, pair.y)))
						{
							if (zoomies.get(new Point2D(pair.x, pair.y)) != null)
							{
								if (depth < 3)
								{
									if (Stat.Random(1, 2) == 1)
									{
										this.SpiralZoomie(zoomies, zoomieArea, new Point2D(pair.x, pair.y), MoonStair.ImpactZoomieColor.GetRandomElement<char>(null).ToString(), depth + 1);
									}
									else
									{
										this.BurstZoomie(zoomies, zoomieArea, new Point2D(pair.x, pair.y), MoonStair.ImpactZoomieColor.GetRandomElement<char>(null).ToString(), depth + 1);
									}
								}
								return;
							}
							hashSet.Add(new Point2D(pair.x, pair.y));
						}
						zoomies.set(pair.x, pair.y, color);
					}
					num7 = num9;
					num8 = num10;
					for (int j = 0; j < num6; j++)
					{
						num2 += num4;
						for (num3 += num5; num3 < 0f; num3 += 6.28f)
						{
						}
						while ((double)num3 > 6.28)
						{
							num3 -= 6.28f;
						}
					}
					i++;
					continue;
				}
				break;
			}
			zoomies.zoomieEnds.Add(new Point2D(num7, num8));
		}

		// Token: 0x06006F4E RID: 28494 RVA: 0x0026C0F8 File Offset: 0x0026A2F8
		public void BurstZoomie(MoonStair.MoonStairZoomiesDefinition zoomies, Rect2D zoomieArea, Point2D start, string color = null, int depth = 0)
		{
			zoomies.zoomieStarts.Add(start);
			int num = Stat.Random(1, 8);
			if (color == null)
			{
				color = MoonStair.ZoomieColor.GetRandomElement<char>(null).ToString();
			}
			HashSet<Point2D> hashSet = new HashSet<Point2D>();
			hashSet.Add(start);
			for (int i = 0; i < num; i++)
			{
				int num2 = Stat.Random(8, 80);
				int num3 = Stat.Random(1, 9);
				int num4 = Stat.Random(1, 6);
				if (Stat.Random(1, 2) == 1)
				{
					num3 = -num3;
				}
				if (Stat.Random(1, 2) == 1)
				{
					num4 = -num4;
				}
				int num5 = start.x;
				int num6 = start.y;
				for (int j = 0; j < num2; j++)
				{
					int num7 = num5 + num3;
					int num8 = num6 + num4;
					if (!zoomieArea.Contains(num7, num8))
					{
						break;
					}
					foreach (Pair pair in MissileWeapon.ListOfVisitedSquares(num5, num6, num7, num8))
					{
						if (!hashSet.Contains(new Point2D(pair.x, pair.y)))
						{
							if (zoomies.get(new Point2D(pair.x, pair.y)) != null)
							{
								if (depth < 3)
								{
									if (Stat.Random(1, 2) == 1)
									{
										this.SpiralZoomie(zoomies, zoomieArea, new Point2D(pair.x, pair.y), MoonStair.ImpactZoomieColor.GetRandomElement<char>(null).ToString(), depth + 1);
									}
									else
									{
										this.BurstZoomie(zoomies, zoomieArea, new Point2D(pair.x, pair.y), MoonStair.ImpactZoomieColor.GetRandomElement<char>(null).ToString(), depth + 1);
									}
								}
								goto IL_01F1;
							}
							hashSet.Add(new Point2D(pair.x, pair.y));
						}
						zoomies.set(pair.x, pair.y, color);
					}
					num5 += num3;
					num6 += num4;
				}
				IL_01F1:
				zoomies.zoomieEnds.Add(new Point2D(num5, num6));
			}
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x0026C328 File Offset: 0x0026A528
		public void MakeZoomies()
		{
			MoonStair.MoonStairZoomiesDefinition moonStairZoomiesDefinition = new MoonStair.MoonStairZoomiesDefinition();
			int num = 10;
			Rect2D rect2D = new Rect2D(17280, 150, 18720, 1275);
			for (int i = 0; i < num; i++)
			{
				int num2 = Stat.Random(1, 2);
				if (num2 == 1)
				{
					this.SpiralZoomie(moonStairZoomiesDefinition, rect2D, rect2D.randomPoint(), null, 0);
				}
				if (num2 == 2)
				{
					this.BurstZoomie(moonStairZoomiesDefinition, rect2D, rect2D.randomPoint(), null, 0);
				}
			}
			The.Game.SetObjectGameState("MoonStairZoomies", moonStairZoomiesDefinition);
		}

		// Token: 0x06006F50 RID: 28496 RVA: 0x0026C3A8 File Offset: 0x0026A5A8
		public bool BuildZone(Zone Z)
		{
			if (!The.Game.HasObjectGameState("MoonStairZoomies"))
			{
				this.MakeZoomies();
			}
			MoonStair.MoonStairZoomiesDefinition moonStairZoomiesDefinition = The.Game.GetObjectGameState("MoonStairZoomies") as MoonStair.MoonStairZoomiesDefinition;
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			bool enablePrereleaseContent = Options.EnablePrereleaseContent;
			Z.ClearReachableMap();
			string text = "Black Marble";
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			int num3 = this.sampleZonescaleSimplexNoiseIntRange("MoonStairRockAmount", num, num2, Z.Z, 0, 24);
			for (int i = 0; i < num3; i++)
			{
				int num4 = Stat.Random(3, Math.Max(12, num3));
				int num5 = Stat.Random(2, Math.Max(7, num3 / 2));
				int num6 = Stat.Random(0, 79 - num4);
				int num7 = Stat.Random(0, 24 - num5);
				for (int j = num6; j < num6 + num4; j++)
				{
					for (int k = num7; k < num7 + num5; k++)
					{
						Z.GetCell(j, k).ClearWalls();
						Z.GetCell(j, k).AddObject(text, null, null, null, null);
					}
				}
			}
			ZoneManager.GetObjectTypeForZone(Z.wX, Z.wY, Z.GetZoneWorld());
			int num8 = 0;
			for (int l = 0; l < Z.Height; l++)
			{
				for (int m = 0; m < Z.Width; m++)
				{
					if (this.sampleCellularNoise("rock", Z.ResolvedStartX + m, (int)((float)(Z.ResolvedStartY + l) * 1.5f), Z.Z, 1f) >= 1.25)
					{
						num8++;
						Z.GetCell(m, l).ClearWalls();
					}
				}
			}
			int num9 = this.sampleZonescaleSimplexNoiseIntRange("MoonStairCrystalAmount", num, num2, Z.Z, 0, 80);
			for (int n = 0; n < Z.Width / 3; n++)
			{
				for (int num10 = 0; num10 < Z.Height / 2; num10++)
				{
					int num11 = n * 3;
					int num12 = num10 * 2 + n % 2;
					if (Stat.Random(1, 100) < num9)
					{
						for (int num13 = 0; num13 < 3; num13++)
						{
							for (int num14 = 0; num14 < 2; num14++)
							{
								Z.GetCell(num11 + num13, num12 + num14).ClearWalls();
								Z.GetCell(num11 + num13, num12 + num14).AddObject("CrystalWall", null, null, null, null);
							}
						}
					}
				}
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(Z, null, InfluenceMapSeedStrategy.FurthestPoint, int.MaxValue, null, false, delegate(Cell c)
			{
				if (c.HasObject("CrystalWall"))
				{
					return 0;
				}
				return 1;
			});
			influenceMap.SeedAllUnseeded(false, true);
			foreach (InfluenceMapRegion influenceMapRegion in influenceMap.Regions)
			{
				string text2 = MoonStair.HexColor.GetRandomElement<char>(null).ToString();
				foreach (Location2D location2D in influenceMapRegion.Cells)
				{
					this.PaintCrystals(Z, location2D.X, location2D.Y, text2);
				}
			}
			foreach (Cell cell in Z.GetCellsWithObject("CrystalWall"))
			{
				foreach (Cell cell2 in cell.GetLocalAdjacentCells())
				{
					if (cell2.HasObject(text))
					{
						cell2.RemoveObject(cell2.FindObject(text), false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
				}
			}
			for (int num15 = 0; num15 < Z.Width; num15++)
			{
				for (int num16 = 0; num16 < Z.Height; num16++)
				{
					int num17 = num * Z.Width + num15;
					int num18 = num2 * Z.Height + num16;
					string text3 = moonStairZoomiesDefinition.get(new Point2D(num17, num18));
					if (text3 != null)
					{
						Cell cell3 = Z.GetCell(num15, num16);
						if (cell3 != null && !cell3.HasObject("SmallHexFloor") && Stat.Random(1, 100) <= 75)
						{
							cell3.AddObject("SmallHexFloor", null, null, null, null).Render.ColorString = "&" + text3;
						}
						this.PaintCrystals(Z, num15, num16, text3);
					}
				}
			}
			foreach (Point2D point2D in moonStairZoomiesDefinition.zoomieStarts)
			{
				int num19 = num * Z.Width;
				int num20 = num19 + Z.Width;
				int num21 = num2 * Z.Height;
				int num22 = num21 + Z.Height;
				if (point2D.x >= num19 && point2D.x < num20 && point2D.y >= num21 && point2D.y < num22)
				{
					Cell cell4 = Z.GetCell(point2D.x - num19, point2D.y - num21);
					if (cell4 != null)
					{
						cell4.AddObject("Space-Time Rift", null, null, null, null);
						The.Game.SetIntGameState("zoomnodes", The.Game.GetIntGameState("zoomnodes", 0));
					}
				}
			}
			foreach (Point2D point2D2 in moonStairZoomiesDefinition.zoomieEnds)
			{
				int num23 = num * Z.Width;
				int num24 = num23 + Z.Width;
				int num25 = num2 * Z.Height;
				int num26 = num25 + Z.Height;
				if (point2D2.x >= num23 && point2D2.x < num24 && point2D2.y >= num25 && point2D2.y < num26)
				{
					Cell cell5 = Z.GetCell(point2D2.x - num23, point2D2.y - num25);
					if (cell5 != null)
					{
						cell5.AddObject("Space-Time Rift", null, null, null, null);
						The.Game.SetIntGameState("zoomnodes", The.Game.GetIntGameState("zoomnodes", 0));
					}
				}
			}
			for (int num27 = 0; num27 < 78; num27 += 2)
			{
				for (int num28 = 0; num28 < Z.Height; num28++)
				{
					if (Z.GetCell(num27, num28).HasObject("SmallHexFloor") && Z.GetCell(num27 + 1, num28).HasObject("SmallHexFloor"))
					{
						Z.GetCell(num27, num28).GetObjects("SmallHexFloor")[0].Render.Tile = "Terrain/sw_hex_dotted_2_l.bmp";
						Z.GetCell(num27 + 1, num28).GetObjects("SmallHexFloor")[0].Render.Tile = "Terrain/sw_hex_dotted_2_r.bmp";
					}
				}
			}
			for (int num29 = 0; num29 < Z.Height; num29++)
			{
				for (int num30 = 0; num30 < 78; num30 += 2)
				{
					if (Z.GetCell(num30, num29).HasObject("SmallHexFloor") && Z.GetCell(num30 + 1, num29).HasObject("SmallHexFloor") && !Z.GetCell(num30, num29).HasWall() && !Z.GetCell(num30 + 1, num29).HasWall())
					{
						Z.GetCell(num30, num29).GetObjects("SmallHexFloor")[0].Render.Tile = "Terrain/sw_hex_dotted_2_l.bmp";
						Z.GetCell(num30 + 1, num29).GetObjects("SmallHexFloor")[0].Render.Tile = "Terrain/sw_hex_dotted_2_r.bmp";
						Debug.Log("painting " + Z.GetCell(num30, num29).GetObjects("SmallHexFloor")[0].CurrentCell.Pos2D.ToString() + " and " + Z.GetCell(num30 + 1, num29).GetObjects("SmallHexFloor")[0].CurrentCell.Pos2D.ToString());
					}
				}
			}
			Z.ClearReachableMap();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (!Z.IsReachable(zoneConnection.X, zoneConnection.Y))
				{
					Z.BuildReachableMap(zoneConnection.X, zoneConnection.Y, false);
				}
			}
			Z.BuildReachableMap();
			Z.BuildReachabilityFromEdges();
			return true;
		}

		// Token: 0x0400331A RID: 13082
		private FastNoise fastNoise = new FastNoise(1337);

		// Token: 0x0400331B RID: 13083
		public static readonly char[] ImpactZoomieColor = new char[] { 'C', 'W', 'B', 'G', 'R' };

		// Token: 0x0400331C RID: 13084
		public static readonly char[] ZoomieColor = new char[] { 'C', 'W' };

		// Token: 0x0400331D RID: 13085
		public static readonly char[] HexColor = new char[] { 'm', 'M', 'y', 'Y' };

		// Token: 0x02001B13 RID: 6931
		[Serializable]
		public class MoonStairZoomiesDefinition : IComposite
		{
			// Token: 0x0600EA7D RID: 60029 RVA: 0x0053D440 File Offset: 0x0053B640
			public void set(int x, int y, string color)
			{
				this.set(new Point2D(x, y), color);
			}

			// Token: 0x0600EA7E RID: 60030 RVA: 0x0053D450 File Offset: 0x0053B650
			public void set(Point2D pos, string color)
			{
				if (this.zoomieColors.ContainsKey(pos))
				{
					this.zoomieColors[pos] = color;
					return;
				}
				this.zoomieColors.Add(pos, color);
			}

			// Token: 0x0600EA7F RID: 60031 RVA: 0x0053D47B File Offset: 0x0053B67B
			public string get(Point2D pos)
			{
				if (this.zoomieColors.ContainsKey(pos))
				{
					return this.zoomieColors[pos];
				}
				return null;
			}

			// Token: 0x04006BAA RID: 27562
			public List<Point2D> zoomieStarts = new List<Point2D>();

			// Token: 0x04006BAB RID: 27563
			public List<Point2D> zoomieEnds = new List<Point2D>();

			// Token: 0x04006BAC RID: 27564
			public Dictionary<Point2D, string> zoomieColors = new Dictionary<Point2D, string>();
		}
	}
}
