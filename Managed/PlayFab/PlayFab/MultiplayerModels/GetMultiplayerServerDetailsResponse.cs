using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class GetMultiplayerServerDetailsResponse : PlayFabResultCommon
	{
		// Token: 0x040004AB RID: 1195
		public List<ConnectedPlayer> ConnectedPlayers;

		// Token: 0x040004AC RID: 1196
		public string FQDN;

		// Token: 0x040004AD RID: 1197
		public string IPV4Address;

		// Token: 0x040004AE RID: 1198
		public DateTime? LastStateTransitionTime;

		// Token: 0x040004AF RID: 1199
		public List<Port> Ports;

		// Token: 0x040004B0 RID: 1200
		public string Region;

		// Token: 0x040004B1 RID: 1201
		public string ServerId;

		// Token: 0x040004B2 RID: 1202
		public string SessionId;

		// Token: 0x040004B3 RID: 1203
		public string State;

		// Token: 0x040004B4 RID: 1204
		public string VmId;
	}
}
