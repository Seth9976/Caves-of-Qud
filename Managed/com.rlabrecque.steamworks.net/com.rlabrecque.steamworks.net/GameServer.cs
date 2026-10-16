using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000013 RID: 19
	public static class GameServer
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003764 File Offset: 0x00001964
		public static ESteamAPIInitResult InitEx(uint unIP, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString, out string OutSteamErrMsg)
		{
			InteropHelp.TestIfPlatformSupported();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SteamUtils010").Append('\0');
			stringBuilder.Append("SteamNetworkingUtils004").Append('\0');
			stringBuilder.Append("SteamGameServer015").Append('\0');
			stringBuilder.Append("SteamGameServerStats001").Append('\0');
			stringBuilder.Append("STEAMHTTP_INTERFACE_VERSION003").Append('\0');
			stringBuilder.Append("STEAMINVENTORY_INTERFACE_V003").Append('\0');
			stringBuilder.Append("SteamNetworking006").Append('\0');
			stringBuilder.Append("SteamNetworkingMessages002").Append('\0');
			stringBuilder.Append("SteamNetworkingSockets012").Append('\0');
			stringBuilder.Append("STEAMUGC_INTERFACE_VERSION020").Append('\0');
			ESteamAPIInitResult esteamAPIInitResult2;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchVersionString))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(stringBuilder.ToString()))
				{
					IntPtr intPtr = Marshal.AllocHGlobal(1024);
					ESteamAPIInitResult esteamAPIInitResult = NativeMethods.SteamInternal_GameServer_Init_V2(unIP, usGamePort, usQueryPort, eServerMode, utf8StringHandle, utf8StringHandle2, intPtr);
					OutSteamErrMsg = InteropHelp.PtrToStringUTF8(intPtr);
					Marshal.FreeHGlobal(intPtr);
					if (esteamAPIInitResult == ESteamAPIInitResult.k_ESteamAPIInitResult_OK)
					{
						if (CSteamGameServerAPIContext.Init())
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
			}
			return esteamAPIInitResult2;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000038C4 File Offset: 0x00001AC4
		public static bool Init(uint unIP, ushort usGamePort, ushort usQueryPort, EServerMode eServerMode, string pchVersionString)
		{
			InteropHelp.TestIfPlatformSupported();
			string text;
			return GameServer.InitEx(unIP, usGamePort, usQueryPort, eServerMode, pchVersionString, out text) == ESteamAPIInitResult.k_ESteamAPIInitResult_OK;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000038E6 File Offset: 0x00001AE6
		public static void Shutdown()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_Shutdown();
			CSteamGameServerAPIContext.Clear();
			CallbackDispatcher.Shutdown();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000038FC File Offset: 0x00001AFC
		public static void RunCallbacks()
		{
			CallbackDispatcher.RunFrame(true);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003904 File Offset: 0x00001B04
		public static void ReleaseCurrentThreadMemory()
		{
			InteropHelp.TestIfPlatformSupported();
			NativeMethods.SteamGameServer_ReleaseCurrentThreadMemory();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003910 File Offset: 0x00001B10
		public static bool BSecure()
		{
			InteropHelp.TestIfPlatformSupported();
			return NativeMethods.SteamGameServer_BSecure();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000391C File Offset: 0x00001B1C
		public static CSteamID GetSteamID()
		{
			InteropHelp.TestIfPlatformSupported();
			return (CSteamID)NativeMethods.SteamGameServer_GetSteamID();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000392D File Offset: 0x00001B2D
		public static HSteamPipe GetHSteamPipe()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamPipe)NativeMethods.SteamGameServer_GetHSteamPipe();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000393E File Offset: 0x00001B3E
		public static HSteamUser GetHSteamUser()
		{
			InteropHelp.TestIfPlatformSupported();
			return (HSteamUser)NativeMethods.SteamGameServer_GetHSteamUser();
		}
	}
}
