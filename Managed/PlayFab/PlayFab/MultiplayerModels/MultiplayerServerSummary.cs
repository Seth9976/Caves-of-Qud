using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public class MultiplayerServerSummary : PlayFabBaseModel
	{
		// Token: 0x04000550 RID: 1360
		public List<ConnectedPlayer> ConnectedPlayers;

		// Token: 0x04000551 RID: 1361
		public DateTime? LastStateTransitionTime;

		// Token: 0x04000552 RID: 1362
		public string Region;

		// Token: 0x04000553 RID: 1363
		public string ServerId;

		// Token: 0x04000554 RID: 1364
		public string SessionId;

		// Token: 0x04000555 RID: 1365
		public string State;

		// Token: 0x04000556 RID: 1366
		public string VmId;
	}
}
