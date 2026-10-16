using System;

namespace UnityEngine
{
	// Token: 0x020001C8 RID: 456
	[Flags]
	public enum RenderTextureCreationFlags
	{
		// Token: 0x04000657 RID: 1623
		MipMap = 1,
		// Token: 0x04000658 RID: 1624
		AutoGenerateMips = 2,
		// Token: 0x04000659 RID: 1625
		SRGB = 4,
		// Token: 0x0400065A RID: 1626
		EyeTexture = 8,
		// Token: 0x0400065B RID: 1627
		EnableRandomWrite = 16,
		// Token: 0x0400065C RID: 1628
		CreatedFromScript = 32,
		// Token: 0x0400065D RID: 1629
		AllowVerticalFlip = 128,
		// Token: 0x0400065E RID: 1630
		NoResolvedColorSurface = 256,
		// Token: 0x0400065F RID: 1631
		DynamicallyScalable = 1024,
		// Token: 0x04000660 RID: 1632
		BindMS = 2048,
		// Token: 0x04000661 RID: 1633
		DynamicallyScalableExplicit = 131072
	}
}
