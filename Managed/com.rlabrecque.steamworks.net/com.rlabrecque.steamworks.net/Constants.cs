using System;

namespace Steamworks
{
	// Token: 0x020000F0 RID: 240
	public static class Constants
	{
		// Token: 0x04000373 RID: 883
		public const string STEAMAPPS_INTERFACE_VERSION = "STEAMAPPS_INTERFACE_VERSION008";

		// Token: 0x04000374 RID: 884
		public const string STEAMAPPTICKET_INTERFACE_VERSION = "STEAMAPPTICKET_INTERFACE_VERSION001";

		// Token: 0x04000375 RID: 885
		public const string STEAMCLIENT_INTERFACE_VERSION = "SteamClient021";

		// Token: 0x04000376 RID: 886
		public const string STEAMFRIENDS_INTERFACE_VERSION = "SteamFriends017";

		// Token: 0x04000377 RID: 887
		public const string STEAMGAMECOORDINATOR_INTERFACE_VERSION = "SteamGameCoordinator001";

		// Token: 0x04000378 RID: 888
		public const string STEAMGAMESERVER_INTERFACE_VERSION = "SteamGameServer015";

		// Token: 0x04000379 RID: 889
		public const string STEAMGAMESERVERSTATS_INTERFACE_VERSION = "SteamGameServerStats001";

		// Token: 0x0400037A RID: 890
		public const string STEAMHTMLSURFACE_INTERFACE_VERSION = "STEAMHTMLSURFACE_INTERFACE_VERSION_005";

		// Token: 0x0400037B RID: 891
		public const string STEAMHTTP_INTERFACE_VERSION = "STEAMHTTP_INTERFACE_VERSION003";

		// Token: 0x0400037C RID: 892
		public const string STEAMINPUT_INTERFACE_VERSION = "SteamInput006";

		// Token: 0x0400037D RID: 893
		public const string STEAMINVENTORY_INTERFACE_VERSION = "STEAMINVENTORY_INTERFACE_V003";

		// Token: 0x0400037E RID: 894
		public const string STEAMMATCHMAKING_INTERFACE_VERSION = "SteamMatchMaking009";

		// Token: 0x0400037F RID: 895
		public const string STEAMMATCHMAKINGSERVERS_INTERFACE_VERSION = "SteamMatchMakingServers002";

		// Token: 0x04000380 RID: 896
		public const string STEAMGAMESEARCH_INTERFACE_VERSION = "SteamMatchGameSearch001";

		// Token: 0x04000381 RID: 897
		public const string STEAMPARTIES_INTERFACE_VERSION = "SteamParties002";

		// Token: 0x04000382 RID: 898
		public const string STEAMMUSIC_INTERFACE_VERSION = "STEAMMUSIC_INTERFACE_VERSION001";

		// Token: 0x04000383 RID: 899
		public const string STEAMMUSICREMOTE_INTERFACE_VERSION = "STEAMMUSICREMOTE_INTERFACE_VERSION001";

		// Token: 0x04000384 RID: 900
		public const string STEAMNETWORKING_INTERFACE_VERSION = "SteamNetworking006";

		// Token: 0x04000385 RID: 901
		public const string STEAMNETWORKINGMESSAGES_INTERFACE_VERSION = "SteamNetworkingMessages002";

		// Token: 0x04000386 RID: 902
		public const string STEAMNETWORKINGSOCKETS_INTERFACE_VERSION = "SteamNetworkingSockets012";

		// Token: 0x04000387 RID: 903
		public const string STEAMNETWORKINGUTILS_INTERFACE_VERSION = "SteamNetworkingUtils004";

		// Token: 0x04000388 RID: 904
		public const string STEAMPARENTALSETTINGS_INTERFACE_VERSION = "STEAMPARENTALSETTINGS_INTERFACE_VERSION001";

		// Token: 0x04000389 RID: 905
		public const string STEAMREMOTEPLAY_INTERFACE_VERSION = "STEAMREMOTEPLAY_INTERFACE_VERSION002";

		// Token: 0x0400038A RID: 906
		public const string STEAMREMOTESTORAGE_INTERFACE_VERSION = "STEAMREMOTESTORAGE_INTERFACE_VERSION016";

		// Token: 0x0400038B RID: 907
		public const string STEAMSCREENSHOTS_INTERFACE_VERSION = "STEAMSCREENSHOTS_INTERFACE_VERSION003";

		// Token: 0x0400038C RID: 908
		public const string STEAMTIMELINE_INTERFACE_VERSION = "STEAMTIMELINE_INTERFACE_V001";

		// Token: 0x0400038D RID: 909
		public const string STEAMUGC_INTERFACE_VERSION = "STEAMUGC_INTERFACE_VERSION020";

		// Token: 0x0400038E RID: 910
		public const string STEAMUSER_INTERFACE_VERSION = "SteamUser023";

		// Token: 0x0400038F RID: 911
		public const string STEAMUSERSTATS_INTERFACE_VERSION = "STEAMUSERSTATS_INTERFACE_VERSION012";

		// Token: 0x04000390 RID: 912
		public const string STEAMUTILS_INTERFACE_VERSION = "SteamUtils010";

		// Token: 0x04000391 RID: 913
		public const string STEAMVIDEO_INTERFACE_VERSION = "STEAMVIDEO_INTERFACE_V007";

		// Token: 0x04000392 RID: 914
		public const int k_cubAppProofOfPurchaseKeyMax = 240;

		// Token: 0x04000393 RID: 915
		public const int k_cchMaxFriendsGroupName = 64;

		// Token: 0x04000394 RID: 916
		public const int k_cFriendsGroupLimit = 100;

		// Token: 0x04000395 RID: 917
		public const int k_cEnumerateFollowersMax = 50;

		// Token: 0x04000396 RID: 918
		public const ushort k_usFriendGameInfoQueryPort_NotInitialized = 65535;

		// Token: 0x04000397 RID: 919
		public const ushort k_usFriendGameInfoQueryPort_Error = 65534;

		// Token: 0x04000398 RID: 920
		public const int k_cchPersonaNameMax = 128;

		// Token: 0x04000399 RID: 921
		public const int k_cwchPersonaNameMax = 32;

		// Token: 0x0400039A RID: 922
		public const int k_cubChatMetadataMax = 8192;

		// Token: 0x0400039B RID: 923
		public const int k_cchMaxRichPresenceKeys = 30;

		// Token: 0x0400039C RID: 924
		public const int k_cchMaxRichPresenceKeyLength = 64;

		// Token: 0x0400039D RID: 925
		public const int k_cchMaxRichPresenceValueLength = 256;

		// Token: 0x0400039E RID: 926
		public const int k_unFavoriteFlagNone = 0;

		// Token: 0x0400039F RID: 927
		public const int k_unFavoriteFlagFavorite = 1;

		// Token: 0x040003A0 RID: 928
		public const int k_unFavoriteFlagHistory = 2;

		// Token: 0x040003A1 RID: 929
		public const int k_unMaxCloudFileChunkSize = 104857600;

		// Token: 0x040003A2 RID: 930
		public const int k_cchPublishedDocumentTitleMax = 129;

		// Token: 0x040003A3 RID: 931
		public const int k_cchPublishedDocumentDescriptionMax = 8000;

		// Token: 0x040003A4 RID: 932
		public const int k_cchPublishedDocumentChangeDescriptionMax = 8000;

		// Token: 0x040003A5 RID: 933
		public const int k_unEnumeratePublishedFilesMaxResults = 50;

		// Token: 0x040003A6 RID: 934
		public const int k_cchTagListMax = 1025;

		// Token: 0x040003A7 RID: 935
		public const int k_cchFilenameMax = 260;

		// Token: 0x040003A8 RID: 936
		public const int k_cchPublishedFileURLMax = 256;

		// Token: 0x040003A9 RID: 937
		public const int k_nScreenshotMaxTaggedUsers = 32;

		// Token: 0x040003AA RID: 938
		public const int k_nScreenshotMaxTaggedPublishedFiles = 32;

		// Token: 0x040003AB RID: 939
		public const int k_cubUFSTagTypeMax = 255;

		// Token: 0x040003AC RID: 940
		public const int k_cubUFSTagValueMax = 255;

		// Token: 0x040003AD RID: 941
		public const int k_ScreenshotThumbWidth = 200;

		// Token: 0x040003AE RID: 942
		public const int k_unMaxTimelinePriority = 1000;

		// Token: 0x040003AF RID: 943
		public const float k_flMaxTimelineEventDuration = 600f;

		// Token: 0x040003B0 RID: 944
		public const int kNumUGCResultsPerPage = 50;

		// Token: 0x040003B1 RID: 945
		public const int k_cchDeveloperMetadataMax = 5000;

		// Token: 0x040003B2 RID: 946
		public const int k_nCubTicketMaxLength = 2560;

		// Token: 0x040003B3 RID: 947
		public const int k_cchStatNameMax = 128;

		// Token: 0x040003B4 RID: 948
		public const int k_cchLeaderboardNameMax = 128;

		// Token: 0x040003B5 RID: 949
		public const int k_cLeaderboardDetailsMax = 64;

		// Token: 0x040003B6 RID: 950
		public const int k_cbMaxGameServerGameDir = 32;

		// Token: 0x040003B7 RID: 951
		public const int k_cbMaxGameServerMapName = 32;

		// Token: 0x040003B8 RID: 952
		public const int k_cbMaxGameServerGameDescription = 64;

		// Token: 0x040003B9 RID: 953
		public const int k_cbMaxGameServerName = 64;

		// Token: 0x040003BA RID: 954
		public const int k_cbMaxGameServerTags = 128;

		// Token: 0x040003BB RID: 955
		public const int k_cbMaxGameServerGameData = 2048;

		// Token: 0x040003BC RID: 956
		public const int k_cchMaxSteamErrMsg = 1024;

		// Token: 0x040003BD RID: 957
		public const int k_iSteamUserCallbacks = 100;

		// Token: 0x040003BE RID: 958
		public const int k_iSteamGameServerCallbacks = 200;

		// Token: 0x040003BF RID: 959
		public const int k_iSteamFriendsCallbacks = 300;

		// Token: 0x040003C0 RID: 960
		public const int k_iSteamBillingCallbacks = 400;

		// Token: 0x040003C1 RID: 961
		public const int k_iSteamMatchmakingCallbacks = 500;

		// Token: 0x040003C2 RID: 962
		public const int k_iSteamContentServerCallbacks = 600;

		// Token: 0x040003C3 RID: 963
		public const int k_iSteamUtilsCallbacks = 700;

		// Token: 0x040003C4 RID: 964
		public const int k_iSteamAppsCallbacks = 1000;

		// Token: 0x040003C5 RID: 965
		public const int k_iSteamUserStatsCallbacks = 1100;

		// Token: 0x040003C6 RID: 966
		public const int k_iSteamNetworkingCallbacks = 1200;

		// Token: 0x040003C7 RID: 967
		public const int k_iSteamNetworkingSocketsCallbacks = 1220;

		// Token: 0x040003C8 RID: 968
		public const int k_iSteamNetworkingMessagesCallbacks = 1250;

		// Token: 0x040003C9 RID: 969
		public const int k_iSteamNetworkingUtilsCallbacks = 1280;

		// Token: 0x040003CA RID: 970
		public const int k_iSteamRemoteStorageCallbacks = 1300;

		// Token: 0x040003CB RID: 971
		public const int k_iSteamGameServerItemsCallbacks = 1500;

		// Token: 0x040003CC RID: 972
		public const int k_iSteamGameCoordinatorCallbacks = 1700;

		// Token: 0x040003CD RID: 973
		public const int k_iSteamGameServerStatsCallbacks = 1800;

		// Token: 0x040003CE RID: 974
		public const int k_iSteam2AsyncCallbacks = 1900;

		// Token: 0x040003CF RID: 975
		public const int k_iSteamGameStatsCallbacks = 2000;

		// Token: 0x040003D0 RID: 976
		public const int k_iSteamHTTPCallbacks = 2100;

		// Token: 0x040003D1 RID: 977
		public const int k_iSteamScreenshotsCallbacks = 2300;

		// Token: 0x040003D2 RID: 978
		public const int k_iSteamStreamLauncherCallbacks = 2600;

		// Token: 0x040003D3 RID: 979
		public const int k_iSteamControllerCallbacks = 2800;

		// Token: 0x040003D4 RID: 980
		public const int k_iSteamUGCCallbacks = 3400;

		// Token: 0x040003D5 RID: 981
		public const int k_iSteamStreamClientCallbacks = 3500;

		// Token: 0x040003D6 RID: 982
		public const int k_iSteamMusicCallbacks = 4000;

		// Token: 0x040003D7 RID: 983
		public const int k_iSteamMusicRemoteCallbacks = 4100;

		// Token: 0x040003D8 RID: 984
		public const int k_iSteamGameNotificationCallbacks = 4400;

		// Token: 0x040003D9 RID: 985
		public const int k_iSteamHTMLSurfaceCallbacks = 4500;

		// Token: 0x040003DA RID: 986
		public const int k_iSteamVideoCallbacks = 4600;

		// Token: 0x040003DB RID: 987
		public const int k_iSteamInventoryCallbacks = 4700;

		// Token: 0x040003DC RID: 988
		public const int k_ISteamParentalSettingsCallbacks = 5000;

		// Token: 0x040003DD RID: 989
		public const int k_iSteamGameSearchCallbacks = 5200;

		// Token: 0x040003DE RID: 990
		public const int k_iSteamPartiesCallbacks = 5300;

		// Token: 0x040003DF RID: 991
		public const int k_iSteamSTARCallbacks = 5500;

		// Token: 0x040003E0 RID: 992
		public const int k_iSteamRemotePlayCallbacks = 5700;

		// Token: 0x040003E1 RID: 993
		public const int k_iSteamChatCallbacks = 5900;

		// Token: 0x040003E2 RID: 994
		public const int k_iSteamTimelineCallbacks = 6000;

		// Token: 0x040003E3 RID: 995
		public const ushort STEAMGAMESERVER_QUERY_PORT_SHARED = 65535;

		// Token: 0x040003E4 RID: 996
		public const int k_unSteamAccountIDMask = -1;

		// Token: 0x040003E5 RID: 997
		public const int k_unSteamAccountInstanceMask = 1048575;

		// Token: 0x040003E6 RID: 998
		public const int k_unSteamUserDefaultInstance = 1;

		// Token: 0x040003E7 RID: 999
		public const int k_cchGameExtraInfoMax = 64;

		// Token: 0x040003E8 RID: 1000
		public const int k_nSteamEncryptedAppTicketSymmetricKeyLen = 32;

		// Token: 0x040003E9 RID: 1001
		public const int k_nMaxReturnPorts = 8;

		// Token: 0x040003EA RID: 1002
		public const int k_cchMaxSteamNetworkingErrMsg = 1024;

		// Token: 0x040003EB RID: 1003
		public const int k_cchSteamNetworkingMaxConnectionCloseReason = 128;

		// Token: 0x040003EC RID: 1004
		public const int k_cchSteamNetworkingMaxConnectionDescription = 128;

		// Token: 0x040003ED RID: 1005
		public const int k_cchSteamNetworkingMaxConnectionAppName = 32;

		// Token: 0x040003EE RID: 1006
		public const int k_nSteamNetworkConnectionInfoFlags_Unauthenticated = 1;

		// Token: 0x040003EF RID: 1007
		public const int k_nSteamNetworkConnectionInfoFlags_Unencrypted = 2;

		// Token: 0x040003F0 RID: 1008
		public const int k_nSteamNetworkConnectionInfoFlags_LoopbackBuffers = 4;

		// Token: 0x040003F1 RID: 1009
		public const int k_nSteamNetworkConnectionInfoFlags_Fast = 8;

		// Token: 0x040003F2 RID: 1010
		public const int k_nSteamNetworkConnectionInfoFlags_Relayed = 16;

		// Token: 0x040003F3 RID: 1011
		public const int k_nSteamNetworkConnectionInfoFlags_DualWifi = 32;

		// Token: 0x040003F4 RID: 1012
		public const int k_cbMaxSteamNetworkingSocketsMessageSizeSend = 524288;

		// Token: 0x040003F5 RID: 1013
		public const int k_nSteamNetworkingSend_Unreliable = 0;

		// Token: 0x040003F6 RID: 1014
		public const int k_nSteamNetworkingSend_NoNagle = 1;

		// Token: 0x040003F7 RID: 1015
		public const int k_nSteamNetworkingSend_UnreliableNoNagle = 1;

		// Token: 0x040003F8 RID: 1016
		public const int k_nSteamNetworkingSend_NoDelay = 4;

		// Token: 0x040003F9 RID: 1017
		public const int k_nSteamNetworkingSend_UnreliableNoDelay = 5;

		// Token: 0x040003FA RID: 1018
		public const int k_nSteamNetworkingSend_Reliable = 8;

		// Token: 0x040003FB RID: 1019
		public const int k_nSteamNetworkingSend_ReliableNoNagle = 9;

		// Token: 0x040003FC RID: 1020
		public const int k_nSteamNetworkingSend_UseCurrentThread = 16;

		// Token: 0x040003FD RID: 1021
		public const int k_nSteamNetworkingSend_AutoRestartBrokenSession = 32;

		// Token: 0x040003FE RID: 1022
		public const int k_cchMaxSteamNetworkingPingLocationString = 1024;

		// Token: 0x040003FF RID: 1023
		public const int k_nSteamNetworkingPing_Failed = -1;

		// Token: 0x04000400 RID: 1024
		public const int k_nSteamNetworkingPing_Unknown = -2;

		// Token: 0x04000401 RID: 1025
		public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Default = -1;

		// Token: 0x04000402 RID: 1026
		public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Disable = 0;

		// Token: 0x04000403 RID: 1027
		public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Relay = 1;

		// Token: 0x04000404 RID: 1028
		public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Private = 2;

		// Token: 0x04000405 RID: 1029
		public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_Public = 4;

		// Token: 0x04000406 RID: 1030
		public const int k_nSteamNetworkingConfig_P2P_Transport_ICE_Enable_All = 2147483647;

		// Token: 0x04000407 RID: 1031
		public const ulong k_ulPartyBeaconIdInvalid = 0UL;

		// Token: 0x04000408 RID: 1032
		public const int INVALID_HTTPREQUEST_HANDLE = 0;

		// Token: 0x04000409 RID: 1033
		public const int STEAM_INPUT_MAX_COUNT = 16;

		// Token: 0x0400040A RID: 1034
		public const int STEAM_INPUT_MAX_ANALOG_ACTIONS = 24;

		// Token: 0x0400040B RID: 1035
		public const int STEAM_INPUT_MAX_DIGITAL_ACTIONS = 256;

		// Token: 0x0400040C RID: 1036
		public const int STEAM_INPUT_MAX_ORIGINS = 8;

		// Token: 0x0400040D RID: 1037
		public const int STEAM_INPUT_MAX_ACTIVE_LAYERS = 16;

		// Token: 0x0400040E RID: 1038
		public const ulong STEAM_INPUT_HANDLE_ALL_CONTROLLERS = 18446744073709551615UL;

		// Token: 0x0400040F RID: 1039
		public const float STEAM_INPUT_MIN_ANALOG_ACTION_DATA = -1f;

		// Token: 0x04000410 RID: 1040
		public const float STEAM_INPUT_MAX_ANALOG_ACTION_DATA = 1f;

		// Token: 0x04000411 RID: 1041
		public const byte k_nMaxLobbyKeyLength = 255;

		// Token: 0x04000412 RID: 1042
		public const int k_SteamMusicNameMaxLength = 255;

		// Token: 0x04000413 RID: 1043
		public const int k_SteamMusicPNGMaxLength = 65535;
	}
}
