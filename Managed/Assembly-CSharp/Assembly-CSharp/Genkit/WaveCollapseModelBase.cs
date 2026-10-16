using System;
using UnityEngine;
using XRL.Rules;

namespace Genkit
{
	// Token: 0x02000392 RID: 914
	public abstract class WaveCollapseModelBase
	{
		// Token: 0x0600227D RID: 8829 RVA: 0x000ADEF8 File Offset: 0x000AC0F8
		protected WaveCollapseModelBase(int width, int height)
		{
			this.FMX = width;
			this.FMY = height;
			this.wave = new bool[this.FMX][][];
			this.changes = new bool[this.FMX][];
			for (int i = 0; i < this.FMX; i++)
			{
				this.wave[i] = new bool[this.FMY][];
				this.changes[i] = new bool[this.FMY];
			}
		}

		// Token: 0x0600227E RID: 8830
		protected abstract bool Propagate();

		// Token: 0x0600227F RID: 8831 RVA: 0x000ADF84 File Offset: 0x000AC184
		private bool Observe()
		{
			double num = 1000.0;
			int num2 = -1;
			int num3 = -1;
			for (int i = 0; i < this.FMX; i++)
			{
				for (int j = 0; j < this.FMY; j++)
				{
					if (!this.OnBoundary(i, j))
					{
						bool[] array = this.wave[i][j];
						int num4 = 0;
						double num5 = 0.0;
						for (int k = 0; k < this.T; k++)
						{
							if (array[k])
							{
								num4++;
								num5 += this.stationary[k];
							}
						}
						if (num5 != 0.0)
						{
							double num6 = 1E-06 * this.random.NextDouble();
							double num7;
							if (num4 == 1)
							{
								num7 = 0.0;
							}
							else if (num4 == this.T)
							{
								num7 = this.logT;
							}
							else
							{
								double num8 = 0.0;
								double num9 = Math.Log(num5);
								for (int l = 0; l < this.T; l++)
								{
									if (array[l])
									{
										num8 += this.stationary[l] * this.logProb[l];
									}
								}
								num7 = num9 - num8 / num5;
							}
							if (num7 > 0.0 && num7 + num6 < num)
							{
								num = num7 + num6;
								num2 = i;
								num3 = j;
							}
						}
					}
				}
			}
			if (num2 == -1 && num3 == -1)
			{
				bool flag = false;
				if (this.observed == null)
				{
					flag = true;
				}
				if (flag)
				{
					this.observed = new int[this.FMX][];
				}
				for (int m = 0; m < this.FMX; m++)
				{
					if (flag)
					{
						this.observed[m] = new int[this.FMY];
					}
					for (int n = 0; n < this.FMY; n++)
					{
						for (int num10 = 0; num10 < this.T; num10++)
						{
							if (this.wave[m][n][num10])
							{
								this.observed[m][n] = num10;
								break;
							}
						}
					}
				}
				return true;
			}
			double[] array2 = new double[this.T];
			for (int num11 = 0; num11 < this.T; num11++)
			{
				array2[num11] = (this.wave[num2][num3][num11] ? this.stationary[num11] : 0.0);
			}
			int num12 = array2.Random(this.random.NextDouble());
			for (int num13 = 0; num13 < this.T; num13++)
			{
				this.wave[num2][num3][num13] = num13 == num12;
			}
			this.changes[num2][num3] = true;
			return false;
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000AE230 File Offset: 0x000AC430
		public bool Run(int seed, int limit)
		{
			this.logT = Math.Log((double)this.T);
			this.logProb = new double[this.T];
			for (int i = 0; i < this.T; i++)
			{
				this.logProb[i] = Math.Log(this.stationary[i]);
			}
			this.Clear();
			this.random = new global::System.Random(seed);
			int num = 0;
			while (num < limit || limit == 0)
			{
				if (this.Observe())
				{
					return true;
				}
				while (this.Propagate())
				{
				}
				num++;
			}
			return true;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000AE2BC File Offset: 0x000AC4BC
		protected virtual void Clear()
		{
			for (int i = 0; i < this.FMX; i++)
			{
				for (int j = 0; j < this.FMY; j++)
				{
					for (int k = 0; k < this.T; k++)
					{
						if (this.bClear)
						{
							this.wave[i][j][k] = true;
						}
					}
					this.changes[i][j] = false;
				}
			}
		}

		// Token: 0x06002282 RID: 8834
		protected abstract bool OnBoundary(int x, int y);

		// Token: 0x06002283 RID: 8835
		public abstract Color32[] GetResult();

		// Token: 0x04001143 RID: 4419
		protected bool[][][] wave;

		// Token: 0x04001144 RID: 4420
		protected bool[][] changes;

		// Token: 0x04001145 RID: 4421
		protected double[] stationary;

		// Token: 0x04001146 RID: 4422
		protected int[][] observed;

		// Token: 0x04001147 RID: 4423
		public int FMX;

		// Token: 0x04001148 RID: 4424
		public int FMY;

		// Token: 0x04001149 RID: 4425
		public int T;

		// Token: 0x0400114A RID: 4426
		public int limit;

		// Token: 0x0400114B RID: 4427
		protected bool periodic;

		// Token: 0x0400114C RID: 4428
		private double[] logProb;

		// Token: 0x0400114D RID: 4429
		private double logT;

		// Token: 0x0400114E RID: 4430
		public bool bClear = true;

		// Token: 0x0400114F RID: 4431
		private global::System.Random random = Stat.Rnd5;
	}
}
