using System;

namespace Steamworks
{
	// Token: 0x02000178 RID: 376
	public static class SteamGameServerNetworkingMessages
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0000747F File Offset: 0x0000567F
		public static EResult SendMessageToUser(ref SteamNetworkingIdentity identityRemote, IntPtr pubData, uint cubData, int nSendFlags, int nRemoteChannel)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingMessages_SendMessageToUser(CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote, pubData, cubData, nSendFlags, nRemoteChannel);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00007496 File Offset: 0x00005696
		public static int ReceiveMessagesOnChannel(int nLocalChannel, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableGameServer();
			if (ppOutMessages != null && ppOutMessages.Length != nMaxMessages)
			{
				throw new ArgumentException("ppOutMessages must be the same size as nMaxMessages!");
			}
			return NativeMethods.ISteamNetworkingMessages_ReceiveMessagesOnChannel(CSteamGameServerAPIContext.GetSteamNetworkingMessages(), nLocalChannel, ppOutMessages, nMaxMessages);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000074BE File Offset: 0x000056BE
		public static bool AcceptSessionWithUser(ref SteamNetworkingIdentity identityRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingMessages_AcceptSessionWithUser(CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000074D0 File Offset: 0x000056D0
		public static bool CloseSessionWithUser(ref SteamNetworkingIdentity identityRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingMessages_CloseSessionWithUser(CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000074E2 File Offset: 0x000056E2
		public static bool CloseChannelWithUser(ref SteamNetworkingIdentity identityRemote, int nLocalChannel)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingMessages_CloseChannelWithUser(CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote, nLocalChannel);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000074F5 File Offset: 0x000056F5
		public static ESteamNetworkingConnectionState GetSessionConnectionInfo(ref SteamNetworkingIdentity identityRemote, out SteamNetConnectionInfo_t pConnectionInfo, out SteamNetConnectionRealTimeStatus_t pQuickStatus)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingMessages_GetSessionConnectionInfo(CSteamGameServerAPIContext.GetSteamNetworkingMessages(), ref identityRemote, out pConnectionInfo, out pQuickStatus);
		}
	}
}
