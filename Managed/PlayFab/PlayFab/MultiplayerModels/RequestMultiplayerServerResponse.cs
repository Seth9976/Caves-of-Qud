using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class RequestMultiplayerServerResponse : PlayFabResultCommon
	{
		// Token: 0x04000576 RID: 1398
		public List<ConnectedPlayer> ConnectedPlayers;

		// Token: 0x04000577 RID: 1399
		public string FQDN;

		// Token: 0x04000578 RID: 1400
		public string IPV4Address;

		// Token: 0x04000579 RID: 1401
		public DateTime? LastStateTransitionTime;

		// Token: 0x0400057A RID: 1402
		public List<Port> Ports;

		// Token: 0x0400057B RID: 1403
		public string Region;

		// Token: 0x0400057C RID: 1404
		public string ServerId;

		// Token: 0x0400057D RID: 1405
		public string SessionId;

		// Token: 0x0400057E RID: 1406
		public string State;

		// Token: 0x0400057F RID: 1407
		public string VmId;
	}
}
