using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genkit;
using HistoryKit;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A54 RID: 2644
	public class Strata : ZoneBuilderSandbox
	{
		// Token: 0x06006E07 RID: 28167 RVA: 0x00257748 File Offset: 0x00255948
		public static void Save(SerializationWriter Writer)
		{
			Writer.Write(0);
		}

		// Token: 0x06006E08 RID: 28168 RVA: 0x00257751 File Offset: 0x00255951
		public static void Load(SerializationReader Reader)
		{
			Reader.ReadInt32();
			Strata.TunnelMaze = null;
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x00257760 File Offset: 0x00255960
		public bool ConnectMaze(Zone Z, int minMazeWidth = 1, int maxMazeWidth = 2)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			int seededRange = base.GetSeededRange("TunnelSeed", int.MinValue, int.MaxValue);
			if (Strata.TunnelMaze == null)
			{
				Strata.TunnelMaze = RecursiveBacktrackerMaze3D.Generate(base.GetSeededRange("TunnelMaze", int.MinValue, int.MaxValue), 240, 75, 30, false, 10, 5, 5, 5, 5, 5);
			}
			MazeCell3D mazeCell3D = Strata.TunnelMaze.Cell[num, num2, Math.Max(0, Z.Z - 11) % 30];
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
				if (this.Stairs)
				{
					The.ZoneManager.AddZonePostBuilderIfNotAlreadyPresent(Z.ZoneID, "ForceConnections");
					if (mazeCell3D.D)
					{
						new StairsDown().BuildZone(Z);
					}
					if (mazeCell3D.U)
					{
						new StairsUp().BuildZone(Z);
					}
				}
				return true;
			}
			if (this.Stairs)
			{
				if (mazeCell3D.D)
				{
					new StairsDown().BuildZone(Z);
				}
				if (mazeCell3D.U)
				{
					new StairsUp().BuildZone(Z);
				}
			}
			List<Point> list = new List<Point>();
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				bool? flag;
				if (zoneConnection == null)
				{
					flag = null;
				}
				else
				{
					string type = zoneConnection.Type;
					flag = ((type != null) ? new bool?(type.Contains("Pull")) : null);
				}
				bool? flag2 = flag;
				if (!flag2.GetValueOrDefault())
				{
					list.Add(new Point(zoneConnection.X, zoneConnection.Y));
				}
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

		// Token: 0x06006E0A RID: 28170 RVA: 0x00257EF0 File Offset: 0x002560F0
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

		// Token: 0x06006E0B RID: 28171 RVA: 0x00258014 File Offset: 0x00256214
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

		// Token: 0x06006E0C RID: 28172 RVA: 0x002580D8 File Offset: 0x002562D8
		public int getSeed(string seed)
		{
			return XRLCore.Core.Game.GetWorldSeed(seed);
		}

		// Token: 0x06006E0D RID: 28173 RVA: 0x002580EC File Offset: 0x002562EC
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

		// Token: 0x06006E0E RID: 28174 RVA: 0x00258180 File Offset: 0x00256380
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

		// Token: 0x06006E0F RID: 28175 RVA: 0x0025821C File Offset: 0x0025641C
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

		// Token: 0x06006E10 RID: 28176 RVA: 0x002582D4 File Offset: 0x002564D4
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

		// Token: 0x06006E11 RID: 28177 RVA: 0x00258364 File Offset: 0x00256564
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

		// Token: 0x06006E12 RID: 28178 RVA: 0x00258400 File Offset: 0x00256600
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

		// Token: 0x06006E13 RID: 28179 RVA: 0x002584B8 File Offset: 0x002566B8
		public Strata.CellType[,] getRandomWalkCaveLayout(Zone Z)
		{
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
			int num = 500;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					array[j, i] = Strata.CellType.Wall;
				}
			}
			int num2 = Stat.Random(0, Z.Width - 1);
			int num3 = Stat.Random(0, Z.Height - 1);
			for (int k = 0; k < 3; k++)
			{
				for (int l = 0; l < num; l++)
				{
					array[num2, num3] = Strata.CellType.Open;
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
						if (array[m, n] == Strata.CellType.Wall)
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

		// Token: 0x06006E14 RID: 28180 RVA: 0x00258650 File Offset: 0x00256850
		public int countBoxFilterAt(int x, int y, Strata.CellType[,] cells)
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
			if (cells[x, y] == Strata.CellType.Wall)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06006E15 RID: 28181 RVA: 0x0025867C File Offset: 0x0025687C
		public int countBoxFilterAdjacentD1(int x, int y, Strata.CellType[,] cells)
		{
			return 0 + this.countBoxFilterAt(x - 1, y - 1, cells) + this.countBoxFilterAt(x - 1, y, cells) + this.countBoxFilterAt(x - 1, y + 1, cells) + this.countBoxFilterAt(x, y - 1, cells) + this.countBoxFilterAt(x, y + 1, cells) + this.countBoxFilterAt(x + 1, y - 1, cells) + this.countBoxFilterAt(x + 1, y, cells) + this.countBoxFilterAt(x + 1, y + 1, cells);
		}

		// Token: 0x06006E16 RID: 28182 RVA: 0x002586F4 File Offset: 0x002568F4
		public int countBoxFilterAdjacentD2(int x, int y, Strata.CellType[,] cells)
		{
			return 0 + this.countBoxFilterAdjacentD1(x, y, cells) + this.countBoxFilterAt(x - 2, y - 1, cells) + this.countBoxFilterAt(x - 2, y, cells) + this.countBoxFilterAt(x - 2, y + 1, cells) + this.countBoxFilterAt(x - 1, y - 2, cells) + this.countBoxFilterAt(x, y - 2, cells) + this.countBoxFilterAt(x + 1, y - 2, cells) + this.countBoxFilterAt(x - 1, y + 2, cells) + this.countBoxFilterAt(x, y + 2, cells) + this.countBoxFilterAt(x + 1, y + 2, cells) + this.countBoxFilterAt(x + 2, y - 1, cells) + this.countBoxFilterAt(x + 2, y, cells) + this.countBoxFilterAt(x + 2, y + 1, cells);
		}

		// Token: 0x06006E17 RID: 28183 RVA: 0x002587AC File Offset: 0x002569AC
		public Strata.CellType[,] getBoxFilterCave(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
			int num3 = this.sampleZonescaleSimplexNoiseIntRange("rwcavesteps", num, num2, Z.Z, 40, 60);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (j <= 2 || j >= Z.Width - 2 || i <= 2 || i >= Z.Height - 2 || num3.in100())
					{
						array[j, i] = Strata.CellType.Wall;
					}
					else
					{
						array[j, i] = Strata.CellType.Open;
					}
				}
			}
			int num4 = this.sampleZonescaleSimplexNoiseIntRange("rwcavesteps", num, num2, Z.Z, 2, 4);
			Strata.CellType[,] array2 = new Strata.CellType[Z.Width, Z.Height];
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
							array2[l, m] = Strata.CellType.Wall;
						}
						else if (this.countBoxFilterAdjacentD2(l, m, array) <= num6)
						{
							array2[l, m] = Strata.CellType.Wall;
						}
						else
						{
							array2[l, m] = Strata.CellType.Open;
						}
					}
				}
				Strata.CellType[,] array3 = array;
				array = array2;
				array2 = array3;
			}
			return array;
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x00258930 File Offset: 0x00256B30
		public Strata.CellType[,] getCaveLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
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
							array[j, i] = Strata.CellType.Open;
						}
						else
						{
							array[j, i] = Strata.CellType.Wall;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06006E19 RID: 28185 RVA: 0x00258A94 File Offset: 0x00256C94
		public Strata.CellType[,] getPillarsLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
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
						array[i, j] = Strata.CellType.Wall;
					}
					else if (noise >= num5)
					{
						array[i, j] = Strata.CellType.Detail;
					}
					else
					{
						array[i, j] = Strata.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006E1A RID: 28186 RVA: 0x00258C2C File Offset: 0x00256E2C
		public Strata.CellType[,] getPorousLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
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
					if (noise < num4)
					{
						array[i, j] = Strata.CellType.Wall;
					}
					else if ((double)noise >= 0.05)
					{
						array[i, j] = Strata.CellType.Detail;
					}
					else
					{
						array[i, j] = Strata.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x00258D6C File Offset: 0x00256F6C
		public Strata.CellType[,] getBlockyLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
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
						array[i, j] = Strata.CellType.Wall;
					}
					else if ((double)noise >= 0.95)
					{
						array[i, j] = Strata.CellType.Detail;
					}
					else
					{
						array[i, j] = Strata.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x00258EBC File Offset: 0x002570BC
		public Strata.CellType[,] getWindyLayout(Zone Z)
		{
			int num = Z.wX * 3 + Z.X;
			int num2 = Z.wY * 3 + Z.Y;
			Strata.CellType[,] array = new Strata.CellType[Z.Width, Z.Height];
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
					if (noise > num4)
					{
						array[i, j] = Strata.CellType.Wall;
					}
					else if ((double)noise >= 0.05)
					{
						array[i, j] = Strata.CellType.Detail;
					}
					else
					{
						array[i, j] = Strata.CellType.Open;
					}
				}
			}
			return array;
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x00259004 File Offset: 0x00257204
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
				int num7 = e.Item2;
				num7 += (int)this.sampleSimplexNoise(e.Item1, gridX, gridY, depth, WALLTYPE_NOISE_AMPLITUDE, 0.33f);
				return new Tuple<string, int>(e.Item1, num7);
			});
			List<string> list5 = list4.Select<Tuple<string, int>, string>((Tuple<string, int> e) => e.Item1).ToList<string>();
			string text = this.getRandomWeightedFromElements(Z.X + num, Z.Y + num2, depth, list2, delegate(Tuple<string, int> e)
			{
				int num8 = e.Item2;
				num8 += (int)this.sampleSimplexNoise(e.Item1, gridX, gridY, depth, WALLTYPE_NOISE_AMPLITUDE, 1f);
				return new Tuple<string, int>(e.Item1, num8);
			});
			Z.SetZoneProperty("DefaultWall", list5[0]);
			Strata.CellType[,] array;
			if (list5[0] == "Oolite" || list5[0] == "Marl")
			{
				array = this.getBoxFilterCave(Z);
			}
			else if (list5[0] == "Black Marble")
			{
				array = this.getPillarsLayout(Z);
			}
			else if (list5[0] == "Halite" || list5[0] == "Gypsum" || list5[0] == "Porous Coral Rag")
			{
				array = this.getPorousLayout(Z);
			}
			else if (list5[0] == "Limestone" || list5[0] == "Coral Rag")
			{
				array = this.getRandomWalkCaveLayout(Z);
			}
			else if (list5[0] == "Serpentinite" || list5[0] == "Sandstone")
			{
				array = this.getWindyLayout(Z);
			}
			else if (list5[0] == "Quartzite")
			{
				array = this.getBlockyLayout(Z);
			}
			else
			{
				array = this.getCaveLayout(Z);
			}
			Strata.CellType[,] array2;
			if (list5[1] == "Black Marble")
			{
				array2 = this.getPillarsLayout(Z);
			}
			else if (list5[1] == "Oolite" || list5[1] == "Marl")
			{
				array2 = this.getBoxFilterCave(Z);
			}
			else if (list5[1] == "Halite" || list5[1] == "Gypsum" || list5[0] == "Porous Coral Rag")
			{
				array2 = this.getPorousLayout(Z);
			}
			else if (list5[1] == "Quartzite")
			{
				array2 = this.getBlockyLayout(Z);
			}
			else if (list5[1] == "Limestone" || list5[1] == "Coral Rag")
			{
				array2 = this.getRandomWalkCaveLayout(Z);
			}
			else if (list5[1] == "Serpentinite" || list5[1] == "Sandstone")
			{
				array2 = this.getWindyLayout(Z);
			}
			else
			{
				array2 = this.getCaveLayout(Z);
			}
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
					Strata.CellType cellType;
					string text2;
					if (this.sampleZonescaleSimplexNoiseRange("layoutSelection", i + num, j + num2, Z.Z, 0f, 1f) > num3)
					{
						cellType = array[i, j];
						text2 = list5[0];
					}
					else
					{
						cellType = array2[i, j];
						text2 = list5[1];
					}
					if (cellType == Strata.CellType.Wall)
					{
						Z.GetCell(i, j).AddObject(text2, null, null, null, null);
					}
					else if (cellType == Strata.CellType.Detail && !string.IsNullOrEmpty(text))
					{
						Z.GetCell(i, j).AddObject(text, null, null, null, null);
					}
				}
			}
			double num4 = (double)list4[1].Item2 / (double)list4[0].Item2 * 0.5;
			num4 += this.sampleSimplexNoiseRange("secondaryThreshold", gridX, gridY, Z.Z, -0.2f, 0.2f);
			if (num4 > 0.5)
			{
				num4 = 1.0 - num4;
			}
			GameObject gameObject = GameObject.Create(list4[0].Item1);
			GameObject gameObject2 = GameObject.Create(list4[1].Item1);
			string text3 = "&" + gameObject.Render.GetForegroundColor();
			string text4 = "&" + gameObject.Render.GetBackgroundColor();
			string text5 = "&" + gameObject2.Render.GetForegroundColor();
			string text6 = "&" + gameObject2.Render.GetBackgroundColor();
			Z.SetZoneProperty("primaryFloorColor1", text3);
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
								cell.PaintColorString = text3;
							}
							else
							{
								cell.PaintColorString = text4;
							}
							cell.PaintTile = "Tiles/tile-dirt1.png";
							cell.PaintDetailColor = "k";
						}
						else
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
						if (If.Chance(5, null, null))
						{
							cell.PaintColorString = "&y";
						}
						int num5 = Stat.Random(1, 5);
						if (num5 == 1)
						{
							cell.PaintRenderString = ".";
						}
						if (num5 == 2)
						{
							cell.PaintRenderString = ".";
						}
						if (num5 == 3)
						{
							cell.PaintRenderString = ".";
						}
						if (num5 == 4)
						{
							cell.PaintRenderString = ".";
						}
					}
				}
			}
			int num6 = this.sampleZonescaleSimplexNoiseIntRange("mazePathWidth", gridX, gridY, Z.Z, 0, 4);
			this.ConnectMaze(Z, num6, num6 + 1);
			return true;
		}

		// Token: 0x04003267 RID: 12903
		private const int MaxWidth = 1200;

		// Token: 0x04003268 RID: 12904
		private const int MaxHeight = 375;

		// Token: 0x04003269 RID: 12905
		public bool Noise;

		// Token: 0x0400326A RID: 12906
		public bool Stairs = true;

		// Token: 0x0400326B RID: 12907
		[NonSerialized]
		public static Maze3D TunnelMaze;

		// Token: 0x0400326C RID: 12908
		public const int GREY_FLOORTILE_CHANCE = 5;

		// Token: 0x0400326D RID: 12909
		private FastNoise fastNoise = new FastNoise(1337);

		// Token: 0x02001AF9 RID: 6905
		public enum CellType
		{
			// Token: 0x04006B50 RID: 27472
			Wall,
			// Token: 0x04006B51 RID: 27473
			Open,
			// Token: 0x04006B52 RID: 27474
			Detail
		}
	}
}
