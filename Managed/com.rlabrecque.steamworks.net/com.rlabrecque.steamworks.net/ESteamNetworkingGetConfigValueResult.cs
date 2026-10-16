using System;

namespace Steamworks
{
	// Token: 0x0200015C RID: 348
	public enum ESteamNetworkingGetConfigValueResult
	{
		// Token: 0x04000A15 RID: 2581
		k_ESteamNetworkingGetConfigValue_BadValue = -1,
		// Token: 0x04000A16 RID: 2582
		k_ESteamNetworkingGetConfigValue_BadScopeObj = -2,
		// Token: 0x04000A17 RID: 2583
		k_ESteamNetworkingGetConfigValue_BufferTooSmall = -3,
		// Token: 0x04000A18 RID: 2584
		k_ESteamNetworkingGetConfigValue_OK = 1,
		// Token: 0x04000A19 RID: 2585
		k_ESteamNetworkingGetConfigValue_OKInherited,
		// Token: 0x04000A1A RID: 2586
		k_ESteamNetworkingGetConfigValueResult__Force32Bit = 2147483647
	}
}
