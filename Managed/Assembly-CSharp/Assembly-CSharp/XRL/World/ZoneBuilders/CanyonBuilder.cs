using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A56 RID: 2646
	public class CanyonBuilder
	{
		// Token: 0x06006E22 RID: 28194 RVA: 0x00259C28 File Offset: 0x00257E28
		public bool BuildZone(Zone Z)
		{
			List<CachedZoneConnection> list = new List<CachedZoneConnection>();
			bool flag = false;
			int num = -1;
			int num2 = -1;
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && cachedZoneConnection.Type.Contains("Canyon"))
				{
					if (cachedZoneConnection.Type.Contains("Start"))
					{
						num = cachedZoneConnection.X;
						num2 = cachedZoneConnection.Y;
					}
					list.Add(cachedZoneConnection);
				}
			}
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type.Contains("Stairs"))
				{
					list.Add(new CachedZoneConnection("-", zoneConnection.X, zoneConnection.Y, "", null));
				}
				if (zoneConnection.Type.Contains("Canyon"))
				{
					if (zoneConnection.Type.Contains("Start"))
					{
						flag = true;
						num = zoneConnection.X;
						num2 = zoneConnection.Y;
					}
					list.Add(new CachedZoneConnection("-", zoneConnection.X, zoneConnection.Y, zoneConnection.Type, null));
				}
			}
			GameObject gameObject = null;
			gameObject = GameObjectFactory.Factory.CreateObject("Drillbot");
			if (num == -1 || num2 == -1)
			{
				flag = true;
				num = Stat.Random(6, Z.Width - 6);
				num2 = Stat.Random(6, Z.Height - 6);
			}
			if (flag)
			{
				if (Z.ReachableMap == null)
				{
					Z.ClearReachableMap(false);
				}
				int num3 = num - 2;
				int num4 = num2 - 2;
				for (int i = 0; i < 4; i++)
				{
					for (int j = 0; j < 4; j++)
					{
						Z.ReachableMap[i + num3, j + num4] = true;
						Z.GetCell(i + num3, j + num4).Clear(null, false, false, null);
					}
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				if (num != list[k].X || num2 != list[k].Y)
				{
					FindPath findPath = new FindPath(Z, num, num2, Z, list[k].X, list[k].Y, false, true, gameObject, true);
					if (findPath.Usable)
					{
						foreach (Cell cell in findPath.Steps)
						{
							Z.ReachableMap[cell.X, cell.Y] = true;
							cell.Clear(null, false, false, null);
						}
						string objectTypeForZone = ZoneManager.GetObjectTypeForZone(Z.wX, Z.wY, Z.GetZoneWorld());
						int num5 = 3;
						if (objectTypeForZone != "Mountains" && objectTypeForZone != "Hills")
						{
							num5 = 1;
						}
						foreach (Cell cell2 in findPath.Steps)
						{
							foreach (Cell cell3 in cell2.GetLocalAdjacentCells(Stat.Random(1, num5), false))
							{
								Z.ReachableMap[cell3.X, cell3.Y] = true;
								cell3.Clear(null, false, false, null);
								cell3.AddObject(GameObjectFactory.Factory.CreateObject("CanyonMarker"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						foreach (Cell cell4 in findPath.Steps)
						{
							cell4.AddObject("CanyonMarker", null, null, null, null);
						}
					}
				}
			}
			return true;
		}
	}
}
