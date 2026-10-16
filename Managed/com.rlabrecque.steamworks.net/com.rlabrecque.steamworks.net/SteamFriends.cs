using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000172 RID: 370
	public static class SteamFriends
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x00005360 File Offset: 0x00003560
		public static string GetPersonaName()
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetPersonaName(CSteamAPIContext.GetSteamFriends()));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00005378 File Offset: 0x00003578
		public static SteamAPICall_t SetPersonaName(string pchPersonaName)
		{
			InteropHelp.TestIfAvailableClient();
			SteamAPICall_t steamAPICall_t;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchPersonaName))
			{
				steamAPICall_t = (SteamAPICall_t)NativeMethods.ISteamFriends_SetPersonaName(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
			return steamAPICall_t;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000053C0 File Offset: 0x000035C0
		public static EPersonaState GetPersonaState()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetPersonaState(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000053D1 File Offset: 0x000035D1
		public static int GetFriendCount(EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCount(CSteamAPIContext.GetSteamFriends(), iFriendFlags);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000053E3 File Offset: 0x000035E3
		public static CSteamID GetFriendByIndex(int iFriend, EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetFriendByIndex(CSteamAPIContext.GetSteamFriends(), iFriend, iFriendFlags);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000053FB File Offset: 0x000035FB
		public static EFriendRelationship GetFriendRelationship(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendRelationship(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000540D File Offset: 0x0000360D
		public static EPersonaState GetFriendPersonaState(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendPersonaState(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000541F File Offset: 0x0000361F
		public static string GetFriendPersonaName(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendPersonaName(CSteamAPIContext.GetSteamFriends(), steamIDFriend));
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00005436 File Offset: 0x00003636
		public static bool GetFriendGamePlayed(CSteamID steamIDFriend, out FriendGameInfo_t pFriendGameInfo)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendGamePlayed(CSteamAPIContext.GetSteamFriends(), steamIDFriend, out pFriendGameInfo);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00005449 File Offset: 0x00003649
		public static string GetFriendPersonaNameHistory(CSteamID steamIDFriend, int iPersonaName)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendPersonaNameHistory(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iPersonaName));
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00005461 File Offset: 0x00003661
		public static int GetFriendSteamLevel(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendSteamLevel(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00005473 File Offset: 0x00003673
		public static string GetPlayerNickname(CSteamID steamIDPlayer)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetPlayerNickname(CSteamAPIContext.GetSteamFriends(), steamIDPlayer));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000548A File Offset: 0x0000368A
		public static int GetFriendsGroupCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendsGroupCount(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000549B File Offset: 0x0000369B
		public static FriendsGroupID_t GetFriendsGroupIDByIndex(int iFG)
		{
			InteropHelp.TestIfAvailableClient();
			return (FriendsGroupID_t)NativeMethods.ISteamFriends_GetFriendsGroupIDByIndex(CSteamAPIContext.GetSteamFriends(), iFG);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000054B2 File Offset: 0x000036B2
		public static string GetFriendsGroupName(FriendsGroupID_t friendsGroupID)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendsGroupName(CSteamAPIContext.GetSteamFriends(), friendsGroupID));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000054C9 File Offset: 0x000036C9
		public static int GetFriendsGroupMembersCount(FriendsGroupID_t friendsGroupID)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendsGroupMembersCount(CSteamAPIContext.GetSteamFriends(), friendsGroupID);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000054DB File Offset: 0x000036DB
		public static void GetFriendsGroupMembersList(FriendsGroupID_t friendsGroupID, CSteamID[] pOutSteamIDMembers, int nMembersCount)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_GetFriendsGroupMembersList(CSteamAPIContext.GetSteamFriends(), friendsGroupID, pOutSteamIDMembers, nMembersCount);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000054EF File Offset: 0x000036EF
		public static bool HasFriend(CSteamID steamIDFriend, EFriendFlags iFriendFlags)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_HasFriend(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iFriendFlags);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00005502 File Offset: 0x00003702
		public static int GetClanCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanCount(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00005513 File Offset: 0x00003713
		public static CSteamID GetClanByIndex(int iClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanByIndex(CSteamAPIContext.GetSteamFriends(), iClan);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000552A File Offset: 0x0000372A
		public static string GetClanName(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetClanName(CSteamAPIContext.GetSteamFriends(), steamIDClan));
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00005541 File Offset: 0x00003741
		public static string GetClanTag(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetClanTag(CSteamAPIContext.GetSteamFriends(), steamIDClan));
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00005558 File Offset: 0x00003758
		public static bool GetClanActivityCounts(CSteamID steamIDClan, out int pnOnline, out int pnInGame, out int pnChatting)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanActivityCounts(CSteamAPIContext.GetSteamFriends(), steamIDClan, out pnOnline, out pnInGame, out pnChatting);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000556D File Offset: 0x0000376D
		public static SteamAPICall_t DownloadClanActivityCounts(CSteamID[] psteamIDClans, int cClansToRequest)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_DownloadClanActivityCounts(CSteamAPIContext.GetSteamFriends(), psteamIDClans, cClansToRequest);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00005585 File Offset: 0x00003785
		public static int GetFriendCountFromSource(CSteamID steamIDSource)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCountFromSource(CSteamAPIContext.GetSteamFriends(), steamIDSource);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00005597 File Offset: 0x00003797
		public static CSteamID GetFriendFromSourceByIndex(CSteamID steamIDSource, int iFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetFriendFromSourceByIndex(CSteamAPIContext.GetSteamFriends(), steamIDSource, iFriend);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000055AF File Offset: 0x000037AF
		public static bool IsUserInSource(CSteamID steamIDUser, CSteamID steamIDSource)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsUserInSource(CSteamAPIContext.GetSteamFriends(), steamIDUser, steamIDSource);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000055C2 File Offset: 0x000037C2
		public static void SetInGameVoiceSpeaking(CSteamID steamIDUser, bool bSpeaking)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_SetInGameVoiceSpeaking(CSteamAPIContext.GetSteamFriends(), steamIDUser, bSpeaking);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000055D8 File Offset: 0x000037D8
		public static void ActivateGameOverlay(string pchDialog)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDialog))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlay(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00005618 File Offset: 0x00003818
		public static void ActivateGameOverlayToUser(string pchDialog, CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDialog))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayToUser(CSteamAPIContext.GetSteamFriends(), utf8StringHandle, steamID);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000565C File Offset: 0x0000385C
		public static void ActivateGameOverlayToWebPage(string pchURL, EActivateGameOverlayToWebPageMode eMode = EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchURL))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayToWebPage(CSteamAPIContext.GetSteamFriends(), utf8StringHandle, eMode);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000056A0 File Offset: 0x000038A0
		public static void ActivateGameOverlayToStore(AppId_t nAppID, EOverlayToStoreFlag eFlag)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayToStore(CSteamAPIContext.GetSteamFriends(), nAppID, eFlag);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000056B3 File Offset: 0x000038B3
		public static void SetPlayedWith(CSteamID steamIDUserPlayedWith)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_SetPlayedWith(CSteamAPIContext.GetSteamFriends(), steamIDUserPlayedWith);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000056C5 File Offset: 0x000038C5
		public static void ActivateGameOverlayInviteDialog(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayInviteDialog(CSteamAPIContext.GetSteamFriends(), steamIDLobby);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000056D7 File Offset: 0x000038D7
		public static int GetSmallFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetSmallFriendAvatar(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000056E9 File Offset: 0x000038E9
		public static int GetMediumFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetMediumFriendAvatar(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000056FB File Offset: 0x000038FB
		public static int GetLargeFriendAvatar(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetLargeFriendAvatar(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000570D File Offset: 0x0000390D
		public static bool RequestUserInformation(CSteamID steamIDUser, bool bRequireNameOnly)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_RequestUserInformation(CSteamAPIContext.GetSteamFriends(), steamIDUser, bRequireNameOnly);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00005720 File Offset: 0x00003920
		public static SteamAPICall_t RequestClanOfficerList(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_RequestClanOfficerList(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00005737 File Offset: 0x00003937
		public static CSteamID GetClanOwner(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanOwner(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000574E File Offset: 0x0000394E
		public static int GetClanOfficerCount(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanOfficerCount(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00005760 File Offset: 0x00003960
		public static CSteamID GetClanOfficerByIndex(CSteamID steamIDClan, int iOfficer)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetClanOfficerByIndex(CSteamAPIContext.GetSteamFriends(), steamIDClan, iOfficer);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00005778 File Offset: 0x00003978
		public static uint GetUserRestrictions()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetUserRestrictions(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000578C File Offset: 0x0000398C
		public static bool SetRichPresence(string pchKey, string pchValue)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchValue))
				{
					flag = NativeMethods.ISteamFriends_SetRichPresence(CSteamAPIContext.GetSteamFriends(), utf8StringHandle, utf8StringHandle2);
				}
			}
			return flag;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000057EC File Offset: 0x000039EC
		public static void ClearRichPresence()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ClearRichPresence(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00005800 File Offset: 0x00003A00
		public static string GetFriendRichPresence(CSteamID steamIDFriend, string pchKey)
		{
			InteropHelp.TestIfAvailableClient();
			string text;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchKey))
			{
				text = InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendRichPresence(CSteamAPIContext.GetSteamFriends(), steamIDFriend, utf8StringHandle));
			}
			return text;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00005848 File Offset: 0x00003A48
		public static int GetFriendRichPresenceKeyCount(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendRichPresenceKeyCount(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000585A File Offset: 0x00003A5A
		public static string GetFriendRichPresenceKeyByIndex(CSteamID steamIDFriend, int iKey)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetFriendRichPresenceKeyByIndex(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iKey));
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00005872 File Offset: 0x00003A72
		public static void RequestFriendRichPresence(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_RequestFriendRichPresence(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00005884 File Offset: 0x00003A84
		public static bool InviteUserToGame(CSteamID steamIDFriend, string pchConnectString)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				flag = NativeMethods.ISteamFriends_InviteUserToGame(CSteamAPIContext.GetSteamFriends(), steamIDFriend, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000058C8 File Offset: 0x00003AC8
		public static int GetCoplayFriendCount()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetCoplayFriendCount(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000058D9 File Offset: 0x00003AD9
		public static CSteamID GetCoplayFriend(int iCoplayFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetCoplayFriend(CSteamAPIContext.GetSteamFriends(), iCoplayFriend);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000058F0 File Offset: 0x00003AF0
		public static int GetFriendCoplayTime(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetFriendCoplayTime(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00005902 File Offset: 0x00003B02
		public static AppId_t GetFriendCoplayGame(CSteamID steamIDFriend)
		{
			InteropHelp.TestIfAvailableClient();
			return (AppId_t)NativeMethods.ISteamFriends_GetFriendCoplayGame(CSteamAPIContext.GetSteamFriends(), steamIDFriend);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00005919 File Offset: 0x00003B19
		public static SteamAPICall_t JoinClanChatRoom(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_JoinClanChatRoom(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00005930 File Offset: 0x00003B30
		public static bool LeaveClanChatRoom(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_LeaveClanChatRoom(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00005942 File Offset: 0x00003B42
		public static int GetClanChatMemberCount(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetClanChatMemberCount(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00005954 File Offset: 0x00003B54
		public static CSteamID GetChatMemberByIndex(CSteamID steamIDClan, int iUser)
		{
			InteropHelp.TestIfAvailableClient();
			return (CSteamID)NativeMethods.ISteamFriends_GetChatMemberByIndex(CSteamAPIContext.GetSteamFriends(), steamIDClan, iUser);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000596C File Offset: 0x00003B6C
		public static bool SendClanChatMessage(CSteamID steamIDClanChat, string pchText)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchText))
			{
				flag = NativeMethods.ISteamFriends_SendClanChatMessage(CSteamAPIContext.GetSteamFriends(), steamIDClanChat, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static int GetClanChatMessage(CSteamID steamIDClanChat, int iMessage, out string prgchText, int cchTextMax, out EChatEntryType peChatEntryType, out CSteamID psteamidChatter)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cchTextMax);
			int num = NativeMethods.ISteamFriends_GetClanChatMessage(CSteamAPIContext.GetSteamFriends(), steamIDClanChat, iMessage, intPtr, cchTextMax, out peChatEntryType, out psteamidChatter);
			prgchText = ((num != 0) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000059F1 File Offset: 0x00003BF1
		public static bool IsClanChatAdmin(CSteamID steamIDClanChat, CSteamID steamIDUser)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanChatAdmin(CSteamAPIContext.GetSteamFriends(), steamIDClanChat, steamIDUser);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00005A04 File Offset: 0x00003C04
		public static bool IsClanChatWindowOpenInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanChatWindowOpenInSteam(CSteamAPIContext.GetSteamFriends(), steamIDClanChat);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00005A16 File Offset: 0x00003C16
		public static bool OpenClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_OpenClanChatWindowInSteam(CSteamAPIContext.GetSteamFriends(), steamIDClanChat);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00005A28 File Offset: 0x00003C28
		public static bool CloseClanChatWindowInSteam(CSteamID steamIDClanChat)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_CloseClanChatWindowInSteam(CSteamAPIContext.GetSteamFriends(), steamIDClanChat);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00005A3A File Offset: 0x00003C3A
		public static bool SetListenForFriendsMessages(bool bInterceptEnabled)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_SetListenForFriendsMessages(CSteamAPIContext.GetSteamFriends(), bInterceptEnabled);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00005A4C File Offset: 0x00003C4C
		public static bool ReplyToFriendMessage(CSteamID steamIDFriend, string pchMsgToSend)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchMsgToSend))
			{
				flag = NativeMethods.ISteamFriends_ReplyToFriendMessage(CSteamAPIContext.GetSteamFriends(), steamIDFriend, utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00005A90 File Offset: 0x00003C90
		public static int GetFriendMessage(CSteamID steamIDFriend, int iMessageID, out string pvData, int cubData, out EChatEntryType peChatEntryType)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cubData);
			int num = NativeMethods.ISteamFriends_GetFriendMessage(CSteamAPIContext.GetSteamFriends(), steamIDFriend, iMessageID, intPtr, cubData, out peChatEntryType);
			pvData = ((num != 0) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00005ACF File Offset: 0x00003CCF
		public static SteamAPICall_t GetFollowerCount(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_GetFollowerCount(CSteamAPIContext.GetSteamFriends(), steamID);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00005AE6 File Offset: 0x00003CE6
		public static SteamAPICall_t IsFollowing(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_IsFollowing(CSteamAPIContext.GetSteamFriends(), steamID);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00005AFD File Offset: 0x00003CFD
		public static SteamAPICall_t EnumerateFollowingList(uint unStartIndex)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_EnumerateFollowingList(CSteamAPIContext.GetSteamFriends(), unStartIndex);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00005B14 File Offset: 0x00003D14
		public static bool IsClanPublic(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanPublic(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00005B26 File Offset: 0x00003D26
		public static bool IsClanOfficialGameGroup(CSteamID steamIDClan)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_IsClanOfficialGameGroup(CSteamAPIContext.GetSteamFriends(), steamIDClan);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00005B38 File Offset: 0x00003D38
		public static int GetNumChatsWithUnreadPriorityMessages()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetNumChatsWithUnreadPriorityMessages(CSteamAPIContext.GetSteamFriends());
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00005B49 File Offset: 0x00003D49
		public static void ActivateGameOverlayRemotePlayTogetherInviteDialog(CSteamID steamIDLobby)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamFriends_ActivateGameOverlayRemotePlayTogetherInviteDialog(CSteamAPIContext.GetSteamFriends(), steamIDLobby);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00005B5C File Offset: 0x00003D5C
		public static bool RegisterProtocolInOverlayBrowser(string pchProtocol)
		{
			InteropHelp.TestIfAvailableClient();
			bool flag;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchProtocol))
			{
				flag = NativeMethods.ISteamFriends_RegisterProtocolInOverlayBrowser(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
			return flag;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public static void ActivateGameOverlayInviteDialogConnectString(string pchConnectString)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchConnectString))
			{
				NativeMethods.ISteamFriends_ActivateGameOverlayInviteDialogConnectString(CSteamAPIContext.GetSteamFriends(), utf8StringHandle);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public static SteamAPICall_t RequestEquippedProfileItems(CSteamID steamID)
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamAPICall_t)NativeMethods.ISteamFriends_RequestEquippedProfileItems(CSteamAPIContext.GetSteamFriends(), steamID);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00005BF7 File Offset: 0x00003DF7
		public static bool BHasEquippedProfileItem(CSteamID steamID, ECommunityProfileItemType itemType)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_BHasEquippedProfileItem(CSteamAPIContext.GetSteamFriends(), steamID, itemType);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00005C0A File Offset: 0x00003E0A
		public static string GetProfileItemPropertyString(CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop)
		{
			InteropHelp.TestIfAvailableClient();
			return InteropHelp.PtrToStringUTF8(NativeMethods.ISteamFriends_GetProfileItemPropertyString(CSteamAPIContext.GetSteamFriends(), steamID, itemType, prop));
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00005C23 File Offset: 0x00003E23
		public static uint GetProfileItemPropertyUint(CSteamID steamID, ECommunityProfileItemType itemType, ECommunityProfileItemProperty prop)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamFriends_GetProfileItemPropertyUint(CSteamAPIContext.GetSteamFriends(), steamID, itemType, prop);
		}
	}
}
