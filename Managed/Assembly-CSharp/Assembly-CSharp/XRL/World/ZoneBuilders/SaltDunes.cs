using System;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD1 RID: 2769
	public class SaltDunes
	{
		// Token: 0x06006FD7 RID: 28631 RVA: 0x00276E7C File Offset: 0x0027507C
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject("DaylightWidget", null, null, null, null);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					Cell cell = Z.GetCell(j, i);
					if (Stat.RandomCosmetic(1, 100) <= 5)
					{
						GameObject gameObject = cell.AddObject("HighSaltDune" + Stat.Random(1, 12).ToString() + (50.in100() ? "White" : "Cyan"), null, null, null, null);
						cell.PaintTile = gameObject.Render.Tile;
						cell.PaintColorString = gameObject.Render.ColorString;
					}
					else if (Stat.RandomCosmetic(1, 100) <= 60)
					{
						cell.PaintTile = "Terrain/sw_ground_desert_1.bmp";
						cell.PaintColorString = "&K";
					}
					else
					{
						cell.PaintTile = "Terrain/sw_ground_desert_" + Stat.Random(1, 12).ToString() + ".bmp";
						cell.PaintColorString = (50.in100() ? "&y" : "&c");
					}
				}
			}
			Z.ClearReachableMap();
			return Z.BuildReachableMap(0, 0) >= 400;
		}
	}
}
