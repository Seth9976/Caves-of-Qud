using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000012 RID: 18
	public static class SteamAPI
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000343C File Offset: 0x0000163C
		public static ESteamAPIInitResult InitEx(out string OutSteamErrMsg)
		{
			InteropHelp.TestIfPlatformSupported();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SteamUtils010").Append("\0");
			stringBuilder.Append("SteamNetworkingUtils004").Append("\0");
			stringBuilder.Append("STEAMAPPS_INTERFACE_VERSION008").Append("\0");
			stringBuilder.Append("SteamFriends017").Append("\0");
			stringBuilder.Append("SteamMatchGameSearch001").Append("\0");
			stringBuilder.Append("STEAMHTMLSURFACE_INTERFACE_VERSION_005").Append("\0");
			stringBuilder.Append("STEAMHTTP_INTERFACE_VERSION003").Append("\0");
			stringBuilder.Append("SteamInput006").Append("\0");
			stringBuilder.Append("STEAMINVENTORY_INTERFACE_V003").Append("\0");
			stringBuilder.Append("SteamMatchMakingServers002").Append("\0");
			stringBuilder.Append("SteamMatchMaking009").Append("\0");
			stringBuilder.Append("STEAMMUSICREMOTE_INTERFACE_VERSION001").Append("\0");
			stringBuilder.Append("STEAMMUSIC_INTERFACE_VERSION001").Append("\0");
			stringBuilder.Append("SteamNetworkingMessages002").Append("\0");
			stringBuilder.Append("SteamNetworkingSockets012").Append("\0");
			stringBuilder.Append("SteamNetworking006").Append("\0");
			stringBuilder.Append("STEAMPARENTALSETTINGS_INTERFACE_VERSION001").Append("\0");
			stringBuilder.Append("SteamParties002").Append("\0");
			stringBuilder.Append("STEAMREMOTEPLAY_INTERFACE_VERSION002").Append("\0");
			stringBuilder.Append("STEAMREMOTESTORAGE_INTERFACE_VERSION016").Append("\0");
			stringBuilder.Append("STEAMSCREENSHOTS_INTERFACE_VERSION003").Append("\0");
			stringBuilder.Append("STEAMUGC_INTERFACE_VERSION020").Append("\0");
			stringBuilder.Append("STEAMUSERSTATS_INTERFACE_VERSION012").Append("\0");
			stringBuilder.Append("SteamUser023").Append("\0");
			stringBuilder.Append("STEAMVIDEO_INTERFACE_V007").Append("\0");
			ESteamAPIInitResult esteamAPIInitResult2;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(stringBuilder.ToString()))
			{
				IntPtr intPtr = Marshal.AllocHGlobal(1024);
				ESteamAPIInitResult esteamAPIInitResult = NativeMethods.SteamInternal_SteamAPI_Init(utf8StringHandle, intPtr);
				OutSteamErrMsg = InteropHelp.PtrToStringUTF8(intPtr);
				Marshal.FreeHGlobal(intPtr);
				if (esteamAPIInitResult == ESteamAPIInitResult.k_ESteamAPIInitResult_OK)
				{
					if (CSteamAPIContext.Init())
					{
						CallbackDispatcher.Initialize();
					}
					else
					{
						esteamAPIInitResult = ESteamAPIInitResult.k_ESteamAPIInitResult_FailedGeneric;
						OutSteamErrMsg = "[Steamworks.NET] Failed to initialize CSteamAPIContext";
					}
				}
				esteamAPIInitResult2 = esteamAPIInitResult;
			}
			return esteamAPIInitResult2;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036E0 File Offset: 0x000018E0
		public static bool Init()
		{
			InteropHelp.TestIfPlatformSupported();
			string text;
			return SteamAPI.InitEx(out text) == ESteamAPIInitResult.k_ESteamAPIInitResult_OK;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000036FC File Offset: 0x000018FC
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_Shutdown();
			CSteamAPIContext.Clear();
			CallbackDispatcher.Shutdown();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003712 File Offset: 0x00001912
		public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_RestartAppIfNecessary(unOwnAppID);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000371F File Offset: 0x0000191F
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamAPI_ReleaseCurrentThreadMemory();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000372B File Offset: 0x0000192B
		public static void RunCallbacks()
		{
			CallbackDispatcher.RunFrame(false);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003733 File Offset: 0x00001933
		public static bool IsSteamRunning()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamAPI_IsSteamRunning();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000373F File Offset: 0x0000193F
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamAPI_GetHSteamPipe();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003750 File Offset: 0x00001950
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamAPI_GetHSteamUser();
		}
	}
}
