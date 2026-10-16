using System;
using System.Collections.Generic;
using Genkit;
using UnityEngine;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC4 RID: 2756
	public class PopTableZoneBuilder
	{
		// Token: 0x06006F8D RID: 28557 RVA: 0x00271BD4 File Offset: 0x0026FDD4
		public bool BuildZone(Zone NewZone, string Table, string Density = "minimum", bool bApplyZoneFactionToObjects = false)
		{
			if (!PopulationManager.HasPopulation(Table))
			{
				Debug.LogError("Unknown population table: " + Table);
				return true;
			}
			List<GameObject> list = PopulationManager.Expand(PopulationManager.Generate(Table, null, null, null, null, null));
			if (this.Pairs == null)
			{
				this.Pairs = new List<Point2D>();
			}
			this.Pairs.Clear();
			if (1000 - 1 == 0)
			{
				return false;
			}
			List<Cell> cells = NewZone.GetCells();
			List<Cell> list2 = new List<Cell>();
			List<Cell> list3 = new List<Cell>();
			List<Cell> list4 = new List<Cell>();
			foreach (Cell cell in cells)
			{
				if (!cell.HasSpawnBlocker())
				{
					if (cell.HasWall())
					{
						list3.Add(cell);
					}
					else if (cell.HasWadingDepthLiquid() && !cell.HasBridge())
					{
						list2.Add(cell);
					}
					else if (cell.IsReachable() && cell.IsEmptyOfSolid(true))
					{
						list4.Add(cell);
					}
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = false;
				bool flag2 = false;
				if (list[i].Property.ContainsKey("StartInLiquid"))
				{
					flag = true;
				}
				if (list[i].HasPart<Brain>() && list[i].Brain.Aquatic)
				{
					flag = true;
				}
				if (list[i].HasPart<Brain>() && list[i].Brain.LivesOnWalls)
				{
					flag2 = true;
				}
				if (bApplyZoneFactionToObjects && NewZone.GetZoneProperty("faction", null) != null && list[i].HasPart<Brain>())
				{
					list[i].Brain.Factions = "";
					list[i].Brain.Allegiance.Clear();
					list[i].Brain.Allegiance.Add(NewZone.GetZoneProperty("faction", ""), 100);
					list[i].RequirePart<DisplayNameAdjectives>(false).AddAdjective("domesticated");
				}
				List<Cell> list5 = list4;
				if (flag && list2.Count > 0)
				{
					list5 = list2;
				}
				if (flag2 && list3.Count > 0)
				{
					list5 = list3;
				}
				if (list5.Count <= 0)
				{
					list5 = cells;
				}
				int num = Stat.Random(0, list5.Count - 1);
				list5[num].AddObject(list[i], false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				if (list5 != cells)
				{
					list5.Remove(list5[num]);
				}
			}
			return true;
		}

		// Token: 0x04003325 RID: 13093
		private List<Point2D> Pairs;
	}
}
