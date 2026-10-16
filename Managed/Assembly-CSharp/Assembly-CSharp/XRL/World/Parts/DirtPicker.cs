using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001252 RID: 4690
	public static class DirtPicker
	{
		// Token: 0x0600BA57 RID: 47703 RVA: 0x00441084 File Offset: 0x0043F284
		public static string GetRandomGrassTile()
		{
			int num = Stat.Random(1, 4);
			if (num <= 1)
			{
				return "assets_content_textures_tiles_tile-grass1.png";
			}
			if (num <= 2)
			{
				return "Terrain/sw_grass1.bmp";
			}
			if (num <= 3)
			{
				return "Terrain/sw_grass2.bmp";
			}
			return "Terrain/sw_grass3.bmp";
		}
	}
}
