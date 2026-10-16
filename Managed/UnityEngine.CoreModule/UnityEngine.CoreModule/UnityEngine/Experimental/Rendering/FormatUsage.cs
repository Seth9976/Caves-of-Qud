using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000597 RID: 1431
	[Obsolete("Use GraphicsFormatUsage instead", false)]
	public enum FormatUsage
	{
		// Token: 0x040012F2 RID: 4850
		Sample,
		// Token: 0x040012F3 RID: 4851
		Linear,
		// Token: 0x040012F4 RID: 4852
		Sparse,
		// Token: 0x040012F5 RID: 4853
		Render = 4,
		// Token: 0x040012F6 RID: 4854
		Blend,
		// Token: 0x040012F7 RID: 4855
		GetPixels,
		// Token: 0x040012F8 RID: 4856
		SetPixels,
		// Token: 0x040012F9 RID: 4857
		SetPixels32,
		// Token: 0x040012FA RID: 4858
		ReadPixels,
		// Token: 0x040012FB RID: 4859
		LoadStore,
		// Token: 0x040012FC RID: 4860
		MSAA2x,
		// Token: 0x040012FD RID: 4861
		MSAA4x,
		// Token: 0x040012FE RID: 4862
		MSAA8x,
		// Token: 0x040012FF RID: 4863
		StencilSampling = 16
	}
}
