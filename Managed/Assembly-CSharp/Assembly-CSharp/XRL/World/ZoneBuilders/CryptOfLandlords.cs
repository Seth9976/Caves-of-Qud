using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B00 RID: 2816
	public class CryptOfLandlords : ZoneBuilderSandbox
	{
		// Token: 0x0600708B RID: 28811 RVA: 0x002839E8 File Offset: 0x00281BE8
		public bool BuildFlames(Zone zone)
		{
			zone.FillHollowBox(new Box(0, 0, zone.Width - 1, zone.Height - 1), "MachineWallHotTubing");
			zone.FillBox(new Box(40, 0, 40, zone.Height - 1), "MachineWallHotTubing", true);
			zone.FillBox(new Box(11, 4, 13, 6), "MachineWallHotTubing", false);
			zone.FillBox(new Box(0, 0, 79, 4), "MachineWallHotTubing", false);
			zone.FillBox(new Box(0, 20, 79, 24), "MachineWallHotTubing", false);
			Grid<Color4> grid = new Grid<Color4>(40, 25);
			grid.fromWFCTemplate("compound3", 3, int.MaxValue);
			grid = grid.mirrorHorizontal();
			grid = grid.mirrorVertical();
			for (int i = 0; i < 40; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					if (grid.get(i, j) == Color4.black)
					{
						zone.GetCell(i, j).ClearWalls();
						zone.GetCell(i, j).AddObject("MachineWallHotTubing", null, null, null, null);
					}
				}
			}
			grid = new Grid<Color4>(40, 25);
			grid.fromWFCTemplate("longhalls2", 3, int.MaxValue);
			for (int k = 0; k < zone.Height; k++)
			{
				int l = 0;
				int num = zone.Width / 2;
				while (l < num)
				{
					if (grid.get(l, k) == Color4.black)
					{
						zone.GetCell(l + 40, k).ClearWalls();
						zone.GetCell(l + 40, k).AddObject("MachineWallHotTubing", null, null, null, null);
					}
					l++;
				}
			}
			Box leftRegion = new Box(1, 1, 39, 23);
			Box rightRegion = new Box(40, 1, 79, 23);
			Box box = new Box(60, 1, 79, 23);
			List<Cell> list = (from cell in zone.GetCells()
				where leftRegion.contains(cell.Location) && cell.HasWall()
				select cell).ToList<Cell>();
			for (int m = 0; m < 20; m++)
			{
				Cell cell8 = list.RemoveRandomElement<Cell>(null);
				if (cell8 != null)
				{
					cell8.ClearWalls();
				}
			}
			List<Cell> list2 = (from cell in zone.GetCells()
				where rightRegion.contains(cell.Location) && cell.IsEmpty()
				select cell).ToList<Cell>();
			for (int n = 0; n < 8; n++)
			{
				Cell cell2 = list2.RemoveRandomElement<Cell>(null);
				if (cell2 != null)
				{
					cell2.AddObject("OpenShaft2", null, null, null, null);
				}
			}
			new SpindleFootprint().BuildZone(zone, SpindleFootprint.FootprintMode.Spindle);
			zone.ClearBox(new Box(35, 8, 45, 16), null);
			zone.GetCell(36, 9).AddObject("TombPillarPlacement", null, null, null, null);
			if (zone.ZoneID == "JoppaWorld.53.3.2.0.9")
			{
				ZoneBuilderSandbox.PlacePrefab(zone, "preset_tile_chunks/CrematoryStairsDown.rpm", zone.GetCell(72, 10), 0, null, delegate(Cell c)
				{
					c.ClearWalls();
				}, null, null, null, null, null);
			}
			else
			{
				ZoneBuilderSandbox.PlacePrefab(zone, "preset_tile_chunks/CrematoryStairsDown.rpm", zone.GetCell(72, 9), 0, null, delegate(Cell c)
				{
					c.ClearWalls();
				}, null, null, null, null, null);
			}
			ZoneBuilderSandbox.EnsureAllVoidsConnected(zone, false, 0.2f, null);
			if (zone.Y == 0)
			{
				zone.ClearBox(new Box(68, 20, 75, 24), null);
				zone.GetCell(68, 24).AddObject("ConveyorDrive", null, null, null, null);
				zone.GetCell(74, 24).AddObject("ConveyorDrive", null, null, null, null);
				base.CreateConveyor(Location2D.Get(75, 24), Location2D.Get(60, 8), zone, delegate(int x, int y)
				{
					if ((y <= 16 || x >= 75) && x >= 60)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad").Concat<Location2D>(base.CreateConveyor(Location2D.Get(59, 8), Location2D.Get(0, 8), zone, delegate(int x, int y)
				{
					if (y <= 13 && y >= 7 && x <= 59)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad"));
				base.CreateConveyor(Location2D.Get(69, 24), Location2D.Get(60, 16), zone, delegate(int x, int y)
				{
					if (x <= 70 && x >= 60)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad").Concat<Location2D>(base.CreateConveyor(Location2D.Get(59, 16), Location2D.Get(0, 16), zone, delegate(int x, int y)
				{
					if (y >= 14 && y <= 20 && x <= 59)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad"));
				zone.GetCell(0, 8).AddObject("BeltCurtains", null, null, null, null);
				zone.GetCell(0, 16).AddObject("BeltCurtains", null, null, null, null);
				zone.GetCell(75, 24).AddObject("BeltCurtains", null, null, null, null);
				zone.GetCell(69, 24).AddObject("BeltCurtains", null, null, null, null);
			}
			else
			{
				zone.ClearBox(new Box(68, 0, 75, 5), null);
				zone.GetCell(68, 0).AddObject("ConveyorDrive", null, null, null, null);
				zone.GetCell(74, 0).AddObject("ConveyorDrive", null, null, null, null);
				base.CreateConveyor(Location2D.Get(69, 0), Location2D.Get(60, 8), zone, delegate(int x, int y)
				{
					if (x <= 69 && x >= 60)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad").Concat<Location2D>(base.CreateConveyor(Location2D.Get(59, 8), Location2D.Get(0, 8), zone, delegate(int x, int y)
				{
					if (y <= 13 && y >= 7 && x <= 59)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad"));
				base.CreateConveyor(Location2D.Get(75, 0), Location2D.Get(60, 16), zone, delegate(int x, int y)
				{
					if ((x >= 75 || y >= 16) && x >= 60)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad").Concat<Location2D>(base.CreateConveyor(Location2D.Get(59, 16), Location2D.Get(0, 16), zone, delegate(int x, int y)
				{
					if (y >= 14 && y <= 20 && x <= 59)
					{
						return 0;
					}
					return 9999;
				}, "W", "CrematoryConveyorPad"));
				zone.GetCell(69, 0).AddObject("BeltCurtains", null, null, null, null);
				zone.GetCell(75, 0).AddObject("BeltCurtains", null, null, null, null);
				zone.GetCell(0, 8).AddObject("BeltCurtains", null, null, null, null);
				zone.GetCell(0, 16).AddObject("BeltCurtains", null, null, null, null);
			}
			List<Cell> list3 = (from cell in zone.GetCells()
				where leftRegion.contains(cell.Location) && cell.HasObject("CrematoryConveyorPad")
				select cell).ToList<Cell>();
			int num2 = 6;
			for (int num3 = 0; num3 < num2; num3++)
			{
				bool flag = false;
				Location2D location2D = list3.RemoveRandomElement<Cell>(null).Location;
				for (;;)
				{
					if (location2D != null && zone.GetCell(location2D) != null)
					{
						zone.GetCell(location2D).walk((Cell c) => c.GetCellFromDirection("N", true), delegate(Cell c)
						{
							if (c.HasWall() && !c.IsEdge())
							{
								c.ClearWalls();
								c.AddObject("WalltrapFireCrematory", null, null, null, null);
								return false;
							}
							return true;
						});
						zone.GetCell(location2D).walk((Cell c) => c.GetCellFromDirection("S", true), delegate(Cell c)
						{
							if (c.HasWall())
							{
								c.ClearWalls();
								c.AddObject("WalltrapFireCrematory", null, null, null, null);
								return false;
							}
							return true;
						});
						zone.GetCell(location2D).walk((Cell c) => c.GetCellFromDirection("E", true), delegate(Cell c)
						{
							if (c.HasWall())
							{
								c.ClearWalls();
								c.AddObject("WalltrapFireCrematory", null, null, null, null);
								return false;
							}
							return true;
						});
						zone.GetCell(location2D).walk((Cell c) => c.GetCellFromDirection("W", true), delegate(Cell c)
						{
							if (c.HasWall())
							{
								c.ClearWalls();
								c.AddObject("WalltrapFireCrematory", null, null, null, null);
								return false;
							}
							return true;
						});
					}
					if (flag)
					{
						break;
					}
					flag = true;
					location2D = Location2D.Get(location2D.X, 25 - location2D.Y);
				}
			}
			this.PlaceFans(zone, 0, 40);
			List<Cell> nsArmCells = new List<Cell>();
			List<Cell> ewArmCells = new List<Cell>();
			Predicate<Cell> emptyEnough = (Cell cell) => cell != null && !cell.HasObject("CrematoryConveyorPad") && !cell.HasWall();
			list3.ForEach(delegate(Cell c)
			{
				if (c.Y > 2 && c.Y < 23 && c.AllInDirections(new string[] { "N", "S" }, 2, emptyEnough))
				{
					nsArmCells.Add(c);
				}
			});
			list3.ForEach(delegate(Cell c)
			{
				if (c.X > 2 && c.X < 38 && c.AllInDirections(new string[] { "W", "E" }, 2, emptyEnough))
				{
					ewArmCells.Add(c);
				}
			});
			int num4 = 18;
			int num5 = 0;
			while (num5 < num4 && (nsArmCells.Count != 0 || ewArmCells.Count != 0))
			{
				int num6 = Stat.Random(1, 2);
				if (num6 == 1 || ewArmCells.Count == 0)
				{
					using (List<Cell>.Enumerator enumerator = this.getPlacementCells(nsArmCells, 2).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Cell cell3 = enumerator.Current;
							string text = ((Stat.Random(0, 1) == 0) ? "N" : "S");
							Cell cellFromDirection = cell3.GetCellFromDirection(text, true);
							if (!cellFromDirection.HasObject("FactoryArm") && !cellFromDirection.HasObject("GrabberArm"))
							{
								cellFromDirection.AddObject("FactoryArm", null, null, null, null).GetPart<FactoryArm>().Direction = text;
							}
							if (!cellFromDirection.GetCellFromDirection(text, true).HasObject("OpenShaft2"))
							{
								cellFromDirection.GetCellFromDirection(text, true).AddObject("OpenShaft2", null, null, null, null);
							}
						}
						goto IL_0AAC;
					}
					goto IL_09D3;
				}
				goto IL_09D3;
				IL_0AAC:
				num5++;
				continue;
				IL_09D3:
				if (num6 == 2 || nsArmCells.Count == 0)
				{
					foreach (Cell cell4 in this.getPlacementCells(ewArmCells, 2))
					{
						string text2 = ((Stat.Random(0, 1) == 0) ? "E" : "W");
						Cell cellFromDirection2 = cell4.GetCellFromDirection(text2, true);
						if (!cellFromDirection2.HasObject("FactoryArm") && !cellFromDirection2.HasObject("GrabberArm"))
						{
							cellFromDirection2.AddObject("FactoryArm", null, null, null, null).GetPart<FactoryArm>().Direction = text2;
						}
						if (!cellFromDirection2.GetCellFromDirection(text2, true).HasObject("OpenShaft2"))
						{
							cellFromDirection2.GetCellFromDirection(text2, true).AddObject("OpenShaft2", null, null, null, null);
						}
					}
					goto IL_0AAC;
				}
				goto IL_0AAC;
			}
			int num7 = 6;
			int num8 = 0;
			while (num8 < num7 && (nsArmCells.Count != 0 || ewArmCells.Count != 0))
			{
				int num9 = Stat.Random(1, 2);
				if (num9 == 1 || ewArmCells.Count == 0)
				{
					using (List<Cell>.Enumerator enumerator = this.getPlacementCells(nsArmCells, 2).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Cell cell5 = enumerator.Current;
							string text3 = ((Stat.Random(0, 1) == 0) ? "N" : "S");
							Cell cellFromDirection3 = cell5.GetCellFromDirection(text3, true);
							if (!cellFromDirection3.HasObject("FactoryArm") && !cellFromDirection3.HasObject("GrabberArm"))
							{
								cellFromDirection3.AddObject("GrabberArm", null, null, null, null).GetPart<GrabberArm>().Direction = Directions.GetOppositeDirection(text3);
							}
						}
						goto IL_0C41;
					}
					goto IL_0B98;
				}
				goto IL_0B98;
				IL_0C41:
				num8++;
				continue;
				IL_0B98:
				if (num9 == 2 || nsArmCells.Count == 0)
				{
					foreach (Cell cell6 in this.getPlacementCells(ewArmCells, 2))
					{
						string text4 = ((Stat.Random(0, 1) == 0) ? "E" : "W");
						Cell cellFromDirection4 = cell6.GetCellFromDirection(text4, true);
						if (!cellFromDirection4.HasObject("FactoryArm") && !cellFromDirection4.HasObject("GrabberArm"))
						{
							cellFromDirection4.AddObject("GrabberArm", null, null, null, null).GetPart<GrabberArm>().Direction = Directions.GetOppositeDirection(text4);
						}
					}
					goto IL_0C41;
				}
				goto IL_0C41;
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.RandomPointFurtherThan4, 100, null, Options.GetOption("OptionDrawInfluenceMaps", "No") == "Yes", null);
			influenceMap.Regions.ForEach(delegate(InfluenceMapRegion r)
			{
				r.Tags.Add("connected");
			});
			foreach (Cell cell7 in zone.GetCells(box))
			{
				if (cell7.IsEmpty())
				{
					cell7.AddObject("AnchorRoomTile", null, null, null, null);
					cell7.PaintTile = (((cell7.X + cell7.Y) % 2 == 0) ? "Tiles2/sw_floor_diamond_1.bmp" : "Tiles2/sw_floor_diamond_2.bmp");
					cell7.PaintTileColor = "&K";
					cell7.PaintColorString = "&K";
					cell7.PaintRenderString = '\u0004'.ToString();
				}
			}
			if (ZoneTemplateManager.HasTemplates("CryptOfLandlords"))
			{
				ZoneTemplateManager.Templates["CryptOfLandlords"].Execute(zone, influenceMap);
			}
			return true;
		}

		// Token: 0x0600708C RID: 28812 RVA: 0x0028479C File Offset: 0x0028299C
		public List<Cell> getPlacementCells(List<Cell> source, int distance = 2)
		{
			if (source.Count == 0)
			{
				return new List<Cell>();
			}
			List<Cell> list = new List<Cell>();
			list.Add(source.RemoveRandomElement<Cell>(null));
			int num = Stat.Random(0, 1);
			Cell cell = list[0];
			for (int i = 0; i < distance; i++)
			{
				cell = cell.GetCellFromDirection("E", true);
				if (cell == null)
				{
					break;
				}
				if (num == 1)
				{
					cell = cell.GetCellFromDirection("E", true);
				}
				if (cell == null || !source.Contains(cell))
				{
					break;
				}
				source.Remove(cell);
				list.Add(cell);
			}
			cell = list[0];
			for (int j = 0; j < distance; j++)
			{
				cell = cell.GetCellFromDirection("W", true);
				if (cell == null)
				{
					break;
				}
				if (num == 1)
				{
					cell = cell.GetCellFromDirection("W", true);
				}
				if (cell == null || !source.Contains(cell))
				{
					break;
				}
				source.Remove(cell);
				list.Add(cell);
			}
			return list;
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x00284878 File Offset: 0x00282A78
		public bool BuildSmashers(Zone zone)
		{
			zone.FillHollowBox(new Box(0, 0, zone.Width - 1, zone.Height - 1), "MachineWallHotTubing");
			zone.FillBox(new Box(40, 0, 40, zone.Height - 1), "MachineWallHotTubing", true);
			zone.FillBox(new Box(11, 4, 13, 6), "MachineWallHotTubing", false);
			zone.FillBox(new Box(0, 0, 79, 4), "MachineWallHotTubing", false);
			zone.FillBox(new Box(0, 20, 79, 24), "MachineWallHotTubing", false);
			Grid<Color4> grid = new Grid<Color4>(40, 25);
			grid.fromWFCTemplate("longhalls2", 3, int.MaxValue);
			grid = grid.mirrorVertical();
			grid = grid.mirrorHorizontal();
			for (int i = 0; i < zone.Height; i++)
			{
				int j = 0;
				int num = zone.Width / 2;
				while (j < num)
				{
					if (grid.get(j, i) == Color4.black)
					{
						zone.GetCell(j, i).ClearWalls();
						zone.GetCell(j, i).AddObject("MachineWallHotTubing", null, null, null, null);
					}
					j++;
				}
			}
			grid = new Grid<Color4>(40, 25);
			grid.fromWFCTemplate("longhalls2", 3, int.MaxValue);
			grid = grid.mirrorVertical();
			grid = grid.mirrorHorizontal();
			for (int k = 0; k < 40; k++)
			{
				for (int l = 0; l < 25; l++)
				{
					if (grid.get(k, l) == Color4.black)
					{
						zone.GetCell(k + 40, l).ClearWalls();
						zone.GetCell(k + 40, l).AddObject("MachineWallHotTubing", null, null, null, null);
					}
				}
			}
			Box fullRegion = new Box(1, 1, 79, 23);
			Box rightRegion = new Box(40, 1, 79, 23);
			List<Cell> list = (from cell in zone.GetCells()
				where rightRegion.contains(cell.Location) && cell.HasWall()
				select cell).ToList<Cell>();
			for (int m = 0; m < 20; m++)
			{
				Cell cell5 = list.RemoveRandomElement<Cell>(null);
				if (cell5 != null)
				{
					cell5.ClearWalls();
				}
			}
			List<Cell> list2 = (from cell in zone.GetCells()
				where rightRegion.contains(cell.Location) && cell.IsEmpty()
				select cell).ToList<Cell>();
			for (int n = 0; n < 8; n++)
			{
				Cell cell2 = list2.RemoveRandomElement<Cell>(null);
				if (cell2 != null)
				{
					cell2.AddObject("OpenShaft2", null, null, null, null);
				}
			}
			new SpindleFootprint().BuildZone(zone, SpindleFootprint.FootprintMode.Spindle);
			zone.ClearBox(new Box(35, 8, 45, 16), null);
			zone.GetCell(36, 9).AddObject("TombPillarPlacement", null, null, null, null);
			ZoneBuilderSandbox.EnsureAllVoidsConnected(zone, false, 0.2f, null);
			zone.GetCell(79, 7).AddObject("ConveyorDrive", null, null, null, null);
			zone.GetCell(79, 17).AddObject("ConveyorDrive", null, null, null, null);
			base.CreateConveyor(Location2D.Get(79, 8), Location2D.Get(0, 8), zone, delegate(int x, int y)
			{
				if (y <= 13 && y >= 7)
				{
					return 0;
				}
				return 9999;
			}, "W", "CrematoryConveyorPad");
			base.CreateConveyor(Location2D.Get(79, 16), Location2D.Get(0, 16), zone, delegate(int x, int y)
			{
				if (y >= 14 && y <= 20)
				{
					return 0;
				}
				return 9999;
			}, "W", "CrematoryConveyorPad");
			zone.GetCell(0, 8).AddObject("BeltCurtains", null, null, null, null);
			zone.GetCell(0, 16).AddObject("BeltCurtains", null, null, null, null);
			zone.GetCell(0, 8).AddObject("BeltCurtains", null, null, null, null);
			zone.GetCell(0, 16).AddObject("BeltCurtains", null, null, null, null);
			List<Cell> nsArmCells = new List<Cell>();
			List<Cell> ewArmCells = new List<Cell>();
			Predicate<Cell> emptyEnough = (Cell cell) => cell != null && !cell.HasObject("CrematoryConveyorPad") && !cell.HasWall();
			List<Cell> list3 = (from cell in zone.GetCells()
				where fullRegion.contains(cell.Location) && cell.HasObject("CrematoryConveyorPad")
				select cell).ToList<Cell>();
			list3.ForEach(delegate(Cell c)
			{
				if (c.Y > 2 && c.Y < 23 && c.AllInDirections(new string[] { "N", "S" }, 2, emptyEnough))
				{
					nsArmCells.Add(c);
				}
			});
			list3.ForEach(delegate(Cell c)
			{
				if (c.X > 2 && c.X < 38 && c.AllInDirections(new string[] { "W", "E" }, 2, emptyEnough))
				{
					ewArmCells.Add(c);
				}
			});
			int num2 = 18;
			int num3 = 0;
			while (num3 < num2 && (nsArmCells.Count != 0 || ewArmCells.Count != 0))
			{
				int num4 = Stat.Random(1, 2);
				if (num4 == 1 || ewArmCells.Count == 0)
				{
					using (List<Cell>.Enumerator enumerator = this.getPlacementCells(nsArmCells, 2).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Cell cell3 = enumerator.Current;
							cell3.GetCellFromDirection("N", true).RequireObject("PistonPressElement", null, null, null, null);
							cell3.GetCellFromDirection("N", true).GetCellFromDirection("N", true).RequireObject("MachineWallHotTubing", null, null, null, null);
							cell3.GetCellFromDirection("S", true).RequireObject("PistonPressElement", null, null, null, null);
							cell3.GetCellFromDirection("S", true).GetCellFromDirection("S", true).RequireObject("MachineWallHotTubing", null, null, null, null);
							cell3.RequireObject("PistonPressController", null, null, null, null).GetPart<PistonPressController>().Directions = "NS";
						}
						goto IL_0657;
					}
					goto IL_0557;
				}
				goto IL_0557;
				IL_0657:
				num3++;
				continue;
				IL_0557:
				if (num4 == 2 || nsArmCells.Count == 0)
				{
					foreach (Cell cell4 in this.getPlacementCells(ewArmCells, 2))
					{
						cell4.GetCellFromDirection("E", true).RequireObject("PistonPressElement", null, null, null, null);
						cell4.GetCellFromDirection("E", true).GetCellFromDirection("E", true).RequireObject("MachineWallHotTubing", null, null, null, null);
						cell4.GetCellFromDirection("W", true).RequireObject("PistonPressElement", null, null, null, null);
						cell4.GetCellFromDirection("W", true).GetCellFromDirection("W", true).RequireObject("MachineWallHotTubing", null, null, null, null);
						cell4.RequireObject("PistonPressController", null, null, null, null);
						cell4.RequireObject("PistonPressController", null, null, null, null).GetPart<PistonPressController>().Directions = "EW";
					}
					goto IL_0657;
				}
				goto IL_0657;
			}
			this.PlaceFans(zone, 0, 60);
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.RandomPointFurtherThan4, 100, null, Options.GetOption("OptionDrawInfluenceMaps", "No") == "Yes", null);
			influenceMap.Regions.ForEach(delegate(InfluenceMapRegion r)
			{
				r.Tags.Add("connected");
			});
			if (ZoneTemplateManager.HasTemplates("CryptOfLandlords"))
			{
				ZoneTemplateManager.Templates["CryptOfLandlords"].Execute(zone, influenceMap);
			}
			return true;
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x00284F8C File Offset: 0x0028318C
		public bool BuildColumbarium(Zone zone, string room)
		{
			MapBuilder mapBuilder = new MapBuilder();
			if (room == "north")
			{
				mapBuilder.ID = "ColumbariumN.rpm";
			}
			else if (room == "south")
			{
				mapBuilder.ID = "ColumbariumS.rpm";
			}
			else
			{
				mapBuilder.ID = "ColumbariumW.rpm";
			}
			mapBuilder.BuildZone(zone);
			for (int i = 0; i < 5; i++)
			{
				zone.GetEmptyCells(null, null).GetRandomElement<Cell>(null).AddObject("Urn Porter", null, null, null, null);
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.RandomPointFurtherThan4, 100, null, Options.GetOption("OptionDrawInfluenceMaps", "No") == "Yes", null);
			influenceMap.Regions.ForEach(delegate(InfluenceMapRegion r)
			{
				r.Tags.Add("connected");
			});
			if (ZoneTemplateManager.HasTemplates("CryptOfLandlordsUrns"))
			{
				ZoneTemplateManager.Templates["CryptOfLandlordsUrns"].Execute(zone, influenceMap);
			}
			for (int j = 0; j < 80; j++)
			{
				for (int k = 0; k < 25; k++)
				{
					zone.GetCell(j, k).AddObject("AnchorRoomTile", null, null, null, null);
				}
			}
			return true;
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x002850C0 File Offset: 0x002832C0
		public void PlaceFans(Zone zone, int minX, int maxX)
		{
			List<Location2D> list = new List<Location2D>();
			List<Location2D> list2 = new List<Location2D>();
			List<Location2D>[] array = new List<Location2D>[] { list, list2 };
			int[] array2 = new int[] { -1, 1 };
			int[] array3 = new int[2];
			array3[0] = -4;
			int[] array4 = array3;
			int num = 19;
			int num2 = 0;
			for (int i = minX; i <= maxX - 7; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (zone.GetCell(i, j).HasObject("CrematoryConveyorPad"))
					{
						list.Add(Location2D.Get(i, j));
						break;
					}
				}
				for (int k = zone.Height - 1; k >= num2; k--)
				{
					if (zone.GetCell(i, k).HasObject("CrematoryConveyorPad"))
					{
						list2.Add(Location2D.Get(i, k));
						break;
					}
				}
			}
			List<List<Location2D>> list3 = new List<List<Location2D>>
			{
				new List<Location2D>(),
				new List<Location2D>()
			};
			List<List<Location2D>> list4 = new List<List<Location2D>>
			{
				new List<Location2D>(),
				new List<Location2D>()
			};
			for (int l = 0; l < array.Length; l++)
			{
				int num3 = 0;
				foreach (Location2D location2D in array[l])
				{
					if (location2D.X >= num3)
					{
						Cell cell = zone.GetCell(location2D);
						while (cell != null && !cell.HasWall() && !cell.HasObjectWithTag("EnsureVoidBlocker"))
						{
							cell = zone.GetCell(cell.X, cell.Y + array2[l]);
							if (cell == null)
							{
								break;
							}
							if (cell.HasWall())
							{
								goto IL_019F;
							}
							if (cell.AnyInDirection("E", 11, (Cell ce) => ce.HasWall()))
							{
								goto IL_019F;
							}
							IL_01EF:
							if (cell.HasWall())
							{
								goto IL_0226;
							}
							if (cell.AnyInDirection("E", 7, (Cell ce) => ce.HasWall()))
							{
								goto IL_0226;
							}
							IL_0271:
							if (cell.HasWall())
							{
								break;
							}
							if (!cell.AnyInDirection("E", 11, (Cell ce) => ce.HasWall() || ce.HasObject("PistonPressElement") || ce.HasObjectWithTag("EnsureVoidBlocker") || ce.HasObject("CrematoryConveyorPad")))
							{
								continue;
							}
							break;
							IL_0226:
							if (!cell.AnyInDirection("E", 7, (Cell ce) => ce.HasObject("CrematoryConveyorPad") || ce.HasObjectWithTag("EnsureVoidBlocker") || ce.HasObject("PistonPressElement")))
							{
								list3[l].Add(cell.Location);
								num3 += 7;
								break;
							}
							goto IL_0271;
							IL_019F:
							if (!cell.AnyInDirection("E", 11, (Cell ce) => ce.HasObject("CrematoryConveyorPad") || ce.HasObjectWithTag("EnsureVoidBlocker") || ce.HasObject("PistonPressElement")))
							{
								list4[l].Add(cell.Location);
								num3 += 11;
								break;
							}
							goto IL_01EF;
						}
					}
				}
			}
			using (List<Location2D>.Enumerator enumerator = list4[0].ShuffleInPlace<Location2D>(null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Location2D location2D2 = enumerator.Current;
					ZoneBuilderSandbox.PlacePrefab(zone, "preset_tile_chunks/IndustrialFanRow_9_S.rpm", zone.GetCell(location2D2.X, location2D2.Y + array4[0]), 0, null, delegate(Cell c)
					{
						c.ClearWalls();
					}, null, null, null, null, null);
				}
			}
			using (List<Location2D>.Enumerator enumerator = list4[1].ShuffleInPlace<Location2D>(null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Location2D location2D3 = enumerator.Current;
					ZoneBuilderSandbox.PlacePrefab(zone, "preset_tile_chunks/IndustrialFanRow_9_N.rpm", zone.GetCell(location2D3.X, location2D3.Y + array4[1]), 0, null, delegate(Cell c)
					{
						c.ClearWalls();
					}, null, null, null, null, null);
				}
			}
			using (List<Location2D>.Enumerator enumerator = list3[0].ShuffleInPlace<Location2D>(null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Location2D location2D4 = enumerator.Current;
					ZoneBuilderSandbox.PlacePrefab(zone, "preset_tile_chunks/IndustrialFanRow_9_S.rpm", zone.GetCell(location2D4.X, location2D4.Y + array4[0]), 0, null, delegate(Cell c)
					{
						c.ClearWalls();
					}, null, null, null, null, null);
				}
			}
			using (List<Location2D>.Enumerator enumerator = list3[1].ShuffleInPlace<Location2D>(null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Location2D location2D5 = enumerator.Current;
					ZoneBuilderSandbox.PlacePrefab(zone, "preset_tile_chunks/IndustrialFanRow_9_N.rpm", zone.GetCell(location2D5.X, location2D5.Y + array4[1]), 0, null, delegate(Cell c)
					{
						c.ClearWalls();
					}, null, null, null, null, null);
				}
			}
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x00285650 File Offset: 0x00283850
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject("ConcreteFloor", null, null, null, null);
			The.Game.RequireSystem<CryptOfLandlordsAnchorSystem>(() => new CryptOfLandlordsAnchorSystem());
			if (Z.Y == 0 || Z.Y == 2)
			{
				if (Z.X == 2 && !this.BuildFlames(Z))
				{
					return false;
				}
				if (Z.X == 1 && !this.BuildSmashers(Z))
				{
					return false;
				}
				if (Z.X == 0 && Z.Y == 0 && !this.BuildColumbarium(Z, "north"))
				{
					return false;
				}
				if (Z.X == 0 && Z.Y == 2 && !this.BuildColumbarium(Z, "south"))
				{
					return false;
				}
			}
			else if (Z.X == 0 && !this.BuildColumbarium(Z, "west"))
			{
				return false;
			}
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					if ((j <= 2 || j >= 22) && (Z.X != 2 || Z.Y != 1 || i < 67 || i > 77 || j < 22 || j > 24) && (Z.X != 2 || Z.Y <= 0 || i < 67 || i > 76 || j < 0 || j > 2) && (Z.X != 2 || Z.Y >= 2 || i < 67 || i > 76 || j < 22 || j > 24))
					{
						Cell cell = Z.GetCell(i, j);
						if (cell.HasObject("MachineWallHotTubing"))
						{
							while (cell.HasObject("MachineWallHotTubing"))
							{
								cell.FindObject("MachineWallHotTubing").Obliterate(null, false, null);
							}
							cell.AddObject("EbonFulcrete", null, null, null, null);
						}
					}
				}
			}
			foreach (Cell cell2 in Z.GetCellsWithObject("StairsUp"))
			{
				base.EnsureCellReachable(Z, cell2);
			}
			foreach (Cell cell3 in Z.GetCellsWithObject("StairsDown"))
			{
				base.EnsureCellReachable(Z, cell3);
			}
			Z.GetCell(0, 0).AddObject("Finish_TombOfTheEaters_EnterTheTombOfTheEaters", null, null, null, null);
			new ChildrenOfTheTomb().BuildZone(Z);
			return true;
		}

		// Token: 0x04003416 RID: 13334
		public const string wallType = "MachineWallHotTubing";
	}
}
