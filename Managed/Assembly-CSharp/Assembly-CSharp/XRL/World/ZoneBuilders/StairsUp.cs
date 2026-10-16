using System;
using System.Collections.Generic;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A7F RID: 2687
	public class StairsUp
	{
		// Token: 0x06006E82 RID: 28290 RVA: 0x0025CE00 File Offset: 0x0025B000
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < this.Number; i++)
			{
				if (!StairsUp.AddStairUp(Z, this.x, this.y, this.Reachable))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006E83 RID: 28291 RVA: 0x0025CE3C File Offset: 0x0025B03C
		public static bool AddStairUp(Zone Z, string x, string y, bool Reachable)
		{
			if (x == "-1" && y == "-1")
			{
				bool flag = false;
				foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnectionsCopy(Z.ZoneID))
				{
					if (!(zoneConnection.Type != "StairsUp"))
					{
						Cell cell = Z.GetCell(zoneConnection.X, zoneConnection.Y);
						if (!cell.HasObjectWithTag("Stairs"))
						{
							if (cell.IsReachable())
							{
								cell.Clear(null, false, false, null);
							}
							cell.ClearWalls();
							cell.AddObject((!string.IsNullOrEmpty(zoneConnection.Object)) ? zoneConnection.Object : "StairsUp", null, null, null, null);
							flag = true;
						}
					}
				}
				if (flag)
				{
					return true;
				}
			}
			if (The.ZoneManager.IsZoneBuilt(The.ZoneManager.GetZoneFromIDAndDirection(Z.ZoneID, "u")))
			{
				return true;
			}
			List<Cell> list = Z.GetEmptyReachableCellsWithout(StairsUp.StairsDownBlueprints);
			if (list == null || list.Count == 0)
			{
				list = Z.GetEmptyCells((Cell C) => C.X > 5 && C.X < 75 && C.Y > 3 && C.Y < 21, null);
			}
			if (list == null || list.Count == 0)
			{
				list = Z.GetEmptyCells(null, null);
			}
			if (list == null || list.Count == 0)
			{
				list = Z.GetCells((Cell c) => !c.HasWall());
			}
			Algorithms.RandomShuffleInPlace<Cell>(list, Stat.Rand);
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			if (x != "-1" && !string.IsNullOrEmpty(x))
			{
				try
				{
					num = Stat.RollMin(x);
					num2 = Stat.RollMax(x);
				}
				catch (Exception)
				{
					Debug.LogError("Invalid x value: " + x);
					num = -1;
					num2 = -1;
				}
			}
			if (y != "-1" && !string.IsNullOrEmpty(y))
			{
				try
				{
					num3 = Stat.RollMin(y);
					num4 = Stat.RollMax(y);
				}
				catch
				{
					Debug.LogError("Invalid y value: " + y);
					num3 = -1;
					num4 = -1;
				}
			}
			foreach (Cell cell2 in list)
			{
				if ((num == -1 || cell2.X >= num) && (num2 == -1 || cell2.X <= num2) && (num3 == -1 || cell2.Y >= num3) && (num4 == -1 || cell2.Y <= num4))
				{
					int num5 = cell2.X;
					int num6 = cell2.Y;
					if (!cell2.HasObjectWithBlueprint("StairsDown") && !cell2.HasObjectWithBlueprint("StairsUp") && !cell2.HasObjectWithBlueprint("OpenShaft") && !cell2.HasObjectWithBlueprint("LazyAir") && !cell2.HasObjectWithBlueprint("LazyPit") && !cell2.HasObjectWithBlueprint("Pit") && cell2.IsReachable())
					{
						cell2.Clear(null, false, false, null);
						cell2.AddObject("StairsUp", null, null, null, null);
						Z.CacheZoneConnection("u", num5, num6, "StairsDown", "StairsDown");
						Z.CacheZoneConnection("-", num5, num6, "Connection", null);
						if (Reachable)
						{
							Z.BuildReachableMap(num5, num6);
						}
						return true;
					}
				}
			}
			if (list.Count > 0)
			{
				list[0].Clear(null, false, false, null);
				list[0].AddObject("StairsUp", null, null, null, null);
				return true;
			}
			return false;
		}

		// Token: 0x0400328D RID: 12941
		public int Number = 1;

		// Token: 0x0400328E RID: 12942
		public string x = "-1";

		// Token: 0x0400328F RID: 12943
		public string y = "-1";

		// Token: 0x04003290 RID: 12944
		public bool Reachable = true;

		// Token: 0x04003291 RID: 12945
		[NonSerialized]
		private static string[] StairsDownBlueprints = new string[] { "StairsDown", "OpenShaft", "LazyAir", "LazyPit", "Pit", "StairBlocker" };
	}
}
