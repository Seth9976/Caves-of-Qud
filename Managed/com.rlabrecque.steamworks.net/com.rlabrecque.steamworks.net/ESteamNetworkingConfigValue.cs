using System;

namespace Steamworks
{
	// Token: 0x0200015B RID: 347
	public enum ESteamNetworkingConfigValue
	{
		// Token: 0x040009CE RID: 2510
		k_ESteamNetworkingConfig_Invalid,
		// Token: 0x040009CF RID: 2511
		k_ESteamNetworkingConfig_TimeoutInitial = 24,
		// Token: 0x040009D0 RID: 2512
		k_ESteamNetworkingConfig_TimeoutConnected,
		// Token: 0x040009D1 RID: 2513
		k_ESteamNetworkingConfig_SendBufferSize = 9,
		// Token: 0x040009D2 RID: 2514
		k_ESteamNetworkingConfig_RecvBufferSize = 47,
		// Token: 0x040009D3 RID: 2515
		k_ESteamNetworkingConfig_RecvBufferMessages,
		// Token: 0x040009D4 RID: 2516
		k_ESteamNetworkingConfig_RecvMaxMessageSize,
		// Token: 0x040009D5 RID: 2517
		k_ESteamNetworkingConfig_RecvMaxSegmentsPerPacket,
		// Token: 0x040009D6 RID: 2518
		k_ESteamNetworkingConfig_ConnectionUserData = 40,
		// Token: 0x040009D7 RID: 2519
		k_ESteamNetworkingConfig_SendRateMin = 10,
		// Token: 0x040009D8 RID: 2520
		k_ESteamNetworkingConfig_SendRateMax,
		// Token: 0x040009D9 RID: 2521
		k_ESteamNetworkingConfig_NagleTime,
		// Token: 0x040009DA RID: 2522
		k_ESteamNetworkingConfig_IP_AllowWithoutAuth = 23,
		// Token: 0x040009DB RID: 2523
		k_ESteamNetworkingConfig_IPLocalHost_AllowWithoutAuth = 52,
		// Token: 0x040009DC RID: 2524
		k_ESteamNetworkingConfig_MTU_PacketSize = 32,
		// Token: 0x040009DD RID: 2525
		k_ESteamNetworkingConfig_MTU_DataSize,
		// Token: 0x040009DE RID: 2526
		k_ESteamNetworkingConfig_Unencrypted,
		// Token: 0x040009DF RID: 2527
		k_ESteamNetworkingConfig_SymmetricConnect = 37,
		// Token: 0x040009E0 RID: 2528
		k_ESteamNetworkingConfig_LocalVirtualPort,
		// Token: 0x040009E1 RID: 2529
		k_ESteamNetworkingConfig_DualWifi_Enable,
		// Token: 0x040009E2 RID: 2530
		k_ESteamNetworkingConfig_EnableDiagnosticsUI = 46,
		// Token: 0x040009E3 RID: 2531
		k_ESteamNetworkingConfig_FakePacketLoss_Send = 2,
		// Token: 0x040009E4 RID: 2532
		k_ESteamNetworkingConfig_FakePacketLoss_Recv,
		// Token: 0x040009E5 RID: 2533
		k_ESteamNetworkingConfig_FakePacketLag_Send,
		// Token: 0x040009E6 RID: 2534
		k_ESteamNetworkingConfig_FakePacketLag_Recv,
		// Token: 0x040009E7 RID: 2535
		k_ESteamNetworkingConfig_FakePacketReorder_Send,
		// Token: 0x040009E8 RID: 2536
		k_ESteamNetworkingConfig_FakePacketReorder_Recv,
		// Token: 0x040009E9 RID: 2537
		k_ESteamNetworkingConfig_FakePacketReorder_Time,
		// Token: 0x040009EA RID: 2538
		k_ESteamNetworkingConfig_FakePacketDup_Send = 26,
		// Token: 0x040009EB RID: 2539
		k_ESteamNetworkingConfig_FakePacketDup_Recv,
		// Token: 0x040009EC RID: 2540
		k_ESteamNetworkingConfig_FakePacketDup_TimeMax,
		// Token: 0x040009ED RID: 2541
		k_ESteamNetworkingConfig_PacketTraceMaxBytes = 41,
		// Token: 0x040009EE RID: 2542
		k_ESteamNetworkingConfig_FakeRateLimit_Send_Rate,
		// Token: 0x040009EF RID: 2543
		k_ESteamNetworkingConfig_FakeRateLimit_Send_Burst,
		// Token: 0x040009F0 RID: 2544
		k_ESteamNetworkingConfig_FakeRateLimit_Recv_Rate,
		// Token: 0x040009F1 RID: 2545
		k_ESteamNetworkingConfig_FakeRateLimit_Recv_Burst,
		// Token: 0x040009F2 RID: 2546
		k_ESteamNetworkingConfig_OutOfOrderCorrectionWindowMicroseconds = 51,
		// Token: 0x040009F3 RID: 2547
		k_ESteamNetworkingConfig_Callback_ConnectionStatusChanged = 201,
		// Token: 0x040009F4 RID: 2548
		k_ESteamNetworkingConfig_Callback_AuthStatusChanged,
		// Token: 0x040009F5 RID: 2549
		k_ESteamNetworkingConfig_Callback_RelayNetworkStatusChanged,
		// Token: 0x040009F6 RID: 2550
		k_ESteamNetworkingConfig_Callback_MessagesSessionRequest,
		// Token: 0x040009F7 RID: 2551
		k_ESteamNetworkingConfig_Callback_MessagesSessionFailed,
		// Token: 0x040009F8 RID: 2552
		k_ESteamNetworkingConfig_Callback_CreateConnectionSignaling,
		// Token: 0x040009F9 RID: 2553
		k_ESteamNetworkingConfig_Callback_FakeIPResult,
		// Token: 0x040009FA RID: 2554
		k_ESteamNetworkingConfig_P2P_STUN_ServerList = 103,
		// Token: 0x040009FB RID: 2555
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Enable,
		// Token: 0x040009FC RID: 2556
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Penalty,
		// Token: 0x040009FD RID: 2557
		k_ESteamNetworkingConfig_P2P_Transport_SDR_Penalty,
		// Token: 0x040009FE RID: 2558
		k_ESteamNetworkingConfig_P2P_TURN_ServerList,
		// Token: 0x040009FF RID: 2559
		k_ESteamNetworkingConfig_P2P_TURN_UserList,
		// Token: 0x04000A00 RID: 2560
		k_ESteamNetworkingConfig_P2P_TURN_PassList,
		// Token: 0x04000A01 RID: 2561
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Implementation,
		// Token: 0x04000A02 RID: 2562
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
		// Token: 0x04000A03 RID: 2563
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFail,
		// Token: 0x04000A04 RID: 2564
		k_ESteamNetworkingConfig_SDRClient_MinPingsBeforePingAccurate,
		// Token: 0x04000A05 RID: 2565
		k_ESteamNetworkingConfig_SDRClient_SingleSocket,
		// Token: 0x04000A06 RID: 2566
		k_ESteamNetworkingConfig_SDRClient_ForceRelayCluster = 29,
		// Token: 0x04000A07 RID: 2567
		k_ESteamNetworkingConfig_SDRClient_DevTicket,
		// Token: 0x04000A08 RID: 2568
		k_ESteamNetworkingConfig_SDRClient_ForceProxyAddr,
		// Token: 0x04000A09 RID: 2569
		k_ESteamNetworkingConfig_SDRClient_FakeClusterPing = 36,
		// Token: 0x04000A0A RID: 2570
		k_ESteamNetworkingConfig_SDRClient_LimitPingProbesToNearestN = 60,
		// Token: 0x04000A0B RID: 2571
		k_ESteamNetworkingConfig_LogLevel_AckRTT = 13,
		// Token: 0x04000A0C RID: 2572
		k_ESteamNetworkingConfig_LogLevel_PacketDecode,
		// Token: 0x04000A0D RID: 2573
		k_ESteamNetworkingConfig_LogLevel_Message,
		// Token: 0x04000A0E RID: 2574
		k_ESteamNetworkingConfig_LogLevel_PacketGaps,
		// Token: 0x04000A0F RID: 2575
		k_ESteamNetworkingConfig_LogLevel_P2PRendezvous,
		// Token: 0x04000A10 RID: 2576
		k_ESteamNetworkingConfig_LogLevel_SDRRelayPings,
		// Token: 0x04000A11 RID: 2577
		k_ESteamNetworkingConfig_ECN = 999,
		// Token: 0x04000A12 RID: 2578
		k_ESteamNetworkingConfig_DELETED_EnumerateDevVars = 35,
		// Token: 0x04000A13 RID: 2579
		k_ESteamNetworkingConfigValue__Force32Bit = 2147483647
	}
}
