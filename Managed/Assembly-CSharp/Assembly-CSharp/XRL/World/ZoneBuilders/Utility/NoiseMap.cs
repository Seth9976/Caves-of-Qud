using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders.Utility
{
	// Token: 0x02000B1A RID: 2842
	public class NoiseMap
	{
		// Token: 0x06007217 RID: 29207 RVA: 0x002B0E50 File Offset: 0x002AF050
		public NoiseMap Copy(int x1, int y1, int x2, int y2)
		{
			NoiseMap noiseMap = new NoiseMap();
			noiseMap.Width = x2 - x1 + 1;
			noiseMap.Height = y2 - y1 + 1;
			noiseMap.Seeds = new int[noiseMap.Width, noiseMap.Height];
			noiseMap.Areas = new int[noiseMap.Width, noiseMap.Height];
			if (this.Noise != null)
			{
				noiseMap.Noise = new int[noiseMap.Width, noiseMap.Height];
				for (int i = x1; i <= x2; i++)
				{
					for (int j = y1; j <= y2; j++)
					{
						noiseMap.Noise[i - x1, j - y1] = this.Noise[x1, y1];
						noiseMap.Areas[i - x1, j - y1] = this.Areas[x1, y1];
					}
				}
			}
			return noiseMap;
		}

		// Token: 0x06007218 RID: 29208 RVA: 0x002B0F1D File Offset: 0x002AF11D
		public NoiseMap()
		{
		}

		// Token: 0x06007219 RID: 29209 RVA: 0x002B0F30 File Offset: 0x002AF130
		public NoiseMap(int _Width, int _Height, int MaxDepth, int SectorsWide, int SectorsHigh, string SeedsPerSector, int MinSeedDepth, int MaxSeedDepth, int BaseNoise, int FilterPasses, int BorderWidth, int _CutoffDepth, List<NoiseMapNode> ExtraNodes)
		{
			this.CreateNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector, MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, -1, int.MaxValue);
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x002B0F74 File Offset: 0x002AF174
		public NoiseMap(int _Width, int _Height, int MaxDepth, int SectorsWide, int SectorsHigh, int SeedsPerSector, int MinSeedDepth, int MaxSeedDepth, int BaseNoise, int FilterPasses, int BorderWidth, int _CutoffDepth, List<NoiseMapNode> ExtraNodes)
		{
			this.CreateNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector.ToString(), MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, -1, int.MaxValue);
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x002B0FBC File Offset: 0x002AF1BC
		public NoiseMap(int _Width, int _Height, int MaxDepth, int SectorsWide, int SectorsHigh, int SeedsPerSector, int MinSeedDepth, int MaxSeedDepth, int BaseNoise, int FilterPasses, int BorderWidth, int _CutoffDepth, List<NoiseMapNode> ExtraNodes, int iFilterBorder)
		{
			this.CreateNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector.ToString(), MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, iFilterBorder, int.MaxValue);
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x002B1004 File Offset: 0x002AF204
		public NoiseMap(int _Width, int _Height, int MaxDepth, int SectorsWide, int SectorsHigh, int SeedsPerSector, int MinSeedDepth, int MaxSeedDepth, int BaseNoise, int FilterPasses, int BorderWidth, int _CutoffDepth, List<NoiseMapNode> ExtraNodes, int iFilterBorder, int MaximumSeeds)
		{
			this.CreateNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector.ToString(), MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, iFilterBorder, MaximumSeeds);
		}

		// Token: 0x0600721D RID: 29213 RVA: 0x002B1048 File Offset: 0x002AF248
		public NoiseMap(int _Width, int _Height, int MaxDepth, int SectorsWide, int SectorsHigh, string SeedsPerSector, int MinSeedDepth, int MaxSeedDepth, int BaseNoise, int FilterPasses, int BorderWidth, int _CutoffDepth, List<NoiseMapNode> ExtraNodes, int iFilterBorder, int MaximumSeeds)
		{
			this.CreateNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector, MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, iFilterBorder, MaximumSeeds);
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x002B1088 File Offset: 0x002AF288
		public void FastQuadPlasma(int Width, int Height, int MinStartHeight, int MaxStartHeight, int RoughnessMin, int RoughnessMax)
		{
			if (Width % 2 == 0)
			{
				Width++;
			}
			if (Height % 2 == 0)
			{
				Height++;
			}
			if (Width < Height)
			{
				Height = Width;
			}
			if (Height < Width)
			{
				Height = Width;
			}
			this.Noise = new int[Width, Height];
			this.Areas = new int[Width, Height];
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					this.Noise[i, j] = int.MaxValue;
				}
			}
			Stack<Box> stack = new Stack<Box>();
			this.Noise[0, 0] = Stat.Random(MinStartHeight, MaxStartHeight);
			this.Noise[Width - 1, 0] = Stat.Random(MinStartHeight, MaxStartHeight);
			this.Noise[0, Height - 1] = Stat.Random(MinStartHeight, MaxStartHeight);
			this.Noise[Width - 1, Height - 1] = Stat.Random(MinStartHeight, MaxStartHeight);
			stack.Push(new Box(0, 0, Width - 1, Height - 1));
			while (stack.Count > 0)
			{
				Box box = stack.Pop();
				if (box.Width > 1)
				{
					if (this.Noise[box.MidX, box.y1] == 2147483647)
					{
						this.Noise[box.MidX, box.y1] = (this.Noise[box.x1, box.y1] + this.Noise[box.x2, box.y1]) / 2 + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.MidX, box.y2] == 2147483647)
					{
						this.Noise[box.MidX, box.y2] = (this.Noise[box.x1, box.y2] + this.Noise[box.x2, box.y2]) / 2 + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.x1, box.MidY] == 2147483647)
					{
						this.Noise[box.x1, box.MidY] = (this.Noise[box.x1, box.y1] + this.Noise[box.x1, box.y2]) / 2 + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.x2, box.MidY] == 2147483647)
					{
						this.Noise[box.x2, box.MidY] = (this.Noise[box.x2, box.y1] + this.Noise[box.x2, box.y2]) / 2 + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.MidX, box.MidY] == 2147483647)
					{
						this.Noise[box.MidX, box.MidY] = (this.Noise[box.x2, box.y1] + this.Noise[box.x2, box.y2] + this.Noise[box.x1, box.y1] + this.Noise[box.x1, box.y2]) / 4 + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (box.Width > 2)
					{
						stack.Push(new Box(box.x1, box.y1, box.MidX, box.MidY));
						stack.Push(new Box(box.MidX, box.y1, box.x2, box.MidY));
						stack.Push(new Box(box.x1, box.MidY, box.MidX, box.y2));
						stack.Push(new Box(box.MidX, box.MidY, box.x2, box.y2));
					}
				}
			}
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x002B1490 File Offset: 0x002AF690
		public void CreateNoiseMap(int _Width, int _Height, int MaxDepth, int SectorsWide, int SectorsHigh, string SeedsPerSector, int MinSeedDepth, int MaxSeedDepth, int BaseNoise, int FilterPasses, int BorderWidth, int _CutoffDepth, List<NoiseMapNode> ExtraNodes, int FilterBorder, int MaximumSeeds = 2147483647)
		{
			if (MinSeedDepth > MaxSeedDepth)
			{
				MaxSeedDepth = MinSeedDepth;
			}
			this.CutoffDepth = _CutoffDepth;
			this.Width = _Width;
			this.Height = _Height;
			this.Noise = new int[this.Width, this.Height];
			this.Seeds = new int[this.Width, this.Height];
			this.Areas = new int[this.Width, this.Height];
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Noise[j, i] = 0;
					this.Seeds[j, i] = 0;
				}
			}
			for (int k = BorderWidth + FilterPasses; k < this.Width - BorderWidth - FilterPasses; k++)
			{
				for (int l = BorderWidth + FilterPasses; l < this.Height - BorderWidth - FilterPasses; l++)
				{
					this.Noise[k, l] = Stat.Random(0, BaseNoise);
					this.Seeds[k, l] = 0;
				}
			}
			List<Rect2D> list = new List<Rect2D>();
			for (int m = 0; m < SectorsHigh; m++)
			{
				for (int n = 0; n < SectorsWide; n++)
				{
					int num = n * (this.Width / SectorsWide);
					int num2 = (n + 1) * (this.Width / SectorsWide);
					int num3 = m * (this.Height / SectorsHigh);
					int num4 = (m + 1) * (this.Height / SectorsHigh);
					list.Add(new Rect2D
					{
						x1 = num,
						x2 = num2,
						y1 = num3,
						y2 = num4
					});
				}
			}
			list.ShuffleInPlace<Rect2D>(null);
			foreach (Rect2D rect2D in list)
			{
				int num5 = rect2D.x1;
				int num6 = rect2D.x2;
				int num7 = rect2D.y1;
				int num8 = rect2D.y2;
				if (FilterBorder == -1)
				{
					if (num5 < BorderWidth + FilterPasses)
					{
						num5 = BorderWidth + FilterPasses;
					}
					if (num7 < BorderWidth + FilterPasses)
					{
						num7 = BorderWidth + FilterPasses;
					}
					if (num6 > this.Width - 1 - BorderWidth - FilterPasses)
					{
						num6 = this.Width - 1 - BorderWidth - FilterPasses;
					}
					if (num8 > this.Height - 1 - BorderWidth - FilterPasses)
					{
						num8 = this.Height - 1 - BorderWidth - FilterPasses;
					}
				}
				else
				{
					if (num5 < BorderWidth + FilterBorder)
					{
						num5 = BorderWidth + FilterBorder;
					}
					if (num7 < BorderWidth + FilterBorder)
					{
						num7 = BorderWidth + FilterBorder;
					}
					if (num6 > this.Width - 1 - BorderWidth - FilterBorder)
					{
						num6 = this.Width - 1 - BorderWidth - FilterBorder;
					}
					if (num8 > this.Height - 1 - BorderWidth - FilterBorder)
					{
						num8 = this.Height - 1 - BorderWidth - FilterBorder;
					}
				}
				int num9 = Stat.Roll(SeedsPerSector, null);
				for (int num10 = 0; num10 < num9; num10++)
				{
					int num11 = Stat.Random(num5, num6);
					int num12 = Stat.Random(num7, num8);
					this.PlacedSeeds.Add(Location2D.Get(num11, num12));
					this.Noise[num11, num12] = Stat.Random(MinSeedDepth, MaxSeedDepth);
					this.Seeds[num11, num12]++;
					MaximumSeeds--;
					if (MaximumSeeds <= 0)
					{
						break;
					}
				}
				if (MaximumSeeds <= 0)
				{
					break;
				}
			}
			if (ExtraNodes != null)
			{
				foreach (NoiseMapNode noiseMapNode in ExtraNodes)
				{
					if (noiseMapNode.x > 0 && noiseMapNode.y > 0 && noiseMapNode.x < this.Width - 1 && noiseMapNode.y < this.Height - 1)
					{
						if (noiseMapNode.depth == -1)
						{
							this.Noise[noiseMapNode.x, noiseMapNode.y] = MaxSeedDepth;
						}
						else
						{
							this.Noise[noiseMapNode.x, noiseMapNode.y] = noiseMapNode.depth;
						}
						this.Seeds[noiseMapNode.x, noiseMapNode.y]++;
					}
				}
			}
			int[,] array = new int[,]
			{
				{ 1, 3, 1 },
				{ 3, 6, 3 },
				{ 1, 3, 1 }
			};
			for (int num13 = 0; num13 < FilterPasses; num13++)
			{
				int[,] array2 = new int[this.Width, this.Height];
				for (int num14 = 0; num14 < this.Height; num14++)
				{
					for (int num15 = 0; num15 < this.Width; num15++)
					{
						int num16 = 0;
						array2[num15, num14] = 0;
						for (int num17 = 0; num17 < 3; num17++)
						{
							for (int num18 = 0; num18 < 3; num18++)
							{
								if (num15 + (num18 - 1) >= 0 && num15 + (num18 - 1) < this.Width && num14 + (num17 - 1) >= 0 && num14 + (num17 - 1) < this.Height)
								{
									array2[num15, num14] += this.Noise[num15 + (num18 - 1), num14 + (num17 - 1)] * array[num18, num17];
									num16 += array[num18, num17];
								}
							}
						}
						array2[num15, num14] /= num16;
					}
				}
				this.Noise = array2;
			}
			this.AreaNodes = new Dictionary<int, List<NoiseMapNode>>();
			try
			{
				this.FillAreas();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("NoiseMap::FillAreas", ex, "game_exception");
			}
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x002B1A70 File Offset: 0x002AFC70
		private void FillAreas()
		{
			this.nAreas = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Areas[j, i] = int.MinValue;
				}
			}
			for (int k = 0; k < this.Height; k++)
			{
				for (int l = 0; l < this.Width; l++)
				{
					if (this.Noise[l, k] > 0 && this.Areas[l, k] == -2147483648)
					{
						this.FillArea(l, k, this.nAreas);
						this.nAreas++;
					}
				}
			}
			for (int m = 0; m < this.nAreas; m++)
			{
				this.AreaNodes[m] = new List<NoiseMapNode>();
			}
			for (int n = 0; n < this.Height; n++)
			{
				for (int num = 0; num < this.Width; num++)
				{
					if (this.Areas[num, n] != -2147483648)
					{
						this.AreaNodes[this.Areas[num, n]].Add(new NoiseMapNode(num, n, this.Noise[num, n]));
					}
				}
			}
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x002B1BB8 File Offset: 0x002AFDB8
		private void FillArea(int x, int y, int a)
		{
			if (x < 0)
			{
				return;
			}
			if (x >= this.Width)
			{
				return;
			}
			if (y < 0)
			{
				return;
			}
			if (y >= this.Height)
			{
				return;
			}
			if (this.Areas[x, y] > -2147483648)
			{
				return;
			}
			if (this.Noise[x, y] <= this.CutoffDepth)
			{
				return;
			}
			this.Areas[x, y] = a;
			this.FillArea(x - 1, y - 1, a);
			this.FillArea(x, y - 1, a);
			this.FillArea(x + 1, y - 1, a);
			this.FillArea(x - 1, y, a);
			this.FillArea(x + 1, y, a);
			this.FillArea(x - 1, y + 1, a);
			this.FillArea(x, y + 1, a);
			this.FillArea(x + 1, y + 1, a);
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x002B1C7C File Offset: 0x002AFE7C
		public void Draw()
		{
			int i = 0;
			int num = this.Noise.GetUpperBound(1) + 1;
			while (i < num)
			{
				int j = 0;
				int num2 = this.Noise.GetUpperBound(0) + 1;
				while (j < num2)
				{
					if (this.Noise[j, i] > this.CutoffDepth)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(49);
					}
					else
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(32);
					}
					if (this.Seeds[j, i] > 0)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write("&W" + ((char)(48 + this.Seeds[j, i])).ToString(), true, false, false, null, -1);
					}
					j++;
				}
				i++;
			}
			Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
			Keyboard.getch();
		}

		// Token: 0x040034EE RID: 13550
		public int Width;

		// Token: 0x040034EF RID: 13551
		public int Height;

		// Token: 0x040034F0 RID: 13552
		public int CutoffDepth;

		// Token: 0x040034F1 RID: 13553
		public int[,] Seeds;

		// Token: 0x040034F2 RID: 13554
		public int[,] Noise;

		// Token: 0x040034F3 RID: 13555
		public int[,] Areas;

		// Token: 0x040034F4 RID: 13556
		public int nAreas;

		// Token: 0x040034F5 RID: 13557
		public Dictionary<int, List<NoiseMapNode>> AreaNodes;

		// Token: 0x040034F6 RID: 13558
		public List<Location2D> PlacedSeeds = new List<Location2D>();
	}
}
