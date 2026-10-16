using System;

namespace Genkit.SimplexNoise
{
	// Token: 0x0200039D RID: 925
	public class CrazySampler : ISample
	{
		// Token: 0x060022C6 RID: 8902 RVA: 0x000B0B4D File Offset: 0x000AED4D
		public CrazySampler(float Scale)
		{
			this.SampleStride = new SamplePoint(Scale, Scale, Scale);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000B0B80 File Offset: 0x000AED80
		public override SamplePoint Sample(float x, float y, float z)
		{
			return new SamplePoint((float)(Math.Sin((double)(x * this.SampleStride.x)) + Math.Cos((double)(y * this.SampleStride.x))), y * this.SampleStride.y, z * this.SampleStride.z);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000B0BD4 File Offset: 0x000AEDD4
		public override SamplePoint Sample(float x, float y)
		{
			return new SamplePoint(x * this.SampleStride.x, y * this.SampleStride.y, 0f);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000B0BFA File Offset: 0x000AEDFA
		public override SamplePoint Sample(float x)
		{
			return new SamplePoint(x * this.SampleStride.x, 0f, 0f);
		}

		// Token: 0x04001188 RID: 4488
		public SamplePoint SampleStride = new SamplePoint(0.01f, 0.01f, 0.01f);
	}
}
