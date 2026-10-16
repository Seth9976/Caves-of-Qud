using System;
using System.Collections.Generic;
using Genkit;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A71 RID: 2673
	public class RiverBuilder
	{
		// Token: 0x06006E60 RID: 28256 RVA: 0x0025B84C File Offset: 0x00259A4C
		public RiverBuilder()
		{
		}

		// Token: 0x06006E61 RID: 28257 RVA: 0x0025B85F File Offset: 0x00259A5F
		public RiverBuilder(bool hardClear, string Puddle = "SaltyWaterDeepPool", bool VillageMode = false)
		{
			this.VillageMode = VillageMode;
			this.Puddle = Puddle;
			this.HardClear = hardClear;
		}

		// Token: 0x06006E62 RID: 28258 RVA: 0x0025B888 File Offset: 0x00259A88
		public bool BuildZone(Zone Z)
		{
			if (Z.IsCheckpoint())
			{
				this.VillageMode = true;
			}
			if (Z.GetTerrainObject().HasTag("NoRivers"))
			{
				return true;
			}
			if (Z.GetTerrainObject().Blueprint == "TerrainSaltdunes" || Z.GetTerrainObject().Blueprint == "TerrainSaltdunes2" || Z.GetTerrainObject().Blueprint == "TerrainTremblezone")
			{
				this.Puddle = "SaltDeepPool";
			}
			if (Z.GetTerrainObject().GetBlueprint(true).InheritsFrom("TerrainFungalOuter"))
			{
				this.Puddle = "ProteanDeepPool";
			}
			if (Z.GetTerrainObject().GetBlueprint(true).InheritsFrom("TerrainPalladiumReef") || Z.GetTerrainObject().GetBlueprint(true).InheritsFrom("TerrainLakeHinnom"))
			{
				this.Puddle = "AlgalWaterDeepPool";
			}
			if (Z.GetTerrainObject().Blueprint == "TerrainMoonStair" || Z.GetTerrainObject().GetBlueprint(true).InheritsFrom("TerrainMoonStair"))
			{
				this.Puddle = "DilutedWarmStaticPuddle";
			}
			List<CachedZoneConnection> list = new List<CachedZoneConnection>();
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && cachedZoneConnection.Type.Contains("River"))
				{
					list.Add(cachedZoneConnection);
				}
			}
			int num = 40;
			int num2 = 20;
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				if (zoneConnection.Type.Contains("River"))
				{
					if (zoneConnection.Type.Contains("Start"))
					{
						num = zoneConnection.X;
						num2 = zoneConnection.Y;
					}
					list.Add(new CachedZoneConnection("-", zoneConnection.X, zoneConnection.Y, zoneConnection.Type, null));
				}
			}
			bool flag = list.Count == 1;
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
			if (Z.BuildTries > 5 || this.Pairs)
			{
				GameObjectFactory.Factory.CreateObject("Drillbot");
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
							if (this.HardClear)
							{
								Z.GetCell(j, i).Clear(null, false, false, null);
							}
							else
							{
								Z.GetCell(j, i).ClearWalls();
							}
							Z.GetCell(j, i).AddObject(this.Puddle, null, null, null, null);
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
					FastNoise pathNoise = new FastNoise(1337);
					pathNoise.SetSeed(Stat.Random(int.MinValue, int.MaxValue));
					pathNoise.SetNoiseType(FastNoise.NoiseType.Simplex);
					pathNoise.SetFractalOctaves(4);
					pathNoise.SetFrequency(0.1f);
					Pathfinder pathfinder = Z.getPathfinder(delegate(int x, int y, Cell c)
					{
						int num6 = (int)(Math.Abs(pathNoise.GetNoise((float)((x + Z.wX * 80) / 3), (float)(y + Z.wY * 25))) * 190f);
						if (this.VillageMode)
						{
							num6 = 0;
						}
						if (!Z.GetCell(x, y).HasWall())
						{
							if (this.VillageMode)
							{
								if (Z.GetCell(x, y).AnyLocalAdjacentCell((Cell c) => c.HasWall()))
								{
									return 8000;
								}
							}
							return num6;
						}
						if (!this.VillageMode)
						{
							return 20 + num6;
						}
						return 8000;
					});
					if (pathfinder.FindPath(Location2D.Get(num, num2), Location2D.Get(list[k].X, list[k].Y), false, true, 24300, true))
					{
						foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
						{
							Cell cell = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
							if (this.VillageMode)
							{
								if (cell.AnyObject((GameObject o) => o.GetBlueprint(true).DescendsFrom("Floor")))
								{
									cell.AddObject("Bridge", null, null, null, null);
								}
								else if (this.HardClear)
								{
									cell.Clear(null, false, false, null);
								}
								else
								{
									cell.ClearTerrain();
								}
							}
							else if (this.HardClear)
							{
								cell.Clear(null, false, false, null);
							}
							else
							{
								cell.ClearTerrain();
							}
							Z.ReachableMap[pathfinderNode.X, pathfinderNode.Y] = true;
							cell.AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
					int num5 = 2;
					if (this.VillageMode)
					{
						num5 = 1;
					}
					foreach (PathfinderNode pathfinderNode2 in pathfinder.Steps)
					{
						Cell cell2 = Z.GetCell(pathfinderNode2.X, pathfinderNode2.Y);
						foreach (Cell cell3 in cell2.GetLocalAdjacentCells(num5, true))
						{
							if (cell3.CosmeticDistanceTo(cell2.Pos2D) <= 1)
							{
								if (this.VillageMode)
								{
									if (cell3.AnyObject((GameObject o) => o.GetBlueprint(true).DescendsFrom("Floor")))
									{
										cell3.AddObject("Bridge", null, null, null, null);
									}
									else if (cell3 == cell2 || cell3.IsEmpty())
									{
										Z.ReachableMap[pathfinderNode2.X, pathfinderNode2.Y] = true;
										cell3.ClearTerrain();
										if (cell3.IsEmpty())
										{
											cell3.AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
										}
									}
								}
								else
								{
									Z.ReachableMap[cell3.X, cell3.Y] = true;
									if (this.HardClear)
									{
										cell3.Clear(null, false, false, null);
										cell3.AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									}
									else
									{
										cell3.ClearTerrain();
										if (cell3.IsEmpty())
										{
											cell3.AddObject(GameObjectFactory.Factory.CreateObject(this.Puddle), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
										}
									}
								}
							}
						}
					}
					pathfinder.Dispose();
				}
			}
			if (list.Count > 0)
			{
				Z.SetZoneProperty("ambient_bed_3", "Sounds/Ambiences/amb_bed_river");
			}
			return true;
		}

		// Token: 0x0400327E RID: 12926
		public string Puddle = "SaltyWaterDeepPool";

		// Token: 0x0400327F RID: 12927
		public bool Pairs;

		// Token: 0x04003280 RID: 12928
		public bool HardClear;

		// Token: 0x04003281 RID: 12929
		private bool VillageMode;
	}
}
