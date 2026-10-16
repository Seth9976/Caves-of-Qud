using System;
using PlayFab.SharedModels;

namespace PlayFab.ProfilesModels
{
	// Token: 0x02000043 RID: 67
	[Serializable]
	public class EntityProfileFileMetadata : PlayFabBaseModel
	{
		// Token: 0x0400030E RID: 782
		public string Checksum;

		// Token: 0x0400030F RID: 783
		public string FileName;

		// Token: 0x04000310 RID: 784
		public DateTime LastModified;

		// Token: 0x04000311 RID: 785
		public int Size;
	}
}
