using System;

namespace Genkit.SimplexNoise
{
	// Token: 0x0200039B RID: 923
	public class ISample
	{
		// Token: 0x060022BE RID: 8894 RVA: 0x000B0A79 File Offset: 0x000AEC79
		public virtual SamplePoint Sample(float x, float y, float z)
		{
			return new SamplePoint(x, y, z);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000B0A83 File Offset: 0x000AEC83
		public virtual SamplePoint Sample(float x, float y)
		{
			return new SamplePoint(x, y, 0f);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000B0A91 File Offset: 0x000AEC91
		public virtual SamplePoint Sample(float x)
		{
			return new SamplePoint(x, 0f, 0f);
		}
	}
}
