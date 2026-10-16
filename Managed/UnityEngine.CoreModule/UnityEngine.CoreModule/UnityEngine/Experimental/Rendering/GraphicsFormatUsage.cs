using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000598 RID: 1432
	[Flags]
	public enum GraphicsFormatUsage
	{
		// Token: 0x04001301 RID: 4865
		None = 0,
		// Token: 0x04001302 RID: 4866
		Sample = 1,
		// Token: 0x04001303 RID: 4867
		Linear = 2,
		// Token: 0x04001304 RID: 4868
		Sparse = 4,
		// Token: 0x04001305 RID: 4869
		Render = 16,
		// Token: 0x04001306 RID: 4870
		Blend = 32,
		// Token: 0x04001307 RID: 4871
		GetPixels = 64,
		// Token: 0x04001308 RID: 4872
		SetPixels = 128,
		// Token: 0x04001309 RID: 4873
		SetPixels32 = 256,
		// Token: 0x0400130A RID: 4874
		ReadPixels = 512,
		// Token: 0x0400130B RID: 4875
		LoadStore = 1024,
		// Token: 0x0400130C RID: 4876
		MSAA2x = 2048,
		// Token: 0x0400130D RID: 4877
		MSAA4x = 4096,
		// Token: 0x0400130E RID: 4878
		MSAA8x = 8192,
		// Token: 0x0400130F RID: 4879
		StencilSampling = 65536
	}
}
