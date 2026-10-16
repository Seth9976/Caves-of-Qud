using System;
using System.Collections.Generic;
using PlayFab.SharedModels;

namespace PlayFab.MultiplayerModels
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public class ServerDetails : PlayFabBaseModel
	{
		// Token: 0x04000584 RID: 1412
		public string IPV4Address;

		// Token: 0x04000585 RID: 1413
		public List<Port> Ports;

		// Token: 0x04000586 RID: 1414
		public string Region;
	}
}
