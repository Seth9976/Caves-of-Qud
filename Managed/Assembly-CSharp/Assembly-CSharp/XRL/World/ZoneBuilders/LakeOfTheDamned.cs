using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Core;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B03 RID: 2819
	public class LakeOfTheDamned : ZoneBuilderSandbox
	{
		// Token: 0x06007099 RID: 28825 RVA: 0x00287D58 File Offset: 0x00285F58
		public int getSeed(string seed)
		{
			return XRLCore.Core.Game.GetWorldSeed(seed);
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x00287D6C File Offset: 0x00285F6C
		public double sampleSimplexNoise(string type, int x, int y, int z, int amplitude, float frequencyMultiplier = 1f)
		{
			this.fastNoise.SetSeed(this.getSeed(type));
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.25f * frequencyMultiplier);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			return Math.Ceiling((double)this.fastNoise.GetNoise((float)x, (float)y, (float)z) * (double)amplitude);
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x00287E00 File Offset: 0x00286000
		public GameObject getLiquid(int x, int y, Zone zone)
		{
			int num = zone.wX * 3 + zone.X;
			int num2 = zone.wY * 3 + zone.Y;
			int offsetX = num * zone.Width;
			int offsetY = num2 * zone.Height;
			List<Tuple<string, double>> list = LakeOfTheDamned.lakeLiquids.Select<string, Tuple<string, double>>((string liquid) => new Tuple<string, double>(liquid, this.sampleSimplexNoise(liquid, offsetX + x, offsetY + y, zone.Z, 10, 0.33f))).ToList<Tuple<string, double>>();
			list.Sort((Tuple<string, double> a, Tuple<string, double> b) => b.Item2.CompareTo(a.Item2));
			GameObject gameObject = GameObject.Create("Water");
			LiquidVolume part = gameObject.GetPart<LiquidVolume>();
			if (list[0].Item2 >= 3.0 && list[1].Item2 < 3.0)
			{
				part.ComponentLiquids.Clear();
				part.SetComponent(list[0].Item1, 1000);
			}
			else
			{
				part.ComponentLiquids.Clear();
				part.SetComponent(list[0].Item1, 500);
				part.SetComponent(list[1].Item1, 500);
			}
			part.Volume = 2000;
			part.Update();
			return gameObject;
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x00287F80 File Offset: 0x00286180
		public List<string> getStairsZones()
		{
			return ZoneBuilderSandbox.GetOracleNFromList<string>(new List<string> { "JoppaWorld.53.3.0.0.12", "JoppaWorld.53.3.1.0.12", "JoppaWorld.53.3.2.0.12", "JoppaWorld.53.3.0.1.12", "JoppaWorld.53.3.2.1.12", "JoppaWorld.53.3.0.2.12", "JoppaWorld.53.3.1.2.12", "JoppaWorld.53.3.2.2.12" }, 2, "LakeOfTheDamnedStairs");
		}

		// Token: 0x0600709D RID: 28829 RVA: 0x00287FF8 File Offset: 0x002861F8
		public static Location2D getStairsLocation(string zone)
		{
			zone = zone.Substring(0, zone.LastIndexOf("."));
			Box box = new Box(39, 12, 40, 13);
			Location2D location2D = Location2D.Get(39, 12);
			while (box.contains(location2D))
			{
				location2D = Location2D.Get(ZoneBuilderSandbox.GetOracleIntFromString(zone + "x", 6, 70), ZoneBuilderSandbox.GetOracleIntFromString(zone + "y", 5, 19));
			}
			return location2D;
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x0028806C File Offset: 0x0028626C
		public static List<string> getCatacombsStairsZones()
		{
			return ZoneBuilderSandbox.GetOracleNFromList<string>(new List<string> { "JoppaWorld.53.3.0.0.11", "JoppaWorld.53.3.1.0.11", "JoppaWorld.53.3.2.0.11", "JoppaWorld.53.3.0.1.11", "JoppaWorld.53.3.2.1.11", "JoppaWorld.53.3.0.2.11", "JoppaWorld.53.3.1.2.11", "JoppaWorld.53.3.2.2.11" }, 2, "LakeOfTheDamnedStairs");
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x002880E4 File Offset: 0x002862E4
		public bool BuildZone(Zone zone)
		{
			if (zone.X == 0 && zone.Y == 0)
			{
				new MapBuilder("SultanFrameNW.rpm").BuildZone(zone);
			}
			if (zone.X == 1 && zone.Y == 0)
			{
				new MapBuilder("SultanFrameN.rpm").BuildZone(zone);
			}
			if (zone.X == 2 && zone.Y == 0)
			{
				new MapBuilder("SultanFrameNE.rpm").BuildZone(zone);
			}
			if (zone.X == 0 && zone.Y == 1)
			{
				new MapBuilder("SultanFrameW.rpm").BuildZone(zone);
			}
			if (zone.X == 2 && zone.Y == 1)
			{
				new MapBuilder("SultanFrameE.rpm").BuildZone(zone);
			}
			if (zone.X == 0 && zone.Y == 2)
			{
				new MapBuilder("SultanFrameSW.rpm").BuildZone(zone);
			}
			if (zone.X == 1 && zone.Y == 2)
			{
				new MapBuilder("SultanFrameS.rpm").BuildZone(zone);
			}
			if (zone.X == 2 && zone.Y == 2)
			{
				new MapBuilder("SultanFrameSE.rpm").BuildZone(zone);
			}
			int num = zone.wX * 3 + zone.X;
			int num2 = zone.wY * 3 + zone.Y;
			int num3 = num * zone.Width;
			int num4 = num2 * zone.Height;
			int num5 = 5;
			for (int i = 0; i < 80; i++)
			{
				for (int j = 0; j < 25; j++)
				{
					if (this.sampleSimplexNoise("islands", num3 + i, num4 + j, zone.Z, num5, 0.33f) <= 1.0)
					{
						Cell cell = zone.GetCell(i, j);
						if (cell.IsEmpty())
						{
							cell.AddObject(this.getLiquid(i, j, zone), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
			InfluenceMap influenceMap = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.LargestRegion, 50, null, false, delegate(Cell c)
			{
				if (c.HasWall() || c.HasObjectWithPart("LiquidVolume"))
				{
					return 1;
				}
				return 0;
			});
			influenceMap.SeedAllUnseeded(false, true);
			ZoneTemplateManager.Templates["LakeOfTheDamnedIslands"].Execute(zone, influenceMap);
			ZoneTemplateManager.Templates["LakeOfTheDamnedIslandPopulation"].Execute(zone, influenceMap);
			InfluenceMap influenceMap2 = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.LargestRegion, 50, null, false, delegate(Cell c)
			{
				if (c.HasObjectWithPart("LiquidVolume"))
				{
					return 0;
				}
				return 1;
			});
			influenceMap2.SeedAllUnseeded(false, true);
			ZoneTemplateManager.Templates["LakeOfTheDamnedWaterPopulation"].Execute(zone, influenceMap2);
			if (this.getStairsZones().Contains(zone.ZoneID))
			{
				zone.GetCell(LakeOfTheDamned.getStairsLocation(zone.ZoneID)).AddObject("StairsUp", null, null, null, null);
			}
			foreach (Cell cell2 in zone.GetCellsWithObject("StairsUp"))
			{
				base.EnsureCellReachable(zone, cell2);
			}
			foreach (Cell cell3 in zone.GetCellsWithObject("StairsDown"))
			{
				base.EnsureCellReachable(zone, cell3);
			}
			zone.GetCell(0, 0).AddObject("PaleDirty", null, null, null, null);
			zone.GetCell(0, 0).AddObject("Finish_TombOfTheEaters_EnterTheTombOfTheEaters", null, null, null, null);
			return true;
		}

		// Token: 0x0400341A RID: 13338
		private FastNoise fastNoise = new FastNoise(1337);

		// Token: 0x0400341B RID: 13339
		public static List<string> lakeLiquids = new List<string> { "putrid", "blood", "wax", "ink" };
	}
}
