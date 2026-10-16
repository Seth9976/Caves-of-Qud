using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Genkit;
using HistoryKit;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000ACC RID: 2764
	public class Reef : ZoneBuilderSandbox
	{
		// Token: 0x06006FAF RID: 28591 RVA: 0x00273962 File Offset: 0x00271B62
		public static void Save(SerializationWriter Writer)
		{
			Writer.Write(0);
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x0027396B File Offset: 0x00271B6B
		public static void Load(SerializationReader Reader)
		{
			Reader.ReadInt32();
			Reef.TunnelMaze = null;
		}

		// Token: 0x06006FB1 RID: 28593 RVA: 0x0027397C File Offset: 0x00271B7C
		public bool ConnectMaze(Zone Z, int minMazeWidth = 1, int maxMazeWidth = 2)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			int seededRange = base.GetSeededRange("TunnelSeed", int.MinValue, int.MaxValue);
			if (Reef.TunnelMaze == null)
			{
				Reef.TunnelMaze = RecursiveBacktrackerMaze3D.Generate(base.GetSeededRange("TunnelMaze", int.MinValue, int.MaxValue), 240, 75, 30, false, 10, 5, 5, 5, 5, 5);
			}
			MazeCell3D mazeCell3D = ((Z.Z > 10) ? Reef.TunnelMaze.Cell[num, num2, (Z.Z - 11) % 30] : new MazeCell3D());
			if (8.in100())
			{
				if (mazeCell3D.N)
				{
					Z.CacheZoneConnection("-", new Point(base.GetSeededRange(num2 + seededRange * Z.Z, 10, 70), 0), "cave", null);
				}
				if (mazeCell3D.S)
				{
					Z.CacheZoneConnection("-", new Point(base.GetSeededRange(num2 + 1 + seededRange * Z.Z, 10, 70), Z.Height - 1), "cave", null);
				}
				if (mazeCell3D.E)
				{
					Z.CacheZoneConnection("-", new Point(Z.Width - 1, base.GetSeededRange(num + 1 + seededRange * Z.Z, 5, 20)), "cave", null);
				}
				if (mazeCell3D.W)
				{
					Z.CacheZoneConnection("-", new Point(0, base.GetSeededRange(num + seededRange * Z.Z, 5, 20)), "cave", null);
				}
				new SultanDungeon().BuildRandomZone(Z, 5, false, "SultanDungeons_");
				The.ZoneManager.AddZonePostBuilderIfNotAlreadyPresent(Z.ZoneID, "ForceConnections");
				if (mazeCell3D.D)
				{
					new StairsDown().BuildZone(Z);
				}
				if (mazeCell3D.U)
				{
					new StairsUp().BuildZone(Z);
				}
				return true;
			}
			if (mazeCell3D.D)
			{
				new StairsDown().BuildZone(Z);
			}
			if (mazeCell3D.U)
			{
				new StairsUp().BuildZone(Z);
			}
			List<Point> list = new List<Point>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list.Add(new Point(zoneConnection.X, zoneConnection.Y));
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-")
				{
					list.Add(new Point(cachedZoneConnection.X, cachedZoneConnection.Y));
				}
			}
			if (mazeCell3D.N)
			{
				list.Add(new Point(base.GetSeededRange(num2 + seededRange * Z.Z, 10, 70), 0));
			}
			if (mazeCell3D.S)
			{
				list.Add(new Point(base.GetSeededRange(num2 + 1 + seededRange * Z.Z, 10, 70), Z.Height - 1));
			}
			if (mazeCell3D.E)
			{
				list.Add(new Point(Z.Width - 1, base.GetSeededRange(num + 1 + seededRange * Z.Z, 5, 20)));
			}
			if (mazeCell3D.W)
			{
				list.Add(new Point(0, base.GetSeededRange(num + seededRange * Z.Z, 5, 20)));
			}
			Algorithms.RandomShuffleInPlace<Point>(list, Stat.Rand);
			while (list.Count < 2)
			{
				list.Add(new Point(Stat.Random(10, 70), Stat.Random(5, 15)));
				list.Add(new Point(Stat.Random(10, 70), Stat.Random(5, 15)));
			}
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					Z.CacheZoneConnection("-", list[i].X, list[i].Y, "tunnelnode", null);
				}
				GameObject gameObject = null;
				gameObject = GameObjectFactory.Factory.CreateObject("Drillbot");
				int x = list[0].X;
				int y = list[0].Y;
				for (int j = 1; j < list.Count; j++)
				{
					if (x != list[j].X || y != list[j].Y)
					{
						FindPath findPath = new FindPath(Z, x, y, Z, list[j].X, list[j].Y, false, true, gameObject, true);
						if (!findPath.Usable)
						{
							return false;
						}
						foreach (Cell cell in findPath.Steps)
						{
							Z.ReachableMap[cell.X, cell.Y] = true;
							cell.ClearWalls();
						}
						foreach (Cell cell2 in findPath.Steps)
						{
							foreach (Cell cell3 in cell2.GetLocalAdjacentCells(Stat.Random(minMazeWidth, maxMazeWidth), false))
							{
								if (Stat.Random(1, 100) <= 75)
								{
									Z.ReachableMap[cell3.X, cell3.Y] = true;
									cell3.ClearWalls();
								}
							}
						}
					}
				}
			}
			foreach (ZoneConnection zoneConnection2 in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (!Z.GetCell(zoneConnection2.X, zoneConnection2.Y).IsSolid())
				{
					Z.BuildReachableMap(zoneConnection2.X, zoneConnection2.Y, false);
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection2 in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection2.TargetDirection == "-" && !Z.GetCell(cachedZoneConnection2.X, cachedZoneConnection2.Y).IsSolid())
				{
					Z.BuildReachableMap(cachedZoneConnection2.X, cachedZoneConnection2.Y, false);
				}
			}
			for (int k = 0; k < Z.Height; k++)
			{
				for (int l = 0; l < Z.Width; l++)
				{
					if (Z.GetCell(l, k).IsReachable())
					{
						Z.BuildReachableMap(l, k, false);
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x06006FB2 RID: 28594 RVA: 0x002740C4 File Offset: 0x002722C4
		public List<Tuple<string, int>> resolveElements(int x, int y, int z, List<Func<int, int, int, Tuple<string, int>>> input, Func<Tuple<string, int>, Tuple<string, int>> transform)
		{
			input.RemoveAll((Func<int, int, int, Tuple<string, int>> e) => e == null);
			List<Tuple<string, int>> list = input.Select<Func<int, int, int, Tuple<string, int>>, Tuple<string, int>>((Func<int, int, int, Tuple<string, int>> e) => e(x, y, z)).ToList<Tuple<string, int>>();
			list.RemoveAll((Tuple<string, int> e) => e == null);
			list = list.Select<Tuple<string, int>, Tuple<string, int>>((Tuple<string, int> e) => transform(e)).ToList<Tuple<string, int>>();
			list.Sort((Tuple<string, int> a, Tuple<string, int> b) => b.Item2.CompareTo(a.Item2));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append(list[i].Item1 + "=" + list[i].Item2.ToString() + "\n");
			}
			return list;
		}

		// Token: 0x06006FB3 RID: 28595 RVA: 0x002741E8 File Offset: 0x002723E8
		public string getRandomWeightedFromElements(int x, int y, int z, List<Func<int, int, int, Tuple<string, int>>> input, Func<Tuple<string, int>, Tuple<string, int>> transform)
		{
			List<Tuple<string, int>> list = this.resolveElements(x, y, z, input, transform);
			int num = 0;
			foreach (Tuple<string, int> tuple in list)
			{
				num += tuple.Item2;
			}
			int num2 = Stat.Random(0, num);
			int num3 = 0;
			foreach (Tuple<string, int> tuple2 in list)
			{
				num3 += tuple2.Item2;
				if (num3 >= num2)
				{
					return tuple2.Item1;
				}
			}
			return null;
		}

		// Token: 0x06006FB4 RID: 28596 RVA: 0x002742AC File Offset: 0x002724AC
		public int getSeed(string seed)
		{
			return XRLCore.Core.Game.GetWorldSeed(seed);
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x002742C0 File Offset: 0x002724C0
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

		// Token: 0x06006FB6 RID: 28598 RVA: 0x00274354 File Offset: 0x00272554
		public double sampleSimplexNoiseRange(string type, int x, int y, int z, float low, float high)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.25f);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			return (double)((this.fastNoise.GetNoise((float)x, (float)y, (float)z) + 1f) / 2f * (high - low) + low);
		}

		// Token: 0x06006FB7 RID: 28599 RVA: 0x002743F0 File Offset: 0x002725F0
		public int sampleSimplexNoiseIntRange(string type, int x, int y, int z, int low, int high)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.25f);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			float num = (this.fastNoise.GetNoise((float)x, (float)y, (float)z) + 1f) / 2f;
			num *= (float)high - (float)low;
			num += (float)low;
			return Math.Min(Math.Max(low, (int)Math.Round((double)num)), high);
		}

		// Token: 0x06006FB8 RID: 28600 RVA: 0x002744A8 File Offset: 0x002726A8
		public double sampleZonescaleSimplexNoise(string type, int x, int y, int z, int amplitude)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.01f);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(2f);
			this.fastNoise.SetFractalGain(0.5f);
			return Math.Ceiling((double)this.fastNoise.GetNoise((float)x, (float)y, (float)z) * (double)amplitude);
		}

		// Token: 0x06006FB9 RID: 28601 RVA: 0x00274538 File Offset: 0x00272738
		public double sampleZonescaleSimplexNoiseRange(string type, int x, int y, int z, float low, float high)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.01f);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(2f);
			this.fastNoise.SetFractalGain(0.5f);
			return (double)((this.fastNoise.GetNoise((float)x, (float)y, (float)z) + 1f) / 2f * (high - low) + low);
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x002745D4 File Offset: 0x002727D4
		public int sampleZonescaleSimplexNoiseIntRange(string type, int x, int y, int z, int low, int high)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.01f);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(2f);
			this.fastNoise.SetFractalGain(0.5f);
			float num = (this.fastNoise.GetNoise((float)x, (float)y, (float)z) + 1f) / 2f;
			num *= (float)high - (float)low;
			num += (float)low;
			return Math.Min(Math.Max(low, (int)Math.Round((double)num)), high);
		}

		// Token: 0x06006FBB RID: 28603 RVA: 0x0027468C File Offset: 0x0027288C
		public Reef.CellType[,] getRandomWalkCaveLayout(Zone Z)
		{
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			int num = 500;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					array[j, i] = Reef.CellType.Open;
				}
			}
			int num2 = Stat.Random(0, Z.Width - 1);
			int num3 = Stat.Random(0, Z.Height - 1);
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < num; l++)
				{
					array[num2, num3] = Reef.CellType.Wall;
					int num4 = Stat.Random(0, 3);
					if (num4 == 0)
					{
						num2++;
					}
					if (num4 == 1)
					{
						num2--;
					}
					if (num4 == 2)
					{
						num3++;
					}
					if (num4 == 3)
					{
						num3--;
					}
					if (num2 < 0)
					{
						num2 = 0;
					}
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num2 > Z.Width - 1)
					{
						num2 = Z.Width - 1;
					}
					if (num3 > Z.Height - 1)
					{
						num3 = Z.Height - 1;
					}
				}
				int[,] array2 = new int[Z.Width, Z.Height];
				for (int m = 0; m < Z.Width; m++)
				{
					for (int n = 0; n < Z.Height; n++)
					{
						if (array[m, n] == Reef.CellType.Open)
						{
							array2[m, n] = 0;
						}
						else
						{
							array2[m, n] = 1;
						}
					}
				}
				Rect2D rect2D = GridTools.MaxRectByArea(array2, false, 0, 0);
				if (rect2D.Area <= 300)
				{
					break;
				}
				num2 = rect2D.Center.x;
				num3 = rect2D.Center.y;
			}
			return array;
		}

		// Token: 0x06006FBC RID: 28604 RVA: 0x00274828 File Offset: 0x00272A28
		public Reef.CellType[,] getRandomWalkReefLayout(Zone Z)
		{
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			int num = 500;
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					array[i, j] = Reef.CellType.Open;
				}
			}
			int num2 = Stat.Random(0, Z.Width - 1);
			int num3 = Stat.Random(0, Z.Height - 1);
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < num; l++)
				{
					array[num2, num3] = Reef.CellType.ReefOuter;
					int num4 = Stat.Random(0, 3);
					if (num4 == 0)
					{
						num2++;
					}
					if (num4 == 1)
					{
						num2--;
					}
					if (num4 == 2)
					{
						num3++;
					}
					if (num4 == 3)
					{
						num3--;
					}
					if (num2 < 0)
					{
						num2 = 0;
					}
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num2 > Z.Width - 1)
					{
						num2 = Z.Width - 1;
					}
					if (num3 > Z.Height - 1)
					{
						num3 = Z.Height - 1;
					}
				}
				int[,] array2 = new int[Z.Width, Z.Height];
				for (int m = 0; m < Z.Height; m++)
				{
					for (int n = 0; n < Z.Width; n++)
					{
						if (array[n, m] == Reef.CellType.Open)
						{
							array2[n, m] = 0;
						}
						else
						{
							array2[n, m] = 1;
						}
					}
				}
				Rect2D rect2D = GridTools.MaxRectByArea(array2, false, 0, 0);
				if (rect2D.Area <= 300)
				{
					break;
				}
				num2 = rect2D.Center.x;
				num3 = rect2D.Center.y;
			}
			return array;
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x002749C1 File Offset: 0x00272BC1
		public int countBoxFilterAt(int x, int y, Reef.CellType[,] cells)
		{
			if (x < 0)
			{
				return 0;
			}
			if (y < 0)
			{
				return 0;
			}
			if (x >= 80)
			{
				return 0;
			}
			if (y >= 24)
			{
				return 0;
			}
			if (cells[x, y] == Reef.CellType.Wall)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06006FBE RID: 28606 RVA: 0x002749EC File Offset: 0x00272BEC
		public int countBoxFilterAdjacentD1(int x, int y, Reef.CellType[,] cells)
		{
			return 0 + this.countBoxFilterAt(x - 1, y - 1, cells) + this.countBoxFilterAt(x - 1, y, cells) + this.countBoxFilterAt(x - 1, y + 1, cells) + this.countBoxFilterAt(x, y - 1, cells) + this.countBoxFilterAt(x, y + 1, cells) + this.countBoxFilterAt(x + 1, y - 1, cells) + this.countBoxFilterAt(x + 1, y, cells) + this.countBoxFilterAt(x + 1, y + 1, cells);
		}

		// Token: 0x06006FBF RID: 28607 RVA: 0x00274A64 File Offset: 0x00272C64
		public int countBoxFilterAdjacentD2(int x, int y, Reef.CellType[,] cells)
		{
			return 0 + this.countBoxFilterAdjacentD1(x, y, cells) + this.countBoxFilterAt(x - 2, y - 1, cells) + this.countBoxFilterAt(x - 2, y, cells) + this.countBoxFilterAt(x - 2, y + 1, cells) + this.countBoxFilterAt(x - 1, y - 2, cells) + this.countBoxFilterAt(x, y - 2, cells) + this.countBoxFilterAt(x + 1, y - 2, cells) + this.countBoxFilterAt(x - 1, y + 2, cells) + this.countBoxFilterAt(x, y + 2, cells) + this.countBoxFilterAt(x + 1, y + 2, cells) + this.countBoxFilterAt(x + 2, y - 1, cells) + this.countBoxFilterAt(x + 2, y, cells) + this.countBoxFilterAt(x + 2, y + 1, cells);
		}

		// Token: 0x06006FC0 RID: 28608 RVA: 0x00274B1C File Offset: 0x00272D1C
		public Reef.CellType[,] getBoxFilterCave(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			int num3 = this.sampleZonescaleSimplexNoiseIntRange("rwcavesteps", num, num2, Z.Z, 40, 60);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (j <= 2 || j >= Z.Width - 2 || i <= 2 || i >= Z.Height - 2 || num3.in100())
					{
						array[j, i] = Reef.CellType.Wall;
					}
					else
					{
						array[j, i] = Reef.CellType.Open;
					}
				}
			}
			int num4 = this.sampleZonescaleSimplexNoiseIntRange("rwcavesteps", num, num2, Z.Z, 2, 4);
			Reef.CellType[,] array2 = new Reef.CellType[Z.Width, Z.Height];
			for (int k = 0; k < num4; k++)
			{
				for (int l = 0; l < Z.Width; l++)
				{
					for (int m = 0; m < Z.Height; m++)
					{
						int num5 = 5;
						int num6 = 2;
						if (this.countBoxFilterAdjacentD1(l, m, array) >= num5)
						{
							array2[l, m] = Reef.CellType.Wall;
						}
						else if (this.countBoxFilterAdjacentD2(l, m, array) <= num6)
						{
							array2[l, m] = Reef.CellType.Wall;
						}
						else
						{
							array2[l, m] = Reef.CellType.Open;
						}
					}
				}
				Reef.CellType[,] array3 = array;
				array = array2;
				array2 = array3;
			}
			return array;
		}

		// Token: 0x06006FC1 RID: 28609 RVA: 0x00274CA0 File Offset: 0x00272EA0
		public Reef.CellType[,] getCaveLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			CellularGrid cellularGrid = new CellularGrid();
			cellularGrid.Passes = this.sampleZonescaleSimplexNoiseIntRange("cavePasses", num, num2, Z.Z, 1, 4);
			cellularGrid.SeedChance = this.sampleZonescaleSimplexNoiseIntRange("cavePasses", num, num2, Z.Z, 40, 80);
			cellularGrid.SeedBorders = true;
			cellularGrid.BorderDepth = 2;
			cellularGrid.Generate(Stat.Rand, Z.Width, Z.Height);
			int num3 = Z.wX * 240 + Z.X * 80 + Z.Z * 3;
			int num4 = Z.wY * 75 + Z.Y * 25 + Z.Z * 5;
			num3 %= 1119;
			num4 %= 349;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (j + num3 < 1200 && i + num4 < 375)
					{
						if (cellularGrid.cells[j, i] == 0)
						{
							array[j, i] = Reef.CellType.Wall;
						}
						else
						{
							array[j, i] = Reef.CellType.Open;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06006FC2 RID: 28610 RVA: 0x00274E04 File Offset: 0x00273004
		public Reef.CellType[,] getPillarsLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			FastNoise fastNoise = new FastNoise(1337);
			fastNoise.SetSeed(this.getSeed("pillars"));
			fastNoise.SetNoiseType(FastNoise.NoiseType.Cellular);
			fastNoise.SetFrequency((float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, 0.025f, 0.1f));
			fastNoise.SetCellularDistanceFunction(FastNoise.CellularDistanceFunction.Natural);
			fastNoise.SetCellularReturnType(FastNoise.CellularReturnType.Distance2Div);
			fastNoise.SetCellularJitter((float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, 0.1f, 1.2f));
			fastNoise.SetGradientPerturbAmp(5f);
			int num3 = this.sampleSimplexNoiseIntRange("pillarLayoutCrack", num, num2, Z.Z, 0, 2);
			fastNoise.SetCellularDistance2Indicies(num3, num3 + 1);
			float num4 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrackWidth", num, num2, Z.Z, 0.5f, 0.9f);
			float num5 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrackWidth", num, num2, Z.Z, 0.85f, 1f);
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					float noise = fastNoise.GetNoise((float)i, (float)j, (float)Z.Z);
					if (noise < num4)
					{
						array[i, j] = Reef.CellType.Wall;
					}
					else if (noise >= num5)
					{
						array[i, j] = Reef.CellType.Detail;
					}
					else
					{
						array[i, j] = Reef.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x00274F9C File Offset: 0x0027319C
		public Reef.CellType[,] getPorousLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			FastNoise fastNoise = new FastNoise(1337);
			fastNoise.SetSeed(this.getSeed("windy"));
			fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			float num3 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, 0.05f, 0.15f);
			fastNoise.SetFrequency(num3);
			fastNoise.SetFractalType(FastNoise.FractalType.Billow);
			fastNoise.SetFractalOctaves(1);
			fastNoise.SetFractalLacunarity(3f);
			fastNoise.SetFractalGain(0.5f);
			float num4 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, -0.75f, -0.5f);
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					float noise = fastNoise.GetNoise((float)i, (float)j);
					if (noise > num4)
					{
						array[i, j] = Reef.CellType.Wall;
					}
					else if ((double)noise <= 0.05)
					{
						array[i, j] = Reef.CellType.Detail;
					}
					else
					{
						array[i, j] = Reef.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x002750DC File Offset: 0x002732DC
		public Reef.CellType[,] getBlockyLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			FastNoise fastNoise = new FastNoise(1337);
			fastNoise.SetSeed(this.getSeed("blocky"));
			fastNoise.SetNoiseType(FastNoise.NoiseType.Cellular);
			float num3 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, 0.25f, 0.9f);
			fastNoise.SetFrequency(num3);
			fastNoise.SetCellularDistanceFunction(FastNoise.CellularDistanceFunction.Manhattan);
			fastNoise.SetCellularReturnType(FastNoise.CellularReturnType.CellValue);
			fastNoise.SetCellularJitter((float)this.sampleSimplexNoiseIntRange("blockyJitter", num, num2, Z.Z, 10, 50));
			fastNoise.SetCellularDistance2Indicies(0, 1);
			float num4 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, -0.25f, -0.25f);
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					float noise = fastNoise.GetNoise((float)i, (float)j);
					if (noise < num4)
					{
						array[i, j] = Reef.CellType.Wall;
					}
					else if ((double)noise >= 0.95)
					{
						array[i, j] = Reef.CellType.Detail;
					}
					else
					{
						array[i, j] = Reef.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x0027522C File Offset: 0x0027342C
		public Reef.CellType[,] getWindyLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Reef.CellType[,] array = new Reef.CellType[Z.Width, Z.Height];
			FastNoise fastNoise = new FastNoise(1337);
			fastNoise.SetSeed(this.getSeed("windy"));
			fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			float num3 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, 0.05f, 0.15f);
			fastNoise.SetFrequency(num3);
			fastNoise.SetFractalType(FastNoise.FractalType.Billow);
			fastNoise.SetFractalOctaves(1);
			fastNoise.SetFractalLacunarity(3f);
			fastNoise.SetFractalGain(0.5f);
			float num4 = (float)this.sampleSimplexNoiseRange("pillarLayoutCrack", num, num2, Z.Z, -0.6f, 0.25f);
			num4 = -0.6f;
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					float noise = fastNoise.GetNoise((float)i, (float)j);
					if (noise < num4)
					{
						array[i, j] = Reef.CellType.Wall;
					}
					else if ((double)noise <= 0.05)
					{
						array[i, j] = Reef.CellType.Detail;
					}
					else
					{
						array[i, j] = Reef.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x00275374 File Offset: 0x00273574
		public Reef.CellType[,] getReefLayout(Zone Z)
		{
			Reef.<>c__DisplayClass30_0 CS$<>8__locals1;
			CS$<>8__locals1.Z = Z;
			int num = CS$<>8__locals1.Z.wX * 3 + CS$<>8__locals1.Z.X;
			int num2 = CS$<>8__locals1.Z.wY * 3 + CS$<>8__locals1.Z.Y;
			CS$<>8__locals1.result = new Reef.CellType[CS$<>8__locals1.Z.Width, CS$<>8__locals1.Z.Height];
			FastNoise fastNoise = new FastNoise(1337);
			fastNoise.SetSeed(this.getSeed("reefpools"));
			fastNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
			fastNoise.SetFrequency(0.05f);
			FastNoise fastNoise2 = new FastNoise(1337);
			fastNoise2.SetSeed(this.getSeed("reef"));
			fastNoise2.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			float num3 = 0.02f;
			fastNoise2.SetFrequency(num3);
			fastNoise2.SetFractalType(FastNoise.FractalType.FBM);
			fastNoise2.SetFractalOctaves(5);
			fastNoise2.SetFractalLacunarity(3f);
			fastNoise2.SetFractalGain(0.5f);
			this.sampleSimplexNoiseRange("reefLayout", num, num2, CS$<>8__locals1.Z.Z, -1f, 1f);
			for (int i = 0; i < CS$<>8__locals1.Z.Height; i++)
			{
				for (int j = 0; j < CS$<>8__locals1.Z.Width; j++)
				{
					float noise = fastNoise2.GetNoise((float)(j + num * 80), (float)(i + num2 * 25));
					float num4 = fastNoise.GetNoise((float)((j + num * 80) / 3), (float)(i + num2 * 25));
					if (noise <= -0.3f)
					{
						CS$<>8__locals1.result[j, i] = Reef.CellType.ReefInner;
					}
					else if (noise <= -0.25f)
					{
						CS$<>8__locals1.result[j, i] = Reef.CellType.ReefMiddle;
					}
					else if (noise <= -0.1f)
					{
						CS$<>8__locals1.result[j, i] = Reef.CellType.ReefOuter;
					}
					else if ((double)noise <= 0.1)
					{
						CS$<>8__locals1.result[j, i] = Reef.CellType.ReefTidal;
					}
					else if ((double)num4 >= 0.4 || (double)num4 <= -0.4)
					{
						CS$<>8__locals1.result[j, i] = Reef.CellType.ReefPool;
					}
					else
					{
						CS$<>8__locals1.result[j, i] = Reef.CellType.ReefTidal;
					}
				}
			}
			bool flag;
			do
			{
				flag = false;
				for (int k = 0; k < CS$<>8__locals1.Z.Height; k++)
				{
					for (int l = 0; l < CS$<>8__locals1.Z.Width; l++)
					{
						if (CS$<>8__locals1.result[l, k] == Reef.CellType.ReefPool && Reef.<getReefLayout>g__adjacentPoolCount|30_0(l, k, 1, ref CS$<>8__locals1) <= 2)
						{
							CS$<>8__locals1.result[l, k] = Reef.CellType.ReefTidal;
							flag = true;
						}
					}
				}
			}
			while (flag);
			int num5 = 1;
			int num6 = 2;
			for (int m = 0; m < CS$<>8__locals1.Z.Height; m++)
			{
				for (int n = 0; n < CS$<>8__locals1.Z.Width; n++)
				{
					if (CS$<>8__locals1.result[n, m] == Reef.CellType.ReefTidal && Reef.<getReefLayout>g__poolWithinX|30_1(n, m, num5, ref CS$<>8__locals1))
					{
						CS$<>8__locals1.result[n, m] = Reef.CellType.ReefShorelineTidal;
					}
					else if (CS$<>8__locals1.result[n, m] == Reef.CellType.ReefTidal && Reef.<getReefLayout>g__poolWithinX|30_1(n, m, num6, ref CS$<>8__locals1))
					{
						CS$<>8__locals1.result[n, m] = Reef.CellType.ReefInnerTidal;
					}
				}
			}
			return CS$<>8__locals1.result;
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x002756D0 File Offset: 0x002738D0
		public bool BuildZone(Zone Z)
		{
			int gridX = Z.wX * 3 + Z.X;
			int gridY = Z.wY * 3 + Z.Y;
			int num = gridX * Z.Width;
			int num2 = gridY * Z.Height;
			int COMMON_WALL_WEIGHT = 5;
			int UNCOMMON_WALL_WEIGHT = 3;
			int RARE_WALL_WEIGHT = 1;
			int WALLTYPE_NOISE_AMPLITUDE = 5;
			List<Func<int, int, int, Tuple<string, int>>> list = new List<Func<int, int, int, Tuple<string, int>>>();
			list.Add(delegate(int x, int y, int z)
			{
				if (z >= 35)
				{
					return new Tuple<string, int>("Pond", 20);
				}
				return new Tuple<string, int>("Pond", 1000);
			});
			list.Add(delegate(int x, int y, int z)
			{
				if (z >= 35)
				{
					return new Tuple<string, int>("OilDeepPool", 20);
				}
				if (z >= 25)
				{
					return new Tuple<string, int>("OilDeepPool", 10);
				}
				return null;
			});
			list.Add(delegate(int x, int y, int z)
			{
				if (z >= 35)
				{
					return new Tuple<string, int>("AsphaltDeepPool", 100);
				}
				if (z >= 25)
				{
					return new Tuple<string, int>("AsphaltDeepPool", 20);
				}
				return null;
			});
			list.Add(delegate(int x, int y, int z)
			{
				if (z >= 35)
				{
					return new Tuple<string, int>("LavaPool", 1000);
				}
				if (z >= 25)
				{
					return new Tuple<string, int>("LavaPool", z * 10);
				}
				return null;
			});
			list.Add((int x, int y, int z) => new Tuple<string, int>("SlimePool", 5));
			list.Add((int x, int y, int z) => new Tuple<string, int>("GooPuddle", 5));
			list.Add((int x, int y, int z) => new Tuple<string, int>("OozePuddle", 5));
			list.Add((int x, int y, int z) => new Tuple<string, int>("SludgePuddle", 5));
			list.Add((int x, int y, int z) => new Tuple<string, int>("AcidPool", 5));
			list.Add((int x, int y, int z) => new Tuple<string, int>("CiderPool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("GelPool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("PutrescencePool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("FreshWaterPuddle", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("WinePool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("WaxPool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("InkPool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("SapPool", 1));
			list.Add((int x, int y, int z) => new Tuple<string, int>("HoneyPool", 1));
			List<Func<int, int, int, Tuple<string, int>>> list2 = list;
			List<Func<int, int, int, Tuple<string, int>>> list3 = new List<Func<int, int, int, Tuple<string, int>>>
			{
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>(Z.GetDefaultWall(), COMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>("Shale", RARE_WALL_WEIGHT);
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Shale", COMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>("Sandstone", RARE_WALL_WEIGHT);
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Sandstone", COMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>("Limestone", RARE_WALL_WEIGHT);
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Limestone", COMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>("Marl", RARE_WALL_WEIGHT);
					}
					if (z <= 15)
					{
						return new Tuple<string, int>("Marl", UNCOMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>("Halite", RARE_WALL_WEIGHT);
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Halite", UNCOMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 5)
					{
						return new Tuple<string, int>("Gypsum", RARE_WALL_WEIGHT);
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Gypsum", UNCOMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z < 15)
					{
						return null;
					}
					if (z <= 15)
					{
						return new Tuple<string, int>("Oolite", RARE_WALL_WEIGHT);
					}
					if (z <= 35)
					{
						return new Tuple<string, int>("Oolite", UNCOMMON_WALL_WEIGHT);
					}
					return null;
				},
				delegate(int x, int y, int z)
				{
					if (z <= 15)
					{
						return null;
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Slate", RARE_WALL_WEIGHT);
					}
					if (z <= 35)
					{
						return new Tuple<string, int>("Slate", UNCOMMON_WALL_WEIGHT);
					}
					return new Tuple<string, int>("Slate", COMMON_WALL_WEIGHT);
				},
				delegate(int x, int y, int z)
				{
					if (z <= 15)
					{
						return null;
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Coral Rag", UNCOMMON_WALL_WEIGHT);
					}
					return new Tuple<string, int>("Coral Rag", COMMON_WALL_WEIGHT);
				},
				delegate(int x, int y, int z)
				{
					if (z <= 15)
					{
						return null;
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Serpentinite", 1);
					}
					return new Tuple<string, int>("Serpentinite", UNCOMMON_WALL_WEIGHT);
				},
				delegate(int x, int y, int z)
				{
					if (z <= 15)
					{
						return null;
					}
					if (z <= 25)
					{
						return new Tuple<string, int>("Quartzite", RARE_WALL_WEIGHT);
					}
					return new Tuple<string, int>("Quartzite", UNCOMMON_WALL_WEIGHT);
				},
				delegate(int x, int y, int z)
				{
					if (z <= 25)
					{
						return null;
					}
					return new Tuple<string, int>("Black Shale", COMMON_WALL_WEIGHT);
				},
				delegate(int x, int y, int z)
				{
					if (z <= 25)
					{
						return null;
					}
					if (z <= 35)
					{
						return new Tuple<string, int>("Black Marble", RARE_WALL_WEIGHT);
					}
					return new Tuple<string, int>("Black Marble", UNCOMMON_WALL_WEIGHT);
				}
			};
			int depth = Z.Z - 10;
			List<Tuple<string, int>> list4 = this.resolveElements(Z.X + num, Z.Y + num2, depth, list3, delegate(Tuple<string, int> e)
			{
				int num13 = e.Item2;
				num13 += (int)this.sampleSimplexNoise(e.Item1, gridX, gridY, depth, WALLTYPE_NOISE_AMPLITUDE, 0.33f);
				return new Tuple<string, int>(e.Item1, num13);
			});
			List<string> list5 = list4.Select<Tuple<string, int>, string>((Tuple<string, int> e) => e.Item1).ToList<string>();
			string text = this.getRandomWeightedFromElements(Z.X + num, Z.Y + num2, depth, list2, delegate(Tuple<string, int> e)
			{
				int num14 = e.Item2;
				num14 += (int)this.sampleSimplexNoise(e.Item1, gridX, gridY, depth, WALLTYPE_NOISE_AMPLITUDE, 1f);
				return new Tuple<string, int>(e.Item1, num14);
			});
			Z.SetZoneProperty("DefaultWall", list5[0]);
			Reef.CellType[,] reefLayout = this.getReefLayout(Z);
			Reef.CellType[,] randomWalkReefLayout = this.getRandomWalkReefLayout(Z);
			double num3 = (double)list4[1].Item2 / (double)list4[0].Item2 * 0.5;
			num3 += this.sampleSimplexNoiseRange("secondaryThreshold", gridX, gridY, Z.Z, -0.2f, 0.2f);
			if (num3 > 0.5)
			{
				num3 = 0.5;
			}
			string region = Z.GetRegion();
			if (!string.IsNullOrEmpty(region) && region.Contains("dunes"))
			{
				text = null;
			}
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					Reef.CellType cellType;
					string text2;
					if (this.sampleZonescaleSimplexNoiseRange("layoutSelection", i + num, j + num2, Z.Z, 0f, 1f) > num3)
					{
						cellType = reefLayout[i, j];
						text2 = list5[0];
					}
					else
					{
						cellType = randomWalkReefLayout[i, j];
						text2 = list5[1];
					}
					float num4 = 0.25f;
					double num5 = this.sampleZonescaleSimplexNoiseRange("colorA", (int)((float)(i + num) / num4), (int)((float)(j + num2) / num4), Z.Z, 0f, 1f) + 0.25;
					double num6 = this.sampleZonescaleSimplexNoiseRange("colorB", (int)((float)(i + num) / num4), (int)((float)(j + num2) / num4), Z.Z, 0f, 1f);
					double num7 = this.sampleZonescaleSimplexNoiseRange("colorC", (int)((float)(i + num) / num4), (int)((float)(j + num2) / num4), Z.Z, 0f, 1f);
					string text3 = "r";
					if (num5 >= num6 && num5 >= num7)
					{
						text3 = "r";
					}
					if (num6 >= num5 && num6 >= num7)
					{
						text3 = "b";
					}
					if (num7 >= num5 && num7 >= num6)
					{
						text3 = "W";
					}
					string text4 = "A";
					if ((i + j) % 2 == 0)
					{
						text4 = "B";
					}
					if (cellType == Reef.CellType.ReefOuter)
					{
						Z.GetCell(i, j).AddObject("Coral Polyp " + text4, null, null, null, null).Render.DetailColor = text3;
					}
					else if (cellType == Reef.CellType.ReefMiddle)
					{
						Z.GetCell(i, j).AddObject("Coral and Palladium Strut " + text4, null, null, null, null).Render.DetailColor = text3;
					}
					else if (cellType == Reef.CellType.ReefInner)
					{
						Z.GetCell(i, j).AddObject("Palladium Strut " + text4, null, null, null, null).Render.DetailColor = text3;
					}
					else if (cellType != Reef.CellType.ReefTidal)
					{
						if (cellType == Reef.CellType.ReefShorelineTidal)
						{
							if (Stat.Random(1, 100) <= 60)
							{
								int num8 = Stat.Random(0, 1);
								if (num8 == 0)
								{
									Z.GetCell(i, j).AddObject("Tunnel Sponge", null, null, null, null);
								}
								if (num8 == 1)
								{
									Z.GetCell(i, j).AddObject("Finger Coral", null, null, null, null);
								}
							}
						}
						else if (cellType == Reef.CellType.ReefInnerTidal)
						{
							if (Stat.Random(1, 100) <= 10)
							{
								int num9 = Stat.Random(0, 1);
								if (num9 == 0)
								{
									Z.GetCell(i, j).AddObject("Tunnel Sponge", null, null, null, null);
								}
								if (num9 == 1)
								{
									Z.GetCell(i, j).AddObject("Finger Coral", null, null, null, null);
								}
							}
						}
						else if (cellType == Reef.CellType.ReefPool)
						{
							Z.GetCell(i, j).AddObject("AlgalWaterDeepPool", null, null, null, null);
						}
					}
					if (cellType == Reef.CellType.Wall)
					{
						Z.GetCell(i, j).AddObject(text2, null, null, null, null);
					}
					else if (cellType == Reef.CellType.Detail && !string.IsNullOrEmpty(text))
					{
						Z.GetCell(i, j).AddObject(text, null, null, null, null);
					}
				}
			}
			double num10 = (double)list4[1].Item2 / (double)list4[0].Item2 * 0.5;
			num10 += this.sampleSimplexNoiseRange("secondaryThreshold", gridX, gridY, Z.Z, -0.2f, 0.2f);
			if (num10 > 0.5)
			{
				num10 = 1.0 - num10;
			}
			GameObject gameObject = GameObject.Create(list4[0].Item1);
			GameObject gameObject2 = GameObject.Create(list4[1].Item1);
			string text5 = "&" + gameObject.Render.GetForegroundColor();
			string text6 = "&" + gameObject.Render.GetBackgroundColor();
			string text7 = "&" + gameObject2.Render.GetForegroundColor();
			string text8 = "&" + gameObject2.Render.GetBackgroundColor();
			for (int k = 0; k < Z.Height; k++)
			{
				for (int l = 0; l < Z.Width; l++)
				{
					Cell cell = Z.GetCell(l, k);
					if (cell.PaintTile.IsNullOrEmpty())
					{
						if (this.sampleZonescaleSimplexNoiseRange("layoutSelection", l + num, k + num2, Z.Z, 0f, 1f) > num3)
						{
							if (If.CoinFlip(null, null))
							{
								cell.PaintColorString = text5;
							}
							else
							{
								cell.PaintColorString = text6;
							}
							cell.PaintTile = "Tiles/tile-dirt1.png";
							cell.PaintDetailColor = "k";
						}
						else
						{
							if (If.CoinFlip(null, null))
							{
								cell.PaintColorString = text7;
							}
							else
							{
								cell.PaintColorString = text8;
							}
							cell.PaintTile = "Tiles/tile-dirt1.png";
							cell.PaintDetailColor = "k";
						}
						if (If.Chance(5, null, null))
						{
							cell.PaintColorString = "&y";
						}
						int num11 = Stat.Random(1, 5);
						if (num11 == 1)
						{
							cell.PaintRenderString = ".";
						}
						if (num11 == 2)
						{
							cell.PaintRenderString = ".";
						}
						if (num11 == 3)
						{
							cell.PaintRenderString = ".";
						}
						if (num11 == 4)
						{
							cell.PaintRenderString = ".";
						}
					}
				}
			}
			int num12 = this.sampleZonescaleSimplexNoiseIntRange("mazePathWidth", gridX, gridY, Z.Z, 0, 4);
			this.ConnectMaze(Z, num12, num12 + 1);
			Z.GetCell(0, 0).AddObject("Kelpy", null, null, null, null);
			return true;
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x002762D4 File Offset: 0x002744D4
		[CompilerGenerated]
		internal static int <getReefLayout>g__adjacentPoolCount|30_0(int x, int y, int r = 1, ref Reef.<>c__DisplayClass30_0 A_3)
		{
			int num = 0;
			for (int i = x - r; i <= x + r; i++)
			{
				for (int j = y - r; j <= y + r; j++)
				{
					if (i >= 0 && j >= 0 && i < A_3.Z.Width && j < A_3.Z.Height && (i != x || j != y) && A_3.result[i, j] == Reef.CellType.ReefPool)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x00276344 File Offset: 0x00274544
		[CompilerGenerated]
		internal static bool <getReefLayout>g__poolWithinX|30_1(int x, int y, int r, ref Reef.<>c__DisplayClass30_0 A_3)
		{
			for (int i = x - r; i <= x + r; i++)
			{
				for (int j = y - r; j <= y + r; j++)
				{
					if (i >= 0 && j >= 0 && i < A_3.Z.Width && j < A_3.Z.Height && A_3.result[i, j] == Reef.CellType.ReefPool)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04003358 RID: 13144
		private const int MaxWidth = 1200;

		// Token: 0x04003359 RID: 13145
		private const int MaxHeight = 375;

		// Token: 0x0400335A RID: 13146
		public bool Noise;

		// Token: 0x0400335B RID: 13147
		[NonSerialized]
		public static Maze3D TunnelMaze;

		// Token: 0x0400335C RID: 13148
		public const int GREY_FLOORTILE_CHANCE = 5;

		// Token: 0x0400335D RID: 13149
		private FastNoise fastNoise = new FastNoise(1337);

		// Token: 0x02001B18 RID: 6936
		public enum CellType
		{
			// Token: 0x04006BB4 RID: 27572
			Wall,
			// Token: 0x04006BB5 RID: 27573
			Open,
			// Token: 0x04006BB6 RID: 27574
			Detail,
			// Token: 0x04006BB7 RID: 27575
			ReefInner,
			// Token: 0x04006BB8 RID: 27576
			ReefMiddle,
			// Token: 0x04006BB9 RID: 27577
			ReefOuter,
			// Token: 0x04006BBA RID: 27578
			ReefPool,
			// Token: 0x04006BBB RID: 27579
			ReefTidal,
			// Token: 0x04006BBC RID: 27580
			ReefShorelineTidal,
			// Token: 0x04006BBD RID: 27581
			ReefInnerTidal
		}
	}
}
