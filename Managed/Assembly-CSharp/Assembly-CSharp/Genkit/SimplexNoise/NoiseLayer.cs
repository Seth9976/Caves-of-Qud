using System;

namespace Genkit.SimplexNoise
{
	// Token: 0x0200039E RID: 926
	public class NoiseLayer
	{
		// Token: 0x060022CA RID: 8906 RVA: 0x000B0C18 File Offset: 0x000AEE18
		public NoiseLayer(int _Seed)
		{
			this.Seed = _Seed;
			this.LayerNoise = new Noise(this.Seed);
			this.SampleFunction = new LinearSampler(0.01f);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000B0C54 File Offset: 0x000AEE54
		public float Generate(float x)
		{
			SamplePoint samplePoint = this.SampleFunction.Sample(x);
			return this.LayerNoise.Generate(samplePoint.x);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000B0C80 File Offset: 0x000AEE80
		public float Generate(float x, float y)
		{
			SamplePoint samplePoint = this.SampleFunction.Sample(x, y);
			return this.LayerNoise.Generate(samplePoint.x, samplePoint.y);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000B0CB4 File Offset: 0x000AEEB4
		public float Generate(float x, float y, float z)
		{
			SamplePoint samplePoint = this.SampleFunction.Sample(x, y, z);
			return this.LayerNoise.Generate(samplePoint.x, samplePoint.y, samplePoint.z);
		}

		// Token: 0x04001189 RID: 4489
		public int Seed;

		// Token: 0x0400118A RID: 4490
		public Noise LayerNoise;

		// Token: 0x0400118B RID: 4491
		public float OctaveStrength = 2f;

		// Token: 0x0400118C RID: 4492
		public ISample SampleFunction;
	}
}
