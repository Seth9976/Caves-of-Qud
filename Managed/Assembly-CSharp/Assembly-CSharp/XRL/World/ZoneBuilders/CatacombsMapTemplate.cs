using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AFC RID: 2812
	[Serializable]
	public class CatacombsMapTemplate
	{
		// Token: 0x0600707F RID: 28799 RVA: 0x00282581 File Offset: 0x00280781
		public CatacombsMapTemplate(int seed)
		{
			this.Generate(seed);
		}

		// Token: 0x06007080 RID: 28800 RVA: 0x002825A8 File Offset: 0x002807A8
		public void Generate(int seed)
		{
			int num = 5;
			Random random = new Random(seed);
			Grid<Color4> sourcegrid = new Grid<Color4>(160, 50);
			sourcegrid.fromWFCTemplate("crypt", 3, seed);
			this.grid = new Grid<Color4>(240, 75);
			this.grid.transform((int x, int y, Color4 c) => sourcegrid.get((int)((float)x / 1.5f), (int)((float)y / 1.5f)));
			this.stairZones.Add("JoppaWorld.53.3.2.0.11");
			this.stairZones.Add("JoppaWorld.53.3.2.2.11");
			Func<string, bool> <>9__3;
			for (int i = 0; i < num; i++)
			{
				List<string> list = this.stairZones;
				IEnumerable<string> catacombsAllowedStairsZones = CatacombsAnchorSystem.catacombsAllowedStairsZones;
				Func<string, bool> func;
				if ((func = <>9__3) == null)
				{
					func = (<>9__3 = (string z) => !this.stairZones.Contains(z) && CatacombsAnchorSystem.catacombsAllowedStairsZones.Contains(z));
				}
				list.Add(catacombsAllowedStairsZones.Where<string>(func).GetRandomElement<string>(random));
			}
			this.properties.Add("k-Goninon:" + CatacombsAnchorSystem.catacombsAllowedStairsZones.GetRandomElement<string>(random));
			this.maskingAreas = new List<Rect2D>();
			this.maskingAreas.Add(new Rect2D(80, 25, 159, 49));
			this.maskingAreas.Add(new Rect2D(0, 0, 239, 0));
			this.maskingAreas.Add(new Rect2D(0, 74, 239, 74));
			this.maskingAreas.Add(new Rect2D(0, 0, 0, 74));
			this.maskingAreas.Add(new Rect2D(239, 0, 239, 74));
			foreach (Rect2D rect2D in this.maskingAreas)
			{
				this.grid.box(rect2D, () => Color4.black);
			}
			int num2 = 160;
			int num3 = 0;
			this.grid.set(73 + num2, 7 + num3, Color4.white);
			this.grid.set(74 + num2, 7 + num3, Color4.white);
			this.grid.set(75 + num2, 7 + num3, Color4.white);
			this.grid.set(73 + num2, 19 + num3, Color4.white);
			this.grid.set(74 + num2, 19 + num3, Color4.white);
			this.grid.set(75 + num2, 19 + num3, Color4.white);
			num2 = 160;
			num3 = 50;
			this.grid.set(73 + num2, 6 + num3, Color4.white);
			this.grid.set(74 + num2, 6 + num3, Color4.white);
			this.grid.set(75 + num2, 6 + num3, Color4.white);
			this.grid.set(73 + num2, 18 + num3, Color4.white);
			this.grid.set(74 + num2, 18 + num3, Color4.white);
			this.grid.set(75 + num2, 18 + num3, Color4.white);
			this.regions = this.grid.regionalize(delegate(int x, int y, Color4 c)
			{
				if (this.maskingAreas.Any<Rect2D>((Rect2D b) => b.Contains(x, y)))
				{
					return int.MaxValue;
				}
				if (c == Color4.black)
				{
					return 21474836;
				}
				return 0;
			}, true);
			List<InfluenceMapRegion> list2 = new List<InfluenceMapRegion>();
			Pathfinder pathfinder = new Pathfinder(240, 75);
			this.regions.Regions.Sort((InfluenceMapRegion a, InfluenceMapRegion b) => a.Center.SquareDistance(this.regions.Regions.First<InfluenceMapRegion>().Center).CompareTo(b.Center.SquareDistance(this.regions.Regions.First<InfluenceMapRegion>().Center)));
			using (List<InfluenceMapRegion>.Enumerator enumerator2 = this.regions.Regions.GetEnumerator())
			{
				Func<int, int, Color4, int> <>9__7;
				while (enumerator2.MoveNext())
				{
					InfluenceMapRegion region = enumerator2.Current;
					if (list2.Count > 0)
					{
						list2.Sort((InfluenceMapRegion a, InfluenceMapRegion b) => a.Center.SquareDistance(region.Center).CompareTo(b.Center.SquareDistance(region.Center)));
						InfluenceMapRegion influenceMapRegion = list2.First<InfluenceMapRegion>();
						Location2D randomElement = region.Cells.GetRandomElement<Location2D>(random);
						Location2D randomElement2 = influenceMapRegion.Cells.GetRandomElement<Location2D>(random);
						Pathfinder pathfinder2 = pathfinder;
						Grid<Color4> grid = this.grid;
						Func<int, int, Color4, int> func2;
						if ((func2 = <>9__7) == null)
						{
							func2 = (<>9__7 = delegate(int x, int y, Color4 c)
							{
								if (this.maskingAreas.Any<Rect2D>((Rect2D b) => b.Contains(x, y)))
								{
									return int.MaxValue;
								}
								if (c == Color4.black)
								{
									return 100;
								}
								return 0;
							});
						}
						pathfinder2.setWeightsFromGrid<Color4>(grid, func2);
						if (pathfinder.FindPath(randomElement, randomElement2, false, true, 24300, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								this.grid.set(pathfinderNode.X, pathfinderNode.Y, Color4.white);
							}
							list2.Add(region);
						}
					}
					else
					{
						list2.Add(region);
					}
				}
			}
			this.regions.RecalculateCostOnly(false);
		}

		// Token: 0x0400340D RID: 13325
		public Grid<Color4> grid;

		// Token: 0x0400340E RID: 13326
		public InfluenceMap regions;

		// Token: 0x0400340F RID: 13327
		public List<Rect2D> maskingAreas;

		// Token: 0x04003410 RID: 13328
		public List<string> stairZones = new List<string>();

		// Token: 0x04003411 RID: 13329
		public List<string> properties = new List<string>();
	}
}
