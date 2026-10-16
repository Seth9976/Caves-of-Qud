using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Core;
using XRL.Rules;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AE1 RID: 2785
	public class Redrock : ZoneBuilderSandbox
	{
		// Token: 0x06006FF9 RID: 28665 RVA: 0x00278834 File Offset: 0x00276A34
		public bool BuildZone(Zone Z)
		{
			if (Z.Z > 10)
			{
				new StairsUp().BuildZone(Z);
			}
			if (Z.Z < 14)
			{
				new StairsDown().BuildZone(Z);
			}
			List<NoiseMapNode> extraNodes = new List<NoiseMapNode>();
			Z.GetCells().ForEach(delegate(Cell c)
			{
				c.AddObject("Shale", null, null, null, null);
			});
			foreach (ZoneConnection zoneConnection in XRLCore.Core.Game.ZoneManager.GetZoneConnections(Z.ZoneID))
			{
				extraNodes.Add(new NoiseMapNode(zoneConnection.X, zoneConnection.Y));
			}
			Z.GetCells().ForEach(delegate(Cell c)
			{
				if (c.HasObjectWithTag("Stairs"))
				{
					extraNodes.Add(new NoiseMapNode(c.X, c.Y));
				}
			});
			string text = "0-7";
			if (Z.Z == 11)
			{
				text = "1-3";
			}
			if (Z.Z == 12)
			{
				text = "0-5";
			}
			if (Z.Z == 13)
			{
				text = "0-6";
			}
			if (Z.Z == 14)
			{
				text = "0-7";
			}
			int num = int.MaxValue;
			if (Z.Z == 11)
			{
				num = 3;
			}
			NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 2, text, 80, 240, 0, 4, -3, 1, extraNodes, -1, num);
			for (int i = 0; i < 80; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					if (i > 0 && j > 0 && i < 79 && j < 24 && noiseMap.Noise[i, j] >= 1 && !Z.GetCell(i, j).HasObject("Fulcrete"))
					{
						Z.GetCell(i, j).ClearWalls();
					}
				}
			}
			string text2 = "0";
			if (Z.Z == 11)
			{
				text2 = "0";
			}
			if (Z.Z == 12)
			{
				text2 = "1";
			}
			if (Z.Z == 13)
			{
				text2 = "2-3";
			}
			if (Z.Z == 14)
			{
				text2 = "3-4";
			}
			int num2 = Stat.Roll(text2, null);
			List<NoiseMapNode> list = new List<NoiseMapNode>();
			for (int k = 0; k < num2; k++)
			{
				Location2D randomElement = noiseMap.PlacedSeeds.GetRandomElement<Location2D>(null);
				list.Add(new NoiseMapNode(randomElement.X, randomElement.Y));
			}
			NoiseMap noiseMap2 = new NoiseMap(80, 25, 10, 1, 1, "0", 80, 240, 0, 3, -3, 1, list, -1, num2 * 3);
			int l;
			for (l = 0; l < Z.Height; l++)
			{
				for (int m = 0; m < Z.Width; m++)
				{
					if (m > 0 && l > 0 && m < 79 && l < 24 && noiseMap2.Noise[m, l] >= 1)
					{
						Z.GetCell(m, l).AddObject("DeepBrackishPool", null, null, null, null);
					}
				}
			}
			using (List<NoiseMapNode>.Enumerator enumerator2 = list.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					NoiseMapNode featureLocation = enumerator2.Current;
					Cell randomElement2 = (from c in Z.GetCells()
						where c.HasWall() && c.DistanceTo(featureLocation.x, featureLocation.y) < 30
						select c).GetRandomElement<Cell>(null);
					ZoneBuilderSandbox.TunnelTo(Z, Location2D.Get(featureLocation.x, featureLocation.y), Location2D.Get(randomElement2.X, randomElement2.Y), true, 0.2f, 200, delegate(Cell c)
					{
						c.Clear(null, false, false, null);
						c.AddObject("DeepBrackishPool", null, null, null, null);
					}, null);
				}
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(Z, null, InfluenceMapSeedStrategy.FurthestPoint, 400, null, false, null);
			List<GameObject> stairs = Z.FindObjects((GameObject o) => o.HasTag("Stairs"));
			Func<Location2D, bool> <>9__7;
			List<InfluenceMapRegion> list2 = influenceMap.Regions.Where<InfluenceMapRegion>(delegate(InfluenceMapRegion r)
			{
				IEnumerable<Location2D> cells2 = r.Cells;
				Func<Location2D, bool> func;
				if ((func = <>9__7) == null)
				{
					func = (<>9__7 = (Location2D l) => Z.GetCell(l).HasObjectWithTag("Stairs"));
				}
				return !cells2.Any<Location2D>(func);
			}).ToList<InfluenceMapRegion>();
			if (stairs.Count > 0)
			{
				list2.Sort((InfluenceMapRegion a, InfluenceMapRegion b) => stairs.Sum<GameObject>((GameObject s) => b.Center.Distance(s.CurrentCell.Location)) - stairs.Sum<GameObject>((GameObject s) => a.Center.Distance(s.CurrentCell.Location)));
			}
			int num3 = 3;
			if (Z.Z == 11)
			{
				num3 = Stat.Random(0, 1);
			}
			if (Z.Z == 12)
			{
				num3 = Stat.Random(1, 2);
			}
			if (Z.Z == 13)
			{
				num3 = Stat.Random(2, 3);
			}
			if (Z.Z == 14)
			{
				num3 = Stat.Random(3, 4);
			}
			for (int n = 0; n < num3; n++)
			{
				Location2D start = noiseMap.PlacedSeeds.GetRandomElement<Location2D>(null);
				noiseMap.PlacedSeeds.Sort((Location2D a, Location2D b) => b.Distance(start) - a.Distance(start));
				ZoneBuilderSandbox.TunnelTo(Z, start, noiseMap.PlacedSeeds.First<Location2D>(), true, 0.2f, 200, null, null);
			}
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.2f, null);
			foreach (Cell cell in from c in Z.GetCells()
				where !c.HasWall()
				select c)
			{
				cell.SetReachable(true);
			}
			if (Z.Z == 12)
			{
				int num4 = 1;
				if (75.in100())
				{
					num4++;
				}
				if (num4 == 2 && 30.in100())
				{
					num4++;
				}
				int num5 = 0;
				Action<Location2D> <>9__12;
				while (num5 < list2.Count && num5 < num4)
				{
					List<Location2D> cells = list2[num5].Cells;
					Action<Location2D> action;
					if ((action = <>9__12) == null)
					{
						action = (<>9__12 = delegate(Location2D c)
						{
							Z.GetCell(c).AddObject("InfluenceMapBlocker", null, null, null, null);
						});
					}
					cells.ForEach(action);
					ZoneBuilderSandbox.PlacePopulationInRegion(Z, list2[num5], "RedrockLevel2Encounters", null, null);
					num5++;
				}
			}
			if (Z.Z == 13)
			{
				string blueprint = PopulationManager.RollOneFrom("RedrockSnapjawFortStyle", null, null).Blueprint;
				if (blueprint == "Stockade")
				{
					new RedrockStockadeMaker().BuildZone(Z, true, "BrinestalkStakes", "SnapjawParty1", null, "SnapjawStockadeRoom with Snapjaws", "SnapjawStockadeRoom with Bear", "SnapjawStockadeRoom Small with Snapjaws", "SnapjawStockadeRoom Small with Bear", "16-35", "12-20", true, "SnapjawStockadeOuterArea");
				}
				if (blueprint == "City")
				{
					InfluenceMap influenceMap2 = ZoneBuilderSandbox.GenerateInfluenceMap(Z, null, InfluenceMapSeedStrategy.FurthestPoint, 100, null, false, null);
					CaveCity caveCity = new CaveCity();
					Zone z = Z;
					InfluenceMap influenceMap3 = influenceMap2;
					string text3 = "Brinestalk Gate";
					string text4 = "BrinestalkStakes";
					int num6 = 2;
					int num7 = 9;
					int num8 = 3;
					int num9 = 3;
					string text5 = "3-5";
					int num10 = 100;
					string text6 = "RedrockSnapjawCaveRoomDecoration";
					string text7 = "RedrockSnapjawCaveRoomCreatures";
					string text8 = "SnapjawBoss";
					GameObject gameObject = Z.FindObject("StairsUp");
					Location2D location2D;
					if (gameObject == null)
					{
						location2D = null;
					}
					else
					{
						Cell currentCell = gameObject.CurrentCell;
						location2D = ((currentCell != null) ? currentCell.Location : null);
					}
					caveCity.BuildZone(z, influenceMap3, text3, text4, num6, num7, num8, num9, text5, num10, text6, text7, text8, location2D);
				}
				if (blueprint == "Fort")
				{
					new SnapjawFortMaker().BuildZone(Z);
				}
			}
			if (Z.Z == 14)
			{
				new RiverStartMouth().BuildZone(Z);
				new RiverSouthMouth().BuildZone(Z);
				new RiverBuilder().BuildZone(Z);
			}
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.2f, null);
			return true;
		}
	}
}
