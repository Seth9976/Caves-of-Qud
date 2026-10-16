using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x020002C7 RID: 711
	[Serializable]
	public class SetPlayerSecretRequest : PlayFabRequestCommon
	{
		// Token: 0x04000E8A RID: 3722
		public string EncryptedRequest;

		// Token: 0x04000E8B RID: 3723
		public string PlayerSecret;
	}
}
