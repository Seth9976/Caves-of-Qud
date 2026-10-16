using System;

namespace Steamworks
{
	// Token: 0x02000177 RID: 375
	public static class SteamGameServerNetworking
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x000072B8 File Offset: 0x000054B8
		public static bool SendP2PPacket(CSteamID steamIDRemote, byte[] pubData, uint cubData, EP2PSend eP2PSendType, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_SendP2PPacket(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, pubData, cubData, eP2PSendType, nChannel);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000072CF File Offset: 0x000054CF
		public static bool IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_IsP2PPacketAvailable(CSteamGameServerAPIContext.GetSteamNetworking(), out pcubMsgSize, nChannel);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000072E2 File Offset: 0x000054E2
		public static bool ReadP2PPacket(byte[] pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_ReadP2PPacket(CSteamGameServerAPIContext.GetSteamNetworking(), pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000072F9 File Offset: 0x000054F9
		public static bool AcceptP2PSessionWithUser(CSteamID steamIDRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_AcceptP2PSessionWithUser(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000730B File Offset: 0x0000550B
		public static bool CloseP2PSessionWithUser(CSteamID steamIDRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_CloseP2PSessionWithUser(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000731D File Offset: 0x0000551D
		public static bool CloseP2PChannelWithUser(CSteamID steamIDRemote, int nChannel)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_CloseP2PChannelWithUser(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, nChannel);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00007330 File Offset: 0x00005530
		public static bool GetP2PSessionState(CSteamID steamIDRemote, out P2PSessionState_t pConnectionState)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetP2PSessionState(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, out pConnectionState);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00007343 File Offset: 0x00005543
		public static bool AllowP2PPacketRelay(bool bAllow)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_AllowP2PPacketRelay(CSteamGameServerAPIContext.GetSteamNetworking(), bAllow);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00007355 File Offset: 0x00005555
		public static SNetListenSocket_t CreateListenSocket(int nVirtualP2PPort, SteamIPAddress_t nIP, ushort nPort, bool bAllowUseOfPacketRelay)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetListenSocket_t)NativeMethods.ISteamNetworking_CreateListenSocket(CSteamGameServerAPIContext.GetSteamNetworking(), nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000736F File Offset: 0x0000556F
		public static SNetSocket_t CreateP2PConnectionSocket(CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetSocket_t)NativeMethods.ISteamNetworking_CreateP2PConnectionSocket(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00007389 File Offset: 0x00005589
		public static SNetSocket_t CreateConnectionSocket(SteamIPAddress_t nIP, ushort nPort, int nTimeoutSec)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetSocket_t)NativeMethods.ISteamNetworking_CreateConnectionSocket(CSteamGameServerAPIContext.GetSteamNetworking(), nIP, nPort, nTimeoutSec);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000073A2 File Offset: 0x000055A2
		public static bool DestroySocket(SNetSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_DestroySocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, bNotifyRemoteEnd);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000073B5 File Offset: 0x000055B5
		public static bool DestroyListenSocket(SNetListenSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_DestroyListenSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, bNotifyRemoteEnd);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000073C8 File Offset: 0x000055C8
		public static bool SendDataOnSocket(SNetSocket_t hSocket, byte[] pubData, uint cubData, bool bReliable)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_SendDataOnSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, pubData, cubData, bReliable);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000073DD File Offset: 0x000055DD
		public static bool IsDataAvailableOnSocket(SNetSocket_t hSocket, out uint pcubMsgSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_IsDataAvailableOnSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, out pcubMsgSize);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000073F0 File Offset: 0x000055F0
		public static bool RetrieveDataFromSocket(SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_RetrieveDataFromSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, pubDest, cubDest, out pcubMsgSize);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00007405 File Offset: 0x00005605
		public static bool IsDataAvailable(SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out SNetSocket_t phSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_IsDataAvailable(CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, out pcubMsgSize, out phSocket);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00007419 File Offset: 0x00005619
		public static bool RetrieveData(SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out SNetSocket_t phSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_RetrieveData(CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, pubDest, cubDest, out pcubMsgSize, out phSocket);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00007430 File Offset: 0x00005630
		public static bool GetSocketInfo(SNetSocket_t hSocket, out CSteamID pSteamIDRemote, out int peSocketStatus, out SteamIPAddress_t punIPRemote, out ushort punPortRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetSocketInfo(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, out pSteamIDRemote, out peSocketStatus, out punIPRemote, out punPortRemote);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00007447 File Offset: 0x00005647
		public static bool GetListenSocketInfo(SNetListenSocket_t hListenSocket, out SteamIPAddress_t pnIP, out ushort pnPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetListenSocketInfo(CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, out pnIP, out pnPort);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000745B File Offset: 0x0000565B
		public static ESNetSocketConnectionType GetSocketConnectionType(SNetSocket_t hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetSocketConnectionType(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000746D File Offset: 0x0000566D
		public static int GetMaxPacketSize(SNetSocket_t hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetMaxPacketSize(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket);
		}
	}
}
