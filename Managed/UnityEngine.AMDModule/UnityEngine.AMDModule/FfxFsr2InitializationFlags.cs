using System;

namespace UnityEngine.AMD
{
	// Token: 0x02000003 RID: 3
	[Flags]
	public enum FfxFsr2InitializationFlags
	{
		// Token: 0x04000002 RID: 2
		EnableHighDynamicRange = 1,
		// Token: 0x04000003 RID: 3
		EnableDisplayResolutionMotionVectors = 2,
		// Token: 0x04000004 RID: 4
		EnableMotionVectorsJitterCancellation = 4,
		// Token: 0x04000005 RID: 5
		DepthInverted = 8,
		// Token: 0x04000006 RID: 6
		EnableDepthInfinite = 16,
		// Token: 0x04000007 RID: 7
		EnableAutoExposure = 32,
		// Token: 0x04000008 RID: 8
		EnableDynamicResolution = 64,
		// Token: 0x04000009 RID: 9
		EnableTexture1DUsage = 128
	}
}
