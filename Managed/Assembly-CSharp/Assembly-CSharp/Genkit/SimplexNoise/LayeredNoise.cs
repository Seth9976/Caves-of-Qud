using System;

namespace Genkit.SimplexNoise
{
	// Token: 0x0200039F RID: 927
	public class LayeredNoise
	{
		// Token: 0x060022CE RID: 8910 RVA: 0x000B0CF0 File Offset: 0x000AEEF0
		private static int LongRandom(Random rand)
		{
			byte[] array = new byte[4];
			rand.NextBytes(array);
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000B0D44 File Offset: 0x000AEF44
		public static LayeredNoise CreateLinearOctiveLayers(int _Layers, float _OctaveStrength, float _BaseStride, string Seed = null)
		{
			LayeredNoise layeredNoise = new LayeredNoise();
			layeredNoise.GenerateNormalized = true;
			layeredNoise.OctaveStrength = _OctaveStrength;
			layeredNoise.Layers = _Layers;
			Random random = ((Seed == null) ? new Random() : new Random(Hash.String(Seed)));
			layeredNoise.LayerSeeds = new int[layeredNoise.Layers];
			layeredNoise.LayerNoise = new NoiseLayer[layeredNoise.Layers];
			for (int i = 0; i < layeredNoise.Layers; i++)
			{
				layeredNoise.LayerSeeds[i] = LayeredNoise.LongRandom(random);
				layeredNoise.LayerNoise[i] = new NoiseLayer(layeredNoise.LayerSeeds[i]);
				layeredNoise.LayerNoise[i].SampleFunction = new LinearSampler(_BaseStride * (float)Math.Pow((double)_OctaveStrength, (double)i));
			}
			return layeredNoise;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000B0DF8 File Offset: 0x000AEFF8
		public static LayeredNoise CreateCrazyOctiveLayers(int _Layers, float _OctaveStrength, float _BaseStride, string Seed = null)
		{
			LayeredNoise layeredNoise = new LayeredNoise();
			layeredNoise.OctaveStrength = _OctaveStrength;
			layeredNoise.Layers = _Layers;
			Random random = ((Seed == null) ? new Random() : new Random(Hash.String(Seed)));
			layeredNoise.LayerSeeds = new int[layeredNoise.Layers];
			layeredNoise.LayerNoise = new NoiseLayer[layeredNoise.Layers];
			for (int i = 0; i < layeredNoise.Layers; i++)
			{
				layeredNoise.LayerSeeds[i] = LayeredNoise.LongRandom(random);
				layeredNoise.LayerNoise[i] = new NoiseLayer(layeredNoise.LayerSeeds[i]);
				layeredNoise.LayerNoise[i].SampleFunction = new CrazySampler(_BaseStride * (float)Math.Pow((double)_OctaveStrength, (double)i));
			}
			return layeredNoise;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000B0EA4 File Offset: 0x000AF0A4
		public float[] Generate1D(int Width)
		{
			float[] array = new float[Width];
			for (int i = 0; i < Width; i++)
			{
				array[i] = this.Generate((float)i);
			}
			if (this.GenerateNormalized)
			{
				float num = Mathx.Max<float>(array, float.MinValue);
				float num2 = Mathx.Min<float>(array, float.MaxValue);
				for (int j = 0; j < Width; j++)
				{
					array[j] = (array[j] - num2) / (num - num2);
				}
				for (int k = 0; k < Width; k++)
				{
					array[k] = array[k] * (this.GenerateMax - this.GenerateMin) + this.GenerateMin;
				}
			}
			return array;
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000B0F3C File Offset: 0x000AF13C
		public float[,] Generate2D(int Width, int Height)
		{
			float[,] array = new float[Width, Height];
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					array[i, j] = this.Generate((float)i, (float)j);
				}
			}
			if (this.GenerateNormalized)
			{
				float num = Mathx.Max<float>(array, float.MinValue);
				float num2 = Mathx.Min<float>(array, float.MaxValue);
				for (int k = 0; k < Width; k++)
				{
					for (int l = 0; l < Height; l++)
					{
						array[k, l] = (array[k, l] - num2) / (num - num2);
						array[k, l] = array[k, l] * (this.GenerateMax - this.GenerateMin) + this.GenerateMin;
					}
				}
			}
			return array;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000B1004 File Offset: 0x000AF204
		public float[,] Generate3DSlice(int Width, int Height, int Depth)
		{
			float[,] array = new float[Width, Height];
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					for (int k = 0; k < Depth; k++)
					{
						array[i, j] = this.Generate((float)i, (float)j, (float)k);
					}
				}
			}
			if (this.GenerateNormalized)
			{
				float num = Mathx.Max<float>(array, float.MinValue);
				float num2 = Mathx.Min<float>(array, float.MaxValue);
				for (int l = 0; l < Width; l++)
				{
					for (int m = 0; m < Height; m++)
					{
						array[l, m] = (array[l, m] - num2) / (num - num2);
						array[l, m] = array[l, m] * (this.GenerateMax - this.GenerateMin) + this.GenerateMin;
					}
				}
			}
			return array;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000B10E0 File Offset: 0x000AF2E0
		public float[,,] Generate3D(int Width, int Height, int Depth)
		{
			float[,,] array = new float[Width, Height, Depth];
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					for (int k = 0; k < Depth; k++)
					{
						array[i, j, k] = this.Generate((float)i, (float)j, (float)k);
					}
				}
			}
			if (this.GenerateNormalized)
			{
				float num = Mathx.Max<float>(array, float.MinValue);
				float num2 = Mathx.Min<float>(array, float.MaxValue);
				for (int l = 0; l < Width; l++)
				{
					for (int m = 0; m < Height; m++)
					{
						for (int n = 0; n < Depth; n++)
						{
							array[l, m, n] = (array[l, m, n] - num2) / (num - num2);
							array[l, m, n] = array[l, m, n] * (this.GenerateMax - this.GenerateMin) + this.GenerateMin;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000B11D4 File Offset: 0x000AF3D4
		public float Generate(float x)
		{
			float num = 0f;
			for (int i = 0; i < this.Layers; i++)
			{
				float num2 = x * this.BaseSampleMultiple;
				num += this.LayerNoise[i].Generate(num2);
			}
			return num / (float)this.Layers;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000B121C File Offset: 0x000AF41C
		public float Generate(float x, float y)
		{
			float num = 0f;
			for (int i = 0; i < this.Layers; i++)
			{
				float num2 = x * this.BaseSampleMultiple;
				float num3 = y * this.BaseSampleMultiple;
				num += this.LayerNoise[i].Generate(num2, num3);
			}
			return num / (float)this.Layers;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000B1270 File Offset: 0x000AF470
		public float Generate(float x, float y, float z)
		{
			float num = 0f;
			for (int i = 0; i < this.Layers; i++)
			{
				num += this.LayerNoise[i].Generate(x, y, z);
			}
			return num / (float)this.Layers;
		}

		// Token: 0x0400118D RID: 4493
		public float GenerateMin;

		// Token: 0x0400118E RID: 4494
		public float GenerateMax = 1f;

		// Token: 0x0400118F RID: 4495
		public bool GenerateNormalized = true;

		// Token: 0x04001190 RID: 4496
		public float BaseSampleMultiple = 1f;

		// Token: 0x04001191 RID: 4497
		public float OctaveStrength = 2f;

		// Token: 0x04001192 RID: 4498
		private int Layers;

		// Token: 0x04001193 RID: 4499
		private int[] LayerSeeds;

		// Token: 0x04001194 RID: 4500
		private NoiseLayer[] LayerNoise;
	}
}
