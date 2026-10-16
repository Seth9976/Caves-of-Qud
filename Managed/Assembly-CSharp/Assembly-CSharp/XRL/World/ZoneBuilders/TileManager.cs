using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AFA RID: 2810
	public class TileManager
	{
		// Token: 0x06007075 RID: 28789 RVA: 0x00280A28 File Offset: 0x0027EC28
		public void AddTile(List<string> Tempalate, bool bMirror, bool bFlip)
		{
			this.Tiles.Add(new BuildingTile(Tempalate));
			if (bMirror)
			{
				this.Tiles.Add(new BuildingTile(this.Mirror(Tempalate)));
				this.Tiles.Add(new BuildingTile(this.Reverse(Tempalate)));
				if (bFlip)
				{
					this.Tiles.Add(new BuildingTile(this.Mirror(this.Reverse(Tempalate))));
				}
				this.Tiles.Add(new BuildingTile(this.RotateLeft(Tempalate)));
				if (bFlip)
				{
					this.Tiles.Add(new BuildingTile(this.Mirror(this.RotateLeft(Tempalate))));
				}
				this.Tiles.Add(new BuildingTile(this.RotateRight(Tempalate)));
				if (bFlip)
				{
					this.Tiles.Add(new BuildingTile(this.Mirror(this.RotateRight(Tempalate))));
				}
			}
		}

		// Token: 0x06007076 RID: 28790 RVA: 0x00280B08 File Offset: 0x0027ED08
		public List<string> RotateRight(List<string> Source)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.TileSize; i++)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < this.TileSize; j++)
				{
					stringBuilder.Append(Source[this.TileSize - 1 - j][i]);
				}
				list.Add(stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x06007077 RID: 28791 RVA: 0x00280B70 File Offset: 0x0027ED70
		public List<string> RotateLeft(List<string> Source)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.TileSize; i++)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < this.TileSize; j++)
				{
					stringBuilder.Append(Source[j][this.TileSize - 1 - i]);
				}
				list.Add(stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x06007078 RID: 28792 RVA: 0x00280BD8 File Offset: 0x0027EDD8
		public List<string> Mirror(List<string> Source)
		{
			List<string> list = new List<string>();
			for (int i = this.TileSize - 1; i >= 0; i--)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = this.TileSize - 1; j >= 0; j--)
				{
					stringBuilder.Append(Source[i][j]);
				}
				list.Add(stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x06007079 RID: 28793 RVA: 0x00280C38 File Offset: 0x0027EE38
		public List<string> Reverse(List<string> Source)
		{
			List<string> list = new List<string>(Source);
			list.Reverse();
			return list;
		}

		// Token: 0x040033F2 RID: 13298
		public Dictionary<string, int> Markers = new Dictionary<string, int>();

		// Token: 0x040033F3 RID: 13299
		public int TileSize = 6;

		// Token: 0x040033F4 RID: 13300
		public List<BuildingTile> Tiles = new List<BuildingTile>();
	}
}
