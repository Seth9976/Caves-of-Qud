using System;

namespace Steamworks
{
	// Token: 0x0200018D RID: 397
	public static class SteamRemotePlay
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0000BD32 File Offset: 0x00009F32
		public static uint GetSessionCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_GetSessionCount(CSteamAPIContext.GetSteamRemotePlay());
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0000BD43 File Offset: 0x00009F43
		public static RemotePlaySessionID_t GetSessionID(int iSessionIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (RemotePlaySessionID_t)NativeMethods.ISteamRemotePlay_GetSessionID(CSteamAPIContext.GetSteamRemotePlay(), iSessionIndex);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0000BD5A File Offset: 0x00009F5A
		public static CSteamID GetSessionSteamID(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamRemotePlay_GetSessionSteamID(CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0000BD71 File Offset: 0x00009F71
		public static string GetSessionClientName(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamRemotePlay_GetSessionClientName(CSteamAPIContext.GetSteamRemotePlay(), unSessionID));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0000BD88 File Offset: 0x00009F88
		public static ESteamDeviceFormFactor GetSessionClientFormFactor(RemotePlaySessionID_t unSessionID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_GetSessionClientFormFactor(CSteamAPIContext.GetSteamRemotePlay(), unSessionID);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0000BD9A File Offset: 0x00009F9A
		public static bool BGetSessionClientResolution(RemotePlaySessionID_t unSessionID, out int pnResolutionX, out int pnResolutionY)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BGetSessionClientResolution(CSteamAPIContext.GetSteamRemotePlay(), unSessionID, out pnResolutionX, out pnResolutionY);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0000BDAE File Offset: 0x00009FAE
		public static bool BStartRemotePlayTogether(bool bShowOverlay = true)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BStartRemotePlayTogether(CSteamAPIContext.GetSteamRemotePlay(), bShowOverlay);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public static bool BSendRemotePlayTogetherInvite(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamRemotePlay_BSendRemotePlayTogetherInvite(CSteamAPIContext.GetSteamRemotePlay(), steamIDFriend);
		}
	}
}
