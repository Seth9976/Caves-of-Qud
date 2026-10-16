using System;
using System.Collections.Generic;
using UnityEngine;

namespace Genkit
{
	// Token: 0x02000391 RID: 913
	public class MultipassModel : WaveCollapseModelBase
	{
		// Token: 0x06002274 RID: 8820 RVA: 0x000ACCE0 File Offset: 0x000AAEE0
		public void ClearColors(string clearColors, string clearstyle = "border1")
		{
			List<Color32> list = new List<Color32>();
			for (int i = 0; i < clearColors.Length; i++)
			{
				if (clearColors[i] == 'W')
				{
					list.Add(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
				}
				if (clearColors[i] == 'w')
				{
					list.Add(new Color32(128, 128, 128, byte.MaxValue));
				}
				if (clearColors[i] == 'R')
				{
					list.Add(new Color32(byte.MaxValue, 0, 0, byte.MaxValue));
				}
				if (clearColors[i] == 'K')
				{
					list.Add(new Color32(0, 0, 0, byte.MaxValue));
				}
				if (clearColors[i] == 'B')
				{
					list.Add(new Color32(0, 0, byte.MaxValue, byte.MaxValue));
				}
				if (clearColors[i] == 'G')
				{
					list.Add(new Color32(0, byte.MaxValue, 0, byte.MaxValue));
				}
				if (clearColors[i] == 'M')
				{
					list.Add(new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue));
				}
				if (clearColors[i] == 'Y')
				{
					list.Add(new Color32(byte.MaxValue, byte.MaxValue, 0, byte.MaxValue));
				}
			}
			for (int j = 0; j < this.FMY; j++)
			{
				int num = ((j < this.FMY - this.N + 1) ? 0 : (this.N - 1));
				for (int k = 0; k < this.FMX; k++)
				{
					int num2 = ((k < this.FMX - this.N + 1) ? 0 : (this.N - 1));
					Color32 color = this.colors[(int)this.patterns[this.observed[k - num2][j - num]][num2 + num * this.N]];
					if (clearstyle == "border1")
					{
						if (list.Contains(color) && (k == 0 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k - 1][j]][num2 + num * this.N]], color)) && (k == this.FMX - 1 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k + 1][j]][num2 + num * this.N]], color)) && (j == 0 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k][j - 1]][num2 + num * this.N]], color)) && (j == this.FMY - 1 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k][j + 1]][num2 + num * this.N]], color)))
						{
							this.wave[k][j] = null;
						}
					}
					else if (clearstyle == "border1.5")
					{
						if (list.Contains(color) && (k == 0 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k - 1][j]][num2 + num * this.N]], color)) && (k == this.FMX - 1 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k + 1][j]][num2 + num * this.N]], color)) && (j == 0 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k][j - 1]][num2 + num * this.N]], color)) && (j == this.FMY - 1 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k][j + 1]][num2 + num * this.N]], color)) && (k <= 0 || j <= 0 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k - 1][j - 1]][num2 + num * this.N]], color)) && (k <= 0 || j >= this.FMY - 1 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k - 1][j + 1]][num2 + num * this.N]], color)) && (k >= this.FMX - 1 || j <= 0 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k + 1][j - 1]][num2 + num * this.N]], color)) && (k >= this.FMX - 1 || j >= this.FMY - 1 || WaveCollapseTools.equals(this.colors[(int)this.patterns[this.observed[k + 1][j + 1]][num2 + num * this.N]], color)))
						{
							this.wave[k][j] = null;
						}
					}
					else if (list.Contains(color))
					{
						this.wave[k][j] = null;
					}
				}
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000AD250 File Offset: 0x000AB450
		public void UpdateSample(string entry, int N, bool periodicInput, bool periodicOutput, int symmetry, int ground)
		{
			if (!WaveCollapseTools.waveTemplates.ContainsKey(entry.ToLower()))
			{
				Debug.LogError("Unknown wave template: " + entry.ToLower());
				return;
			}
			this.UpdateSample(WaveCollapseTools.waveTemplates[entry.ToLower()], N, periodicInput, periodicOutput, symmetry, ground);
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		public void UpdateSample(WaveTemplateEntry entry, int N, bool periodicInput, bool periodicOutput, int symmetry, int ground)
		{
			this.N = N;
			int SMX = entry.width;
			int SMY = entry.height;
			byte[,] sample = new byte[SMX, SMY];
			this.bClear = this.colors == null;
			if (this.colors == null)
			{
				this.colors = new Color32[256];
				this.colorCount = 0;
			}
			for (int i = 0; i < SMY; i++)
			{
				for (int j = 0; j < SMX; j++)
				{
					Color32 color = entry.pixels[j + (SMY - 1 - i) * SMX];
					byte b = 0;
					int num = 0;
					while (num < this.colorCount && !WaveCollapseTools.equals(this.colors[num], color))
					{
						b += 1;
						num++;
					}
					if ((int)b >= this.colorCount)
					{
						this.colors[(int)b] = color;
						this.colorCount++;
					}
					sample[j, i] = b;
				}
			}
			int C = this.colorCount;
			long W = WaveCollapseTools.Power(C, N * N);
			Func<Func<int, int, byte>, byte[]> pattern = delegate(Func<int, int, byte> f)
			{
				byte[] array4 = new byte[N * N];
				for (int num17 = 0; num17 < N; num17++)
				{
					for (int num18 = 0; num18 < N; num18++)
					{
						array4[num18 + num17 * N] = f(num18, num17);
					}
				}
				return array4;
			};
			Func<int, int, byte[]> func = (int x, int y) => pattern((int dx, int dy) => sample[(x + dx) % SMX, (y + dy) % SMY]);
			Func<byte[], byte[]> func2 = (byte[] p) => pattern((int x, int y) => p[N - 1 - y + x * N]);
			Func<byte[], byte[]> func3 = (byte[] p) => pattern((int x, int y) => p[N - 1 - x + y * N]);
			Func<byte[], long> func4 = delegate(byte[] p)
			{
				long num19 = 0L;
				long num20 = 1L;
				for (int num21 = 0; num21 < p.Length; num21++)
				{
					num19 += (long)((ulong)p[p.Length - 1 - num21] * (ulong)num20);
					num20 *= (long)C;
				}
				return num19;
			};
			Func<long, byte[]> func5 = delegate(long ind)
			{
				long num22 = ind;
				long num23 = W;
				byte[] array5 = new byte[N * N];
				for (int num24 = 0; num24 < array5.Length; num24++)
				{
					num23 /= (long)C;
					int num25 = 0;
					while (num22 >= num23)
					{
						num22 -= num23;
						num25++;
					}
					array5[num24] = (byte)num25;
				}
				return array5;
			};
			Dictionary<long, int> dictionary = new Dictionary<long, int>();
			List<long> list = new List<long>();
			for (int k = 0; k < (periodicInput ? SMY : (SMY - N + 1)); k++)
			{
				for (int l = 0; l < (periodicInput ? SMX : (SMX - N + 1)); l++)
				{
					byte[][] array = new byte[8][];
					array[0] = func(l, k);
					array[1] = func3(array[0]);
					array[2] = func2(array[0]);
					array[3] = func3(array[2]);
					array[4] = func2(array[2]);
					array[5] = func3(array[4]);
					array[6] = func2(array[4]);
					array[7] = func3(array[6]);
					for (int m = 0; m < symmetry; m++)
					{
						long num2 = func4(array[m]);
						if (dictionary.ContainsKey(num2))
						{
							Dictionary<long, int> dictionary2 = dictionary;
							long num3 = num2;
							int num4 = dictionary2[num3];
							dictionary2[num3] = num4 + 1;
						}
						else
						{
							dictionary.Add(num2, 1);
							list.Add(num2);
						}
					}
				}
			}
			this.TStart = this.T;
			this.T += dictionary.Count;
			this.ground = (ground + this.T) % this.T;
			byte[][] array2 = this.patterns;
			this.patterns = new byte[this.T][];
			for (int n = 0; n < this.TStart; n++)
			{
				this.patterns[n] = array2[n];
			}
			double[] stationary = this.stationary;
			this.stationary = new double[this.T];
			for (int num5 = 0; num5 < this.TStart; num5++)
			{
				this.stationary[num5] = stationary[num5];
			}
			this.propagator = new int[2 * N - 1][][][];
			int num6 = this.T - dictionary.Count;
			foreach (long num7 in list)
			{
				this.patterns[num6] = func5(num7);
				this.stationary[num6] = (double)dictionary[num7];
				num6++;
			}
			for (int num8 = 0; num8 < this.FMX; num8++)
			{
				for (int num9 = 0; num9 < this.FMY; num9++)
				{
					if (this.wave[num8][num9] == null)
					{
						this.wave[num8][num9] = new bool[this.T];
						for (int num10 = 0; num10 < this.T; num10++)
						{
							this.wave[num8][num9][num10] = num10 > this.TStart;
						}
					}
					else if (this.wave[num8][num9].GetUpperBound(0) < this.T)
					{
						int upperBound = this.wave[num8][num9].GetUpperBound(0);
						bool[] array3 = new bool[this.T];
						for (int num11 = 0; num11 < this.T; num11++)
						{
							if (num11 <= upperBound)
							{
								array3[num11] = this.wave[num8][num9][num11];
							}
							else
							{
								array3[num11] = false;
							}
						}
						this.wave[num8][num9] = array3;
					}
				}
			}
			Func<byte[], byte[], int, int, bool> func6 = delegate(byte[] p1, byte[] p2, int dx, int dy)
			{
				int num26 = ((dx < 0) ? 0 : dx);
				int num27 = ((dx < 0) ? (dx + N) : N);
				int num28 = ((dy < 0) ? 0 : dy);
				int num29 = ((dy < 0) ? (dy + N) : N);
				for (int num30 = num28; num30 < num29; num30++)
				{
					for (int num31 = num26; num31 < num27; num31++)
					{
						if (p1[num31 + N * num30] != p2[num31 - dx + N * (num30 - dy)])
						{
							return false;
						}
					}
				}
				return true;
			};
			for (int num12 = 0; num12 < 2 * N - 1; num12++)
			{
				this.propagator[num12] = new int[2 * N - 1][][];
				for (int num13 = 0; num13 < 2 * N - 1; num13++)
				{
					this.propagator[num12][num13] = new int[this.T][];
					for (int num14 = 0; num14 < this.T; num14++)
					{
						List<int> list2 = new List<int>();
						for (int num15 = 0; num15 < this.T; num15++)
						{
							if (func6(this.patterns[num14], this.patterns[num15], num12 - N + 1, num13 - N + 1))
							{
								list2.Add(num15);
							}
						}
						this.propagator[num12][num13][num14] = new int[list2.Count];
						for (int num16 = 0; num16 < list2.Count; num16++)
						{
							this.propagator[num12][num13][num14][num16] = list2[num16];
						}
					}
				}
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000AD92C File Offset: 0x000ABB2C
		public MultipassModel(string entry, int N, int width, int height, bool periodicInput, bool periodicOutput, int symmetry, int ground)
			: base(width, height)
		{
			this.periodic = periodicOutput;
			if (!WaveCollapseTools.waveTemplates.ContainsKey(entry))
			{
				Debug.LogError("Unknown WFC template:" + entry);
			}
			this.UpdateSample(WaveCollapseTools.waveTemplates[entry], N, periodicInput, periodicOutput, symmetry, ground);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000AD981 File Offset: 0x000ABB81
		public MultipassModel(WaveTemplateEntry entry, int N, int width, int height, bool periodicInput, bool periodicOutput, int symmetry, int ground)
			: base(width, height)
		{
			this.periodic = periodicOutput;
			this.UpdateSample(entry, N, periodicInput, periodicOutput, symmetry, ground);
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000AD9A4 File Offset: 0x000ABBA4
		protected override bool OnBoundary(int x, int y)
		{
			return !this.periodic && (x + this.N > this.FMX || y + this.N > this.FMY);
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000AD9D4 File Offset: 0x000ABBD4
		protected override bool Propagate()
		{
			bool flag = false;
			for (int i = 0; i < this.FMX; i++)
			{
				for (int j = 0; j < this.FMY; j++)
				{
					if (this.changes[i][j])
					{
						this.changes[i][j] = false;
						for (int k = -this.N + 1; k < this.N; k++)
						{
							for (int l = -this.N + 1; l < this.N; l++)
							{
								int num = i + k;
								if (num < 0)
								{
									num += this.FMX;
								}
								else if (num >= this.FMX)
								{
									num -= this.FMX;
								}
								int num2 = j + l;
								if (num2 < 0)
								{
									num2 += this.FMY;
								}
								else if (num2 >= this.FMY)
								{
									num2 -= this.FMY;
								}
								if (this.periodic || (num + this.N <= this.FMX && num2 + this.N <= this.FMY))
								{
									bool[] array = this.wave[i][j];
									bool[] array2 = this.wave[num][num2];
									int[][] array3 = this.propagator[this.N - 1 - k][this.N - 1 - l];
									for (int m = this.TStart; m < this.T; m++)
									{
										if (array2[m])
										{
											bool flag2 = false;
											int[] array4 = array3[m];
											int num3 = 0;
											while (num3 < array4.Length && !flag2)
											{
												flag2 = array[array4[num3]];
												num3++;
											}
											if (!flag2)
											{
												this.changes[num][num2] = true;
												flag = true;
												array2[m] = false;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000ADB94 File Offset: 0x000ABD94
		public override Color32[] GetResult()
		{
			Color32[] array = new Color32[this.FMX * this.FMY];
			if (this.observed != null)
			{
				for (int i = 0; i < this.FMY; i++)
				{
					int num = ((i < this.FMY - this.N + 1) ? 0 : (this.N - 1));
					for (int j = 0; j < this.FMX; j++)
					{
						int num2 = ((j < this.FMX - this.N + 1) ? 0 : (this.N - 1));
						Color32 color = this.colors[(int)this.patterns[this.observed[j - num2][i - num]][num2 + num * this.N]];
						if (this.wave[j][i] == null)
						{
							array[j + i * this.FMX] = new Color32(byte.MaxValue, 0, byte.MaxValue, 128);
						}
						else
						{
							array[j + i * this.FMX] = new Color32(color.r, color.g, color.b, byte.MaxValue);
						}
					}
				}
			}
			else
			{
				for (int k = 0; k < this.FMY; k++)
				{
					for (int l = 0; l < this.FMX; l++)
					{
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						for (int m = 0; m < this.N; m++)
						{
							for (int n = 0; n < this.N; n++)
							{
								int num7 = l - n;
								if (num7 < 0)
								{
									num7 += this.FMX;
								}
								int num8 = k - m;
								if (num8 < 0)
								{
									num8 += this.FMY;
								}
								if (!this.OnBoundary(num7, num8))
								{
									for (int num9 = 0; num9 < this.T; num9++)
									{
										if (this.wave[num7][num8][num9])
										{
											num3++;
											Color32 color2 = this.colors[(int)this.patterns[num9][n + m * this.N]];
											num4 += (int)color2.r;
											num5 += (int)color2.g;
											num6 += (int)color2.b;
										}
									}
								}
							}
						}
						array[l + k * this.FMX] = new Color32((byte)(num4 / num3 * 255), (byte)(num5 / num3 * 255), (byte)(num6 / num3 * 255), byte.MaxValue);
					}
				}
			}
			return array;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000ADE28 File Offset: 0x000AC028
		protected override void Clear()
		{
			base.Clear();
			if (this.ground != 0)
			{
				for (int i = 0; i < this.FMX; i++)
				{
					if (this.bClear)
					{
						for (int j = 0; j < this.T; j++)
						{
							if (j != this.ground)
							{
								this.wave[i][this.FMY - 1][j] = false;
							}
						}
					}
					if (this.bClear)
					{
						this.changes[i][this.FMY - 1] = true;
					}
					for (int k = 0; k < this.FMY - 1; k++)
					{
						this.wave[i][k][this.ground] = false;
						if (this.bClear)
						{
							this.changes[i][k] = true;
						}
					}
				}
				if (this.bClear)
				{
					while (this.Propagate())
					{
					}
				}
			}
		}

		// Token: 0x0400113C RID: 4412
		private int[][][][] propagator;

		// Token: 0x0400113D RID: 4413
		private int N;

		// Token: 0x0400113E RID: 4414
		private int TStart;

		// Token: 0x0400113F RID: 4415
		private byte[][] patterns;

		// Token: 0x04001140 RID: 4416
		private Color32[] colors;

		// Token: 0x04001141 RID: 4417
		private int colorCount;

		// Token: 0x04001142 RID: 4418
		private int ground;
	}
}
