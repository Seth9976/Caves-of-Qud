using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000170 RID: 368
	public static class SteamApps
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x000047C7 File Offset: 0x000029C7
		public static bool BIsSubscribed()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribed(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000047D8 File Offset: 0x000029D8
		public static bool BIsLowViolence()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsLowViolence(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000047E9 File Offset: 0x000029E9
		public static bool BIsCybercafe()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsCybercafe(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000047FA File Offset: 0x000029FA
		public static bool BIsVACBanned()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsVACBanned(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000480B File Offset: 0x00002A0B
		public static string GetCurrentGameLanguage()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamApps_GetCurrentGameLanguage(CSteamAPIContext.GetSteamApps()));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00004821 File Offset: 0x00002A21
		public static string GetAvailableGameLanguages()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamApps_GetAvailableGameLanguages(CSteamAPIContext.GetSteamApps()));
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00004837 File Offset: 0x00002A37
		public static bool BIsSubscribedApp(AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribedApp(CSteamAPIContext.GetSteamApps(), appID);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00004849 File Offset: 0x00002A49
		public static bool BIsDlcInstalled(AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsDlcInstalled(CSteamAPIContext.GetSteamApps(), appID);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000485B File Offset: 0x00002A5B
		public static uint GetEarliestPurchaseUnixTime(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetEarliestPurchaseUnixTime(CSteamAPIContext.GetSteamApps(), nAppID);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000486D File Offset: 0x00002A6D
		public static bool BIsSubscribedFromFreeWeekend()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribedFromFreeWeekend(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000487E File Offset: 0x00002A7E
		public static int GetDLCCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetDLCCount(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00004890 File Offset: 0x00002A90
		public static bool BGetDLCDataByIndex(int iDLC, out AppId_t pAppID, out bool pbAvailable, out string pchName, int cchNameBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchNameBufferSize);
			bool flag = NativeMethods.ISteamApps_BGetDLCDataByIndex(CSteamAPIContext.GetSteamApps(), iDLC, out pAppID, out pbAvailable, intPtr, cchNameBufferSize);
			pchName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000048D0 File Offset: 0x00002AD0
		public static void InstallDLC(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_InstallDLC(CSteamAPIContext.GetSteamApps(), nAppID);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000048E2 File Offset: 0x00002AE2
		public static void UninstallDLC(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_UninstallDLC(CSteamAPIContext.GetSteamApps(), nAppID);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000048F4 File Offset: 0x00002AF4
		public static void RequestAppProofOfPurchaseKey(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_RequestAppProofOfPurchaseKey(CSteamAPIContext.GetSteamApps(), nAppID);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00004908 File Offset: 0x00002B08
		public static bool GetCurrentBetaName(out string pchName, int cchNameBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchNameBufferSize);
			bool flag = NativeMethods.ISteamApps_GetCurrentBetaName(CSteamAPIContext.GetSteamApps(), intPtr, cchNameBufferSize);
			pchName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00004943 File Offset: 0x00002B43
		public static bool MarkContentCorrupt(bool bMissingFilesOnly)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_MarkContentCorrupt(CSteamAPIContext.GetSteamApps(), bMissingFilesOnly);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00004955 File Offset: 0x00002B55
		public static uint GetInstalledDepots(AppId_t appID, DepotId_t[] pvecDepots, uint cMaxDepots)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetInstalledDepots(CSteamAPIContext.GetSteamApps(), appID, pvecDepots, cMaxDepots);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000496C File Offset: 0x00002B6C
		public static uint GetAppInstallDir(AppId_t appID, out string pchFolder, uint cchFolderBufferSize)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal((int)cchFolderBufferSize);
			uint num = NativeMethods.ISteamApps_GetAppInstallDir(CSteamAPIContext.GetSteamApps(), appID, intPtr, cchFolderBufferSize);
			pchFolder = ((num != 0U) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000049A8 File Offset: 0x00002BA8
		public static bool BIsAppInstalled(AppId_t appID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsAppInstalled(CSteamAPIContext.GetSteamApps(), appID);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000049BA File Offset: 0x00002BBA
		public static CSteamID GetAppOwner()
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamApps_GetAppOwner(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000049D0 File Offset: 0x00002BD0
		public static string GetLaunchQueryParam(string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string text;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				text = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamApps_GetLaunchQueryParam(CSteamAPIContext.GetSteamApps(), utf8StringHandle));
			}
			return text;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00004A18 File Offset: 0x00002C18
		public static bool GetDlcDownloadProgress(AppId_t nAppID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetDlcDownloadProgress(CSteamAPIContext.GetSteamApps(), nAppID, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00004A2C File Offset: 0x00002C2C
		public static int GetAppBuildId()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetAppBuildId(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00004A3D File Offset: 0x00002C3D
		public static void RequestAllProofOfPurchaseKeys()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamApps_RequestAllProofOfPurchaseKeys(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00004A50 File Offset: 0x00002C50
		public static SteamAPICall_t GetFileDetails(string pszFileName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t steamAPICall_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszFileName))
			{
				steamAPICall_t = (SteamAPICall_t)NativeMethods.ISteamApps_GetFileDetails(CSteamAPIContext.GetSteamApps(), utf8StringHandle);
			}
			return steamAPICall_t;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00004A98 File Offset: 0x00002C98
		public static int GetLaunchCommandLine(out string pszCommandLine, int cubCommandLine)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubCommandLine);
			int num = NativeMethods.ISteamApps_GetLaunchCommandLine(CSteamAPIContext.GetSteamApps(), intPtr, cubCommandLine);
			pszCommandLine = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static bool BIsSubscribedFromFamilySharing()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsSubscribedFromFamilySharing(CSteamAPIContext.GetSteamApps());
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00004AE5 File Offset: 0x00002CE5
		public static bool BIsTimedTrial(out uint punSecondsAllowed, out uint punSecondsPlayed)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_BIsTimedTrial(CSteamAPIContext.GetSteamApps(), out punSecondsAllowed, out punSecondsPlayed);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public static bool SetDlcContext(AppId_t nAppID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_SetDlcContext(CSteamAPIContext.GetSteamApps(), nAppID);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00004B0A File Offset: 0x00002D0A
		public static int GetNumBetas(out int pnAvailable, out int pnPrivate)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamApps_GetNumBetas(CSteamAPIContext.GetSteamApps(), out pnAvailable, out pnPrivate);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00004B20 File Offset: 0x00002D20
		public static bool GetBetaInfo(int iBetaIndex, out uint punFlags, out uint punBuildID, out string pchBetaName, int cchBetaName, out string pchDescription, int cchDescription)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchBetaName);
			IntPtr intPtr2 = Marshal.AllocHGlobal(cchDescription);
			bool flag = NativeMethods.ISteamApps_GetBetaInfo(CSteamAPIContext.GetSteamApps(), iBetaIndex, out punFlags, out punBuildID, intPtr, cchBetaName, intPtr2, cchDescription);
			pchBetaName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			pchDescription = (flag ? InteropHelp.PtrToStringUTF8(intPtr2) : null);
			Marshal.FreeHGlobal(intPtr2);
			return flag;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00004B80 File Offset: 0x00002D80
		public static bool SetActiveBeta(string pchBetaName)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchBetaName))
			{
				flag = NativeMethods.ISteamApps_SetActiveBeta(CSteamAPIContext.GetSteamApps(), utf8StringHandle);
			}
			return flag;
		}
	}
}
