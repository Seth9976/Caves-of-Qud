using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF9 RID: 2809
	public class BuildingTile
	{
		// Token: 0x06007071 RID: 28785 RVA: 0x002807CD File Offset: 0x0027E9CD
		public BuildingTile()
		{
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x002807D8 File Offset: 0x0027E9D8
		public BuildingTile(List<string> Template)
		{
			int length = Template[0].Length;
			for (int i = 0; i < length; i++)
			{
			}
			this.Tile = new TileType[length, length];
			this.CustomTile = new char[length, length];
			for (int j = 0; j < length; j++)
			{
				for (int k = 0; k < length; k++)
				{
					this.Tile[k, j] = TileType.Custom;
					if (Template[j][k] == '%')
					{
						this.Tile[k, j] = TileType.Garbage;
					}
					if (Template[j][k] == 'c')
					{
						this.Tile[k, j] = TileType.LittleChest;
					}
					if (Template[j][k] == 'C')
					{
						this.Tile[k, j] = TileType.BigChest;
					}
					if (Template[j][k] == '1')
					{
						this.Tile[k, j] = TileType.Creature1;
					}
					if (Template[j][k] == '.')
					{
						this.Tile[k, j] = TileType.Open;
					}
					if (Template[j][k] == ',')
					{
						this.Tile[k, j] = TileType.OpenConnect;
					}
					if (Template[j][k] == '#')
					{
						this.Tile[k, j] = TileType.Wall;
					}
					if (Template[j][k] == 'D')
					{
						this.Tile[k, j] = TileType.Door;
					}
					if (Template[j][k] == '?')
					{
						this.Tile[k, j] = TileType.Any;
					}
					if (Template[j][k] == '~')
					{
						this.Tile[k, j] = TileType.Liquid;
					}
					this.CustomTile[k, j] = Template[j][k];
				}
			}
			this.Template = Template;
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x002809AC File Offset: 0x0027EBAC
		public void Trace()
		{
			if (this.Template == null)
			{
				return;
			}
			foreach (string text in this.Template)
			{
			}
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x00280A04 File Offset: 0x0027EC04
		public static bool Matches(TileType T1, TileType T2)
		{
			return T1 == TileType.Any || T2 == TileType.Any || ((T1 != TileType.Open || T2 == TileType.Open) && (T2 != TileType.Open || T1 == TileType.Open));
		}

		// Token: 0x040033EF RID: 13295
		public List<string> Template;

		// Token: 0x040033F0 RID: 13296
		public TileType[,] Tile;

		// Token: 0x040033F1 RID: 13297
		public char[,] CustomTile;
	}
}
