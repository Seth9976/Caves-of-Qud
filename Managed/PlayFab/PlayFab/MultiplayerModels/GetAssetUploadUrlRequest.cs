using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	public class GetAssetUploadUrlRequest : PlayFabRequestCommon
	{
		// Token: 0x0400046D RID: 1133
		public Dictionary<string, string> CustomTags;

		// Token: 0x0400046E RID: 1134
		public string FileName;
	}
}
