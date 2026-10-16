using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200005C RID: 92
	public static class GetAppleProductMetadataExtension
	{
		// Token: 0x06000215 RID: 533 RVA: 0x000072F6 File Offset: 0x000054F6
		public static AppleProductMetadata GetAppleProductMetadata(this ProductMetadata productMetadata)
		{
			return productMetadata as AppleProductMetadata;
		}
	}
}
