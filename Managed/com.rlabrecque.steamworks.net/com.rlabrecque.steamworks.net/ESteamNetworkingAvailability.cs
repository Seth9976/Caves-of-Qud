using System;

namespace Steamworks
{
	// Token: 0x02000154 RID: 340
	public enum ESteamNetworkingAvailability
	{
		// Token: 0x04000978 RID: 2424
		k_ESteamNetworkingAvailability_CannotTry = -102,
		// Token: 0x04000979 RID: 2425
		k_ESteamNetworkingAvailability_Failed,
		// Token: 0x0400097A RID: 2426
		k_ESteamNetworkingAvailability_Previously,
		// Token: 0x0400097B RID: 2427
		k_ESteamNetworkingAvailability_Retrying = -10,
		// Token: 0x0400097C RID: 2428
		k_ESteamNetworkingAvailability_NeverTried = 1,
		// Token: 0x0400097D RID: 2429
		k_ESteamNetworkingAvailability_Waiting,
		// Token: 0x0400097E RID: 2430
		k_ESteamNetworkingAvailability_Attempting,
		// Token: 0x0400097F RID: 2431
		k_ESteamNetworkingAvailability_Current = 100,
		// Token: 0x04000980 RID: 2432
		k_ESteamNetworkingAvailability_Unknown = 0,
		// Token: 0x04000981 RID: 2433
		k_ESteamNetworkingAvailability__Force32bit = 2147483647
	}
}
