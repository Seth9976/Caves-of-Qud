using System;
using System.Collections.Generic;
using UnityEngine;

namespace Genkit
{
	// Token: 0x02000397 RID: 919
	public class WaveCollapseFastModel : WaveCollapseFastModelBase
	{
		// Token: 0x060022A2 RID: 8866 RVA: 0x000AF070 File Offset: 0x000AD270
		public void UpdateSample(string templateName, int N, bool periodicInput, bool periodicOutput, int symmetry, int ground)
		{
			if (!WaveCollapseTools.waveTemplates.ContainsKey(templateName.ToLower()))
			{
				Debug.LogError("Unknown wave template: " + templateName.ToLower());
				return;
			}
			WaveTemplateEntry waveTemplateEntry = WaveCollapseTools.waveTemplates[templateName];
			this.UpdateSample(waveTemplateEntry, N, periodicInput, periodicOutput, symmetry, ground);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000AF0C0 File Offset: 0x000AD2C0
		public void UpdateSample(WaveTemplateEntry entry, int N, bool periodicInput, bool periodicOutput, int symmetry, int ground)
		{
			this.N = N;
			this.periodic = periodicOutput;
			int SMX = entry.width;
			int SMY = entry.height;
			byte[,] sample = new byte[SMX, SMY];
			this.colors = new List<Color32>();
			for (int i = 0; i < SMY; i++)
			{
				for (int j = 0; j < SMX; j++)
				{
					Color color = entry.pixels[j + (SMY - 1 - i) * SMX];
					int num = 0;
					using (List<Color32>.Enumerator enumerator = this.colors.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == color)
							{
								break;
							}
							num++;
						}
					}
					if (num == this.colors.Count)
					{
						this.colors.Add(color);
					}
					sample[j, i] = (byte)num;
				}
			}
			int C = this.colors.Count;
			long W = WaveCollapseTools.Power(C, N * N);
			Func<Func<int, int, byte>, byte[]> pattern = delegate(Func<int, int, byte> f)
			{
				byte[] array2 = new byte[N * N];
				for (int num10 = 0; num10 < N; num10++)
				{
					for (int num11 = 0; num11 < N; num11++)
					{
						array2[num11 + num10 * N] = f(num11, num10);
					}
				}
				return array2;
			};
			Func<int, int, byte[]> func = (int x, int y) => pattern((int dx, int dy) => sample[(x + dx) % SMX, (y + dy) % SMY]);
			Func<byte[], byte[]> func2 = (byte[] p) => pattern((int x, int y) => p[N - 1 - y + x * N]);
			Func<byte[], byte[]> func3 = (byte[] p) => pattern((int x, int y) => p[N - 1 - x + y * N]);
			Func<byte[], long> func4 = delegate(byte[] p)
			{
				long num12 = 0L;
				long num13 = 1L;
				for (int num14 = 0; num14 < p.Length; num14++)
				{
					num12 += (long)((ulong)p[p.Length - 1 - num14] * (ulong)num13);
					num13 *= (long)C;
				}
				return num12;
			};
			Func<long, byte[]> func5 = delegate(long ind)
			{
				long num15 = ind;
				long num16 = W;
				byte[] array3 = new byte[N * N];
				for (int num17 = 0; num17 < array3.Length; num17++)
				{
					num16 /= (long)C;
					int num18 = 0;
					while (num15 >= num16)
					{
						num15 -= num16;
						num18++;
					}
					array3[num17] = (byte)num18;
				}
				return array3;
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
			this.T = dictionary.Count;
			Debug.Log(string.Concat(new string[]
			{
				"*** T= ",
				this.T.ToString(),
				" using sample: ",
				entry.ToString(),
				" pi:",
				periodicInput.ToString(),
				" po:",
				periodicOutput.ToString(),
				" s:",
				symmetry.ToString(),
				" g:",
				ground.ToString()
			}));
			this.ground = (ushort)((ground + this.T) % this.T);
			if (WaveCollapseFastModel.patterns == null || WaveCollapseFastModel.patterns.Length < this.T)
			{
				WaveCollapseFastModel.patterns = new byte[this.T][];
			}
			this.weights = new double[this.T];
			int num5 = 0;
			foreach (long num6 in list)
			{
				WaveCollapseFastModel.patterns[num5] = func5(num6);
				this.weights[num5] = (double)dictionary[num6];
				num5++;
			}
			List<ushort> list2 = new List<ushort>();
			if (WaveCollapseFastModelBase.propagator == null)
			{
				WaveCollapseFastModelBase.propagator = new List<ushort>[4][];
			}
			int num7 = Math.Max(WaveCollapseFastModelBase.INIT_SIZE, this.T);
			for (int n = 0; n < 4; n++)
			{
				if (WaveCollapseFastModelBase.propagator[n] == null || WaveCollapseFastModelBase.propagator[n].Length < this.T)
				{
					WaveCollapseFastModelBase.propagator[n] = new List<ushort>[num7];
				}
				for (int num8 = 0; num8 < this.T; num8++)
				{
					list2.Clear();
					ushort num9 = 0;
					while ((int)num9 < this.T)
					{
						if (this.agrees(WaveCollapseFastModel.patterns[num8], WaveCollapseFastModel.patterns[(int)num9], WaveCollapseFastModelBase.DX[n], WaveCollapseFastModelBase.DY[n]))
						{
							list2.Add(num9);
						}
						num9 += 1;
					}
					if (WaveCollapseFastModelBase.propagator[n][num8] == null)
					{
						WaveCollapseFastModelBase.propagator[n][num8] = new List<ushort>(12);
					}
					WaveCollapseFastModelBase.propagator[n][num8].Clear();
					WaveCollapseFastModelBase.propagator[n][num8].AddRange(list2);
				}
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000AF62C File Offset: 0x000AD82C
		public WaveCollapseFastModel(WaveTemplateEntry entry, int N, int width, int height, bool periodicInput, bool periodicOutput, int symmetry, int ground)
			: base(width, height)
		{
			this.UpdateSample(entry, N, periodicInput, periodicOutput, symmetry, ground);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000AF647 File Offset: 0x000AD847
		public WaveCollapseFastModel(string templateName, int N, int width, int height, bool periodicInput, bool periodicOutput, int symmetry, int ground)
			: base(width, height)
		{
			this.UpdateSample(templateName, N, periodicInput, periodicOutput, symmetry, ground);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000AF664 File Offset: 0x000AD864
		private bool agrees(byte[] p1, byte[] p2, int dx, int dy)
		{
			int num = ((dx < 0) ? 0 : dx);
			int num2 = ((dx < 0) ? (dx + this.N) : this.N);
			int num3 = ((dy < 0) ? 0 : dy);
			int num4 = ((dy < 0) ? (dy + this.N) : this.N);
			for (int i = num3; i < num4; i++)
			{
				for (int j = num; j < num2; j++)
				{
					if (p1[j + this.N * i] != p2[j - dx + this.N * (i - dy)])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000AF6EF File Offset: 0x000AD8EF
		public override Color32[] GetResult()
		{
			return this.results;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000AF6F8 File Offset: 0x000AD8F8
		public void ClearColors(string clearColors, string clearstyle = "border1")
		{
			if (WaveCollapseFastModelBase.wave == null)
			{
				return;
			}
			if (this.mask == null)
			{
				this.mask = new bool[this.FMX * this.FMY];
			}
			Color32[] result = this.GetResult();
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
				for (int k = 0; k < this.FMX; k++)
				{
					Color32 color = result[k + j * this.FMX];
					this.mask[k + j * this.FMX] = true;
					if (clearstyle == "border1")
					{
						if (list.Contains(color))
						{
							if (k != this.FMX - 1)
							{
								int num = k + 1 + j * this.FMX;
								int num2 = result.Length;
							}
							if ((k == 0 || WaveCollapseTools.equals(result[k - 1 + j * this.FMX], color)) && (k == this.FMX - 1 || WaveCollapseTools.equals(result[k + 1 + j * this.FMX], color)) && (j == 0 || WaveCollapseTools.equals(result[k + (j - 1) * this.FMX], color)) && (j == this.FMY - 1 || WaveCollapseTools.equals(result[k + (j + 1) * this.FMX], color)))
							{
								this.mask[k + j * this.FMX] = false;
							}
						}
					}
					else if (clearstyle == "border1.5")
					{
						if (list.Contains(color) && (k == 0 || WaveCollapseTools.equals(result[k - 1 + j * this.FMX], color)) && (k == this.FMX - 1 || WaveCollapseTools.equals(result[k + 1 + j * this.FMX], color)) && (j == 0 || WaveCollapseTools.equals(result[k + (j - 1) * this.FMX], color)) && (j == this.FMY - 1 || WaveCollapseTools.equals(result[k + (j + 1) * this.FMX], color)) && (k <= 0 || j <= 0 || WaveCollapseTools.equals(result[k - 1 + (j - 1) * this.FMX], color)) && (k <= 0 || j >= this.FMY - 1 || WaveCollapseTools.equals(result[k - 1 + (j + 1) * this.FMX], color)) && (k >= this.FMX - 1 || j <= 0 || WaveCollapseTools.equals(result[k + 1 + (j - 1) * this.FMX], color)) && (k >= this.FMX - 1 || j >= this.FMY - 1 || WaveCollapseTools.equals(result[k + 1 + (j + 1) * this.FMX], color)))
						{
							this.mask[k + j * this.FMX] = false;
						}
					}
					else if (list.Contains(color))
					{
						this.mask[k + j * this.FMX] = false;
					}
				}
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000AFB61 File Offset: 0x000ADD61
		public override bool Run(int seed, int limit)
		{
			bool flag = base.Run(seed, limit);
			this.GenResult();
			return flag;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000AFB74 File Offset: 0x000ADD74
		private void GenResult()
		{
			if (this.results == null)
			{
				this.results = new Color32[this.FMX * this.FMY];
			}
			if (WaveCollapseFastModelBase.observed != null)
			{
				for (int i = 0; i < this.FMY; i++)
				{
					int num = ((i < this.FMY - this.N + 1) ? 0 : (this.N - 1));
					for (int j = 0; j < this.FMX; j++)
					{
						int num2 = ((j < this.FMX - this.N + 1) ? 0 : (this.N - 1));
						int num3 = j - num2 + (i - num) * this.FMX;
						int num4 = num2 + num * this.N;
						Color32 color = new Color32(byte.MaxValue, 0, byte.MaxValue, byte.MaxValue);
						try
						{
							color = this.colors[(int)WaveCollapseFastModel.patterns[(int)WaveCollapseFastModelBase.observed[num3]][num4]];
						}
						catch
						{
						}
						if (this.mask == null || !this.mask[j + i * this.FMX])
						{
							this.results[j + i * this.FMX] = new Color32(color.r, color.g, color.b, byte.MaxValue);
						}
						else
						{
							this.results[j + i * this.FMX] = this.results[j + i * this.FMX];
						}
					}
				}
				return;
			}
			for (int k = 0; k < WaveCollapseFastModelBase.wave.Length; k++)
			{
				int num5 = 0;
				float num6 = 0f;
				float num7 = 0f;
				float num8 = 0f;
				int num9 = k % this.FMX;
				int num10 = k / this.FMX;
				for (int l = 0; l < this.N; l++)
				{
					for (int m = 0; m < this.N; m++)
					{
						int num11 = num9 - m;
						if (num11 < 0)
						{
							num11 += this.FMX;
						}
						int num12 = num10 - l;
						if (num12 < 0)
						{
							num12 += this.FMY;
						}
						int num13 = num11 + num12 * this.FMX;
						if (!this.OnBoundary(num11, num12))
						{
							for (int n = 0; n < this.T; n++)
							{
								if (WaveCollapseFastModelBase.wave[num13][n])
								{
									num5++;
									Color color2 = this.colors[(int)WaveCollapseFastModel.patterns[n][m + l * this.N]];
									num6 += color2.r;
									num7 += color2.g;
									num8 += color2.b;
								}
							}
						}
					}
				}
				if (this.mask == null || !this.mask[num9 + num10 * this.FMX])
				{
					this.results[num9 + num10 * this.FMX] = new Color32((byte)(num6 / (float)num5 * 255f), (byte)(num7 / (float)num5 * 255f), (byte)(num8 / (float)num5 * 255f), byte.MaxValue);
				}
				else
				{
					this.results[num9 + num10 * this.FMX] = this.results[num9 + num10 * this.FMX];
				}
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000AFECC File Offset: 0x000AE0CC
		protected override bool OnBoundary(int x, int y)
		{
			return !this.periodic && (x + this.N > this.FMX || y + this.N > this.FMY || x < 0 || y < 0);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000AFF04 File Offset: 0x000AE104
		protected override void Clear()
		{
			base.Clear();
			if (this.ground != 0)
			{
				for (int i = 0; i < this.FMX; i++)
				{
					ushort num = 0;
					while ((int)num < this.T)
					{
						if (num != this.ground)
						{
							base.Ban(i + (this.FMY - 1) * this.FMX, num);
						}
						num += 1;
					}
					for (int j = 0; j < this.FMY - 1; j++)
					{
						base.Ban(i + j * this.FMX, this.ground);
					}
				}
				base.Propagate();
			}
		}

		// Token: 0x04001161 RID: 4449
		private int N;

		// Token: 0x04001162 RID: 4450
		private static byte[][] patterns;

		// Token: 0x04001163 RID: 4451
		private List<Color32> colors;

		// Token: 0x04001164 RID: 4452
		private ushort ground;

		// Token: 0x04001165 RID: 4453
		public Color32[] results;

		// Token: 0x04001166 RID: 4454
		public bool[] mask;
	}
}
