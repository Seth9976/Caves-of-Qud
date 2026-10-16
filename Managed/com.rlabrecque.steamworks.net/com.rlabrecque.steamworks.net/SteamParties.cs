using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000185 RID: 389
	public static class SteamParties
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0000AD61 File Offset: 0x00008F61
		public static uint GetNumActiveBeacons()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetNumActiveBeacons(CSteamAPIContext.GetSteamParties());
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000AD72 File Offset: 0x00008F72
		public static PartyBeaconID_t GetBeaconByIndex(uint unIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (PartyBeaconID_t)NativeMethods.ISteamParties_GetBeaconByIndex(CSteamAPIContext.GetSteamParties(), unIndex);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000AD8C File Offset: 0x00008F8C
		public static bool GetBeaconDetails(PartyBeaconID_t ulBeaconID, out CSteamID pSteamIDBeaconOwner, out SteamPartyBeaconLocation_t pLocation, out string pchMetadata, int cchMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchMetadata);
			bool flag = NativeMethods.ISteamParties_GetBeaconDetails(CSteamAPIContext.GetSteamParties(), ulBeaconID, out pSteamIDBeaconOwner, out pLocation, intPtr, cchMetadata);
			pchMetadata = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public static SteamAPICall_t JoinParty(PartyBeaconID_t ulBeaconID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamParties_JoinParty(CSteamAPIContext.GetSteamParties(), ulBeaconID);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000ADE3 File Offset: 0x00008FE3
		public static bool GetNumAvailableBeaconLocations(out uint puNumLocations)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetNumAvailableBeaconLocations(CSteamAPIContext.GetSteamParties(), out puNumLocations);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000ADF5 File Offset: 0x00008FF5
		public static bool GetAvailableBeaconLocations(SteamPartyBeaconLocation_t[] pLocationList, uint uMaxNumLocations)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_GetAvailableBeaconLocations(CSteamAPIContext.GetSteamParties(), pLocationList, uMaxNumLocations);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000AE08 File Offset: 0x00009008
		public static SteamAPICall_t CreateBeacon(uint unOpenSlots, ref SteamPartyBeaconLocation_t pBeaconLocation, string pchConnectString, string pchMetadata)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t steamAPICall_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchMetadata))
				{
					steamAPICall_t = (SteamAPICall_t)NativeMethods.ISteamParties_CreateBeacon(CSteamAPIContext.GetSteamParties(), unOpenSlots, ref pBeaconLocation, utf8StringHandle, utf8StringHandle2);
				}
			}
			return steamAPICall_t;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000AE70 File Offset: 0x00009070
		public static void OnReservationCompleted(PartyBeaconID_t ulBeacon, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamParties_OnReservationCompleted(CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000AE83 File Offset: 0x00009083
		public static void CancelReservation(PartyBeaconID_t ulBeacon, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamParties_CancelReservation(CSteamAPIContext.GetSteamParties(), ulBeacon, steamIDUser);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000AE96 File Offset: 0x00009096
		public static SteamAPICall_t ChangeNumOpenSlots(PartyBeaconID_t ulBeacon, uint unOpenSlots)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamParties_ChangeNumOpenSlots(CSteamAPIContext.GetSteamParties(), ulBeacon, unOpenSlots);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000AEAE File Offset: 0x000090AE
		public static bool DestroyBeacon(PartyBeaconID_t ulBeacon)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamParties_DestroyBeacon(CSteamAPIContext.GetSteamParties(), ulBeacon);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public static bool GetBeaconLocationData(SteamPartyBeaconLocation_t BeaconLocation, ESteamPartyBeaconLocationData eData, out string pchDataStringOut, int cchDataStringOut)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchDataStringOut);
			bool flag = NativeMethods.ISteamParties_GetBeaconLocationData(CSteamAPIContext.GetSteamParties(), BeaconLocation, eData, intPtr, cchDataStringOut);
			pchDataStringOut = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}
	}
}
