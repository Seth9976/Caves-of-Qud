using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002AA RID: 682
	[Serializable]
	public class RefreshPSNAuthTokenRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E3D RID: 3645
		public string AuthCode;

		// Token: 0x04000E3E RID: 3646
		public int? IssuerId;

		// Token: 0x04000E3F RID: 3647
		public string RedirectUri;
	}
}
