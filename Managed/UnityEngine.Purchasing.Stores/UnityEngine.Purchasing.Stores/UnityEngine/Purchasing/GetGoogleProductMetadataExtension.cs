using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000026 RID: 38
	public static class GetGoogleProductMetadataExtension
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003D8B File Offset: 0x00001F8B
		public static GoogleProductMetadata GetGoogleProductMetadata(this ProductMetadata productMetadata)
		{
			return productMetadata as GoogleProductMetadata;
		}
	}
}
