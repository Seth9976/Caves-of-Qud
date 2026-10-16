using System;
using System.Collections.Generic;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A7E RID: 2686
	public class StairsDown
	{
		// Token: 0x06006E7D RID: 28285 RVA: 0x0025C924 File Offset: 0x0025AB24
		public StairsDown setEmptyOnly(bool val)
		{
			this.EmptyOnly = val;
			return this;
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x0025C930 File Offset: 0x0025AB30
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < this.Number; i++)
			{
				if (!StairsDown.AddStairDown(Z, this.x, this.y, this.Reachable, this.EmptyOnly))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x0025C974 File Offset: 0x0025AB74
		public static bool AddStairDown(Zone Z, string x, string y, bool Reachable, bool EmptyOnly)
		{
			bool flag = false;
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnectionsCopy(Z.ZoneID))
			{
				if (!(zoneConnection.Type != "StairsDown"))
				{
					Cell cell = Z.GetCell(zoneConnection.X, zoneConnection.Y);
					if (!cell.HasObjectWithTag("Stairs"))
					{
						if (cell.IsReachable())
						{
							cell.Clear(null, false, false, null);
						}
						cell.ClearWalls();
						cell.AddObject(zoneConnection.Object.IsNullOrEmpty() ? "StairsDown" : zoneConnection.Object, null, null, null, null);
						flag = true;
					}
				}
			}
			if (The.ZoneManager.IsZoneBuilt(The.ZoneManager.GetZoneFromIDAndDirection(Z.ZoneID, "d")))
			{
				return true;
			}
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
			if (num == -1 || num3 == -1 || num2 - num != 0 || num4 - num3 != 0)
			{
				if (flag)
				{
					return true;
				}
				foreach (GameObject gameObject in Z.GetObjectsWithTag("Stairs"))
				{
					StairsDown stairsDown;
					if (gameObject.TryGetPart<StairsDown>(out stairsDown) && !gameObject.HasTagOrProperty("IgnoreForStairConnections") && stairsDown.Connected)
					{
						return true;
					}
				}
			}
			List<Cell> list = null;
			if (EmptyOnly)
			{
				list = (Reachable ? Z.GetEmptyReachableCellsWithout(StairsDown.UpStairsBlueprints) : Z.GetEmptyCells(null, null));
			}
			else if (Reachable)
			{
				list = Z.GetReachableCellsWithout(StairsDown.UpStairsBlueprints);
			}
			else
			{
				list = Z.GetCells((Cell c) => !c.HasWall());
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
			foreach (Cell cell2 in list)
			{
				if ((num == -1 || cell2.X >= num) && (num2 == -1 || cell2.X <= num2) && (num3 == -1 || cell2.Y >= num3) && (num4 == -1 || cell2.Y <= num4))
				{
					int num5 = cell2.X;
					int num6 = cell2.Y;
					if (!cell2.HasObjectWithTag("Stairs") && (!Reachable || cell2.IsReachable()))
					{
						cell2.Clear(null, false, false, null);
						cell2.AddObject("StairsDown", null, null, null, null);
						Z.CacheZoneConnection("d", num5, num6, "StairsUp", "StairsUp");
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
				list[0].AddObject("StairsDown", null, null, null, null);
				return true;
			}
			return false;
		}

		// Token: 0x04003287 RID: 12935
		public int Number = 1;

		// Token: 0x04003288 RID: 12936
		public string x = "-1";

		// Token: 0x04003289 RID: 12937
		public string y = "-1";

		// Token: 0x0400328A RID: 12938
		public bool Reachable = true;

		// Token: 0x0400328B RID: 12939
		public bool EmptyOnly = true;

		// Token: 0x0400328C RID: 12940
		[NonSerialized]
		private static string[] UpStairsBlueprints = new string[] { "StairsUp", "OpenShaft", "LazyPit", "LazyAir", "Pit", "StairBlocker" };
	}
}
