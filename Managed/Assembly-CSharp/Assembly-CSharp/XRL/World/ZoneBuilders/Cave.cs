using System;
using System.Collections.Generic;
using Genkit;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A52 RID: 2642
	[HasGameBasedStaticCache]
	public class Cave : ZoneBuilderSandbox
	{
		// Token: 0x06006E00 RID: 28160 RVA: 0x002569A3 File Offset: 0x00254BA3
		public static void Save(SerializationWriter Writer)
		{
			Writer.Write(0);
		}

		// Token: 0x06006E01 RID: 28161 RVA: 0x002569AC File Offset: 0x00254BAC
		public static void Load(SerializationReader Reader)
		{
			Reader.ReadInt32();
			Cave.CaveNoise = null;
			Cave.TunnelMaze = null;
		}

		// Token: 0x06006E02 RID: 28162 RVA: 0x002569C4 File Offset: 0x00254BC4
		public bool BuildZone(Zone Z)
		{
			string defaultWall = base.GetDefaultWall(Z);
			Z.ClearReachableMap();
			Z.Fill(defaultWall);
			int seededRange = base.GetSeededRange("TunnelSeed", int.MinValue, int.MaxValue);
			if (Cave.TunnelMaze == null)
			{
				Cave.TunnelMaze = RecursiveBacktrackerMaze3D.Generate(base.GetSeededRange("TunnelMaze", int.MinValue, int.MaxValue), 240, 75, 30, false, 10, 5, 5, 5, 5, 5);
			}
			if (Cave.CaveNoise == null)
			{
				Random seededRand = base.GetSeededRand("CaveNoise");
				Cave.CaveNoise = PerlinNoise2Df.sumNoiseFunctions(1200, 375, 0, 0, new List<PerlinNoise2Df>
				{
					new PerlinNoise2Df(4, 1f, seededRand),
					new PerlinNoise2Df(8, 0.86f, seededRand),
					new PerlinNoise2Df(64, 0.6f, seededRand),
					new PerlinNoise2Df(300, 0.36f, seededRand),
					new PerlinNoise2Df(1200, 0.06f, seededRand)
				}, 0.5f);
				Cave.CaveNoise = PerlinNoise2Df.Smooth(Cave.CaveNoise, 1200, 375, 1);
			}
			CellularGrid cellularGrid = new CellularGrid();
			cellularGrid.Passes = 2;
			cellularGrid.SeedChance = 60;
			cellularGrid.SeedBorders = true;
			cellularGrid.BorderDepth = 2;
			cellularGrid.Generate(Stat.Rand, Z.Width, Z.Height);
			int num = Z.wX * 240 + Z.X * 80 + Z.Z * 3;
			int num2 = Z.wY * 75 + Z.Y * 25 + Z.Z * 5;
			num %= 1119;
			num2 %= 349;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (j + num < 1200 && i + num2 < 375)
					{
						double num3 = (double)Cave.CaveNoise[j + num, i + num2];
						if (cellularGrid.cells[j, i] == 0 || num3 <= 0.4699999988079071)
						{
							Z.GetCell(j, i).Clear(null, false, false, null);
							Z.GetCell(j, i).SetReachable(true);
						}
					}
				}
			}
			int num4 = Z.wX * 3 + Z.X;
			int num5 = Z.wY * 3 + Z.Y;
			MazeCell3D mazeCell3D = Cave.TunnelMaze.Cell[num4, num5, Math.Max(0, Z.Z - 11) % 30];
			if (8.in100())
			{
				if (mazeCell3D.N)
				{
					Z.CacheZoneConnection("-", new Point(base.GetSeededRange(num5 + seededRange * Z.Z, 10, 70), 0), "cave", null);
				}
				if (mazeCell3D.S)
				{
					Z.CacheZoneConnection("-", new Point(base.GetSeededRange(num5 + 1 + seededRange * Z.Z, 10, 70), Z.Height - 1), "cave", null);
				}
				if (mazeCell3D.E)
				{
					Z.CacheZoneConnection("-", new Point(Z.Width - 1, base.GetSeededRange(num4 + 1 + seededRange * Z.Z, 5, 20)), "cave", null);
				}
				if (mazeCell3D.W)
				{
					Z.CacheZoneConnection("-", new Point(0, base.GetSeededRange(num4 + seededRange * Z.Z, 5, 20)), "cave", null);
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
				list.Add(new Point(base.GetSeededRange(num5 + seededRange * Z.Z, 10, 70), 0));
			}
			if (mazeCell3D.S)
			{
				list.Add(new Point(base.GetSeededRange(num5 + 1 + seededRange * Z.Z, 10, 70), Z.Height - 1));
			}
			if (mazeCell3D.E)
			{
				list.Add(new Point(Z.Width - 1, base.GetSeededRange(num4 + 1 + seededRange * Z.Z, 5, 20)));
			}
			if (mazeCell3D.W)
			{
				list.Add(new Point(0, base.GetSeededRange(num4 + seededRange * Z.Z, 5, 20)));
			}
			Algorithms.RandomShuffleInPlace<Point>(list, Stat.Rand);
			while (list.Count < 2)
			{
				list.Add(new Point(Stat.Random(10, 70), Stat.Random(5, 15)));
				list.Add(new Point(Stat.Random(10, 70), Stat.Random(5, 15)));
			}
			if (list.Count > 0)
			{
				for (int k = 0; k < list.Count; k++)
				{
					Z.CacheZoneConnection("-", list[k].X, list[k].Y, "tunnelnode", null);
				}
				GameObject gameObject = null;
				gameObject = GameObjectFactory.Factory.CreateObject("Drillbot");
				int x = list[0].X;
				int y = list[0].Y;
				for (int l = 1; l < list.Count; l++)
				{
					if (x != list[l].X || y != list[l].Y)
					{
						FindPath findPath = new FindPath(Z, x, y, Z, list[l].X, list[l].Y, false, true, gameObject, true);
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
							foreach (Cell cell3 in cell2.GetLocalAdjacentCells(Stat.Random(1, 2), false))
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
			for (int m = 0; m < Z.Height; m++)
			{
				for (int n = 0; n < Z.Width; n++)
				{
					if (Z.GetCell(n, m).IsReachable())
					{
						Z.BuildReachableMap(n, m, false);
						goto IL_08F2;
					}
				}
			}
			IL_08F2:
			if (Z.Z > 49)
			{
				new LiquidPools().BuildZone(Z, "LavaPool", Z.Z - 50, "0-2", null);
			}
			if (Stat.Random(1, 1000) <= 2)
			{
				InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(Z, list, InfluenceMapSeedStrategy.FurthestPoint, 100, null, false, null);
				new CaveCity().BuildZone(Z, influenceMap, "Door", "BrinestalkWall", 1, 9, 3, 3, "4", 100, null, null, null, null);
				Z.GetCell(0, 0).AddObject("Rocky", null, null, null, null);
			}
			else
			{
				Z.GetCell(0, 0).AddObject("Rocky", null, null, null, null);
			}
			return true;
		}

		// Token: 0x04003262 RID: 12898
		public static float[,] CaveNoise;

		// Token: 0x04003263 RID: 12899
		private const int MaxWidth = 1200;

		// Token: 0x04003264 RID: 12900
		private const int MaxHeight = 375;

		// Token: 0x04003265 RID: 12901
		public bool Noise;

		// Token: 0x04003266 RID: 12902
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static Maze3D TunnelMaze;
	}
}
