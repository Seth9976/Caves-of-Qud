using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000499 RID: 1177
	[Flags]
	public enum ShadowMapPass
	{
		// Token: 0x04000D5A RID: 3418
		PointlightPositiveX = 1,
		// Token: 0x04000D5B RID: 3419
		PointlightNegativeX = 2,
		// Token: 0x04000D5C RID: 3420
		PointlightPositiveY = 4,
		// Token: 0x04000D5D RID: 3421
		PointlightNegativeY = 8,
		// Token: 0x04000D5E RID: 3422
		PointlightPositiveZ = 16,
		// Token: 0x04000D5F RID: 3423
		PointlightNegativeZ = 32,
		// Token: 0x04000D60 RID: 3424
		DirectionalCascade0 = 64,
		// Token: 0x04000D61 RID: 3425
		DirectionalCascade1 = 128,
		// Token: 0x04000D62 RID: 3426
		DirectionalCascade2 = 256,
		// Token: 0x04000D63 RID: 3427
		DirectionalCascade3 = 512,
		// Token: 0x04000D64 RID: 3428
		Spotlight = 1024,
		// Token: 0x04000D65 RID: 3429
		AreaLight = 2048,
		// Token: 0x04000D66 RID: 3430
		Pointlight = 63,
		// Token: 0x04000D67 RID: 3431
		Directional = 960,
		// Token: 0x04000D68 RID: 3432
		All = 2047
	}
}
