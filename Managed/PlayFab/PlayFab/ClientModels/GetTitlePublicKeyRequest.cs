using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x02000249 RID: 585
	[Serializable]
	public class GetTitlePublicKeyRequest : PlayFabRequestCommon
	{
		// Token: 0x04000C91 RID: 3217
		public string TitleId;

		// Token: 0x04000C92 RID: 3218
		public string TitleSharedSecret;
	}
}
