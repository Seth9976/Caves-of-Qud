using System;

namespace Genkit.SimplexNoise
{
	// Token: 0x0200039C RID: 924
	public class LinearSampler : ISample
	{
		// Token: 0x060022C2 RID: 8898 RVA: 0x000B0AAB File Offset: 0x000AECAB
		public LinearSampler(float Scale)
		{
			this.SampleStride = new SamplePoint(Scale, Scale, Scale);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000B0ADB File Offset: 0x000AECDB
		public override SamplePoint Sample(float x, float y, float z)
		{
			return new SamplePoint(x * this.SampleStride.x, y * this.SampleStride.y, z * this.SampleStride.z);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000B0B09 File Offset: 0x000AED09
		public override SamplePoint Sample(float x, float y)
		{
			return new SamplePoint(x * this.SampleStride.x, y * this.SampleStride.y, 0f);
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000B0B2F File Offset: 0x000AED2F
		public override SamplePoint Sample(float x)
		{
			return new SamplePoint(x * this.SampleStride.x, 0f, 0f);
		}

		// Token: 0x04001187 RID: 4487
		public SamplePoint SampleStride = new SamplePoint(0.01f, 0.01f, 0.01f);
	}
}
