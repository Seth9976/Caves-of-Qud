using System;
using PlayFab.SharedModels;

namespace PlayFab.ClientModels
{
	// Token: 0x0200031E RID: 798
	[Serializable]
	public class UserOpenIdInfo : PlayFabBaseModel
	{
		// Token: 0x04000F7C RID: 3964
		public string ConnectionId;

		// Token: 0x04000F7D RID: 3965
		public string Issuer;

		// Token: 0x04000F7E RID: 3966
		public string Subject;
	}
}
