using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x0200016D RID: 365
	[Serializable]
	public class InitiateFileUploadMetadata : PlayFabBaseModel
	{
		// Token: 0x04000736 RID: 1846
		public string FileName;

		// Token: 0x04000737 RID: 1847
		public string UploadUrl;
	}
}
