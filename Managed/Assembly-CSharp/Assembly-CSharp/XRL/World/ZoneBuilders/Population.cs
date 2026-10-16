using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC5 RID: 2757
	public class Population : ZoneBuilderSandbox
	{
		// Token: 0x06006F8F RID: 28559 RVA: 0x00271E84 File Offset: 0x00270084
		public bool BuildZone(Zone NewZone)
		{
			if (!PopulationManager.HasPopulation(this.Table))
			{
				Debug.LogError("Unknown population table: " + this.Table);
				return true;
			}
			List<GameObject> list = PopulationManager.Expand(PopulationManager.Generate(this.Table, "zonetier", NewZone.NewTier.ToString(), null, null, null));
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
				if (list[i].HasTagOrProperty("StartInLiquid"))
				{
					flag = true;
				}
				if (list[i].LimitToAquatic())
				{
					flag = true;
				}
				if (list[i].Brain != null && list[i].Brain.LivesOnWalls)
				{
					flag2 = true;
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

		// Token: 0x04003326 RID: 13094
		public string Table;

		// Token: 0x04003327 RID: 13095
		public string Density = "lowest";
	}
}
