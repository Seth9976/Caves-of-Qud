using System;

namespace Steamworks
{
	// Token: 0x02000157 RID: 343
	public enum ESteamNetworkingConnectionState
	{
		// Token: 0x04000994 RID: 2452
		k_ESteamNetworkingConnectionState_None,
		// Token: 0x04000995 RID: 2453
		k_ESteamNetworkingConnectionState_Connecting,
		// Token: 0x04000996 RID: 2454
		k_ESteamNetworkingConnectionState_FindingRoute,
		// Token: 0x04000997 RID: 2455
		k_ESteamNetworkingConnectionState_Connected,
		// Token: 0x04000998 RID: 2456
		k_ESteamNetworkingConnectionState_ClosedByPeer,
		// Token: 0x04000999 RID: 2457
		k_ESteamNetworkingConnectionState_ProblemDetectedLocally,
		// Token: 0x0400099A RID: 2458
		k_ESteamNetworkingConnectionState_FinWait = -1,
		// Token: 0x0400099B RID: 2459
		k_ESteamNetworkingConnectionState_Linger = -2,
		// Token: 0x0400099C RID: 2460
		k_ESteamNetworkingConnectionState_Dead = -3,
		// Token: 0x0400099D RID: 2461
		k_ESteamNetworkingConnectionState__Force32Bit = 2147483647
	}
}
