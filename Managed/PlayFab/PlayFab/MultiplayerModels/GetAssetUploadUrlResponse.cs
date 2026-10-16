using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class GetAssetUploadUrlResponse : PlayFabResultCommon
	{
		// Token: 0x0400046F RID: 1135
		public string AssetUploadUrl;

		// Token: 0x04000470 RID: 1136
		public string FileName;
	}
}
