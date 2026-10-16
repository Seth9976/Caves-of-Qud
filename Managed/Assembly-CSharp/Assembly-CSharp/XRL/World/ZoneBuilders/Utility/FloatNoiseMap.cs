using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders.Utility
{
	// Token: 0x02000B18 RID: 2840
	public class FloatNoiseMap
	{
		// Token: 0x0600720C RID: 29196 RVA: 0x002B0050 File Offset: 0x002AE250
		public FloatNoiseMap Copy(int x1, int y1, int x2, int y2)
		{
			FloatNoiseMap floatNoiseMap = new FloatNoiseMap();
			floatNoiseMap.Width = x2 - x1 + 1;
			floatNoiseMap.Height = y2 - y1 + 1;
			floatNoiseMap.Seeds = new float[floatNoiseMap.Width, floatNoiseMap.Height];
			floatNoiseMap.Areas = new float[floatNoiseMap.Width, floatNoiseMap.Height];
			if (this.Noise != null)
			{
				floatNoiseMap.Noise = new float[floatNoiseMap.Width, floatNoiseMap.Height];
				for (int i = x1; i <= x2; i++)
				{
					for (int j = y1; j <= y2; j++)
					{
						floatNoiseMap.Noise[i - x1, j - y1] = this.Noise[x1, y1];
					}
				}
			}
			return floatNoiseMap;
		}

		// Token: 0x0600720D RID: 29197 RVA: 0x002B00FF File Offset: 0x002AE2FF
		public FloatNoiseMap()
		{
		}

		// Token: 0x0600720E RID: 29198 RVA: 0x002B0108 File Offset: 0x002AE308
		public FloatNoiseMap(int _Width, int _Height, float MaxDepth, int SectorsWide, int SectorsHigh, int SeedsPerSector, float MinSeedDepth, float MaxSeedDepth, float BaseNoise, int FilterPasses, int BorderWidth, float _CutoffDepth, List<FloatNoiseMapNode> ExtraNodes)
		{
			this.CreateFloatNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector, MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, -1, true);
		}

		// Token: 0x0600720F RID: 29199 RVA: 0x002B013C File Offset: 0x002AE33C
		public FloatNoiseMap(int _Width, int _Height, float MaxDepth, int SectorsWide, int SectorsHigh, int SeedsPerSector, float MinSeedDepth, float MaxSeedDepth, float BaseNoise, int FilterPasses, int BorderWidth, float _CutoffDepth, List<FloatNoiseMapNode> ExtraNodes, int iFilterBorder)
		{
			this.CreateFloatNoiseMap(_Width, _Height, MaxDepth, SectorsWide, SectorsHigh, SeedsPerSector, MinSeedDepth, MaxSeedDepth, BaseNoise, FilterPasses, BorderWidth, _CutoffDepth, ExtraNodes, iFilterBorder, true);
		}

		// Token: 0x06007210 RID: 29200 RVA: 0x002B0170 File Offset: 0x002AE370
		public void FastQuadPlasma(int Width, int Height, int MinStartHeight, int MaxStartHeight, float RoughnessMin, float RoughnessMax)
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
			this.Noise = new float[Width, Height];
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					this.Noise[i, j] = float.MaxValue;
				}
			}
			Stack<Box> stack = new Stack<Box>();
			this.Noise[0, 0] = (float)Stat.Random(MinStartHeight, MaxStartHeight);
			this.Noise[Width - 1, 0] = (float)Stat.Random(MinStartHeight, MaxStartHeight);
			this.Noise[0, Height - 1] = (float)Stat.Random(MinStartHeight, MaxStartHeight);
			this.Noise[Width - 1, Height - 1] = (float)Stat.Random(MinStartHeight, MaxStartHeight);
			stack.Push(new Box(0, 0, Width - 1, Height - 1));
			while (stack.Count > 0)
			{
				Box box = stack.Pop();
				if (box.Width > 1)
				{
					if (this.Noise[box.MidX, box.y1] == 3.4028235E+38f)
					{
						this.Noise[box.MidX, box.y1] = (this.Noise[box.x1, box.y1] + this.Noise[box.x2, box.y1]) / 2f + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.MidX, box.y2] == 3.4028235E+38f)
					{
						this.Noise[box.MidX, box.y2] = (this.Noise[box.x1, box.y2] + this.Noise[box.x2, box.y2]) / 2f + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.x1, box.MidY] == 3.4028235E+38f)
					{
						this.Noise[box.x1, box.MidY] = (this.Noise[box.x1, box.y1] + this.Noise[box.x1, box.y2]) / 2f + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.x2, box.MidY] == 3.4028235E+38f)
					{
						this.Noise[box.x2, box.MidY] = (this.Noise[box.x2, box.y1] + this.Noise[box.x2, box.y2]) / 2f + Stat.Random(RoughnessMin, RoughnessMax);
					}
					if (this.Noise[box.MidX, box.MidY] == 3.4028235E+38f)
					{
						this.Noise[box.MidX, box.MidY] = (this.Noise[box.x2, box.y1] + this.Noise[box.x2, box.y2] + this.Noise[box.x1, box.y1] + this.Noise[box.x1, box.y2]) / 4f + Stat.Random(RoughnessMin, RoughnessMax);
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

		// Token: 0x06007211 RID: 29201 RVA: 0x002B0584 File Offset: 0x002AE784
		public void CreateFloatNoiseMap(int _Width, int _Height, float MaxDepth, int SectorsWide, int SectorsHigh, int SeedsPerSector, float MinSeedDepth, float MaxSeedDepth, float BaseNoise, int FilterPasses, int BorderWidth, float _CutoffDepth, List<FloatNoiseMapNode> ExtraNodes, int FilterBorder, bool DoAreas)
		{
			if (MinSeedDepth > MaxSeedDepth)
			{
				MaxSeedDepth = MinSeedDepth;
			}
			this.CutoffDepth = _CutoffDepth;
			this.Width = _Width;
			this.Height = _Height;
			this.Noise = new float[this.Width, this.Height];
			this.Seeds = new float[this.Width, this.Height];
			this.Areas = new float[this.Width, this.Height];
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Noise[j, i] = 0f;
					this.Seeds[j, i] = 0f;
				}
			}
			for (int k = BorderWidth + FilterPasses; k < this.Height - BorderWidth - FilterPasses; k++)
			{
				for (int l = BorderWidth + FilterPasses; l < this.Width - BorderWidth - FilterPasses; l++)
				{
					this.Noise[l, k] = Stat.Random(0f, BaseNoise);
					this.Seeds[l, k] = 0f;
				}
			}
			for (int m = 0; m < SectorsHigh; m++)
			{
				for (int n = 0; n < SectorsWide; n++)
				{
					int num = n * (this.Width / SectorsWide);
					int num2 = (n + 1) * (this.Width / SectorsWide);
					int num3 = m * (this.Height / SectorsHigh);
					int num4 = (m + 1) * (this.Height / SectorsHigh);
					if (FilterBorder == -1)
					{
						if (num < BorderWidth + FilterPasses)
						{
							num = BorderWidth + FilterPasses;
						}
						if (num3 < BorderWidth + FilterPasses)
						{
							num3 = BorderWidth + FilterPasses;
						}
						if (num2 > this.Width - 1 - BorderWidth - FilterPasses)
						{
							num2 = this.Width - 1 - BorderWidth - FilterPasses;
						}
						if (num4 > this.Height - 1 - BorderWidth - FilterPasses)
						{
							num4 = this.Height - 1 - BorderWidth - FilterPasses;
						}
					}
					else
					{
						if (num < BorderWidth + FilterBorder)
						{
							num = BorderWidth + FilterBorder;
						}
						if (num3 < BorderWidth + FilterBorder)
						{
							num3 = BorderWidth + FilterBorder;
						}
						if (num2 > this.Width - 1 - BorderWidth - FilterBorder)
						{
							num2 = this.Width - 1 - BorderWidth - FilterBorder;
						}
						if (num4 > this.Height - 1 - BorderWidth - FilterBorder)
						{
							num4 = this.Height - 1 - BorderWidth - FilterBorder;
						}
					}
					for (float num5 = 0f; num5 < (float)SeedsPerSector; num5 += 1f)
					{
						int num6 = Stat.Random(num, num2);
						int num7 = Stat.Random(num3, num4);
						this.Noise[num6, num7] = Stat.Random(MinSeedDepth, MaxSeedDepth);
						this.Seeds[num6, num7] += 1f;
					}
				}
			}
			if (ExtraNodes != null)
			{
				foreach (FloatNoiseMapNode floatNoiseMapNode in ExtraNodes)
				{
					if (floatNoiseMapNode.x > 0 && floatNoiseMapNode.y > 0 && floatNoiseMapNode.x < this.Width - 1 && floatNoiseMapNode.y < this.Height - 1)
					{
						if (floatNoiseMapNode.depth == -1f)
						{
							this.Noise[floatNoiseMapNode.x, floatNoiseMapNode.y] = MaxSeedDepth;
						}
						else
						{
							this.Noise[floatNoiseMapNode.x, floatNoiseMapNode.y] = floatNoiseMapNode.depth;
						}
						this.Seeds[floatNoiseMapNode.x, floatNoiseMapNode.y] += 1f;
					}
				}
			}
			float[,] array = new float[,]
			{
				{ 1f, 3f, 1f },
				{ 3f, 6f, 3f },
				{ 1f, 3f, 1f }
			};
			for (int num8 = 0; num8 < FilterPasses; num8++)
			{
				float[,] array2 = new float[this.Width, this.Height];
				for (int num9 = 0; num9 < this.Height; num9++)
				{
					for (int num10 = 0; num10 < this.Width; num10++)
					{
						float num11 = 0f;
						array2[num10, num9] = 0f;
						for (int num12 = 0; num12 < 3; num12++)
						{
							for (int num13 = 0; num13 < 3; num13++)
							{
								if (num10 + (num13 - 1) >= 0 && num10 + (num13 - 1) < this.Width && num9 + (num12 - 1) >= 0 && num9 + (num12 - 1) < this.Height)
								{
									array2[num10, num9] += this.Noise[num10 + (num13 - 1), num9 + (num12 - 1)] * array[num13, num12];
									num11 += array[num13, num12];
								}
							}
						}
						array2[num10, num9] /= num11;
					}
				}
				this.Noise = array2;
			}
			this.AreaNodes = new Dictionary<float, List<FloatNoiseMapNode>>();
			if (DoAreas)
			{
				this.FillAreas();
			}
		}

		// Token: 0x06007212 RID: 29202 RVA: 0x002B0A84 File Offset: 0x002AEC84
		private void FillAreas()
		{
			this.nAreas = 0f;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Areas[j, i] = -1f;
				}
			}
			for (int k = 0; k < this.Height; k++)
			{
				for (int l = 0; l < this.Width; l++)
				{
					if (this.Noise[l, k] > 1f && this.Areas[l, k] == -1f)
					{
						this.FillArea(l, k, this.nAreas);
						this.nAreas += 1f;
					}
				}
			}
			int num = 0;
			while ((float)num < this.nAreas)
			{
				this.AreaNodes[(float)num] = new List<FloatNoiseMapNode>();
				num++;
			}
			for (int m = 0; m < this.Width; m++)
			{
				for (int n = 0; n < this.Height; n++)
				{
					if (this.Areas[m, n] != -1f)
					{
						this.AreaNodes[this.Areas[m, n]].Add(new FloatNoiseMapNode(m, n, this.Noise[m, n]));
					}
				}
			}
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x002B0BDC File Offset: 0x002AEDDC
		private void FillArea(int x, int y, float a)
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
			if (this.Noise[x, y] <= this.CutoffDepth)
			{
				return;
			}
			if (this.Areas[x, y] != -1f)
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

		// Token: 0x06007214 RID: 29204 RVA: 0x002B0CA0 File Offset: 0x002AEEA0
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
					if (this.Areas[j, i] != -1f)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write((short)((float)this.Areas[j, i].ToString()[0]));
					}
					else
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write(32);
					}
					if (this.Seeds[j, i] > 0f)
					{
						Popup._ScreenBuffer.Goto(j, i);
						Popup._ScreenBuffer.Write("&W" + ((char)(48f + this.Seeds[j, i])).ToString(), true, false, false, null, -1);
					}
					j++;
				}
				i++;
			}
			Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
			Keyboard.getch();
		}

		// Token: 0x040034E3 RID: 13539
		public int Width;

		// Token: 0x040034E4 RID: 13540
		public int Height;

		// Token: 0x040034E5 RID: 13541
		public float CutoffDepth;

		// Token: 0x040034E6 RID: 13542
		public float[,] Seeds;

		// Token: 0x040034E7 RID: 13543
		public float[,] Noise;

		// Token: 0x040034E8 RID: 13544
		public float[,] Areas;

		// Token: 0x040034E9 RID: 13545
		public float nAreas;

		// Token: 0x040034EA RID: 13546
		public Dictionary<float, List<FloatNoiseMapNode>> AreaNodes;
	}
}
