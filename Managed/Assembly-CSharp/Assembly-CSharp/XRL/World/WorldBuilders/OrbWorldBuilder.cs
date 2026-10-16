using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;

namespace XRL.World.WorldBuilders
{
	// Token: 0x0200099D RID: 2461
	public class OrbWorldBuilder : WorldBuilder
	{
		// Token: 0x06006A7B RID: 27259 RVA: 0x0022208C File Offset: 0x0022028C
		public override bool BuildWorld(string worldName)
		{
			Zone zone = ZoneManager.instance.GetZone(worldName);
			List<Tuple<Location2D, int>> list = new List<Tuple<Location2D, int>>();
			for (int i = 0; i < 5; i++)
			{
				int num = Stat.Random(4, 16);
				Location2D location2D = Location2D.Get(Stat.Random(0, zone.Width - 1), Stat.Random(0, zone.Height - 1));
				list.Add(new Tuple<Location2D, int>(location2D, num));
			}
			List<Tuple<Location2D, int>> list2 = new List<Tuple<Location2D, int>>();
			for (int j = 0; j < 5; j++)
			{
				int num2 = Stat.Random(15, 40);
				Location2D location2D2 = Location2D.Get(Stat.Random(0, zone.Width - 1), Stat.Random(0, zone.Height - 1));
				list2.Add(new Tuple<Location2D, int>(location2D2, num2));
			}
			int x;
			int num3;
			for (x = 0; x < zone.Width; x = num3 + 1)
			{
				int y;
				for (y = 0; y < zone.Height; y = num3 + 1)
				{
					if (x == 40 && y == 15)
					{
						zone.GetCell(x, y).AddObject("TerrainOtar", null, null, null, null);
					}
					else
					{
						GameObject gameObject = zone.GetCell(x, y).AddObject("TerrainCraters", null, null, null, null);
						if (list2.Any<Tuple<Location2D, int>>((Tuple<Location2D, int> t) => t.Item1.Distance(Location2D.Get(x, y)) == t.Item2))
						{
							gameObject.SetStringProperty("SmallCrater", "yes", false);
						}
						if (list.Any<Tuple<Location2D, int>>((Tuple<Location2D, int> t) => t.Item1.Distance(Location2D.Get(x, y)) == t.Item2))
						{
							gameObject.SetStringProperty("BigCrater", "yes", false);
						}
					}
					num3 = y;
				}
				num3 = x;
			}
			zone.HandleEvent(new BeforeZoneBuiltEvent());
			zone.HandleEvent(new ZoneBuiltEvent());
			zone.HandleEvent(new AfterZoneBuiltEvent());
			return true;
		}
	}
}
