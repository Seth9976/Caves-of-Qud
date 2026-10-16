using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public class GetContainerRegistryCredentialsResponse : PlayFabResultCommon
	{
		// Token: 0x0400048A RID: 1162
		public string DnsName;

		// Token: 0x0400048B RID: 1163
		public string Password;

		// Token: 0x0400048C RID: 1164
		public string Username;
	}
}
