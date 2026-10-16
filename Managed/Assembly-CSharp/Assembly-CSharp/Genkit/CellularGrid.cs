using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.UI;

namespace Genkit
{
	// Token: 0x02000373 RID: 883
	public class CellularGrid
	{
		// Token: 0x060020ED RID: 8429 RVA: 0x000A2C9C File Offset: 0x000A0E9C
		public CellularGrid()
		{
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000A2CFC File Offset: 0x000A0EFC
		public void Draw()
		{
			ScreenBuffer scrapBuffer = TextConsole.GetScrapBuffer1(false);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					scrapBuffer.Goto(i, j);
					if (this.cells[i, j] <= 0)
					{
						scrapBuffer.Write("&K.", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("&Y#", true, false, false, null, -1);
					}
				}
			}
			Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
			Keyboard.getch();
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000A2D84 File Offset: 0x000A0F84
		public void ApplyCAPass(int numIterations)
		{
			int[,] array = new int[this.Width, this.Height];
			int num = 1;
			List<int> list = new List<int>(this.BornList);
			List<int> list2 = new List<int>(this.SurviveList);
			while (numIterations > 0)
			{
				numIterations--;
				for (int i = 0; i < this.Width; i += num)
				{
					for (int j = 0; j < this.Height; j += num)
					{
						bool flag;
						if (i == 0 || i >= this.Width - num || j == 0 || j >= this.Height - num)
						{
							flag = this.SeedBorders;
						}
						else
						{
							int num2 = 0;
							num2 += this.cells[i - num, j - num];
							num2 += this.cells[i - num, j];
							num2 += this.cells[i - num, j + num];
							num2 += this.cells[i, j - num];
							num2 += this.cells[i, j + num];
							num2 += this.cells[i + num, j - num];
							num2 += this.cells[i + num, j];
							num2 += this.cells[i + num, j + num];
							int num3 = this.cells[i, j];
							flag = (num3 == 0 && list.Contains(num2)) || (num3 == 1 && list2.Contains(num2));
						}
						for (int k = 0; k < num; k++)
						{
							for (int l = 0; l < num; l++)
							{
								array[i + k, j + l] = (flag ? 1 : 0);
							}
						}
					}
				}
			}
			for (int m = 0; m < this.Width; m++)
			{
				for (int n = 0; n < this.Height; n++)
				{
					this.cells[m, n] = array[m, n];
				}
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000A2FA0 File Offset: 0x000A11A0
		public CellularGrid(Random R, int Seed, int Width, int Height)
		{
			this.Generate(R, Seed, Width, Height);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000A300C File Offset: 0x000A120C
		public CellularGrid(Random R, string Seed, int Width, int Height)
		{
			this.Generate(R, Hash.String(Seed), Width, Height);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000A307A File Offset: 0x000A127A
		public void Generate(Random R, int Width, int Height)
		{
			this.Generate(R, R.Next(), Width, Height);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000A308B File Offset: 0x000A128B
		public void Generate(Random R, string Seed, int Width, int Height)
		{
			this.Generate(R, Hash.String(Seed), Width, Height);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000A30A0 File Offset: 0x000A12A0
		public void Generate(Random R, int Seed, int _Width, int _Height)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.cells = new int[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (this.SeedBorders && (i <= this.BorderDepth - 1 || j <= this.BorderDepth - 1 || i >= this.Width - this.BorderDepth || j >= this.Height - this.BorderDepth))
					{
						this.cells[i, j] = 1;
					}
					else if (R.Next(1, 100) <= this.SeedChance)
					{
						this.cells[i, j] = 1;
					}
					else
					{
						this.cells[i, j] = 0;
					}
				}
			}
			if (Options.GetOption("OptionDrawCASystems", "No") == "Yes")
			{
				this.Draw();
			}
			for (int k = 0; k < this.Passes; k++)
			{
				this.ApplyCAPass(1);
				if (Options.GetOption("OptionDrawCASystems", "No") == "Yes")
				{
					this.Draw();
				}
			}
		}

		// Token: 0x040010A6 RID: 4262
		public Random R;

		// Token: 0x040010A7 RID: 4263
		public bool SeedBorders = true;

		// Token: 0x040010A8 RID: 4264
		public int BorderDepth = 1;

		// Token: 0x040010A9 RID: 4265
		public int Width;

		// Token: 0x040010AA RID: 4266
		public int Height;

		// Token: 0x040010AB RID: 4267
		public int SeedChance = 55;

		// Token: 0x040010AC RID: 4268
		public int Passes = 2;

		// Token: 0x040010AD RID: 4269
		public int[] BornList = new int[] { 6, 7, 8 };

		// Token: 0x040010AE RID: 4270
		public int[] SurviveList = new int[] { 5, 6, 7, 8 };

		// Token: 0x040010AF RID: 4271
		public int[,] cells;
	}
}
