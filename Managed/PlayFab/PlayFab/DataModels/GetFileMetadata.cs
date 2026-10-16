using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000168 RID: 360
	[Serializable]
	public class GetFileMetadata : PlayFabBaseModel
	{
		// Token: 0x04000726 RID: 1830
		public string Checksum;

		// Token: 0x04000727 RID: 1831
		public string DownloadUrl;

		// Token: 0x04000728 RID: 1832
		public string FileName;

		// Token: 0x04000729 RID: 1833
		public DateTime LastModified;

		// Token: 0x0400072A RID: 1834
		public int Size;
	}
}
