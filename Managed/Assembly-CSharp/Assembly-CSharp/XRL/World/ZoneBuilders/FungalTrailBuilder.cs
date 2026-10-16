using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A64 RID: 2660
	public class FungalTrailBuilder
	{
		// Token: 0x06006E41 RID: 28225 RVA: 0x0025ABDC File Offset: 0x00258DDC
		public bool BuildZone(Zone Z)
		{
			List<CachedZoneConnection> list = new List<CachedZoneConnection>();
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && cachedZoneConnection.Type.Contains("FungalTrail"))
				{
					list.Add(cachedZoneConnection);
				}
			}
			int num = 40;
			int num2 = 20;
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type.Contains("FungalTrail"))
				{
					if (zoneConnection.Type.Contains("Start"))
					{
						num = zoneConnection.X;
						num2 = zoneConnection.Y;
					}
					list.Add(new CachedZoneConnection("-", zoneConnection.X, zoneConnection.Y, "", null));
				}
			}
			bool flag = false;
			if (list.Count == 1)
			{
				flag = true;
			}
			if (list.Count <= 1)
			{
				num = 40;
				num2 = 20;
			}
			else
			{
				num = list[0].X;
				num2 = list[0].Y;
			}
			GameObject gameObject = null;
			if (Z.BuildTries > 5 || this.Pairs)
			{
				gameObject = GameObjectFactory.Factory.CreateObject("Drillbot");
			}
			if (flag)
			{
				CellularGrid cellularGrid = new CellularGrid();
				cellularGrid.SeedBorders = false;
				cellularGrid.Passes = 4;
				cellularGrid.SeedChance = 40;
				cellularGrid.Generate(Stat.Rand, 80, 30);
				int i = 1;
				int num3 = Z.Height - 1;
				while (i < num3)
				{
					int j = 1;
					int num4 = Z.Width - 1;
					while (j < num4)
					{
						if (cellularGrid.cells[j, i] == 1)
						{
							Z.ReachableMap[j, i] = true;
							Z.GetCell(j, i).ClearTerrain();
							Z.GetCell(j, i).AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						j++;
					}
					i++;
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
							if (Stat.Random(1, 100) <= 100)
							{
								cell.ClearTerrain();
								cell.AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
						foreach (Cell cell2 in findPath.Steps)
						{
							foreach (Cell cell3 in cell2.GetLocalAdjacentCells())
							{
								Z.ReachableMap[cell3.X, cell3.Y] = true;
								if (Stat.Random(1, 100) <= 100)
								{
									cell3.ClearTerrain();
									cell3.AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04003275 RID: 12917
		public string Puddle = "FungalTrailBrick";

		// Token: 0x04003276 RID: 12918
		public bool Pairs;
	}
}
