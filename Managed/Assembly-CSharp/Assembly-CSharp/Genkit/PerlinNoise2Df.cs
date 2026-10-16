using System;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x0200038D RID: 909
	public class PerlinNoise2Df
	{
		// Token: 0x06002228 RID: 8744 RVA: 0x000AB6EC File Offset: 0x000A98EC
		public PerlinNoise2Df(int freq, float _amp, Random rand)
		{
			this.Noise = new float[freq, freq];
			this.Amplitude = _amp;
			this.Frequency = freq;
			for (int i = 0; i < freq; i++)
			{
				for (int j = 0; j < freq; j++)
				{
					this.Noise[i, j] = (float)rand.NextDouble();
				}
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000AB758 File Offset: 0x000A9958
		public static float getMax(float[,] Array)
		{
			float num = float.MinValue;
			int upperBound = Array.GetUpperBound(0);
			int upperBound2 = Array.GetUpperBound(1);
			for (int i = Array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = Array.GetLowerBound(1); j <= upperBound2; j++)
				{
					float num2 = Array[i, j];
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000AB7C0 File Offset: 0x000A99C0
		public static float getMax(List<float> DoubleCollection)
		{
			float num = float.MinValue;
			foreach (float num2 in DoubleCollection)
			{
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000AB814 File Offset: 0x000A9A14
		public static float[,] sumNoiseFunctions(int width, int height, int sx, int sy, List<PerlinNoise2Df> noiseFunctions)
		{
			return PerlinNoise2Df.sumNoiseFunctions(width, height, sx, sy, noiseFunctions, 1f);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000AB828 File Offset: 0x000A9A28
		public static float[,] sumNoiseFunctions(int width, int height, int sx, int sy, List<PerlinNoise2Df> noiseFunctions, float xmul)
		{
			float[,] array = new float[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[i, j] = 0f;
				}
			}
			for (int k = 0; k < noiseFunctions.Count; k++)
			{
				float num = (float)width * xmul / (float)noiseFunctions[k].Frequency;
				float num2 = (float)height / (float)noiseFunctions[k].Frequency;
				for (int l = 0; l < width; l++)
				{
					for (int m = 0; m < height; m++)
					{
						int num3 = (int)((float)l / num);
						int num4 = num3 + 1;
						int num5 = (int)((float)m / num2);
						int num6 = num5 + 1;
						float interpolatedPoint = noiseFunctions[k].getInterpolatedPoint(num3 + sx, num4 + sx, num5 + sy, num6 + sy, (float)l / num - (float)num3, (float)m / num2 - (float)num5);
						array[l, m] += interpolatedPoint * noiseFunctions[k].Amplitude;
					}
				}
			}
			float max = PerlinNoise2Df.getMax(array);
			for (int n = 0; n < width; n++)
			{
				for (int num7 = 0; num7 < height; num7++)
				{
					array[n, num7] /= max;
				}
			}
			return array;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000AB978 File Offset: 0x000A9B78
		public float getInterpolatedPoint(int _xa, int _xb, int _ya, int _yb, float x, float y)
		{
			float num = this.interpolate(this.Noise[_xa % this.Frequency, _ya % this.Frequency], this.Noise[_xb % this.Frequency, _ya % this.Frequency], x);
			float num2 = this.interpolate(this.Noise[_xa % this.Frequency, _yb % this.Frequency], this.Noise[_xb % this.Frequency, _yb % this.Frequency], x);
			return this.interpolate(num, num2, y);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000ABA10 File Offset: 0x000A9C10
		private float interpolate(float a, float b, float x)
		{
			float num = x * 3.1415927f;
			float num2 = (1f - (float)Math.Cos((double)num)) * 0.5f;
			return a * (1f - num2) + b * num2;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000ABA48 File Offset: 0x000A9C48
		public static float[,] Smooth(float[,] Field, int Width, int Height, int FilterPasses)
		{
			int[,] array = new int[,]
			{
				{ 2, 4, 2 },
				{ 4, 8, 4 },
				{ 2, 4, 2 }
			};
			for (int i = 0; i < FilterPasses; i++)
			{
				float[,] array2 = new float[Width, Height];
				for (int j = 0; j < Width; j++)
				{
					for (int k = 0; k < Height; k++)
					{
						int num = 0;
						array2[j, k] = 0f;
						for (int l = 0; l < 3; l++)
						{
							for (int m = 0; m < 3; m++)
							{
								if (j + (l - 1) >= 0 && j + (l - 1) < Width && k + (m - 1) >= 0 && k + (m - 1) < Height)
								{
									array2[j, k] += Field[j + (l - 1), k + (m - 1)] * (float)array[l, m];
									num += array[l, m];
								}
							}
						}
						array2[j, k] /= (float)num;
					}
				}
				Field = array2;
			}
			return Field;
		}

		// Token: 0x04001125 RID: 4389
		private float[,] Noise;

		// Token: 0x04001126 RID: 4390
		private float Amplitude = 1f;

		// Token: 0x04001127 RID: 4391
		private int Frequency = 1;
	}
}
