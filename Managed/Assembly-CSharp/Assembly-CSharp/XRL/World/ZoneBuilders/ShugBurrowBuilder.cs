using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A95 RID: 2709
	public class ShugBurrowBuilder
	{
		// Token: 0x06006EED RID: 28397 RVA: 0x00262926 File Offset: 0x00260B26
		public ShugBurrowBuilder()
		{
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x00262939 File Offset: 0x00260B39
		public ShugBurrowBuilder(bool hardClear, string Wall = "BaseNephilimWall_Shug'rith")
		{
			this.Wall = Wall;
			this.HardClear = hardClear;
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x0026295C File Offset: 0x00260B5C
		public bool BuildZone(Zone Z)
		{
			List<CachedZoneConnection> list = new List<CachedZoneConnection>();
			List<ZoneConnection> list2 = new List<ZoneConnection>();
			List<ZoneConnection> list3 = new List<ZoneConnection>();
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-")
				{
					if (cachedZoneConnection.Type.Contains("Descending"))
					{
						list2.Add(cachedZoneConnection);
					}
					if (cachedZoneConnection.Type.Contains("Ascending"))
					{
						list3.Add(cachedZoneConnection);
					}
					if (cachedZoneConnection.Type.Contains("ShugBurrow"))
					{
						list.Add(cachedZoneConnection);
					}
				}
			}
			int num = 40;
			int num2 = 20;
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type.Contains("ShugBurrow"))
				{
					if (zoneConnection.Type.Contains("Descending") || zoneConnection.Type.Contains("Ascending"))
					{
						num = zoneConnection.X;
						num2 = zoneConnection.Y;
						if (zoneConnection.Type.Contains("Descending"))
						{
							list2.Add(zoneConnection);
						}
						if (zoneConnection.Type.Contains("Ascending"))
						{
							list3.Add(zoneConnection);
						}
					}
					list.Add(new CachedZoneConnection("-", zoneConnection.X, zoneConnection.Y, zoneConnection.Type, null));
				}
			}
			if (list.Count <= 1)
			{
				num = Stat.Random(5, 75);
				num2 = Stat.Random(5, 20);
			}
			else
			{
				num = list[0].X;
				num2 = list[0].Y;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (num != list[i].X || num2 != list[i].Y)
				{
					FastNoise pathNoise = new FastNoise(1337);
					pathNoise.SetSeed(Stat.Random(int.MinValue, int.MaxValue));
					pathNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
					pathNoise.SetFractalOctaves(4);
					pathNoise.SetFrequency(0.1f);
					Pathfinder pathfinder = Z.getPathfinder(delegate(int x, int y, Cell c)
					{
						int num3 = (int)(Math.Abs(pathNoise.GetNoise((float)((x + Z.wX * 80) / 3), (float)(y + Z.wY * 25))) * 190f);
						if (Z.GetCell(x, y).HasWall())
						{
							return 20 + num3;
						}
						return num3;
					});
					if (pathfinder.FindPath(Location2D.Get(num, num2), Location2D.Get(list[i].X, list[i].Y), false, true, 24300, true))
					{
						foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
						{
							foreach (Cell cell in Z.GetCell(pathfinderNode.X, pathfinderNode.Y).GetLocalAdjacentCellsCircular(2, true))
							{
								Z.ReachableMap[pathfinderNode.X, pathfinderNode.Y] = true;
								if (this.HardClear)
								{
									cell.Clear(null, false, false, null);
								}
								else
								{
									cell.ClearTerrain();
								}
							}
						}
					}
					foreach (PathfinderNode pathfinderNode2 in pathfinder.Steps)
					{
						foreach (Cell cell2 in Z.GetCell(pathfinderNode2.X, pathfinderNode2.Y).GetLocalAdjacentCellsCircular(3, true))
						{
							if (cell2.HasWall())
							{
								cell2.ClearWalls();
								cell2.AddObject(this.Wall, null, null, null, null);
							}
							else if (Stat.Random(1, 100) <= 1)
							{
								cell2.AddObject(PopulationManager.RollOneFrom("IsolatedPocketDecoration", null, null).Blueprint, null, null, null, null);
							}
						}
					}
					pathfinder.Dispose();
				}
			}
			foreach (ZoneConnection zoneConnection2 in list3)
			{
				foreach (Cell cell3 in Z.GetCell(zoneConnection2.Loc2D).GetLocalAdjacentCellsCircular(3, true))
				{
					cell3.ClearWalls();
				}
				foreach (Cell cell4 in Z.GetCell(zoneConnection2.Loc2D).GetLocalAdjacentCellsCircular(4, true))
				{
					if (cell4.HasWall())
					{
						cell4.ClearWalls();
						cell4.AddObject("BaseNephilimWall_Shug'rith", null, null, null, null);
					}
				}
			}
			foreach (ZoneConnection zoneConnection3 in list2)
			{
				foreach (Cell cell5 in Z.GetCell(zoneConnection3.Loc2D).GetLocalAdjacentCellsCircular(4, true))
				{
					if (cell5.HasWall())
					{
						cell5.ClearWalls();
						cell5.AddObject("BaseNephilimWall_Shug'rith", null, null, null, null);
					}
				}
				foreach (Cell cell6 in Z.GetCell(zoneConnection3.Loc2D).GetLocalAdjacentCellsCircular(3, true))
				{
					cell6.ClearWalls();
					GameObject gameObject = GameObjectFactory.Factory.CreateObject("LazyPit");
					if (Z.Z == 10)
					{
						GameObject gameObject2 = GameObject.Create(Z.GetDefaultWall());
						string text = "&" + gameObject2.Render.GetForegroundColor();
						gameObject.Render.ColorString = text + "^k";
						gameObject.Render.TileColor = text + "^k";
					}
					gameObject.GetPart<StairsDown>().ConnectLanding = false;
					cell6.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					cell6.AddObject("StairBlocker", null, null, null, null);
					cell6.AddObject("InfluenceMapBlocker", null, null, null, null);
				}
			}
			ZoneTemplateManager.Templates["ShugruithTunnel"].Execute(Z, null);
			return true;
		}

		// Token: 0x040032BD RID: 12989
		public string Wall = "BaseNephilimWall_Shug'rith";

		// Token: 0x040032BE RID: 12990
		public bool Pairs;

		// Token: 0x040032BF RID: 12991
		public bool HardClear;
	}
}
