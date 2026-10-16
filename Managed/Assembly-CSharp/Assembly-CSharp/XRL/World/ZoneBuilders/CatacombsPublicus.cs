using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using UnityEngine;
using XRL.EditorFormats.Map;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AFD RID: 2813
	public class CatacombsPublicus : ZoneBuilderSandbox
	{
		// Token: 0x06007081 RID: 28801 RVA: 0x00282AB0 File Offset: 0x00280CB0
		public bool BuildZone(Zone Z)
		{
			CatacombsPublicus.<>c__DisplayClass0_0 CS$<>8__locals1 = new CatacombsPublicus.<>c__DisplayClass0_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.Z = Z;
			The.Game.RequireSystem<CatacombsAnchorSystem>(() => new CatacombsAnchorSystem());
			CatacombsMapTemplate catacombsMapTemplate = The.Game.RequireTransientGameState<CatacombsMapTemplate>("CatacombsMapTemplate", () => new CatacombsMapTemplate(CS$<>8__locals1.<>4__this.GetSeedValue("CatacombsMapTemplate")));
			CS$<>8__locals1.Z.GetCell(0, 0).AddObject("ConcreteFloor", null, null, null, null);
			CS$<>8__locals1.xo = CS$<>8__locals1.Z.X * 80;
			CS$<>8__locals1.yo = CS$<>8__locals1.Z.Y * 25;
			int x;
			int i;
			for (x = 0; x < CS$<>8__locals1.Z.Width; x = i + 1)
			{
				int y;
				for (y = 0; y < CS$<>8__locals1.Z.Height; y = i + 1)
				{
					Color4 color = catacombsMapTemplate.grid.get(x + CS$<>8__locals1.xo, y + CS$<>8__locals1.yo);
					if (catacombsMapTemplate.maskingAreas.Any<Rect2D>((Rect2D r) => r.Contains(x + CS$<>8__locals1.xo, y + CS$<>8__locals1.yo)))
					{
						CS$<>8__locals1.Z.GetCell(x, y).AddObject("EbonFulcrete", null, null, null, null);
					}
					else if (color == Color4.black)
					{
						CS$<>8__locals1.Z.GetCell(x, y).AddObject("CatacombWall", null, null, null, null);
					}
					i = y;
				}
				i = x;
			}
			new SpindleFootprint().BuildZone(CS$<>8__locals1.Z, SpindleFootprint.FootprintMode.Foundation);
			CS$<>8__locals1.Z.ClearBox(new Box(35, 8, 45, 16), null);
			CS$<>8__locals1.Z.GetCell(36, 9).AddObject("TombPillarPlacement", null, null, null, null);
			CS$<>8__locals1.zoneBounds = new Rect2D(CS$<>8__locals1.xo, CS$<>8__locals1.yo, CS$<>8__locals1.xo + 79, CS$<>8__locals1.yo + 24);
			IEnumerable<InfluenceMapRegion> enumerable = catacombsMapTemplate.regions.Regions.Where<InfluenceMapRegion>((InfluenceMapRegion r) => r.BoundingBox.overlaps(CS$<>8__locals1.zoneBounds));
			CS$<>8__locals1.lampSpots = new List<Location2D>();
			Predicate<Cell> predicate = delegate(Cell c)
			{
				if (c.HasObjectWithPart("LightSource"))
				{
					return false;
				}
				if (c.HasObjectWithTag("EnsureVoidBlocker"))
				{
					return false;
				}
				if (c.HasWall() && !c.IsEdge())
				{
					c.ClearWalls();
					c.AddObject("CatacombLight", null, null, null, null);
					CS$<>8__locals1.lampSpots.Add(c.Location);
					return false;
				}
				return true;
			};
			foreach (InfluenceMapRegion influenceMapRegion in enumerable)
			{
				IEnumerable<Location2D> cells = influenceMapRegion.Cells;
				Func<Location2D, bool> func;
				if ((func = CS$<>8__locals1.<>9__10) == null)
				{
					func = (CS$<>8__locals1.<>9__10 = (Location2D c) => c.X % 3 == 0 && c.Y % 3 == 0 && CS$<>8__locals1.zoneBounds.Contains(c.X, c.Y));
				}
				Location2D location2D = cells.Where<Location2D>(func).GetRandomElement<Location2D>(null);
				if (location2D != null)
				{
					location2D = Location2D.Get(location2D.X - CS$<>8__locals1.xo, location2D.Y - CS$<>8__locals1.yo);
				}
				if (location2D != null && CS$<>8__locals1.Z.GetCell(location2D) != null && !CS$<>8__locals1.Z.GetCell(location2D).HasObjectWithTag("EnsureVoidBlocker"))
				{
					string[] cardinalDirectionList = Directions.CardinalDirectionList;
					for (i = 0; i < cardinalDirectionList.Length; i++)
					{
						string direction = cardinalDirectionList[i];
						CS$<>8__locals1.Z.GetCell(location2D).walk((Cell c) => c.GetCellFromDirection(direction, true), predicate);
					}
				}
			}
			Pathfinder pathfinder = CS$<>8__locals1.Z.getPathfinder(null);
			if (CS$<>8__locals1.Z.X == 0 && CS$<>8__locals1.Z.Y == 2)
			{
				for (int j = 44; j <= 45; j++)
				{
					foreach (Cell cell in CS$<>8__locals1.Z.GetEmptyCellsShuffled())
					{
						if (cell.Y < 20)
						{
							Location2D location = cell.Location;
							Location2D location2 = CS$<>8__locals1.Z.GetCell(j, 24).Location;
							if (pathfinder.FindPath(location2, location, false, false, 9999, false))
							{
								using (List<PathfinderNode>.Enumerator enumerator2 = pathfinder.Steps.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										PathfinderNode pathfinderNode = enumerator2.Current;
										CS$<>8__locals1.Z.GetCell(pathfinderNode.pos).ClearWalls();
									}
									break;
								}
							}
						}
					}
				}
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(CS$<>8__locals1.Z, new List<Point>(), InfluenceMapSeedStrategy.RandomPointFurtherThan4, 100, CS$<>8__locals1.lampSpots, Options.GetOption("OptionDrawInfluenceMaps", "No") == "Yes", null);
			influenceMap.Regions.ForEach(delegate(InfluenceMapRegion r)
			{
				r.Tags.Add("connected");
			});
			List<InfluenceMapRegion> list = influenceMap.Regions.Where<InfluenceMapRegion>((InfluenceMapRegion r) => r.Cells.Count > 0 && r != null).ToList<InfluenceMapRegion>();
			list.Sort(delegate(InfluenceMapRegion a, InfluenceMapRegion b)
			{
				if (a.Cells.Count >= 16 && b.Cells.Count < 16)
				{
					return -1;
				}
				if (b.Cells.Count >= 16 && a.Cells.Count < 16)
				{
					return 1;
				}
				if (a.AdjacentRegions.Count == b.AdjacentRegions.Count)
				{
					return 0;
				}
				if (a.AdjacentRegions.Count == 0)
				{
					return 1;
				}
				if (b.AdjacentRegions.Count == 0)
				{
					return -1;
				}
				if (a.AdjacentRegions.Count == 1)
				{
					return -1;
				}
				if (b.AdjacentRegions.Count == 1)
				{
					return 1;
				}
				return 0;
			});
			List<InfluenceMapRegion> list2 = new List<InfluenceMapRegion>();
			int k = 0;
			int num = Stat.Random(1, 2);
			while (k < num)
			{
				list2.Add(list[0]);
				list.RemoveAt(0);
				k++;
			}
			list2.RemoveAll((InfluenceMapRegion a) => a == null);
			ZoneTemplateManager.Templates["CatacombsPublicus"].Execute(CS$<>8__locals1.Z, influenceMap);
			int num2 = 20;
			foreach (InfluenceMapRegion influenceMapRegion2 in list2)
			{
				List<Location2D> cells2 = influenceMapRegion2.Cells;
				Action<Location2D> action;
				if ((action = CS$<>8__locals1.<>9__12) == null)
				{
					action = (CS$<>8__locals1.<>9__12 = delegate(Location2D loc)
					{
						CS$<>8__locals1.Z.GetCell(loc).AddObject("AnchorRoomTile", null, null, null, null);
						CS$<>8__locals1.Z.GetCell(loc).PaintTile = (((loc.X + loc.Y) % 2 == 0) ? "Tiles2/sw_floor_diamond_1.bmp" : "Tiles2/sw_floor_diamond_2.bmp");
						CS$<>8__locals1.Z.GetCell(loc).PaintTileColor = "&K";
						CS$<>8__locals1.Z.GetCell(loc).PaintColorString = "&K";
						CS$<>8__locals1.Z.GetCell(loc).PaintRenderString = '\u0004'.ToString();
					});
				}
				cells2.ForEach(action);
				if (Stat.Random(1, 100) <= num2)
				{
					List<Location2D> cells3 = influenceMapRegion2.Cells;
					Action<Location2D> action2;
					if ((action2 = CS$<>8__locals1.<>9__13) == null)
					{
						action2 = (CS$<>8__locals1.<>9__13 = delegate(Location2D loc)
						{
							CS$<>8__locals1.Z.GetCell(loc).AddObject("MopangoHideoutTile", null, null, null, null);
						});
					}
					cells3.ForEach(action2);
					ZoneBuilderSandbox.PlacePopulationInRegion(CS$<>8__locals1.Z, influenceMapRegion2, "MopangoHideout", null, null);
				}
				else if (PopulationManager.HasPopulation("CatacombsAnchorRoom"))
				{
					ZoneBuilderSandbox.PlacePopulationInRegion(CS$<>8__locals1.Z, influenceMapRegion2, "CatacombsAnchorRoom", null, null);
				}
			}
			if (list2.Count == 0)
			{
				Debug.LogError("no anchor room regions!");
			}
			if (CS$<>8__locals1.Z.X == 0 && CS$<>8__locals1.Z.Y == 0)
			{
				MapFile mapFile = MapFile.Resolve("MopangoSettlement", true);
				for (int l = 0; l < 13; l++)
				{
					for (int m = 0; m < 35; m++)
					{
						Cell cell2 = CS$<>8__locals1.Z.GetCell(m, l);
						cell2.Clear(null, false, false, null);
						foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFile.Cells[m, l].Objects)
						{
							cell2.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
						}
						if (!cell2.HasWall())
						{
							cell2.AddObject("AnchorRoomTile", null, null, null, null);
							cell2.AddObject("MopangoHideoutTile", null, null, null, null);
							cell2.PaintTile = (((cell2.X + cell2.Y) % 2 == 0) ? "Tiles2/sw_floor_diamond_1.bmp" : "Tiles2/sw_floor_diamond_2.bmp");
							cell2.PaintTileColor = "&K";
							cell2.PaintColorString = "&K";
							cell2.PaintRenderString = "\u0004";
						}
					}
				}
				ZoneBuilderSandbox.EnsureAllVoidsConnected(CS$<>8__locals1.Z, false, 0.2f, null);
			}
			if (CS$<>8__locals1.Z.ZoneID == "JoppaWorld.53.3.2.2.11")
			{
				Cell cell3 = CS$<>8__locals1.Z.GetCell(41, 12);
				cell3.ClearWalls();
				base.EnsureCellReachable(CS$<>8__locals1.Z, cell3);
				cell3.AddObject("StairsUp", null, null, null, null);
			}
			else if (CS$<>8__locals1.Z.ZoneID == "JoppaWorld.53.3.2.0.11")
			{
				Cell cell4 = CS$<>8__locals1.Z.GetCell(41, 13);
				cell4.ClearWalls();
				base.EnsureCellReachable(CS$<>8__locals1.Z, cell4);
				cell4.AddObject("StairsUp", null, null, null, null);
			}
			else
			{
				CS$<>8__locals1.Z.GetEmptyCells(null, null).GetRandomElement<Cell>(null).AddObject("Catacombs Exit Teleporter", null, null, null, null);
			}
			if (catacombsMapTemplate.properties.Contains("k-Goninon:" + CS$<>8__locals1.Z.ZoneID))
			{
				(from c in CS$<>8__locals1.Z.GetEmptyCells(null, null)
					where !c.HasObject("MopangoHideoutTile")
					select c).GetRandomElement<Cell>(null).AddObject("k-Goninon", null, null, null, null);
			}
			pathfinder.Dispose();
			foreach (Cell cell5 in CS$<>8__locals1.Z.GetCellsWithObject("StairsUp"))
			{
				base.EnsureCellReachable(CS$<>8__locals1.Z, cell5);
			}
			foreach (Cell cell6 in CS$<>8__locals1.Z.GetCellsWithObject("StairsDown"))
			{
				base.EnsureCellReachable(CS$<>8__locals1.Z, cell6);
			}
			CS$<>8__locals1.Z.GetCell(0, 0).AddObject("Finish_TombOfTheEaters_EnterTheTombOfTheEaters", null, null, null, null);
			new ChildrenOfTheTomb().BuildZone(CS$<>8__locals1.Z);
			return true;
		}
	}
}
