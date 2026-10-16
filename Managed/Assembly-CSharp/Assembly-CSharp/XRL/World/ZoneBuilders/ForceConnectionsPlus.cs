using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A63 RID: 2659
	public class ForceConnectionsPlus : ZoneBuilderSandbox
	{
		// Token: 0x06006E3E RID: 28222 RVA: 0x0025A940 File Offset: 0x00258B40
		public bool BuildZone(Zone Z)
		{
			return this._BuildZone(Z, null);
		}

		// Token: 0x06006E3F RID: 28223 RVA: 0x0025A94C File Offset: 0x00258B4C
		public bool _BuildZone(Zone Z, List<Point2D> additional = null)
		{
			if (Z.GetZoneProperty("DisableForcedConnections", "") == "Yes")
			{
				return true;
			}
			List<Cell> cells = Z.GetCells((Cell c) => c.IsPassable(null, true));
			Z.ClearReachableMap();
			Z.SetReachable(cells[0].X, cells[0].Y, true);
			Z.BuildReachableMap(cells[0].X, cells[0].Y, false);
			GameObject gameObject = null;
			gameObject = GameObjectFactory.Factory.CreateObject("Drillbot");
			for (int i = 1; i < cells.Count; i++)
			{
				if (!Z.IsReachable(cells[i].X, cells[i].Y))
				{
					FindPath findPath = new FindPath(Z, cells[0].X, cells[0].Y, Z, cells[i].X, cells[i].Y, false, true, gameObject, true);
					if (findPath.Usable)
					{
						foreach (Cell cell in findPath.Steps)
						{
							Z.ReachableMap[cell.X, cell.Y] = true;
							cell.ClearWalls();
						}
						if (this.CaveLike)
						{
							foreach (Cell cell2 in findPath.Steps)
							{
								if (!cell2.IsReachable() && !cell2.HasWall())
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
						Z.BuildReachableMap(cells[i].X, cells[i].Y, false);
					}
				}
			}
			return true;
		}

		// Token: 0x04003274 RID: 12916
		public bool CaveLike = true;
	}
}
