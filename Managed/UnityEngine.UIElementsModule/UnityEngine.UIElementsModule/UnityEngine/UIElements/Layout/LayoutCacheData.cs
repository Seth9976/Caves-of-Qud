using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200069A RID: 1690
	internal struct LayoutCacheData
	{
		// Token: 0x0400183C RID: 6204
		public static LayoutCacheData Default = new LayoutCacheData
		{
			NextCachedMeasurementsIndex = 0U,
			CachedLayout = LayoutCachedMeasurement.Default
		};

		// Token: 0x0400183D RID: 6205
		public uint NextCachedMeasurementsIndex;

		// Token: 0x0400183E RID: 6206
		public FixedBuffer16<LayoutCachedMeasurement> cachedMeasurements;

		// Token: 0x0400183F RID: 6207
		public LayoutCachedMeasurement CachedLayout;
	}
}
