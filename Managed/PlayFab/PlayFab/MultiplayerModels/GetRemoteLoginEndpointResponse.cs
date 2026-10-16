using System;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public class GetRemoteLoginEndpointResponse : PlayFabResultCommon
	{
		// Token: 0x040004C2 RID: 1218
		public string IPV4Address;

		// Token: 0x040004C3 RID: 1219
		public int Port;
	}
}
