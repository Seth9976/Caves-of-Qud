using System;
using System.Collections.Generic;

namespace XRL.EditorFormats.Screen
{
	// Token: 0x020005C2 RID: 1474
	[Serializable]
	public class ScreenData
	{
		// Token: 0x06003ED6 RID: 16086 RVA: 0x00134DA6 File Offset: 0x00132FA6
		public ScreenData(int Width, int Height)
		{
			this.Rows = Height;
			this.Columns = Width;
			this.ResizeCells();
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00134DD0 File Offset: 0x00132FD0
		public void Clear()
		{
			Cell[,] cells = this.Cells;
			int upperBound = cells.GetUpperBound(0);
			int upperBound2 = cells.GetUpperBound(1);
			for (int i = cells.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = cells.GetLowerBound(1); j <= upperBound2; j++)
				{
					Cell cell = cells[i, j];
					cell.Char = ' ';
					cell.Background = 'k';
					cell.Foreground = 'k';
				}
			}
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00134E3C File Offset: 0x0013303C
		public void ResizeCells()
		{
			Cell[,] cells = this.Cells;
			this.Cells = new Cell[this.Columns, this.Rows];
			if (cells != null)
			{
				for (int i = 0; i < this.Rows; i++)
				{
					for (int j = 0; j < this.Columns; j++)
					{
						if (j < cells.GetUpperBound(0) && i < cells.GetUpperBound(1))
						{
							this.Cells[j, i] = cells[j, i];
						}
						else
						{
							this.Cells[j, i] = new Cell();
						}
					}
				}
				return;
			}
			for (int k = 0; k < this.Rows; k++)
			{
				for (int l = 0; l < this.Columns; l++)
				{
					this.Cells[l, k] = new Cell();
				}
			}
		}

		// Token: 0x04001F09 RID: 7945
		public Cell[,] Cells;

		// Token: 0x04001F0A RID: 7946
		public int Rows;

		// Token: 0x04001F0B RID: 7947
		public int Columns;

		// Token: 0x04001F0C RID: 7948
		public List<string> Properties = new List<string>();
	}
}
