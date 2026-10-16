using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000201 RID: 513
	[Serializable]
	public class GetContentDownloadUrlRequest : PlayFabRequestCommon
	{
		// Token: 0x04000BE9 RID: 3049
		public string HttpMethod;

		// Token: 0x04000BEA RID: 3050
		public string Key;

		// Token: 0x04000BEB RID: 3051
		public bool? ThruCDN;
	}
}
