using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.World.AI.Pathfinding;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A77 RID: 2679
	public class RoadBuilder
	{
		// Token: 0x06006E6D RID: 28269 RVA: 0x0025C1D3 File Offset: 0x0025A3D3
		public RoadBuilder()
		{
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x0025C1F0 File Offset: 0x0025A3F0
		public RoadBuilder(bool HardClear)
		{
			this.HardClear = HardClear;
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x0025C214 File Offset: 0x0025A414
		public bool BuildRoads(Zone Z, List<Point> Roads, bool bClearSolids, bool bNoise)
		{
			for (int i = 1; i < Roads.Count; i++)
			{
				GameObject gameObject = null;
				if (Z.BuildTries > 5)
				{
					gameObject = GameObject.Create("Drillbot");
				}
				if (Roads[0].X != Roads[i].X || Roads[0].Y != Roads[i].Y)
				{
					FindPath findPath = new FindPath();
					findPath.PerformPathfind(Z, Roads[0].X, Roads[0].Y, Z, Roads[i].X, Roads[i].Y, false, gameObject, true, bNoise, false, null, int.MaxValue, false, false, false, false, false, false, true);
					if (!findPath.Usable)
					{
						return false;
					}
					foreach (Cell cell in findPath.Steps)
					{
						Action<Cell> beforePlacement = this.BeforePlacement;
						if (beforePlacement != null)
						{
							beforePlacement(cell);
						}
						GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("LiquidVolume");
						if (firstObjectWithPart != null)
						{
							if (!firstObjectWithPart.LiquidVolume.ContainsLiquid("blood") && !cell.HasBridge())
							{
								cell.AddObject(GameObject.Create("Bridge"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						else if (bClearSolids || !cell.IsOccluding())
						{
							Z.ReachableMap[cell.X, cell.Y] = true;
							if (bClearSolids)
							{
								if (this.HardClear)
								{
									cell.Clear(null, false, false, null);
								}
								else
								{
									cell.ClearTerrain();
								}
							}
							if (!cell.HasObjectWithBlueprint("DirtRoad"))
							{
								cell.AddObject(GameObject.Create("DirtRoad"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
					foreach (Cell cell2 in findPath.Steps)
					{
						foreach (Cell cell3 in cell2.GetLocalAdjacentCells())
						{
							Action<Cell> beforePlacement2 = this.BeforePlacement;
							if (beforePlacement2 != null)
							{
								beforePlacement2(cell3);
							}
							GameObject openLiquidVolume = cell3.GetOpenLiquidVolume();
							if (openLiquidVolume != null)
							{
								if (!openLiquidVolume.LiquidVolume.ContainsLiquid("blood") && !cell3.HasBridge())
								{
									cell3.AddObject(GameObject.Create("Bridge"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
							else if (bClearSolids || !cell3.IsOccluding())
							{
								Z.ReachableMap[cell3.X, cell3.Y] = true;
								if (bClearSolids && this.ClearAdjacent)
								{
									if (this.HardClear)
									{
										cell3.Clear(null, false, false, null);
									}
									else
									{
										cell3.ClearTerrain();
									}
								}
								if (!cell3.HasObjectWithBlueprint("DirtRoad"))
								{
									cell3.AddObject(GameObjectFactory.Factory.CreateObject("DirtRoad"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x0025C598 File Offset: 0x0025A798
		public bool BuildZone(Zone Z)
		{
			List<Point> list = new List<Point>();
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && cachedZoneConnection.Type.Contains("Road"))
				{
					list.Add(new Point(cachedZoneConnection.X, cachedZoneConnection.Y));
				}
			}
			bool flag = false;
			int num = 40;
			int num2 = 20;
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type.Contains("Road"))
				{
					if (zoneConnection.Type.Contains("Start"))
					{
						flag = true;
						num = zoneConnection.X;
						num2 = zoneConnection.Y;
						list.Insert(0, new Point(zoneConnection.X, zoneConnection.Y));
					}
					else
					{
						list.Add(new Point(zoneConnection.X, zoneConnection.Y));
					}
				}
			}
			if (flag)
			{
				int num3 = num - 5;
				int num4 = num2 - 5;
				NoiseMap noiseMap = new NoiseMap(10, 10, 10, 3, 3, 2, 80, 80, 3, 1, 0, 1, null);
				for (int i = 0; i < 10; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						if (noiseMap.Noise[i, j] > 1)
						{
							Z.GetCell(i + num3, j + num4).AddObject(GameObjectFactory.Factory.CreateObject("DirtPath"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				this.BuildRoads(Z, list, this.ClearSolids, this.Noise);
			}
			return true;
		}

		// Token: 0x04003282 RID: 12930
		public bool HardClear;

		// Token: 0x04003283 RID: 12931
		public bool ClearSolids = true;

		// Token: 0x04003284 RID: 12932
		public bool ClearAdjacent = true;

		// Token: 0x04003285 RID: 12933
		public bool Noise = true;

		// Token: 0x04003286 RID: 12934
		[NonSerialized]
		public Action<Cell> BeforePlacement;
	}
}
