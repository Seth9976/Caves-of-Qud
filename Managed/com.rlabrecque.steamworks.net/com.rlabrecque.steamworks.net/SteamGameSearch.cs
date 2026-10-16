using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000184 RID: 388
	public static class SteamGameSearch
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x0000AB64 File Offset: 0x00008D64
		public static EGameSearchErrorCode_t AddGameSearchParams(string pchKeyToFind, string pchValuesToFind)
		{
			InteropHelp.TestIfAvailableClient();
			EGameSearchErrorCode_t egameSearchErrorCode_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKeyToFind))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValuesToFind))
				{
					egameSearchErrorCode_t = NativeMethods.ISteamGameSearch_AddGameSearchParams(CSteamAPIContext.GetSteamGameSearch(), utf8StringHandle, utf8StringHandle2);
				}
			}
			return egameSearchErrorCode_t;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		public static EGameSearchErrorCode_t SearchForGameWithLobby(CSteamID steamIDLobby, int nPlayerMin, int nPlayerMax)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_SearchForGameWithLobby(CSteamAPIContext.GetSteamGameSearch(), steamIDLobby, nPlayerMin, nPlayerMax);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public static EGameSearchErrorCode_t SearchForGameSolo(int nPlayerMin, int nPlayerMax)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_SearchForGameSolo(CSteamAPIContext.GetSteamGameSearch(), nPlayerMin, nPlayerMax);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000ABEB File Offset: 0x00008DEB
		public static EGameSearchErrorCode_t AcceptGame()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_AcceptGame(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000ABFC File Offset: 0x00008DFC
		public static EGameSearchErrorCode_t DeclineGame()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_DeclineGame(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000AC10 File Offset: 0x00008E10
		public static EGameSearchErrorCode_t RetrieveConnectionDetails(CSteamID steamIDHost, out string pchConnectionDetails, int cubConnectionDetails)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubConnectionDetails);
			EGameSearchErrorCode_t egameSearchErrorCode_t = NativeMethods.ISteamGameSearch_RetrieveConnectionDetails(CSteamAPIContext.GetSteamGameSearch(), steamIDHost, intPtr, cubConnectionDetails);
			pchConnectionDetails = ((egameSearchErrorCode_t != (EGameSearchErrorCode_t)0) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return egameSearchErrorCode_t;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public static EGameSearchErrorCode_t EndGameSearch()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_EndGameSearch(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000AC60 File Offset: 0x00008E60
		public static EGameSearchErrorCode_t SetGameHostParams(string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			EGameSearchErrorCode_t egameSearchErrorCode_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					egameSearchErrorCode_t = NativeMethods.ISteamGameSearch_SetGameHostParams(CSteamAPIContext.GetSteamGameSearch(), utf8StringHandle, utf8StringHandle2);
				}
			}
			return egameSearchErrorCode_t;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		public static EGameSearchErrorCode_t SetConnectionDetails(string pchConnectionDetails, int cubConnectionDetails)
		{
			InteropHelp.TestIfAvailableClient();
			EGameSearchErrorCode_t egameSearchErrorCode_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectionDetails))
			{
				egameSearchErrorCode_t = NativeMethods.ISteamGameSearch_SetConnectionDetails(CSteamAPIContext.GetSteamGameSearch(), utf8StringHandle, cubConnectionDetails);
			}
			return egameSearchErrorCode_t;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000AD04 File Offset: 0x00008F04
		public static EGameSearchErrorCode_t RequestPlayersForGame(int nPlayerMin, int nPlayerMax, int nMaxTeamSize)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_RequestPlayersForGame(CSteamAPIContext.GetSteamGameSearch(), nPlayerMin, nPlayerMax, nMaxTeamSize);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000AD18 File Offset: 0x00008F18
		public static EGameSearchErrorCode_t HostConfirmGameStart(ulong ullUniqueGameID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_HostConfirmGameStart(CSteamAPIContext.GetSteamGameSearch(), ullUniqueGameID);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000AD2A File Offset: 0x00008F2A
		public static EGameSearchErrorCode_t CancelRequestPlayersForGame()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_CancelRequestPlayersForGame(CSteamAPIContext.GetSteamGameSearch());
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000AD3B File Offset: 0x00008F3B
		public static EGameSearchErrorCode_t SubmitPlayerResult(ulong ullUniqueGameID, CSteamID steamIDPlayer, EPlayerResult_t EPlayerResult)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_SubmitPlayerResult(CSteamAPIContext.GetSteamGameSearch(), ullUniqueGameID, steamIDPlayer, EPlayerResult);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000AD4F File Offset: 0x00008F4F
		public static EGameSearchErrorCode_t EndGame(ulong ullUniqueGameID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamGameSearch_EndGame(CSteamAPIContext.GetSteamGameSearch(), ullUniqueGameID);
		}
	}
}
