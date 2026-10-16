using System;
using UnityEngine;
using XRL.Rules;

namespace Genkit
{
	// Token: 0x02000375 RID: 885
	public static class GridExtensions
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x000A3388 File Offset: 0x000A1588
		public static Texture2D toTexture2D(this Grid<Color> grid)
		{
			Texture2D texture2D = new Texture2D(240, 75);
			texture2D.filterMode = FilterMode.Point;
			for (int i = 0; i < grid.width; i++)
			{
				for (int j = 0; j < grid.height; j++)
				{
					texture2D.SetPixel(i, grid.height - j - 1, grid.get(i, j));
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000A33EC File Offset: 0x000A15EC
		public static Texture2D toTexture2D(this Grid<Color4> grid)
		{
			Texture2D texture2D = new Texture2D(240, 75);
			texture2D.filterMode = FilterMode.Point;
			for (int i = 0; i < grid.width; i++)
			{
				for (int j = 0; j < grid.height; j++)
				{
					texture2D.SetPixel(i, grid.height - j - 1, grid.get(i, j).toUnityColor);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000A3457 File Offset: 0x000A1657
		public static Sprite toSprite(this Grid<Color> grid)
		{
			return Sprite.Create(grid.toTexture2D(), new Rect(0f, 0f, (float)grid.width, (float)grid.height), new Vector2(0f, 0f));
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000A3490 File Offset: 0x000A1690
		public static Sprite toSprite(this Grid<Color4> grid)
		{
			return Sprite.Create(grid.toTexture2D(), new Rect(0f, 0f, (float)grid.width, (float)grid.height), new Vector2(0f, 0f));
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000A34CC File Offset: 0x000A16CC
		public static void fromWFCTemplate(this Grid<Color> grid, string templateName, int n = 3, int seed = 2147483647)
		{
			WaveCollapseTools.LoadTemplates(false);
			WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(templateName, n, grid.width, grid.height, true, false, 8, 0);
			if (seed == 2147483647)
			{
				seed = new global::System.Random().Next();
			}
			waveCollapseFastModel.Run(seed, 0);
			Color32[] result = waveCollapseFastModel.GetResult();
			for (int i = 0; i < grid.width; i++)
			{
				for (int j = 0; j < grid.height; j++)
				{
					grid.set(i, j, new Color((float)result[i + j * grid.width].r, (float)result[i + j * grid.width].g, (float)result[i + j * grid.width].b));
				}
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000A358C File Offset: 0x000A178C
		public static void fromWFCTemplate(this Grid<Color4> grid, string templateName, int n = 3, int seed = 2147483647)
		{
			WaveCollapseTools.LoadTemplates(false);
			WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(templateName, n, grid.width, grid.height, true, false, 8, 0);
			if (seed == 2147483647)
			{
				seed = Stat.Random(0, 2147483646);
			}
			waveCollapseFastModel.Run(seed, 0);
			Color32[] result = waveCollapseFastModel.GetResult();
			for (int i = 0; i < grid.width; i++)
			{
				for (int j = 0; j < grid.height; j++)
				{
					grid.set(i, j, new Color4((float)result[i + j * grid.width].r, (float)result[i + j * grid.width].g, (float)result[i + j * grid.width].b));
				}
			}
		}
	}
}
