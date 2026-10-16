using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Core;
using XRL.UI;

namespace Genkit
{
	// Token: 0x0200037D RID: 893
	public static class GridTools
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x000A7BA8 File Offset: 0x000A5DA8
		public static void DrawIntGrid(int[,] Grid, Rect2D R, bool bWait = true)
		{
			ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
			for (int i = 0; i <= Grid.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= Grid.GetUpperBound(1); j++)
				{
					scrapBuffer.Goto(i, j);
					if (i >= R.x1 && i <= R.x2 && j >= R.y1 && j <= R.y2)
					{
						scrapBuffer.Write("#", true, false, false, null, -1);
					}
					else
					{
						string text = Grid[i, j].ToString("X");
						scrapBuffer.Write(text, true, false, false, null, -1);
					}
				}
			}
			XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
			if (bWait)
			{
				Keyboard.getch();
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000A7C58 File Offset: 0x000A5E58
		public static void DrawIntGrid(int[,] Grid, bool bWait = true)
		{
			ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
			scrapBuffer.Clear();
			for (int i = 0; i <= Grid.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= Grid.GetUpperBound(1); j++)
				{
					scrapBuffer.Goto(i, j);
					int num = Grid[i, j];
					string text = Grid[i, j].ToString("X");
					if (num > 15)
					{
						num -= 16;
						text = "&G" + Grid[i, j].ToString("X");
					}
					if (num > 15)
					{
						num -= 16;
						text = "&W" + Grid[i, j].ToString("X");
					}
					if (num > 15)
					{
						num -= 16;
						text = "&R" + Grid[i, j].ToString("X");
					}
					scrapBuffer.Write(text, true, false, false, null, -1);
				}
			}
			XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
			if (bWait)
			{
				Keyboard.getch();
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000A7D64 File Offset: 0x000A5F64
		public static Rect2D MaxRectByArea(int[,] Grid, bool bDraw = false, int minWidth = 0, int minHeight = 0)
		{
			Rect2D zero = Rect2D.zero;
			int[,] array = new int[Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1];
			for (int i = 0; i <= Grid.GetUpperBound(0); i++)
			{
				int num = 0;
				for (int j = 0; j <= Grid.GetUpperBound(1); j++)
				{
					if (Grid[i, j] == 0)
					{
						num++;
					}
					else
					{
						num = 0;
					}
					array[i, j] = num;
				}
			}
			Stack<int> stack = new Stack<int>();
			int[,] array2 = new int[Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1];
			int[,] array3 = new int[Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1];
			int[,] array4 = new int[Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1];
			for (int k = 0; k <= Grid.GetUpperBound(1); k++)
			{
				stack.Clear();
				for (int l = 0; l <= Grid.GetUpperBound(0); l++)
				{
					while (stack.Count > 0 && array[l, k] <= array[stack.Peek(), k])
					{
						stack.Pop();
					}
					int num2;
					if (stack.Count == 0)
					{
						num2 = -1;
					}
					else
					{
						num2 = stack.Peek();
					}
					array2[l, k] = l - num2 - 1;
					array3[l, k] = l - num2 - 1;
					stack.Push(l);
				}
				stack.Clear();
				for (int l = Grid.GetUpperBound(0); l >= 0; l--)
				{
					while (stack.Count > 0 && array[l, k] <= array[stack.Peek(), k])
					{
						stack.Pop();
					}
					int num2;
					if (stack.Count == 0)
					{
						num2 = Grid.GetUpperBound(0) + 1;
					}
					else
					{
						num2 = stack.Peek();
					}
					array2[l, k] += num2 - l - 1;
					array4[l, k] = num2 - l - 1;
					stack.Push(l);
				}
			}
			int num3 = 0;
			for (int m = 0; m <= Grid.GetUpperBound(1); m++)
			{
				for (int n = 0; n <= Grid.GetUpperBound(0); n++)
				{
					array2[n, m] = array[n, m] * (array2[n, m] + 1);
					if (array2[n, m] > num3 && n + array4[n, m] - (n - array3[n, m]) + 1 >= minWidth && m - (m - array[n, m] + 1) + 1 >= minHeight)
					{
						zero.x1 = n - array3[n, m];
						zero.x2 = n + array4[n, m];
						zero.y1 = m - array[n, m] + 1;
						zero.y2 = m;
						num3 = array2[n, m];
					}
				}
			}
			if (bDraw)
			{
				GridTools.DrawIntGrid(array2, zero, true);
			}
			return zero;
		}
	}
}
