using System;

namespace Steamworks
{
	// Token: 0x02000158 RID: 344
	public enum ESteamNetConnectionEnd
	{
		// Token: 0x0400099F RID: 2463
		k_ESteamNetConnectionEnd_Invalid,
		// Token: 0x040009A0 RID: 2464
		k_ESteamNetConnectionEnd_App_Min = 1000,
		// Token: 0x040009A1 RID: 2465
		k_ESteamNetConnectionEnd_App_Generic = 1000,
		// Token: 0x040009A2 RID: 2466
		k_ESteamNetConnectionEnd_App_Max = 1999,
		// Token: 0x040009A3 RID: 2467
		k_ESteamNetConnectionEnd_AppException_Min,
		// Token: 0x040009A4 RID: 2468
		k_ESteamNetConnectionEnd_AppException_Generic = 2000,
		// Token: 0x040009A5 RID: 2469
		k_ESteamNetConnectionEnd_AppException_Max = 2999,
		// Token: 0x040009A6 RID: 2470
		k_ESteamNetConnectionEnd_Local_Min,
		// Token: 0x040009A7 RID: 2471
		k_ESteamNetConnectionEnd_Local_OfflineMode,
		// Token: 0x040009A8 RID: 2472
		k_ESteamNetConnectionEnd_Local_ManyRelayConnectivity,
		// Token: 0x040009A9 RID: 2473
		k_ESteamNetConnectionEnd_Local_HostedServerPrimaryRelay,
		// Token: 0x040009AA RID: 2474
		k_ESteamNetConnectionEnd_Local_NetworkConfig,
		// Token: 0x040009AB RID: 2475
		k_ESteamNetConnectionEnd_Local_Rights,
		// Token: 0x040009AC RID: 2476
		k_ESteamNetConnectionEnd_Local_P2P_ICE_NoPublicAddresses,
		// Token: 0x040009AD RID: 2477
		k_ESteamNetConnectionEnd_Local_Max = 3999,
		// Token: 0x040009AE RID: 2478
		k_ESteamNetConnectionEnd_Remote_Min,
		// Token: 0x040009AF RID: 2479
		k_ESteamNetConnectionEnd_Remote_Timeout,
		// Token: 0x040009B0 RID: 2480
		k_ESteamNetConnectionEnd_Remote_BadCrypt,
		// Token: 0x040009B1 RID: 2481
		k_ESteamNetConnectionEnd_Remote_BadCert,
		// Token: 0x040009B2 RID: 2482
		k_ESteamNetConnectionEnd_Remote_BadProtocolVersion = 4006,
		// Token: 0x040009B3 RID: 2483
		k_ESteamNetConnectionEnd_Remote_P2P_ICE_NoPublicAddresses,
		// Token: 0x040009B4 RID: 2484
		k_ESteamNetConnectionEnd_Remote_Max = 4999,
		// Token: 0x040009B5 RID: 2485
		k_ESteamNetConnectionEnd_Misc_Min,
		// Token: 0x040009B6 RID: 2486
		k_ESteamNetConnectionEnd_Misc_Generic,
		// Token: 0x040009B7 RID: 2487
		k_ESteamNetConnectionEnd_Misc_InternalError,
		// Token: 0x040009B8 RID: 2488
		k_ESteamNetConnectionEnd_Misc_Timeout,
		// Token: 0x040009B9 RID: 2489
		k_ESteamNetConnectionEnd_Misc_SteamConnectivity = 5005,
		// Token: 0x040009BA RID: 2490
		k_ESteamNetConnectionEnd_Misc_NoRelaySessionsToClient,
		// Token: 0x040009BB RID: 2491
		k_ESteamNetConnectionEnd_Misc_P2P_Rendezvous = 5008,
		// Token: 0x040009BC RID: 2492
		k_ESteamNetConnectionEnd_Misc_P2P_NAT_Firewall,
		// Token: 0x040009BD RID: 2493
		k_ESteamNetConnectionEnd_Misc_PeerSentNoConnection,
		// Token: 0x040009BE RID: 2494
		k_ESteamNetConnectionEnd_Misc_Max = 5999,
		// Token: 0x040009BF RID: 2495
		k_ESteamNetConnectionEnd__Force32Bit = 2147483647
	}
}
