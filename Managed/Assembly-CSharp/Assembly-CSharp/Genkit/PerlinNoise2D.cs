using System;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x0200038C RID: 908
	public class PerlinNoise2D
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x000AB264 File Offset: 0x000A9464
		public PerlinNoise2D(int freq, float _amp, Random rand)
		{
			this.Noise = new double[freq, freq];
			this.Amplitude = _amp;
			this.Frequency = freq;
			for (int i = 0; i < freq; i++)
			{
				for (int j = 0; j < freq; j++)
				{
					this.Noise[i, j] = rand.NextDouble();
				}
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000AB2D0 File Offset: 0x000A94D0
		public static double getMax(double[,] Array)
		{
			double num = double.MinValue;
			int upperBound = Array.GetUpperBound(0);
			int upperBound2 = Array.GetUpperBound(1);
			for (int i = Array.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = Array.GetLowerBound(1); j <= upperBound2; j++)
				{
					double num2 = Array[i, j];
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000AB33C File Offset: 0x000A953C
		public static double getMax(List<double> DoubleCollection)
		{
			double num = double.MinValue;
			foreach (double num2 in DoubleCollection)
			{
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000AB394 File Offset: 0x000A9594
		public static double[,] sumNoiseFunctions(int width, int height, int sx, int sy, List<PerlinNoise2D> noiseFunctions)
		{
			return PerlinNoise2D.sumNoiseFunctions(width, height, sx, sy, noiseFunctions, 1f);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000AB3A8 File Offset: 0x000A95A8
		public static double[,] sumNoiseFunctions(int width, int height, int sx, int sy, List<PerlinNoise2D> noiseFunctions, float xmul)
		{
			double[,] array = new double[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					array[i, j] = 0.0;
				}
			}
			for (int k = 0; k < noiseFunctions.Count; k++)
			{
				double num = (double)((float)width * xmul / (float)noiseFunctions[k].Frequency);
				double num2 = (double)((float)height / (float)noiseFunctions[k].Frequency);
				for (int l = 0; l < width; l++)
				{
					for (int m = 0; m < height; m++)
					{
						int num3 = (int)((double)l / num);
						int num4 = num3 + 1;
						int num5 = (int)((double)m / num2);
						int num6 = num5 + 1;
						double interpolatedPoint = noiseFunctions[k].getInterpolatedPoint(num3 + sx, num4 + sx, num5 + sy, num6 + sy, (double)l / num - (double)num3, (double)m / num2 - (double)num5);
						array[l, m] += interpolatedPoint * (double)noiseFunctions[k].Amplitude;
					}
				}
			}
			double max = PerlinNoise2D.getMax(array);
			for (int n = 0; n < width; n++)
			{
				for (int num7 = 0; num7 < height; num7++)
				{
					array[n, num7] /= max;
				}
			}
			return array;
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000AB500 File Offset: 0x000A9700
		public double getInterpolatedPoint(int _xa, int _xb, int _ya, int _yb, double x, double y)
		{
			double num = this.interpolate(this.Noise[_xa % this.Frequency, _ya % this.Frequency], this.Noise[_xb % this.Frequency, _ya % this.Frequency], x);
			double num2 = this.interpolate(this.Noise[_xa % this.Frequency, _yb % this.Frequency], this.Noise[_xb % this.Frequency, _yb % this.Frequency], x);
			return this.interpolate(num, num2, y);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000AB598 File Offset: 0x000A9798
		private double interpolate(double a, double b, double x)
		{
			double num = x * 3.141592653589793;
			double num2 = (1.0 - Math.Cos(num)) * 0.5;
			return a * (1.0 - num2) + b * num2;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000AB5E0 File Offset: 0x000A97E0
		public static double[,] Smooth(double[,] Field, int Width, int Height, int FilterPasses)
		{
			int[,] array = new int[,]
			{
				{ 2, 4, 2 },
				{ 4, 8, 4 },
				{ 2, 4, 2 }
			};
			for (int i = 0; i < FilterPasses; i++)
			{
				double[,] array2 = new double[Width, Height];
				for (int j = 0; j < Width; j++)
				{
					for (int k = 0; k < Height; k++)
					{
						int num = 0;
						array2[j, k] = 0.0;
						for (int l = 0; l < 3; l++)
						{
							for (int m = 0; m < 3; m++)
							{
								if (j + (l - 1) >= 0 && j + (l - 1) < Width && k + (m - 1) >= 0 && k + (m - 1) < Height)
								{
									array2[j, k] += Field[j + (l - 1), k + (m - 1)] * (double)array[l, m];
									num += array[l, m];
								}
							}
						}
						array2[j, k] /= (double)num;
					}
				}
				Field = array2;
			}
			return Field;
		}

		// Token: 0x04001122 RID: 4386
		private double[,] Noise;

		// Token: 0x04001123 RID: 4387
		private float Amplitude = 1f;

		// Token: 0x04001124 RID: 4388
		private int Frequency = 1;
	}
}
