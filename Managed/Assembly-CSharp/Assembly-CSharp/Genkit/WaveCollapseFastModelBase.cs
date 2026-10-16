using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRL.Rules;

namespace Genkit
{
	// Token: 0x02000398 RID: 920
	public abstract class WaveCollapseFastModelBase
	{
		// Token: 0x060022AD RID: 8877 RVA: 0x000AFF92 File Offset: 0x000AE192
		protected WaveCollapseFastModelBase(int width, int height)
		{
			this.FMX = width;
			this.FMY = height;
		}

		// Token: 0x060022AE RID: 8878
		public abstract Color32[] GetResult();

		// Token: 0x060022AF RID: 8879 RVA: 0x000AFFB4 File Offset: 0x000AE1B4
		protected void Init()
		{
			Debug.Log("Init T=" + this.T.ToString());
			this.MaxT = Math.Max(this.T, WaveCollapseFastModelBase.INIT_SIZE);
			if (WaveCollapseFastModelBase.wave == null)
			{
				WaveCollapseFastModelBase.wave = new BitArray[18000];
			}
			if (WaveCollapseFastModelBase.compatible == null || WaveCollapseFastModelBase.compatible.Length < WaveCollapseFastModelBase.wave.Length * this.MaxT * 4)
			{
				WaveCollapseFastModelBase.compatible = new ushort[WaveCollapseFastModelBase.wave.Length * this.MaxT * 4];
			}
			for (int i = 0; i < WaveCollapseFastModelBase.wave.Length; i++)
			{
				if (WaveCollapseFastModelBase.wave[i] == null || WaveCollapseFastModelBase.wave[i].Length < this.T)
				{
					WaveCollapseFastModelBase.wave[i] = new BitArray(this.MaxT);
				}
			}
			if (WaveCollapseFastModelBase.weightLogWeights == null || WaveCollapseFastModelBase.weightLogWeights.Length < this.T)
			{
				Debug.Log("Reinitializing weightLogWeights to T=" + this.MaxT.ToString());
				WaveCollapseFastModelBase.weightLogWeights = new double[this.MaxT];
			}
			this.sumOfWeights = 0.0;
			this.sumOfWeightLogWeights = 0.0;
			for (int j = 0; j < this.T; j++)
			{
				WaveCollapseFastModelBase.weightLogWeights[j] = this.weights[j] * Math.Log(this.weights[j]);
				this.sumOfWeights += this.weights[j];
				this.sumOfWeightLogWeights += WaveCollapseFastModelBase.weightLogWeights[j];
			}
			this.startingEntropy = Math.Log(this.sumOfWeights) - this.sumOfWeightLogWeights / this.sumOfWeights;
			if (WaveCollapseFastModelBase.sumsOfOnes == null)
			{
				WaveCollapseFastModelBase.sumsOfOnes = new ushort[WaveCollapseFastModelBase.wave.Length];
			}
			if (WaveCollapseFastModelBase.sumsOfWeights == null)
			{
				WaveCollapseFastModelBase.sumsOfWeights = new double[WaveCollapseFastModelBase.wave.Length];
			}
			if (WaveCollapseFastModelBase.sumsOfWeightLogWeights == null)
			{
				WaveCollapseFastModelBase.sumsOfWeightLogWeights = new double[WaveCollapseFastModelBase.wave.Length];
			}
			if (WaveCollapseFastModelBase.entropies == null)
			{
				WaveCollapseFastModelBase.entropies = new double[WaveCollapseFastModelBase.wave.Length];
			}
			if (WaveCollapseFastModelBase.observed == null)
			{
				WaveCollapseFastModelBase.observed = new ushort[WaveCollapseFastModelBase.wave.Length];
			}
			if (WaveCollapseFastModelBase.stack == null || WaveCollapseFastModelBase.stack.Length < WaveCollapseFastModelBase.wave.Length * this.T)
			{
				Debug.Log("Reinitializing stack to T=" + this.MaxT.ToString());
				WaveCollapseFastModelBase.stack = new Tuple<int, ushort>[WaveCollapseFastModelBase.wave.Length * this.MaxT];
			}
			this.stacksize = 0;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000B0228 File Offset: 0x000AE428
		private bool? Observe()
		{
			double num = 1000.0;
			int num2 = -1;
			for (int i = 0; i < WaveCollapseFastModelBase.wave.Length; i++)
			{
				if (!this.OnBoundary(i % this.FMX, i / this.FMX))
				{
					int num3 = (int)WaveCollapseFastModelBase.sumsOfOnes[i];
					if (num3 == 0)
					{
						return new bool?(false);
					}
					double num4 = WaveCollapseFastModelBase.entropies[i];
					if (num3 > 1 && num4 <= num)
					{
						double num5 = 1E-06 * this.random.NextDouble();
						if (num4 + num5 < num)
						{
							num = num4 + num5;
							num2 = i;
						}
					}
				}
			}
			if (num2 == -1)
			{
				for (int j = 0; j < WaveCollapseFastModelBase.wave.Length; j++)
				{
					ushort num6 = 0;
					while ((int)num6 < this.T)
					{
						if (WaveCollapseFastModelBase.wave[j][(int)num6])
						{
							WaveCollapseFastModelBase.observed[j] = num6;
							break;
						}
						num6 += 1;
					}
				}
				return new bool?(true);
			}
			WaveCollapseFastModelBase.distribution.Clear();
			for (int k = 0; k < this.T; k++)
			{
				WaveCollapseFastModelBase.distribution.Add(WaveCollapseFastModelBase.wave[num2][k] ? this.weights[k] : 0.0);
			}
			int num7 = WaveCollapseFastModelBase.distribution.Random(this.random.NextDouble());
			BitArray bitArray = WaveCollapseFastModelBase.wave[num2];
			ushort num8 = 0;
			while ((int)num8 < this.T)
			{
				if (bitArray[(int)num8] != ((int)num8 == num7))
				{
					this.Ban(num2, num8);
				}
				num8 += 1;
			}
			return null;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000B03BC File Offset: 0x000AE5BC
		protected void Propagate()
		{
			while (this.stacksize > 0)
			{
				Tuple<int, ushort> tuple = WaveCollapseFastModelBase.stack[this.stacksize - 1];
				WaveCollapseFastModelBase.stack[this.stacksize - 1] = null;
				this.stacksize--;
				int item = tuple.Item1;
				int num = item % this.FMX;
				int num2 = item / this.FMX;
				for (int i = 0; i < 4; i++)
				{
					int num3 = WaveCollapseFastModelBase.DX[i];
					int num4 = WaveCollapseFastModelBase.DY[i];
					int num5 = num + num3;
					int num6 = num2 + num4;
					if (!this.OnBoundary(num5, num6))
					{
						if (num5 < 0)
						{
							num5 += this.FMX;
						}
						else if (num5 >= this.FMX)
						{
							num5 -= this.FMX;
						}
						if (num6 < 0)
						{
							num6 += this.FMY;
						}
						else if (num6 >= this.FMY)
						{
							num6 -= this.FMY;
						}
						int num7 = num5 + num6 * this.FMX;
						List<ushort> list = WaveCollapseFastModelBase.propagator[i][(int)tuple.Item2];
						int num8 = num7 * this.MaxT * 4;
						for (int j = 0; j < list.Count; j++)
						{
							ushort num9 = list[j];
							ushort[] array = WaveCollapseFastModelBase.compatible;
							int num10 = num8 + (int)(num9 * 4) + i;
							array[num10] -= 1;
							if (WaveCollapseFastModelBase.compatible[num8 + (int)(num9 * 4) + i] == 0)
							{
								this.Ban(num7, num9);
							}
						}
					}
				}
				WaveCollapseFastModelBase.tuplePool.Push(tuple);
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000B0538 File Offset: 0x000AE738
		public virtual bool Run(int seed, int limit)
		{
			this.Init();
			this.Clear();
			this.random = new global::System.Random(seed);
			int num = 0;
			while (num < limit || limit == 0)
			{
				bool? flag = this.Observe();
				if (flag != null)
				{
					return flag.Value;
				}
				this.Propagate();
				num++;
			}
			return true;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000B058C File Offset: 0x000AE78C
		protected void Ban(int i, ushort t)
		{
			WaveCollapseFastModelBase.wave[i][(int)t] = false;
			int num = i * this.MaxT * 4 + (int)(t * 4);
			for (int j = 0; j < 4; j++)
			{
				WaveCollapseFastModelBase.compatible[num + j] = 0;
			}
			if (WaveCollapseFastModelBase.tuplePool.Count > 0)
			{
				WaveCollapseFastModelBase.stack[this.stacksize] = WaveCollapseFastModelBase.tuplePool.Pop();
				WaveCollapseFastModelBase.stack[this.stacksize] = new Tuple<int, ushort>(i, t);
			}
			else
			{
				WaveCollapseFastModelBase.stack[this.stacksize] = new Tuple<int, ushort>(i, t);
			}
			this.stacksize++;
			double num2 = WaveCollapseFastModelBase.sumsOfWeights[i];
			WaveCollapseFastModelBase.entropies[i] += WaveCollapseFastModelBase.sumsOfWeightLogWeights[i] / num2 - Math.Log(num2);
			ushort[] array = WaveCollapseFastModelBase.sumsOfOnes;
			array[i] -= 1;
			WaveCollapseFastModelBase.sumsOfWeights[i] -= this.weights[(int)t];
			WaveCollapseFastModelBase.sumsOfWeightLogWeights[i] -= WaveCollapseFastModelBase.weightLogWeights[(int)t];
			num2 = WaveCollapseFastModelBase.sumsOfWeights[i];
			WaveCollapseFastModelBase.entropies[i] -= WaveCollapseFastModelBase.sumsOfWeightLogWeights[i] / num2 - Math.Log(num2);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000B06B4 File Offset: 0x000AE8B4
		protected virtual void Clear()
		{
			for (int i = 0; i < WaveCollapseFastModelBase.wave.Length; i++)
			{
				for (int j = 0; j < this.T; j++)
				{
					WaveCollapseFastModelBase.wave[i][j] = true;
					for (int k = 0; k < 4; k++)
					{
						WaveCollapseFastModelBase.compatible[i * this.MaxT * 4 + j * 4 + k] = (ushort)WaveCollapseFastModelBase.propagator[WaveCollapseFastModelBase.opposite[k]][j].Count;
					}
				}
				WaveCollapseFastModelBase.sumsOfOnes[i] = (ushort)this.weights.Length;
				WaveCollapseFastModelBase.sumsOfWeights[i] = this.sumOfWeights;
				WaveCollapseFastModelBase.sumsOfWeightLogWeights[i] = this.sumOfWeightLogWeights;
				WaveCollapseFastModelBase.entropies[i] = this.startingEntropy;
				WaveCollapseFastModelBase.observed[i] = 0;
			}
		}

		// Token: 0x060022B5 RID: 8885
		protected abstract bool OnBoundary(int x, int y);

		// Token: 0x060022B6 RID: 8886 RVA: 0x000B0770 File Offset: 0x000AE970
		// Note: this type is marked as 'beforefieldinit'.
		static WaveCollapseFastModelBase()
		{
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			WaveCollapseFastModelBase.DX = array;
			WaveCollapseFastModelBase.DY = new int[] { 0, 1, 0, -1 };
			WaveCollapseFastModelBase.opposite = new int[] { 2, 3, 0, 1 };
		}

		// Token: 0x04001167 RID: 4455
		public static readonly int INIT_SIZE = 350;

		// Token: 0x04001168 RID: 4456
		protected static BitArray[] wave;

		// Token: 0x04001169 RID: 4457
		protected static List<ushort>[][] propagator;

		// Token: 0x0400116A RID: 4458
		private static ushort[] compatible;

		// Token: 0x0400116B RID: 4459
		private static Tuple<int, ushort>[] stack;

		// Token: 0x0400116C RID: 4460
		private int stacksize;

		// Token: 0x0400116D RID: 4461
		protected global::System.Random random = Stat.Rnd5;

		// Token: 0x0400116E RID: 4462
		public int FMX;

		// Token: 0x0400116F RID: 4463
		public int FMY;

		// Token: 0x04001170 RID: 4464
		public int T;

		// Token: 0x04001171 RID: 4465
		private int MaxT;

		// Token: 0x04001172 RID: 4466
		protected bool periodic;

		// Token: 0x04001173 RID: 4467
		protected double[] weights;

		// Token: 0x04001174 RID: 4468
		private static double[] weightLogWeights;

		// Token: 0x04001175 RID: 4469
		protected double sumOfWeights;

		// Token: 0x04001176 RID: 4470
		protected double sumOfWeightLogWeights;

		// Token: 0x04001177 RID: 4471
		protected double startingEntropy;

		// Token: 0x04001178 RID: 4472
		protected static ushort[] observed;

		// Token: 0x04001179 RID: 4473
		private static ushort[] sumsOfOnes;

		// Token: 0x0400117A RID: 4474
		protected static double[] sumsOfWeights;

		// Token: 0x0400117B RID: 4475
		protected static double[] sumsOfWeightLogWeights;

		// Token: 0x0400117C RID: 4476
		protected static double[] entropies;

		// Token: 0x0400117D RID: 4477
		private static List<double> distribution = new List<double>(256);

		// Token: 0x0400117E RID: 4478
		private static Stack<Tuple<int, ushort>> tuplePool = new Stack<Tuple<int, ushort>>();

		// Token: 0x0400117F RID: 4479
		protected static int[] DX;

		// Token: 0x04001180 RID: 4480
		protected static int[] DY;

		// Token: 0x04001181 RID: 4481
		private static int[] opposite;
	}
}
