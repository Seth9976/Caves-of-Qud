using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public class RolloverContainerRegistryCredentialsResponse : PlayFabResultCommon
	{
		// Token: 0x04000581 RID: 1409
		public string DnsName;

		// Token: 0x04000582 RID: 1410
		public string Password;

		// Token: 0x04000583 RID: 1411
		public string Username;
	}
}
