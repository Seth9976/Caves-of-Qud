using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using UnityEngine;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD2 RID: 2770
	public class SixDayTents : ZoneBuilderSandbox
	{
		// Token: 0x06006FD9 RID: 28633 RVA: 0x00276FC8 File Offset: 0x002751C8
		public bool BuildZone(Zone Z)
		{
			Z.ClearReachableMap(true);
			if (!XRLCore.Core.Game.HasIntGameState("SixDaySeed"))
			{
				XRLCore.Core.Game.SetIntGameState("SixDaySeed", Stat.Random(0, 2147483646));
			}
			List<Location2D> list = new List<Location2D>();
			if (Z.Y == 0)
			{
				if (Z.X == 0)
				{
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(79, 12));
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(38, 24));
				}
				if (Z.X == 1)
				{
					list.Add(Location2D.Get(0, 12));
					list.Add(Location2D.Get(79, 12));
				}
				if (Z.X == 2)
				{
					list.Add(Location2D.Get(0, 12));
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(38, 24));
				}
			}
			if (Z.Y == 1)
			{
				if (Z.X == 0)
				{
					list.Add(Location2D.Get(38, 0));
					list.Add(Location2D.Get(38, 24));
				}
				int x = Z.X;
				if (Z.X == 2)
				{
					list.Add(Location2D.Get(38, 0));
					list.Add(Location2D.Get(38, 24));
				}
			}
			if (Z.Y == 2)
			{
				if (Z.X == 0)
				{
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(79, 12));
					list.Add(Location2D.Get(38, 0));
					list.Add(Location2D.Get(38, 12));
				}
				if (Z.X == 1)
				{
					new MapBuilder
					{
						ID = "StiltSouth.rpm"
					}.BuildZone(Z);
					list.Add(Location2D.Get(22, 0));
					list.Add(Location2D.Get(38, 24));
					list.Add(Location2D.Get(23, 0));
					list.Add(Location2D.Get(38, 24));
					list.Add(Location2D.Get(38, 0));
					list.Add(Location2D.Get(38, 24));
					list.Add(Location2D.Get(40, 0));
					list.Add(Location2D.Get(40, 24));
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(0, 12));
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(79, 12));
					ZoneManager.instance.SetZoneProperty(Z.ZoneID, "PullDownLocation", "38,20");
				}
				if (Z.X == 2)
				{
					list.Add(Location2D.Get(0, 12));
					list.Add(Location2D.Get(38, 12));
					list.Add(Location2D.Get(38, 0));
					list.Add(Location2D.Get(38, 12));
				}
			}
			List<Location2D> list2 = new List<Location2D>();
			if (list.Count > 0)
			{
				list2 = base.BuildSimplePathWithObject(Z, list, "SaltPath", 4, true);
			}
			list2.AddRange(from c in Z.GetCellsWithObject("NoStiltSpawn")
				select c.Location);
			int num = 300;
			InfluenceMap influenceMap;
			string text;
			int num2;
			int num3;
			int num4;
			for (;;)
			{
				influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(Z, null, InfluenceMapSeedStrategy.RandomPointFurtherThan4, num, list2, false, null);
				text = ".";
				if (Z.X == 0 && Z.Y == 0)
				{
					text = "NW";
				}
				if (Z.X == 1 && Z.Y == 0)
				{
					text = "N";
				}
				if (Z.X == 2 && Z.Y == 0)
				{
					text = "NE";
				}
				if (Z.X == 0 && Z.Y == 1)
				{
					text = "W";
				}
				if (Z.X == 1 && Z.Y == 1)
				{
					text = ".";
				}
				if (Z.X == 2 && Z.Y == 1)
				{
					text = "E";
				}
				if (Z.X == 0 && Z.Y == 2)
				{
					text = "SW";
				}
				if (Z.X == 1 && Z.Y == 2)
				{
					text = "S";
				}
				if (Z.X == 2 && Z.Y == 2)
				{
					text = "SE";
				}
				num2 = "3-4".RollCached();
				num3 = 0;
				num4 = 0;
				if (text == "SE")
				{
					num4 = 4;
				}
				if (num2 < num4)
				{
					num2 = num4;
				}
				int num5 = 0;
				foreach (InfluenceMapRegion influenceMapRegion in influenceMap.Regions)
				{
					Rect2D rect2D = GridTools.MaxRectByArea(influenceMapRegion.GetGrid(), false, 0, 0).ReduceBy(1, 1).Translate(influenceMapRegion.BoundingBox.UpperLeft);
					if (rect2D.Width >= 6 && rect2D.Height >= 6)
					{
						num5++;
					}
				}
				if (num5 >= num4 || num >= 2000)
				{
					break;
				}
				num += 100;
			}
			foreach (InfluenceMapRegion influenceMapRegion2 in influenceMap.Regions)
			{
				Rect2D rect2D2 = GridTools.MaxRectByArea(influenceMapRegion2.GetGrid(), false, 0, 0).ReduceBy(1, 1).Translate(influenceMapRegion2.BoundingBox.UpperLeft);
				if (rect2D2.Width >= 6 && rect2D2.Height >= 6 && Stat.Random(1, 100) <= 15 && num3 >= num4)
				{
					Location2D location = rect2D2.GetRandomDoorCell("?", 2).location;
					ZoneBuilderSandbox.PlaceObjectOnRect(Z, "BrinestalkFence", rect2D2, false);
					base.GetCell(Z, location).Clear(null, false, false, null);
					base.GetCell(Z, location).AddObject("Brinestalk Gate", null, null, null, null);
					string cellSide = rect2D2.GetCellSide(location.Point);
					Rect2D rect2D3 = rect2D2.ReduceBy(0, 0);
					int num6 = 0;
					if (cellSide == "N")
					{
						num6 = ((Stat.Random(0, 1) == 0) ? 2 : 3);
					}
					if (cellSide == "S")
					{
						num6 = ((Stat.Random(0, 1) == 0) ? 0 : 1);
					}
					if (cellSide == "E")
					{
						num6 = ((Stat.Random(0, 1) == 0) ? 0 : 3);
					}
					if (cellSide == "W")
					{
						num6 = ((Stat.Random(0, 1) == 0) ? 1 : 2);
					}
					if (num6 == 0 || num6 == 1)
					{
						rect2D3.y2 = rect2D3.y1 + 3;
					}
					else
					{
						rect2D3.y1 = rect2D3.y2 - 3;
					}
					if (num6 == 0 || num6 == 3)
					{
						rect2D3.x2 = rect2D3.x1 + 3;
					}
					else
					{
						rect2D3.x1 = rect2D3.x2 - 3;
					}
					base.ClearRect(Z, rect2D3);
					ZoneBuilderSandbox.PlaceObjectOnRect(Z, "BrinestalkWall", rect2D3, false);
					Location2D location2 = rect2D3.GetRandomDoorCell(cellSide, 1).location;
					Z.GetCell(location2).Clear(null, false, false, null);
					Z.GetCell(location2).AddObject("Door", null, null, null, null);
					if (PopulationManager.HasPopulation("StiltAnimalPen"))
					{
						ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D2.ReduceBy(1, 1), "StiltAnimalPen");
					}
					if (PopulationManager.HasPopulation("StiltAnimalPenHut"))
					{
						ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D3.ReduceBy(1, 1), "StiltAnimalPenHut");
					}
				}
				else if (rect2D2.Width >= 6 && rect2D2.Height >= 6 && num2 > 0)
				{
					Location2D location3 = rect2D2.Center.location;
					Debug.Log(location3);
					Debug.Log(location3.RegionDirection(80, 25));
					Debug.Log(Calc.GetOppositeDirection(location3.RegionDirection(80, 25)[0]));
					Location2D location4 = rect2D2.GetRandomDoorCell((location3.RegionDirection(80, 25) == ".") ? Calc.GetOppositeDirection(text[0]) : Calc.GetOppositeDirection(location3.RegionDirection(80, 25)[0]), 2).location;
					if (rect2D2.DoorDirection == "N" || rect2D2.DoorDirection == "S")
					{
						rect2D2.Pinch = rect2D2.Height / 2;
					}
					if (rect2D2.DoorDirection == "E" || rect2D2.DoorDirection == "W")
					{
						rect2D2.Pinch = rect2D2.Width / 2;
					}
					ZoneBuilderSandbox.PlaceObjectOnRect(Z, "CanvasWall", rect2D2, false);
					base.GetCell(Z, location4).Clear(null, false, false, null);
					if (PopulationManager.HasPopulation("StiltTents_" + text + "#" + num3.ToString()))
					{
						ZoneBuilderSandbox.PlacePopulationInRegionRect(Z, influenceMapRegion2, rect2D2.ReduceBy(1, 1), rect2D2.DoorDirection, "StiltTents_" + text + "#" + num3.ToString(), "Merchants", null, null);
					}
					else if (PopulationManager.HasPopulation("StiltTents_" + text))
					{
						ZoneBuilderSandbox.PlacePopulationInRegionRect(Z, influenceMapRegion2, rect2D2.ReduceBy(1, 1), rect2D2.DoorDirection, "StiltTents_" + text, "Merchants", null, null);
					}
					else
					{
						ZoneBuilderSandbox.PlacePopulationInRegionRect(Z, influenceMapRegion2, rect2D2.ReduceBy(1, 1), rect2D2.DoorDirection, "StiltTents", "Merchants", null, null);
					}
					num2--;
					num3++;
				}
				else if (PopulationManager.HasPopulation("StiltFields_" + text))
				{
					ZoneBuilderSandbox.PlacePopulationInRegion(Z, influenceMapRegion2, "StiltFields_" + text, null, null);
				}
				else
				{
					ZoneBuilderSandbox.PlacePopulationInRegion(Z, influenceMapRegion2, "StiltFields", null, null);
				}
			}
			foreach (GameObject gameObject in Z.GetObjects("CanvasWall"))
			{
				gameObject.SetIntProperty("NoClear", 1, false);
			}
			if (PopulationManager.HasPopulation("StiltRoads_" + text))
			{
				ZoneBuilderSandbox.PlacePopulationInCells(Z, list2, "StiltRoads_" + text);
			}
			else if (PopulationManager.HasPopulation("StiltRoads"))
			{
				ZoneBuilderSandbox.PlacePopulationInCells(Z, list2, "StiltRoads");
			}
			Z.BuildReachabilityFromEdges();
			return true;
		}

		// Token: 0x04003372 RID: 13170
		private const int MaxWidth = 1200;

		// Token: 0x04003373 RID: 13171
		private const int MaxHeight = 375;

		// Token: 0x04003374 RID: 13172
		public bool Noise;

		// Token: 0x04003375 RID: 13173
		[NonSerialized]
		public static Maze3D TunnelMaze;
	}
}
