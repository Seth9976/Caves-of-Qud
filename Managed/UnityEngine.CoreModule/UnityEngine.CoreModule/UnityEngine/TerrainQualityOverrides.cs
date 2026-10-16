using System;

namespace UnityEngine
{
	// Token: 0x020001DE RID: 478
	[Flags]
	public enum TerrainQualityOverrides
	{
		// Token: 0x040006B2 RID: 1714
		None = 0,
		// Token: 0x040006B3 RID: 1715
		PixelError = 1,
		// Token: 0x040006B4 RID: 1716
		BasemapDistance = 2,
		// Token: 0x040006B5 RID: 1717
		DetailDensity = 4,
		// Token: 0x040006B6 RID: 1718
		DetailDistance = 8,
		// Token: 0x040006B7 RID: 1719
		TreeDistance = 16,
		// Token: 0x040006B8 RID: 1720
		BillboardStart = 32,
		// Token: 0x040006B9 RID: 1721
		FadeLength = 64,
		// Token: 0x040006BA RID: 1722
		MaxTrees = 128
	}
}
