using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A9B RID: 2715
	public class HamilcrabShop
	{
		// Token: 0x06006F00 RID: 28416 RVA: 0x00265F04 File Offset: 0x00264104
		public List<Cell> GetContiguous(Zone Z, int X, int Y, int IX, int IY)
		{
			List<Cell> list = new List<Cell>();
			List<Cell> list2 = new List<Cell>();
			while (X < Z.Width && Y < Z.Height)
			{
				Cell cell = Z.GetCell(X, Y);
				if (cell.HasWall())
				{
					list.Add(cell);
				}
				else
				{
					if (list.Count > list2.Count)
					{
						list2.Clear();
						list2.AddRange(list);
					}
					list.Clear();
				}
				X += IX;
				Y += IY;
			}
			if (list.Count > list2.Count)
			{
				list2.Clear();
				list2.AddRange(list);
			}
			return list2;
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x00265F94 File Offset: 0x00264194
		public void Fill(BallBag<Cell> Bag, List<Cell> Cells)
		{
			int i = 0;
			int count = Cells.Count;
			int num = count / 2;
			while (i < count)
			{
				Cell cell = Cells[i];
				Bag.Add(cell, count / (1 + Math.Abs(i - num)));
				i++;
			}
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x00265FD4 File Offset: 0x002641D4
		public bool BuildZone(Zone Z)
		{
			BallBag<Cell> ballBag = new BallBag<Cell>(0);
			List<Cell> contiguous = this.GetContiguous(Z, 0, this.Padding, 1, 0);
			List<Cell> contiguous2 = this.GetContiguous(Z, 79 - this.Padding, 0, 0, 1);
			List<Cell> contiguous3 = this.GetContiguous(Z, this.Padding, 0, 0, 1);
			if (contiguous.Count >= 10)
			{
				this.Fill(ballBag, contiguous);
			}
			if (contiguous2.Count >= 10)
			{
				this.Fill(ballBag, contiguous2);
			}
			if (contiguous3.Count >= 10)
			{
				this.Fill(ballBag, contiguous3);
			}
			Cell cell = ballBag.PeekOne() ?? Z.GetRandomCell(0);
			int num = 0;
			if (!contiguous.Contains(cell))
			{
				if (contiguous2.Contains(cell))
				{
					num = 1;
				}
				else if (contiguous3.Contains(cell))
				{
					num = 3;
				}
			}
			MapChunkPlacement.PlaceFromFile(cell, "preset_tile_chunks/HamilcrabShop.rpm", this.Width, this.Height, this.Padding, num, null, null, null);
			return true;
		}

		// Token: 0x040032CA RID: 13002
		public int Width = 6;

		// Token: 0x040032CB RID: 13003
		public int Height = 7;

		// Token: 0x040032CC RID: 13004
		public int Padding;
	}
}
