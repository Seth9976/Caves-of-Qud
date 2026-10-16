using System;
using System.Collections;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x02000376 RID: 886
	[Serializable]
	public class Grid<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x000A364C File Offset: 0x000A184C
		public Grid(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.cells = new T[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					this.cells[i, j] = default(T);
				}
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000A36A8 File Offset: 0x000A18A8
		public void fill(T fill)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.cells[i, j] = fill;
				}
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000A36E8 File Offset: 0x000A18E8
		public Grid<T> mirrorHorizontal()
		{
			Grid<T> grid = new Grid<T>(this.width, this.height);
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (i < this.width / 2)
					{
						grid.cells[i, j] = this.cells[i, j];
					}
					else
					{
						grid.cells[i, j] = this.cells[this.width - i - 1, j];
					}
				}
			}
			return grid;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000A3774 File Offset: 0x000A1974
		public Grid<T> mirrorVertical()
		{
			Grid<T> grid = new Grid<T>(this.width, this.height);
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					if (j < this.height / 2)
					{
						grid.cells[i, j] = this.cells[i, j];
					}
					else
					{
						grid.cells[i, j] = this.cells[i, this.height - j - 1];
					}
				}
			}
			return grid;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000A3800 File Offset: 0x000A1A00
		public T get(int x, int y)
		{
			return this.cells[x, y];
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000A3810 File Offset: 0x000A1A10
		public void setRect(int x1, int y1, int x2, int y2, T value)
		{
			for (int i = x1; i <= x2; i++)
			{
				for (int j = y1; j <= y2; j++)
				{
					this.set(i, j, value);
				}
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000A3840 File Offset: 0x000A1A40
		public void set(int x, int y, T value)
		{
			this.cells[x, y] = value;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000A3850 File Offset: 0x000A1A50
		public T[,] copy()
		{
			return this.transformCopy<T>((T element) => element);
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000A3878 File Offset: 0x000A1A78
		public InfluenceMap regionalize(Func<int, int, T, int> wallTest, bool costOnly = true)
		{
			InfluenceMap regionMap = new InfluenceMap(this.width, this.height);
			this.forEach(delegate(int x, int y, T c)
			{
				regionMap.Walls[x, y] = wallTest(x, y, c);
			});
			regionMap.SeedAllUnseeded(false, true);
			if (costOnly)
			{
				regionMap.RecalculateCostOnly(false);
			}
			else
			{
				regionMap.Recalculate(false);
			}
			return regionMap;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000A38EC File Offset: 0x000A1AEC
		public void forEach(Action<int, int, T> action)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					action(i, j, this.get(i, j));
				}
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000A392B File Offset: 0x000A1B2B
		public void box(Rect2D rect, Func<T> value)
		{
			this.box(rect.x1, rect.y1, rect.Width, rect.Height, value);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000A3950 File Offset: 0x000A1B50
		public void box(int xp, int yp, int width, int height, Func<T> value)
		{
			for (int i = xp; i < xp + width; i++)
			{
				for (int j = yp; j < yp + height; j++)
				{
					this.set(i, j, value());
				}
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x000A398C File Offset: 0x000A1B8C
		public void box(int xp, int yp, int width, int height, Func<int, int, T> value)
		{
			for (int i = xp; i < xp + width; i++)
			{
				for (int j = yp; j < yp + height; j++)
				{
					this.set(i, j, value(i, j));
				}
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x000A39C8 File Offset: 0x000A1BC8
		public U[,] transformCopy<U>(Func<T, U> transform)
		{
			U[,] array = new U[this.width, this.height];
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					array[i, j] = transform(this.cells[i, j]);
				}
			}
			return array;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000A3A28 File Offset: 0x000A1C28
		public void transform(Func<T, T> transform)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.cells[i, j] = transform(this.get(i, j));
				}
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x000A3A74 File Offset: 0x000A1C74
		public void transform(Func<int, int, T, T> transform)
		{
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					this.cells[i, j] = transform(i, j, this.get(i, j));
				}
			}
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int x = 0; x < this.width; x = num + 1)
			{
				for (int y = 0; y < this.height; y = num + 1)
				{
					yield return this.get(x, y);
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x000A3ACF File Offset: 0x000A1CCF
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int x = 0; x < this.width; x = num + 1)
			{
				for (int y = 0; y < this.height; y = num + 1)
				{
					yield return this.get(x, y);
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x040010B4 RID: 4276
		public int width;

		// Token: 0x040010B5 RID: 4277
		public int height;

		// Token: 0x040010B6 RID: 4278
		public T[,] cells;
	}
}
