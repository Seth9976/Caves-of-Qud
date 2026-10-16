using System;

namespace Steamworks
{
	// Token: 0x02000155 RID: 341
	public enum ESteamNetworkingIdentityType
	{
		// Token: 0x04000983 RID: 2435
		k_ESteamNetworkingIdentityType_Invalid,
		// Token: 0x04000984 RID: 2436
		k_ESteamNetworkingIdentityType_SteamID = 16,
		// Token: 0x04000985 RID: 2437
		k_ESteamNetworkingIdentityType_XboxPairwiseID,
		// Token: 0x04000986 RID: 2438
		k_ESteamNetworkingIdentityType_SonyPSN,
		// Token: 0x04000987 RID: 2439
		k_ESteamNetworkingIdentityType_GoogleStadia,
		// Token: 0x04000988 RID: 2440
		k_ESteamNetworkingIdentityType_IPAddress = 1,
		// Token: 0x04000989 RID: 2441
		k_ESteamNetworkingIdentityType_GenericString,
		// Token: 0x0400098A RID: 2442
		k_ESteamNetworkingIdentityType_GenericBytes,
		// Token: 0x0400098B RID: 2443
		k_ESteamNetworkingIdentityType_UnknownType,
		// Token: 0x0400098C RID: 2444
		k_ESteamNetworkingIdentityType__Force32bit = 2147483647
	}
}
