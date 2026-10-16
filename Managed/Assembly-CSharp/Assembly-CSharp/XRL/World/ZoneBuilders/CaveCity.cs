using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A53 RID: 2643
	public class CaveCity : ZoneBuilderSandbox
	{
		// Token: 0x06006E04 RID: 28164 RVA: 0x002573C4 File Offset: 0x002555C4
		public static bool MakeRegionCaveBuilding(Zone Z, InfluenceMap IF, InfluenceMapRegion R, string DoorObject = "Door", string WallObject = "BrinestalkWall", int WallRadius = 4, string BuildingDecorationPopulation = null, string BuildingCreaturePopulation = null, Location2D Anchor = null)
		{
			bool flag = false;
			List<Location2D> list = new List<Location2D>();
			foreach (Location2D location2D in R.Cells)
			{
				if (IF.CostMap[location2D.X, location2D.Y] < WallRadius)
				{
					list.Add(location2D);
				}
				if (IF.CostMap[location2D.X, location2D.Y] == WallRadius)
				{
					if (!flag)
					{
						if (Anchor == null)
						{
							Z.GetCell(location2D.X, location2D.Y).AddObject(DoorObject, null, null, null, null);
						}
						if (Anchor != null)
						{
							Z.GetCell(location2D.X, location2D.Y).AddObject(WallObject, null, null, null, null);
						}
						flag = true;
					}
					else
					{
						Z.GetCell(location2D.X, location2D.Y).AddObject(WallObject, null, null, null, null);
					}
				}
			}
			if (flag)
			{
				if (BuildingDecorationPopulation != null)
				{
					ZoneBuilderSandbox.PlacePopulationInRegion(Z, list, BuildingDecorationPopulation, null, null);
				}
				if (BuildingCreaturePopulation != null)
				{
					ZoneBuilderSandbox.PlacePopulationInRegion(Z, list, BuildingCreaturePopulation, null, null);
				}
			}
			return flag;
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x00257500 File Offset: 0x00255700
		public bool BuildZone(Zone Z, InfluenceMap IF, string DoorObject = "Door", string WallObject = "BrinestalkWall", int MaxAdjacentRegions = 1, int MinRegionSize = 9, int MinRegionHeight = 3, int MinRegionWidth = 3, string WallRadius = "4", int ChancePerZone = 100, string BuildingDecorationPopulation = null, string BuildingCreaturePopulation = null, string BuildingBossPopulation = null, Location2D Anchor = null)
		{
			List<InfluenceMapRegion> list = new List<InfluenceMapRegion>();
			bool flag = false;
			Func<Location2D, bool> <>9__0;
			foreach (InfluenceMapRegion influenceMapRegion in IF.Regions)
			{
				if (influenceMapRegion.AdjacentRegions.Count <= MaxAdjacentRegions && Stat.Random(1, 100) <= ChancePerZone && influenceMapRegion.Size >= MinRegionSize && influenceMapRegion.BoundingBox.Width >= MinRegionWidth && influenceMapRegion.BoundingBox.Height >= MinRegionHeight)
				{
					IEnumerable<Location2D> cells = influenceMapRegion.Cells;
					Func<Location2D, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (Location2D c) => Z.GetCell(c).HasStairs());
					}
					if (!cells.Any<Location2D>(func))
					{
						if (!flag)
						{
							if (CaveCity.MakeRegionCaveBuilding(Z, IF, influenceMapRegion, DoorObject, WallObject, Stat.Roll(WallRadius, null), BuildingDecorationPopulation, BuildingBossPopulation, Anchor))
							{
								list.Add(influenceMapRegion);
								flag = true;
							}
						}
						else if (CaveCity.MakeRegionCaveBuilding(Z, IF, influenceMapRegion, DoorObject, WallObject, Stat.Roll(WallRadius, null), BuildingDecorationPopulation, BuildingCreaturePopulation, Anchor))
						{
							list.Add(influenceMapRegion);
						}
					}
				}
			}
			Func<int, int, int, int> <>9__2;
			foreach (InfluenceMapRegion influenceMapRegion2 in list)
			{
				bool bConvertedDoor = false;
				Zone z = Z;
				Location2D location2D = IF.Seeds[influenceMapRegion2.Seed];
				bool flag2 = true;
				float num = 0.2f;
				int num2 = 0;
				Action<Cell> action = delegate(Cell c)
				{
					if (c.HasObject(WallObject))
					{
						c.Clear(null, false, false, null);
						if (!bConvertedDoor)
						{
							c.AddObject(DoorObject, null, null, null, null);
							bConvertedDoor = true;
							return;
						}
					}
					else if (c.HasWall())
					{
						c.ClearWalls();
					}
				};
				Func<int, int, int, int> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = delegate(int x, int y, int c)
					{
						if (x == 0)
						{
							return 2000;
						}
						if (y == 0)
						{
							return 2000;
						}
						if (x == Z.Width - 1)
						{
							return 2000;
						}
						if (y == Z.Height - 1)
						{
							return 2000;
						}
						int num3 = (Z.GetCell(x, y).HasObjectInDirection("N", WallObject) ? 1 : 0) + (Z.GetCell(x, y).HasObjectInDirection("S", WallObject) ? 1 : 0);
						int num4 = (Z.GetCell(x, y).HasObjectInDirection("E", WallObject) ? 1 : 0) + (Z.GetCell(x, y).HasObjectInDirection("W", WallObject) ? 1 : 0);
						if (Z.GetCell(x, y).HasObject(WallObject) && num3 > 0 && num4 > 0)
						{
							return 80;
						}
						if (Z.GetCell(x, y).HasObject(WallObject))
						{
							return 20;
						}
						if (Z.GetCell(x, y).HasWall())
						{
							return 80;
						}
						if (Z.GetCell(x, y).HasObject("InfluenceMapBlocker"))
						{
							return 80;
						}
						return 0;
					});
				}
				ZoneBuilderSandbox.TunnelTo(z, Anchor, location2D, flag2, num, num2, action, func2);
			}
			return true;
		}
	}
}
