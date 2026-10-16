using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE2 RID: 2786
	public class Rustwells : ZoneBuilderSandbox
	{
		// Token: 0x06006FFB RID: 28667 RVA: 0x00279068 File Offset: 0x00277268
		public bool BuildZone(Zone Z)
		{
			if (Z.Z == 10)
			{
				Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			Z.ForeachObjectWithTagOrProperty("Stairs", delegate(GameObject O)
			{
				string text = "Pre-existing stair in ";
				Cell currentCell = O.CurrentCell;
				MetricsManager.LogError(text + ((currentCell != null) ? currentCell.ToString() : null));
			});
			Pitted pitted = new Pitted();
			pitted.MaxWells = 1;
			pitted.MinWells = 1;
			pitted.MinRadius = 8;
			pitted.MaxRadius = 10;
			pitted.XMargin = 20;
			List<Location2D> list = new List<Location2D>();
			List<Location2D> list2 = new List<Location2D>();
			pitted.BuildZone(Z, list, list2);
			Rect2D pitBounds = new Rect2D(list).ExpandBy(1, true);
			int num = 10;
			if (25.in100())
			{
				num += Stat.Random(1, 6);
			}
			int depth = ZoneBuilderSandbox.GetOracleIntColumn(Z, 1, 4);
			float anglechunks = (float)ZoneBuilderSandbox.GetOracleIntColumn(Z, 1, num);
			int divs = ZoneBuilderSandbox.GetOracleIntColumn(Z, 2, 4);
			Z.ForeachCell(delegate(Cell C)
			{
				int num3 = C.CosmeticDistanceTo(pitBounds.Center);
				int num4 = ((int)(Math.Atan2((double)(C.Pos2D.x - pitBounds.Center.x), (double)(C.Pos2D.y - pitBounds.Center.y)) / (3.141592653589793 / (double)anglechunks)) + num3 / (divs * depth)) % divs;
				if (!pitBounds.Contains(C.Pos2D) && num3 / depth % 2 == 0 && num4 == 0)
				{
					C.AddObject("Fulcrete", null, null, null, null);
				}
			});
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("Dirty"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			List<NoiseMapNode> list3 = new List<NoiseMapNode>();
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				list3.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			if (Z.Z > 10)
			{
				NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
				for (int i = 0; i < 80; i++)
				{
					for (int j = 0; j < 25; j++)
					{
						if (i > 0 && j > 0 && i < 79 && j < 24 && noiseMap.Noise[i, j] > 2 && !Z.GetCell(i, j).HasObject("Fulcrete"))
						{
							Z.GetCell(i, j).ClearWalls();
						}
					}
				}
			}
			Z.GetCell(10 + (Z.Z - 1) / 3, Z.Height - 1).AddObject("Fulcrete", null, null, null, null);
			if (Z.Z > 10)
			{
				new StairsUp
				{
					Reachable = false
				}.BuildZone(Z);
			}
			if (list.Count > 9)
			{
				new StairsDown
				{
					Reachable = false,
					EmptyOnly = false,
					x = "1-78",
					y = "1-23"
				}.BuildZone(Z);
			}
			Z.FireEvent("FirmPitEdges");
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.2f, null);
			if (Z.Z == 10)
			{
				int k = 0;
				int num2 = Stat.Random(0, 4);
				while (k < num2)
				{
					using (Pathfinder pathfinder = Z.getPathfinder(null))
					{
						Cell randomElement = (from c in Z.GetEmptyCells(null, null)
							where c.X == 0 || c.Y == 0 || c.X == 79 || c.Y == 24
							select c).GetRandomElement<Cell>(null);
						if (randomElement != null && pathfinder.FindPath(Z.GetCell(randomElement.X, randomElement.Y).Location, Z.GetCell(40, 15).Location, false, true, 24300, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								Z.GetCell(pathfinderNode.X, pathfinderNode.Y).AddObject("SaltyWaterDeepPool", null, null, null, null);
							}
						}
					}
					k++;
				}
			}
			Z.ForeachCell(new Action<Cell>(this.SanityCheck));
			if (Z.Z == 13)
			{
				ZoneBuilderSandbox.PlacePopulationInRegion(Z, list2, "RustwellsBottomPool", null, null);
			}
			return true;
		}

		// Token: 0x06006FFC RID: 28668 RVA: 0x002794D0 File Offset: 0x002776D0
		public void SanityCheck(Cell C)
		{
			if (!C.HasWall())
			{
				C.SetReachable(true);
				return;
			}
			if (C.HasStairs())
			{
				MetricsManager.LogError("Stairs generated blocked by wall, clearing.");
				C.ClearWalls();
				C.SetReachable(true);
			}
		}
	}
}
