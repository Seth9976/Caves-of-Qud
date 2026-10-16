using System;
using System.Collections.Generic;
using Genkit;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A62 RID: 2658
	public class ForceConnections : ZoneBuilderSandbox
	{
		// Token: 0x06006E3A RID: 28218 RVA: 0x0025A336 File Offset: 0x00258536
		public bool BuildZone(Zone Z)
		{
			return this._BuildZone(Z, null);
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x0025A340 File Offset: 0x00258540
		public bool _BuildZone(Zone Z, List<Point2D> additional = null)
		{
			if (Z.GetZoneProperty("DisableForcedConnections", "") == "Yes")
			{
				return true;
			}
			List<Point2D> list = new List<Point2D>();
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					if ((Z.GetCell(i, j).HasObjectWithPart("StairsUp") || Z.GetCell(i, j).HasObjectWithPart("StairsDown")) && !list.Contains(new Point2D(i, j)))
					{
						list.Add(new Point2D(i, j));
					}
				}
			}
			if (additional != null)
			{
				foreach (Point2D point2D in additional)
				{
					if (!list.Contains(point2D))
					{
						list.Add(point2D);
					}
				}
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && !list.Contains(new Point2D(cachedZoneConnection.X, cachedZoneConnection.Y)))
				{
					list.Add(new Point2D(cachedZoneConnection.X, cachedZoneConnection.Y));
				}
			}
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (!list.Contains(new Point2D(zoneConnection.X, zoneConnection.Y)))
				{
					list.Add(new Point2D(zoneConnection.X, zoneConnection.Y));
				}
			}
			Algorithms.RandomShuffleInPlace<Point2D>(list);
			List<Cell> emptyReachableCells = Z.GetEmptyReachableCells();
			if (emptyReachableCells.Count == 0 && list.Count > 0)
			{
				emptyReachableCells.Add(Z.GetCell(list[0].location));
			}
			if (emptyReachableCells.Count <= 0)
			{
				return true;
			}
			emptyReachableCells.ShuffleInPlace<Cell>(null);
			list.Add(emptyReachableCells[0].Pos2D);
			if (list.Count == 0)
			{
				return true;
			}
			Z.ClearReachableMap();
			Z.SetReachable(list[0].x, list[0].y, true);
			Z.BuildReachableMap(list[0].x, list[0].y, false);
			ForceConnections.pathNoise.SetSeed(Stat.Random(int.MinValue, int.MaxValue));
			ForceConnections.pathNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
			ForceConnections.pathNoise.SetFractalOctaves(2);
			ForceConnections.pathNoise.SetFrequency(0.2f);
			GameObject.Create("Drillbot");
			using (Pathfinder pathfinder = Z.getPathfinder(delegate(int x, int y, Cell c)
			{
				int num = (int)(Math.Abs(ForceConnections.pathNoise.GetNoise((float)((x + Z.wX * 80) / 3), (float)(y + Z.wY * 25))) * 160f);
				if (Z.GetCell(x, y).HasWall())
				{
					return 80 + num;
				}
				if (Z.GetCell(x, y).HasObject("InfluenceMapBlocker"))
				{
					return 80 + num;
				}
				return num;
			}))
			{
				for (int k = 1; k < list.Count; k++)
				{
					if (!Z.IsReachable(list[k].x, list[k].y) && pathfinder.FindPath(list[0].location, list[k].location, false, true, 24300, true))
					{
						foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
						{
							Z.ReachableMap[pathfinderNode.X, pathfinderNode.Y] = true;
							Cell cell = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
							if (cell != null)
							{
								cell.ClearWalls();
							}
						}
						if (this.CaveLike)
						{
							foreach (PathfinderNode pathfinderNode2 in pathfinder.Steps)
							{
								Cell cell2 = Z.GetCell(pathfinderNode2.X, pathfinderNode2.Y);
								if (cell2 != null && !cell2.IsReachable() && !cell2.HasWall())
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
						Z.BuildReachableMap(list[k].x, list[k].y, false);
					}
				}
			}
			return true;
		}

		// Token: 0x04003272 RID: 12914
		public bool CaveLike = true;

		// Token: 0x04003273 RID: 12915
		public new static FastNoise pathNoise = new FastNoise(1337);
	}
}
