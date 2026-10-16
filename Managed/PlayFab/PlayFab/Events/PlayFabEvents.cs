using System;
using PlayFab.AuthenticationModels;
using PlayFab.ClientModels;
using PlayFab.CloudScriptModels;
using PlayFab.DataModels;
using PlayFab.EventsModels;
using PlayFab.ExperimentationModels;
using PlayFab.GroupsModels;
using PlayFab.InsightsModels;
using PlayFab.Internal;
using PlayFab.LocalizationModels;
using PlayFab.MultiplayerModels;
using PlayFab.ProfilesModels;
using PlayFab.SharedModels;

namespace PlayFab.Events
{
	// Token: 0x02000338 RID: 824
	public class PlayFabEvents
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060006D1 RID: 1745 RVA: 0x00016528 File Offset: 0x00014728
		// (remove) Token: 0x060006D2 RID: 1746 RVA: 0x00016560 File Offset: 0x00014760
		public event PlayFabEvents.PlayFabRequestEvent<GetEntityTokenRequest> OnAuthenticationGetEntityTokenRequestEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060006D3 RID: 1747 RVA: 0x00016598 File Offset: 0x00014798
		// (remove) Token: 0x060006D4 RID: 1748 RVA: 0x000165D0 File Offset: 0x000147D0
		public event PlayFabEvents.PlayFabResultEvent<GetEntityTokenResponse> OnAuthenticationGetEntityTokenResultEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060006D5 RID: 1749 RVA: 0x00016608 File Offset: 0x00014808
		// (remove) Token: 0x060006D6 RID: 1750 RVA: 0x00016640 File Offset: 0x00014840
		public event PlayFabEvents.PlayFabRequestEvent<ValidateEntityTokenRequest> OnAuthenticationValidateEntityTokenRequestEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060006D7 RID: 1751 RVA: 0x00016678 File Offset: 0x00014878
		// (remove) Token: 0x060006D8 RID: 1752 RVA: 0x000166B0 File Offset: 0x000148B0
		public event PlayFabEvents.PlayFabResultEvent<ValidateEntityTokenResponse> OnAuthenticationValidateEntityTokenResultEvent;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060006D9 RID: 1753 RVA: 0x000166E8 File Offset: 0x000148E8
		// (remove) Token: 0x060006DA RID: 1754 RVA: 0x00016720 File Offset: 0x00014920
		public event PlayFabEvents.PlayFabResultEvent<LoginResult> OnLoginResultEvent;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060006DB RID: 1755 RVA: 0x00016758 File Offset: 0x00014958
		// (remove) Token: 0x060006DC RID: 1756 RVA: 0x00016790 File Offset: 0x00014990
		public event PlayFabEvents.PlayFabRequestEvent<AcceptTradeRequest> OnAcceptTradeRequestEvent;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060006DD RID: 1757 RVA: 0x000167C8 File Offset: 0x000149C8
		// (remove) Token: 0x060006DE RID: 1758 RVA: 0x00016800 File Offset: 0x00014A00
		public event PlayFabEvents.PlayFabResultEvent<AcceptTradeResponse> OnAcceptTradeResultEvent;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060006DF RID: 1759 RVA: 0x00016838 File Offset: 0x00014A38
		// (remove) Token: 0x060006E0 RID: 1760 RVA: 0x00016870 File Offset: 0x00014A70
		public event PlayFabEvents.PlayFabRequestEvent<AddFriendRequest> OnAddFriendRequestEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060006E1 RID: 1761 RVA: 0x000168A8 File Offset: 0x00014AA8
		// (remove) Token: 0x060006E2 RID: 1762 RVA: 0x000168E0 File Offset: 0x00014AE0
		public event PlayFabEvents.PlayFabResultEvent<AddFriendResult> OnAddFriendResultEvent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060006E3 RID: 1763 RVA: 0x00016918 File Offset: 0x00014B18
		// (remove) Token: 0x060006E4 RID: 1764 RVA: 0x00016950 File Offset: 0x00014B50
		public event PlayFabEvents.PlayFabRequestEvent<AddGenericIDRequest> OnAddGenericIDRequestEvent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060006E5 RID: 1765 RVA: 0x00016988 File Offset: 0x00014B88
		// (remove) Token: 0x060006E6 RID: 1766 RVA: 0x000169C0 File Offset: 0x00014BC0
		public event PlayFabEvents.PlayFabResultEvent<AddGenericIDResult> OnAddGenericIDResultEvent;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060006E7 RID: 1767 RVA: 0x000169F8 File Offset: 0x00014BF8
		// (remove) Token: 0x060006E8 RID: 1768 RVA: 0x00016A30 File Offset: 0x00014C30
		public event PlayFabEvents.PlayFabRequestEvent<AddOrUpdateContactEmailRequest> OnAddOrUpdateContactEmailRequestEvent;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060006E9 RID: 1769 RVA: 0x00016A68 File Offset: 0x00014C68
		// (remove) Token: 0x060006EA RID: 1770 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public event PlayFabEvents.PlayFabResultEvent<AddOrUpdateContactEmailResult> OnAddOrUpdateContactEmailResultEvent;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060006EB RID: 1771 RVA: 0x00016AD8 File Offset: 0x00014CD8
		// (remove) Token: 0x060006EC RID: 1772 RVA: 0x00016B10 File Offset: 0x00014D10
		public event PlayFabEvents.PlayFabRequestEvent<AddSharedGroupMembersRequest> OnAddSharedGroupMembersRequestEvent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060006ED RID: 1773 RVA: 0x00016B48 File Offset: 0x00014D48
		// (remove) Token: 0x060006EE RID: 1774 RVA: 0x00016B80 File Offset: 0x00014D80
		public event PlayFabEvents.PlayFabResultEvent<AddSharedGroupMembersResult> OnAddSharedGroupMembersResultEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060006EF RID: 1775 RVA: 0x00016BB8 File Offset: 0x00014DB8
		// (remove) Token: 0x060006F0 RID: 1776 RVA: 0x00016BF0 File Offset: 0x00014DF0
		public event PlayFabEvents.PlayFabRequestEvent<AddUsernamePasswordRequest> OnAddUsernamePasswordRequestEvent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060006F1 RID: 1777 RVA: 0x00016C28 File Offset: 0x00014E28
		// (remove) Token: 0x060006F2 RID: 1778 RVA: 0x00016C60 File Offset: 0x00014E60
		public event PlayFabEvents.PlayFabResultEvent<AddUsernamePasswordResult> OnAddUsernamePasswordResultEvent;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060006F3 RID: 1779 RVA: 0x00016C98 File Offset: 0x00014E98
		// (remove) Token: 0x060006F4 RID: 1780 RVA: 0x00016CD0 File Offset: 0x00014ED0
		public event PlayFabEvents.PlayFabRequestEvent<AddUserVirtualCurrencyRequest> OnAddUserVirtualCurrencyRequestEvent;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060006F5 RID: 1781 RVA: 0x00016D08 File Offset: 0x00014F08
		// (remove) Token: 0x060006F6 RID: 1782 RVA: 0x00016D40 File Offset: 0x00014F40
		public event PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult> OnAddUserVirtualCurrencyResultEvent;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060006F7 RID: 1783 RVA: 0x00016D78 File Offset: 0x00014F78
		// (remove) Token: 0x060006F8 RID: 1784 RVA: 0x00016DB0 File Offset: 0x00014FB0
		public event PlayFabEvents.PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest> OnAndroidDevicePushNotificationRegistrationRequestEvent;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060006F9 RID: 1785 RVA: 0x00016DE8 File Offset: 0x00014FE8
		// (remove) Token: 0x060006FA RID: 1786 RVA: 0x00016E20 File Offset: 0x00015020
		public event PlayFabEvents.PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult> OnAndroidDevicePushNotificationRegistrationResultEvent;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060006FB RID: 1787 RVA: 0x00016E58 File Offset: 0x00015058
		// (remove) Token: 0x060006FC RID: 1788 RVA: 0x00016E90 File Offset: 0x00015090
		public event PlayFabEvents.PlayFabRequestEvent<AttributeInstallRequest> OnAttributeInstallRequestEvent;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060006FD RID: 1789 RVA: 0x00016EC8 File Offset: 0x000150C8
		// (remove) Token: 0x060006FE RID: 1790 RVA: 0x00016F00 File Offset: 0x00015100
		public event PlayFabEvents.PlayFabResultEvent<AttributeInstallResult> OnAttributeInstallResultEvent;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060006FF RID: 1791 RVA: 0x00016F38 File Offset: 0x00015138
		// (remove) Token: 0x06000700 RID: 1792 RVA: 0x00016F70 File Offset: 0x00015170
		public event PlayFabEvents.PlayFabRequestEvent<CancelTradeRequest> OnCancelTradeRequestEvent;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000701 RID: 1793 RVA: 0x00016FA8 File Offset: 0x000151A8
		// (remove) Token: 0x06000702 RID: 1794 RVA: 0x00016FE0 File Offset: 0x000151E0
		public event PlayFabEvents.PlayFabResultEvent<CancelTradeResponse> OnCancelTradeResultEvent;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000703 RID: 1795 RVA: 0x00017018 File Offset: 0x00015218
		// (remove) Token: 0x06000704 RID: 1796 RVA: 0x00017050 File Offset: 0x00015250
		public event PlayFabEvents.PlayFabRequestEvent<ConfirmPurchaseRequest> OnConfirmPurchaseRequestEvent;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000705 RID: 1797 RVA: 0x00017088 File Offset: 0x00015288
		// (remove) Token: 0x06000706 RID: 1798 RVA: 0x000170C0 File Offset: 0x000152C0
		public event PlayFabEvents.PlayFabResultEvent<ConfirmPurchaseResult> OnConfirmPurchaseResultEvent;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000707 RID: 1799 RVA: 0x000170F8 File Offset: 0x000152F8
		// (remove) Token: 0x06000708 RID: 1800 RVA: 0x00017130 File Offset: 0x00015330
		public event PlayFabEvents.PlayFabRequestEvent<ConsumeItemRequest> OnConsumeItemRequestEvent;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000709 RID: 1801 RVA: 0x00017168 File Offset: 0x00015368
		// (remove) Token: 0x0600070A RID: 1802 RVA: 0x000171A0 File Offset: 0x000153A0
		public event PlayFabEvents.PlayFabResultEvent<ConsumeItemResult> OnConsumeItemResultEvent;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600070B RID: 1803 RVA: 0x000171D8 File Offset: 0x000153D8
		// (remove) Token: 0x0600070C RID: 1804 RVA: 0x00017210 File Offset: 0x00015410
		public event PlayFabEvents.PlayFabRequestEvent<ConsumePSNEntitlementsRequest> OnConsumePSNEntitlementsRequestEvent;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600070D RID: 1805 RVA: 0x00017248 File Offset: 0x00015448
		// (remove) Token: 0x0600070E RID: 1806 RVA: 0x00017280 File Offset: 0x00015480
		public event PlayFabEvents.PlayFabResultEvent<ConsumePSNEntitlementsResult> OnConsumePSNEntitlementsResultEvent;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600070F RID: 1807 RVA: 0x000172B8 File Offset: 0x000154B8
		// (remove) Token: 0x06000710 RID: 1808 RVA: 0x000172F0 File Offset: 0x000154F0
		public event PlayFabEvents.PlayFabRequestEvent<ConsumeXboxEntitlementsRequest> OnConsumeXboxEntitlementsRequestEvent;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000711 RID: 1809 RVA: 0x00017328 File Offset: 0x00015528
		// (remove) Token: 0x06000712 RID: 1810 RVA: 0x00017360 File Offset: 0x00015560
		public event PlayFabEvents.PlayFabResultEvent<ConsumeXboxEntitlementsResult> OnConsumeXboxEntitlementsResultEvent;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000713 RID: 1811 RVA: 0x00017398 File Offset: 0x00015598
		// (remove) Token: 0x06000714 RID: 1812 RVA: 0x000173D0 File Offset: 0x000155D0
		public event PlayFabEvents.PlayFabRequestEvent<CreateSharedGroupRequest> OnCreateSharedGroupRequestEvent;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000715 RID: 1813 RVA: 0x00017408 File Offset: 0x00015608
		// (remove) Token: 0x06000716 RID: 1814 RVA: 0x00017440 File Offset: 0x00015640
		public event PlayFabEvents.PlayFabResultEvent<CreateSharedGroupResult> OnCreateSharedGroupResultEvent;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000717 RID: 1815 RVA: 0x00017478 File Offset: 0x00015678
		// (remove) Token: 0x06000718 RID: 1816 RVA: 0x000174B0 File Offset: 0x000156B0
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptRequest> OnExecuteCloudScriptRequestEvent;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000719 RID: 1817 RVA: 0x000174E8 File Offset: 0x000156E8
		// (remove) Token: 0x0600071A RID: 1818 RVA: 0x00017520 File Offset: 0x00015720
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult> OnExecuteCloudScriptResultEvent;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600071B RID: 1819 RVA: 0x00017558 File Offset: 0x00015758
		// (remove) Token: 0x0600071C RID: 1820 RVA: 0x00017590 File Offset: 0x00015790
		public event PlayFabEvents.PlayFabRequestEvent<GetAccountInfoRequest> OnGetAccountInfoRequestEvent;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600071D RID: 1821 RVA: 0x000175C8 File Offset: 0x000157C8
		// (remove) Token: 0x0600071E RID: 1822 RVA: 0x00017600 File Offset: 0x00015800
		public event PlayFabEvents.PlayFabResultEvent<GetAccountInfoResult> OnGetAccountInfoResultEvent;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600071F RID: 1823 RVA: 0x00017638 File Offset: 0x00015838
		// (remove) Token: 0x06000720 RID: 1824 RVA: 0x00017670 File Offset: 0x00015870
		public event PlayFabEvents.PlayFabRequestEvent<GetAdPlacementsRequest> OnGetAdPlacementsRequestEvent;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000721 RID: 1825 RVA: 0x000176A8 File Offset: 0x000158A8
		// (remove) Token: 0x06000722 RID: 1826 RVA: 0x000176E0 File Offset: 0x000158E0
		public event PlayFabEvents.PlayFabResultEvent<GetAdPlacementsResult> OnGetAdPlacementsResultEvent;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000723 RID: 1827 RVA: 0x00017718 File Offset: 0x00015918
		// (remove) Token: 0x06000724 RID: 1828 RVA: 0x00017750 File Offset: 0x00015950
		public event PlayFabEvents.PlayFabRequestEvent<ListUsersCharactersRequest> OnGetAllUsersCharactersRequestEvent;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000725 RID: 1829 RVA: 0x00017788 File Offset: 0x00015988
		// (remove) Token: 0x06000726 RID: 1830 RVA: 0x000177C0 File Offset: 0x000159C0
		public event PlayFabEvents.PlayFabResultEvent<ListUsersCharactersResult> OnGetAllUsersCharactersResultEvent;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000727 RID: 1831 RVA: 0x000177F8 File Offset: 0x000159F8
		// (remove) Token: 0x06000728 RID: 1832 RVA: 0x00017830 File Offset: 0x00015A30
		public event PlayFabEvents.PlayFabRequestEvent<GetCatalogItemsRequest> OnGetCatalogItemsRequestEvent;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000729 RID: 1833 RVA: 0x00017868 File Offset: 0x00015A68
		// (remove) Token: 0x0600072A RID: 1834 RVA: 0x000178A0 File Offset: 0x00015AA0
		public event PlayFabEvents.PlayFabResultEvent<GetCatalogItemsResult> OnGetCatalogItemsResultEvent;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600072B RID: 1835 RVA: 0x000178D8 File Offset: 0x00015AD8
		// (remove) Token: 0x0600072C RID: 1836 RVA: 0x00017910 File Offset: 0x00015B10
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest> OnGetCharacterDataRequestEvent;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600072D RID: 1837 RVA: 0x00017948 File Offset: 0x00015B48
		// (remove) Token: 0x0600072E RID: 1838 RVA: 0x00017980 File Offset: 0x00015B80
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult> OnGetCharacterDataResultEvent;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600072F RID: 1839 RVA: 0x000179B8 File Offset: 0x00015BB8
		// (remove) Token: 0x06000730 RID: 1840 RVA: 0x000179F0 File Offset: 0x00015BF0
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterInventoryRequest> OnGetCharacterInventoryRequestEvent;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000731 RID: 1841 RVA: 0x00017A28 File Offset: 0x00015C28
		// (remove) Token: 0x06000732 RID: 1842 RVA: 0x00017A60 File Offset: 0x00015C60
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterInventoryResult> OnGetCharacterInventoryResultEvent;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000733 RID: 1843 RVA: 0x00017A98 File Offset: 0x00015C98
		// (remove) Token: 0x06000734 RID: 1844 RVA: 0x00017AD0 File Offset: 0x00015CD0
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterLeaderboardRequest> OnGetCharacterLeaderboardRequestEvent;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000735 RID: 1845 RVA: 0x00017B08 File Offset: 0x00015D08
		// (remove) Token: 0x06000736 RID: 1846 RVA: 0x00017B40 File Offset: 0x00015D40
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterLeaderboardResult> OnGetCharacterLeaderboardResultEvent;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000737 RID: 1847 RVA: 0x00017B78 File Offset: 0x00015D78
		// (remove) Token: 0x06000738 RID: 1848 RVA: 0x00017BB0 File Offset: 0x00015DB0
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest> OnGetCharacterReadOnlyDataRequestEvent;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000739 RID: 1849 RVA: 0x00017BE8 File Offset: 0x00015DE8
		// (remove) Token: 0x0600073A RID: 1850 RVA: 0x00017C20 File Offset: 0x00015E20
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult> OnGetCharacterReadOnlyDataResultEvent;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x0600073B RID: 1851 RVA: 0x00017C58 File Offset: 0x00015E58
		// (remove) Token: 0x0600073C RID: 1852 RVA: 0x00017C90 File Offset: 0x00015E90
		public event PlayFabEvents.PlayFabRequestEvent<GetCharacterStatisticsRequest> OnGetCharacterStatisticsRequestEvent;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x0600073D RID: 1853 RVA: 0x00017CC8 File Offset: 0x00015EC8
		// (remove) Token: 0x0600073E RID: 1854 RVA: 0x00017D00 File Offset: 0x00015F00
		public event PlayFabEvents.PlayFabResultEvent<GetCharacterStatisticsResult> OnGetCharacterStatisticsResultEvent;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x0600073F RID: 1855 RVA: 0x00017D38 File Offset: 0x00015F38
		// (remove) Token: 0x06000740 RID: 1856 RVA: 0x00017D70 File Offset: 0x00015F70
		public event PlayFabEvents.PlayFabRequestEvent<GetContentDownloadUrlRequest> OnGetContentDownloadUrlRequestEvent;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000741 RID: 1857 RVA: 0x00017DA8 File Offset: 0x00015FA8
		// (remove) Token: 0x06000742 RID: 1858 RVA: 0x00017DE0 File Offset: 0x00015FE0
		public event PlayFabEvents.PlayFabResultEvent<GetContentDownloadUrlResult> OnGetContentDownloadUrlResultEvent;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000743 RID: 1859 RVA: 0x00017E18 File Offset: 0x00016018
		// (remove) Token: 0x06000744 RID: 1860 RVA: 0x00017E50 File Offset: 0x00016050
		public event PlayFabEvents.PlayFabRequestEvent<CurrentGamesRequest> OnGetCurrentGamesRequestEvent;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000745 RID: 1861 RVA: 0x00017E88 File Offset: 0x00016088
		// (remove) Token: 0x06000746 RID: 1862 RVA: 0x00017EC0 File Offset: 0x000160C0
		public event PlayFabEvents.PlayFabResultEvent<CurrentGamesResult> OnGetCurrentGamesResultEvent;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000747 RID: 1863 RVA: 0x00017EF8 File Offset: 0x000160F8
		// (remove) Token: 0x06000748 RID: 1864 RVA: 0x00017F30 File Offset: 0x00016130
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardRequest> OnGetFriendLeaderboardRequestEvent;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000749 RID: 1865 RVA: 0x00017F68 File Offset: 0x00016168
		// (remove) Token: 0x0600074A RID: 1866 RVA: 0x00017FA0 File Offset: 0x000161A0
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult> OnGetFriendLeaderboardResultEvent;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600074B RID: 1867 RVA: 0x00017FD8 File Offset: 0x000161D8
		// (remove) Token: 0x0600074C RID: 1868 RVA: 0x00018010 File Offset: 0x00016210
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest> OnGetFriendLeaderboardAroundPlayerRequestEvent;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600074D RID: 1869 RVA: 0x00018048 File Offset: 0x00016248
		// (remove) Token: 0x0600074E RID: 1870 RVA: 0x00018080 File Offset: 0x00016280
		public event PlayFabEvents.PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult> OnGetFriendLeaderboardAroundPlayerResultEvent;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600074F RID: 1871 RVA: 0x000180B8 File Offset: 0x000162B8
		// (remove) Token: 0x06000750 RID: 1872 RVA: 0x000180F0 File Offset: 0x000162F0
		public event PlayFabEvents.PlayFabRequestEvent<GetFriendsListRequest> OnGetFriendsListRequestEvent;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06000751 RID: 1873 RVA: 0x00018128 File Offset: 0x00016328
		// (remove) Token: 0x06000752 RID: 1874 RVA: 0x00018160 File Offset: 0x00016360
		public event PlayFabEvents.PlayFabResultEvent<GetFriendsListResult> OnGetFriendsListResultEvent;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06000753 RID: 1875 RVA: 0x00018198 File Offset: 0x00016398
		// (remove) Token: 0x06000754 RID: 1876 RVA: 0x000181D0 File Offset: 0x000163D0
		public event PlayFabEvents.PlayFabRequestEvent<GameServerRegionsRequest> OnGetGameServerRegionsRequestEvent;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06000755 RID: 1877 RVA: 0x00018208 File Offset: 0x00016408
		// (remove) Token: 0x06000756 RID: 1878 RVA: 0x00018240 File Offset: 0x00016440
		public event PlayFabEvents.PlayFabResultEvent<GameServerRegionsResult> OnGetGameServerRegionsResultEvent;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000757 RID: 1879 RVA: 0x00018278 File Offset: 0x00016478
		// (remove) Token: 0x06000758 RID: 1880 RVA: 0x000182B0 File Offset: 0x000164B0
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardRequest> OnGetLeaderboardRequestEvent;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06000759 RID: 1881 RVA: 0x000182E8 File Offset: 0x000164E8
		// (remove) Token: 0x0600075A RID: 1882 RVA: 0x00018320 File Offset: 0x00016520
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult> OnGetLeaderboardResultEvent;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x0600075B RID: 1883 RVA: 0x00018358 File Offset: 0x00016558
		// (remove) Token: 0x0600075C RID: 1884 RVA: 0x00018390 File Offset: 0x00016590
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest> OnGetLeaderboardAroundCharacterRequestEvent;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600075D RID: 1885 RVA: 0x000183C8 File Offset: 0x000165C8
		// (remove) Token: 0x0600075E RID: 1886 RVA: 0x00018400 File Offset: 0x00016600
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundCharacterResult> OnGetLeaderboardAroundCharacterResultEvent;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600075F RID: 1887 RVA: 0x00018438 File Offset: 0x00016638
		// (remove) Token: 0x06000760 RID: 1888 RVA: 0x00018470 File Offset: 0x00016670
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest> OnGetLeaderboardAroundPlayerRequestEvent;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000761 RID: 1889 RVA: 0x000184A8 File Offset: 0x000166A8
		// (remove) Token: 0x06000762 RID: 1890 RVA: 0x000184E0 File Offset: 0x000166E0
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundPlayerResult> OnGetLeaderboardAroundPlayerResultEvent;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06000763 RID: 1891 RVA: 0x00018518 File Offset: 0x00016718
		// (remove) Token: 0x06000764 RID: 1892 RVA: 0x00018550 File Offset: 0x00016750
		public event PlayFabEvents.PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest> OnGetLeaderboardForUserCharactersRequestEvent;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000765 RID: 1893 RVA: 0x00018588 File Offset: 0x00016788
		// (remove) Token: 0x06000766 RID: 1894 RVA: 0x000185C0 File Offset: 0x000167C0
		public event PlayFabEvents.PlayFabResultEvent<GetLeaderboardForUsersCharactersResult> OnGetLeaderboardForUserCharactersResultEvent;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000767 RID: 1895 RVA: 0x000185F8 File Offset: 0x000167F8
		// (remove) Token: 0x06000768 RID: 1896 RVA: 0x00018630 File Offset: 0x00016830
		public event PlayFabEvents.PlayFabRequestEvent<GetPaymentTokenRequest> OnGetPaymentTokenRequestEvent;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000769 RID: 1897 RVA: 0x00018668 File Offset: 0x00016868
		// (remove) Token: 0x0600076A RID: 1898 RVA: 0x000186A0 File Offset: 0x000168A0
		public event PlayFabEvents.PlayFabResultEvent<GetPaymentTokenResult> OnGetPaymentTokenResultEvent;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600076B RID: 1899 RVA: 0x000186D8 File Offset: 0x000168D8
		// (remove) Token: 0x0600076C RID: 1900 RVA: 0x00018710 File Offset: 0x00016910
		public event PlayFabEvents.PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest> OnGetPhotonAuthenticationTokenRequestEvent;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600076D RID: 1901 RVA: 0x00018748 File Offset: 0x00016948
		// (remove) Token: 0x0600076E RID: 1902 RVA: 0x00018780 File Offset: 0x00016980
		public event PlayFabEvents.PlayFabResultEvent<GetPhotonAuthenticationTokenResult> OnGetPhotonAuthenticationTokenResultEvent;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x0600076F RID: 1903 RVA: 0x000187B8 File Offset: 0x000169B8
		// (remove) Token: 0x06000770 RID: 1904 RVA: 0x000187F0 File Offset: 0x000169F0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerCombinedInfoRequest> OnGetPlayerCombinedInfoRequestEvent;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000771 RID: 1905 RVA: 0x00018828 File Offset: 0x00016A28
		// (remove) Token: 0x06000772 RID: 1906 RVA: 0x00018860 File Offset: 0x00016A60
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerCombinedInfoResult> OnGetPlayerCombinedInfoResultEvent;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000773 RID: 1907 RVA: 0x00018898 File Offset: 0x00016A98
		// (remove) Token: 0x06000774 RID: 1908 RVA: 0x000188D0 File Offset: 0x00016AD0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerProfileRequest> OnGetPlayerProfileRequestEvent;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06000775 RID: 1909 RVA: 0x00018908 File Offset: 0x00016B08
		// (remove) Token: 0x06000776 RID: 1910 RVA: 0x00018940 File Offset: 0x00016B40
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerProfileResult> OnGetPlayerProfileResultEvent;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06000777 RID: 1911 RVA: 0x00018978 File Offset: 0x00016B78
		// (remove) Token: 0x06000778 RID: 1912 RVA: 0x000189B0 File Offset: 0x00016BB0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerSegmentsRequest> OnGetPlayerSegmentsRequestEvent;

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000779 RID: 1913 RVA: 0x000189E8 File Offset: 0x00016BE8
		// (remove) Token: 0x0600077A RID: 1914 RVA: 0x00018A20 File Offset: 0x00016C20
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerSegmentsResult> OnGetPlayerSegmentsResultEvent;

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x0600077B RID: 1915 RVA: 0x00018A58 File Offset: 0x00016C58
		// (remove) Token: 0x0600077C RID: 1916 RVA: 0x00018A90 File Offset: 0x00016C90
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticsRequest> OnGetPlayerStatisticsRequestEvent;

		// Token: 0x14000059 RID: 89
		// (add) Token: 0x0600077D RID: 1917 RVA: 0x00018AC8 File Offset: 0x00016CC8
		// (remove) Token: 0x0600077E RID: 1918 RVA: 0x00018B00 File Offset: 0x00016D00
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticsResult> OnGetPlayerStatisticsResultEvent;

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x0600077F RID: 1919 RVA: 0x00018B38 File Offset: 0x00016D38
		// (remove) Token: 0x06000780 RID: 1920 RVA: 0x00018B70 File Offset: 0x00016D70
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticVersionsRequest> OnGetPlayerStatisticVersionsRequestEvent;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06000781 RID: 1921 RVA: 0x00018BA8 File Offset: 0x00016DA8
		// (remove) Token: 0x06000782 RID: 1922 RVA: 0x00018BE0 File Offset: 0x00016DE0
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticVersionsResult> OnGetPlayerStatisticVersionsResultEvent;

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06000783 RID: 1923 RVA: 0x00018C18 File Offset: 0x00016E18
		// (remove) Token: 0x06000784 RID: 1924 RVA: 0x00018C50 File Offset: 0x00016E50
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerTagsRequest> OnGetPlayerTagsRequestEvent;

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06000785 RID: 1925 RVA: 0x00018C88 File Offset: 0x00016E88
		// (remove) Token: 0x06000786 RID: 1926 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerTagsResult> OnGetPlayerTagsResultEvent;

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06000787 RID: 1927 RVA: 0x00018CF8 File Offset: 0x00016EF8
		// (remove) Token: 0x06000788 RID: 1928 RVA: 0x00018D30 File Offset: 0x00016F30
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayerTradesRequest> OnGetPlayerTradesRequestEvent;

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06000789 RID: 1929 RVA: 0x00018D68 File Offset: 0x00016F68
		// (remove) Token: 0x0600078A RID: 1930 RVA: 0x00018DA0 File Offset: 0x00016FA0
		public event PlayFabEvents.PlayFabResultEvent<GetPlayerTradesResponse> OnGetPlayerTradesResultEvent;

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x0600078B RID: 1931 RVA: 0x00018DD8 File Offset: 0x00016FD8
		// (remove) Token: 0x0600078C RID: 1932 RVA: 0x00018E10 File Offset: 0x00017010
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest> OnGetPlayFabIDsFromFacebookIDsRequestEvent;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x0600078D RID: 1933 RVA: 0x00018E48 File Offset: 0x00017048
		// (remove) Token: 0x0600078E RID: 1934 RVA: 0x00018E80 File Offset: 0x00017080
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult> OnGetPlayFabIDsFromFacebookIDsResultEvent;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x0600078F RID: 1935 RVA: 0x00018EB8 File Offset: 0x000170B8
		// (remove) Token: 0x06000790 RID: 1936 RVA: 0x00018EF0 File Offset: 0x000170F0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest> OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000791 RID: 1937 RVA: 0x00018F28 File Offset: 0x00017128
		// (remove) Token: 0x06000792 RID: 1938 RVA: 0x00018F60 File Offset: 0x00017160
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult> OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent;

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000793 RID: 1939 RVA: 0x00018F98 File Offset: 0x00017198
		// (remove) Token: 0x06000794 RID: 1940 RVA: 0x00018FD0 File Offset: 0x000171D0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest> OnGetPlayFabIDsFromGameCenterIDsRequestEvent;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000795 RID: 1941 RVA: 0x00019008 File Offset: 0x00017208
		// (remove) Token: 0x06000796 RID: 1942 RVA: 0x00019040 File Offset: 0x00017240
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult> OnGetPlayFabIDsFromGameCenterIDsResultEvent;

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06000797 RID: 1943 RVA: 0x00019078 File Offset: 0x00017278
		// (remove) Token: 0x06000798 RID: 1944 RVA: 0x000190B0 File Offset: 0x000172B0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest> OnGetPlayFabIDsFromGenericIDsRequestEvent;

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000799 RID: 1945 RVA: 0x000190E8 File Offset: 0x000172E8
		// (remove) Token: 0x0600079A RID: 1946 RVA: 0x00019120 File Offset: 0x00017320
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult> OnGetPlayFabIDsFromGenericIDsResultEvent;

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x0600079B RID: 1947 RVA: 0x00019158 File Offset: 0x00017358
		// (remove) Token: 0x0600079C RID: 1948 RVA: 0x00019190 File Offset: 0x00017390
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest> OnGetPlayFabIDsFromGoogleIDsRequestEvent;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x0600079D RID: 1949 RVA: 0x000191C8 File Offset: 0x000173C8
		// (remove) Token: 0x0600079E RID: 1950 RVA: 0x00019200 File Offset: 0x00017400
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult> OnGetPlayFabIDsFromGoogleIDsResultEvent;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x0600079F RID: 1951 RVA: 0x00019238 File Offset: 0x00017438
		// (remove) Token: 0x060007A0 RID: 1952 RVA: 0x00019270 File Offset: 0x00017470
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest> OnGetPlayFabIDsFromKongregateIDsRequestEvent;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060007A1 RID: 1953 RVA: 0x000192A8 File Offset: 0x000174A8
		// (remove) Token: 0x060007A2 RID: 1954 RVA: 0x000192E0 File Offset: 0x000174E0
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult> OnGetPlayFabIDsFromKongregateIDsResultEvent;

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060007A3 RID: 1955 RVA: 0x00019318 File Offset: 0x00017518
		// (remove) Token: 0x060007A4 RID: 1956 RVA: 0x00019350 File Offset: 0x00017550
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest> OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent;

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060007A5 RID: 1957 RVA: 0x00019388 File Offset: 0x00017588
		// (remove) Token: 0x060007A6 RID: 1958 RVA: 0x000193C0 File Offset: 0x000175C0
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult> OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent;

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060007A7 RID: 1959 RVA: 0x000193F8 File Offset: 0x000175F8
		// (remove) Token: 0x060007A8 RID: 1960 RVA: 0x00019430 File Offset: 0x00017630
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest> OnGetPlayFabIDsFromPSNAccountIDsRequestEvent;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060007A9 RID: 1961 RVA: 0x00019468 File Offset: 0x00017668
		// (remove) Token: 0x060007AA RID: 1962 RVA: 0x000194A0 File Offset: 0x000176A0
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult> OnGetPlayFabIDsFromPSNAccountIDsResultEvent;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060007AB RID: 1963 RVA: 0x000194D8 File Offset: 0x000176D8
		// (remove) Token: 0x060007AC RID: 1964 RVA: 0x00019510 File Offset: 0x00017710
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest> OnGetPlayFabIDsFromSteamIDsRequestEvent;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060007AD RID: 1965 RVA: 0x00019548 File Offset: 0x00017748
		// (remove) Token: 0x060007AE RID: 1966 RVA: 0x00019580 File Offset: 0x00017780
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult> OnGetPlayFabIDsFromSteamIDsResultEvent;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060007AF RID: 1967 RVA: 0x000195B8 File Offset: 0x000177B8
		// (remove) Token: 0x060007B0 RID: 1968 RVA: 0x000195F0 File Offset: 0x000177F0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest> OnGetPlayFabIDsFromTwitchIDsRequestEvent;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060007B1 RID: 1969 RVA: 0x00019628 File Offset: 0x00017828
		// (remove) Token: 0x060007B2 RID: 1970 RVA: 0x00019660 File Offset: 0x00017860
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult> OnGetPlayFabIDsFromTwitchIDsResultEvent;

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060007B3 RID: 1971 RVA: 0x00019698 File Offset: 0x00017898
		// (remove) Token: 0x060007B4 RID: 1972 RVA: 0x000196D0 File Offset: 0x000178D0
		public event PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest> OnGetPlayFabIDsFromXboxLiveIDsRequestEvent;

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060007B5 RID: 1973 RVA: 0x00019708 File Offset: 0x00017908
		// (remove) Token: 0x060007B6 RID: 1974 RVA: 0x00019740 File Offset: 0x00017940
		public event PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult> OnGetPlayFabIDsFromXboxLiveIDsResultEvent;

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060007B7 RID: 1975 RVA: 0x00019778 File Offset: 0x00017978
		// (remove) Token: 0x060007B8 RID: 1976 RVA: 0x000197B0 File Offset: 0x000179B0
		public event PlayFabEvents.PlayFabRequestEvent<GetPublisherDataRequest> OnGetPublisherDataRequestEvent;

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060007B9 RID: 1977 RVA: 0x000197E8 File Offset: 0x000179E8
		// (remove) Token: 0x060007BA RID: 1978 RVA: 0x00019820 File Offset: 0x00017A20
		public event PlayFabEvents.PlayFabResultEvent<GetPublisherDataResult> OnGetPublisherDataResultEvent;

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x060007BB RID: 1979 RVA: 0x00019858 File Offset: 0x00017A58
		// (remove) Token: 0x060007BC RID: 1980 RVA: 0x00019890 File Offset: 0x00017A90
		public event PlayFabEvents.PlayFabRequestEvent<GetPurchaseRequest> OnGetPurchaseRequestEvent;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060007BD RID: 1981 RVA: 0x000198C8 File Offset: 0x00017AC8
		// (remove) Token: 0x060007BE RID: 1982 RVA: 0x00019900 File Offset: 0x00017B00
		public event PlayFabEvents.PlayFabResultEvent<GetPurchaseResult> OnGetPurchaseResultEvent;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060007BF RID: 1983 RVA: 0x00019938 File Offset: 0x00017B38
		// (remove) Token: 0x060007C0 RID: 1984 RVA: 0x00019970 File Offset: 0x00017B70
		public event PlayFabEvents.PlayFabRequestEvent<GetSharedGroupDataRequest> OnGetSharedGroupDataRequestEvent;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060007C1 RID: 1985 RVA: 0x000199A8 File Offset: 0x00017BA8
		// (remove) Token: 0x060007C2 RID: 1986 RVA: 0x000199E0 File Offset: 0x00017BE0
		public event PlayFabEvents.PlayFabResultEvent<GetSharedGroupDataResult> OnGetSharedGroupDataResultEvent;

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060007C3 RID: 1987 RVA: 0x00019A18 File Offset: 0x00017C18
		// (remove) Token: 0x060007C4 RID: 1988 RVA: 0x00019A50 File Offset: 0x00017C50
		public event PlayFabEvents.PlayFabRequestEvent<GetStoreItemsRequest> OnGetStoreItemsRequestEvent;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060007C5 RID: 1989 RVA: 0x00019A88 File Offset: 0x00017C88
		// (remove) Token: 0x060007C6 RID: 1990 RVA: 0x00019AC0 File Offset: 0x00017CC0
		public event PlayFabEvents.PlayFabResultEvent<GetStoreItemsResult> OnGetStoreItemsResultEvent;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060007C7 RID: 1991 RVA: 0x00019AF8 File Offset: 0x00017CF8
		// (remove) Token: 0x060007C8 RID: 1992 RVA: 0x00019B30 File Offset: 0x00017D30
		public event PlayFabEvents.PlayFabRequestEvent<GetTimeRequest> OnGetTimeRequestEvent;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060007C9 RID: 1993 RVA: 0x00019B68 File Offset: 0x00017D68
		// (remove) Token: 0x060007CA RID: 1994 RVA: 0x00019BA0 File Offset: 0x00017DA0
		public event PlayFabEvents.PlayFabResultEvent<GetTimeResult> OnGetTimeResultEvent;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x060007CB RID: 1995 RVA: 0x00019BD8 File Offset: 0x00017DD8
		// (remove) Token: 0x060007CC RID: 1996 RVA: 0x00019C10 File Offset: 0x00017E10
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleDataRequest> OnGetTitleDataRequestEvent;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060007CD RID: 1997 RVA: 0x00019C48 File Offset: 0x00017E48
		// (remove) Token: 0x060007CE RID: 1998 RVA: 0x00019C80 File Offset: 0x00017E80
		public event PlayFabEvents.PlayFabResultEvent<GetTitleDataResult> OnGetTitleDataResultEvent;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060007CF RID: 1999 RVA: 0x00019CB8 File Offset: 0x00017EB8
		// (remove) Token: 0x060007D0 RID: 2000 RVA: 0x00019CF0 File Offset: 0x00017EF0
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleNewsRequest> OnGetTitleNewsRequestEvent;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x060007D1 RID: 2001 RVA: 0x00019D28 File Offset: 0x00017F28
		// (remove) Token: 0x060007D2 RID: 2002 RVA: 0x00019D60 File Offset: 0x00017F60
		public event PlayFabEvents.PlayFabResultEvent<GetTitleNewsResult> OnGetTitleNewsResultEvent;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x060007D3 RID: 2003 RVA: 0x00019D98 File Offset: 0x00017F98
		// (remove) Token: 0x060007D4 RID: 2004 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public event PlayFabEvents.PlayFabRequestEvent<GetTitlePublicKeyRequest> OnGetTitlePublicKeyRequestEvent;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x060007D5 RID: 2005 RVA: 0x00019E08 File Offset: 0x00018008
		// (remove) Token: 0x060007D6 RID: 2006 RVA: 0x00019E40 File Offset: 0x00018040
		public event PlayFabEvents.PlayFabResultEvent<GetTitlePublicKeyResult> OnGetTitlePublicKeyResultEvent;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x060007D7 RID: 2007 RVA: 0x00019E78 File Offset: 0x00018078
		// (remove) Token: 0x060007D8 RID: 2008 RVA: 0x00019EB0 File Offset: 0x000180B0
		public event PlayFabEvents.PlayFabRequestEvent<GetTradeStatusRequest> OnGetTradeStatusRequestEvent;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060007D9 RID: 2009 RVA: 0x00019EE8 File Offset: 0x000180E8
		// (remove) Token: 0x060007DA RID: 2010 RVA: 0x00019F20 File Offset: 0x00018120
		public event PlayFabEvents.PlayFabResultEvent<GetTradeStatusResponse> OnGetTradeStatusResultEvent;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060007DB RID: 2011 RVA: 0x00019F58 File Offset: 0x00018158
		// (remove) Token: 0x060007DC RID: 2012 RVA: 0x00019F90 File Offset: 0x00018190
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserDataRequestEvent;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060007DD RID: 2013 RVA: 0x00019FC8 File Offset: 0x000181C8
		// (remove) Token: 0x060007DE RID: 2014 RVA: 0x0001A000 File Offset: 0x00018200
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserDataResultEvent;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x060007DF RID: 2015 RVA: 0x0001A038 File Offset: 0x00018238
		// (remove) Token: 0x060007E0 RID: 2016 RVA: 0x0001A070 File Offset: 0x00018270
		public event PlayFabEvents.PlayFabRequestEvent<GetUserInventoryRequest> OnGetUserInventoryRequestEvent;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060007E1 RID: 2017 RVA: 0x0001A0A8 File Offset: 0x000182A8
		// (remove) Token: 0x060007E2 RID: 2018 RVA: 0x0001A0E0 File Offset: 0x000182E0
		public event PlayFabEvents.PlayFabResultEvent<GetUserInventoryResult> OnGetUserInventoryResultEvent;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060007E3 RID: 2019 RVA: 0x0001A118 File Offset: 0x00018318
		// (remove) Token: 0x060007E4 RID: 2020 RVA: 0x0001A150 File Offset: 0x00018350
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserPublisherDataRequestEvent;

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x060007E5 RID: 2021 RVA: 0x0001A188 File Offset: 0x00018388
		// (remove) Token: 0x060007E6 RID: 2022 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserPublisherDataResultEvent;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x060007E7 RID: 2023 RVA: 0x0001A1F8 File Offset: 0x000183F8
		// (remove) Token: 0x060007E8 RID: 2024 RVA: 0x0001A230 File Offset: 0x00018430
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserPublisherReadOnlyDataRequestEvent;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x060007E9 RID: 2025 RVA: 0x0001A268 File Offset: 0x00018468
		// (remove) Token: 0x060007EA RID: 2026 RVA: 0x0001A2A0 File Offset: 0x000184A0
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserPublisherReadOnlyDataResultEvent;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x060007EB RID: 2027 RVA: 0x0001A2D8 File Offset: 0x000184D8
		// (remove) Token: 0x060007EC RID: 2028 RVA: 0x0001A310 File Offset: 0x00018510
		public event PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest> OnGetUserReadOnlyDataRequestEvent;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x060007ED RID: 2029 RVA: 0x0001A348 File Offset: 0x00018548
		// (remove) Token: 0x060007EE RID: 2030 RVA: 0x0001A380 File Offset: 0x00018580
		public event PlayFabEvents.PlayFabResultEvent<GetUserDataResult> OnGetUserReadOnlyDataResultEvent;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x060007EF RID: 2031 RVA: 0x0001A3B8 File Offset: 0x000185B8
		// (remove) Token: 0x060007F0 RID: 2032 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public event PlayFabEvents.PlayFabRequestEvent<GetWindowsHelloChallengeRequest> OnGetWindowsHelloChallengeRequestEvent;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x060007F1 RID: 2033 RVA: 0x0001A428 File Offset: 0x00018628
		// (remove) Token: 0x060007F2 RID: 2034 RVA: 0x0001A460 File Offset: 0x00018660
		public event PlayFabEvents.PlayFabResultEvent<GetWindowsHelloChallengeResponse> OnGetWindowsHelloChallengeResultEvent;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x060007F3 RID: 2035 RVA: 0x0001A498 File Offset: 0x00018698
		// (remove) Token: 0x060007F4 RID: 2036 RVA: 0x0001A4D0 File Offset: 0x000186D0
		public event PlayFabEvents.PlayFabRequestEvent<GrantCharacterToUserRequest> OnGrantCharacterToUserRequestEvent;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060007F5 RID: 2037 RVA: 0x0001A508 File Offset: 0x00018708
		// (remove) Token: 0x060007F6 RID: 2038 RVA: 0x0001A540 File Offset: 0x00018740
		public event PlayFabEvents.PlayFabResultEvent<GrantCharacterToUserResult> OnGrantCharacterToUserResultEvent;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x060007F7 RID: 2039 RVA: 0x0001A578 File Offset: 0x00018778
		// (remove) Token: 0x060007F8 RID: 2040 RVA: 0x0001A5B0 File Offset: 0x000187B0
		public event PlayFabEvents.PlayFabRequestEvent<LinkAndroidDeviceIDRequest> OnLinkAndroidDeviceIDRequestEvent;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x060007F9 RID: 2041 RVA: 0x0001A5E8 File Offset: 0x000187E8
		// (remove) Token: 0x060007FA RID: 2042 RVA: 0x0001A620 File Offset: 0x00018820
		public event PlayFabEvents.PlayFabResultEvent<LinkAndroidDeviceIDResult> OnLinkAndroidDeviceIDResultEvent;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x060007FB RID: 2043 RVA: 0x0001A658 File Offset: 0x00018858
		// (remove) Token: 0x060007FC RID: 2044 RVA: 0x0001A690 File Offset: 0x00018890
		public event PlayFabEvents.PlayFabRequestEvent<LinkAppleRequest> OnLinkAppleRequestEvent;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060007FD RID: 2045 RVA: 0x0001A6C8 File Offset: 0x000188C8
		// (remove) Token: 0x060007FE RID: 2046 RVA: 0x0001A700 File Offset: 0x00018900
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnLinkAppleResultEvent;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x060007FF RID: 2047 RVA: 0x0001A738 File Offset: 0x00018938
		// (remove) Token: 0x06000800 RID: 2048 RVA: 0x0001A770 File Offset: 0x00018970
		public event PlayFabEvents.PlayFabRequestEvent<LinkCustomIDRequest> OnLinkCustomIDRequestEvent;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06000801 RID: 2049 RVA: 0x0001A7A8 File Offset: 0x000189A8
		// (remove) Token: 0x06000802 RID: 2050 RVA: 0x0001A7E0 File Offset: 0x000189E0
		public event PlayFabEvents.PlayFabResultEvent<LinkCustomIDResult> OnLinkCustomIDResultEvent;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06000803 RID: 2051 RVA: 0x0001A818 File Offset: 0x00018A18
		// (remove) Token: 0x06000804 RID: 2052 RVA: 0x0001A850 File Offset: 0x00018A50
		public event PlayFabEvents.PlayFabRequestEvent<LinkFacebookAccountRequest> OnLinkFacebookAccountRequestEvent;

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06000805 RID: 2053 RVA: 0x0001A888 File Offset: 0x00018A88
		// (remove) Token: 0x06000806 RID: 2054 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public event PlayFabEvents.PlayFabResultEvent<LinkFacebookAccountResult> OnLinkFacebookAccountResultEvent;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06000807 RID: 2055 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		// (remove) Token: 0x06000808 RID: 2056 RVA: 0x0001A930 File Offset: 0x00018B30
		public event PlayFabEvents.PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest> OnLinkFacebookInstantGamesIdRequestEvent;

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06000809 RID: 2057 RVA: 0x0001A968 File Offset: 0x00018B68
		// (remove) Token: 0x0600080A RID: 2058 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		public event PlayFabEvents.PlayFabResultEvent<LinkFacebookInstantGamesIdResult> OnLinkFacebookInstantGamesIdResultEvent;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x0600080B RID: 2059 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		// (remove) Token: 0x0600080C RID: 2060 RVA: 0x0001AA10 File Offset: 0x00018C10
		public event PlayFabEvents.PlayFabRequestEvent<LinkGameCenterAccountRequest> OnLinkGameCenterAccountRequestEvent;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x0600080D RID: 2061 RVA: 0x0001AA48 File Offset: 0x00018C48
		// (remove) Token: 0x0600080E RID: 2062 RVA: 0x0001AA80 File Offset: 0x00018C80
		public event PlayFabEvents.PlayFabResultEvent<LinkGameCenterAccountResult> OnLinkGameCenterAccountResultEvent;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x0600080F RID: 2063 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		// (remove) Token: 0x06000810 RID: 2064 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		public event PlayFabEvents.PlayFabRequestEvent<LinkGoogleAccountRequest> OnLinkGoogleAccountRequestEvent;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06000811 RID: 2065 RVA: 0x0001AB28 File Offset: 0x00018D28
		// (remove) Token: 0x06000812 RID: 2066 RVA: 0x0001AB60 File Offset: 0x00018D60
		public event PlayFabEvents.PlayFabResultEvent<LinkGoogleAccountResult> OnLinkGoogleAccountResultEvent;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x06000813 RID: 2067 RVA: 0x0001AB98 File Offset: 0x00018D98
		// (remove) Token: 0x06000814 RID: 2068 RVA: 0x0001ABD0 File Offset: 0x00018DD0
		public event PlayFabEvents.PlayFabRequestEvent<LinkIOSDeviceIDRequest> OnLinkIOSDeviceIDRequestEvent;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x06000815 RID: 2069 RVA: 0x0001AC08 File Offset: 0x00018E08
		// (remove) Token: 0x06000816 RID: 2070 RVA: 0x0001AC40 File Offset: 0x00018E40
		public event PlayFabEvents.PlayFabResultEvent<LinkIOSDeviceIDResult> OnLinkIOSDeviceIDResultEvent;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06000817 RID: 2071 RVA: 0x0001AC78 File Offset: 0x00018E78
		// (remove) Token: 0x06000818 RID: 2072 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		public event PlayFabEvents.PlayFabRequestEvent<LinkKongregateAccountRequest> OnLinkKongregateRequestEvent;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06000819 RID: 2073 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		// (remove) Token: 0x0600081A RID: 2074 RVA: 0x0001AD20 File Offset: 0x00018F20
		public event PlayFabEvents.PlayFabResultEvent<LinkKongregateAccountResult> OnLinkKongregateResultEvent;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x0600081B RID: 2075 RVA: 0x0001AD58 File Offset: 0x00018F58
		// (remove) Token: 0x0600081C RID: 2076 RVA: 0x0001AD90 File Offset: 0x00018F90
		public event PlayFabEvents.PlayFabRequestEvent<LinkNintendoServiceAccountRequest> OnLinkNintendoServiceAccountRequestEvent;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x0600081D RID: 2077 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		// (remove) Token: 0x0600081E RID: 2078 RVA: 0x0001AE00 File Offset: 0x00019000
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnLinkNintendoServiceAccountResultEvent;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x0600081F RID: 2079 RVA: 0x0001AE38 File Offset: 0x00019038
		// (remove) Token: 0x06000820 RID: 2080 RVA: 0x0001AE70 File Offset: 0x00019070
		public event PlayFabEvents.PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest> OnLinkNintendoSwitchDeviceIdRequestEvent;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06000821 RID: 2081 RVA: 0x0001AEA8 File Offset: 0x000190A8
		// (remove) Token: 0x06000822 RID: 2082 RVA: 0x0001AEE0 File Offset: 0x000190E0
		public event PlayFabEvents.PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult> OnLinkNintendoSwitchDeviceIdResultEvent;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06000823 RID: 2083 RVA: 0x0001AF18 File Offset: 0x00019118
		// (remove) Token: 0x06000824 RID: 2084 RVA: 0x0001AF50 File Offset: 0x00019150
		public event PlayFabEvents.PlayFabRequestEvent<LinkOpenIdConnectRequest> OnLinkOpenIdConnectRequestEvent;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06000825 RID: 2085 RVA: 0x0001AF88 File Offset: 0x00019188
		// (remove) Token: 0x06000826 RID: 2086 RVA: 0x0001AFC0 File Offset: 0x000191C0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult> OnLinkOpenIdConnectResultEvent;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06000827 RID: 2087 RVA: 0x0001AFF8 File Offset: 0x000191F8
		// (remove) Token: 0x06000828 RID: 2088 RVA: 0x0001B030 File Offset: 0x00019230
		public event PlayFabEvents.PlayFabRequestEvent<LinkPSNAccountRequest> OnLinkPSNAccountRequestEvent;

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06000829 RID: 2089 RVA: 0x0001B068 File Offset: 0x00019268
		// (remove) Token: 0x0600082A RID: 2090 RVA: 0x0001B0A0 File Offset: 0x000192A0
		public event PlayFabEvents.PlayFabResultEvent<LinkPSNAccountResult> OnLinkPSNAccountResultEvent;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x0600082B RID: 2091 RVA: 0x0001B0D8 File Offset: 0x000192D8
		// (remove) Token: 0x0600082C RID: 2092 RVA: 0x0001B110 File Offset: 0x00019310
		public event PlayFabEvents.PlayFabRequestEvent<LinkSteamAccountRequest> OnLinkSteamAccountRequestEvent;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x0600082D RID: 2093 RVA: 0x0001B148 File Offset: 0x00019348
		// (remove) Token: 0x0600082E RID: 2094 RVA: 0x0001B180 File Offset: 0x00019380
		public event PlayFabEvents.PlayFabResultEvent<LinkSteamAccountResult> OnLinkSteamAccountResultEvent;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x0600082F RID: 2095 RVA: 0x0001B1B8 File Offset: 0x000193B8
		// (remove) Token: 0x06000830 RID: 2096 RVA: 0x0001B1F0 File Offset: 0x000193F0
		public event PlayFabEvents.PlayFabRequestEvent<LinkTwitchAccountRequest> OnLinkTwitchRequestEvent;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06000831 RID: 2097 RVA: 0x0001B228 File Offset: 0x00019428
		// (remove) Token: 0x06000832 RID: 2098 RVA: 0x0001B260 File Offset: 0x00019460
		public event PlayFabEvents.PlayFabResultEvent<LinkTwitchAccountResult> OnLinkTwitchResultEvent;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x06000833 RID: 2099 RVA: 0x0001B298 File Offset: 0x00019498
		// (remove) Token: 0x06000834 RID: 2100 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public event PlayFabEvents.PlayFabRequestEvent<LinkWindowsHelloAccountRequest> OnLinkWindowsHelloRequestEvent;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x06000835 RID: 2101 RVA: 0x0001B308 File Offset: 0x00019508
		// (remove) Token: 0x06000836 RID: 2102 RVA: 0x0001B340 File Offset: 0x00019540
		public event PlayFabEvents.PlayFabResultEvent<LinkWindowsHelloAccountResponse> OnLinkWindowsHelloResultEvent;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06000837 RID: 2103 RVA: 0x0001B378 File Offset: 0x00019578
		// (remove) Token: 0x06000838 RID: 2104 RVA: 0x0001B3B0 File Offset: 0x000195B0
		public event PlayFabEvents.PlayFabRequestEvent<LinkXboxAccountRequest> OnLinkXboxAccountRequestEvent;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06000839 RID: 2105 RVA: 0x0001B3E8 File Offset: 0x000195E8
		// (remove) Token: 0x0600083A RID: 2106 RVA: 0x0001B420 File Offset: 0x00019620
		public event PlayFabEvents.PlayFabResultEvent<LinkXboxAccountResult> OnLinkXboxAccountResultEvent;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x0600083B RID: 2107 RVA: 0x0001B458 File Offset: 0x00019658
		// (remove) Token: 0x0600083C RID: 2108 RVA: 0x0001B490 File Offset: 0x00019690
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest> OnLoginWithAndroidDeviceIDRequestEvent;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x0600083D RID: 2109 RVA: 0x0001B4C8 File Offset: 0x000196C8
		// (remove) Token: 0x0600083E RID: 2110 RVA: 0x0001B500 File Offset: 0x00019700
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithAppleRequest> OnLoginWithAppleRequestEvent;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x0600083F RID: 2111 RVA: 0x0001B538 File Offset: 0x00019738
		// (remove) Token: 0x06000840 RID: 2112 RVA: 0x0001B570 File Offset: 0x00019770
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithCustomIDRequest> OnLoginWithCustomIDRequestEvent;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06000841 RID: 2113 RVA: 0x0001B5A8 File Offset: 0x000197A8
		// (remove) Token: 0x06000842 RID: 2114 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithEmailAddressRequest> OnLoginWithEmailAddressRequestEvent;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06000843 RID: 2115 RVA: 0x0001B618 File Offset: 0x00019818
		// (remove) Token: 0x06000844 RID: 2116 RVA: 0x0001B650 File Offset: 0x00019850
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookRequest> OnLoginWithFacebookRequestEvent;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x06000845 RID: 2117 RVA: 0x0001B688 File Offset: 0x00019888
		// (remove) Token: 0x06000846 RID: 2118 RVA: 0x0001B6C0 File Offset: 0x000198C0
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest> OnLoginWithFacebookInstantGamesIdRequestEvent;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06000847 RID: 2119 RVA: 0x0001B6F8 File Offset: 0x000198F8
		// (remove) Token: 0x06000848 RID: 2120 RVA: 0x0001B730 File Offset: 0x00019930
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithGameCenterRequest> OnLoginWithGameCenterRequestEvent;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06000849 RID: 2121 RVA: 0x0001B768 File Offset: 0x00019968
		// (remove) Token: 0x0600084A RID: 2122 RVA: 0x0001B7A0 File Offset: 0x000199A0
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithGoogleAccountRequest> OnLoginWithGoogleAccountRequestEvent;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x0600084B RID: 2123 RVA: 0x0001B7D8 File Offset: 0x000199D8
		// (remove) Token: 0x0600084C RID: 2124 RVA: 0x0001B810 File Offset: 0x00019A10
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithIOSDeviceIDRequest> OnLoginWithIOSDeviceIDRequestEvent;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x0600084D RID: 2125 RVA: 0x0001B848 File Offset: 0x00019A48
		// (remove) Token: 0x0600084E RID: 2126 RVA: 0x0001B880 File Offset: 0x00019A80
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithKongregateRequest> OnLoginWithKongregateRequestEvent;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x0600084F RID: 2127 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		// (remove) Token: 0x06000850 RID: 2128 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithNintendoServiceAccountRequest> OnLoginWithNintendoServiceAccountRequestEvent;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06000851 RID: 2129 RVA: 0x0001B928 File Offset: 0x00019B28
		// (remove) Token: 0x06000852 RID: 2130 RVA: 0x0001B960 File Offset: 0x00019B60
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest> OnLoginWithNintendoSwitchDeviceIdRequestEvent;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06000853 RID: 2131 RVA: 0x0001B998 File Offset: 0x00019B98
		// (remove) Token: 0x06000854 RID: 2132 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithOpenIdConnectRequest> OnLoginWithOpenIdConnectRequestEvent;

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06000855 RID: 2133 RVA: 0x0001BA08 File Offset: 0x00019C08
		// (remove) Token: 0x06000856 RID: 2134 RVA: 0x0001BA40 File Offset: 0x00019C40
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithPlayFabRequest> OnLoginWithPlayFabRequestEvent;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06000857 RID: 2135 RVA: 0x0001BA78 File Offset: 0x00019C78
		// (remove) Token: 0x06000858 RID: 2136 RVA: 0x0001BAB0 File Offset: 0x00019CB0
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithPSNRequest> OnLoginWithPSNRequestEvent;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06000859 RID: 2137 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		// (remove) Token: 0x0600085A RID: 2138 RVA: 0x0001BB20 File Offset: 0x00019D20
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithSteamRequest> OnLoginWithSteamRequestEvent;

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x0600085B RID: 2139 RVA: 0x0001BB58 File Offset: 0x00019D58
		// (remove) Token: 0x0600085C RID: 2140 RVA: 0x0001BB90 File Offset: 0x00019D90
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithTwitchRequest> OnLoginWithTwitchRequestEvent;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x0600085D RID: 2141 RVA: 0x0001BBC8 File Offset: 0x00019DC8
		// (remove) Token: 0x0600085E RID: 2142 RVA: 0x0001BC00 File Offset: 0x00019E00
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithWindowsHelloRequest> OnLoginWithWindowsHelloRequestEvent;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x0600085F RID: 2143 RVA: 0x0001BC38 File Offset: 0x00019E38
		// (remove) Token: 0x06000860 RID: 2144 RVA: 0x0001BC70 File Offset: 0x00019E70
		public event PlayFabEvents.PlayFabRequestEvent<LoginWithXboxRequest> OnLoginWithXboxRequestEvent;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06000861 RID: 2145 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		// (remove) Token: 0x06000862 RID: 2146 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public event PlayFabEvents.PlayFabRequestEvent<MatchmakeRequest> OnMatchmakeRequestEvent;

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06000863 RID: 2147 RVA: 0x0001BD18 File Offset: 0x00019F18
		// (remove) Token: 0x06000864 RID: 2148 RVA: 0x0001BD50 File Offset: 0x00019F50
		public event PlayFabEvents.PlayFabResultEvent<MatchmakeResult> OnMatchmakeResultEvent;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x06000865 RID: 2149 RVA: 0x0001BD88 File Offset: 0x00019F88
		// (remove) Token: 0x06000866 RID: 2150 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public event PlayFabEvents.PlayFabRequestEvent<OpenTradeRequest> OnOpenTradeRequestEvent;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x06000867 RID: 2151 RVA: 0x0001BDF8 File Offset: 0x00019FF8
		// (remove) Token: 0x06000868 RID: 2152 RVA: 0x0001BE30 File Offset: 0x0001A030
		public event PlayFabEvents.PlayFabResultEvent<OpenTradeResponse> OnOpenTradeResultEvent;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x06000869 RID: 2153 RVA: 0x0001BE68 File Offset: 0x0001A068
		// (remove) Token: 0x0600086A RID: 2154 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		public event PlayFabEvents.PlayFabRequestEvent<PayForPurchaseRequest> OnPayForPurchaseRequestEvent;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x0600086B RID: 2155 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		// (remove) Token: 0x0600086C RID: 2156 RVA: 0x0001BF10 File Offset: 0x0001A110
		public event PlayFabEvents.PlayFabResultEvent<PayForPurchaseResult> OnPayForPurchaseResultEvent;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x0600086D RID: 2157 RVA: 0x0001BF48 File Offset: 0x0001A148
		// (remove) Token: 0x0600086E RID: 2158 RVA: 0x0001BF80 File Offset: 0x0001A180
		public event PlayFabEvents.PlayFabRequestEvent<PurchaseItemRequest> OnPurchaseItemRequestEvent;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x0600086F RID: 2159 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		// (remove) Token: 0x06000870 RID: 2160 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		public event PlayFabEvents.PlayFabResultEvent<PurchaseItemResult> OnPurchaseItemResultEvent;

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x06000871 RID: 2161 RVA: 0x0001C028 File Offset: 0x0001A228
		// (remove) Token: 0x06000872 RID: 2162 RVA: 0x0001C060 File Offset: 0x0001A260
		public event PlayFabEvents.PlayFabRequestEvent<RedeemCouponRequest> OnRedeemCouponRequestEvent;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x06000873 RID: 2163 RVA: 0x0001C098 File Offset: 0x0001A298
		// (remove) Token: 0x06000874 RID: 2164 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		public event PlayFabEvents.PlayFabResultEvent<RedeemCouponResult> OnRedeemCouponResultEvent;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x0001C108 File Offset: 0x0001A308
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x0001C140 File Offset: 0x0001A340
		public event PlayFabEvents.PlayFabRequestEvent<RefreshPSNAuthTokenRequest> OnRefreshPSNAuthTokenRequestEvent;

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06000877 RID: 2167 RVA: 0x0001C178 File Offset: 0x0001A378
		// (remove) Token: 0x06000878 RID: 2168 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnRefreshPSNAuthTokenResultEvent;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x06000879 RID: 2169 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		// (remove) Token: 0x0600087A RID: 2170 RVA: 0x0001C220 File Offset: 0x0001A420
		public event PlayFabEvents.PlayFabRequestEvent<RegisterForIOSPushNotificationRequest> OnRegisterForIOSPushNotificationRequestEvent;

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x0600087B RID: 2171 RVA: 0x0001C258 File Offset: 0x0001A458
		// (remove) Token: 0x0600087C RID: 2172 RVA: 0x0001C290 File Offset: 0x0001A490
		public event PlayFabEvents.PlayFabResultEvent<RegisterForIOSPushNotificationResult> OnRegisterForIOSPushNotificationResultEvent;

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x0600087D RID: 2173 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		// (remove) Token: 0x0600087E RID: 2174 RVA: 0x0001C300 File Offset: 0x0001A500
		public event PlayFabEvents.PlayFabRequestEvent<RegisterPlayFabUserRequest> OnRegisterPlayFabUserRequestEvent;

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x0600087F RID: 2175 RVA: 0x0001C338 File Offset: 0x0001A538
		// (remove) Token: 0x06000880 RID: 2176 RVA: 0x0001C370 File Offset: 0x0001A570
		public event PlayFabEvents.PlayFabResultEvent<RegisterPlayFabUserResult> OnRegisterPlayFabUserResultEvent;

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x06000881 RID: 2177 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		// (remove) Token: 0x06000882 RID: 2178 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		public event PlayFabEvents.PlayFabRequestEvent<RegisterWithWindowsHelloRequest> OnRegisterWithWindowsHelloRequestEvent;

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x06000883 RID: 2179 RVA: 0x0001C418 File Offset: 0x0001A618
		// (remove) Token: 0x06000884 RID: 2180 RVA: 0x0001C450 File Offset: 0x0001A650
		public event PlayFabEvents.PlayFabRequestEvent<RemoveContactEmailRequest> OnRemoveContactEmailRequestEvent;

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x06000885 RID: 2181 RVA: 0x0001C488 File Offset: 0x0001A688
		// (remove) Token: 0x06000886 RID: 2182 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
		public event PlayFabEvents.PlayFabResultEvent<RemoveContactEmailResult> OnRemoveContactEmailResultEvent;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x06000887 RID: 2183 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		// (remove) Token: 0x06000888 RID: 2184 RVA: 0x0001C530 File Offset: 0x0001A730
		public event PlayFabEvents.PlayFabRequestEvent<RemoveFriendRequest> OnRemoveFriendRequestEvent;

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06000889 RID: 2185 RVA: 0x0001C568 File Offset: 0x0001A768
		// (remove) Token: 0x0600088A RID: 2186 RVA: 0x0001C5A0 File Offset: 0x0001A7A0
		public event PlayFabEvents.PlayFabResultEvent<RemoveFriendResult> OnRemoveFriendResultEvent;

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x0600088B RID: 2187 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		// (remove) Token: 0x0600088C RID: 2188 RVA: 0x0001C610 File Offset: 0x0001A810
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGenericIDRequest> OnRemoveGenericIDRequestEvent;

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x0600088D RID: 2189 RVA: 0x0001C648 File Offset: 0x0001A848
		// (remove) Token: 0x0600088E RID: 2190 RVA: 0x0001C680 File Offset: 0x0001A880
		public event PlayFabEvents.PlayFabResultEvent<RemoveGenericIDResult> OnRemoveGenericIDResultEvent;

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x0600088F RID: 2191 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		// (remove) Token: 0x06000890 RID: 2192 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		public event PlayFabEvents.PlayFabRequestEvent<RemoveSharedGroupMembersRequest> OnRemoveSharedGroupMembersRequestEvent;

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06000891 RID: 2193 RVA: 0x0001C728 File Offset: 0x0001A928
		// (remove) Token: 0x06000892 RID: 2194 RVA: 0x0001C760 File Offset: 0x0001A960
		public event PlayFabEvents.PlayFabResultEvent<RemoveSharedGroupMembersResult> OnRemoveSharedGroupMembersResultEvent;

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06000893 RID: 2195 RVA: 0x0001C798 File Offset: 0x0001A998
		// (remove) Token: 0x06000894 RID: 2196 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
		public event PlayFabEvents.PlayFabRequestEvent<ReportAdActivityRequest> OnReportAdActivityRequestEvent;

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06000895 RID: 2197 RVA: 0x0001C808 File Offset: 0x0001AA08
		// (remove) Token: 0x06000896 RID: 2198 RVA: 0x0001C840 File Offset: 0x0001AA40
		public event PlayFabEvents.PlayFabResultEvent<ReportAdActivityResult> OnReportAdActivityResultEvent;

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06000897 RID: 2199 RVA: 0x0001C878 File Offset: 0x0001AA78
		// (remove) Token: 0x06000898 RID: 2200 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		public event PlayFabEvents.PlayFabRequestEvent<DeviceInfoRequest> OnReportDeviceInfoRequestEvent;

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06000899 RID: 2201 RVA: 0x0001C8E8 File Offset: 0x0001AAE8
		// (remove) Token: 0x0600089A RID: 2202 RVA: 0x0001C920 File Offset: 0x0001AB20
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnReportDeviceInfoResultEvent;

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x0600089B RID: 2203 RVA: 0x0001C958 File Offset: 0x0001AB58
		// (remove) Token: 0x0600089C RID: 2204 RVA: 0x0001C990 File Offset: 0x0001AB90
		public event PlayFabEvents.PlayFabRequestEvent<ReportPlayerClientRequest> OnReportPlayerRequestEvent;

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x0600089D RID: 2205 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		// (remove) Token: 0x0600089E RID: 2206 RVA: 0x0001CA00 File Offset: 0x0001AC00
		public event PlayFabEvents.PlayFabResultEvent<ReportPlayerClientResult> OnReportPlayerResultEvent;

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x0600089F RID: 2207 RVA: 0x0001CA38 File Offset: 0x0001AC38
		// (remove) Token: 0x060008A0 RID: 2208 RVA: 0x0001CA70 File Offset: 0x0001AC70
		public event PlayFabEvents.PlayFabRequestEvent<RestoreIOSPurchasesRequest> OnRestoreIOSPurchasesRequestEvent;

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x060008A1 RID: 2209 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		// (remove) Token: 0x060008A2 RID: 2210 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		public event PlayFabEvents.PlayFabResultEvent<RestoreIOSPurchasesResult> OnRestoreIOSPurchasesResultEvent;

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x060008A3 RID: 2211 RVA: 0x0001CB18 File Offset: 0x0001AD18
		// (remove) Token: 0x060008A4 RID: 2212 RVA: 0x0001CB50 File Offset: 0x0001AD50
		public event PlayFabEvents.PlayFabRequestEvent<RewardAdActivityRequest> OnRewardAdActivityRequestEvent;

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x060008A5 RID: 2213 RVA: 0x0001CB88 File Offset: 0x0001AD88
		// (remove) Token: 0x060008A6 RID: 2214 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public event PlayFabEvents.PlayFabResultEvent<RewardAdActivityResult> OnRewardAdActivityResultEvent;

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x060008A7 RID: 2215 RVA: 0x0001CBF8 File Offset: 0x0001ADF8
		// (remove) Token: 0x060008A8 RID: 2216 RVA: 0x0001CC30 File Offset: 0x0001AE30
		public event PlayFabEvents.PlayFabRequestEvent<SendAccountRecoveryEmailRequest> OnSendAccountRecoveryEmailRequestEvent;

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x060008A9 RID: 2217 RVA: 0x0001CC68 File Offset: 0x0001AE68
		// (remove) Token: 0x060008AA RID: 2218 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		public event PlayFabEvents.PlayFabResultEvent<SendAccountRecoveryEmailResult> OnSendAccountRecoveryEmailResultEvent;

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x060008AB RID: 2219 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		// (remove) Token: 0x060008AC RID: 2220 RVA: 0x0001CD10 File Offset: 0x0001AF10
		public event PlayFabEvents.PlayFabRequestEvent<SetFriendTagsRequest> OnSetFriendTagsRequestEvent;

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x060008AD RID: 2221 RVA: 0x0001CD48 File Offset: 0x0001AF48
		// (remove) Token: 0x060008AE RID: 2222 RVA: 0x0001CD80 File Offset: 0x0001AF80
		public event PlayFabEvents.PlayFabResultEvent<SetFriendTagsResult> OnSetFriendTagsResultEvent;

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x060008AF RID: 2223 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		// (remove) Token: 0x060008B0 RID: 2224 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		public event PlayFabEvents.PlayFabRequestEvent<SetPlayerSecretRequest> OnSetPlayerSecretRequestEvent;

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x060008B1 RID: 2225 RVA: 0x0001CE28 File Offset: 0x0001B028
		// (remove) Token: 0x060008B2 RID: 2226 RVA: 0x0001CE60 File Offset: 0x0001B060
		public event PlayFabEvents.PlayFabResultEvent<SetPlayerSecretResult> OnSetPlayerSecretResultEvent;

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x060008B3 RID: 2227 RVA: 0x0001CE98 File Offset: 0x0001B098
		// (remove) Token: 0x060008B4 RID: 2228 RVA: 0x0001CED0 File Offset: 0x0001B0D0
		public event PlayFabEvents.PlayFabRequestEvent<StartGameRequest> OnStartGameRequestEvent;

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x060008B5 RID: 2229 RVA: 0x0001CF08 File Offset: 0x0001B108
		// (remove) Token: 0x060008B6 RID: 2230 RVA: 0x0001CF40 File Offset: 0x0001B140
		public event PlayFabEvents.PlayFabResultEvent<StartGameResult> OnStartGameResultEvent;

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x060008B7 RID: 2231 RVA: 0x0001CF78 File Offset: 0x0001B178
		// (remove) Token: 0x060008B8 RID: 2232 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		public event PlayFabEvents.PlayFabRequestEvent<StartPurchaseRequest> OnStartPurchaseRequestEvent;

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x060008B9 RID: 2233 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		// (remove) Token: 0x060008BA RID: 2234 RVA: 0x0001D020 File Offset: 0x0001B220
		public event PlayFabEvents.PlayFabResultEvent<StartPurchaseResult> OnStartPurchaseResultEvent;

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x060008BB RID: 2235 RVA: 0x0001D058 File Offset: 0x0001B258
		// (remove) Token: 0x060008BC RID: 2236 RVA: 0x0001D090 File Offset: 0x0001B290
		public event PlayFabEvents.PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest> OnSubtractUserVirtualCurrencyRequestEvent;

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x060008BD RID: 2237 RVA: 0x0001D0C8 File Offset: 0x0001B2C8
		// (remove) Token: 0x060008BE RID: 2238 RVA: 0x0001D100 File Offset: 0x0001B300
		public event PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult> OnSubtractUserVirtualCurrencyResultEvent;

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x060008BF RID: 2239 RVA: 0x0001D138 File Offset: 0x0001B338
		// (remove) Token: 0x060008C0 RID: 2240 RVA: 0x0001D170 File Offset: 0x0001B370
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest> OnUnlinkAndroidDeviceIDRequestEvent;

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x060008C1 RID: 2241 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		// (remove) Token: 0x060008C2 RID: 2242 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
		public event PlayFabEvents.PlayFabResultEvent<UnlinkAndroidDeviceIDResult> OnUnlinkAndroidDeviceIDResultEvent;

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x060008C3 RID: 2243 RVA: 0x0001D218 File Offset: 0x0001B418
		// (remove) Token: 0x060008C4 RID: 2244 RVA: 0x0001D250 File Offset: 0x0001B450
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkAppleRequest> OnUnlinkAppleRequestEvent;

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x060008C5 RID: 2245 RVA: 0x0001D288 File Offset: 0x0001B488
		// (remove) Token: 0x060008C6 RID: 2246 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkAppleResultEvent;

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x060008C7 RID: 2247 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		// (remove) Token: 0x060008C8 RID: 2248 RVA: 0x0001D330 File Offset: 0x0001B530
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkCustomIDRequest> OnUnlinkCustomIDRequestEvent;

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x060008C9 RID: 2249 RVA: 0x0001D368 File Offset: 0x0001B568
		// (remove) Token: 0x060008CA RID: 2250 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		public event PlayFabEvents.PlayFabResultEvent<UnlinkCustomIDResult> OnUnlinkCustomIDResultEvent;

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x060008CB RID: 2251 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		// (remove) Token: 0x060008CC RID: 2252 RVA: 0x0001D410 File Offset: 0x0001B610
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookAccountRequest> OnUnlinkFacebookAccountRequestEvent;

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x060008CD RID: 2253 RVA: 0x0001D448 File Offset: 0x0001B648
		// (remove) Token: 0x060008CE RID: 2254 RVA: 0x0001D480 File Offset: 0x0001B680
		public event PlayFabEvents.PlayFabResultEvent<UnlinkFacebookAccountResult> OnUnlinkFacebookAccountResultEvent;

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x060008CF RID: 2255 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		// (remove) Token: 0x060008D0 RID: 2256 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest> OnUnlinkFacebookInstantGamesIdRequestEvent;

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x060008D1 RID: 2257 RVA: 0x0001D528 File Offset: 0x0001B728
		// (remove) Token: 0x060008D2 RID: 2258 RVA: 0x0001D560 File Offset: 0x0001B760
		public event PlayFabEvents.PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult> OnUnlinkFacebookInstantGamesIdResultEvent;

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x060008D3 RID: 2259 RVA: 0x0001D598 File Offset: 0x0001B798
		// (remove) Token: 0x060008D4 RID: 2260 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkGameCenterAccountRequest> OnUnlinkGameCenterAccountRequestEvent;

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x060008D5 RID: 2261 RVA: 0x0001D608 File Offset: 0x0001B808
		// (remove) Token: 0x060008D6 RID: 2262 RVA: 0x0001D640 File Offset: 0x0001B840
		public event PlayFabEvents.PlayFabResultEvent<UnlinkGameCenterAccountResult> OnUnlinkGameCenterAccountResultEvent;

		// Token: 0x14000106 RID: 262
		// (add) Token: 0x060008D7 RID: 2263 RVA: 0x0001D678 File Offset: 0x0001B878
		// (remove) Token: 0x060008D8 RID: 2264 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkGoogleAccountRequest> OnUnlinkGoogleAccountRequestEvent;

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x060008D9 RID: 2265 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		// (remove) Token: 0x060008DA RID: 2266 RVA: 0x0001D720 File Offset: 0x0001B920
		public event PlayFabEvents.PlayFabResultEvent<UnlinkGoogleAccountResult> OnUnlinkGoogleAccountResultEvent;

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x060008DB RID: 2267 RVA: 0x0001D758 File Offset: 0x0001B958
		// (remove) Token: 0x060008DC RID: 2268 RVA: 0x0001D790 File Offset: 0x0001B990
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkIOSDeviceIDRequest> OnUnlinkIOSDeviceIDRequestEvent;

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x060008DD RID: 2269 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		// (remove) Token: 0x060008DE RID: 2270 RVA: 0x0001D800 File Offset: 0x0001BA00
		public event PlayFabEvents.PlayFabResultEvent<UnlinkIOSDeviceIDResult> OnUnlinkIOSDeviceIDResultEvent;

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x060008DF RID: 2271 RVA: 0x0001D838 File Offset: 0x0001BA38
		// (remove) Token: 0x060008E0 RID: 2272 RVA: 0x0001D870 File Offset: 0x0001BA70
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkKongregateAccountRequest> OnUnlinkKongregateRequestEvent;

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x060008E1 RID: 2273 RVA: 0x0001D8A8 File Offset: 0x0001BAA8
		// (remove) Token: 0x060008E2 RID: 2274 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		public event PlayFabEvents.PlayFabResultEvent<UnlinkKongregateAccountResult> OnUnlinkKongregateResultEvent;

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x060008E3 RID: 2275 RVA: 0x0001D918 File Offset: 0x0001BB18
		// (remove) Token: 0x060008E4 RID: 2276 RVA: 0x0001D950 File Offset: 0x0001BB50
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkNintendoServiceAccountRequest> OnUnlinkNintendoServiceAccountRequestEvent;

		// Token: 0x1400010D RID: 269
		// (add) Token: 0x060008E5 RID: 2277 RVA: 0x0001D988 File Offset: 0x0001BB88
		// (remove) Token: 0x060008E6 RID: 2278 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkNintendoServiceAccountResultEvent;

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x060008E7 RID: 2279 RVA: 0x0001D9F8 File Offset: 0x0001BBF8
		// (remove) Token: 0x060008E8 RID: 2280 RVA: 0x0001DA30 File Offset: 0x0001BC30
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest> OnUnlinkNintendoSwitchDeviceIdRequestEvent;

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x060008E9 RID: 2281 RVA: 0x0001DA68 File Offset: 0x0001BC68
		// (remove) Token: 0x060008EA RID: 2282 RVA: 0x0001DAA0 File Offset: 0x0001BCA0
		public event PlayFabEvents.PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult> OnUnlinkNintendoSwitchDeviceIdResultEvent;

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x060008EB RID: 2283 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
		// (remove) Token: 0x060008EC RID: 2284 RVA: 0x0001DB10 File Offset: 0x0001BD10
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkOpenIdConnectRequest> OnUnlinkOpenIdConnectRequestEvent;

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x060008ED RID: 2285 RVA: 0x0001DB48 File Offset: 0x0001BD48
		// (remove) Token: 0x060008EE RID: 2286 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUnlinkOpenIdConnectResultEvent;

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x060008EF RID: 2287 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		// (remove) Token: 0x060008F0 RID: 2288 RVA: 0x0001DBF0 File Offset: 0x0001BDF0
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkPSNAccountRequest> OnUnlinkPSNAccountRequestEvent;

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x060008F1 RID: 2289 RVA: 0x0001DC28 File Offset: 0x0001BE28
		// (remove) Token: 0x060008F2 RID: 2290 RVA: 0x0001DC60 File Offset: 0x0001BE60
		public event PlayFabEvents.PlayFabResultEvent<UnlinkPSNAccountResult> OnUnlinkPSNAccountResultEvent;

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x060008F3 RID: 2291 RVA: 0x0001DC98 File Offset: 0x0001BE98
		// (remove) Token: 0x060008F4 RID: 2292 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkSteamAccountRequest> OnUnlinkSteamAccountRequestEvent;

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x060008F5 RID: 2293 RVA: 0x0001DD08 File Offset: 0x0001BF08
		// (remove) Token: 0x060008F6 RID: 2294 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public event PlayFabEvents.PlayFabResultEvent<UnlinkSteamAccountResult> OnUnlinkSteamAccountResultEvent;

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x060008F7 RID: 2295 RVA: 0x0001DD78 File Offset: 0x0001BF78
		// (remove) Token: 0x060008F8 RID: 2296 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkTwitchAccountRequest> OnUnlinkTwitchRequestEvent;

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x060008F9 RID: 2297 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
		// (remove) Token: 0x060008FA RID: 2298 RVA: 0x0001DE20 File Offset: 0x0001C020
		public event PlayFabEvents.PlayFabResultEvent<UnlinkTwitchAccountResult> OnUnlinkTwitchResultEvent;

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x060008FB RID: 2299 RVA: 0x0001DE58 File Offset: 0x0001C058
		// (remove) Token: 0x060008FC RID: 2300 RVA: 0x0001DE90 File Offset: 0x0001C090
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest> OnUnlinkWindowsHelloRequestEvent;

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x060008FD RID: 2301 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		// (remove) Token: 0x060008FE RID: 2302 RVA: 0x0001DF00 File Offset: 0x0001C100
		public event PlayFabEvents.PlayFabResultEvent<UnlinkWindowsHelloAccountResponse> OnUnlinkWindowsHelloResultEvent;

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x060008FF RID: 2303 RVA: 0x0001DF38 File Offset: 0x0001C138
		// (remove) Token: 0x06000900 RID: 2304 RVA: 0x0001DF70 File Offset: 0x0001C170
		public event PlayFabEvents.PlayFabRequestEvent<UnlinkXboxAccountRequest> OnUnlinkXboxAccountRequestEvent;

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x06000901 RID: 2305 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
		// (remove) Token: 0x06000902 RID: 2306 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
		public event PlayFabEvents.PlayFabResultEvent<UnlinkXboxAccountResult> OnUnlinkXboxAccountResultEvent;

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x06000903 RID: 2307 RVA: 0x0001E018 File Offset: 0x0001C218
		// (remove) Token: 0x06000904 RID: 2308 RVA: 0x0001E050 File Offset: 0x0001C250
		public event PlayFabEvents.PlayFabRequestEvent<UnlockContainerInstanceRequest> OnUnlockContainerInstanceRequestEvent;

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x06000905 RID: 2309 RVA: 0x0001E088 File Offset: 0x0001C288
		// (remove) Token: 0x06000906 RID: 2310 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		public event PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult> OnUnlockContainerInstanceResultEvent;

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x06000907 RID: 2311 RVA: 0x0001E0F8 File Offset: 0x0001C2F8
		// (remove) Token: 0x06000908 RID: 2312 RVA: 0x0001E130 File Offset: 0x0001C330
		public event PlayFabEvents.PlayFabRequestEvent<UnlockContainerItemRequest> OnUnlockContainerItemRequestEvent;

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x06000909 RID: 2313 RVA: 0x0001E168 File Offset: 0x0001C368
		// (remove) Token: 0x0600090A RID: 2314 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public event PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult> OnUnlockContainerItemResultEvent;

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x0600090B RID: 2315 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		// (remove) Token: 0x0600090C RID: 2316 RVA: 0x0001E210 File Offset: 0x0001C410
		public event PlayFabEvents.PlayFabRequestEvent<UpdateAvatarUrlRequest> OnUpdateAvatarUrlRequestEvent;

		// Token: 0x14000121 RID: 289
		// (add) Token: 0x0600090D RID: 2317 RVA: 0x0001E248 File Offset: 0x0001C448
		// (remove) Token: 0x0600090E RID: 2318 RVA: 0x0001E280 File Offset: 0x0001C480
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse> OnUpdateAvatarUrlResultEvent;

		// Token: 0x14000122 RID: 290
		// (add) Token: 0x0600090F RID: 2319 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		// (remove) Token: 0x06000910 RID: 2320 RVA: 0x0001E2F0 File Offset: 0x0001C4F0
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCharacterDataRequest> OnUpdateCharacterDataRequestEvent;

		// Token: 0x14000123 RID: 291
		// (add) Token: 0x06000911 RID: 2321 RVA: 0x0001E328 File Offset: 0x0001C528
		// (remove) Token: 0x06000912 RID: 2322 RVA: 0x0001E360 File Offset: 0x0001C560
		public event PlayFabEvents.PlayFabResultEvent<UpdateCharacterDataResult> OnUpdateCharacterDataResultEvent;

		// Token: 0x14000124 RID: 292
		// (add) Token: 0x06000913 RID: 2323 RVA: 0x0001E398 File Offset: 0x0001C598
		// (remove) Token: 0x06000914 RID: 2324 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
		public event PlayFabEvents.PlayFabRequestEvent<UpdateCharacterStatisticsRequest> OnUpdateCharacterStatisticsRequestEvent;

		// Token: 0x14000125 RID: 293
		// (add) Token: 0x06000915 RID: 2325 RVA: 0x0001E408 File Offset: 0x0001C608
		// (remove) Token: 0x06000916 RID: 2326 RVA: 0x0001E440 File Offset: 0x0001C640
		public event PlayFabEvents.PlayFabResultEvent<UpdateCharacterStatisticsResult> OnUpdateCharacterStatisticsResultEvent;

		// Token: 0x14000126 RID: 294
		// (add) Token: 0x06000917 RID: 2327 RVA: 0x0001E478 File Offset: 0x0001C678
		// (remove) Token: 0x06000918 RID: 2328 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public event PlayFabEvents.PlayFabRequestEvent<UpdatePlayerStatisticsRequest> OnUpdatePlayerStatisticsRequestEvent;

		// Token: 0x14000127 RID: 295
		// (add) Token: 0x06000919 RID: 2329 RVA: 0x0001E4E8 File Offset: 0x0001C6E8
		// (remove) Token: 0x0600091A RID: 2330 RVA: 0x0001E520 File Offset: 0x0001C720
		public event PlayFabEvents.PlayFabResultEvent<UpdatePlayerStatisticsResult> OnUpdatePlayerStatisticsResultEvent;

		// Token: 0x14000128 RID: 296
		// (add) Token: 0x0600091B RID: 2331 RVA: 0x0001E558 File Offset: 0x0001C758
		// (remove) Token: 0x0600091C RID: 2332 RVA: 0x0001E590 File Offset: 0x0001C790
		public event PlayFabEvents.PlayFabRequestEvent<UpdateSharedGroupDataRequest> OnUpdateSharedGroupDataRequestEvent;

		// Token: 0x14000129 RID: 297
		// (add) Token: 0x0600091D RID: 2333 RVA: 0x0001E5C8 File Offset: 0x0001C7C8
		// (remove) Token: 0x0600091E RID: 2334 RVA: 0x0001E600 File Offset: 0x0001C800
		public event PlayFabEvents.PlayFabResultEvent<UpdateSharedGroupDataResult> OnUpdateSharedGroupDataResultEvent;

		// Token: 0x1400012A RID: 298
		// (add) Token: 0x0600091F RID: 2335 RVA: 0x0001E638 File Offset: 0x0001C838
		// (remove) Token: 0x06000920 RID: 2336 RVA: 0x0001E670 File Offset: 0x0001C870
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest> OnUpdateUserDataRequestEvent;

		// Token: 0x1400012B RID: 299
		// (add) Token: 0x06000921 RID: 2337 RVA: 0x0001E6A8 File Offset: 0x0001C8A8
		// (remove) Token: 0x06000922 RID: 2338 RVA: 0x0001E6E0 File Offset: 0x0001C8E0
		public event PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult> OnUpdateUserDataResultEvent;

		// Token: 0x1400012C RID: 300
		// (add) Token: 0x06000923 RID: 2339 RVA: 0x0001E718 File Offset: 0x0001C918
		// (remove) Token: 0x06000924 RID: 2340 RVA: 0x0001E750 File Offset: 0x0001C950
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest> OnUpdateUserPublisherDataRequestEvent;

		// Token: 0x1400012D RID: 301
		// (add) Token: 0x06000925 RID: 2341 RVA: 0x0001E788 File Offset: 0x0001C988
		// (remove) Token: 0x06000926 RID: 2342 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		public event PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult> OnUpdateUserPublisherDataResultEvent;

		// Token: 0x1400012E RID: 302
		// (add) Token: 0x06000927 RID: 2343 RVA: 0x0001E7F8 File Offset: 0x0001C9F8
		// (remove) Token: 0x06000928 RID: 2344 RVA: 0x0001E830 File Offset: 0x0001CA30
		public event PlayFabEvents.PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest> OnUpdateUserTitleDisplayNameRequestEvent;

		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06000929 RID: 2345 RVA: 0x0001E868 File Offset: 0x0001CA68
		// (remove) Token: 0x0600092A RID: 2346 RVA: 0x0001E8A0 File Offset: 0x0001CAA0
		public event PlayFabEvents.PlayFabResultEvent<UpdateUserTitleDisplayNameResult> OnUpdateUserTitleDisplayNameResultEvent;

		// Token: 0x14000130 RID: 304
		// (add) Token: 0x0600092B RID: 2347 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
		// (remove) Token: 0x0600092C RID: 2348 RVA: 0x0001E910 File Offset: 0x0001CB10
		public event PlayFabEvents.PlayFabRequestEvent<ValidateAmazonReceiptRequest> OnValidateAmazonIAPReceiptRequestEvent;

		// Token: 0x14000131 RID: 305
		// (add) Token: 0x0600092D RID: 2349 RVA: 0x0001E948 File Offset: 0x0001CB48
		// (remove) Token: 0x0600092E RID: 2350 RVA: 0x0001E980 File Offset: 0x0001CB80
		public event PlayFabEvents.PlayFabResultEvent<ValidateAmazonReceiptResult> OnValidateAmazonIAPReceiptResultEvent;

		// Token: 0x14000132 RID: 306
		// (add) Token: 0x0600092F RID: 2351 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		// (remove) Token: 0x06000930 RID: 2352 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		public event PlayFabEvents.PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest> OnValidateGooglePlayPurchaseRequestEvent;

		// Token: 0x14000133 RID: 307
		// (add) Token: 0x06000931 RID: 2353 RVA: 0x0001EA28 File Offset: 0x0001CC28
		// (remove) Token: 0x06000932 RID: 2354 RVA: 0x0001EA60 File Offset: 0x0001CC60
		public event PlayFabEvents.PlayFabResultEvent<ValidateGooglePlayPurchaseResult> OnValidateGooglePlayPurchaseResultEvent;

		// Token: 0x14000134 RID: 308
		// (add) Token: 0x06000933 RID: 2355 RVA: 0x0001EA98 File Offset: 0x0001CC98
		// (remove) Token: 0x06000934 RID: 2356 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
		public event PlayFabEvents.PlayFabRequestEvent<ValidateIOSReceiptRequest> OnValidateIOSReceiptRequestEvent;

		// Token: 0x14000135 RID: 309
		// (add) Token: 0x06000935 RID: 2357 RVA: 0x0001EB08 File Offset: 0x0001CD08
		// (remove) Token: 0x06000936 RID: 2358 RVA: 0x0001EB40 File Offset: 0x0001CD40
		public event PlayFabEvents.PlayFabResultEvent<ValidateIOSReceiptResult> OnValidateIOSReceiptResultEvent;

		// Token: 0x14000136 RID: 310
		// (add) Token: 0x06000937 RID: 2359 RVA: 0x0001EB78 File Offset: 0x0001CD78
		// (remove) Token: 0x06000938 RID: 2360 RVA: 0x0001EBB0 File Offset: 0x0001CDB0
		public event PlayFabEvents.PlayFabRequestEvent<ValidateWindowsReceiptRequest> OnValidateWindowsStoreReceiptRequestEvent;

		// Token: 0x14000137 RID: 311
		// (add) Token: 0x06000939 RID: 2361 RVA: 0x0001EBE8 File Offset: 0x0001CDE8
		// (remove) Token: 0x0600093A RID: 2362 RVA: 0x0001EC20 File Offset: 0x0001CE20
		public event PlayFabEvents.PlayFabResultEvent<ValidateWindowsReceiptResult> OnValidateWindowsStoreReceiptResultEvent;

		// Token: 0x14000138 RID: 312
		// (add) Token: 0x0600093B RID: 2363 RVA: 0x0001EC58 File Offset: 0x0001CE58
		// (remove) Token: 0x0600093C RID: 2364 RVA: 0x0001EC90 File Offset: 0x0001CE90
		public event PlayFabEvents.PlayFabRequestEvent<WriteClientCharacterEventRequest> OnWriteCharacterEventRequestEvent;

		// Token: 0x14000139 RID: 313
		// (add) Token: 0x0600093D RID: 2365 RVA: 0x0001ECC8 File Offset: 0x0001CEC8
		// (remove) Token: 0x0600093E RID: 2366 RVA: 0x0001ED00 File Offset: 0x0001CF00
		public event PlayFabEvents.PlayFabResultEvent<WriteEventResponse> OnWriteCharacterEventResultEvent;

		// Token: 0x1400013A RID: 314
		// (add) Token: 0x0600093F RID: 2367 RVA: 0x0001ED38 File Offset: 0x0001CF38
		// (remove) Token: 0x06000940 RID: 2368 RVA: 0x0001ED70 File Offset: 0x0001CF70
		public event PlayFabEvents.PlayFabRequestEvent<WriteClientPlayerEventRequest> OnWritePlayerEventRequestEvent;

		// Token: 0x1400013B RID: 315
		// (add) Token: 0x06000941 RID: 2369 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		// (remove) Token: 0x06000942 RID: 2370 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public event PlayFabEvents.PlayFabResultEvent<WriteEventResponse> OnWritePlayerEventResultEvent;

		// Token: 0x1400013C RID: 316
		// (add) Token: 0x06000943 RID: 2371 RVA: 0x0001EE18 File Offset: 0x0001D018
		// (remove) Token: 0x06000944 RID: 2372 RVA: 0x0001EE50 File Offset: 0x0001D050
		public event PlayFabEvents.PlayFabRequestEvent<WriteTitleEventRequest> OnWriteTitleEventRequestEvent;

		// Token: 0x1400013D RID: 317
		// (add) Token: 0x06000945 RID: 2373 RVA: 0x0001EE88 File Offset: 0x0001D088
		// (remove) Token: 0x06000946 RID: 2374 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		public event PlayFabEvents.PlayFabResultEvent<WriteEventResponse> OnWriteTitleEventResultEvent;

		// Token: 0x1400013E RID: 318
		// (add) Token: 0x06000947 RID: 2375 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		// (remove) Token: 0x06000948 RID: 2376 RVA: 0x0001EF30 File Offset: 0x0001D130
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteEntityCloudScriptRequest> OnCloudScriptExecuteEntityCloudScriptRequestEvent;

		// Token: 0x1400013F RID: 319
		// (add) Token: 0x06000949 RID: 2377 RVA: 0x0001EF68 File Offset: 0x0001D168
		// (remove) Token: 0x0600094A RID: 2378 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult> OnCloudScriptExecuteEntityCloudScriptResultEvent;

		// Token: 0x14000140 RID: 320
		// (add) Token: 0x0600094B RID: 2379 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		// (remove) Token: 0x0600094C RID: 2380 RVA: 0x0001F010 File Offset: 0x0001D210
		public event PlayFabEvents.PlayFabRequestEvent<ExecuteFunctionRequest> OnCloudScriptExecuteFunctionRequestEvent;

		// Token: 0x14000141 RID: 321
		// (add) Token: 0x0600094D RID: 2381 RVA: 0x0001F048 File Offset: 0x0001D248
		// (remove) Token: 0x0600094E RID: 2382 RVA: 0x0001F080 File Offset: 0x0001D280
		public event PlayFabEvents.PlayFabResultEvent<ExecuteFunctionResult> OnCloudScriptExecuteFunctionResultEvent;

		// Token: 0x14000142 RID: 322
		// (add) Token: 0x0600094F RID: 2383 RVA: 0x0001F0B8 File Offset: 0x0001D2B8
		// (remove) Token: 0x06000950 RID: 2384 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public event PlayFabEvents.PlayFabRequestEvent<ListFunctionsRequest> OnCloudScriptListFunctionsRequestEvent;

		// Token: 0x14000143 RID: 323
		// (add) Token: 0x06000951 RID: 2385 RVA: 0x0001F128 File Offset: 0x0001D328
		// (remove) Token: 0x06000952 RID: 2386 RVA: 0x0001F160 File Offset: 0x0001D360
		public event PlayFabEvents.PlayFabResultEvent<ListFunctionsResult> OnCloudScriptListFunctionsResultEvent;

		// Token: 0x14000144 RID: 324
		// (add) Token: 0x06000953 RID: 2387 RVA: 0x0001F198 File Offset: 0x0001D398
		// (remove) Token: 0x06000954 RID: 2388 RVA: 0x0001F1D0 File Offset: 0x0001D3D0
		public event PlayFabEvents.PlayFabRequestEvent<ListFunctionsRequest> OnCloudScriptListHttpFunctionsRequestEvent;

		// Token: 0x14000145 RID: 325
		// (add) Token: 0x06000955 RID: 2389 RVA: 0x0001F208 File Offset: 0x0001D408
		// (remove) Token: 0x06000956 RID: 2390 RVA: 0x0001F240 File Offset: 0x0001D440
		public event PlayFabEvents.PlayFabResultEvent<ListHttpFunctionsResult> OnCloudScriptListHttpFunctionsResultEvent;

		// Token: 0x14000146 RID: 326
		// (add) Token: 0x06000957 RID: 2391 RVA: 0x0001F278 File Offset: 0x0001D478
		// (remove) Token: 0x06000958 RID: 2392 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		public event PlayFabEvents.PlayFabRequestEvent<ListFunctionsRequest> OnCloudScriptListQueuedFunctionsRequestEvent;

		// Token: 0x14000147 RID: 327
		// (add) Token: 0x06000959 RID: 2393 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		// (remove) Token: 0x0600095A RID: 2394 RVA: 0x0001F320 File Offset: 0x0001D520
		public event PlayFabEvents.PlayFabResultEvent<ListQueuedFunctionsResult> OnCloudScriptListQueuedFunctionsResultEvent;

		// Token: 0x14000148 RID: 328
		// (add) Token: 0x0600095B RID: 2395 RVA: 0x0001F358 File Offset: 0x0001D558
		// (remove) Token: 0x0600095C RID: 2396 RVA: 0x0001F390 File Offset: 0x0001D590
		public event PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForEntityTriggeredActionRequest> OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent;

		// Token: 0x14000149 RID: 329
		// (add) Token: 0x0600095D RID: 2397 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		// (remove) Token: 0x0600095E RID: 2398 RVA: 0x0001F400 File Offset: 0x0001D600
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent;

		// Token: 0x1400014A RID: 330
		// (add) Token: 0x0600095F RID: 2399 RVA: 0x0001F438 File Offset: 0x0001D638
		// (remove) Token: 0x06000960 RID: 2400 RVA: 0x0001F470 File Offset: 0x0001D670
		public event PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForFunctionExecutionRequest> OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent;

		// Token: 0x1400014B RID: 331
		// (add) Token: 0x06000961 RID: 2401 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		// (remove) Token: 0x06000962 RID: 2402 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent;

		// Token: 0x1400014C RID: 332
		// (add) Token: 0x06000963 RID: 2403 RVA: 0x0001F518 File Offset: 0x0001D718
		// (remove) Token: 0x06000964 RID: 2404 RVA: 0x0001F550 File Offset: 0x0001D750
		public event PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForPlayerTriggeredActionRequest> OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent;

		// Token: 0x1400014D RID: 333
		// (add) Token: 0x06000965 RID: 2405 RVA: 0x0001F588 File Offset: 0x0001D788
		// (remove) Token: 0x06000966 RID: 2406 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent;

		// Token: 0x1400014E RID: 334
		// (add) Token: 0x06000967 RID: 2407 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		// (remove) Token: 0x06000968 RID: 2408 RVA: 0x0001F630 File Offset: 0x0001D830
		public event PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForScheduledTaskRequest> OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent;

		// Token: 0x1400014F RID: 335
		// (add) Token: 0x06000969 RID: 2409 RVA: 0x0001F668 File Offset: 0x0001D868
		// (remove) Token: 0x0600096A RID: 2410 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptPostFunctionResultForScheduledTaskResultEvent;

		// Token: 0x14000150 RID: 336
		// (add) Token: 0x0600096B RID: 2411 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		// (remove) Token: 0x0600096C RID: 2412 RVA: 0x0001F710 File Offset: 0x0001D910
		public event PlayFabEvents.PlayFabRequestEvent<RegisterHttpFunctionRequest> OnCloudScriptRegisterHttpFunctionRequestEvent;

		// Token: 0x14000151 RID: 337
		// (add) Token: 0x0600096D RID: 2413 RVA: 0x0001F748 File Offset: 0x0001D948
		// (remove) Token: 0x0600096E RID: 2414 RVA: 0x0001F780 File Offset: 0x0001D980
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptRegisterHttpFunctionResultEvent;

		// Token: 0x14000152 RID: 338
		// (add) Token: 0x0600096F RID: 2415 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		// (remove) Token: 0x06000970 RID: 2416 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
		public event PlayFabEvents.PlayFabRequestEvent<RegisterQueuedFunctionRequest> OnCloudScriptRegisterQueuedFunctionRequestEvent;

		// Token: 0x14000153 RID: 339
		// (add) Token: 0x06000971 RID: 2417 RVA: 0x0001F828 File Offset: 0x0001DA28
		// (remove) Token: 0x06000972 RID: 2418 RVA: 0x0001F860 File Offset: 0x0001DA60
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptRegisterQueuedFunctionResultEvent;

		// Token: 0x14000154 RID: 340
		// (add) Token: 0x06000973 RID: 2419 RVA: 0x0001F898 File Offset: 0x0001DA98
		// (remove) Token: 0x06000974 RID: 2420 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		public event PlayFabEvents.PlayFabRequestEvent<UnregisterFunctionRequest> OnCloudScriptUnregisterFunctionRequestEvent;

		// Token: 0x14000155 RID: 341
		// (add) Token: 0x06000975 RID: 2421 RVA: 0x0001F908 File Offset: 0x0001DB08
		// (remove) Token: 0x06000976 RID: 2422 RVA: 0x0001F940 File Offset: 0x0001DB40
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult> OnCloudScriptUnregisterFunctionResultEvent;

		// Token: 0x14000156 RID: 342
		// (add) Token: 0x06000977 RID: 2423 RVA: 0x0001F978 File Offset: 0x0001DB78
		// (remove) Token: 0x06000978 RID: 2424 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		public event PlayFabEvents.PlayFabRequestEvent<AbortFileUploadsRequest> OnDataAbortFileUploadsRequestEvent;

		// Token: 0x14000157 RID: 343
		// (add) Token: 0x06000979 RID: 2425 RVA: 0x0001F9E8 File Offset: 0x0001DBE8
		// (remove) Token: 0x0600097A RID: 2426 RVA: 0x0001FA20 File Offset: 0x0001DC20
		public event PlayFabEvents.PlayFabResultEvent<AbortFileUploadsResponse> OnDataAbortFileUploadsResultEvent;

		// Token: 0x14000158 RID: 344
		// (add) Token: 0x0600097B RID: 2427 RVA: 0x0001FA58 File Offset: 0x0001DC58
		// (remove) Token: 0x0600097C RID: 2428 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public event PlayFabEvents.PlayFabRequestEvent<DeleteFilesRequest> OnDataDeleteFilesRequestEvent;

		// Token: 0x14000159 RID: 345
		// (add) Token: 0x0600097D RID: 2429 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		// (remove) Token: 0x0600097E RID: 2430 RVA: 0x0001FB00 File Offset: 0x0001DD00
		public event PlayFabEvents.PlayFabResultEvent<DeleteFilesResponse> OnDataDeleteFilesResultEvent;

		// Token: 0x1400015A RID: 346
		// (add) Token: 0x0600097F RID: 2431 RVA: 0x0001FB38 File Offset: 0x0001DD38
		// (remove) Token: 0x06000980 RID: 2432 RVA: 0x0001FB70 File Offset: 0x0001DD70
		public event PlayFabEvents.PlayFabRequestEvent<FinalizeFileUploadsRequest> OnDataFinalizeFileUploadsRequestEvent;

		// Token: 0x1400015B RID: 347
		// (add) Token: 0x06000981 RID: 2433 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		// (remove) Token: 0x06000982 RID: 2434 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		public event PlayFabEvents.PlayFabResultEvent<FinalizeFileUploadsResponse> OnDataFinalizeFileUploadsResultEvent;

		// Token: 0x1400015C RID: 348
		// (add) Token: 0x06000983 RID: 2435 RVA: 0x0001FC18 File Offset: 0x0001DE18
		// (remove) Token: 0x06000984 RID: 2436 RVA: 0x0001FC50 File Offset: 0x0001DE50
		public event PlayFabEvents.PlayFabRequestEvent<GetFilesRequest> OnDataGetFilesRequestEvent;

		// Token: 0x1400015D RID: 349
		// (add) Token: 0x06000985 RID: 2437 RVA: 0x0001FC88 File Offset: 0x0001DE88
		// (remove) Token: 0x06000986 RID: 2438 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		public event PlayFabEvents.PlayFabResultEvent<GetFilesResponse> OnDataGetFilesResultEvent;

		// Token: 0x1400015E RID: 350
		// (add) Token: 0x06000987 RID: 2439 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		// (remove) Token: 0x06000988 RID: 2440 RVA: 0x0001FD30 File Offset: 0x0001DF30
		public event PlayFabEvents.PlayFabRequestEvent<GetObjectsRequest> OnDataGetObjectsRequestEvent;

		// Token: 0x1400015F RID: 351
		// (add) Token: 0x06000989 RID: 2441 RVA: 0x0001FD68 File Offset: 0x0001DF68
		// (remove) Token: 0x0600098A RID: 2442 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		public event PlayFabEvents.PlayFabResultEvent<GetObjectsResponse> OnDataGetObjectsResultEvent;

		// Token: 0x14000160 RID: 352
		// (add) Token: 0x0600098B RID: 2443 RVA: 0x0001FDD8 File Offset: 0x0001DFD8
		// (remove) Token: 0x0600098C RID: 2444 RVA: 0x0001FE10 File Offset: 0x0001E010
		public event PlayFabEvents.PlayFabRequestEvent<InitiateFileUploadsRequest> OnDataInitiateFileUploadsRequestEvent;

		// Token: 0x14000161 RID: 353
		// (add) Token: 0x0600098D RID: 2445 RVA: 0x0001FE48 File Offset: 0x0001E048
		// (remove) Token: 0x0600098E RID: 2446 RVA: 0x0001FE80 File Offset: 0x0001E080
		public event PlayFabEvents.PlayFabResultEvent<InitiateFileUploadsResponse> OnDataInitiateFileUploadsResultEvent;

		// Token: 0x14000162 RID: 354
		// (add) Token: 0x0600098F RID: 2447 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		// (remove) Token: 0x06000990 RID: 2448 RVA: 0x0001FEF0 File Offset: 0x0001E0F0
		public event PlayFabEvents.PlayFabRequestEvent<SetObjectsRequest> OnDataSetObjectsRequestEvent;

		// Token: 0x14000163 RID: 355
		// (add) Token: 0x06000991 RID: 2449 RVA: 0x0001FF28 File Offset: 0x0001E128
		// (remove) Token: 0x06000992 RID: 2450 RVA: 0x0001FF60 File Offset: 0x0001E160
		public event PlayFabEvents.PlayFabResultEvent<SetObjectsResponse> OnDataSetObjectsResultEvent;

		// Token: 0x14000164 RID: 356
		// (add) Token: 0x06000993 RID: 2451 RVA: 0x0001FF98 File Offset: 0x0001E198
		// (remove) Token: 0x06000994 RID: 2452 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		public event PlayFabEvents.PlayFabRequestEvent<WriteEventsRequest> OnEventsWriteEventsRequestEvent;

		// Token: 0x14000165 RID: 357
		// (add) Token: 0x06000995 RID: 2453 RVA: 0x00020008 File Offset: 0x0001E208
		// (remove) Token: 0x06000996 RID: 2454 RVA: 0x00020040 File Offset: 0x0001E240
		public event PlayFabEvents.PlayFabResultEvent<WriteEventsResponse> OnEventsWriteEventsResultEvent;

		// Token: 0x14000166 RID: 358
		// (add) Token: 0x06000997 RID: 2455 RVA: 0x00020078 File Offset: 0x0001E278
		// (remove) Token: 0x06000998 RID: 2456 RVA: 0x000200B0 File Offset: 0x0001E2B0
		public event PlayFabEvents.PlayFabRequestEvent<WriteEventsRequest> OnEventsWriteTelemetryEventsRequestEvent;

		// Token: 0x14000167 RID: 359
		// (add) Token: 0x06000999 RID: 2457 RVA: 0x000200E8 File Offset: 0x0001E2E8
		// (remove) Token: 0x0600099A RID: 2458 RVA: 0x00020120 File Offset: 0x0001E320
		public event PlayFabEvents.PlayFabResultEvent<WriteEventsResponse> OnEventsWriteTelemetryEventsResultEvent;

		// Token: 0x14000168 RID: 360
		// (add) Token: 0x0600099B RID: 2459 RVA: 0x00020158 File Offset: 0x0001E358
		// (remove) Token: 0x0600099C RID: 2460 RVA: 0x00020190 File Offset: 0x0001E390
		public event PlayFabEvents.PlayFabRequestEvent<CreateExperimentRequest> OnExperimentationCreateExperimentRequestEvent;

		// Token: 0x14000169 RID: 361
		// (add) Token: 0x0600099D RID: 2461 RVA: 0x000201C8 File Offset: 0x0001E3C8
		// (remove) Token: 0x0600099E RID: 2462 RVA: 0x00020200 File Offset: 0x0001E400
		public event PlayFabEvents.PlayFabResultEvent<CreateExperimentResult> OnExperimentationCreateExperimentResultEvent;

		// Token: 0x1400016A RID: 362
		// (add) Token: 0x0600099F RID: 2463 RVA: 0x00020238 File Offset: 0x0001E438
		// (remove) Token: 0x060009A0 RID: 2464 RVA: 0x00020270 File Offset: 0x0001E470
		public event PlayFabEvents.PlayFabRequestEvent<DeleteExperimentRequest> OnExperimentationDeleteExperimentRequestEvent;

		// Token: 0x1400016B RID: 363
		// (add) Token: 0x060009A1 RID: 2465 RVA: 0x000202A8 File Offset: 0x0001E4A8
		// (remove) Token: 0x060009A2 RID: 2466 RVA: 0x000202E0 File Offset: 0x0001E4E0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationDeleteExperimentResultEvent;

		// Token: 0x1400016C RID: 364
		// (add) Token: 0x060009A3 RID: 2467 RVA: 0x00020318 File Offset: 0x0001E518
		// (remove) Token: 0x060009A4 RID: 2468 RVA: 0x00020350 File Offset: 0x0001E550
		public event PlayFabEvents.PlayFabRequestEvent<GetExperimentsRequest> OnExperimentationGetExperimentsRequestEvent;

		// Token: 0x1400016D RID: 365
		// (add) Token: 0x060009A5 RID: 2469 RVA: 0x00020388 File Offset: 0x0001E588
		// (remove) Token: 0x060009A6 RID: 2470 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public event PlayFabEvents.PlayFabResultEvent<GetExperimentsResult> OnExperimentationGetExperimentsResultEvent;

		// Token: 0x1400016E RID: 366
		// (add) Token: 0x060009A7 RID: 2471 RVA: 0x000203F8 File Offset: 0x0001E5F8
		// (remove) Token: 0x060009A8 RID: 2472 RVA: 0x00020430 File Offset: 0x0001E630
		public event PlayFabEvents.PlayFabRequestEvent<GetLatestScorecardRequest> OnExperimentationGetLatestScorecardRequestEvent;

		// Token: 0x1400016F RID: 367
		// (add) Token: 0x060009A9 RID: 2473 RVA: 0x00020468 File Offset: 0x0001E668
		// (remove) Token: 0x060009AA RID: 2474 RVA: 0x000204A0 File Offset: 0x0001E6A0
		public event PlayFabEvents.PlayFabResultEvent<GetLatestScorecardResult> OnExperimentationGetLatestScorecardResultEvent;

		// Token: 0x14000170 RID: 368
		// (add) Token: 0x060009AB RID: 2475 RVA: 0x000204D8 File Offset: 0x0001E6D8
		// (remove) Token: 0x060009AC RID: 2476 RVA: 0x00020510 File Offset: 0x0001E710
		public event PlayFabEvents.PlayFabRequestEvent<GetTreatmentAssignmentRequest> OnExperimentationGetTreatmentAssignmentRequestEvent;

		// Token: 0x14000171 RID: 369
		// (add) Token: 0x060009AD RID: 2477 RVA: 0x00020548 File Offset: 0x0001E748
		// (remove) Token: 0x060009AE RID: 2478 RVA: 0x00020580 File Offset: 0x0001E780
		public event PlayFabEvents.PlayFabResultEvent<GetTreatmentAssignmentResult> OnExperimentationGetTreatmentAssignmentResultEvent;

		// Token: 0x14000172 RID: 370
		// (add) Token: 0x060009AF RID: 2479 RVA: 0x000205B8 File Offset: 0x0001E7B8
		// (remove) Token: 0x060009B0 RID: 2480 RVA: 0x000205F0 File Offset: 0x0001E7F0
		public event PlayFabEvents.PlayFabRequestEvent<StartExperimentRequest> OnExperimentationStartExperimentRequestEvent;

		// Token: 0x14000173 RID: 371
		// (add) Token: 0x060009B1 RID: 2481 RVA: 0x00020628 File Offset: 0x0001E828
		// (remove) Token: 0x060009B2 RID: 2482 RVA: 0x00020660 File Offset: 0x0001E860
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationStartExperimentResultEvent;

		// Token: 0x14000174 RID: 372
		// (add) Token: 0x060009B3 RID: 2483 RVA: 0x00020698 File Offset: 0x0001E898
		// (remove) Token: 0x060009B4 RID: 2484 RVA: 0x000206D0 File Offset: 0x0001E8D0
		public event PlayFabEvents.PlayFabRequestEvent<StopExperimentRequest> OnExperimentationStopExperimentRequestEvent;

		// Token: 0x14000175 RID: 373
		// (add) Token: 0x060009B5 RID: 2485 RVA: 0x00020708 File Offset: 0x0001E908
		// (remove) Token: 0x060009B6 RID: 2486 RVA: 0x00020740 File Offset: 0x0001E940
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationStopExperimentResultEvent;

		// Token: 0x14000176 RID: 374
		// (add) Token: 0x060009B7 RID: 2487 RVA: 0x00020778 File Offset: 0x0001E978
		// (remove) Token: 0x060009B8 RID: 2488 RVA: 0x000207B0 File Offset: 0x0001E9B0
		public event PlayFabEvents.PlayFabRequestEvent<UpdateExperimentRequest> OnExperimentationUpdateExperimentRequestEvent;

		// Token: 0x14000177 RID: 375
		// (add) Token: 0x060009B9 RID: 2489 RVA: 0x000207E8 File Offset: 0x0001E9E8
		// (remove) Token: 0x060009BA RID: 2490 RVA: 0x00020820 File Offset: 0x0001EA20
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse> OnExperimentationUpdateExperimentResultEvent;

		// Token: 0x14000178 RID: 376
		// (add) Token: 0x060009BB RID: 2491 RVA: 0x00020858 File Offset: 0x0001EA58
		// (remove) Token: 0x060009BC RID: 2492 RVA: 0x00020890 File Offset: 0x0001EA90
		public event PlayFabEvents.PlayFabRequestEvent<AcceptGroupApplicationRequest> OnGroupsAcceptGroupApplicationRequestEvent;

		// Token: 0x14000179 RID: 377
		// (add) Token: 0x060009BD RID: 2493 RVA: 0x000208C8 File Offset: 0x0001EAC8
		// (remove) Token: 0x060009BE RID: 2494 RVA: 0x00020900 File Offset: 0x0001EB00
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAcceptGroupApplicationResultEvent;

		// Token: 0x1400017A RID: 378
		// (add) Token: 0x060009BF RID: 2495 RVA: 0x00020938 File Offset: 0x0001EB38
		// (remove) Token: 0x060009C0 RID: 2496 RVA: 0x00020970 File Offset: 0x0001EB70
		public event PlayFabEvents.PlayFabRequestEvent<AcceptGroupInvitationRequest> OnGroupsAcceptGroupInvitationRequestEvent;

		// Token: 0x1400017B RID: 379
		// (add) Token: 0x060009C1 RID: 2497 RVA: 0x000209A8 File Offset: 0x0001EBA8
		// (remove) Token: 0x060009C2 RID: 2498 RVA: 0x000209E0 File Offset: 0x0001EBE0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAcceptGroupInvitationResultEvent;

		// Token: 0x1400017C RID: 380
		// (add) Token: 0x060009C3 RID: 2499 RVA: 0x00020A18 File Offset: 0x0001EC18
		// (remove) Token: 0x060009C4 RID: 2500 RVA: 0x00020A50 File Offset: 0x0001EC50
		public event PlayFabEvents.PlayFabRequestEvent<AddMembersRequest> OnGroupsAddMembersRequestEvent;

		// Token: 0x1400017D RID: 381
		// (add) Token: 0x060009C5 RID: 2501 RVA: 0x00020A88 File Offset: 0x0001EC88
		// (remove) Token: 0x060009C6 RID: 2502 RVA: 0x00020AC0 File Offset: 0x0001ECC0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsAddMembersResultEvent;

		// Token: 0x1400017E RID: 382
		// (add) Token: 0x060009C7 RID: 2503 RVA: 0x00020AF8 File Offset: 0x0001ECF8
		// (remove) Token: 0x060009C8 RID: 2504 RVA: 0x00020B30 File Offset: 0x0001ED30
		public event PlayFabEvents.PlayFabRequestEvent<ApplyToGroupRequest> OnGroupsApplyToGroupRequestEvent;

		// Token: 0x1400017F RID: 383
		// (add) Token: 0x060009C9 RID: 2505 RVA: 0x00020B68 File Offset: 0x0001ED68
		// (remove) Token: 0x060009CA RID: 2506 RVA: 0x00020BA0 File Offset: 0x0001EDA0
		public event PlayFabEvents.PlayFabResultEvent<ApplyToGroupResponse> OnGroupsApplyToGroupResultEvent;

		// Token: 0x14000180 RID: 384
		// (add) Token: 0x060009CB RID: 2507 RVA: 0x00020BD8 File Offset: 0x0001EDD8
		// (remove) Token: 0x060009CC RID: 2508 RVA: 0x00020C10 File Offset: 0x0001EE10
		public event PlayFabEvents.PlayFabRequestEvent<BlockEntityRequest> OnGroupsBlockEntityRequestEvent;

		// Token: 0x14000181 RID: 385
		// (add) Token: 0x060009CD RID: 2509 RVA: 0x00020C48 File Offset: 0x0001EE48
		// (remove) Token: 0x060009CE RID: 2510 RVA: 0x00020C80 File Offset: 0x0001EE80
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsBlockEntityResultEvent;

		// Token: 0x14000182 RID: 386
		// (add) Token: 0x060009CF RID: 2511 RVA: 0x00020CB8 File Offset: 0x0001EEB8
		// (remove) Token: 0x060009D0 RID: 2512 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		public event PlayFabEvents.PlayFabRequestEvent<ChangeMemberRoleRequest> OnGroupsChangeMemberRoleRequestEvent;

		// Token: 0x14000183 RID: 387
		// (add) Token: 0x060009D1 RID: 2513 RVA: 0x00020D28 File Offset: 0x0001EF28
		// (remove) Token: 0x060009D2 RID: 2514 RVA: 0x00020D60 File Offset: 0x0001EF60
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsChangeMemberRoleResultEvent;

		// Token: 0x14000184 RID: 388
		// (add) Token: 0x060009D3 RID: 2515 RVA: 0x00020D98 File Offset: 0x0001EF98
		// (remove) Token: 0x060009D4 RID: 2516 RVA: 0x00020DD0 File Offset: 0x0001EFD0
		public event PlayFabEvents.PlayFabRequestEvent<CreateGroupRequest> OnGroupsCreateGroupRequestEvent;

		// Token: 0x14000185 RID: 389
		// (add) Token: 0x060009D5 RID: 2517 RVA: 0x00020E08 File Offset: 0x0001F008
		// (remove) Token: 0x060009D6 RID: 2518 RVA: 0x00020E40 File Offset: 0x0001F040
		public event PlayFabEvents.PlayFabResultEvent<CreateGroupResponse> OnGroupsCreateGroupResultEvent;

		// Token: 0x14000186 RID: 390
		// (add) Token: 0x060009D7 RID: 2519 RVA: 0x00020E78 File Offset: 0x0001F078
		// (remove) Token: 0x060009D8 RID: 2520 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		public event PlayFabEvents.PlayFabRequestEvent<CreateGroupRoleRequest> OnGroupsCreateRoleRequestEvent;

		// Token: 0x14000187 RID: 391
		// (add) Token: 0x060009D9 RID: 2521 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		// (remove) Token: 0x060009DA RID: 2522 RVA: 0x00020F20 File Offset: 0x0001F120
		public event PlayFabEvents.PlayFabResultEvent<CreateGroupRoleResponse> OnGroupsCreateRoleResultEvent;

		// Token: 0x14000188 RID: 392
		// (add) Token: 0x060009DB RID: 2523 RVA: 0x00020F58 File Offset: 0x0001F158
		// (remove) Token: 0x060009DC RID: 2524 RVA: 0x00020F90 File Offset: 0x0001F190
		public event PlayFabEvents.PlayFabRequestEvent<DeleteGroupRequest> OnGroupsDeleteGroupRequestEvent;

		// Token: 0x14000189 RID: 393
		// (add) Token: 0x060009DD RID: 2525 RVA: 0x00020FC8 File Offset: 0x0001F1C8
		// (remove) Token: 0x060009DE RID: 2526 RVA: 0x00021000 File Offset: 0x0001F200
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsDeleteGroupResultEvent;

		// Token: 0x1400018A RID: 394
		// (add) Token: 0x060009DF RID: 2527 RVA: 0x00021038 File Offset: 0x0001F238
		// (remove) Token: 0x060009E0 RID: 2528 RVA: 0x00021070 File Offset: 0x0001F270
		public event PlayFabEvents.PlayFabRequestEvent<DeleteRoleRequest> OnGroupsDeleteRoleRequestEvent;

		// Token: 0x1400018B RID: 395
		// (add) Token: 0x060009E1 RID: 2529 RVA: 0x000210A8 File Offset: 0x0001F2A8
		// (remove) Token: 0x060009E2 RID: 2530 RVA: 0x000210E0 File Offset: 0x0001F2E0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsDeleteRoleResultEvent;

		// Token: 0x1400018C RID: 396
		// (add) Token: 0x060009E3 RID: 2531 RVA: 0x00021118 File Offset: 0x0001F318
		// (remove) Token: 0x060009E4 RID: 2532 RVA: 0x00021150 File Offset: 0x0001F350
		public event PlayFabEvents.PlayFabRequestEvent<GetGroupRequest> OnGroupsGetGroupRequestEvent;

		// Token: 0x1400018D RID: 397
		// (add) Token: 0x060009E5 RID: 2533 RVA: 0x00021188 File Offset: 0x0001F388
		// (remove) Token: 0x060009E6 RID: 2534 RVA: 0x000211C0 File Offset: 0x0001F3C0
		public event PlayFabEvents.PlayFabResultEvent<GetGroupResponse> OnGroupsGetGroupResultEvent;

		// Token: 0x1400018E RID: 398
		// (add) Token: 0x060009E7 RID: 2535 RVA: 0x000211F8 File Offset: 0x0001F3F8
		// (remove) Token: 0x060009E8 RID: 2536 RVA: 0x00021230 File Offset: 0x0001F430
		public event PlayFabEvents.PlayFabRequestEvent<InviteToGroupRequest> OnGroupsInviteToGroupRequestEvent;

		// Token: 0x1400018F RID: 399
		// (add) Token: 0x060009E9 RID: 2537 RVA: 0x00021268 File Offset: 0x0001F468
		// (remove) Token: 0x060009EA RID: 2538 RVA: 0x000212A0 File Offset: 0x0001F4A0
		public event PlayFabEvents.PlayFabResultEvent<InviteToGroupResponse> OnGroupsInviteToGroupResultEvent;

		// Token: 0x14000190 RID: 400
		// (add) Token: 0x060009EB RID: 2539 RVA: 0x000212D8 File Offset: 0x0001F4D8
		// (remove) Token: 0x060009EC RID: 2540 RVA: 0x00021310 File Offset: 0x0001F510
		public event PlayFabEvents.PlayFabRequestEvent<IsMemberRequest> OnGroupsIsMemberRequestEvent;

		// Token: 0x14000191 RID: 401
		// (add) Token: 0x060009ED RID: 2541 RVA: 0x00021348 File Offset: 0x0001F548
		// (remove) Token: 0x060009EE RID: 2542 RVA: 0x00021380 File Offset: 0x0001F580
		public event PlayFabEvents.PlayFabResultEvent<IsMemberResponse> OnGroupsIsMemberResultEvent;

		// Token: 0x14000192 RID: 402
		// (add) Token: 0x060009EF RID: 2543 RVA: 0x000213B8 File Offset: 0x0001F5B8
		// (remove) Token: 0x060009F0 RID: 2544 RVA: 0x000213F0 File Offset: 0x0001F5F0
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupApplicationsRequest> OnGroupsListGroupApplicationsRequestEvent;

		// Token: 0x14000193 RID: 403
		// (add) Token: 0x060009F1 RID: 2545 RVA: 0x00021428 File Offset: 0x0001F628
		// (remove) Token: 0x060009F2 RID: 2546 RVA: 0x00021460 File Offset: 0x0001F660
		public event PlayFabEvents.PlayFabResultEvent<ListGroupApplicationsResponse> OnGroupsListGroupApplicationsResultEvent;

		// Token: 0x14000194 RID: 404
		// (add) Token: 0x060009F3 RID: 2547 RVA: 0x00021498 File Offset: 0x0001F698
		// (remove) Token: 0x060009F4 RID: 2548 RVA: 0x000214D0 File Offset: 0x0001F6D0
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupBlocksRequest> OnGroupsListGroupBlocksRequestEvent;

		// Token: 0x14000195 RID: 405
		// (add) Token: 0x060009F5 RID: 2549 RVA: 0x00021508 File Offset: 0x0001F708
		// (remove) Token: 0x060009F6 RID: 2550 RVA: 0x00021540 File Offset: 0x0001F740
		public event PlayFabEvents.PlayFabResultEvent<ListGroupBlocksResponse> OnGroupsListGroupBlocksResultEvent;

		// Token: 0x14000196 RID: 406
		// (add) Token: 0x060009F7 RID: 2551 RVA: 0x00021578 File Offset: 0x0001F778
		// (remove) Token: 0x060009F8 RID: 2552 RVA: 0x000215B0 File Offset: 0x0001F7B0
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupInvitationsRequest> OnGroupsListGroupInvitationsRequestEvent;

		// Token: 0x14000197 RID: 407
		// (add) Token: 0x060009F9 RID: 2553 RVA: 0x000215E8 File Offset: 0x0001F7E8
		// (remove) Token: 0x060009FA RID: 2554 RVA: 0x00021620 File Offset: 0x0001F820
		public event PlayFabEvents.PlayFabResultEvent<ListGroupInvitationsResponse> OnGroupsListGroupInvitationsResultEvent;

		// Token: 0x14000198 RID: 408
		// (add) Token: 0x060009FB RID: 2555 RVA: 0x00021658 File Offset: 0x0001F858
		// (remove) Token: 0x060009FC RID: 2556 RVA: 0x00021690 File Offset: 0x0001F890
		public event PlayFabEvents.PlayFabRequestEvent<ListGroupMembersRequest> OnGroupsListGroupMembersRequestEvent;

		// Token: 0x14000199 RID: 409
		// (add) Token: 0x060009FD RID: 2557 RVA: 0x000216C8 File Offset: 0x0001F8C8
		// (remove) Token: 0x060009FE RID: 2558 RVA: 0x00021700 File Offset: 0x0001F900
		public event PlayFabEvents.PlayFabResultEvent<ListGroupMembersResponse> OnGroupsListGroupMembersResultEvent;

		// Token: 0x1400019A RID: 410
		// (add) Token: 0x060009FF RID: 2559 RVA: 0x00021738 File Offset: 0x0001F938
		// (remove) Token: 0x06000A00 RID: 2560 RVA: 0x00021770 File Offset: 0x0001F970
		public event PlayFabEvents.PlayFabRequestEvent<ListMembershipRequest> OnGroupsListMembershipRequestEvent;

		// Token: 0x1400019B RID: 411
		// (add) Token: 0x06000A01 RID: 2561 RVA: 0x000217A8 File Offset: 0x0001F9A8
		// (remove) Token: 0x06000A02 RID: 2562 RVA: 0x000217E0 File Offset: 0x0001F9E0
		public event PlayFabEvents.PlayFabResultEvent<ListMembershipResponse> OnGroupsListMembershipResultEvent;

		// Token: 0x1400019C RID: 412
		// (add) Token: 0x06000A03 RID: 2563 RVA: 0x00021818 File Offset: 0x0001FA18
		// (remove) Token: 0x06000A04 RID: 2564 RVA: 0x00021850 File Offset: 0x0001FA50
		public event PlayFabEvents.PlayFabRequestEvent<ListMembershipOpportunitiesRequest> OnGroupsListMembershipOpportunitiesRequestEvent;

		// Token: 0x1400019D RID: 413
		// (add) Token: 0x06000A05 RID: 2565 RVA: 0x00021888 File Offset: 0x0001FA88
		// (remove) Token: 0x06000A06 RID: 2566 RVA: 0x000218C0 File Offset: 0x0001FAC0
		public event PlayFabEvents.PlayFabResultEvent<ListMembershipOpportunitiesResponse> OnGroupsListMembershipOpportunitiesResultEvent;

		// Token: 0x1400019E RID: 414
		// (add) Token: 0x06000A07 RID: 2567 RVA: 0x000218F8 File Offset: 0x0001FAF8
		// (remove) Token: 0x06000A08 RID: 2568 RVA: 0x00021930 File Offset: 0x0001FB30
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGroupApplicationRequest> OnGroupsRemoveGroupApplicationRequestEvent;

		// Token: 0x1400019F RID: 415
		// (add) Token: 0x06000A09 RID: 2569 RVA: 0x00021968 File Offset: 0x0001FB68
		// (remove) Token: 0x06000A0A RID: 2570 RVA: 0x000219A0 File Offset: 0x0001FBA0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveGroupApplicationResultEvent;

		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x06000A0B RID: 2571 RVA: 0x000219D8 File Offset: 0x0001FBD8
		// (remove) Token: 0x06000A0C RID: 2572 RVA: 0x00021A10 File Offset: 0x0001FC10
		public event PlayFabEvents.PlayFabRequestEvent<RemoveGroupInvitationRequest> OnGroupsRemoveGroupInvitationRequestEvent;

		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x06000A0D RID: 2573 RVA: 0x00021A48 File Offset: 0x0001FC48
		// (remove) Token: 0x06000A0E RID: 2574 RVA: 0x00021A80 File Offset: 0x0001FC80
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveGroupInvitationResultEvent;

		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x06000A0F RID: 2575 RVA: 0x00021AB8 File Offset: 0x0001FCB8
		// (remove) Token: 0x06000A10 RID: 2576 RVA: 0x00021AF0 File Offset: 0x0001FCF0
		public event PlayFabEvents.PlayFabRequestEvent<RemoveMembersRequest> OnGroupsRemoveMembersRequestEvent;

		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x06000A11 RID: 2577 RVA: 0x00021B28 File Offset: 0x0001FD28
		// (remove) Token: 0x06000A12 RID: 2578 RVA: 0x00021B60 File Offset: 0x0001FD60
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsRemoveMembersResultEvent;

		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x06000A13 RID: 2579 RVA: 0x00021B98 File Offset: 0x0001FD98
		// (remove) Token: 0x06000A14 RID: 2580 RVA: 0x00021BD0 File Offset: 0x0001FDD0
		public event PlayFabEvents.PlayFabRequestEvent<UnblockEntityRequest> OnGroupsUnblockEntityRequestEvent;

		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x06000A15 RID: 2581 RVA: 0x00021C08 File Offset: 0x0001FE08
		// (remove) Token: 0x06000A16 RID: 2582 RVA: 0x00021C40 File Offset: 0x0001FE40
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse> OnGroupsUnblockEntityResultEvent;

		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x06000A17 RID: 2583 RVA: 0x00021C78 File Offset: 0x0001FE78
		// (remove) Token: 0x06000A18 RID: 2584 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		public event PlayFabEvents.PlayFabRequestEvent<UpdateGroupRequest> OnGroupsUpdateGroupRequestEvent;

		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x06000A19 RID: 2585 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		// (remove) Token: 0x06000A1A RID: 2586 RVA: 0x00021D20 File Offset: 0x0001FF20
		public event PlayFabEvents.PlayFabResultEvent<UpdateGroupResponse> OnGroupsUpdateGroupResultEvent;

		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x06000A1B RID: 2587 RVA: 0x00021D58 File Offset: 0x0001FF58
		// (remove) Token: 0x06000A1C RID: 2588 RVA: 0x00021D90 File Offset: 0x0001FF90
		public event PlayFabEvents.PlayFabRequestEvent<UpdateGroupRoleRequest> OnGroupsUpdateRoleRequestEvent;

		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x06000A1D RID: 2589 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		// (remove) Token: 0x06000A1E RID: 2590 RVA: 0x00021E00 File Offset: 0x00020000
		public event PlayFabEvents.PlayFabResultEvent<UpdateGroupRoleResponse> OnGroupsUpdateRoleResultEvent;

		// Token: 0x140001AA RID: 426
		// (add) Token: 0x06000A1F RID: 2591 RVA: 0x00021E38 File Offset: 0x00020038
		// (remove) Token: 0x06000A20 RID: 2592 RVA: 0x00021E70 File Offset: 0x00020070
		public event PlayFabEvents.PlayFabRequestEvent<InsightsEmptyRequest> OnInsightsGetDetailsRequestEvent;

		// Token: 0x140001AB RID: 427
		// (add) Token: 0x06000A21 RID: 2593 RVA: 0x00021EA8 File Offset: 0x000200A8
		// (remove) Token: 0x06000A22 RID: 2594 RVA: 0x00021EE0 File Offset: 0x000200E0
		public event PlayFabEvents.PlayFabResultEvent<InsightsGetDetailsResponse> OnInsightsGetDetailsResultEvent;

		// Token: 0x140001AC RID: 428
		// (add) Token: 0x06000A23 RID: 2595 RVA: 0x00021F18 File Offset: 0x00020118
		// (remove) Token: 0x06000A24 RID: 2596 RVA: 0x00021F50 File Offset: 0x00020150
		public event PlayFabEvents.PlayFabRequestEvent<InsightsEmptyRequest> OnInsightsGetLimitsRequestEvent;

		// Token: 0x140001AD RID: 429
		// (add) Token: 0x06000A25 RID: 2597 RVA: 0x00021F88 File Offset: 0x00020188
		// (remove) Token: 0x06000A26 RID: 2598 RVA: 0x00021FC0 File Offset: 0x000201C0
		public event PlayFabEvents.PlayFabResultEvent<InsightsGetLimitsResponse> OnInsightsGetLimitsResultEvent;

		// Token: 0x140001AE RID: 430
		// (add) Token: 0x06000A27 RID: 2599 RVA: 0x00021FF8 File Offset: 0x000201F8
		// (remove) Token: 0x06000A28 RID: 2600 RVA: 0x00022030 File Offset: 0x00020230
		public event PlayFabEvents.PlayFabRequestEvent<InsightsGetOperationStatusRequest> OnInsightsGetOperationStatusRequestEvent;

		// Token: 0x140001AF RID: 431
		// (add) Token: 0x06000A29 RID: 2601 RVA: 0x00022068 File Offset: 0x00020268
		// (remove) Token: 0x06000A2A RID: 2602 RVA: 0x000220A0 File Offset: 0x000202A0
		public event PlayFabEvents.PlayFabResultEvent<InsightsGetOperationStatusResponse> OnInsightsGetOperationStatusResultEvent;

		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x06000A2B RID: 2603 RVA: 0x000220D8 File Offset: 0x000202D8
		// (remove) Token: 0x06000A2C RID: 2604 RVA: 0x00022110 File Offset: 0x00020310
		public event PlayFabEvents.PlayFabRequestEvent<InsightsGetPendingOperationsRequest> OnInsightsGetPendingOperationsRequestEvent;

		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x06000A2D RID: 2605 RVA: 0x00022148 File Offset: 0x00020348
		// (remove) Token: 0x06000A2E RID: 2606 RVA: 0x00022180 File Offset: 0x00020380
		public event PlayFabEvents.PlayFabResultEvent<InsightsGetPendingOperationsResponse> OnInsightsGetPendingOperationsResultEvent;

		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x06000A2F RID: 2607 RVA: 0x000221B8 File Offset: 0x000203B8
		// (remove) Token: 0x06000A30 RID: 2608 RVA: 0x000221F0 File Offset: 0x000203F0
		public event PlayFabEvents.PlayFabRequestEvent<InsightsSetPerformanceRequest> OnInsightsSetPerformanceRequestEvent;

		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x06000A31 RID: 2609 RVA: 0x00022228 File Offset: 0x00020428
		// (remove) Token: 0x06000A32 RID: 2610 RVA: 0x00022260 File Offset: 0x00020460
		public event PlayFabEvents.PlayFabResultEvent<InsightsOperationResponse> OnInsightsSetPerformanceResultEvent;

		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x06000A33 RID: 2611 RVA: 0x00022298 File Offset: 0x00020498
		// (remove) Token: 0x06000A34 RID: 2612 RVA: 0x000222D0 File Offset: 0x000204D0
		public event PlayFabEvents.PlayFabRequestEvent<InsightsSetStorageRetentionRequest> OnInsightsSetStorageRetentionRequestEvent;

		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x06000A35 RID: 2613 RVA: 0x00022308 File Offset: 0x00020508
		// (remove) Token: 0x06000A36 RID: 2614 RVA: 0x00022340 File Offset: 0x00020540
		public event PlayFabEvents.PlayFabResultEvent<InsightsOperationResponse> OnInsightsSetStorageRetentionResultEvent;

		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x06000A37 RID: 2615 RVA: 0x00022378 File Offset: 0x00020578
		// (remove) Token: 0x06000A38 RID: 2616 RVA: 0x000223B0 File Offset: 0x000205B0
		public event PlayFabEvents.PlayFabRequestEvent<GetLanguageListRequest> OnLocalizationGetLanguageListRequestEvent;

		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x06000A39 RID: 2617 RVA: 0x000223E8 File Offset: 0x000205E8
		// (remove) Token: 0x06000A3A RID: 2618 RVA: 0x00022420 File Offset: 0x00020620
		public event PlayFabEvents.PlayFabResultEvent<GetLanguageListResponse> OnLocalizationGetLanguageListResultEvent;

		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x06000A3B RID: 2619 RVA: 0x00022458 File Offset: 0x00020658
		// (remove) Token: 0x06000A3C RID: 2620 RVA: 0x00022490 File Offset: 0x00020690
		public event PlayFabEvents.PlayFabRequestEvent<CancelAllMatchmakingTicketsForPlayerRequest> OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent;

		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x06000A3D RID: 2621 RVA: 0x000224C8 File Offset: 0x000206C8
		// (remove) Token: 0x06000A3E RID: 2622 RVA: 0x00022500 File Offset: 0x00020700
		public event PlayFabEvents.PlayFabResultEvent<CancelAllMatchmakingTicketsForPlayerResult> OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent;

		// Token: 0x140001BA RID: 442
		// (add) Token: 0x06000A3F RID: 2623 RVA: 0x00022538 File Offset: 0x00020738
		// (remove) Token: 0x06000A40 RID: 2624 RVA: 0x00022570 File Offset: 0x00020770
		public event PlayFabEvents.PlayFabRequestEvent<CancelAllServerBackfillTicketsForPlayerRequest> OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent;

		// Token: 0x140001BB RID: 443
		// (add) Token: 0x06000A41 RID: 2625 RVA: 0x000225A8 File Offset: 0x000207A8
		// (remove) Token: 0x06000A42 RID: 2626 RVA: 0x000225E0 File Offset: 0x000207E0
		public event PlayFabEvents.PlayFabResultEvent<CancelAllServerBackfillTicketsForPlayerResult> OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent;

		// Token: 0x140001BC RID: 444
		// (add) Token: 0x06000A43 RID: 2627 RVA: 0x00022618 File Offset: 0x00020818
		// (remove) Token: 0x06000A44 RID: 2628 RVA: 0x00022650 File Offset: 0x00020850
		public event PlayFabEvents.PlayFabRequestEvent<CancelMatchmakingTicketRequest> OnMultiplayerCancelMatchmakingTicketRequestEvent;

		// Token: 0x140001BD RID: 445
		// (add) Token: 0x06000A45 RID: 2629 RVA: 0x00022688 File Offset: 0x00020888
		// (remove) Token: 0x06000A46 RID: 2630 RVA: 0x000226C0 File Offset: 0x000208C0
		public event PlayFabEvents.PlayFabResultEvent<CancelMatchmakingTicketResult> OnMultiplayerCancelMatchmakingTicketResultEvent;

		// Token: 0x140001BE RID: 446
		// (add) Token: 0x06000A47 RID: 2631 RVA: 0x000226F8 File Offset: 0x000208F8
		// (remove) Token: 0x06000A48 RID: 2632 RVA: 0x00022730 File Offset: 0x00020930
		public event PlayFabEvents.PlayFabRequestEvent<CancelServerBackfillTicketRequest> OnMultiplayerCancelServerBackfillTicketRequestEvent;

		// Token: 0x140001BF RID: 447
		// (add) Token: 0x06000A49 RID: 2633 RVA: 0x00022768 File Offset: 0x00020968
		// (remove) Token: 0x06000A4A RID: 2634 RVA: 0x000227A0 File Offset: 0x000209A0
		public event PlayFabEvents.PlayFabResultEvent<CancelServerBackfillTicketResult> OnMultiplayerCancelServerBackfillTicketResultEvent;

		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x06000A4B RID: 2635 RVA: 0x000227D8 File Offset: 0x000209D8
		// (remove) Token: 0x06000A4C RID: 2636 RVA: 0x00022810 File Offset: 0x00020A10
		public event PlayFabEvents.PlayFabRequestEvent<CreateBuildAliasRequest> OnMultiplayerCreateBuildAliasRequestEvent;

		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x06000A4D RID: 2637 RVA: 0x00022848 File Offset: 0x00020A48
		// (remove) Token: 0x06000A4E RID: 2638 RVA: 0x00022880 File Offset: 0x00020A80
		public event PlayFabEvents.PlayFabResultEvent<BuildAliasDetailsResponse> OnMultiplayerCreateBuildAliasResultEvent;

		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x06000A4F RID: 2639 RVA: 0x000228B8 File Offset: 0x00020AB8
		// (remove) Token: 0x06000A50 RID: 2640 RVA: 0x000228F0 File Offset: 0x00020AF0
		public event PlayFabEvents.PlayFabRequestEvent<CreateBuildWithCustomContainerRequest> OnMultiplayerCreateBuildWithCustomContainerRequestEvent;

		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x06000A51 RID: 2641 RVA: 0x00022928 File Offset: 0x00020B28
		// (remove) Token: 0x06000A52 RID: 2642 RVA: 0x00022960 File Offset: 0x00020B60
		public event PlayFabEvents.PlayFabResultEvent<CreateBuildWithCustomContainerResponse> OnMultiplayerCreateBuildWithCustomContainerResultEvent;

		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x06000A53 RID: 2643 RVA: 0x00022998 File Offset: 0x00020B98
		// (remove) Token: 0x06000A54 RID: 2644 RVA: 0x000229D0 File Offset: 0x00020BD0
		public event PlayFabEvents.PlayFabRequestEvent<CreateBuildWithManagedContainerRequest> OnMultiplayerCreateBuildWithManagedContainerRequestEvent;

		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x06000A55 RID: 2645 RVA: 0x00022A08 File Offset: 0x00020C08
		// (remove) Token: 0x06000A56 RID: 2646 RVA: 0x00022A40 File Offset: 0x00020C40
		public event PlayFabEvents.PlayFabResultEvent<CreateBuildWithManagedContainerResponse> OnMultiplayerCreateBuildWithManagedContainerResultEvent;

		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x06000A57 RID: 2647 RVA: 0x00022A78 File Offset: 0x00020C78
		// (remove) Token: 0x06000A58 RID: 2648 RVA: 0x00022AB0 File Offset: 0x00020CB0
		public event PlayFabEvents.PlayFabRequestEvent<CreateBuildWithProcessBasedServerRequest> OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent;

		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x06000A59 RID: 2649 RVA: 0x00022AE8 File Offset: 0x00020CE8
		// (remove) Token: 0x06000A5A RID: 2650 RVA: 0x00022B20 File Offset: 0x00020D20
		public event PlayFabEvents.PlayFabResultEvent<CreateBuildWithProcessBasedServerResponse> OnMultiplayerCreateBuildWithProcessBasedServerResultEvent;

		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x06000A5B RID: 2651 RVA: 0x00022B58 File Offset: 0x00020D58
		// (remove) Token: 0x06000A5C RID: 2652 RVA: 0x00022B90 File Offset: 0x00020D90
		public event PlayFabEvents.PlayFabRequestEvent<CreateMatchmakingTicketRequest> OnMultiplayerCreateMatchmakingTicketRequestEvent;

		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x06000A5D RID: 2653 RVA: 0x00022BC8 File Offset: 0x00020DC8
		// (remove) Token: 0x06000A5E RID: 2654 RVA: 0x00022C00 File Offset: 0x00020E00
		public event PlayFabEvents.PlayFabResultEvent<CreateMatchmakingTicketResult> OnMultiplayerCreateMatchmakingTicketResultEvent;

		// Token: 0x140001CA RID: 458
		// (add) Token: 0x06000A5F RID: 2655 RVA: 0x00022C38 File Offset: 0x00020E38
		// (remove) Token: 0x06000A60 RID: 2656 RVA: 0x00022C70 File Offset: 0x00020E70
		public event PlayFabEvents.PlayFabRequestEvent<CreateRemoteUserRequest> OnMultiplayerCreateRemoteUserRequestEvent;

		// Token: 0x140001CB RID: 459
		// (add) Token: 0x06000A61 RID: 2657 RVA: 0x00022CA8 File Offset: 0x00020EA8
		// (remove) Token: 0x06000A62 RID: 2658 RVA: 0x00022CE0 File Offset: 0x00020EE0
		public event PlayFabEvents.PlayFabResultEvent<CreateRemoteUserResponse> OnMultiplayerCreateRemoteUserResultEvent;

		// Token: 0x140001CC RID: 460
		// (add) Token: 0x06000A63 RID: 2659 RVA: 0x00022D18 File Offset: 0x00020F18
		// (remove) Token: 0x06000A64 RID: 2660 RVA: 0x00022D50 File Offset: 0x00020F50
		public event PlayFabEvents.PlayFabRequestEvent<CreateServerBackfillTicketRequest> OnMultiplayerCreateServerBackfillTicketRequestEvent;

		// Token: 0x140001CD RID: 461
		// (add) Token: 0x06000A65 RID: 2661 RVA: 0x00022D88 File Offset: 0x00020F88
		// (remove) Token: 0x06000A66 RID: 2662 RVA: 0x00022DC0 File Offset: 0x00020FC0
		public event PlayFabEvents.PlayFabResultEvent<CreateServerBackfillTicketResult> OnMultiplayerCreateServerBackfillTicketResultEvent;

		// Token: 0x140001CE RID: 462
		// (add) Token: 0x06000A67 RID: 2663 RVA: 0x00022DF8 File Offset: 0x00020FF8
		// (remove) Token: 0x06000A68 RID: 2664 RVA: 0x00022E30 File Offset: 0x00021030
		public event PlayFabEvents.PlayFabRequestEvent<CreateServerMatchmakingTicketRequest> OnMultiplayerCreateServerMatchmakingTicketRequestEvent;

		// Token: 0x140001CF RID: 463
		// (add) Token: 0x06000A69 RID: 2665 RVA: 0x00022E68 File Offset: 0x00021068
		// (remove) Token: 0x06000A6A RID: 2666 RVA: 0x00022EA0 File Offset: 0x000210A0
		public event PlayFabEvents.PlayFabResultEvent<CreateMatchmakingTicketResult> OnMultiplayerCreateServerMatchmakingTicketResultEvent;

		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x06000A6B RID: 2667 RVA: 0x00022ED8 File Offset: 0x000210D8
		// (remove) Token: 0x06000A6C RID: 2668 RVA: 0x00022F10 File Offset: 0x00021110
		public event PlayFabEvents.PlayFabRequestEvent<DeleteAssetRequest> OnMultiplayerDeleteAssetRequestEvent;

		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x06000A6D RID: 2669 RVA: 0x00022F48 File Offset: 0x00021148
		// (remove) Token: 0x06000A6E RID: 2670 RVA: 0x00022F80 File Offset: 0x00021180
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteAssetResultEvent;

		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x06000A6F RID: 2671 RVA: 0x00022FB8 File Offset: 0x000211B8
		// (remove) Token: 0x06000A70 RID: 2672 RVA: 0x00022FF0 File Offset: 0x000211F0
		public event PlayFabEvents.PlayFabRequestEvent<DeleteBuildRequest> OnMultiplayerDeleteBuildRequestEvent;

		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x06000A71 RID: 2673 RVA: 0x00023028 File Offset: 0x00021228
		// (remove) Token: 0x06000A72 RID: 2674 RVA: 0x00023060 File Offset: 0x00021260
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildResultEvent;

		// Token: 0x140001D4 RID: 468
		// (add) Token: 0x06000A73 RID: 2675 RVA: 0x00023098 File Offset: 0x00021298
		// (remove) Token: 0x06000A74 RID: 2676 RVA: 0x000230D0 File Offset: 0x000212D0
		public event PlayFabEvents.PlayFabRequestEvent<DeleteBuildAliasRequest> OnMultiplayerDeleteBuildAliasRequestEvent;

		// Token: 0x140001D5 RID: 469
		// (add) Token: 0x06000A75 RID: 2677 RVA: 0x00023108 File Offset: 0x00021308
		// (remove) Token: 0x06000A76 RID: 2678 RVA: 0x00023140 File Offset: 0x00021340
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildAliasResultEvent;

		// Token: 0x140001D6 RID: 470
		// (add) Token: 0x06000A77 RID: 2679 RVA: 0x00023178 File Offset: 0x00021378
		// (remove) Token: 0x06000A78 RID: 2680 RVA: 0x000231B0 File Offset: 0x000213B0
		public event PlayFabEvents.PlayFabRequestEvent<DeleteBuildRegionRequest> OnMultiplayerDeleteBuildRegionRequestEvent;

		// Token: 0x140001D7 RID: 471
		// (add) Token: 0x06000A79 RID: 2681 RVA: 0x000231E8 File Offset: 0x000213E8
		// (remove) Token: 0x06000A7A RID: 2682 RVA: 0x00023220 File Offset: 0x00021420
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteBuildRegionResultEvent;

		// Token: 0x140001D8 RID: 472
		// (add) Token: 0x06000A7B RID: 2683 RVA: 0x00023258 File Offset: 0x00021458
		// (remove) Token: 0x06000A7C RID: 2684 RVA: 0x00023290 File Offset: 0x00021490
		public event PlayFabEvents.PlayFabRequestEvent<DeleteCertificateRequest> OnMultiplayerDeleteCertificateRequestEvent;

		// Token: 0x140001D9 RID: 473
		// (add) Token: 0x06000A7D RID: 2685 RVA: 0x000232C8 File Offset: 0x000214C8
		// (remove) Token: 0x06000A7E RID: 2686 RVA: 0x00023300 File Offset: 0x00021500
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteCertificateResultEvent;

		// Token: 0x140001DA RID: 474
		// (add) Token: 0x06000A7F RID: 2687 RVA: 0x00023338 File Offset: 0x00021538
		// (remove) Token: 0x06000A80 RID: 2688 RVA: 0x00023370 File Offset: 0x00021570
		public event PlayFabEvents.PlayFabRequestEvent<DeleteContainerImageRequest> OnMultiplayerDeleteContainerImageRepositoryRequestEvent;

		// Token: 0x140001DB RID: 475
		// (add) Token: 0x06000A81 RID: 2689 RVA: 0x000233A8 File Offset: 0x000215A8
		// (remove) Token: 0x06000A82 RID: 2690 RVA: 0x000233E0 File Offset: 0x000215E0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteContainerImageRepositoryResultEvent;

		// Token: 0x140001DC RID: 476
		// (add) Token: 0x06000A83 RID: 2691 RVA: 0x00023418 File Offset: 0x00021618
		// (remove) Token: 0x06000A84 RID: 2692 RVA: 0x00023450 File Offset: 0x00021650
		public event PlayFabEvents.PlayFabRequestEvent<DeleteRemoteUserRequest> OnMultiplayerDeleteRemoteUserRequestEvent;

		// Token: 0x140001DD RID: 477
		// (add) Token: 0x06000A85 RID: 2693 RVA: 0x00023488 File Offset: 0x00021688
		// (remove) Token: 0x06000A86 RID: 2694 RVA: 0x000234C0 File Offset: 0x000216C0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerDeleteRemoteUserResultEvent;

		// Token: 0x140001DE RID: 478
		// (add) Token: 0x06000A87 RID: 2695 RVA: 0x000234F8 File Offset: 0x000216F8
		// (remove) Token: 0x06000A88 RID: 2696 RVA: 0x00023530 File Offset: 0x00021730
		public event PlayFabEvents.PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest> OnMultiplayerEnableMultiplayerServersForTitleRequestEvent;

		// Token: 0x140001DF RID: 479
		// (add) Token: 0x06000A89 RID: 2697 RVA: 0x00023568 File Offset: 0x00021768
		// (remove) Token: 0x06000A8A RID: 2698 RVA: 0x000235A0 File Offset: 0x000217A0
		public event PlayFabEvents.PlayFabResultEvent<EnableMultiplayerServersForTitleResponse> OnMultiplayerEnableMultiplayerServersForTitleResultEvent;

		// Token: 0x140001E0 RID: 480
		// (add) Token: 0x06000A8B RID: 2699 RVA: 0x000235D8 File Offset: 0x000217D8
		// (remove) Token: 0x06000A8C RID: 2700 RVA: 0x00023610 File Offset: 0x00021810
		public event PlayFabEvents.PlayFabRequestEvent<GetAssetUploadUrlRequest> OnMultiplayerGetAssetUploadUrlRequestEvent;

		// Token: 0x140001E1 RID: 481
		// (add) Token: 0x06000A8D RID: 2701 RVA: 0x00023648 File Offset: 0x00021848
		// (remove) Token: 0x06000A8E RID: 2702 RVA: 0x00023680 File Offset: 0x00021880
		public event PlayFabEvents.PlayFabResultEvent<GetAssetUploadUrlResponse> OnMultiplayerGetAssetUploadUrlResultEvent;

		// Token: 0x140001E2 RID: 482
		// (add) Token: 0x06000A8F RID: 2703 RVA: 0x000236B8 File Offset: 0x000218B8
		// (remove) Token: 0x06000A90 RID: 2704 RVA: 0x000236F0 File Offset: 0x000218F0
		public event PlayFabEvents.PlayFabRequestEvent<GetBuildRequest> OnMultiplayerGetBuildRequestEvent;

		// Token: 0x140001E3 RID: 483
		// (add) Token: 0x06000A91 RID: 2705 RVA: 0x00023728 File Offset: 0x00021928
		// (remove) Token: 0x06000A92 RID: 2706 RVA: 0x00023760 File Offset: 0x00021960
		public event PlayFabEvents.PlayFabResultEvent<GetBuildResponse> OnMultiplayerGetBuildResultEvent;

		// Token: 0x140001E4 RID: 484
		// (add) Token: 0x06000A93 RID: 2707 RVA: 0x00023798 File Offset: 0x00021998
		// (remove) Token: 0x06000A94 RID: 2708 RVA: 0x000237D0 File Offset: 0x000219D0
		public event PlayFabEvents.PlayFabRequestEvent<GetBuildAliasRequest> OnMultiplayerGetBuildAliasRequestEvent;

		// Token: 0x140001E5 RID: 485
		// (add) Token: 0x06000A95 RID: 2709 RVA: 0x00023808 File Offset: 0x00021A08
		// (remove) Token: 0x06000A96 RID: 2710 RVA: 0x00023840 File Offset: 0x00021A40
		public event PlayFabEvents.PlayFabResultEvent<BuildAliasDetailsResponse> OnMultiplayerGetBuildAliasResultEvent;

		// Token: 0x140001E6 RID: 486
		// (add) Token: 0x06000A97 RID: 2711 RVA: 0x00023878 File Offset: 0x00021A78
		// (remove) Token: 0x06000A98 RID: 2712 RVA: 0x000238B0 File Offset: 0x00021AB0
		public event PlayFabEvents.PlayFabRequestEvent<GetContainerRegistryCredentialsRequest> OnMultiplayerGetContainerRegistryCredentialsRequestEvent;

		// Token: 0x140001E7 RID: 487
		// (add) Token: 0x06000A99 RID: 2713 RVA: 0x000238E8 File Offset: 0x00021AE8
		// (remove) Token: 0x06000A9A RID: 2714 RVA: 0x00023920 File Offset: 0x00021B20
		public event PlayFabEvents.PlayFabResultEvent<GetContainerRegistryCredentialsResponse> OnMultiplayerGetContainerRegistryCredentialsResultEvent;

		// Token: 0x140001E8 RID: 488
		// (add) Token: 0x06000A9B RID: 2715 RVA: 0x00023958 File Offset: 0x00021B58
		// (remove) Token: 0x06000A9C RID: 2716 RVA: 0x00023990 File Offset: 0x00021B90
		public event PlayFabEvents.PlayFabRequestEvent<GetMatchRequest> OnMultiplayerGetMatchRequestEvent;

		// Token: 0x140001E9 RID: 489
		// (add) Token: 0x06000A9D RID: 2717 RVA: 0x000239C8 File Offset: 0x00021BC8
		// (remove) Token: 0x06000A9E RID: 2718 RVA: 0x00023A00 File Offset: 0x00021C00
		public event PlayFabEvents.PlayFabResultEvent<GetMatchResult> OnMultiplayerGetMatchResultEvent;

		// Token: 0x140001EA RID: 490
		// (add) Token: 0x06000A9F RID: 2719 RVA: 0x00023A38 File Offset: 0x00021C38
		// (remove) Token: 0x06000AA0 RID: 2720 RVA: 0x00023A70 File Offset: 0x00021C70
		public event PlayFabEvents.PlayFabRequestEvent<GetMatchmakingQueueRequest> OnMultiplayerGetMatchmakingQueueRequestEvent;

		// Token: 0x140001EB RID: 491
		// (add) Token: 0x06000AA1 RID: 2721 RVA: 0x00023AA8 File Offset: 0x00021CA8
		// (remove) Token: 0x06000AA2 RID: 2722 RVA: 0x00023AE0 File Offset: 0x00021CE0
		public event PlayFabEvents.PlayFabResultEvent<GetMatchmakingQueueResult> OnMultiplayerGetMatchmakingQueueResultEvent;

		// Token: 0x140001EC RID: 492
		// (add) Token: 0x06000AA3 RID: 2723 RVA: 0x00023B18 File Offset: 0x00021D18
		// (remove) Token: 0x06000AA4 RID: 2724 RVA: 0x00023B50 File Offset: 0x00021D50
		public event PlayFabEvents.PlayFabRequestEvent<GetMatchmakingTicketRequest> OnMultiplayerGetMatchmakingTicketRequestEvent;

		// Token: 0x140001ED RID: 493
		// (add) Token: 0x06000AA5 RID: 2725 RVA: 0x00023B88 File Offset: 0x00021D88
		// (remove) Token: 0x06000AA6 RID: 2726 RVA: 0x00023BC0 File Offset: 0x00021DC0
		public event PlayFabEvents.PlayFabResultEvent<GetMatchmakingTicketResult> OnMultiplayerGetMatchmakingTicketResultEvent;

		// Token: 0x140001EE RID: 494
		// (add) Token: 0x06000AA7 RID: 2727 RVA: 0x00023BF8 File Offset: 0x00021DF8
		// (remove) Token: 0x06000AA8 RID: 2728 RVA: 0x00023C30 File Offset: 0x00021E30
		public event PlayFabEvents.PlayFabRequestEvent<GetMultiplayerServerDetailsRequest> OnMultiplayerGetMultiplayerServerDetailsRequestEvent;

		// Token: 0x140001EF RID: 495
		// (add) Token: 0x06000AA9 RID: 2729 RVA: 0x00023C68 File Offset: 0x00021E68
		// (remove) Token: 0x06000AAA RID: 2730 RVA: 0x00023CA0 File Offset: 0x00021EA0
		public event PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerDetailsResponse> OnMultiplayerGetMultiplayerServerDetailsResultEvent;

		// Token: 0x140001F0 RID: 496
		// (add) Token: 0x06000AAB RID: 2731 RVA: 0x00023CD8 File Offset: 0x00021ED8
		// (remove) Token: 0x06000AAC RID: 2732 RVA: 0x00023D10 File Offset: 0x00021F10
		public event PlayFabEvents.PlayFabRequestEvent<GetMultiplayerServerLogsRequest> OnMultiplayerGetMultiplayerServerLogsRequestEvent;

		// Token: 0x140001F1 RID: 497
		// (add) Token: 0x06000AAD RID: 2733 RVA: 0x00023D48 File Offset: 0x00021F48
		// (remove) Token: 0x06000AAE RID: 2734 RVA: 0x00023D80 File Offset: 0x00021F80
		public event PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerLogsResponse> OnMultiplayerGetMultiplayerServerLogsResultEvent;

		// Token: 0x140001F2 RID: 498
		// (add) Token: 0x06000AAF RID: 2735 RVA: 0x00023DB8 File Offset: 0x00021FB8
		// (remove) Token: 0x06000AB0 RID: 2736 RVA: 0x00023DF0 File Offset: 0x00021FF0
		public event PlayFabEvents.PlayFabRequestEvent<GetMultiplayerSessionLogsBySessionIdRequest> OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent;

		// Token: 0x140001F3 RID: 499
		// (add) Token: 0x06000AB1 RID: 2737 RVA: 0x00023E28 File Offset: 0x00022028
		// (remove) Token: 0x06000AB2 RID: 2738 RVA: 0x00023E60 File Offset: 0x00022060
		public event PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerLogsResponse> OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent;

		// Token: 0x140001F4 RID: 500
		// (add) Token: 0x06000AB3 RID: 2739 RVA: 0x00023E98 File Offset: 0x00022098
		// (remove) Token: 0x06000AB4 RID: 2740 RVA: 0x00023ED0 File Offset: 0x000220D0
		public event PlayFabEvents.PlayFabRequestEvent<GetQueueStatisticsRequest> OnMultiplayerGetQueueStatisticsRequestEvent;

		// Token: 0x140001F5 RID: 501
		// (add) Token: 0x06000AB5 RID: 2741 RVA: 0x00023F08 File Offset: 0x00022108
		// (remove) Token: 0x06000AB6 RID: 2742 RVA: 0x00023F40 File Offset: 0x00022140
		public event PlayFabEvents.PlayFabResultEvent<GetQueueStatisticsResult> OnMultiplayerGetQueueStatisticsResultEvent;

		// Token: 0x140001F6 RID: 502
		// (add) Token: 0x06000AB7 RID: 2743 RVA: 0x00023F78 File Offset: 0x00022178
		// (remove) Token: 0x06000AB8 RID: 2744 RVA: 0x00023FB0 File Offset: 0x000221B0
		public event PlayFabEvents.PlayFabRequestEvent<GetRemoteLoginEndpointRequest> OnMultiplayerGetRemoteLoginEndpointRequestEvent;

		// Token: 0x140001F7 RID: 503
		// (add) Token: 0x06000AB9 RID: 2745 RVA: 0x00023FE8 File Offset: 0x000221E8
		// (remove) Token: 0x06000ABA RID: 2746 RVA: 0x00024020 File Offset: 0x00022220
		public event PlayFabEvents.PlayFabResultEvent<GetRemoteLoginEndpointResponse> OnMultiplayerGetRemoteLoginEndpointResultEvent;

		// Token: 0x140001F8 RID: 504
		// (add) Token: 0x06000ABB RID: 2747 RVA: 0x00024058 File Offset: 0x00022258
		// (remove) Token: 0x06000ABC RID: 2748 RVA: 0x00024090 File Offset: 0x00022290
		public event PlayFabEvents.PlayFabRequestEvent<GetServerBackfillTicketRequest> OnMultiplayerGetServerBackfillTicketRequestEvent;

		// Token: 0x140001F9 RID: 505
		// (add) Token: 0x06000ABD RID: 2749 RVA: 0x000240C8 File Offset: 0x000222C8
		// (remove) Token: 0x06000ABE RID: 2750 RVA: 0x00024100 File Offset: 0x00022300
		public event PlayFabEvents.PlayFabResultEvent<GetServerBackfillTicketResult> OnMultiplayerGetServerBackfillTicketResultEvent;

		// Token: 0x140001FA RID: 506
		// (add) Token: 0x06000ABF RID: 2751 RVA: 0x00024138 File Offset: 0x00022338
		// (remove) Token: 0x06000AC0 RID: 2752 RVA: 0x00024170 File Offset: 0x00022370
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest> OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent;

		// Token: 0x140001FB RID: 507
		// (add) Token: 0x06000AC1 RID: 2753 RVA: 0x000241A8 File Offset: 0x000223A8
		// (remove) Token: 0x06000AC2 RID: 2754 RVA: 0x000241E0 File Offset: 0x000223E0
		public event PlayFabEvents.PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse> OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent;

		// Token: 0x140001FC RID: 508
		// (add) Token: 0x06000AC3 RID: 2755 RVA: 0x00024218 File Offset: 0x00022418
		// (remove) Token: 0x06000AC4 RID: 2756 RVA: 0x00024250 File Offset: 0x00022450
		public event PlayFabEvents.PlayFabRequestEvent<GetTitleMultiplayerServersQuotasRequest> OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent;

		// Token: 0x140001FD RID: 509
		// (add) Token: 0x06000AC5 RID: 2757 RVA: 0x00024288 File Offset: 0x00022488
		// (remove) Token: 0x06000AC6 RID: 2758 RVA: 0x000242C0 File Offset: 0x000224C0
		public event PlayFabEvents.PlayFabResultEvent<GetTitleMultiplayerServersQuotasResponse> OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent;

		// Token: 0x140001FE RID: 510
		// (add) Token: 0x06000AC7 RID: 2759 RVA: 0x000242F8 File Offset: 0x000224F8
		// (remove) Token: 0x06000AC8 RID: 2760 RVA: 0x00024330 File Offset: 0x00022530
		public event PlayFabEvents.PlayFabRequestEvent<JoinMatchmakingTicketRequest> OnMultiplayerJoinMatchmakingTicketRequestEvent;

		// Token: 0x140001FF RID: 511
		// (add) Token: 0x06000AC9 RID: 2761 RVA: 0x00024368 File Offset: 0x00022568
		// (remove) Token: 0x06000ACA RID: 2762 RVA: 0x000243A0 File Offset: 0x000225A0
		public event PlayFabEvents.PlayFabResultEvent<JoinMatchmakingTicketResult> OnMultiplayerJoinMatchmakingTicketResultEvent;

		// Token: 0x14000200 RID: 512
		// (add) Token: 0x06000ACB RID: 2763 RVA: 0x000243D8 File Offset: 0x000225D8
		// (remove) Token: 0x06000ACC RID: 2764 RVA: 0x00024410 File Offset: 0x00022610
		public event PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest> OnMultiplayerListArchivedMultiplayerServersRequestEvent;

		// Token: 0x14000201 RID: 513
		// (add) Token: 0x06000ACD RID: 2765 RVA: 0x00024448 File Offset: 0x00022648
		// (remove) Token: 0x06000ACE RID: 2766 RVA: 0x00024480 File Offset: 0x00022680
		public event PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse> OnMultiplayerListArchivedMultiplayerServersResultEvent;

		// Token: 0x14000202 RID: 514
		// (add) Token: 0x06000ACF RID: 2767 RVA: 0x000244B8 File Offset: 0x000226B8
		// (remove) Token: 0x06000AD0 RID: 2768 RVA: 0x000244F0 File Offset: 0x000226F0
		public event PlayFabEvents.PlayFabRequestEvent<ListAssetSummariesRequest> OnMultiplayerListAssetSummariesRequestEvent;

		// Token: 0x14000203 RID: 515
		// (add) Token: 0x06000AD1 RID: 2769 RVA: 0x00024528 File Offset: 0x00022728
		// (remove) Token: 0x06000AD2 RID: 2770 RVA: 0x00024560 File Offset: 0x00022760
		public event PlayFabEvents.PlayFabResultEvent<ListAssetSummariesResponse> OnMultiplayerListAssetSummariesResultEvent;

		// Token: 0x14000204 RID: 516
		// (add) Token: 0x06000AD3 RID: 2771 RVA: 0x00024598 File Offset: 0x00022798
		// (remove) Token: 0x06000AD4 RID: 2772 RVA: 0x000245D0 File Offset: 0x000227D0
		public event PlayFabEvents.PlayFabRequestEvent<MultiplayerEmptyRequest> OnMultiplayerListBuildAliasesRequestEvent;

		// Token: 0x14000205 RID: 517
		// (add) Token: 0x06000AD5 RID: 2773 RVA: 0x00024608 File Offset: 0x00022808
		// (remove) Token: 0x06000AD6 RID: 2774 RVA: 0x00024640 File Offset: 0x00022840
		public event PlayFabEvents.PlayFabResultEvent<ListBuildAliasesForTitleResponse> OnMultiplayerListBuildAliasesResultEvent;

		// Token: 0x14000206 RID: 518
		// (add) Token: 0x06000AD7 RID: 2775 RVA: 0x00024678 File Offset: 0x00022878
		// (remove) Token: 0x06000AD8 RID: 2776 RVA: 0x000246B0 File Offset: 0x000228B0
		public event PlayFabEvents.PlayFabRequestEvent<ListBuildSummariesRequest> OnMultiplayerListBuildSummariesRequestEvent;

		// Token: 0x14000207 RID: 519
		// (add) Token: 0x06000AD9 RID: 2777 RVA: 0x000246E8 File Offset: 0x000228E8
		// (remove) Token: 0x06000ADA RID: 2778 RVA: 0x00024720 File Offset: 0x00022920
		public event PlayFabEvents.PlayFabResultEvent<ListBuildSummariesResponse> OnMultiplayerListBuildSummariesResultEvent;

		// Token: 0x14000208 RID: 520
		// (add) Token: 0x06000ADB RID: 2779 RVA: 0x00024758 File Offset: 0x00022958
		// (remove) Token: 0x06000ADC RID: 2780 RVA: 0x00024790 File Offset: 0x00022990
		public event PlayFabEvents.PlayFabRequestEvent<ListCertificateSummariesRequest> OnMultiplayerListCertificateSummariesRequestEvent;

		// Token: 0x14000209 RID: 521
		// (add) Token: 0x06000ADD RID: 2781 RVA: 0x000247C8 File Offset: 0x000229C8
		// (remove) Token: 0x06000ADE RID: 2782 RVA: 0x00024800 File Offset: 0x00022A00
		public event PlayFabEvents.PlayFabResultEvent<ListCertificateSummariesResponse> OnMultiplayerListCertificateSummariesResultEvent;

		// Token: 0x1400020A RID: 522
		// (add) Token: 0x06000ADF RID: 2783 RVA: 0x00024838 File Offset: 0x00022A38
		// (remove) Token: 0x06000AE0 RID: 2784 RVA: 0x00024870 File Offset: 0x00022A70
		public event PlayFabEvents.PlayFabRequestEvent<ListContainerImagesRequest> OnMultiplayerListContainerImagesRequestEvent;

		// Token: 0x1400020B RID: 523
		// (add) Token: 0x06000AE1 RID: 2785 RVA: 0x000248A8 File Offset: 0x00022AA8
		// (remove) Token: 0x06000AE2 RID: 2786 RVA: 0x000248E0 File Offset: 0x00022AE0
		public event PlayFabEvents.PlayFabResultEvent<ListContainerImagesResponse> OnMultiplayerListContainerImagesResultEvent;

		// Token: 0x1400020C RID: 524
		// (add) Token: 0x06000AE3 RID: 2787 RVA: 0x00024918 File Offset: 0x00022B18
		// (remove) Token: 0x06000AE4 RID: 2788 RVA: 0x00024950 File Offset: 0x00022B50
		public event PlayFabEvents.PlayFabRequestEvent<ListContainerImageTagsRequest> OnMultiplayerListContainerImageTagsRequestEvent;

		// Token: 0x1400020D RID: 525
		// (add) Token: 0x06000AE5 RID: 2789 RVA: 0x00024988 File Offset: 0x00022B88
		// (remove) Token: 0x06000AE6 RID: 2790 RVA: 0x000249C0 File Offset: 0x00022BC0
		public event PlayFabEvents.PlayFabResultEvent<ListContainerImageTagsResponse> OnMultiplayerListContainerImageTagsResultEvent;

		// Token: 0x1400020E RID: 526
		// (add) Token: 0x06000AE7 RID: 2791 RVA: 0x000249F8 File Offset: 0x00022BF8
		// (remove) Token: 0x06000AE8 RID: 2792 RVA: 0x00024A30 File Offset: 0x00022C30
		public event PlayFabEvents.PlayFabRequestEvent<ListMatchmakingQueuesRequest> OnMultiplayerListMatchmakingQueuesRequestEvent;

		// Token: 0x1400020F RID: 527
		// (add) Token: 0x06000AE9 RID: 2793 RVA: 0x00024A68 File Offset: 0x00022C68
		// (remove) Token: 0x06000AEA RID: 2794 RVA: 0x00024AA0 File Offset: 0x00022CA0
		public event PlayFabEvents.PlayFabResultEvent<ListMatchmakingQueuesResult> OnMultiplayerListMatchmakingQueuesResultEvent;

		// Token: 0x14000210 RID: 528
		// (add) Token: 0x06000AEB RID: 2795 RVA: 0x00024AD8 File Offset: 0x00022CD8
		// (remove) Token: 0x06000AEC RID: 2796 RVA: 0x00024B10 File Offset: 0x00022D10
		public event PlayFabEvents.PlayFabRequestEvent<ListMatchmakingTicketsForPlayerRequest> OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent;

		// Token: 0x14000211 RID: 529
		// (add) Token: 0x06000AED RID: 2797 RVA: 0x00024B48 File Offset: 0x00022D48
		// (remove) Token: 0x06000AEE RID: 2798 RVA: 0x00024B80 File Offset: 0x00022D80
		public event PlayFabEvents.PlayFabResultEvent<ListMatchmakingTicketsForPlayerResult> OnMultiplayerListMatchmakingTicketsForPlayerResultEvent;

		// Token: 0x14000212 RID: 530
		// (add) Token: 0x06000AEF RID: 2799 RVA: 0x00024BB8 File Offset: 0x00022DB8
		// (remove) Token: 0x06000AF0 RID: 2800 RVA: 0x00024BF0 File Offset: 0x00022DF0
		public event PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest> OnMultiplayerListMultiplayerServersRequestEvent;

		// Token: 0x14000213 RID: 531
		// (add) Token: 0x06000AF1 RID: 2801 RVA: 0x00024C28 File Offset: 0x00022E28
		// (remove) Token: 0x06000AF2 RID: 2802 RVA: 0x00024C60 File Offset: 0x00022E60
		public event PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse> OnMultiplayerListMultiplayerServersResultEvent;

		// Token: 0x14000214 RID: 532
		// (add) Token: 0x06000AF3 RID: 2803 RVA: 0x00024C98 File Offset: 0x00022E98
		// (remove) Token: 0x06000AF4 RID: 2804 RVA: 0x00024CD0 File Offset: 0x00022ED0
		public event PlayFabEvents.PlayFabRequestEvent<ListPartyQosServersRequest> OnMultiplayerListPartyQosServersRequestEvent;

		// Token: 0x14000215 RID: 533
		// (add) Token: 0x06000AF5 RID: 2805 RVA: 0x00024D08 File Offset: 0x00022F08
		// (remove) Token: 0x06000AF6 RID: 2806 RVA: 0x00024D40 File Offset: 0x00022F40
		public event PlayFabEvents.PlayFabResultEvent<ListPartyQosServersResponse> OnMultiplayerListPartyQosServersResultEvent;

		// Token: 0x14000216 RID: 534
		// (add) Token: 0x06000AF7 RID: 2807 RVA: 0x00024D78 File Offset: 0x00022F78
		// (remove) Token: 0x06000AF8 RID: 2808 RVA: 0x00024DB0 File Offset: 0x00022FB0
		public event PlayFabEvents.PlayFabRequestEvent<ListQosServersRequest> OnMultiplayerListQosServersRequestEvent;

		// Token: 0x14000217 RID: 535
		// (add) Token: 0x06000AF9 RID: 2809 RVA: 0x00024DE8 File Offset: 0x00022FE8
		// (remove) Token: 0x06000AFA RID: 2810 RVA: 0x00024E20 File Offset: 0x00023020
		public event PlayFabEvents.PlayFabResultEvent<ListQosServersResponse> OnMultiplayerListQosServersResultEvent;

		// Token: 0x14000218 RID: 536
		// (add) Token: 0x06000AFB RID: 2811 RVA: 0x00024E58 File Offset: 0x00023058
		// (remove) Token: 0x06000AFC RID: 2812 RVA: 0x00024E90 File Offset: 0x00023090
		public event PlayFabEvents.PlayFabRequestEvent<ListQosServersForTitleRequest> OnMultiplayerListQosServersForTitleRequestEvent;

		// Token: 0x14000219 RID: 537
		// (add) Token: 0x06000AFD RID: 2813 RVA: 0x00024EC8 File Offset: 0x000230C8
		// (remove) Token: 0x06000AFE RID: 2814 RVA: 0x00024F00 File Offset: 0x00023100
		public event PlayFabEvents.PlayFabResultEvent<ListQosServersForTitleResponse> OnMultiplayerListQosServersForTitleResultEvent;

		// Token: 0x1400021A RID: 538
		// (add) Token: 0x06000AFF RID: 2815 RVA: 0x00024F38 File Offset: 0x00023138
		// (remove) Token: 0x06000B00 RID: 2816 RVA: 0x00024F70 File Offset: 0x00023170
		public event PlayFabEvents.PlayFabRequestEvent<ListServerBackfillTicketsForPlayerRequest> OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent;

		// Token: 0x1400021B RID: 539
		// (add) Token: 0x06000B01 RID: 2817 RVA: 0x00024FA8 File Offset: 0x000231A8
		// (remove) Token: 0x06000B02 RID: 2818 RVA: 0x00024FE0 File Offset: 0x000231E0
		public event PlayFabEvents.PlayFabResultEvent<ListServerBackfillTicketsForPlayerResult> OnMultiplayerListServerBackfillTicketsForPlayerResultEvent;

		// Token: 0x1400021C RID: 540
		// (add) Token: 0x06000B03 RID: 2819 RVA: 0x00025018 File Offset: 0x00023218
		// (remove) Token: 0x06000B04 RID: 2820 RVA: 0x00025050 File Offset: 0x00023250
		public event PlayFabEvents.PlayFabRequestEvent<ListVirtualMachineSummariesRequest> OnMultiplayerListVirtualMachineSummariesRequestEvent;

		// Token: 0x1400021D RID: 541
		// (add) Token: 0x06000B05 RID: 2821 RVA: 0x00025088 File Offset: 0x00023288
		// (remove) Token: 0x06000B06 RID: 2822 RVA: 0x000250C0 File Offset: 0x000232C0
		public event PlayFabEvents.PlayFabResultEvent<ListVirtualMachineSummariesResponse> OnMultiplayerListVirtualMachineSummariesResultEvent;

		// Token: 0x1400021E RID: 542
		// (add) Token: 0x06000B07 RID: 2823 RVA: 0x000250F8 File Offset: 0x000232F8
		// (remove) Token: 0x06000B08 RID: 2824 RVA: 0x00025130 File Offset: 0x00023330
		public event PlayFabEvents.PlayFabRequestEvent<RemoveMatchmakingQueueRequest> OnMultiplayerRemoveMatchmakingQueueRequestEvent;

		// Token: 0x1400021F RID: 543
		// (add) Token: 0x06000B09 RID: 2825 RVA: 0x00025168 File Offset: 0x00023368
		// (remove) Token: 0x06000B0A RID: 2826 RVA: 0x000251A0 File Offset: 0x000233A0
		public event PlayFabEvents.PlayFabResultEvent<RemoveMatchmakingQueueResult> OnMultiplayerRemoveMatchmakingQueueResultEvent;

		// Token: 0x14000220 RID: 544
		// (add) Token: 0x06000B0B RID: 2827 RVA: 0x000251D8 File Offset: 0x000233D8
		// (remove) Token: 0x06000B0C RID: 2828 RVA: 0x00025210 File Offset: 0x00023410
		public event PlayFabEvents.PlayFabRequestEvent<RequestMultiplayerServerRequest> OnMultiplayerRequestMultiplayerServerRequestEvent;

		// Token: 0x14000221 RID: 545
		// (add) Token: 0x06000B0D RID: 2829 RVA: 0x00025248 File Offset: 0x00023448
		// (remove) Token: 0x06000B0E RID: 2830 RVA: 0x00025280 File Offset: 0x00023480
		public event PlayFabEvents.PlayFabResultEvent<RequestMultiplayerServerResponse> OnMultiplayerRequestMultiplayerServerResultEvent;

		// Token: 0x14000222 RID: 546
		// (add) Token: 0x06000B0F RID: 2831 RVA: 0x000252B8 File Offset: 0x000234B8
		// (remove) Token: 0x06000B10 RID: 2832 RVA: 0x000252F0 File Offset: 0x000234F0
		public event PlayFabEvents.PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest> OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent;

		// Token: 0x14000223 RID: 547
		// (add) Token: 0x06000B11 RID: 2833 RVA: 0x00025328 File Offset: 0x00023528
		// (remove) Token: 0x06000B12 RID: 2834 RVA: 0x00025360 File Offset: 0x00023560
		public event PlayFabEvents.PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse> OnMultiplayerRolloverContainerRegistryCredentialsResultEvent;

		// Token: 0x14000224 RID: 548
		// (add) Token: 0x06000B13 RID: 2835 RVA: 0x00025398 File Offset: 0x00023598
		// (remove) Token: 0x06000B14 RID: 2836 RVA: 0x000253D0 File Offset: 0x000235D0
		public event PlayFabEvents.PlayFabRequestEvent<SetMatchmakingQueueRequest> OnMultiplayerSetMatchmakingQueueRequestEvent;

		// Token: 0x14000225 RID: 549
		// (add) Token: 0x06000B15 RID: 2837 RVA: 0x00025408 File Offset: 0x00023608
		// (remove) Token: 0x06000B16 RID: 2838 RVA: 0x00025440 File Offset: 0x00023640
		public event PlayFabEvents.PlayFabResultEvent<SetMatchmakingQueueResult> OnMultiplayerSetMatchmakingQueueResultEvent;

		// Token: 0x14000226 RID: 550
		// (add) Token: 0x06000B17 RID: 2839 RVA: 0x00025478 File Offset: 0x00023678
		// (remove) Token: 0x06000B18 RID: 2840 RVA: 0x000254B0 File Offset: 0x000236B0
		public event PlayFabEvents.PlayFabRequestEvent<ShutdownMultiplayerServerRequest> OnMultiplayerShutdownMultiplayerServerRequestEvent;

		// Token: 0x14000227 RID: 551
		// (add) Token: 0x06000B19 RID: 2841 RVA: 0x000254E8 File Offset: 0x000236E8
		// (remove) Token: 0x06000B1A RID: 2842 RVA: 0x00025520 File Offset: 0x00023720
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerShutdownMultiplayerServerResultEvent;

		// Token: 0x14000228 RID: 552
		// (add) Token: 0x06000B1B RID: 2843 RVA: 0x00025558 File Offset: 0x00023758
		// (remove) Token: 0x06000B1C RID: 2844 RVA: 0x00025590 File Offset: 0x00023790
		public event PlayFabEvents.PlayFabRequestEvent<UntagContainerImageRequest> OnMultiplayerUntagContainerImageRequestEvent;

		// Token: 0x14000229 RID: 553
		// (add) Token: 0x06000B1D RID: 2845 RVA: 0x000255C8 File Offset: 0x000237C8
		// (remove) Token: 0x06000B1E RID: 2846 RVA: 0x00025600 File Offset: 0x00023800
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUntagContainerImageResultEvent;

		// Token: 0x1400022A RID: 554
		// (add) Token: 0x06000B1F RID: 2847 RVA: 0x00025638 File Offset: 0x00023838
		// (remove) Token: 0x06000B20 RID: 2848 RVA: 0x00025670 File Offset: 0x00023870
		public event PlayFabEvents.PlayFabRequestEvent<UpdateBuildAliasRequest> OnMultiplayerUpdateBuildAliasRequestEvent;

		// Token: 0x1400022B RID: 555
		// (add) Token: 0x06000B21 RID: 2849 RVA: 0x000256A8 File Offset: 0x000238A8
		// (remove) Token: 0x06000B22 RID: 2850 RVA: 0x000256E0 File Offset: 0x000238E0
		public event PlayFabEvents.PlayFabResultEvent<BuildAliasDetailsResponse> OnMultiplayerUpdateBuildAliasResultEvent;

		// Token: 0x1400022C RID: 556
		// (add) Token: 0x06000B23 RID: 2851 RVA: 0x00025718 File Offset: 0x00023918
		// (remove) Token: 0x06000B24 RID: 2852 RVA: 0x00025750 File Offset: 0x00023950
		public event PlayFabEvents.PlayFabRequestEvent<UpdateBuildRegionRequest> OnMultiplayerUpdateBuildRegionRequestEvent;

		// Token: 0x1400022D RID: 557
		// (add) Token: 0x06000B25 RID: 2853 RVA: 0x00025788 File Offset: 0x00023988
		// (remove) Token: 0x06000B26 RID: 2854 RVA: 0x000257C0 File Offset: 0x000239C0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUpdateBuildRegionResultEvent;

		// Token: 0x1400022E RID: 558
		// (add) Token: 0x06000B27 RID: 2855 RVA: 0x000257F8 File Offset: 0x000239F8
		// (remove) Token: 0x06000B28 RID: 2856 RVA: 0x00025830 File Offset: 0x00023A30
		public event PlayFabEvents.PlayFabRequestEvent<UpdateBuildRegionsRequest> OnMultiplayerUpdateBuildRegionsRequestEvent;

		// Token: 0x1400022F RID: 559
		// (add) Token: 0x06000B29 RID: 2857 RVA: 0x00025868 File Offset: 0x00023A68
		// (remove) Token: 0x06000B2A RID: 2858 RVA: 0x000258A0 File Offset: 0x00023AA0
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUpdateBuildRegionsResultEvent;

		// Token: 0x14000230 RID: 560
		// (add) Token: 0x06000B2B RID: 2859 RVA: 0x000258D8 File Offset: 0x00023AD8
		// (remove) Token: 0x06000B2C RID: 2860 RVA: 0x00025910 File Offset: 0x00023B10
		public event PlayFabEvents.PlayFabRequestEvent<UploadCertificateRequest> OnMultiplayerUploadCertificateRequestEvent;

		// Token: 0x14000231 RID: 561
		// (add) Token: 0x06000B2D RID: 2861 RVA: 0x00025948 File Offset: 0x00023B48
		// (remove) Token: 0x06000B2E RID: 2862 RVA: 0x00025980 File Offset: 0x00023B80
		public event PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse> OnMultiplayerUploadCertificateResultEvent;

		// Token: 0x14000232 RID: 562
		// (add) Token: 0x06000B2F RID: 2863 RVA: 0x000259B8 File Offset: 0x00023BB8
		// (remove) Token: 0x06000B30 RID: 2864 RVA: 0x000259F0 File Offset: 0x00023BF0
		public event PlayFabEvents.PlayFabRequestEvent<GetGlobalPolicyRequest> OnProfilesGetGlobalPolicyRequestEvent;

		// Token: 0x14000233 RID: 563
		// (add) Token: 0x06000B31 RID: 2865 RVA: 0x00025A28 File Offset: 0x00023C28
		// (remove) Token: 0x06000B32 RID: 2866 RVA: 0x00025A60 File Offset: 0x00023C60
		public event PlayFabEvents.PlayFabResultEvent<GetGlobalPolicyResponse> OnProfilesGetGlobalPolicyResultEvent;

		// Token: 0x14000234 RID: 564
		// (add) Token: 0x06000B33 RID: 2867 RVA: 0x00025A98 File Offset: 0x00023C98
		// (remove) Token: 0x06000B34 RID: 2868 RVA: 0x00025AD0 File Offset: 0x00023CD0
		public event PlayFabEvents.PlayFabRequestEvent<GetEntityProfileRequest> OnProfilesGetProfileRequestEvent;

		// Token: 0x14000235 RID: 565
		// (add) Token: 0x06000B35 RID: 2869 RVA: 0x00025B08 File Offset: 0x00023D08
		// (remove) Token: 0x06000B36 RID: 2870 RVA: 0x00025B40 File Offset: 0x00023D40
		public event PlayFabEvents.PlayFabResultEvent<GetEntityProfileResponse> OnProfilesGetProfileResultEvent;

		// Token: 0x14000236 RID: 566
		// (add) Token: 0x06000B37 RID: 2871 RVA: 0x00025B78 File Offset: 0x00023D78
		// (remove) Token: 0x06000B38 RID: 2872 RVA: 0x00025BB0 File Offset: 0x00023DB0
		public event PlayFabEvents.PlayFabRequestEvent<GetEntityProfilesRequest> OnProfilesGetProfilesRequestEvent;

		// Token: 0x14000237 RID: 567
		// (add) Token: 0x06000B39 RID: 2873 RVA: 0x00025BE8 File Offset: 0x00023DE8
		// (remove) Token: 0x06000B3A RID: 2874 RVA: 0x00025C20 File Offset: 0x00023E20
		public event PlayFabEvents.PlayFabResultEvent<GetEntityProfilesResponse> OnProfilesGetProfilesResultEvent;

		// Token: 0x14000238 RID: 568
		// (add) Token: 0x06000B3B RID: 2875 RVA: 0x00025C58 File Offset: 0x00023E58
		// (remove) Token: 0x06000B3C RID: 2876 RVA: 0x00025C90 File Offset: 0x00023E90
		public event PlayFabEvents.PlayFabRequestEvent<GetTitlePlayersFromMasterPlayerAccountIdsRequest> OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent;

		// Token: 0x14000239 RID: 569
		// (add) Token: 0x06000B3D RID: 2877 RVA: 0x00025CC8 File Offset: 0x00023EC8
		// (remove) Token: 0x06000B3E RID: 2878 RVA: 0x00025D00 File Offset: 0x00023F00
		public event PlayFabEvents.PlayFabResultEvent<GetTitlePlayersFromMasterPlayerAccountIdsResponse> OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent;

		// Token: 0x1400023A RID: 570
		// (add) Token: 0x06000B3F RID: 2879 RVA: 0x00025D38 File Offset: 0x00023F38
		// (remove) Token: 0x06000B40 RID: 2880 RVA: 0x00025D70 File Offset: 0x00023F70
		public event PlayFabEvents.PlayFabRequestEvent<SetGlobalPolicyRequest> OnProfilesSetGlobalPolicyRequestEvent;

		// Token: 0x1400023B RID: 571
		// (add) Token: 0x06000B41 RID: 2881 RVA: 0x00025DA8 File Offset: 0x00023FA8
		// (remove) Token: 0x06000B42 RID: 2882 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public event PlayFabEvents.PlayFabResultEvent<SetGlobalPolicyResponse> OnProfilesSetGlobalPolicyResultEvent;

		// Token: 0x1400023C RID: 572
		// (add) Token: 0x06000B43 RID: 2883 RVA: 0x00025E18 File Offset: 0x00024018
		// (remove) Token: 0x06000B44 RID: 2884 RVA: 0x00025E50 File Offset: 0x00024050
		public event PlayFabEvents.PlayFabRequestEvent<SetProfileLanguageRequest> OnProfilesSetProfileLanguageRequestEvent;

		// Token: 0x1400023D RID: 573
		// (add) Token: 0x06000B45 RID: 2885 RVA: 0x00025E88 File Offset: 0x00024088
		// (remove) Token: 0x06000B46 RID: 2886 RVA: 0x00025EC0 File Offset: 0x000240C0
		public event PlayFabEvents.PlayFabResultEvent<SetProfileLanguageResponse> OnProfilesSetProfileLanguageResultEvent;

		// Token: 0x1400023E RID: 574
		// (add) Token: 0x06000B47 RID: 2887 RVA: 0x00025EF8 File Offset: 0x000240F8
		// (remove) Token: 0x06000B48 RID: 2888 RVA: 0x00025F30 File Offset: 0x00024130
		public event PlayFabEvents.PlayFabRequestEvent<SetEntityProfilePolicyRequest> OnProfilesSetProfilePolicyRequestEvent;

		// Token: 0x1400023F RID: 575
		// (add) Token: 0x06000B49 RID: 2889 RVA: 0x00025F68 File Offset: 0x00024168
		// (remove) Token: 0x06000B4A RID: 2890 RVA: 0x00025FA0 File Offset: 0x000241A0
		public event PlayFabEvents.PlayFabResultEvent<SetEntityProfilePolicyResponse> OnProfilesSetProfilePolicyResultEvent;

		// Token: 0x14000240 RID: 576
		// (add) Token: 0x06000B4B RID: 2891 RVA: 0x00025FD8 File Offset: 0x000241D8
		// (remove) Token: 0x06000B4C RID: 2892 RVA: 0x00026010 File Offset: 0x00024210
		public event PlayFabEvents.PlayFabErrorEvent OnGlobalErrorEvent;

		// Token: 0x06000B4D RID: 2893 RVA: 0x00026045 File Offset: 0x00024245
		private PlayFabEvents()
		{
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00026050 File Offset: 0x00024250
		public static PlayFabEvents Init()
		{
			if (PlayFabEvents._instance == null)
			{
				PlayFabEvents._instance = new PlayFabEvents();
			}
			PlayFabHttp.ApiProcessingEventHandler += PlayFabEvents._instance.OnProcessingEvent;
			PlayFabHttp.ApiProcessingErrorEventHandler += PlayFabEvents._instance.OnProcessingErrorEvent;
			return PlayFabEvents._instance;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000260A0 File Offset: 0x000242A0
		public void UnregisterInstance(object instance)
		{
			if (this.OnLoginResultEvent != null)
			{
				foreach (Delegate @delegate in this.OnLoginResultEvent.GetInvocationList())
				{
					if (@delegate.Target == instance)
					{
						this.OnLoginResultEvent -= (PlayFabEvents.PlayFabResultEvent<LoginResult>)@delegate;
					}
				}
			}
			if (this.OnAcceptTradeRequestEvent != null)
			{
				foreach (Delegate delegate2 in this.OnAcceptTradeRequestEvent.GetInvocationList())
				{
					if (delegate2.Target == instance)
					{
						this.OnAcceptTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptTradeRequest>)delegate2;
					}
				}
			}
			if (this.OnAcceptTradeResultEvent != null)
			{
				foreach (Delegate delegate3 in this.OnAcceptTradeResultEvent.GetInvocationList())
				{
					if (delegate3.Target == instance)
					{
						this.OnAcceptTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<AcceptTradeResponse>)delegate3;
					}
				}
			}
			if (this.OnAddFriendRequestEvent != null)
			{
				foreach (Delegate delegate4 in this.OnAddFriendRequestEvent.GetInvocationList())
				{
					if (delegate4.Target == instance)
					{
						this.OnAddFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddFriendRequest>)delegate4;
					}
				}
			}
			if (this.OnAddFriendResultEvent != null)
			{
				foreach (Delegate delegate5 in this.OnAddFriendResultEvent.GetInvocationList())
				{
					if (delegate5.Target == instance)
					{
						this.OnAddFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddFriendResult>)delegate5;
					}
				}
			}
			if (this.OnAddGenericIDRequestEvent != null)
			{
				foreach (Delegate delegate6 in this.OnAddGenericIDRequestEvent.GetInvocationList())
				{
					if (delegate6.Target == instance)
					{
						this.OnAddGenericIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddGenericIDRequest>)delegate6;
					}
				}
			}
			if (this.OnAddGenericIDResultEvent != null)
			{
				foreach (Delegate delegate7 in this.OnAddGenericIDResultEvent.GetInvocationList())
				{
					if (delegate7.Target == instance)
					{
						this.OnAddGenericIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddGenericIDResult>)delegate7;
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailRequestEvent != null)
			{
				foreach (Delegate delegate8 in this.OnAddOrUpdateContactEmailRequestEvent.GetInvocationList())
				{
					if (delegate8.Target == instance)
					{
						this.OnAddOrUpdateContactEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddOrUpdateContactEmailRequest>)delegate8;
					}
				}
			}
			if (this.OnAddOrUpdateContactEmailResultEvent != null)
			{
				foreach (Delegate delegate9 in this.OnAddOrUpdateContactEmailResultEvent.GetInvocationList())
				{
					if (delegate9.Target == instance)
					{
						this.OnAddOrUpdateContactEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddOrUpdateContactEmailResult>)delegate9;
					}
				}
			}
			if (this.OnAddSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate10 in this.OnAddSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (delegate10.Target == instance)
					{
						this.OnAddSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddSharedGroupMembersRequest>)delegate10;
					}
				}
			}
			if (this.OnAddSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate11 in this.OnAddSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (delegate11.Target == instance)
					{
						this.OnAddSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddSharedGroupMembersResult>)delegate11;
					}
				}
			}
			if (this.OnAddUsernamePasswordRequestEvent != null)
			{
				foreach (Delegate delegate12 in this.OnAddUsernamePasswordRequestEvent.GetInvocationList())
				{
					if (delegate12.Target == instance)
					{
						this.OnAddUsernamePasswordRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddUsernamePasswordRequest>)delegate12;
					}
				}
			}
			if (this.OnAddUsernamePasswordResultEvent != null)
			{
				foreach (Delegate delegate13 in this.OnAddUsernamePasswordResultEvent.GetInvocationList())
				{
					if (delegate13.Target == instance)
					{
						this.OnAddUsernamePasswordResultEvent -= (PlayFabEvents.PlayFabResultEvent<AddUsernamePasswordResult>)delegate13;
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate14 in this.OnAddUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (delegate14.Target == instance)
					{
						this.OnAddUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddUserVirtualCurrencyRequest>)delegate14;
					}
				}
			}
			if (this.OnAddUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate15 in this.OnAddUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (delegate15.Target == instance)
					{
						this.OnAddUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)delegate15;
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
			{
				foreach (Delegate delegate16 in this.OnAndroidDevicePushNotificationRegistrationRequestEvent.GetInvocationList())
				{
					if (delegate16.Target == instance)
					{
						this.OnAndroidDevicePushNotificationRegistrationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AndroidDevicePushNotificationRegistrationRequest>)delegate16;
					}
				}
			}
			if (this.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
			{
				foreach (Delegate delegate17 in this.OnAndroidDevicePushNotificationRegistrationResultEvent.GetInvocationList())
				{
					if (delegate17.Target == instance)
					{
						this.OnAndroidDevicePushNotificationRegistrationResultEvent -= (PlayFabEvents.PlayFabResultEvent<AndroidDevicePushNotificationRegistrationResult>)delegate17;
					}
				}
			}
			if (this.OnAttributeInstallRequestEvent != null)
			{
				foreach (Delegate delegate18 in this.OnAttributeInstallRequestEvent.GetInvocationList())
				{
					if (delegate18.Target == instance)
					{
						this.OnAttributeInstallRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AttributeInstallRequest>)delegate18;
					}
				}
			}
			if (this.OnAttributeInstallResultEvent != null)
			{
				foreach (Delegate delegate19 in this.OnAttributeInstallResultEvent.GetInvocationList())
				{
					if (delegate19.Target == instance)
					{
						this.OnAttributeInstallResultEvent -= (PlayFabEvents.PlayFabResultEvent<AttributeInstallResult>)delegate19;
					}
				}
			}
			if (this.OnCancelTradeRequestEvent != null)
			{
				foreach (Delegate delegate20 in this.OnCancelTradeRequestEvent.GetInvocationList())
				{
					if (delegate20.Target == instance)
					{
						this.OnCancelTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelTradeRequest>)delegate20;
					}
				}
			}
			if (this.OnCancelTradeResultEvent != null)
			{
				foreach (Delegate delegate21 in this.OnCancelTradeResultEvent.GetInvocationList())
				{
					if (delegate21.Target == instance)
					{
						this.OnCancelTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelTradeResponse>)delegate21;
					}
				}
			}
			if (this.OnConfirmPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate22 in this.OnConfirmPurchaseRequestEvent.GetInvocationList())
				{
					if (delegate22.Target == instance)
					{
						this.OnConfirmPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConfirmPurchaseRequest>)delegate22;
					}
				}
			}
			if (this.OnConfirmPurchaseResultEvent != null)
			{
				foreach (Delegate delegate23 in this.OnConfirmPurchaseResultEvent.GetInvocationList())
				{
					if (delegate23.Target == instance)
					{
						this.OnConfirmPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConfirmPurchaseResult>)delegate23;
					}
				}
			}
			if (this.OnConsumeItemRequestEvent != null)
			{
				foreach (Delegate delegate24 in this.OnConsumeItemRequestEvent.GetInvocationList())
				{
					if (delegate24.Target == instance)
					{
						this.OnConsumeItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConsumeItemRequest>)delegate24;
					}
				}
			}
			if (this.OnConsumeItemResultEvent != null)
			{
				foreach (Delegate delegate25 in this.OnConsumeItemResultEvent.GetInvocationList())
				{
					if (delegate25.Target == instance)
					{
						this.OnConsumeItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConsumeItemResult>)delegate25;
					}
				}
			}
			if (this.OnConsumePSNEntitlementsRequestEvent != null)
			{
				foreach (Delegate delegate26 in this.OnConsumePSNEntitlementsRequestEvent.GetInvocationList())
				{
					if (delegate26.Target == instance)
					{
						this.OnConsumePSNEntitlementsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConsumePSNEntitlementsRequest>)delegate26;
					}
				}
			}
			if (this.OnConsumePSNEntitlementsResultEvent != null)
			{
				foreach (Delegate delegate27 in this.OnConsumePSNEntitlementsResultEvent.GetInvocationList())
				{
					if (delegate27.Target == instance)
					{
						this.OnConsumePSNEntitlementsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConsumePSNEntitlementsResult>)delegate27;
					}
				}
			}
			if (this.OnConsumeXboxEntitlementsRequestEvent != null)
			{
				foreach (Delegate delegate28 in this.OnConsumeXboxEntitlementsRequestEvent.GetInvocationList())
				{
					if (delegate28.Target == instance)
					{
						this.OnConsumeXboxEntitlementsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ConsumeXboxEntitlementsRequest>)delegate28;
					}
				}
			}
			if (this.OnConsumeXboxEntitlementsResultEvent != null)
			{
				foreach (Delegate delegate29 in this.OnConsumeXboxEntitlementsResultEvent.GetInvocationList())
				{
					if (delegate29.Target == instance)
					{
						this.OnConsumeXboxEntitlementsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ConsumeXboxEntitlementsResult>)delegate29;
					}
				}
			}
			if (this.OnCreateSharedGroupRequestEvent != null)
			{
				foreach (Delegate delegate30 in this.OnCreateSharedGroupRequestEvent.GetInvocationList())
				{
					if (delegate30.Target == instance)
					{
						this.OnCreateSharedGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateSharedGroupRequest>)delegate30;
					}
				}
			}
			if (this.OnCreateSharedGroupResultEvent != null)
			{
				foreach (Delegate delegate31 in this.OnCreateSharedGroupResultEvent.GetInvocationList())
				{
					if (delegate31.Target == instance)
					{
						this.OnCreateSharedGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateSharedGroupResult>)delegate31;
					}
				}
			}
			if (this.OnExecuteCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate32 in this.OnExecuteCloudScriptRequestEvent.GetInvocationList())
				{
					if (delegate32.Target == instance)
					{
						this.OnExecuteCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteCloudScriptRequest>)delegate32;
					}
				}
			}
			if (this.OnExecuteCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate33 in this.OnExecuteCloudScriptResultEvent.GetInvocationList())
				{
					if (delegate33.Target == instance)
					{
						this.OnExecuteCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.ExecuteCloudScriptResult>)delegate33;
					}
				}
			}
			if (this.OnGetAccountInfoRequestEvent != null)
			{
				foreach (Delegate delegate34 in this.OnGetAccountInfoRequestEvent.GetInvocationList())
				{
					if (delegate34.Target == instance)
					{
						this.OnGetAccountInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetAccountInfoRequest>)delegate34;
					}
				}
			}
			if (this.OnGetAccountInfoResultEvent != null)
			{
				foreach (Delegate delegate35 in this.OnGetAccountInfoResultEvent.GetInvocationList())
				{
					if (delegate35.Target == instance)
					{
						this.OnGetAccountInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetAccountInfoResult>)delegate35;
					}
				}
			}
			if (this.OnGetAdPlacementsRequestEvent != null)
			{
				foreach (Delegate delegate36 in this.OnGetAdPlacementsRequestEvent.GetInvocationList())
				{
					if (delegate36.Target == instance)
					{
						this.OnGetAdPlacementsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetAdPlacementsRequest>)delegate36;
					}
				}
			}
			if (this.OnGetAdPlacementsResultEvent != null)
			{
				foreach (Delegate delegate37 in this.OnGetAdPlacementsResultEvent.GetInvocationList())
				{
					if (delegate37.Target == instance)
					{
						this.OnGetAdPlacementsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetAdPlacementsResult>)delegate37;
					}
				}
			}
			if (this.OnGetAllUsersCharactersRequestEvent != null)
			{
				foreach (Delegate delegate38 in this.OnGetAllUsersCharactersRequestEvent.GetInvocationList())
				{
					if (delegate38.Target == instance)
					{
						this.OnGetAllUsersCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListUsersCharactersRequest>)delegate38;
					}
				}
			}
			if (this.OnGetAllUsersCharactersResultEvent != null)
			{
				foreach (Delegate delegate39 in this.OnGetAllUsersCharactersResultEvent.GetInvocationList())
				{
					if (delegate39.Target == instance)
					{
						this.OnGetAllUsersCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListUsersCharactersResult>)delegate39;
					}
				}
			}
			if (this.OnGetCatalogItemsRequestEvent != null)
			{
				foreach (Delegate delegate40 in this.OnGetCatalogItemsRequestEvent.GetInvocationList())
				{
					if (delegate40.Target == instance)
					{
						this.OnGetCatalogItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCatalogItemsRequest>)delegate40;
					}
				}
			}
			if (this.OnGetCatalogItemsResultEvent != null)
			{
				foreach (Delegate delegate41 in this.OnGetCatalogItemsResultEvent.GetInvocationList())
				{
					if (delegate41.Target == instance)
					{
						this.OnGetCatalogItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCatalogItemsResult>)delegate41;
					}
				}
			}
			if (this.OnGetCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate42 in this.OnGetCharacterDataRequestEvent.GetInvocationList())
				{
					if (delegate42.Target == instance)
					{
						this.OnGetCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest>)delegate42;
					}
				}
			}
			if (this.OnGetCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate43 in this.OnGetCharacterDataResultEvent.GetInvocationList())
				{
					if (delegate43.Target == instance)
					{
						this.OnGetCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult>)delegate43;
					}
				}
			}
			if (this.OnGetCharacterInventoryRequestEvent != null)
			{
				foreach (Delegate delegate44 in this.OnGetCharacterInventoryRequestEvent.GetInvocationList())
				{
					if (delegate44.Target == instance)
					{
						this.OnGetCharacterInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterInventoryRequest>)delegate44;
					}
				}
			}
			if (this.OnGetCharacterInventoryResultEvent != null)
			{
				foreach (Delegate delegate45 in this.OnGetCharacterInventoryResultEvent.GetInvocationList())
				{
					if (delegate45.Target == instance)
					{
						this.OnGetCharacterInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterInventoryResult>)delegate45;
					}
				}
			}
			if (this.OnGetCharacterLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate46 in this.OnGetCharacterLeaderboardRequestEvent.GetInvocationList())
				{
					if (delegate46.Target == instance)
					{
						this.OnGetCharacterLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterLeaderboardRequest>)delegate46;
					}
				}
			}
			if (this.OnGetCharacterLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate47 in this.OnGetCharacterLeaderboardResultEvent.GetInvocationList())
				{
					if (delegate47.Target == instance)
					{
						this.OnGetCharacterLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterLeaderboardResult>)delegate47;
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate48 in this.OnGetCharacterReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (delegate48.Target == instance)
					{
						this.OnGetCharacterReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterDataRequest>)delegate48;
					}
				}
			}
			if (this.OnGetCharacterReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate49 in this.OnGetCharacterReadOnlyDataResultEvent.GetInvocationList())
				{
					if (delegate49.Target == instance)
					{
						this.OnGetCharacterReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterDataResult>)delegate49;
					}
				}
			}
			if (this.OnGetCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate50 in this.OnGetCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (delegate50.Target == instance)
					{
						this.OnGetCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetCharacterStatisticsRequest>)delegate50;
					}
				}
			}
			if (this.OnGetCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate51 in this.OnGetCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (delegate51.Target == instance)
					{
						this.OnGetCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetCharacterStatisticsResult>)delegate51;
					}
				}
			}
			if (this.OnGetContentDownloadUrlRequestEvent != null)
			{
				foreach (Delegate delegate52 in this.OnGetContentDownloadUrlRequestEvent.GetInvocationList())
				{
					if (delegate52.Target == instance)
					{
						this.OnGetContentDownloadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetContentDownloadUrlRequest>)delegate52;
					}
				}
			}
			if (this.OnGetContentDownloadUrlResultEvent != null)
			{
				foreach (Delegate delegate53 in this.OnGetContentDownloadUrlResultEvent.GetInvocationList())
				{
					if (delegate53.Target == instance)
					{
						this.OnGetContentDownloadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetContentDownloadUrlResult>)delegate53;
					}
				}
			}
			if (this.OnGetCurrentGamesRequestEvent != null)
			{
				foreach (Delegate delegate54 in this.OnGetCurrentGamesRequestEvent.GetInvocationList())
				{
					if (delegate54.Target == instance)
					{
						this.OnGetCurrentGamesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CurrentGamesRequest>)delegate54;
					}
				}
			}
			if (this.OnGetCurrentGamesResultEvent != null)
			{
				foreach (Delegate delegate55 in this.OnGetCurrentGamesResultEvent.GetInvocationList())
				{
					if (delegate55.Target == instance)
					{
						this.OnGetCurrentGamesResultEvent -= (PlayFabEvents.PlayFabResultEvent<CurrentGamesResult>)delegate55;
					}
				}
			}
			if (this.OnGetFriendLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate56 in this.OnGetFriendLeaderboardRequestEvent.GetInvocationList())
				{
					if (delegate56.Target == instance)
					{
						this.OnGetFriendLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardRequest>)delegate56;
					}
				}
			}
			if (this.OnGetFriendLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate57 in this.OnGetFriendLeaderboardResultEvent.GetInvocationList())
				{
					if (delegate57.Target == instance)
					{
						this.OnGetFriendLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult>)delegate57;
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
			{
				foreach (Delegate delegate58 in this.OnGetFriendLeaderboardAroundPlayerRequestEvent.GetInvocationList())
				{
					if (delegate58.Target == instance)
					{
						this.OnGetFriendLeaderboardAroundPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendLeaderboardAroundPlayerRequest>)delegate58;
					}
				}
			}
			if (this.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
			{
				foreach (Delegate delegate59 in this.OnGetFriendLeaderboardAroundPlayerResultEvent.GetInvocationList())
				{
					if (delegate59.Target == instance)
					{
						this.OnGetFriendLeaderboardAroundPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFriendLeaderboardAroundPlayerResult>)delegate59;
					}
				}
			}
			if (this.OnGetFriendsListRequestEvent != null)
			{
				foreach (Delegate delegate60 in this.OnGetFriendsListRequestEvent.GetInvocationList())
				{
					if (delegate60.Target == instance)
					{
						this.OnGetFriendsListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFriendsListRequest>)delegate60;
					}
				}
			}
			if (this.OnGetFriendsListResultEvent != null)
			{
				foreach (Delegate delegate61 in this.OnGetFriendsListResultEvent.GetInvocationList())
				{
					if (delegate61.Target == instance)
					{
						this.OnGetFriendsListResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFriendsListResult>)delegate61;
					}
				}
			}
			if (this.OnGetGameServerRegionsRequestEvent != null)
			{
				foreach (Delegate delegate62 in this.OnGetGameServerRegionsRequestEvent.GetInvocationList())
				{
					if (delegate62.Target == instance)
					{
						this.OnGetGameServerRegionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GameServerRegionsRequest>)delegate62;
					}
				}
			}
			if (this.OnGetGameServerRegionsResultEvent != null)
			{
				foreach (Delegate delegate63 in this.OnGetGameServerRegionsResultEvent.GetInvocationList())
				{
					if (delegate63.Target == instance)
					{
						this.OnGetGameServerRegionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GameServerRegionsResult>)delegate63;
					}
				}
			}
			if (this.OnGetLeaderboardRequestEvent != null)
			{
				foreach (Delegate delegate64 in this.OnGetLeaderboardRequestEvent.GetInvocationList())
				{
					if (delegate64.Target == instance)
					{
						this.OnGetLeaderboardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardRequest>)delegate64;
					}
				}
			}
			if (this.OnGetLeaderboardResultEvent != null)
			{
				foreach (Delegate delegate65 in this.OnGetLeaderboardResultEvent.GetInvocationList())
				{
					if (delegate65.Target == instance)
					{
						this.OnGetLeaderboardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardResult>)delegate65;
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterRequestEvent != null)
			{
				foreach (Delegate delegate66 in this.OnGetLeaderboardAroundCharacterRequestEvent.GetInvocationList())
				{
					if (delegate66.Target == instance)
					{
						this.OnGetLeaderboardAroundCharacterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundCharacterRequest>)delegate66;
					}
				}
			}
			if (this.OnGetLeaderboardAroundCharacterResultEvent != null)
			{
				foreach (Delegate delegate67 in this.OnGetLeaderboardAroundCharacterResultEvent.GetInvocationList())
				{
					if (delegate67.Target == instance)
					{
						this.OnGetLeaderboardAroundCharacterResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundCharacterResult>)delegate67;
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerRequestEvent != null)
			{
				foreach (Delegate delegate68 in this.OnGetLeaderboardAroundPlayerRequestEvent.GetInvocationList())
				{
					if (delegate68.Target == instance)
					{
						this.OnGetLeaderboardAroundPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardAroundPlayerRequest>)delegate68;
					}
				}
			}
			if (this.OnGetLeaderboardAroundPlayerResultEvent != null)
			{
				foreach (Delegate delegate69 in this.OnGetLeaderboardAroundPlayerResultEvent.GetInvocationList())
				{
					if (delegate69.Target == instance)
					{
						this.OnGetLeaderboardAroundPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardAroundPlayerResult>)delegate69;
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersRequestEvent != null)
			{
				foreach (Delegate delegate70 in this.OnGetLeaderboardForUserCharactersRequestEvent.GetInvocationList())
				{
					if (delegate70.Target == instance)
					{
						this.OnGetLeaderboardForUserCharactersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLeaderboardForUsersCharactersRequest>)delegate70;
					}
				}
			}
			if (this.OnGetLeaderboardForUserCharactersResultEvent != null)
			{
				foreach (Delegate delegate71 in this.OnGetLeaderboardForUserCharactersResultEvent.GetInvocationList())
				{
					if (delegate71.Target == instance)
					{
						this.OnGetLeaderboardForUserCharactersResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLeaderboardForUsersCharactersResult>)delegate71;
					}
				}
			}
			if (this.OnGetPaymentTokenRequestEvent != null)
			{
				foreach (Delegate delegate72 in this.OnGetPaymentTokenRequestEvent.GetInvocationList())
				{
					if (delegate72.Target == instance)
					{
						this.OnGetPaymentTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPaymentTokenRequest>)delegate72;
					}
				}
			}
			if (this.OnGetPaymentTokenResultEvent != null)
			{
				foreach (Delegate delegate73 in this.OnGetPaymentTokenResultEvent.GetInvocationList())
				{
					if (delegate73.Target == instance)
					{
						this.OnGetPaymentTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPaymentTokenResult>)delegate73;
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenRequestEvent != null)
			{
				foreach (Delegate delegate74 in this.OnGetPhotonAuthenticationTokenRequestEvent.GetInvocationList())
				{
					if (delegate74.Target == instance)
					{
						this.OnGetPhotonAuthenticationTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPhotonAuthenticationTokenRequest>)delegate74;
					}
				}
			}
			if (this.OnGetPhotonAuthenticationTokenResultEvent != null)
			{
				foreach (Delegate delegate75 in this.OnGetPhotonAuthenticationTokenResultEvent.GetInvocationList())
				{
					if (delegate75.Target == instance)
					{
						this.OnGetPhotonAuthenticationTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPhotonAuthenticationTokenResult>)delegate75;
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoRequestEvent != null)
			{
				foreach (Delegate delegate76 in this.OnGetPlayerCombinedInfoRequestEvent.GetInvocationList())
				{
					if (delegate76.Target == instance)
					{
						this.OnGetPlayerCombinedInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerCombinedInfoRequest>)delegate76;
					}
				}
			}
			if (this.OnGetPlayerCombinedInfoResultEvent != null)
			{
				foreach (Delegate delegate77 in this.OnGetPlayerCombinedInfoResultEvent.GetInvocationList())
				{
					if (delegate77.Target == instance)
					{
						this.OnGetPlayerCombinedInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerCombinedInfoResult>)delegate77;
					}
				}
			}
			if (this.OnGetPlayerProfileRequestEvent != null)
			{
				foreach (Delegate delegate78 in this.OnGetPlayerProfileRequestEvent.GetInvocationList())
				{
					if (delegate78.Target == instance)
					{
						this.OnGetPlayerProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerProfileRequest>)delegate78;
					}
				}
			}
			if (this.OnGetPlayerProfileResultEvent != null)
			{
				foreach (Delegate delegate79 in this.OnGetPlayerProfileResultEvent.GetInvocationList())
				{
					if (delegate79.Target == instance)
					{
						this.OnGetPlayerProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerProfileResult>)delegate79;
					}
				}
			}
			if (this.OnGetPlayerSegmentsRequestEvent != null)
			{
				foreach (Delegate delegate80 in this.OnGetPlayerSegmentsRequestEvent.GetInvocationList())
				{
					if (delegate80.Target == instance)
					{
						this.OnGetPlayerSegmentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerSegmentsRequest>)delegate80;
					}
				}
			}
			if (this.OnGetPlayerSegmentsResultEvent != null)
			{
				foreach (Delegate delegate81 in this.OnGetPlayerSegmentsResultEvent.GetInvocationList())
				{
					if (delegate81.Target == instance)
					{
						this.OnGetPlayerSegmentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerSegmentsResult>)delegate81;
					}
				}
			}
			if (this.OnGetPlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate82 in this.OnGetPlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (delegate82.Target == instance)
					{
						this.OnGetPlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticsRequest>)delegate82;
					}
				}
			}
			if (this.OnGetPlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate83 in this.OnGetPlayerStatisticsResultEvent.GetInvocationList())
				{
					if (delegate83.Target == instance)
					{
						this.OnGetPlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticsResult>)delegate83;
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsRequestEvent != null)
			{
				foreach (Delegate delegate84 in this.OnGetPlayerStatisticVersionsRequestEvent.GetInvocationList())
				{
					if (delegate84.Target == instance)
					{
						this.OnGetPlayerStatisticVersionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerStatisticVersionsRequest>)delegate84;
					}
				}
			}
			if (this.OnGetPlayerStatisticVersionsResultEvent != null)
			{
				foreach (Delegate delegate85 in this.OnGetPlayerStatisticVersionsResultEvent.GetInvocationList())
				{
					if (delegate85.Target == instance)
					{
						this.OnGetPlayerStatisticVersionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerStatisticVersionsResult>)delegate85;
					}
				}
			}
			if (this.OnGetPlayerTagsRequestEvent != null)
			{
				foreach (Delegate delegate86 in this.OnGetPlayerTagsRequestEvent.GetInvocationList())
				{
					if (delegate86.Target == instance)
					{
						this.OnGetPlayerTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerTagsRequest>)delegate86;
					}
				}
			}
			if (this.OnGetPlayerTagsResultEvent != null)
			{
				foreach (Delegate delegate87 in this.OnGetPlayerTagsResultEvent.GetInvocationList())
				{
					if (delegate87.Target == instance)
					{
						this.OnGetPlayerTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerTagsResult>)delegate87;
					}
				}
			}
			if (this.OnGetPlayerTradesRequestEvent != null)
			{
				foreach (Delegate delegate88 in this.OnGetPlayerTradesRequestEvent.GetInvocationList())
				{
					if (delegate88.Target == instance)
					{
						this.OnGetPlayerTradesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayerTradesRequest>)delegate88;
					}
				}
			}
			if (this.OnGetPlayerTradesResultEvent != null)
			{
				foreach (Delegate delegate89 in this.OnGetPlayerTradesResultEvent.GetInvocationList())
				{
					if (delegate89.Target == instance)
					{
						this.OnGetPlayerTradesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayerTradesResponse>)delegate89;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
			{
				foreach (Delegate delegate90 in this.OnGetPlayFabIDsFromFacebookIDsRequestEvent.GetInvocationList())
				{
					if (delegate90.Target == instance)
					{
						this.OnGetPlayFabIDsFromFacebookIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookIDsRequest>)delegate90;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
			{
				foreach (Delegate delegate91 in this.OnGetPlayFabIDsFromFacebookIDsResultEvent.GetInvocationList())
				{
					if (delegate91.Target == instance)
					{
						this.OnGetPlayFabIDsFromFacebookIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookIDsResult>)delegate91;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent != null)
			{
				foreach (Delegate delegate92 in this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent.GetInvocationList())
				{
					if (delegate92.Target == instance)
					{
						this.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromFacebookInstantGamesIdsRequest>)delegate92;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent != null)
			{
				foreach (Delegate delegate93 in this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent.GetInvocationList())
				{
					if (delegate93.Target == instance)
					{
						this.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromFacebookInstantGamesIdsResult>)delegate93;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
			{
				foreach (Delegate delegate94 in this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent.GetInvocationList())
				{
					if (delegate94.Target == instance)
					{
						this.OnGetPlayFabIDsFromGameCenterIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGameCenterIDsRequest>)delegate94;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
			{
				foreach (Delegate delegate95 in this.OnGetPlayFabIDsFromGameCenterIDsResultEvent.GetInvocationList())
				{
					if (delegate95.Target == instance)
					{
						this.OnGetPlayFabIDsFromGameCenterIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGameCenterIDsResult>)delegate95;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
			{
				foreach (Delegate delegate96 in this.OnGetPlayFabIDsFromGenericIDsRequestEvent.GetInvocationList())
				{
					if (delegate96.Target == instance)
					{
						this.OnGetPlayFabIDsFromGenericIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGenericIDsRequest>)delegate96;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
			{
				foreach (Delegate delegate97 in this.OnGetPlayFabIDsFromGenericIDsResultEvent.GetInvocationList())
				{
					if (delegate97.Target == instance)
					{
						this.OnGetPlayFabIDsFromGenericIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGenericIDsResult>)delegate97;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
			{
				foreach (Delegate delegate98 in this.OnGetPlayFabIDsFromGoogleIDsRequestEvent.GetInvocationList())
				{
					if (delegate98.Target == instance)
					{
						this.OnGetPlayFabIDsFromGoogleIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromGoogleIDsRequest>)delegate98;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
			{
				foreach (Delegate delegate99 in this.OnGetPlayFabIDsFromGoogleIDsResultEvent.GetInvocationList())
				{
					if (delegate99.Target == instance)
					{
						this.OnGetPlayFabIDsFromGoogleIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromGoogleIDsResult>)delegate99;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
			{
				foreach (Delegate delegate100 in this.OnGetPlayFabIDsFromKongregateIDsRequestEvent.GetInvocationList())
				{
					if (delegate100.Target == instance)
					{
						this.OnGetPlayFabIDsFromKongregateIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromKongregateIDsRequest>)delegate100;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
			{
				foreach (Delegate delegate101 in this.OnGetPlayFabIDsFromKongregateIDsResultEvent.GetInvocationList())
				{
					if (delegate101.Target == instance)
					{
						this.OnGetPlayFabIDsFromKongregateIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromKongregateIDsResult>)delegate101;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent != null)
			{
				foreach (Delegate delegate102 in this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent.GetInvocationList())
				{
					if (delegate102.Target == instance)
					{
						this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest>)delegate102;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent != null)
			{
				foreach (Delegate delegate103 in this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent.GetInvocationList())
				{
					if (delegate103.Target == instance)
					{
						this.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromNintendoSwitchDeviceIdsResult>)delegate103;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent != null)
			{
				foreach (Delegate delegate104 in this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent.GetInvocationList())
				{
					if (delegate104.Target == instance)
					{
						this.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromPSNAccountIDsRequest>)delegate104;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent != null)
			{
				foreach (Delegate delegate105 in this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent.GetInvocationList())
				{
					if (delegate105.Target == instance)
					{
						this.OnGetPlayFabIDsFromPSNAccountIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromPSNAccountIDsResult>)delegate105;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
			{
				foreach (Delegate delegate106 in this.OnGetPlayFabIDsFromSteamIDsRequestEvent.GetInvocationList())
				{
					if (delegate106.Target == instance)
					{
						this.OnGetPlayFabIDsFromSteamIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromSteamIDsRequest>)delegate106;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
			{
				foreach (Delegate delegate107 in this.OnGetPlayFabIDsFromSteamIDsResultEvent.GetInvocationList())
				{
					if (delegate107.Target == instance)
					{
						this.OnGetPlayFabIDsFromSteamIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromSteamIDsResult>)delegate107;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
			{
				foreach (Delegate delegate108 in this.OnGetPlayFabIDsFromTwitchIDsRequestEvent.GetInvocationList())
				{
					if (delegate108.Target == instance)
					{
						this.OnGetPlayFabIDsFromTwitchIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromTwitchIDsRequest>)delegate108;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
			{
				foreach (Delegate delegate109 in this.OnGetPlayFabIDsFromTwitchIDsResultEvent.GetInvocationList())
				{
					if (delegate109.Target == instance)
					{
						this.OnGetPlayFabIDsFromTwitchIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromTwitchIDsResult>)delegate109;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent != null)
			{
				foreach (Delegate delegate110 in this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent.GetInvocationList())
				{
					if (delegate110.Target == instance)
					{
						this.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPlayFabIDsFromXboxLiveIDsRequest>)delegate110;
					}
				}
			}
			if (this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent != null)
			{
				foreach (Delegate delegate111 in this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent.GetInvocationList())
				{
					if (delegate111.Target == instance)
					{
						this.OnGetPlayFabIDsFromXboxLiveIDsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPlayFabIDsFromXboxLiveIDsResult>)delegate111;
					}
				}
			}
			if (this.OnGetPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate112 in this.OnGetPublisherDataRequestEvent.GetInvocationList())
				{
					if (delegate112.Target == instance)
					{
						this.OnGetPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPublisherDataRequest>)delegate112;
					}
				}
			}
			if (this.OnGetPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate113 in this.OnGetPublisherDataResultEvent.GetInvocationList())
				{
					if (delegate113.Target == instance)
					{
						this.OnGetPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPublisherDataResult>)delegate113;
					}
				}
			}
			if (this.OnGetPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate114 in this.OnGetPurchaseRequestEvent.GetInvocationList())
				{
					if (delegate114.Target == instance)
					{
						this.OnGetPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetPurchaseRequest>)delegate114;
					}
				}
			}
			if (this.OnGetPurchaseResultEvent != null)
			{
				foreach (Delegate delegate115 in this.OnGetPurchaseResultEvent.GetInvocationList())
				{
					if (delegate115.Target == instance)
					{
						this.OnGetPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetPurchaseResult>)delegate115;
					}
				}
			}
			if (this.OnGetSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate116 in this.OnGetSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (delegate116.Target == instance)
					{
						this.OnGetSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetSharedGroupDataRequest>)delegate116;
					}
				}
			}
			if (this.OnGetSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate117 in this.OnGetSharedGroupDataResultEvent.GetInvocationList())
				{
					if (delegate117.Target == instance)
					{
						this.OnGetSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetSharedGroupDataResult>)delegate117;
					}
				}
			}
			if (this.OnGetStoreItemsRequestEvent != null)
			{
				foreach (Delegate delegate118 in this.OnGetStoreItemsRequestEvent.GetInvocationList())
				{
					if (delegate118.Target == instance)
					{
						this.OnGetStoreItemsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetStoreItemsRequest>)delegate118;
					}
				}
			}
			if (this.OnGetStoreItemsResultEvent != null)
			{
				foreach (Delegate delegate119 in this.OnGetStoreItemsResultEvent.GetInvocationList())
				{
					if (delegate119.Target == instance)
					{
						this.OnGetStoreItemsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetStoreItemsResult>)delegate119;
					}
				}
			}
			if (this.OnGetTimeRequestEvent != null)
			{
				foreach (Delegate delegate120 in this.OnGetTimeRequestEvent.GetInvocationList())
				{
					if (delegate120.Target == instance)
					{
						this.OnGetTimeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTimeRequest>)delegate120;
					}
				}
			}
			if (this.OnGetTimeResultEvent != null)
			{
				foreach (Delegate delegate121 in this.OnGetTimeResultEvent.GetInvocationList())
				{
					if (delegate121.Target == instance)
					{
						this.OnGetTimeResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTimeResult>)delegate121;
					}
				}
			}
			if (this.OnGetTitleDataRequestEvent != null)
			{
				foreach (Delegate delegate122 in this.OnGetTitleDataRequestEvent.GetInvocationList())
				{
					if (delegate122.Target == instance)
					{
						this.OnGetTitleDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleDataRequest>)delegate122;
					}
				}
			}
			if (this.OnGetTitleDataResultEvent != null)
			{
				foreach (Delegate delegate123 in this.OnGetTitleDataResultEvent.GetInvocationList())
				{
					if (delegate123.Target == instance)
					{
						this.OnGetTitleDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleDataResult>)delegate123;
					}
				}
			}
			if (this.OnGetTitleNewsRequestEvent != null)
			{
				foreach (Delegate delegate124 in this.OnGetTitleNewsRequestEvent.GetInvocationList())
				{
					if (delegate124.Target == instance)
					{
						this.OnGetTitleNewsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleNewsRequest>)delegate124;
					}
				}
			}
			if (this.OnGetTitleNewsResultEvent != null)
			{
				foreach (Delegate delegate125 in this.OnGetTitleNewsResultEvent.GetInvocationList())
				{
					if (delegate125.Target == instance)
					{
						this.OnGetTitleNewsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleNewsResult>)delegate125;
					}
				}
			}
			if (this.OnGetTitlePublicKeyRequestEvent != null)
			{
				foreach (Delegate delegate126 in this.OnGetTitlePublicKeyRequestEvent.GetInvocationList())
				{
					if (delegate126.Target == instance)
					{
						this.OnGetTitlePublicKeyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitlePublicKeyRequest>)delegate126;
					}
				}
			}
			if (this.OnGetTitlePublicKeyResultEvent != null)
			{
				foreach (Delegate delegate127 in this.OnGetTitlePublicKeyResultEvent.GetInvocationList())
				{
					if (delegate127.Target == instance)
					{
						this.OnGetTitlePublicKeyResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitlePublicKeyResult>)delegate127;
					}
				}
			}
			if (this.OnGetTradeStatusRequestEvent != null)
			{
				foreach (Delegate delegate128 in this.OnGetTradeStatusRequestEvent.GetInvocationList())
				{
					if (delegate128.Target == instance)
					{
						this.OnGetTradeStatusRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTradeStatusRequest>)delegate128;
					}
				}
			}
			if (this.OnGetTradeStatusResultEvent != null)
			{
				foreach (Delegate delegate129 in this.OnGetTradeStatusResultEvent.GetInvocationList())
				{
					if (delegate129.Target == instance)
					{
						this.OnGetTradeStatusResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTradeStatusResponse>)delegate129;
					}
				}
			}
			if (this.OnGetUserDataRequestEvent != null)
			{
				foreach (Delegate delegate130 in this.OnGetUserDataRequestEvent.GetInvocationList())
				{
					if (delegate130.Target == instance)
					{
						this.OnGetUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate130;
					}
				}
			}
			if (this.OnGetUserDataResultEvent != null)
			{
				foreach (Delegate delegate131 in this.OnGetUserDataResultEvent.GetInvocationList())
				{
					if (delegate131.Target == instance)
					{
						this.OnGetUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate131;
					}
				}
			}
			if (this.OnGetUserInventoryRequestEvent != null)
			{
				foreach (Delegate delegate132 in this.OnGetUserInventoryRequestEvent.GetInvocationList())
				{
					if (delegate132.Target == instance)
					{
						this.OnGetUserInventoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserInventoryRequest>)delegate132;
					}
				}
			}
			if (this.OnGetUserInventoryResultEvent != null)
			{
				foreach (Delegate delegate133 in this.OnGetUserInventoryResultEvent.GetInvocationList())
				{
					if (delegate133.Target == instance)
					{
						this.OnGetUserInventoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserInventoryResult>)delegate133;
					}
				}
			}
			if (this.OnGetUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate134 in this.OnGetUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (delegate134.Target == instance)
					{
						this.OnGetUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate134;
					}
				}
			}
			if (this.OnGetUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate135 in this.OnGetUserPublisherDataResultEvent.GetInvocationList())
				{
					if (delegate135.Target == instance)
					{
						this.OnGetUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate135;
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate136 in this.OnGetUserPublisherReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (delegate136.Target == instance)
					{
						this.OnGetUserPublisherReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate136;
					}
				}
			}
			if (this.OnGetUserPublisherReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate137 in this.OnGetUserPublisherReadOnlyDataResultEvent.GetInvocationList())
				{
					if (delegate137.Target == instance)
					{
						this.OnGetUserPublisherReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate137;
					}
				}
			}
			if (this.OnGetUserReadOnlyDataRequestEvent != null)
			{
				foreach (Delegate delegate138 in this.OnGetUserReadOnlyDataRequestEvent.GetInvocationList())
				{
					if (delegate138.Target == instance)
					{
						this.OnGetUserReadOnlyDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetUserDataRequest>)delegate138;
					}
				}
			}
			if (this.OnGetUserReadOnlyDataResultEvent != null)
			{
				foreach (Delegate delegate139 in this.OnGetUserReadOnlyDataResultEvent.GetInvocationList())
				{
					if (delegate139.Target == instance)
					{
						this.OnGetUserReadOnlyDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetUserDataResult>)delegate139;
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeRequestEvent != null)
			{
				foreach (Delegate delegate140 in this.OnGetWindowsHelloChallengeRequestEvent.GetInvocationList())
				{
					if (delegate140.Target == instance)
					{
						this.OnGetWindowsHelloChallengeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetWindowsHelloChallengeRequest>)delegate140;
					}
				}
			}
			if (this.OnGetWindowsHelloChallengeResultEvent != null)
			{
				foreach (Delegate delegate141 in this.OnGetWindowsHelloChallengeResultEvent.GetInvocationList())
				{
					if (delegate141.Target == instance)
					{
						this.OnGetWindowsHelloChallengeResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetWindowsHelloChallengeResponse>)delegate141;
					}
				}
			}
			if (this.OnGrantCharacterToUserRequestEvent != null)
			{
				foreach (Delegate delegate142 in this.OnGrantCharacterToUserRequestEvent.GetInvocationList())
				{
					if (delegate142.Target == instance)
					{
						this.OnGrantCharacterToUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GrantCharacterToUserRequest>)delegate142;
					}
				}
			}
			if (this.OnGrantCharacterToUserResultEvent != null)
			{
				foreach (Delegate delegate143 in this.OnGrantCharacterToUserResultEvent.GetInvocationList())
				{
					if (delegate143.Target == instance)
					{
						this.OnGrantCharacterToUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<GrantCharacterToUserResult>)delegate143;
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate144 in this.OnLinkAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (delegate144.Target == instance)
					{
						this.OnLinkAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkAndroidDeviceIDRequest>)delegate144;
					}
				}
			}
			if (this.OnLinkAndroidDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate145 in this.OnLinkAndroidDeviceIDResultEvent.GetInvocationList())
				{
					if (delegate145.Target == instance)
					{
						this.OnLinkAndroidDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkAndroidDeviceIDResult>)delegate145;
					}
				}
			}
			if (this.OnLinkAppleRequestEvent != null)
			{
				foreach (Delegate delegate146 in this.OnLinkAppleRequestEvent.GetInvocationList())
				{
					if (delegate146.Target == instance)
					{
						this.OnLinkAppleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkAppleRequest>)delegate146;
					}
				}
			}
			if (this.OnLinkAppleResultEvent != null)
			{
				foreach (Delegate delegate147 in this.OnLinkAppleResultEvent.GetInvocationList())
				{
					if (delegate147.Target == instance)
					{
						this.OnLinkAppleResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)delegate147;
					}
				}
			}
			if (this.OnLinkCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate148 in this.OnLinkCustomIDRequestEvent.GetInvocationList())
				{
					if (delegate148.Target == instance)
					{
						this.OnLinkCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkCustomIDRequest>)delegate148;
					}
				}
			}
			if (this.OnLinkCustomIDResultEvent != null)
			{
				foreach (Delegate delegate149 in this.OnLinkCustomIDResultEvent.GetInvocationList())
				{
					if (delegate149.Target == instance)
					{
						this.OnLinkCustomIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkCustomIDResult>)delegate149;
					}
				}
			}
			if (this.OnLinkFacebookAccountRequestEvent != null)
			{
				foreach (Delegate delegate150 in this.OnLinkFacebookAccountRequestEvent.GetInvocationList())
				{
					if (delegate150.Target == instance)
					{
						this.OnLinkFacebookAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkFacebookAccountRequest>)delegate150;
					}
				}
			}
			if (this.OnLinkFacebookAccountResultEvent != null)
			{
				foreach (Delegate delegate151 in this.OnLinkFacebookAccountResultEvent.GetInvocationList())
				{
					if (delegate151.Target == instance)
					{
						this.OnLinkFacebookAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkFacebookAccountResult>)delegate151;
					}
				}
			}
			if (this.OnLinkFacebookInstantGamesIdRequestEvent != null)
			{
				foreach (Delegate delegate152 in this.OnLinkFacebookInstantGamesIdRequestEvent.GetInvocationList())
				{
					if (delegate152.Target == instance)
					{
						this.OnLinkFacebookInstantGamesIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkFacebookInstantGamesIdRequest>)delegate152;
					}
				}
			}
			if (this.OnLinkFacebookInstantGamesIdResultEvent != null)
			{
				foreach (Delegate delegate153 in this.OnLinkFacebookInstantGamesIdResultEvent.GetInvocationList())
				{
					if (delegate153.Target == instance)
					{
						this.OnLinkFacebookInstantGamesIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkFacebookInstantGamesIdResult>)delegate153;
					}
				}
			}
			if (this.OnLinkGameCenterAccountRequestEvent != null)
			{
				foreach (Delegate delegate154 in this.OnLinkGameCenterAccountRequestEvent.GetInvocationList())
				{
					if (delegate154.Target == instance)
					{
						this.OnLinkGameCenterAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkGameCenterAccountRequest>)delegate154;
					}
				}
			}
			if (this.OnLinkGameCenterAccountResultEvent != null)
			{
				foreach (Delegate delegate155 in this.OnLinkGameCenterAccountResultEvent.GetInvocationList())
				{
					if (delegate155.Target == instance)
					{
						this.OnLinkGameCenterAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkGameCenterAccountResult>)delegate155;
					}
				}
			}
			if (this.OnLinkGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate156 in this.OnLinkGoogleAccountRequestEvent.GetInvocationList())
				{
					if (delegate156.Target == instance)
					{
						this.OnLinkGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkGoogleAccountRequest>)delegate156;
					}
				}
			}
			if (this.OnLinkGoogleAccountResultEvent != null)
			{
				foreach (Delegate delegate157 in this.OnLinkGoogleAccountResultEvent.GetInvocationList())
				{
					if (delegate157.Target == instance)
					{
						this.OnLinkGoogleAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkGoogleAccountResult>)delegate157;
					}
				}
			}
			if (this.OnLinkIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate158 in this.OnLinkIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (delegate158.Target == instance)
					{
						this.OnLinkIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkIOSDeviceIDRequest>)delegate158;
					}
				}
			}
			if (this.OnLinkIOSDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate159 in this.OnLinkIOSDeviceIDResultEvent.GetInvocationList())
				{
					if (delegate159.Target == instance)
					{
						this.OnLinkIOSDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkIOSDeviceIDResult>)delegate159;
					}
				}
			}
			if (this.OnLinkKongregateRequestEvent != null)
			{
				foreach (Delegate delegate160 in this.OnLinkKongregateRequestEvent.GetInvocationList())
				{
					if (delegate160.Target == instance)
					{
						this.OnLinkKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkKongregateAccountRequest>)delegate160;
					}
				}
			}
			if (this.OnLinkKongregateResultEvent != null)
			{
				foreach (Delegate delegate161 in this.OnLinkKongregateResultEvent.GetInvocationList())
				{
					if (delegate161.Target == instance)
					{
						this.OnLinkKongregateResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkKongregateAccountResult>)delegate161;
					}
				}
			}
			if (this.OnLinkNintendoServiceAccountRequestEvent != null)
			{
				foreach (Delegate delegate162 in this.OnLinkNintendoServiceAccountRequestEvent.GetInvocationList())
				{
					if (delegate162.Target == instance)
					{
						this.OnLinkNintendoServiceAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkNintendoServiceAccountRequest>)delegate162;
					}
				}
			}
			if (this.OnLinkNintendoServiceAccountResultEvent != null)
			{
				foreach (Delegate delegate163 in this.OnLinkNintendoServiceAccountResultEvent.GetInvocationList())
				{
					if (delegate163.Target == instance)
					{
						this.OnLinkNintendoServiceAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)delegate163;
					}
				}
			}
			if (this.OnLinkNintendoSwitchDeviceIdRequestEvent != null)
			{
				foreach (Delegate delegate164 in this.OnLinkNintendoSwitchDeviceIdRequestEvent.GetInvocationList())
				{
					if (delegate164.Target == instance)
					{
						this.OnLinkNintendoSwitchDeviceIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkNintendoSwitchDeviceIdRequest>)delegate164;
					}
				}
			}
			if (this.OnLinkNintendoSwitchDeviceIdResultEvent != null)
			{
				foreach (Delegate delegate165 in this.OnLinkNintendoSwitchDeviceIdResultEvent.GetInvocationList())
				{
					if (delegate165.Target == instance)
					{
						this.OnLinkNintendoSwitchDeviceIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkNintendoSwitchDeviceIdResult>)delegate165;
					}
				}
			}
			if (this.OnLinkOpenIdConnectRequestEvent != null)
			{
				foreach (Delegate delegate166 in this.OnLinkOpenIdConnectRequestEvent.GetInvocationList())
				{
					if (delegate166.Target == instance)
					{
						this.OnLinkOpenIdConnectRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkOpenIdConnectRequest>)delegate166;
					}
				}
			}
			if (this.OnLinkOpenIdConnectResultEvent != null)
			{
				foreach (Delegate delegate167 in this.OnLinkOpenIdConnectResultEvent.GetInvocationList())
				{
					if (delegate167.Target == instance)
					{
						this.OnLinkOpenIdConnectResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResult>)delegate167;
					}
				}
			}
			if (this.OnLinkPSNAccountRequestEvent != null)
			{
				foreach (Delegate delegate168 in this.OnLinkPSNAccountRequestEvent.GetInvocationList())
				{
					if (delegate168.Target == instance)
					{
						this.OnLinkPSNAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkPSNAccountRequest>)delegate168;
					}
				}
			}
			if (this.OnLinkPSNAccountResultEvent != null)
			{
				foreach (Delegate delegate169 in this.OnLinkPSNAccountResultEvent.GetInvocationList())
				{
					if (delegate169.Target == instance)
					{
						this.OnLinkPSNAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkPSNAccountResult>)delegate169;
					}
				}
			}
			if (this.OnLinkSteamAccountRequestEvent != null)
			{
				foreach (Delegate delegate170 in this.OnLinkSteamAccountRequestEvent.GetInvocationList())
				{
					if (delegate170.Target == instance)
					{
						this.OnLinkSteamAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkSteamAccountRequest>)delegate170;
					}
				}
			}
			if (this.OnLinkSteamAccountResultEvent != null)
			{
				foreach (Delegate delegate171 in this.OnLinkSteamAccountResultEvent.GetInvocationList())
				{
					if (delegate171.Target == instance)
					{
						this.OnLinkSteamAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkSteamAccountResult>)delegate171;
					}
				}
			}
			if (this.OnLinkTwitchRequestEvent != null)
			{
				foreach (Delegate delegate172 in this.OnLinkTwitchRequestEvent.GetInvocationList())
				{
					if (delegate172.Target == instance)
					{
						this.OnLinkTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkTwitchAccountRequest>)delegate172;
					}
				}
			}
			if (this.OnLinkTwitchResultEvent != null)
			{
				foreach (Delegate delegate173 in this.OnLinkTwitchResultEvent.GetInvocationList())
				{
					if (delegate173.Target == instance)
					{
						this.OnLinkTwitchResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkTwitchAccountResult>)delegate173;
					}
				}
			}
			if (this.OnLinkWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate174 in this.OnLinkWindowsHelloRequestEvent.GetInvocationList())
				{
					if (delegate174.Target == instance)
					{
						this.OnLinkWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkWindowsHelloAccountRequest>)delegate174;
					}
				}
			}
			if (this.OnLinkWindowsHelloResultEvent != null)
			{
				foreach (Delegate delegate175 in this.OnLinkWindowsHelloResultEvent.GetInvocationList())
				{
					if (delegate175.Target == instance)
					{
						this.OnLinkWindowsHelloResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkWindowsHelloAccountResponse>)delegate175;
					}
				}
			}
			if (this.OnLinkXboxAccountRequestEvent != null)
			{
				foreach (Delegate delegate176 in this.OnLinkXboxAccountRequestEvent.GetInvocationList())
				{
					if (delegate176.Target == instance)
					{
						this.OnLinkXboxAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LinkXboxAccountRequest>)delegate176;
					}
				}
			}
			if (this.OnLinkXboxAccountResultEvent != null)
			{
				foreach (Delegate delegate177 in this.OnLinkXboxAccountResultEvent.GetInvocationList())
				{
					if (delegate177.Target == instance)
					{
						this.OnLinkXboxAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<LinkXboxAccountResult>)delegate177;
					}
				}
			}
			if (this.OnLoginWithAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate178 in this.OnLoginWithAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (delegate178.Target == instance)
					{
						this.OnLoginWithAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithAndroidDeviceIDRequest>)delegate178;
					}
				}
			}
			if (this.OnLoginWithAppleRequestEvent != null)
			{
				foreach (Delegate delegate179 in this.OnLoginWithAppleRequestEvent.GetInvocationList())
				{
					if (delegate179.Target == instance)
					{
						this.OnLoginWithAppleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithAppleRequest>)delegate179;
					}
				}
			}
			if (this.OnLoginWithCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate180 in this.OnLoginWithCustomIDRequestEvent.GetInvocationList())
				{
					if (delegate180.Target == instance)
					{
						this.OnLoginWithCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithCustomIDRequest>)delegate180;
					}
				}
			}
			if (this.OnLoginWithEmailAddressRequestEvent != null)
			{
				foreach (Delegate delegate181 in this.OnLoginWithEmailAddressRequestEvent.GetInvocationList())
				{
					if (delegate181.Target == instance)
					{
						this.OnLoginWithEmailAddressRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithEmailAddressRequest>)delegate181;
					}
				}
			}
			if (this.OnLoginWithFacebookRequestEvent != null)
			{
				foreach (Delegate delegate182 in this.OnLoginWithFacebookRequestEvent.GetInvocationList())
				{
					if (delegate182.Target == instance)
					{
						this.OnLoginWithFacebookRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookRequest>)delegate182;
					}
				}
			}
			if (this.OnLoginWithFacebookInstantGamesIdRequestEvent != null)
			{
				foreach (Delegate delegate183 in this.OnLoginWithFacebookInstantGamesIdRequestEvent.GetInvocationList())
				{
					if (delegate183.Target == instance)
					{
						this.OnLoginWithFacebookInstantGamesIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithFacebookInstantGamesIdRequest>)delegate183;
					}
				}
			}
			if (this.OnLoginWithGameCenterRequestEvent != null)
			{
				foreach (Delegate delegate184 in this.OnLoginWithGameCenterRequestEvent.GetInvocationList())
				{
					if (delegate184.Target == instance)
					{
						this.OnLoginWithGameCenterRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithGameCenterRequest>)delegate184;
					}
				}
			}
			if (this.OnLoginWithGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate185 in this.OnLoginWithGoogleAccountRequestEvent.GetInvocationList())
				{
					if (delegate185.Target == instance)
					{
						this.OnLoginWithGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithGoogleAccountRequest>)delegate185;
					}
				}
			}
			if (this.OnLoginWithIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate186 in this.OnLoginWithIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (delegate186.Target == instance)
					{
						this.OnLoginWithIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithIOSDeviceIDRequest>)delegate186;
					}
				}
			}
			if (this.OnLoginWithKongregateRequestEvent != null)
			{
				foreach (Delegate delegate187 in this.OnLoginWithKongregateRequestEvent.GetInvocationList())
				{
					if (delegate187.Target == instance)
					{
						this.OnLoginWithKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithKongregateRequest>)delegate187;
					}
				}
			}
			if (this.OnLoginWithNintendoServiceAccountRequestEvent != null)
			{
				foreach (Delegate delegate188 in this.OnLoginWithNintendoServiceAccountRequestEvent.GetInvocationList())
				{
					if (delegate188.Target == instance)
					{
						this.OnLoginWithNintendoServiceAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithNintendoServiceAccountRequest>)delegate188;
					}
				}
			}
			if (this.OnLoginWithNintendoSwitchDeviceIdRequestEvent != null)
			{
				foreach (Delegate delegate189 in this.OnLoginWithNintendoSwitchDeviceIdRequestEvent.GetInvocationList())
				{
					if (delegate189.Target == instance)
					{
						this.OnLoginWithNintendoSwitchDeviceIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithNintendoSwitchDeviceIdRequest>)delegate189;
					}
				}
			}
			if (this.OnLoginWithOpenIdConnectRequestEvent != null)
			{
				foreach (Delegate delegate190 in this.OnLoginWithOpenIdConnectRequestEvent.GetInvocationList())
				{
					if (delegate190.Target == instance)
					{
						this.OnLoginWithOpenIdConnectRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithOpenIdConnectRequest>)delegate190;
					}
				}
			}
			if (this.OnLoginWithPlayFabRequestEvent != null)
			{
				foreach (Delegate delegate191 in this.OnLoginWithPlayFabRequestEvent.GetInvocationList())
				{
					if (delegate191.Target == instance)
					{
						this.OnLoginWithPlayFabRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithPlayFabRequest>)delegate191;
					}
				}
			}
			if (this.OnLoginWithPSNRequestEvent != null)
			{
				foreach (Delegate delegate192 in this.OnLoginWithPSNRequestEvent.GetInvocationList())
				{
					if (delegate192.Target == instance)
					{
						this.OnLoginWithPSNRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithPSNRequest>)delegate192;
					}
				}
			}
			if (this.OnLoginWithSteamRequestEvent != null)
			{
				foreach (Delegate delegate193 in this.OnLoginWithSteamRequestEvent.GetInvocationList())
				{
					if (delegate193.Target == instance)
					{
						this.OnLoginWithSteamRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithSteamRequest>)delegate193;
					}
				}
			}
			if (this.OnLoginWithTwitchRequestEvent != null)
			{
				foreach (Delegate delegate194 in this.OnLoginWithTwitchRequestEvent.GetInvocationList())
				{
					if (delegate194.Target == instance)
					{
						this.OnLoginWithTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithTwitchRequest>)delegate194;
					}
				}
			}
			if (this.OnLoginWithWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate195 in this.OnLoginWithWindowsHelloRequestEvent.GetInvocationList())
				{
					if (delegate195.Target == instance)
					{
						this.OnLoginWithWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithWindowsHelloRequest>)delegate195;
					}
				}
			}
			if (this.OnLoginWithXboxRequestEvent != null)
			{
				foreach (Delegate delegate196 in this.OnLoginWithXboxRequestEvent.GetInvocationList())
				{
					if (delegate196.Target == instance)
					{
						this.OnLoginWithXboxRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<LoginWithXboxRequest>)delegate196;
					}
				}
			}
			if (this.OnMatchmakeRequestEvent != null)
			{
				foreach (Delegate delegate197 in this.OnMatchmakeRequestEvent.GetInvocationList())
				{
					if (delegate197.Target == instance)
					{
						this.OnMatchmakeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MatchmakeRequest>)delegate197;
					}
				}
			}
			if (this.OnMatchmakeResultEvent != null)
			{
				foreach (Delegate delegate198 in this.OnMatchmakeResultEvent.GetInvocationList())
				{
					if (delegate198.Target == instance)
					{
						this.OnMatchmakeResultEvent -= (PlayFabEvents.PlayFabResultEvent<MatchmakeResult>)delegate198;
					}
				}
			}
			if (this.OnOpenTradeRequestEvent != null)
			{
				foreach (Delegate delegate199 in this.OnOpenTradeRequestEvent.GetInvocationList())
				{
					if (delegate199.Target == instance)
					{
						this.OnOpenTradeRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<OpenTradeRequest>)delegate199;
					}
				}
			}
			if (this.OnOpenTradeResultEvent != null)
			{
				foreach (Delegate delegate200 in this.OnOpenTradeResultEvent.GetInvocationList())
				{
					if (delegate200.Target == instance)
					{
						this.OnOpenTradeResultEvent -= (PlayFabEvents.PlayFabResultEvent<OpenTradeResponse>)delegate200;
					}
				}
			}
			if (this.OnPayForPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate201 in this.OnPayForPurchaseRequestEvent.GetInvocationList())
				{
					if (delegate201.Target == instance)
					{
						this.OnPayForPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PayForPurchaseRequest>)delegate201;
					}
				}
			}
			if (this.OnPayForPurchaseResultEvent != null)
			{
				foreach (Delegate delegate202 in this.OnPayForPurchaseResultEvent.GetInvocationList())
				{
					if (delegate202.Target == instance)
					{
						this.OnPayForPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<PayForPurchaseResult>)delegate202;
					}
				}
			}
			if (this.OnPurchaseItemRequestEvent != null)
			{
				foreach (Delegate delegate203 in this.OnPurchaseItemRequestEvent.GetInvocationList())
				{
					if (delegate203.Target == instance)
					{
						this.OnPurchaseItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PurchaseItemRequest>)delegate203;
					}
				}
			}
			if (this.OnPurchaseItemResultEvent != null)
			{
				foreach (Delegate delegate204 in this.OnPurchaseItemResultEvent.GetInvocationList())
				{
					if (delegate204.Target == instance)
					{
						this.OnPurchaseItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<PurchaseItemResult>)delegate204;
					}
				}
			}
			if (this.OnRedeemCouponRequestEvent != null)
			{
				foreach (Delegate delegate205 in this.OnRedeemCouponRequestEvent.GetInvocationList())
				{
					if (delegate205.Target == instance)
					{
						this.OnRedeemCouponRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RedeemCouponRequest>)delegate205;
					}
				}
			}
			if (this.OnRedeemCouponResultEvent != null)
			{
				foreach (Delegate delegate206 in this.OnRedeemCouponResultEvent.GetInvocationList())
				{
					if (delegate206.Target == instance)
					{
						this.OnRedeemCouponResultEvent -= (PlayFabEvents.PlayFabResultEvent<RedeemCouponResult>)delegate206;
					}
				}
			}
			if (this.OnRefreshPSNAuthTokenRequestEvent != null)
			{
				foreach (Delegate delegate207 in this.OnRefreshPSNAuthTokenRequestEvent.GetInvocationList())
				{
					if (delegate207.Target == instance)
					{
						this.OnRefreshPSNAuthTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RefreshPSNAuthTokenRequest>)delegate207;
					}
				}
			}
			if (this.OnRefreshPSNAuthTokenResultEvent != null)
			{
				foreach (Delegate delegate208 in this.OnRefreshPSNAuthTokenResultEvent.GetInvocationList())
				{
					if (delegate208.Target == instance)
					{
						this.OnRefreshPSNAuthTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate208;
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationRequestEvent != null)
			{
				foreach (Delegate delegate209 in this.OnRegisterForIOSPushNotificationRequestEvent.GetInvocationList())
				{
					if (delegate209.Target == instance)
					{
						this.OnRegisterForIOSPushNotificationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterForIOSPushNotificationRequest>)delegate209;
					}
				}
			}
			if (this.OnRegisterForIOSPushNotificationResultEvent != null)
			{
				foreach (Delegate delegate210 in this.OnRegisterForIOSPushNotificationResultEvent.GetInvocationList())
				{
					if (delegate210.Target == instance)
					{
						this.OnRegisterForIOSPushNotificationResultEvent -= (PlayFabEvents.PlayFabResultEvent<RegisterForIOSPushNotificationResult>)delegate210;
					}
				}
			}
			if (this.OnRegisterPlayFabUserRequestEvent != null)
			{
				foreach (Delegate delegate211 in this.OnRegisterPlayFabUserRequestEvent.GetInvocationList())
				{
					if (delegate211.Target == instance)
					{
						this.OnRegisterPlayFabUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterPlayFabUserRequest>)delegate211;
					}
				}
			}
			if (this.OnRegisterPlayFabUserResultEvent != null)
			{
				foreach (Delegate delegate212 in this.OnRegisterPlayFabUserResultEvent.GetInvocationList())
				{
					if (delegate212.Target == instance)
					{
						this.OnRegisterPlayFabUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<RegisterPlayFabUserResult>)delegate212;
					}
				}
			}
			if (this.OnRegisterWithWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate213 in this.OnRegisterWithWindowsHelloRequestEvent.GetInvocationList())
				{
					if (delegate213.Target == instance)
					{
						this.OnRegisterWithWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterWithWindowsHelloRequest>)delegate213;
					}
				}
			}
			if (this.OnRemoveContactEmailRequestEvent != null)
			{
				foreach (Delegate delegate214 in this.OnRemoveContactEmailRequestEvent.GetInvocationList())
				{
					if (delegate214.Target == instance)
					{
						this.OnRemoveContactEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveContactEmailRequest>)delegate214;
					}
				}
			}
			if (this.OnRemoveContactEmailResultEvent != null)
			{
				foreach (Delegate delegate215 in this.OnRemoveContactEmailResultEvent.GetInvocationList())
				{
					if (delegate215.Target == instance)
					{
						this.OnRemoveContactEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveContactEmailResult>)delegate215;
					}
				}
			}
			if (this.OnRemoveFriendRequestEvent != null)
			{
				foreach (Delegate delegate216 in this.OnRemoveFriendRequestEvent.GetInvocationList())
				{
					if (delegate216.Target == instance)
					{
						this.OnRemoveFriendRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveFriendRequest>)delegate216;
					}
				}
			}
			if (this.OnRemoveFriendResultEvent != null)
			{
				foreach (Delegate delegate217 in this.OnRemoveFriendResultEvent.GetInvocationList())
				{
					if (delegate217.Target == instance)
					{
						this.OnRemoveFriendResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveFriendResult>)delegate217;
					}
				}
			}
			if (this.OnRemoveGenericIDRequestEvent != null)
			{
				foreach (Delegate delegate218 in this.OnRemoveGenericIDRequestEvent.GetInvocationList())
				{
					if (delegate218.Target == instance)
					{
						this.OnRemoveGenericIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGenericIDRequest>)delegate218;
					}
				}
			}
			if (this.OnRemoveGenericIDResultEvent != null)
			{
				foreach (Delegate delegate219 in this.OnRemoveGenericIDResultEvent.GetInvocationList())
				{
					if (delegate219.Target == instance)
					{
						this.OnRemoveGenericIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveGenericIDResult>)delegate219;
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate220 in this.OnRemoveSharedGroupMembersRequestEvent.GetInvocationList())
				{
					if (delegate220.Target == instance)
					{
						this.OnRemoveSharedGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveSharedGroupMembersRequest>)delegate220;
					}
				}
			}
			if (this.OnRemoveSharedGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate221 in this.OnRemoveSharedGroupMembersResultEvent.GetInvocationList())
				{
					if (delegate221.Target == instance)
					{
						this.OnRemoveSharedGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveSharedGroupMembersResult>)delegate221;
					}
				}
			}
			if (this.OnReportAdActivityRequestEvent != null)
			{
				foreach (Delegate delegate222 in this.OnReportAdActivityRequestEvent.GetInvocationList())
				{
					if (delegate222.Target == instance)
					{
						this.OnReportAdActivityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ReportAdActivityRequest>)delegate222;
					}
				}
			}
			if (this.OnReportAdActivityResultEvent != null)
			{
				foreach (Delegate delegate223 in this.OnReportAdActivityResultEvent.GetInvocationList())
				{
					if (delegate223.Target == instance)
					{
						this.OnReportAdActivityResultEvent -= (PlayFabEvents.PlayFabResultEvent<ReportAdActivityResult>)delegate223;
					}
				}
			}
			if (this.OnReportDeviceInfoRequestEvent != null)
			{
				foreach (Delegate delegate224 in this.OnReportDeviceInfoRequestEvent.GetInvocationList())
				{
					if (delegate224.Target == instance)
					{
						this.OnReportDeviceInfoRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeviceInfoRequest>)delegate224;
					}
				}
			}
			if (this.OnReportDeviceInfoResultEvent != null)
			{
				foreach (Delegate delegate225 in this.OnReportDeviceInfoResultEvent.GetInvocationList())
				{
					if (delegate225.Target == instance)
					{
						this.OnReportDeviceInfoResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate225;
					}
				}
			}
			if (this.OnReportPlayerRequestEvent != null)
			{
				foreach (Delegate delegate226 in this.OnReportPlayerRequestEvent.GetInvocationList())
				{
					if (delegate226.Target == instance)
					{
						this.OnReportPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ReportPlayerClientRequest>)delegate226;
					}
				}
			}
			if (this.OnReportPlayerResultEvent != null)
			{
				foreach (Delegate delegate227 in this.OnReportPlayerResultEvent.GetInvocationList())
				{
					if (delegate227.Target == instance)
					{
						this.OnReportPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<ReportPlayerClientResult>)delegate227;
					}
				}
			}
			if (this.OnRestoreIOSPurchasesRequestEvent != null)
			{
				foreach (Delegate delegate228 in this.OnRestoreIOSPurchasesRequestEvent.GetInvocationList())
				{
					if (delegate228.Target == instance)
					{
						this.OnRestoreIOSPurchasesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RestoreIOSPurchasesRequest>)delegate228;
					}
				}
			}
			if (this.OnRestoreIOSPurchasesResultEvent != null)
			{
				foreach (Delegate delegate229 in this.OnRestoreIOSPurchasesResultEvent.GetInvocationList())
				{
					if (delegate229.Target == instance)
					{
						this.OnRestoreIOSPurchasesResultEvent -= (PlayFabEvents.PlayFabResultEvent<RestoreIOSPurchasesResult>)delegate229;
					}
				}
			}
			if (this.OnRewardAdActivityRequestEvent != null)
			{
				foreach (Delegate delegate230 in this.OnRewardAdActivityRequestEvent.GetInvocationList())
				{
					if (delegate230.Target == instance)
					{
						this.OnRewardAdActivityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RewardAdActivityRequest>)delegate230;
					}
				}
			}
			if (this.OnRewardAdActivityResultEvent != null)
			{
				foreach (Delegate delegate231 in this.OnRewardAdActivityResultEvent.GetInvocationList())
				{
					if (delegate231.Target == instance)
					{
						this.OnRewardAdActivityResultEvent -= (PlayFabEvents.PlayFabResultEvent<RewardAdActivityResult>)delegate231;
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailRequestEvent != null)
			{
				foreach (Delegate delegate232 in this.OnSendAccountRecoveryEmailRequestEvent.GetInvocationList())
				{
					if (delegate232.Target == instance)
					{
						this.OnSendAccountRecoveryEmailRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SendAccountRecoveryEmailRequest>)delegate232;
					}
				}
			}
			if (this.OnSendAccountRecoveryEmailResultEvent != null)
			{
				foreach (Delegate delegate233 in this.OnSendAccountRecoveryEmailResultEvent.GetInvocationList())
				{
					if (delegate233.Target == instance)
					{
						this.OnSendAccountRecoveryEmailResultEvent -= (PlayFabEvents.PlayFabResultEvent<SendAccountRecoveryEmailResult>)delegate233;
					}
				}
			}
			if (this.OnSetFriendTagsRequestEvent != null)
			{
				foreach (Delegate delegate234 in this.OnSetFriendTagsRequestEvent.GetInvocationList())
				{
					if (delegate234.Target == instance)
					{
						this.OnSetFriendTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetFriendTagsRequest>)delegate234;
					}
				}
			}
			if (this.OnSetFriendTagsResultEvent != null)
			{
				foreach (Delegate delegate235 in this.OnSetFriendTagsResultEvent.GetInvocationList())
				{
					if (delegate235.Target == instance)
					{
						this.OnSetFriendTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetFriendTagsResult>)delegate235;
					}
				}
			}
			if (this.OnSetPlayerSecretRequestEvent != null)
			{
				foreach (Delegate delegate236 in this.OnSetPlayerSecretRequestEvent.GetInvocationList())
				{
					if (delegate236.Target == instance)
					{
						this.OnSetPlayerSecretRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetPlayerSecretRequest>)delegate236;
					}
				}
			}
			if (this.OnSetPlayerSecretResultEvent != null)
			{
				foreach (Delegate delegate237 in this.OnSetPlayerSecretResultEvent.GetInvocationList())
				{
					if (delegate237.Target == instance)
					{
						this.OnSetPlayerSecretResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetPlayerSecretResult>)delegate237;
					}
				}
			}
			if (this.OnStartGameRequestEvent != null)
			{
				foreach (Delegate delegate238 in this.OnStartGameRequestEvent.GetInvocationList())
				{
					if (delegate238.Target == instance)
					{
						this.OnStartGameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StartGameRequest>)delegate238;
					}
				}
			}
			if (this.OnStartGameResultEvent != null)
			{
				foreach (Delegate delegate239 in this.OnStartGameResultEvent.GetInvocationList())
				{
					if (delegate239.Target == instance)
					{
						this.OnStartGameResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartGameResult>)delegate239;
					}
				}
			}
			if (this.OnStartPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate240 in this.OnStartPurchaseRequestEvent.GetInvocationList())
				{
					if (delegate240.Target == instance)
					{
						this.OnStartPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StartPurchaseRequest>)delegate240;
					}
				}
			}
			if (this.OnStartPurchaseResultEvent != null)
			{
				foreach (Delegate delegate241 in this.OnStartPurchaseResultEvent.GetInvocationList())
				{
					if (delegate241.Target == instance)
					{
						this.OnStartPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<StartPurchaseResult>)delegate241;
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyRequestEvent != null)
			{
				foreach (Delegate delegate242 in this.OnSubtractUserVirtualCurrencyRequestEvent.GetInvocationList())
				{
					if (delegate242.Target == instance)
					{
						this.OnSubtractUserVirtualCurrencyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SubtractUserVirtualCurrencyRequest>)delegate242;
					}
				}
			}
			if (this.OnSubtractUserVirtualCurrencyResultEvent != null)
			{
				foreach (Delegate delegate243 in this.OnSubtractUserVirtualCurrencyResultEvent.GetInvocationList())
				{
					if (delegate243.Target == instance)
					{
						this.OnSubtractUserVirtualCurrencyResultEvent -= (PlayFabEvents.PlayFabResultEvent<ModifyUserVirtualCurrencyResult>)delegate243;
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate244 in this.OnUnlinkAndroidDeviceIDRequestEvent.GetInvocationList())
				{
					if (delegate244.Target == instance)
					{
						this.OnUnlinkAndroidDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkAndroidDeviceIDRequest>)delegate244;
					}
				}
			}
			if (this.OnUnlinkAndroidDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate245 in this.OnUnlinkAndroidDeviceIDResultEvent.GetInvocationList())
				{
					if (delegate245.Target == instance)
					{
						this.OnUnlinkAndroidDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkAndroidDeviceIDResult>)delegate245;
					}
				}
			}
			if (this.OnUnlinkAppleRequestEvent != null)
			{
				foreach (Delegate delegate246 in this.OnUnlinkAppleRequestEvent.GetInvocationList())
				{
					if (delegate246.Target == instance)
					{
						this.OnUnlinkAppleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkAppleRequest>)delegate246;
					}
				}
			}
			if (this.OnUnlinkAppleResultEvent != null)
			{
				foreach (Delegate delegate247 in this.OnUnlinkAppleResultEvent.GetInvocationList())
				{
					if (delegate247.Target == instance)
					{
						this.OnUnlinkAppleResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate247;
					}
				}
			}
			if (this.OnUnlinkCustomIDRequestEvent != null)
			{
				foreach (Delegate delegate248 in this.OnUnlinkCustomIDRequestEvent.GetInvocationList())
				{
					if (delegate248.Target == instance)
					{
						this.OnUnlinkCustomIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkCustomIDRequest>)delegate248;
					}
				}
			}
			if (this.OnUnlinkCustomIDResultEvent != null)
			{
				foreach (Delegate delegate249 in this.OnUnlinkCustomIDResultEvent.GetInvocationList())
				{
					if (delegate249.Target == instance)
					{
						this.OnUnlinkCustomIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkCustomIDResult>)delegate249;
					}
				}
			}
			if (this.OnUnlinkFacebookAccountRequestEvent != null)
			{
				foreach (Delegate delegate250 in this.OnUnlinkFacebookAccountRequestEvent.GetInvocationList())
				{
					if (delegate250.Target == instance)
					{
						this.OnUnlinkFacebookAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookAccountRequest>)delegate250;
					}
				}
			}
			if (this.OnUnlinkFacebookAccountResultEvent != null)
			{
				foreach (Delegate delegate251 in this.OnUnlinkFacebookAccountResultEvent.GetInvocationList())
				{
					if (delegate251.Target == instance)
					{
						this.OnUnlinkFacebookAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkFacebookAccountResult>)delegate251;
					}
				}
			}
			if (this.OnUnlinkFacebookInstantGamesIdRequestEvent != null)
			{
				foreach (Delegate delegate252 in this.OnUnlinkFacebookInstantGamesIdRequestEvent.GetInvocationList())
				{
					if (delegate252.Target == instance)
					{
						this.OnUnlinkFacebookInstantGamesIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkFacebookInstantGamesIdRequest>)delegate252;
					}
				}
			}
			if (this.OnUnlinkFacebookInstantGamesIdResultEvent != null)
			{
				foreach (Delegate delegate253 in this.OnUnlinkFacebookInstantGamesIdResultEvent.GetInvocationList())
				{
					if (delegate253.Target == instance)
					{
						this.OnUnlinkFacebookInstantGamesIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkFacebookInstantGamesIdResult>)delegate253;
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountRequestEvent != null)
			{
				foreach (Delegate delegate254 in this.OnUnlinkGameCenterAccountRequestEvent.GetInvocationList())
				{
					if (delegate254.Target == instance)
					{
						this.OnUnlinkGameCenterAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkGameCenterAccountRequest>)delegate254;
					}
				}
			}
			if (this.OnUnlinkGameCenterAccountResultEvent != null)
			{
				foreach (Delegate delegate255 in this.OnUnlinkGameCenterAccountResultEvent.GetInvocationList())
				{
					if (delegate255.Target == instance)
					{
						this.OnUnlinkGameCenterAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkGameCenterAccountResult>)delegate255;
					}
				}
			}
			if (this.OnUnlinkGoogleAccountRequestEvent != null)
			{
				foreach (Delegate delegate256 in this.OnUnlinkGoogleAccountRequestEvent.GetInvocationList())
				{
					if (delegate256.Target == instance)
					{
						this.OnUnlinkGoogleAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkGoogleAccountRequest>)delegate256;
					}
				}
			}
			if (this.OnUnlinkGoogleAccountResultEvent != null)
			{
				foreach (Delegate delegate257 in this.OnUnlinkGoogleAccountResultEvent.GetInvocationList())
				{
					if (delegate257.Target == instance)
					{
						this.OnUnlinkGoogleAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkGoogleAccountResult>)delegate257;
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDRequestEvent != null)
			{
				foreach (Delegate delegate258 in this.OnUnlinkIOSDeviceIDRequestEvent.GetInvocationList())
				{
					if (delegate258.Target == instance)
					{
						this.OnUnlinkIOSDeviceIDRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkIOSDeviceIDRequest>)delegate258;
					}
				}
			}
			if (this.OnUnlinkIOSDeviceIDResultEvent != null)
			{
				foreach (Delegate delegate259 in this.OnUnlinkIOSDeviceIDResultEvent.GetInvocationList())
				{
					if (delegate259.Target == instance)
					{
						this.OnUnlinkIOSDeviceIDResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkIOSDeviceIDResult>)delegate259;
					}
				}
			}
			if (this.OnUnlinkKongregateRequestEvent != null)
			{
				foreach (Delegate delegate260 in this.OnUnlinkKongregateRequestEvent.GetInvocationList())
				{
					if (delegate260.Target == instance)
					{
						this.OnUnlinkKongregateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkKongregateAccountRequest>)delegate260;
					}
				}
			}
			if (this.OnUnlinkKongregateResultEvent != null)
			{
				foreach (Delegate delegate261 in this.OnUnlinkKongregateResultEvent.GetInvocationList())
				{
					if (delegate261.Target == instance)
					{
						this.OnUnlinkKongregateResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkKongregateAccountResult>)delegate261;
					}
				}
			}
			if (this.OnUnlinkNintendoServiceAccountRequestEvent != null)
			{
				foreach (Delegate delegate262 in this.OnUnlinkNintendoServiceAccountRequestEvent.GetInvocationList())
				{
					if (delegate262.Target == instance)
					{
						this.OnUnlinkNintendoServiceAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkNintendoServiceAccountRequest>)delegate262;
					}
				}
			}
			if (this.OnUnlinkNintendoServiceAccountResultEvent != null)
			{
				foreach (Delegate delegate263 in this.OnUnlinkNintendoServiceAccountResultEvent.GetInvocationList())
				{
					if (delegate263.Target == instance)
					{
						this.OnUnlinkNintendoServiceAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate263;
					}
				}
			}
			if (this.OnUnlinkNintendoSwitchDeviceIdRequestEvent != null)
			{
				foreach (Delegate delegate264 in this.OnUnlinkNintendoSwitchDeviceIdRequestEvent.GetInvocationList())
				{
					if (delegate264.Target == instance)
					{
						this.OnUnlinkNintendoSwitchDeviceIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkNintendoSwitchDeviceIdRequest>)delegate264;
					}
				}
			}
			if (this.OnUnlinkNintendoSwitchDeviceIdResultEvent != null)
			{
				foreach (Delegate delegate265 in this.OnUnlinkNintendoSwitchDeviceIdResultEvent.GetInvocationList())
				{
					if (delegate265.Target == instance)
					{
						this.OnUnlinkNintendoSwitchDeviceIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkNintendoSwitchDeviceIdResult>)delegate265;
					}
				}
			}
			if (this.OnUnlinkOpenIdConnectRequestEvent != null)
			{
				foreach (Delegate delegate266 in this.OnUnlinkOpenIdConnectRequestEvent.GetInvocationList())
				{
					if (delegate266.Target == instance)
					{
						this.OnUnlinkOpenIdConnectRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkOpenIdConnectRequest>)delegate266;
					}
				}
			}
			if (this.OnUnlinkOpenIdConnectResultEvent != null)
			{
				foreach (Delegate delegate267 in this.OnUnlinkOpenIdConnectResultEvent.GetInvocationList())
				{
					if (delegate267.Target == instance)
					{
						this.OnUnlinkOpenIdConnectResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate267;
					}
				}
			}
			if (this.OnUnlinkPSNAccountRequestEvent != null)
			{
				foreach (Delegate delegate268 in this.OnUnlinkPSNAccountRequestEvent.GetInvocationList())
				{
					if (delegate268.Target == instance)
					{
						this.OnUnlinkPSNAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkPSNAccountRequest>)delegate268;
					}
				}
			}
			if (this.OnUnlinkPSNAccountResultEvent != null)
			{
				foreach (Delegate delegate269 in this.OnUnlinkPSNAccountResultEvent.GetInvocationList())
				{
					if (delegate269.Target == instance)
					{
						this.OnUnlinkPSNAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkPSNAccountResult>)delegate269;
					}
				}
			}
			if (this.OnUnlinkSteamAccountRequestEvent != null)
			{
				foreach (Delegate delegate270 in this.OnUnlinkSteamAccountRequestEvent.GetInvocationList())
				{
					if (delegate270.Target == instance)
					{
						this.OnUnlinkSteamAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkSteamAccountRequest>)delegate270;
					}
				}
			}
			if (this.OnUnlinkSteamAccountResultEvent != null)
			{
				foreach (Delegate delegate271 in this.OnUnlinkSteamAccountResultEvent.GetInvocationList())
				{
					if (delegate271.Target == instance)
					{
						this.OnUnlinkSteamAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkSteamAccountResult>)delegate271;
					}
				}
			}
			if (this.OnUnlinkTwitchRequestEvent != null)
			{
				foreach (Delegate delegate272 in this.OnUnlinkTwitchRequestEvent.GetInvocationList())
				{
					if (delegate272.Target == instance)
					{
						this.OnUnlinkTwitchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkTwitchAccountRequest>)delegate272;
					}
				}
			}
			if (this.OnUnlinkTwitchResultEvent != null)
			{
				foreach (Delegate delegate273 in this.OnUnlinkTwitchResultEvent.GetInvocationList())
				{
					if (delegate273.Target == instance)
					{
						this.OnUnlinkTwitchResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkTwitchAccountResult>)delegate273;
					}
				}
			}
			if (this.OnUnlinkWindowsHelloRequestEvent != null)
			{
				foreach (Delegate delegate274 in this.OnUnlinkWindowsHelloRequestEvent.GetInvocationList())
				{
					if (delegate274.Target == instance)
					{
						this.OnUnlinkWindowsHelloRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkWindowsHelloAccountRequest>)delegate274;
					}
				}
			}
			if (this.OnUnlinkWindowsHelloResultEvent != null)
			{
				foreach (Delegate delegate275 in this.OnUnlinkWindowsHelloResultEvent.GetInvocationList())
				{
					if (delegate275.Target == instance)
					{
						this.OnUnlinkWindowsHelloResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkWindowsHelloAccountResponse>)delegate275;
					}
				}
			}
			if (this.OnUnlinkXboxAccountRequestEvent != null)
			{
				foreach (Delegate delegate276 in this.OnUnlinkXboxAccountRequestEvent.GetInvocationList())
				{
					if (delegate276.Target == instance)
					{
						this.OnUnlinkXboxAccountRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlinkXboxAccountRequest>)delegate276;
					}
				}
			}
			if (this.OnUnlinkXboxAccountResultEvent != null)
			{
				foreach (Delegate delegate277 in this.OnUnlinkXboxAccountResultEvent.GetInvocationList())
				{
					if (delegate277.Target == instance)
					{
						this.OnUnlinkXboxAccountResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlinkXboxAccountResult>)delegate277;
					}
				}
			}
			if (this.OnUnlockContainerInstanceRequestEvent != null)
			{
				foreach (Delegate delegate278 in this.OnUnlockContainerInstanceRequestEvent.GetInvocationList())
				{
					if (delegate278.Target == instance)
					{
						this.OnUnlockContainerInstanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlockContainerInstanceRequest>)delegate278;
					}
				}
			}
			if (this.OnUnlockContainerInstanceResultEvent != null)
			{
				foreach (Delegate delegate279 in this.OnUnlockContainerInstanceResultEvent.GetInvocationList())
				{
					if (delegate279.Target == instance)
					{
						this.OnUnlockContainerInstanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult>)delegate279;
					}
				}
			}
			if (this.OnUnlockContainerItemRequestEvent != null)
			{
				foreach (Delegate delegate280 in this.OnUnlockContainerItemRequestEvent.GetInvocationList())
				{
					if (delegate280.Target == instance)
					{
						this.OnUnlockContainerItemRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnlockContainerItemRequest>)delegate280;
					}
				}
			}
			if (this.OnUnlockContainerItemResultEvent != null)
			{
				foreach (Delegate delegate281 in this.OnUnlockContainerItemResultEvent.GetInvocationList())
				{
					if (delegate281.Target == instance)
					{
						this.OnUnlockContainerItemResultEvent -= (PlayFabEvents.PlayFabResultEvent<UnlockContainerItemResult>)delegate281;
					}
				}
			}
			if (this.OnUpdateAvatarUrlRequestEvent != null)
			{
				foreach (Delegate delegate282 in this.OnUpdateAvatarUrlRequestEvent.GetInvocationList())
				{
					if (delegate282.Target == instance)
					{
						this.OnUpdateAvatarUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateAvatarUrlRequest>)delegate282;
					}
				}
			}
			if (this.OnUpdateAvatarUrlResultEvent != null)
			{
				foreach (Delegate delegate283 in this.OnUpdateAvatarUrlResultEvent.GetInvocationList())
				{
					if (delegate283.Target == instance)
					{
						this.OnUpdateAvatarUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ClientModels.EmptyResponse>)delegate283;
					}
				}
			}
			if (this.OnUpdateCharacterDataRequestEvent != null)
			{
				foreach (Delegate delegate284 in this.OnUpdateCharacterDataRequestEvent.GetInvocationList())
				{
					if (delegate284.Target == instance)
					{
						this.OnUpdateCharacterDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCharacterDataRequest>)delegate284;
					}
				}
			}
			if (this.OnUpdateCharacterDataResultEvent != null)
			{
				foreach (Delegate delegate285 in this.OnUpdateCharacterDataResultEvent.GetInvocationList())
				{
					if (delegate285.Target == instance)
					{
						this.OnUpdateCharacterDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCharacterDataResult>)delegate285;
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate286 in this.OnUpdateCharacterStatisticsRequestEvent.GetInvocationList())
				{
					if (delegate286.Target == instance)
					{
						this.OnUpdateCharacterStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateCharacterStatisticsRequest>)delegate286;
					}
				}
			}
			if (this.OnUpdateCharacterStatisticsResultEvent != null)
			{
				foreach (Delegate delegate287 in this.OnUpdateCharacterStatisticsResultEvent.GetInvocationList())
				{
					if (delegate287.Target == instance)
					{
						this.OnUpdateCharacterStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateCharacterStatisticsResult>)delegate287;
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate288 in this.OnUpdatePlayerStatisticsRequestEvent.GetInvocationList())
				{
					if (delegate288.Target == instance)
					{
						this.OnUpdatePlayerStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdatePlayerStatisticsRequest>)delegate288;
					}
				}
			}
			if (this.OnUpdatePlayerStatisticsResultEvent != null)
			{
				foreach (Delegate delegate289 in this.OnUpdatePlayerStatisticsResultEvent.GetInvocationList())
				{
					if (delegate289.Target == instance)
					{
						this.OnUpdatePlayerStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdatePlayerStatisticsResult>)delegate289;
					}
				}
			}
			if (this.OnUpdateSharedGroupDataRequestEvent != null)
			{
				foreach (Delegate delegate290 in this.OnUpdateSharedGroupDataRequestEvent.GetInvocationList())
				{
					if (delegate290.Target == instance)
					{
						this.OnUpdateSharedGroupDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateSharedGroupDataRequest>)delegate290;
					}
				}
			}
			if (this.OnUpdateSharedGroupDataResultEvent != null)
			{
				foreach (Delegate delegate291 in this.OnUpdateSharedGroupDataResultEvent.GetInvocationList())
				{
					if (delegate291.Target == instance)
					{
						this.OnUpdateSharedGroupDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateSharedGroupDataResult>)delegate291;
					}
				}
			}
			if (this.OnUpdateUserDataRequestEvent != null)
			{
				foreach (Delegate delegate292 in this.OnUpdateUserDataRequestEvent.GetInvocationList())
				{
					if (delegate292.Target == instance)
					{
						this.OnUpdateUserDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest>)delegate292;
					}
				}
			}
			if (this.OnUpdateUserDataResultEvent != null)
			{
				foreach (Delegate delegate293 in this.OnUpdateUserDataResultEvent.GetInvocationList())
				{
					if (delegate293.Target == instance)
					{
						this.OnUpdateUserDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult>)delegate293;
					}
				}
			}
			if (this.OnUpdateUserPublisherDataRequestEvent != null)
			{
				foreach (Delegate delegate294 in this.OnUpdateUserPublisherDataRequestEvent.GetInvocationList())
				{
					if (delegate294.Target == instance)
					{
						this.OnUpdateUserPublisherDataRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserDataRequest>)delegate294;
					}
				}
			}
			if (this.OnUpdateUserPublisherDataResultEvent != null)
			{
				foreach (Delegate delegate295 in this.OnUpdateUserPublisherDataResultEvent.GetInvocationList())
				{
					if (delegate295.Target == instance)
					{
						this.OnUpdateUserPublisherDataResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateUserDataResult>)delegate295;
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameRequestEvent != null)
			{
				foreach (Delegate delegate296 in this.OnUpdateUserTitleDisplayNameRequestEvent.GetInvocationList())
				{
					if (delegate296.Target == instance)
					{
						this.OnUpdateUserTitleDisplayNameRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateUserTitleDisplayNameRequest>)delegate296;
					}
				}
			}
			if (this.OnUpdateUserTitleDisplayNameResultEvent != null)
			{
				foreach (Delegate delegate297 in this.OnUpdateUserTitleDisplayNameResultEvent.GetInvocationList())
				{
					if (delegate297.Target == instance)
					{
						this.OnUpdateUserTitleDisplayNameResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateUserTitleDisplayNameResult>)delegate297;
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptRequestEvent != null)
			{
				foreach (Delegate delegate298 in this.OnValidateAmazonIAPReceiptRequestEvent.GetInvocationList())
				{
					if (delegate298.Target == instance)
					{
						this.OnValidateAmazonIAPReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateAmazonReceiptRequest>)delegate298;
					}
				}
			}
			if (this.OnValidateAmazonIAPReceiptResultEvent != null)
			{
				foreach (Delegate delegate299 in this.OnValidateAmazonIAPReceiptResultEvent.GetInvocationList())
				{
					if (delegate299.Target == instance)
					{
						this.OnValidateAmazonIAPReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateAmazonReceiptResult>)delegate299;
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseRequestEvent != null)
			{
				foreach (Delegate delegate300 in this.OnValidateGooglePlayPurchaseRequestEvent.GetInvocationList())
				{
					if (delegate300.Target == instance)
					{
						this.OnValidateGooglePlayPurchaseRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateGooglePlayPurchaseRequest>)delegate300;
					}
				}
			}
			if (this.OnValidateGooglePlayPurchaseResultEvent != null)
			{
				foreach (Delegate delegate301 in this.OnValidateGooglePlayPurchaseResultEvent.GetInvocationList())
				{
					if (delegate301.Target == instance)
					{
						this.OnValidateGooglePlayPurchaseResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateGooglePlayPurchaseResult>)delegate301;
					}
				}
			}
			if (this.OnValidateIOSReceiptRequestEvent != null)
			{
				foreach (Delegate delegate302 in this.OnValidateIOSReceiptRequestEvent.GetInvocationList())
				{
					if (delegate302.Target == instance)
					{
						this.OnValidateIOSReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateIOSReceiptRequest>)delegate302;
					}
				}
			}
			if (this.OnValidateIOSReceiptResultEvent != null)
			{
				foreach (Delegate delegate303 in this.OnValidateIOSReceiptResultEvent.GetInvocationList())
				{
					if (delegate303.Target == instance)
					{
						this.OnValidateIOSReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateIOSReceiptResult>)delegate303;
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptRequestEvent != null)
			{
				foreach (Delegate delegate304 in this.OnValidateWindowsStoreReceiptRequestEvent.GetInvocationList())
				{
					if (delegate304.Target == instance)
					{
						this.OnValidateWindowsStoreReceiptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateWindowsReceiptRequest>)delegate304;
					}
				}
			}
			if (this.OnValidateWindowsStoreReceiptResultEvent != null)
			{
				foreach (Delegate delegate305 in this.OnValidateWindowsStoreReceiptResultEvent.GetInvocationList())
				{
					if (delegate305.Target == instance)
					{
						this.OnValidateWindowsStoreReceiptResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateWindowsReceiptResult>)delegate305;
					}
				}
			}
			if (this.OnWriteCharacterEventRequestEvent != null)
			{
				foreach (Delegate delegate306 in this.OnWriteCharacterEventRequestEvent.GetInvocationList())
				{
					if (delegate306.Target == instance)
					{
						this.OnWriteCharacterEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteClientCharacterEventRequest>)delegate306;
					}
				}
			}
			if (this.OnWriteCharacterEventResultEvent != null)
			{
				foreach (Delegate delegate307 in this.OnWriteCharacterEventResultEvent.GetInvocationList())
				{
					if (delegate307.Target == instance)
					{
						this.OnWriteCharacterEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventResponse>)delegate307;
					}
				}
			}
			if (this.OnWritePlayerEventRequestEvent != null)
			{
				foreach (Delegate delegate308 in this.OnWritePlayerEventRequestEvent.GetInvocationList())
				{
					if (delegate308.Target == instance)
					{
						this.OnWritePlayerEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteClientPlayerEventRequest>)delegate308;
					}
				}
			}
			if (this.OnWritePlayerEventResultEvent != null)
			{
				foreach (Delegate delegate309 in this.OnWritePlayerEventResultEvent.GetInvocationList())
				{
					if (delegate309.Target == instance)
					{
						this.OnWritePlayerEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventResponse>)delegate309;
					}
				}
			}
			if (this.OnWriteTitleEventRequestEvent != null)
			{
				foreach (Delegate delegate310 in this.OnWriteTitleEventRequestEvent.GetInvocationList())
				{
					if (delegate310.Target == instance)
					{
						this.OnWriteTitleEventRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteTitleEventRequest>)delegate310;
					}
				}
			}
			if (this.OnWriteTitleEventResultEvent != null)
			{
				foreach (Delegate delegate311 in this.OnWriteTitleEventResultEvent.GetInvocationList())
				{
					if (delegate311.Target == instance)
					{
						this.OnWriteTitleEventResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventResponse>)delegate311;
					}
				}
			}
			if (this.OnAuthenticationGetEntityTokenRequestEvent != null)
			{
				foreach (Delegate delegate312 in this.OnAuthenticationGetEntityTokenRequestEvent.GetInvocationList())
				{
					if (delegate312.Target == instance)
					{
						this.OnAuthenticationGetEntityTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetEntityTokenRequest>)delegate312;
					}
				}
			}
			if (this.OnAuthenticationGetEntityTokenResultEvent != null)
			{
				foreach (Delegate delegate313 in this.OnAuthenticationGetEntityTokenResultEvent.GetInvocationList())
				{
					if (delegate313.Target == instance)
					{
						this.OnAuthenticationGetEntityTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetEntityTokenResponse>)delegate313;
					}
				}
			}
			if (this.OnAuthenticationValidateEntityTokenRequestEvent != null)
			{
				foreach (Delegate delegate314 in this.OnAuthenticationValidateEntityTokenRequestEvent.GetInvocationList())
				{
					if (delegate314.Target == instance)
					{
						this.OnAuthenticationValidateEntityTokenRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ValidateEntityTokenRequest>)delegate314;
					}
				}
			}
			if (this.OnAuthenticationValidateEntityTokenResultEvent != null)
			{
				foreach (Delegate delegate315 in this.OnAuthenticationValidateEntityTokenResultEvent.GetInvocationList())
				{
					if (delegate315.Target == instance)
					{
						this.OnAuthenticationValidateEntityTokenResultEvent -= (PlayFabEvents.PlayFabResultEvent<ValidateEntityTokenResponse>)delegate315;
					}
				}
			}
			if (this.OnCloudScriptExecuteEntityCloudScriptRequestEvent != null)
			{
				foreach (Delegate delegate316 in this.OnCloudScriptExecuteEntityCloudScriptRequestEvent.GetInvocationList())
				{
					if (delegate316.Target == instance)
					{
						this.OnCloudScriptExecuteEntityCloudScriptRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteEntityCloudScriptRequest>)delegate316;
					}
				}
			}
			if (this.OnCloudScriptExecuteEntityCloudScriptResultEvent != null)
			{
				foreach (Delegate delegate317 in this.OnCloudScriptExecuteEntityCloudScriptResultEvent.GetInvocationList())
				{
					if (delegate317.Target == instance)
					{
						this.OnCloudScriptExecuteEntityCloudScriptResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.ExecuteCloudScriptResult>)delegate317;
					}
				}
			}
			if (this.OnCloudScriptExecuteFunctionRequestEvent != null)
			{
				foreach (Delegate delegate318 in this.OnCloudScriptExecuteFunctionRequestEvent.GetInvocationList())
				{
					if (delegate318.Target == instance)
					{
						this.OnCloudScriptExecuteFunctionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ExecuteFunctionRequest>)delegate318;
					}
				}
			}
			if (this.OnCloudScriptExecuteFunctionResultEvent != null)
			{
				foreach (Delegate delegate319 in this.OnCloudScriptExecuteFunctionResultEvent.GetInvocationList())
				{
					if (delegate319.Target == instance)
					{
						this.OnCloudScriptExecuteFunctionResultEvent -= (PlayFabEvents.PlayFabResultEvent<ExecuteFunctionResult>)delegate319;
					}
				}
			}
			if (this.OnCloudScriptListFunctionsRequestEvent != null)
			{
				foreach (Delegate delegate320 in this.OnCloudScriptListFunctionsRequestEvent.GetInvocationList())
				{
					if (delegate320.Target == instance)
					{
						this.OnCloudScriptListFunctionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListFunctionsRequest>)delegate320;
					}
				}
			}
			if (this.OnCloudScriptListFunctionsResultEvent != null)
			{
				foreach (Delegate delegate321 in this.OnCloudScriptListFunctionsResultEvent.GetInvocationList())
				{
					if (delegate321.Target == instance)
					{
						this.OnCloudScriptListFunctionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListFunctionsResult>)delegate321;
					}
				}
			}
			if (this.OnCloudScriptListHttpFunctionsRequestEvent != null)
			{
				foreach (Delegate delegate322 in this.OnCloudScriptListHttpFunctionsRequestEvent.GetInvocationList())
				{
					if (delegate322.Target == instance)
					{
						this.OnCloudScriptListHttpFunctionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListFunctionsRequest>)delegate322;
					}
				}
			}
			if (this.OnCloudScriptListHttpFunctionsResultEvent != null)
			{
				foreach (Delegate delegate323 in this.OnCloudScriptListHttpFunctionsResultEvent.GetInvocationList())
				{
					if (delegate323.Target == instance)
					{
						this.OnCloudScriptListHttpFunctionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListHttpFunctionsResult>)delegate323;
					}
				}
			}
			if (this.OnCloudScriptListQueuedFunctionsRequestEvent != null)
			{
				foreach (Delegate delegate324 in this.OnCloudScriptListQueuedFunctionsRequestEvent.GetInvocationList())
				{
					if (delegate324.Target == instance)
					{
						this.OnCloudScriptListQueuedFunctionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListFunctionsRequest>)delegate324;
					}
				}
			}
			if (this.OnCloudScriptListQueuedFunctionsResultEvent != null)
			{
				foreach (Delegate delegate325 in this.OnCloudScriptListQueuedFunctionsResultEvent.GetInvocationList())
				{
					if (delegate325.Target == instance)
					{
						this.OnCloudScriptListQueuedFunctionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListQueuedFunctionsResult>)delegate325;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent != null)
			{
				foreach (Delegate delegate326 in this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent.GetInvocationList())
				{
					if (delegate326.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForEntityTriggeredActionRequest>)delegate326;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent != null)
			{
				foreach (Delegate delegate327 in this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent.GetInvocationList())
				{
					if (delegate327.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate327;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent != null)
			{
				foreach (Delegate delegate328 in this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent.GetInvocationList())
				{
					if (delegate328.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForFunctionExecutionRequest>)delegate328;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent != null)
			{
				foreach (Delegate delegate329 in this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent.GetInvocationList())
				{
					if (delegate329.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate329;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent != null)
			{
				foreach (Delegate delegate330 in this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent.GetInvocationList())
				{
					if (delegate330.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForPlayerTriggeredActionRequest>)delegate330;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent != null)
			{
				foreach (Delegate delegate331 in this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent.GetInvocationList())
				{
					if (delegate331.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate331;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent != null)
			{
				foreach (Delegate delegate332 in this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent.GetInvocationList())
				{
					if (delegate332.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<PostFunctionResultForScheduledTaskRequest>)delegate332;
					}
				}
			}
			if (this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent != null)
			{
				foreach (Delegate delegate333 in this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent.GetInvocationList())
				{
					if (delegate333.Target == instance)
					{
						this.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate333;
					}
				}
			}
			if (this.OnCloudScriptRegisterHttpFunctionRequestEvent != null)
			{
				foreach (Delegate delegate334 in this.OnCloudScriptRegisterHttpFunctionRequestEvent.GetInvocationList())
				{
					if (delegate334.Target == instance)
					{
						this.OnCloudScriptRegisterHttpFunctionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterHttpFunctionRequest>)delegate334;
					}
				}
			}
			if (this.OnCloudScriptRegisterHttpFunctionResultEvent != null)
			{
				foreach (Delegate delegate335 in this.OnCloudScriptRegisterHttpFunctionResultEvent.GetInvocationList())
				{
					if (delegate335.Target == instance)
					{
						this.OnCloudScriptRegisterHttpFunctionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate335;
					}
				}
			}
			if (this.OnCloudScriptRegisterQueuedFunctionRequestEvent != null)
			{
				foreach (Delegate delegate336 in this.OnCloudScriptRegisterQueuedFunctionRequestEvent.GetInvocationList())
				{
					if (delegate336.Target == instance)
					{
						this.OnCloudScriptRegisterQueuedFunctionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RegisterQueuedFunctionRequest>)delegate336;
					}
				}
			}
			if (this.OnCloudScriptRegisterQueuedFunctionResultEvent != null)
			{
				foreach (Delegate delegate337 in this.OnCloudScriptRegisterQueuedFunctionResultEvent.GetInvocationList())
				{
					if (delegate337.Target == instance)
					{
						this.OnCloudScriptRegisterQueuedFunctionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate337;
					}
				}
			}
			if (this.OnCloudScriptUnregisterFunctionRequestEvent != null)
			{
				foreach (Delegate delegate338 in this.OnCloudScriptUnregisterFunctionRequestEvent.GetInvocationList())
				{
					if (delegate338.Target == instance)
					{
						this.OnCloudScriptUnregisterFunctionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnregisterFunctionRequest>)delegate338;
					}
				}
			}
			if (this.OnCloudScriptUnregisterFunctionResultEvent != null)
			{
				foreach (Delegate delegate339 in this.OnCloudScriptUnregisterFunctionResultEvent.GetInvocationList())
				{
					if (delegate339.Target == instance)
					{
						this.OnCloudScriptUnregisterFunctionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.CloudScriptModels.EmptyResult>)delegate339;
					}
				}
			}
			if (this.OnDataAbortFileUploadsRequestEvent != null)
			{
				foreach (Delegate delegate340 in this.OnDataAbortFileUploadsRequestEvent.GetInvocationList())
				{
					if (delegate340.Target == instance)
					{
						this.OnDataAbortFileUploadsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AbortFileUploadsRequest>)delegate340;
					}
				}
			}
			if (this.OnDataAbortFileUploadsResultEvent != null)
			{
				foreach (Delegate delegate341 in this.OnDataAbortFileUploadsResultEvent.GetInvocationList())
				{
					if (delegate341.Target == instance)
					{
						this.OnDataAbortFileUploadsResultEvent -= (PlayFabEvents.PlayFabResultEvent<AbortFileUploadsResponse>)delegate341;
					}
				}
			}
			if (this.OnDataDeleteFilesRequestEvent != null)
			{
				foreach (Delegate delegate342 in this.OnDataDeleteFilesRequestEvent.GetInvocationList())
				{
					if (delegate342.Target == instance)
					{
						this.OnDataDeleteFilesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteFilesRequest>)delegate342;
					}
				}
			}
			if (this.OnDataDeleteFilesResultEvent != null)
			{
				foreach (Delegate delegate343 in this.OnDataDeleteFilesResultEvent.GetInvocationList())
				{
					if (delegate343.Target == instance)
					{
						this.OnDataDeleteFilesResultEvent -= (PlayFabEvents.PlayFabResultEvent<DeleteFilesResponse>)delegate343;
					}
				}
			}
			if (this.OnDataFinalizeFileUploadsRequestEvent != null)
			{
				foreach (Delegate delegate344 in this.OnDataFinalizeFileUploadsRequestEvent.GetInvocationList())
				{
					if (delegate344.Target == instance)
					{
						this.OnDataFinalizeFileUploadsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<FinalizeFileUploadsRequest>)delegate344;
					}
				}
			}
			if (this.OnDataFinalizeFileUploadsResultEvent != null)
			{
				foreach (Delegate delegate345 in this.OnDataFinalizeFileUploadsResultEvent.GetInvocationList())
				{
					if (delegate345.Target == instance)
					{
						this.OnDataFinalizeFileUploadsResultEvent -= (PlayFabEvents.PlayFabResultEvent<FinalizeFileUploadsResponse>)delegate345;
					}
				}
			}
			if (this.OnDataGetFilesRequestEvent != null)
			{
				foreach (Delegate delegate346 in this.OnDataGetFilesRequestEvent.GetInvocationList())
				{
					if (delegate346.Target == instance)
					{
						this.OnDataGetFilesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetFilesRequest>)delegate346;
					}
				}
			}
			if (this.OnDataGetFilesResultEvent != null)
			{
				foreach (Delegate delegate347 in this.OnDataGetFilesResultEvent.GetInvocationList())
				{
					if (delegate347.Target == instance)
					{
						this.OnDataGetFilesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetFilesResponse>)delegate347;
					}
				}
			}
			if (this.OnDataGetObjectsRequestEvent != null)
			{
				foreach (Delegate delegate348 in this.OnDataGetObjectsRequestEvent.GetInvocationList())
				{
					if (delegate348.Target == instance)
					{
						this.OnDataGetObjectsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetObjectsRequest>)delegate348;
					}
				}
			}
			if (this.OnDataGetObjectsResultEvent != null)
			{
				foreach (Delegate delegate349 in this.OnDataGetObjectsResultEvent.GetInvocationList())
				{
					if (delegate349.Target == instance)
					{
						this.OnDataGetObjectsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetObjectsResponse>)delegate349;
					}
				}
			}
			if (this.OnDataInitiateFileUploadsRequestEvent != null)
			{
				foreach (Delegate delegate350 in this.OnDataInitiateFileUploadsRequestEvent.GetInvocationList())
				{
					if (delegate350.Target == instance)
					{
						this.OnDataInitiateFileUploadsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InitiateFileUploadsRequest>)delegate350;
					}
				}
			}
			if (this.OnDataInitiateFileUploadsResultEvent != null)
			{
				foreach (Delegate delegate351 in this.OnDataInitiateFileUploadsResultEvent.GetInvocationList())
				{
					if (delegate351.Target == instance)
					{
						this.OnDataInitiateFileUploadsResultEvent -= (PlayFabEvents.PlayFabResultEvent<InitiateFileUploadsResponse>)delegate351;
					}
				}
			}
			if (this.OnDataSetObjectsRequestEvent != null)
			{
				foreach (Delegate delegate352 in this.OnDataSetObjectsRequestEvent.GetInvocationList())
				{
					if (delegate352.Target == instance)
					{
						this.OnDataSetObjectsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetObjectsRequest>)delegate352;
					}
				}
			}
			if (this.OnDataSetObjectsResultEvent != null)
			{
				foreach (Delegate delegate353 in this.OnDataSetObjectsResultEvent.GetInvocationList())
				{
					if (delegate353.Target == instance)
					{
						this.OnDataSetObjectsResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetObjectsResponse>)delegate353;
					}
				}
			}
			if (this.OnEventsWriteEventsRequestEvent != null)
			{
				foreach (Delegate delegate354 in this.OnEventsWriteEventsRequestEvent.GetInvocationList())
				{
					if (delegate354.Target == instance)
					{
						this.OnEventsWriteEventsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteEventsRequest>)delegate354;
					}
				}
			}
			if (this.OnEventsWriteEventsResultEvent != null)
			{
				foreach (Delegate delegate355 in this.OnEventsWriteEventsResultEvent.GetInvocationList())
				{
					if (delegate355.Target == instance)
					{
						this.OnEventsWriteEventsResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventsResponse>)delegate355;
					}
				}
			}
			if (this.OnEventsWriteTelemetryEventsRequestEvent != null)
			{
				foreach (Delegate delegate356 in this.OnEventsWriteTelemetryEventsRequestEvent.GetInvocationList())
				{
					if (delegate356.Target == instance)
					{
						this.OnEventsWriteTelemetryEventsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<WriteEventsRequest>)delegate356;
					}
				}
			}
			if (this.OnEventsWriteTelemetryEventsResultEvent != null)
			{
				foreach (Delegate delegate357 in this.OnEventsWriteTelemetryEventsResultEvent.GetInvocationList())
				{
					if (delegate357.Target == instance)
					{
						this.OnEventsWriteTelemetryEventsResultEvent -= (PlayFabEvents.PlayFabResultEvent<WriteEventsResponse>)delegate357;
					}
				}
			}
			if (this.OnExperimentationCreateExperimentRequestEvent != null)
			{
				foreach (Delegate delegate358 in this.OnExperimentationCreateExperimentRequestEvent.GetInvocationList())
				{
					if (delegate358.Target == instance)
					{
						this.OnExperimentationCreateExperimentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateExperimentRequest>)delegate358;
					}
				}
			}
			if (this.OnExperimentationCreateExperimentResultEvent != null)
			{
				foreach (Delegate delegate359 in this.OnExperimentationCreateExperimentResultEvent.GetInvocationList())
				{
					if (delegate359.Target == instance)
					{
						this.OnExperimentationCreateExperimentResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateExperimentResult>)delegate359;
					}
				}
			}
			if (this.OnExperimentationDeleteExperimentRequestEvent != null)
			{
				foreach (Delegate delegate360 in this.OnExperimentationDeleteExperimentRequestEvent.GetInvocationList())
				{
					if (delegate360.Target == instance)
					{
						this.OnExperimentationDeleteExperimentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteExperimentRequest>)delegate360;
					}
				}
			}
			if (this.OnExperimentationDeleteExperimentResultEvent != null)
			{
				foreach (Delegate delegate361 in this.OnExperimentationDeleteExperimentResultEvent.GetInvocationList())
				{
					if (delegate361.Target == instance)
					{
						this.OnExperimentationDeleteExperimentResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)delegate361;
					}
				}
			}
			if (this.OnExperimentationGetExperimentsRequestEvent != null)
			{
				foreach (Delegate delegate362 in this.OnExperimentationGetExperimentsRequestEvent.GetInvocationList())
				{
					if (delegate362.Target == instance)
					{
						this.OnExperimentationGetExperimentsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetExperimentsRequest>)delegate362;
					}
				}
			}
			if (this.OnExperimentationGetExperimentsResultEvent != null)
			{
				foreach (Delegate delegate363 in this.OnExperimentationGetExperimentsResultEvent.GetInvocationList())
				{
					if (delegate363.Target == instance)
					{
						this.OnExperimentationGetExperimentsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetExperimentsResult>)delegate363;
					}
				}
			}
			if (this.OnExperimentationGetLatestScorecardRequestEvent != null)
			{
				foreach (Delegate delegate364 in this.OnExperimentationGetLatestScorecardRequestEvent.GetInvocationList())
				{
					if (delegate364.Target == instance)
					{
						this.OnExperimentationGetLatestScorecardRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLatestScorecardRequest>)delegate364;
					}
				}
			}
			if (this.OnExperimentationGetLatestScorecardResultEvent != null)
			{
				foreach (Delegate delegate365 in this.OnExperimentationGetLatestScorecardResultEvent.GetInvocationList())
				{
					if (delegate365.Target == instance)
					{
						this.OnExperimentationGetLatestScorecardResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLatestScorecardResult>)delegate365;
					}
				}
			}
			if (this.OnExperimentationGetTreatmentAssignmentRequestEvent != null)
			{
				foreach (Delegate delegate366 in this.OnExperimentationGetTreatmentAssignmentRequestEvent.GetInvocationList())
				{
					if (delegate366.Target == instance)
					{
						this.OnExperimentationGetTreatmentAssignmentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTreatmentAssignmentRequest>)delegate366;
					}
				}
			}
			if (this.OnExperimentationGetTreatmentAssignmentResultEvent != null)
			{
				foreach (Delegate delegate367 in this.OnExperimentationGetTreatmentAssignmentResultEvent.GetInvocationList())
				{
					if (delegate367.Target == instance)
					{
						this.OnExperimentationGetTreatmentAssignmentResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTreatmentAssignmentResult>)delegate367;
					}
				}
			}
			if (this.OnExperimentationStartExperimentRequestEvent != null)
			{
				foreach (Delegate delegate368 in this.OnExperimentationStartExperimentRequestEvent.GetInvocationList())
				{
					if (delegate368.Target == instance)
					{
						this.OnExperimentationStartExperimentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StartExperimentRequest>)delegate368;
					}
				}
			}
			if (this.OnExperimentationStartExperimentResultEvent != null)
			{
				foreach (Delegate delegate369 in this.OnExperimentationStartExperimentResultEvent.GetInvocationList())
				{
					if (delegate369.Target == instance)
					{
						this.OnExperimentationStartExperimentResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)delegate369;
					}
				}
			}
			if (this.OnExperimentationStopExperimentRequestEvent != null)
			{
				foreach (Delegate delegate370 in this.OnExperimentationStopExperimentRequestEvent.GetInvocationList())
				{
					if (delegate370.Target == instance)
					{
						this.OnExperimentationStopExperimentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<StopExperimentRequest>)delegate370;
					}
				}
			}
			if (this.OnExperimentationStopExperimentResultEvent != null)
			{
				foreach (Delegate delegate371 in this.OnExperimentationStopExperimentResultEvent.GetInvocationList())
				{
					if (delegate371.Target == instance)
					{
						this.OnExperimentationStopExperimentResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)delegate371;
					}
				}
			}
			if (this.OnExperimentationUpdateExperimentRequestEvent != null)
			{
				foreach (Delegate delegate372 in this.OnExperimentationUpdateExperimentRequestEvent.GetInvocationList())
				{
					if (delegate372.Target == instance)
					{
						this.OnExperimentationUpdateExperimentRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateExperimentRequest>)delegate372;
					}
				}
			}
			if (this.OnExperimentationUpdateExperimentResultEvent != null)
			{
				foreach (Delegate delegate373 in this.OnExperimentationUpdateExperimentResultEvent.GetInvocationList())
				{
					if (delegate373.Target == instance)
					{
						this.OnExperimentationUpdateExperimentResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.ExperimentationModels.EmptyResponse>)delegate373;
					}
				}
			}
			if (this.OnInsightsGetDetailsRequestEvent != null)
			{
				foreach (Delegate delegate374 in this.OnInsightsGetDetailsRequestEvent.GetInvocationList())
				{
					if (delegate374.Target == instance)
					{
						this.OnInsightsGetDetailsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InsightsEmptyRequest>)delegate374;
					}
				}
			}
			if (this.OnInsightsGetDetailsResultEvent != null)
			{
				foreach (Delegate delegate375 in this.OnInsightsGetDetailsResultEvent.GetInvocationList())
				{
					if (delegate375.Target == instance)
					{
						this.OnInsightsGetDetailsResultEvent -= (PlayFabEvents.PlayFabResultEvent<InsightsGetDetailsResponse>)delegate375;
					}
				}
			}
			if (this.OnInsightsGetLimitsRequestEvent != null)
			{
				foreach (Delegate delegate376 in this.OnInsightsGetLimitsRequestEvent.GetInvocationList())
				{
					if (delegate376.Target == instance)
					{
						this.OnInsightsGetLimitsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InsightsEmptyRequest>)delegate376;
					}
				}
			}
			if (this.OnInsightsGetLimitsResultEvent != null)
			{
				foreach (Delegate delegate377 in this.OnInsightsGetLimitsResultEvent.GetInvocationList())
				{
					if (delegate377.Target == instance)
					{
						this.OnInsightsGetLimitsResultEvent -= (PlayFabEvents.PlayFabResultEvent<InsightsGetLimitsResponse>)delegate377;
					}
				}
			}
			if (this.OnInsightsGetOperationStatusRequestEvent != null)
			{
				foreach (Delegate delegate378 in this.OnInsightsGetOperationStatusRequestEvent.GetInvocationList())
				{
					if (delegate378.Target == instance)
					{
						this.OnInsightsGetOperationStatusRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InsightsGetOperationStatusRequest>)delegate378;
					}
				}
			}
			if (this.OnInsightsGetOperationStatusResultEvent != null)
			{
				foreach (Delegate delegate379 in this.OnInsightsGetOperationStatusResultEvent.GetInvocationList())
				{
					if (delegate379.Target == instance)
					{
						this.OnInsightsGetOperationStatusResultEvent -= (PlayFabEvents.PlayFabResultEvent<InsightsGetOperationStatusResponse>)delegate379;
					}
				}
			}
			if (this.OnInsightsGetPendingOperationsRequestEvent != null)
			{
				foreach (Delegate delegate380 in this.OnInsightsGetPendingOperationsRequestEvent.GetInvocationList())
				{
					if (delegate380.Target == instance)
					{
						this.OnInsightsGetPendingOperationsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InsightsGetPendingOperationsRequest>)delegate380;
					}
				}
			}
			if (this.OnInsightsGetPendingOperationsResultEvent != null)
			{
				foreach (Delegate delegate381 in this.OnInsightsGetPendingOperationsResultEvent.GetInvocationList())
				{
					if (delegate381.Target == instance)
					{
						this.OnInsightsGetPendingOperationsResultEvent -= (PlayFabEvents.PlayFabResultEvent<InsightsGetPendingOperationsResponse>)delegate381;
					}
				}
			}
			if (this.OnInsightsSetPerformanceRequestEvent != null)
			{
				foreach (Delegate delegate382 in this.OnInsightsSetPerformanceRequestEvent.GetInvocationList())
				{
					if (delegate382.Target == instance)
					{
						this.OnInsightsSetPerformanceRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InsightsSetPerformanceRequest>)delegate382;
					}
				}
			}
			if (this.OnInsightsSetPerformanceResultEvent != null)
			{
				foreach (Delegate delegate383 in this.OnInsightsSetPerformanceResultEvent.GetInvocationList())
				{
					if (delegate383.Target == instance)
					{
						this.OnInsightsSetPerformanceResultEvent -= (PlayFabEvents.PlayFabResultEvent<InsightsOperationResponse>)delegate383;
					}
				}
			}
			if (this.OnInsightsSetStorageRetentionRequestEvent != null)
			{
				foreach (Delegate delegate384 in this.OnInsightsSetStorageRetentionRequestEvent.GetInvocationList())
				{
					if (delegate384.Target == instance)
					{
						this.OnInsightsSetStorageRetentionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InsightsSetStorageRetentionRequest>)delegate384;
					}
				}
			}
			if (this.OnInsightsSetStorageRetentionResultEvent != null)
			{
				foreach (Delegate delegate385 in this.OnInsightsSetStorageRetentionResultEvent.GetInvocationList())
				{
					if (delegate385.Target == instance)
					{
						this.OnInsightsSetStorageRetentionResultEvent -= (PlayFabEvents.PlayFabResultEvent<InsightsOperationResponse>)delegate385;
					}
				}
			}
			if (this.OnGroupsAcceptGroupApplicationRequestEvent != null)
			{
				foreach (Delegate delegate386 in this.OnGroupsAcceptGroupApplicationRequestEvent.GetInvocationList())
				{
					if (delegate386.Target == instance)
					{
						this.OnGroupsAcceptGroupApplicationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptGroupApplicationRequest>)delegate386;
					}
				}
			}
			if (this.OnGroupsAcceptGroupApplicationResultEvent != null)
			{
				foreach (Delegate delegate387 in this.OnGroupsAcceptGroupApplicationResultEvent.GetInvocationList())
				{
					if (delegate387.Target == instance)
					{
						this.OnGroupsAcceptGroupApplicationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate387;
					}
				}
			}
			if (this.OnGroupsAcceptGroupInvitationRequestEvent != null)
			{
				foreach (Delegate delegate388 in this.OnGroupsAcceptGroupInvitationRequestEvent.GetInvocationList())
				{
					if (delegate388.Target == instance)
					{
						this.OnGroupsAcceptGroupInvitationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AcceptGroupInvitationRequest>)delegate388;
					}
				}
			}
			if (this.OnGroupsAcceptGroupInvitationResultEvent != null)
			{
				foreach (Delegate delegate389 in this.OnGroupsAcceptGroupInvitationResultEvent.GetInvocationList())
				{
					if (delegate389.Target == instance)
					{
						this.OnGroupsAcceptGroupInvitationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate389;
					}
				}
			}
			if (this.OnGroupsAddMembersRequestEvent != null)
			{
				foreach (Delegate delegate390 in this.OnGroupsAddMembersRequestEvent.GetInvocationList())
				{
					if (delegate390.Target == instance)
					{
						this.OnGroupsAddMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<AddMembersRequest>)delegate390;
					}
				}
			}
			if (this.OnGroupsAddMembersResultEvent != null)
			{
				foreach (Delegate delegate391 in this.OnGroupsAddMembersResultEvent.GetInvocationList())
				{
					if (delegate391.Target == instance)
					{
						this.OnGroupsAddMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate391;
					}
				}
			}
			if (this.OnGroupsApplyToGroupRequestEvent != null)
			{
				foreach (Delegate delegate392 in this.OnGroupsApplyToGroupRequestEvent.GetInvocationList())
				{
					if (delegate392.Target == instance)
					{
						this.OnGroupsApplyToGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ApplyToGroupRequest>)delegate392;
					}
				}
			}
			if (this.OnGroupsApplyToGroupResultEvent != null)
			{
				foreach (Delegate delegate393 in this.OnGroupsApplyToGroupResultEvent.GetInvocationList())
				{
					if (delegate393.Target == instance)
					{
						this.OnGroupsApplyToGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<ApplyToGroupResponse>)delegate393;
					}
				}
			}
			if (this.OnGroupsBlockEntityRequestEvent != null)
			{
				foreach (Delegate delegate394 in this.OnGroupsBlockEntityRequestEvent.GetInvocationList())
				{
					if (delegate394.Target == instance)
					{
						this.OnGroupsBlockEntityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<BlockEntityRequest>)delegate394;
					}
				}
			}
			if (this.OnGroupsBlockEntityResultEvent != null)
			{
				foreach (Delegate delegate395 in this.OnGroupsBlockEntityResultEvent.GetInvocationList())
				{
					if (delegate395.Target == instance)
					{
						this.OnGroupsBlockEntityResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate395;
					}
				}
			}
			if (this.OnGroupsChangeMemberRoleRequestEvent != null)
			{
				foreach (Delegate delegate396 in this.OnGroupsChangeMemberRoleRequestEvent.GetInvocationList())
				{
					if (delegate396.Target == instance)
					{
						this.OnGroupsChangeMemberRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ChangeMemberRoleRequest>)delegate396;
					}
				}
			}
			if (this.OnGroupsChangeMemberRoleResultEvent != null)
			{
				foreach (Delegate delegate397 in this.OnGroupsChangeMemberRoleResultEvent.GetInvocationList())
				{
					if (delegate397.Target == instance)
					{
						this.OnGroupsChangeMemberRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate397;
					}
				}
			}
			if (this.OnGroupsCreateGroupRequestEvent != null)
			{
				foreach (Delegate delegate398 in this.OnGroupsCreateGroupRequestEvent.GetInvocationList())
				{
					if (delegate398.Target == instance)
					{
						this.OnGroupsCreateGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateGroupRequest>)delegate398;
					}
				}
			}
			if (this.OnGroupsCreateGroupResultEvent != null)
			{
				foreach (Delegate delegate399 in this.OnGroupsCreateGroupResultEvent.GetInvocationList())
				{
					if (delegate399.Target == instance)
					{
						this.OnGroupsCreateGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateGroupResponse>)delegate399;
					}
				}
			}
			if (this.OnGroupsCreateRoleRequestEvent != null)
			{
				foreach (Delegate delegate400 in this.OnGroupsCreateRoleRequestEvent.GetInvocationList())
				{
					if (delegate400.Target == instance)
					{
						this.OnGroupsCreateRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateGroupRoleRequest>)delegate400;
					}
				}
			}
			if (this.OnGroupsCreateRoleResultEvent != null)
			{
				foreach (Delegate delegate401 in this.OnGroupsCreateRoleResultEvent.GetInvocationList())
				{
					if (delegate401.Target == instance)
					{
						this.OnGroupsCreateRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateGroupRoleResponse>)delegate401;
					}
				}
			}
			if (this.OnGroupsDeleteGroupRequestEvent != null)
			{
				foreach (Delegate delegate402 in this.OnGroupsDeleteGroupRequestEvent.GetInvocationList())
				{
					if (delegate402.Target == instance)
					{
						this.OnGroupsDeleteGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteGroupRequest>)delegate402;
					}
				}
			}
			if (this.OnGroupsDeleteGroupResultEvent != null)
			{
				foreach (Delegate delegate403 in this.OnGroupsDeleteGroupResultEvent.GetInvocationList())
				{
					if (delegate403.Target == instance)
					{
						this.OnGroupsDeleteGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate403;
					}
				}
			}
			if (this.OnGroupsDeleteRoleRequestEvent != null)
			{
				foreach (Delegate delegate404 in this.OnGroupsDeleteRoleRequestEvent.GetInvocationList())
				{
					if (delegate404.Target == instance)
					{
						this.OnGroupsDeleteRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteRoleRequest>)delegate404;
					}
				}
			}
			if (this.OnGroupsDeleteRoleResultEvent != null)
			{
				foreach (Delegate delegate405 in this.OnGroupsDeleteRoleResultEvent.GetInvocationList())
				{
					if (delegate405.Target == instance)
					{
						this.OnGroupsDeleteRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate405;
					}
				}
			}
			if (this.OnGroupsGetGroupRequestEvent != null)
			{
				foreach (Delegate delegate406 in this.OnGroupsGetGroupRequestEvent.GetInvocationList())
				{
					if (delegate406.Target == instance)
					{
						this.OnGroupsGetGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetGroupRequest>)delegate406;
					}
				}
			}
			if (this.OnGroupsGetGroupResultEvent != null)
			{
				foreach (Delegate delegate407 in this.OnGroupsGetGroupResultEvent.GetInvocationList())
				{
					if (delegate407.Target == instance)
					{
						this.OnGroupsGetGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetGroupResponse>)delegate407;
					}
				}
			}
			if (this.OnGroupsInviteToGroupRequestEvent != null)
			{
				foreach (Delegate delegate408 in this.OnGroupsInviteToGroupRequestEvent.GetInvocationList())
				{
					if (delegate408.Target == instance)
					{
						this.OnGroupsInviteToGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<InviteToGroupRequest>)delegate408;
					}
				}
			}
			if (this.OnGroupsInviteToGroupResultEvent != null)
			{
				foreach (Delegate delegate409 in this.OnGroupsInviteToGroupResultEvent.GetInvocationList())
				{
					if (delegate409.Target == instance)
					{
						this.OnGroupsInviteToGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<InviteToGroupResponse>)delegate409;
					}
				}
			}
			if (this.OnGroupsIsMemberRequestEvent != null)
			{
				foreach (Delegate delegate410 in this.OnGroupsIsMemberRequestEvent.GetInvocationList())
				{
					if (delegate410.Target == instance)
					{
						this.OnGroupsIsMemberRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<IsMemberRequest>)delegate410;
					}
				}
			}
			if (this.OnGroupsIsMemberResultEvent != null)
			{
				foreach (Delegate delegate411 in this.OnGroupsIsMemberResultEvent.GetInvocationList())
				{
					if (delegate411.Target == instance)
					{
						this.OnGroupsIsMemberResultEvent -= (PlayFabEvents.PlayFabResultEvent<IsMemberResponse>)delegate411;
					}
				}
			}
			if (this.OnGroupsListGroupApplicationsRequestEvent != null)
			{
				foreach (Delegate delegate412 in this.OnGroupsListGroupApplicationsRequestEvent.GetInvocationList())
				{
					if (delegate412.Target == instance)
					{
						this.OnGroupsListGroupApplicationsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupApplicationsRequest>)delegate412;
					}
				}
			}
			if (this.OnGroupsListGroupApplicationsResultEvent != null)
			{
				foreach (Delegate delegate413 in this.OnGroupsListGroupApplicationsResultEvent.GetInvocationList())
				{
					if (delegate413.Target == instance)
					{
						this.OnGroupsListGroupApplicationsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupApplicationsResponse>)delegate413;
					}
				}
			}
			if (this.OnGroupsListGroupBlocksRequestEvent != null)
			{
				foreach (Delegate delegate414 in this.OnGroupsListGroupBlocksRequestEvent.GetInvocationList())
				{
					if (delegate414.Target == instance)
					{
						this.OnGroupsListGroupBlocksRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupBlocksRequest>)delegate414;
					}
				}
			}
			if (this.OnGroupsListGroupBlocksResultEvent != null)
			{
				foreach (Delegate delegate415 in this.OnGroupsListGroupBlocksResultEvent.GetInvocationList())
				{
					if (delegate415.Target == instance)
					{
						this.OnGroupsListGroupBlocksResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupBlocksResponse>)delegate415;
					}
				}
			}
			if (this.OnGroupsListGroupInvitationsRequestEvent != null)
			{
				foreach (Delegate delegate416 in this.OnGroupsListGroupInvitationsRequestEvent.GetInvocationList())
				{
					if (delegate416.Target == instance)
					{
						this.OnGroupsListGroupInvitationsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupInvitationsRequest>)delegate416;
					}
				}
			}
			if (this.OnGroupsListGroupInvitationsResultEvent != null)
			{
				foreach (Delegate delegate417 in this.OnGroupsListGroupInvitationsResultEvent.GetInvocationList())
				{
					if (delegate417.Target == instance)
					{
						this.OnGroupsListGroupInvitationsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupInvitationsResponse>)delegate417;
					}
				}
			}
			if (this.OnGroupsListGroupMembersRequestEvent != null)
			{
				foreach (Delegate delegate418 in this.OnGroupsListGroupMembersRequestEvent.GetInvocationList())
				{
					if (delegate418.Target == instance)
					{
						this.OnGroupsListGroupMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListGroupMembersRequest>)delegate418;
					}
				}
			}
			if (this.OnGroupsListGroupMembersResultEvent != null)
			{
				foreach (Delegate delegate419 in this.OnGroupsListGroupMembersResultEvent.GetInvocationList())
				{
					if (delegate419.Target == instance)
					{
						this.OnGroupsListGroupMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListGroupMembersResponse>)delegate419;
					}
				}
			}
			if (this.OnGroupsListMembershipRequestEvent != null)
			{
				foreach (Delegate delegate420 in this.OnGroupsListMembershipRequestEvent.GetInvocationList())
				{
					if (delegate420.Target == instance)
					{
						this.OnGroupsListMembershipRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMembershipRequest>)delegate420;
					}
				}
			}
			if (this.OnGroupsListMembershipResultEvent != null)
			{
				foreach (Delegate delegate421 in this.OnGroupsListMembershipResultEvent.GetInvocationList())
				{
					if (delegate421.Target == instance)
					{
						this.OnGroupsListMembershipResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMembershipResponse>)delegate421;
					}
				}
			}
			if (this.OnGroupsListMembershipOpportunitiesRequestEvent != null)
			{
				foreach (Delegate delegate422 in this.OnGroupsListMembershipOpportunitiesRequestEvent.GetInvocationList())
				{
					if (delegate422.Target == instance)
					{
						this.OnGroupsListMembershipOpportunitiesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMembershipOpportunitiesRequest>)delegate422;
					}
				}
			}
			if (this.OnGroupsListMembershipOpportunitiesResultEvent != null)
			{
				foreach (Delegate delegate423 in this.OnGroupsListMembershipOpportunitiesResultEvent.GetInvocationList())
				{
					if (delegate423.Target == instance)
					{
						this.OnGroupsListMembershipOpportunitiesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMembershipOpportunitiesResponse>)delegate423;
					}
				}
			}
			if (this.OnGroupsRemoveGroupApplicationRequestEvent != null)
			{
				foreach (Delegate delegate424 in this.OnGroupsRemoveGroupApplicationRequestEvent.GetInvocationList())
				{
					if (delegate424.Target == instance)
					{
						this.OnGroupsRemoveGroupApplicationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGroupApplicationRequest>)delegate424;
					}
				}
			}
			if (this.OnGroupsRemoveGroupApplicationResultEvent != null)
			{
				foreach (Delegate delegate425 in this.OnGroupsRemoveGroupApplicationResultEvent.GetInvocationList())
				{
					if (delegate425.Target == instance)
					{
						this.OnGroupsRemoveGroupApplicationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate425;
					}
				}
			}
			if (this.OnGroupsRemoveGroupInvitationRequestEvent != null)
			{
				foreach (Delegate delegate426 in this.OnGroupsRemoveGroupInvitationRequestEvent.GetInvocationList())
				{
					if (delegate426.Target == instance)
					{
						this.OnGroupsRemoveGroupInvitationRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveGroupInvitationRequest>)delegate426;
					}
				}
			}
			if (this.OnGroupsRemoveGroupInvitationResultEvent != null)
			{
				foreach (Delegate delegate427 in this.OnGroupsRemoveGroupInvitationResultEvent.GetInvocationList())
				{
					if (delegate427.Target == instance)
					{
						this.OnGroupsRemoveGroupInvitationResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate427;
					}
				}
			}
			if (this.OnGroupsRemoveMembersRequestEvent != null)
			{
				foreach (Delegate delegate428 in this.OnGroupsRemoveMembersRequestEvent.GetInvocationList())
				{
					if (delegate428.Target == instance)
					{
						this.OnGroupsRemoveMembersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveMembersRequest>)delegate428;
					}
				}
			}
			if (this.OnGroupsRemoveMembersResultEvent != null)
			{
				foreach (Delegate delegate429 in this.OnGroupsRemoveMembersResultEvent.GetInvocationList())
				{
					if (delegate429.Target == instance)
					{
						this.OnGroupsRemoveMembersResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate429;
					}
				}
			}
			if (this.OnGroupsUnblockEntityRequestEvent != null)
			{
				foreach (Delegate delegate430 in this.OnGroupsUnblockEntityRequestEvent.GetInvocationList())
				{
					if (delegate430.Target == instance)
					{
						this.OnGroupsUnblockEntityRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UnblockEntityRequest>)delegate430;
					}
				}
			}
			if (this.OnGroupsUnblockEntityResultEvent != null)
			{
				foreach (Delegate delegate431 in this.OnGroupsUnblockEntityResultEvent.GetInvocationList())
				{
					if (delegate431.Target == instance)
					{
						this.OnGroupsUnblockEntityResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.GroupsModels.EmptyResponse>)delegate431;
					}
				}
			}
			if (this.OnGroupsUpdateGroupRequestEvent != null)
			{
				foreach (Delegate delegate432 in this.OnGroupsUpdateGroupRequestEvent.GetInvocationList())
				{
					if (delegate432.Target == instance)
					{
						this.OnGroupsUpdateGroupRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateGroupRequest>)delegate432;
					}
				}
			}
			if (this.OnGroupsUpdateGroupResultEvent != null)
			{
				foreach (Delegate delegate433 in this.OnGroupsUpdateGroupResultEvent.GetInvocationList())
				{
					if (delegate433.Target == instance)
					{
						this.OnGroupsUpdateGroupResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateGroupResponse>)delegate433;
					}
				}
			}
			if (this.OnGroupsUpdateRoleRequestEvent != null)
			{
				foreach (Delegate delegate434 in this.OnGroupsUpdateRoleRequestEvent.GetInvocationList())
				{
					if (delegate434.Target == instance)
					{
						this.OnGroupsUpdateRoleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateGroupRoleRequest>)delegate434;
					}
				}
			}
			if (this.OnGroupsUpdateRoleResultEvent != null)
			{
				foreach (Delegate delegate435 in this.OnGroupsUpdateRoleResultEvent.GetInvocationList())
				{
					if (delegate435.Target == instance)
					{
						this.OnGroupsUpdateRoleResultEvent -= (PlayFabEvents.PlayFabResultEvent<UpdateGroupRoleResponse>)delegate435;
					}
				}
			}
			if (this.OnLocalizationGetLanguageListRequestEvent != null)
			{
				foreach (Delegate delegate436 in this.OnLocalizationGetLanguageListRequestEvent.GetInvocationList())
				{
					if (delegate436.Target == instance)
					{
						this.OnLocalizationGetLanguageListRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetLanguageListRequest>)delegate436;
					}
				}
			}
			if (this.OnLocalizationGetLanguageListResultEvent != null)
			{
				foreach (Delegate delegate437 in this.OnLocalizationGetLanguageListResultEvent.GetInvocationList())
				{
					if (delegate437.Target == instance)
					{
						this.OnLocalizationGetLanguageListResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetLanguageListResponse>)delegate437;
					}
				}
			}
			if (this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent != null)
			{
				foreach (Delegate delegate438 in this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent.GetInvocationList())
				{
					if (delegate438.Target == instance)
					{
						this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelAllMatchmakingTicketsForPlayerRequest>)delegate438;
					}
				}
			}
			if (this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent != null)
			{
				foreach (Delegate delegate439 in this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent.GetInvocationList())
				{
					if (delegate439.Target == instance)
					{
						this.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelAllMatchmakingTicketsForPlayerResult>)delegate439;
					}
				}
			}
			if (this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent != null)
			{
				foreach (Delegate delegate440 in this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent.GetInvocationList())
				{
					if (delegate440.Target == instance)
					{
						this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelAllServerBackfillTicketsForPlayerRequest>)delegate440;
					}
				}
			}
			if (this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent != null)
			{
				foreach (Delegate delegate441 in this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent.GetInvocationList())
				{
					if (delegate441.Target == instance)
					{
						this.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelAllServerBackfillTicketsForPlayerResult>)delegate441;
					}
				}
			}
			if (this.OnMultiplayerCancelMatchmakingTicketRequestEvent != null)
			{
				foreach (Delegate delegate442 in this.OnMultiplayerCancelMatchmakingTicketRequestEvent.GetInvocationList())
				{
					if (delegate442.Target == instance)
					{
						this.OnMultiplayerCancelMatchmakingTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelMatchmakingTicketRequest>)delegate442;
					}
				}
			}
			if (this.OnMultiplayerCancelMatchmakingTicketResultEvent != null)
			{
				foreach (Delegate delegate443 in this.OnMultiplayerCancelMatchmakingTicketResultEvent.GetInvocationList())
				{
					if (delegate443.Target == instance)
					{
						this.OnMultiplayerCancelMatchmakingTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelMatchmakingTicketResult>)delegate443;
					}
				}
			}
			if (this.OnMultiplayerCancelServerBackfillTicketRequestEvent != null)
			{
				foreach (Delegate delegate444 in this.OnMultiplayerCancelServerBackfillTicketRequestEvent.GetInvocationList())
				{
					if (delegate444.Target == instance)
					{
						this.OnMultiplayerCancelServerBackfillTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CancelServerBackfillTicketRequest>)delegate444;
					}
				}
			}
			if (this.OnMultiplayerCancelServerBackfillTicketResultEvent != null)
			{
				foreach (Delegate delegate445 in this.OnMultiplayerCancelServerBackfillTicketResultEvent.GetInvocationList())
				{
					if (delegate445.Target == instance)
					{
						this.OnMultiplayerCancelServerBackfillTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<CancelServerBackfillTicketResult>)delegate445;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildAliasRequestEvent != null)
			{
				foreach (Delegate delegate446 in this.OnMultiplayerCreateBuildAliasRequestEvent.GetInvocationList())
				{
					if (delegate446.Target == instance)
					{
						this.OnMultiplayerCreateBuildAliasRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateBuildAliasRequest>)delegate446;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildAliasResultEvent != null)
			{
				foreach (Delegate delegate447 in this.OnMultiplayerCreateBuildAliasResultEvent.GetInvocationList())
				{
					if (delegate447.Target == instance)
					{
						this.OnMultiplayerCreateBuildAliasResultEvent -= (PlayFabEvents.PlayFabResultEvent<BuildAliasDetailsResponse>)delegate447;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent != null)
			{
				foreach (Delegate delegate448 in this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent.GetInvocationList())
				{
					if (delegate448.Target == instance)
					{
						this.OnMultiplayerCreateBuildWithCustomContainerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateBuildWithCustomContainerRequest>)delegate448;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithCustomContainerResultEvent != null)
			{
				foreach (Delegate delegate449 in this.OnMultiplayerCreateBuildWithCustomContainerResultEvent.GetInvocationList())
				{
					if (delegate449.Target == instance)
					{
						this.OnMultiplayerCreateBuildWithCustomContainerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateBuildWithCustomContainerResponse>)delegate449;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent != null)
			{
				foreach (Delegate delegate450 in this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent.GetInvocationList())
				{
					if (delegate450.Target == instance)
					{
						this.OnMultiplayerCreateBuildWithManagedContainerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateBuildWithManagedContainerRequest>)delegate450;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithManagedContainerResultEvent != null)
			{
				foreach (Delegate delegate451 in this.OnMultiplayerCreateBuildWithManagedContainerResultEvent.GetInvocationList())
				{
					if (delegate451.Target == instance)
					{
						this.OnMultiplayerCreateBuildWithManagedContainerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateBuildWithManagedContainerResponse>)delegate451;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent != null)
			{
				foreach (Delegate delegate452 in this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent.GetInvocationList())
				{
					if (delegate452.Target == instance)
					{
						this.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateBuildWithProcessBasedServerRequest>)delegate452;
					}
				}
			}
			if (this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent != null)
			{
				foreach (Delegate delegate453 in this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent.GetInvocationList())
				{
					if (delegate453.Target == instance)
					{
						this.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateBuildWithProcessBasedServerResponse>)delegate453;
					}
				}
			}
			if (this.OnMultiplayerCreateMatchmakingTicketRequestEvent != null)
			{
				foreach (Delegate delegate454 in this.OnMultiplayerCreateMatchmakingTicketRequestEvent.GetInvocationList())
				{
					if (delegate454.Target == instance)
					{
						this.OnMultiplayerCreateMatchmakingTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateMatchmakingTicketRequest>)delegate454;
					}
				}
			}
			if (this.OnMultiplayerCreateMatchmakingTicketResultEvent != null)
			{
				foreach (Delegate delegate455 in this.OnMultiplayerCreateMatchmakingTicketResultEvent.GetInvocationList())
				{
					if (delegate455.Target == instance)
					{
						this.OnMultiplayerCreateMatchmakingTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateMatchmakingTicketResult>)delegate455;
					}
				}
			}
			if (this.OnMultiplayerCreateRemoteUserRequestEvent != null)
			{
				foreach (Delegate delegate456 in this.OnMultiplayerCreateRemoteUserRequestEvent.GetInvocationList())
				{
					if (delegate456.Target == instance)
					{
						this.OnMultiplayerCreateRemoteUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateRemoteUserRequest>)delegate456;
					}
				}
			}
			if (this.OnMultiplayerCreateRemoteUserResultEvent != null)
			{
				foreach (Delegate delegate457 in this.OnMultiplayerCreateRemoteUserResultEvent.GetInvocationList())
				{
					if (delegate457.Target == instance)
					{
						this.OnMultiplayerCreateRemoteUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateRemoteUserResponse>)delegate457;
					}
				}
			}
			if (this.OnMultiplayerCreateServerBackfillTicketRequestEvent != null)
			{
				foreach (Delegate delegate458 in this.OnMultiplayerCreateServerBackfillTicketRequestEvent.GetInvocationList())
				{
					if (delegate458.Target == instance)
					{
						this.OnMultiplayerCreateServerBackfillTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateServerBackfillTicketRequest>)delegate458;
					}
				}
			}
			if (this.OnMultiplayerCreateServerBackfillTicketResultEvent != null)
			{
				foreach (Delegate delegate459 in this.OnMultiplayerCreateServerBackfillTicketResultEvent.GetInvocationList())
				{
					if (delegate459.Target == instance)
					{
						this.OnMultiplayerCreateServerBackfillTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateServerBackfillTicketResult>)delegate459;
					}
				}
			}
			if (this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent != null)
			{
				foreach (Delegate delegate460 in this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent.GetInvocationList())
				{
					if (delegate460.Target == instance)
					{
						this.OnMultiplayerCreateServerMatchmakingTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<CreateServerMatchmakingTicketRequest>)delegate460;
					}
				}
			}
			if (this.OnMultiplayerCreateServerMatchmakingTicketResultEvent != null)
			{
				foreach (Delegate delegate461 in this.OnMultiplayerCreateServerMatchmakingTicketResultEvent.GetInvocationList())
				{
					if (delegate461.Target == instance)
					{
						this.OnMultiplayerCreateServerMatchmakingTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<CreateMatchmakingTicketResult>)delegate461;
					}
				}
			}
			if (this.OnMultiplayerDeleteAssetRequestEvent != null)
			{
				foreach (Delegate delegate462 in this.OnMultiplayerDeleteAssetRequestEvent.GetInvocationList())
				{
					if (delegate462.Target == instance)
					{
						this.OnMultiplayerDeleteAssetRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteAssetRequest>)delegate462;
					}
				}
			}
			if (this.OnMultiplayerDeleteAssetResultEvent != null)
			{
				foreach (Delegate delegate463 in this.OnMultiplayerDeleteAssetResultEvent.GetInvocationList())
				{
					if (delegate463.Target == instance)
					{
						this.OnMultiplayerDeleteAssetResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate463;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRequestEvent != null)
			{
				foreach (Delegate delegate464 in this.OnMultiplayerDeleteBuildRequestEvent.GetInvocationList())
				{
					if (delegate464.Target == instance)
					{
						this.OnMultiplayerDeleteBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteBuildRequest>)delegate464;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildResultEvent != null)
			{
				foreach (Delegate delegate465 in this.OnMultiplayerDeleteBuildResultEvent.GetInvocationList())
				{
					if (delegate465.Target == instance)
					{
						this.OnMultiplayerDeleteBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate465;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildAliasRequestEvent != null)
			{
				foreach (Delegate delegate466 in this.OnMultiplayerDeleteBuildAliasRequestEvent.GetInvocationList())
				{
					if (delegate466.Target == instance)
					{
						this.OnMultiplayerDeleteBuildAliasRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteBuildAliasRequest>)delegate466;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildAliasResultEvent != null)
			{
				foreach (Delegate delegate467 in this.OnMultiplayerDeleteBuildAliasResultEvent.GetInvocationList())
				{
					if (delegate467.Target == instance)
					{
						this.OnMultiplayerDeleteBuildAliasResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate467;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRegionRequestEvent != null)
			{
				foreach (Delegate delegate468 in this.OnMultiplayerDeleteBuildRegionRequestEvent.GetInvocationList())
				{
					if (delegate468.Target == instance)
					{
						this.OnMultiplayerDeleteBuildRegionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteBuildRegionRequest>)delegate468;
					}
				}
			}
			if (this.OnMultiplayerDeleteBuildRegionResultEvent != null)
			{
				foreach (Delegate delegate469 in this.OnMultiplayerDeleteBuildRegionResultEvent.GetInvocationList())
				{
					if (delegate469.Target == instance)
					{
						this.OnMultiplayerDeleteBuildRegionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate469;
					}
				}
			}
			if (this.OnMultiplayerDeleteCertificateRequestEvent != null)
			{
				foreach (Delegate delegate470 in this.OnMultiplayerDeleteCertificateRequestEvent.GetInvocationList())
				{
					if (delegate470.Target == instance)
					{
						this.OnMultiplayerDeleteCertificateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteCertificateRequest>)delegate470;
					}
				}
			}
			if (this.OnMultiplayerDeleteCertificateResultEvent != null)
			{
				foreach (Delegate delegate471 in this.OnMultiplayerDeleteCertificateResultEvent.GetInvocationList())
				{
					if (delegate471.Target == instance)
					{
						this.OnMultiplayerDeleteCertificateResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate471;
					}
				}
			}
			if (this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent != null)
			{
				foreach (Delegate delegate472 in this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent.GetInvocationList())
				{
					if (delegate472.Target == instance)
					{
						this.OnMultiplayerDeleteContainerImageRepositoryRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteContainerImageRequest>)delegate472;
					}
				}
			}
			if (this.OnMultiplayerDeleteContainerImageRepositoryResultEvent != null)
			{
				foreach (Delegate delegate473 in this.OnMultiplayerDeleteContainerImageRepositoryResultEvent.GetInvocationList())
				{
					if (delegate473.Target == instance)
					{
						this.OnMultiplayerDeleteContainerImageRepositoryResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate473;
					}
				}
			}
			if (this.OnMultiplayerDeleteRemoteUserRequestEvent != null)
			{
				foreach (Delegate delegate474 in this.OnMultiplayerDeleteRemoteUserRequestEvent.GetInvocationList())
				{
					if (delegate474.Target == instance)
					{
						this.OnMultiplayerDeleteRemoteUserRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<DeleteRemoteUserRequest>)delegate474;
					}
				}
			}
			if (this.OnMultiplayerDeleteRemoteUserResultEvent != null)
			{
				foreach (Delegate delegate475 in this.OnMultiplayerDeleteRemoteUserResultEvent.GetInvocationList())
				{
					if (delegate475.Target == instance)
					{
						this.OnMultiplayerDeleteRemoteUserResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate475;
					}
				}
			}
			if (this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent != null)
			{
				foreach (Delegate delegate476 in this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent.GetInvocationList())
				{
					if (delegate476.Target == instance)
					{
						this.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<EnableMultiplayerServersForTitleRequest>)delegate476;
					}
				}
			}
			if (this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent != null)
			{
				foreach (Delegate delegate477 in this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent.GetInvocationList())
				{
					if (delegate477.Target == instance)
					{
						this.OnMultiplayerEnableMultiplayerServersForTitleResultEvent -= (PlayFabEvents.PlayFabResultEvent<EnableMultiplayerServersForTitleResponse>)delegate477;
					}
				}
			}
			if (this.OnMultiplayerGetAssetUploadUrlRequestEvent != null)
			{
				foreach (Delegate delegate478 in this.OnMultiplayerGetAssetUploadUrlRequestEvent.GetInvocationList())
				{
					if (delegate478.Target == instance)
					{
						this.OnMultiplayerGetAssetUploadUrlRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetAssetUploadUrlRequest>)delegate478;
					}
				}
			}
			if (this.OnMultiplayerGetAssetUploadUrlResultEvent != null)
			{
				foreach (Delegate delegate479 in this.OnMultiplayerGetAssetUploadUrlResultEvent.GetInvocationList())
				{
					if (delegate479.Target == instance)
					{
						this.OnMultiplayerGetAssetUploadUrlResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetAssetUploadUrlResponse>)delegate479;
					}
				}
			}
			if (this.OnMultiplayerGetBuildRequestEvent != null)
			{
				foreach (Delegate delegate480 in this.OnMultiplayerGetBuildRequestEvent.GetInvocationList())
				{
					if (delegate480.Target == instance)
					{
						this.OnMultiplayerGetBuildRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetBuildRequest>)delegate480;
					}
				}
			}
			if (this.OnMultiplayerGetBuildResultEvent != null)
			{
				foreach (Delegate delegate481 in this.OnMultiplayerGetBuildResultEvent.GetInvocationList())
				{
					if (delegate481.Target == instance)
					{
						this.OnMultiplayerGetBuildResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetBuildResponse>)delegate481;
					}
				}
			}
			if (this.OnMultiplayerGetBuildAliasRequestEvent != null)
			{
				foreach (Delegate delegate482 in this.OnMultiplayerGetBuildAliasRequestEvent.GetInvocationList())
				{
					if (delegate482.Target == instance)
					{
						this.OnMultiplayerGetBuildAliasRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetBuildAliasRequest>)delegate482;
					}
				}
			}
			if (this.OnMultiplayerGetBuildAliasResultEvent != null)
			{
				foreach (Delegate delegate483 in this.OnMultiplayerGetBuildAliasResultEvent.GetInvocationList())
				{
					if (delegate483.Target == instance)
					{
						this.OnMultiplayerGetBuildAliasResultEvent -= (PlayFabEvents.PlayFabResultEvent<BuildAliasDetailsResponse>)delegate483;
					}
				}
			}
			if (this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent != null)
			{
				foreach (Delegate delegate484 in this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent.GetInvocationList())
				{
					if (delegate484.Target == instance)
					{
						this.OnMultiplayerGetContainerRegistryCredentialsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetContainerRegistryCredentialsRequest>)delegate484;
					}
				}
			}
			if (this.OnMultiplayerGetContainerRegistryCredentialsResultEvent != null)
			{
				foreach (Delegate delegate485 in this.OnMultiplayerGetContainerRegistryCredentialsResultEvent.GetInvocationList())
				{
					if (delegate485.Target == instance)
					{
						this.OnMultiplayerGetContainerRegistryCredentialsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetContainerRegistryCredentialsResponse>)delegate485;
					}
				}
			}
			if (this.OnMultiplayerGetMatchRequestEvent != null)
			{
				foreach (Delegate delegate486 in this.OnMultiplayerGetMatchRequestEvent.GetInvocationList())
				{
					if (delegate486.Target == instance)
					{
						this.OnMultiplayerGetMatchRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMatchRequest>)delegate486;
					}
				}
			}
			if (this.OnMultiplayerGetMatchResultEvent != null)
			{
				foreach (Delegate delegate487 in this.OnMultiplayerGetMatchResultEvent.GetInvocationList())
				{
					if (delegate487.Target == instance)
					{
						this.OnMultiplayerGetMatchResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMatchResult>)delegate487;
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingQueueRequestEvent != null)
			{
				foreach (Delegate delegate488 in this.OnMultiplayerGetMatchmakingQueueRequestEvent.GetInvocationList())
				{
					if (delegate488.Target == instance)
					{
						this.OnMultiplayerGetMatchmakingQueueRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMatchmakingQueueRequest>)delegate488;
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingQueueResultEvent != null)
			{
				foreach (Delegate delegate489 in this.OnMultiplayerGetMatchmakingQueueResultEvent.GetInvocationList())
				{
					if (delegate489.Target == instance)
					{
						this.OnMultiplayerGetMatchmakingQueueResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMatchmakingQueueResult>)delegate489;
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingTicketRequestEvent != null)
			{
				foreach (Delegate delegate490 in this.OnMultiplayerGetMatchmakingTicketRequestEvent.GetInvocationList())
				{
					if (delegate490.Target == instance)
					{
						this.OnMultiplayerGetMatchmakingTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMatchmakingTicketRequest>)delegate490;
					}
				}
			}
			if (this.OnMultiplayerGetMatchmakingTicketResultEvent != null)
			{
				foreach (Delegate delegate491 in this.OnMultiplayerGetMatchmakingTicketResultEvent.GetInvocationList())
				{
					if (delegate491.Target == instance)
					{
						this.OnMultiplayerGetMatchmakingTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMatchmakingTicketResult>)delegate491;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent != null)
			{
				foreach (Delegate delegate492 in this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent.GetInvocationList())
				{
					if (delegate492.Target == instance)
					{
						this.OnMultiplayerGetMultiplayerServerDetailsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMultiplayerServerDetailsRequest>)delegate492;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerDetailsResultEvent != null)
			{
				foreach (Delegate delegate493 in this.OnMultiplayerGetMultiplayerServerDetailsResultEvent.GetInvocationList())
				{
					if (delegate493.Target == instance)
					{
						this.OnMultiplayerGetMultiplayerServerDetailsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerDetailsResponse>)delegate493;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerLogsRequestEvent != null)
			{
				foreach (Delegate delegate494 in this.OnMultiplayerGetMultiplayerServerLogsRequestEvent.GetInvocationList())
				{
					if (delegate494.Target == instance)
					{
						this.OnMultiplayerGetMultiplayerServerLogsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMultiplayerServerLogsRequest>)delegate494;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerServerLogsResultEvent != null)
			{
				foreach (Delegate delegate495 in this.OnMultiplayerGetMultiplayerServerLogsResultEvent.GetInvocationList())
				{
					if (delegate495.Target == instance)
					{
						this.OnMultiplayerGetMultiplayerServerLogsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerLogsResponse>)delegate495;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent != null)
			{
				foreach (Delegate delegate496 in this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent.GetInvocationList())
				{
					if (delegate496.Target == instance)
					{
						this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetMultiplayerSessionLogsBySessionIdRequest>)delegate496;
					}
				}
			}
			if (this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent != null)
			{
				foreach (Delegate delegate497 in this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent.GetInvocationList())
				{
					if (delegate497.Target == instance)
					{
						this.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetMultiplayerServerLogsResponse>)delegate497;
					}
				}
			}
			if (this.OnMultiplayerGetQueueStatisticsRequestEvent != null)
			{
				foreach (Delegate delegate498 in this.OnMultiplayerGetQueueStatisticsRequestEvent.GetInvocationList())
				{
					if (delegate498.Target == instance)
					{
						this.OnMultiplayerGetQueueStatisticsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetQueueStatisticsRequest>)delegate498;
					}
				}
			}
			if (this.OnMultiplayerGetQueueStatisticsResultEvent != null)
			{
				foreach (Delegate delegate499 in this.OnMultiplayerGetQueueStatisticsResultEvent.GetInvocationList())
				{
					if (delegate499.Target == instance)
					{
						this.OnMultiplayerGetQueueStatisticsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetQueueStatisticsResult>)delegate499;
					}
				}
			}
			if (this.OnMultiplayerGetRemoteLoginEndpointRequestEvent != null)
			{
				foreach (Delegate delegate500 in this.OnMultiplayerGetRemoteLoginEndpointRequestEvent.GetInvocationList())
				{
					if (delegate500.Target == instance)
					{
						this.OnMultiplayerGetRemoteLoginEndpointRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetRemoteLoginEndpointRequest>)delegate500;
					}
				}
			}
			if (this.OnMultiplayerGetRemoteLoginEndpointResultEvent != null)
			{
				foreach (Delegate delegate501 in this.OnMultiplayerGetRemoteLoginEndpointResultEvent.GetInvocationList())
				{
					if (delegate501.Target == instance)
					{
						this.OnMultiplayerGetRemoteLoginEndpointResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetRemoteLoginEndpointResponse>)delegate501;
					}
				}
			}
			if (this.OnMultiplayerGetServerBackfillTicketRequestEvent != null)
			{
				foreach (Delegate delegate502 in this.OnMultiplayerGetServerBackfillTicketRequestEvent.GetInvocationList())
				{
					if (delegate502.Target == instance)
					{
						this.OnMultiplayerGetServerBackfillTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetServerBackfillTicketRequest>)delegate502;
					}
				}
			}
			if (this.OnMultiplayerGetServerBackfillTicketResultEvent != null)
			{
				foreach (Delegate delegate503 in this.OnMultiplayerGetServerBackfillTicketResultEvent.GetInvocationList())
				{
					if (delegate503.Target == instance)
					{
						this.OnMultiplayerGetServerBackfillTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetServerBackfillTicketResult>)delegate503;
					}
				}
			}
			if (this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent != null)
			{
				foreach (Delegate delegate504 in this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent.GetInvocationList())
				{
					if (delegate504.Target == instance)
					{
						this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleEnabledForMultiplayerServersStatusRequest>)delegate504;
					}
				}
			}
			if (this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent != null)
			{
				foreach (Delegate delegate505 in this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent.GetInvocationList())
				{
					if (delegate505.Target == instance)
					{
						this.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleEnabledForMultiplayerServersStatusResponse>)delegate505;
					}
				}
			}
			if (this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent != null)
			{
				foreach (Delegate delegate506 in this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent.GetInvocationList())
				{
					if (delegate506.Target == instance)
					{
						this.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitleMultiplayerServersQuotasRequest>)delegate506;
					}
				}
			}
			if (this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent != null)
			{
				foreach (Delegate delegate507 in this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent.GetInvocationList())
				{
					if (delegate507.Target == instance)
					{
						this.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitleMultiplayerServersQuotasResponse>)delegate507;
					}
				}
			}
			if (this.OnMultiplayerJoinMatchmakingTicketRequestEvent != null)
			{
				foreach (Delegate delegate508 in this.OnMultiplayerJoinMatchmakingTicketRequestEvent.GetInvocationList())
				{
					if (delegate508.Target == instance)
					{
						this.OnMultiplayerJoinMatchmakingTicketRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<JoinMatchmakingTicketRequest>)delegate508;
					}
				}
			}
			if (this.OnMultiplayerJoinMatchmakingTicketResultEvent != null)
			{
				foreach (Delegate delegate509 in this.OnMultiplayerJoinMatchmakingTicketResultEvent.GetInvocationList())
				{
					if (delegate509.Target == instance)
					{
						this.OnMultiplayerJoinMatchmakingTicketResultEvent -= (PlayFabEvents.PlayFabResultEvent<JoinMatchmakingTicketResult>)delegate509;
					}
				}
			}
			if (this.OnMultiplayerListArchivedMultiplayerServersRequestEvent != null)
			{
				foreach (Delegate delegate510 in this.OnMultiplayerListArchivedMultiplayerServersRequestEvent.GetInvocationList())
				{
					if (delegate510.Target == instance)
					{
						this.OnMultiplayerListArchivedMultiplayerServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest>)delegate510;
					}
				}
			}
			if (this.OnMultiplayerListArchivedMultiplayerServersResultEvent != null)
			{
				foreach (Delegate delegate511 in this.OnMultiplayerListArchivedMultiplayerServersResultEvent.GetInvocationList())
				{
					if (delegate511.Target == instance)
					{
						this.OnMultiplayerListArchivedMultiplayerServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse>)delegate511;
					}
				}
			}
			if (this.OnMultiplayerListAssetSummariesRequestEvent != null)
			{
				foreach (Delegate delegate512 in this.OnMultiplayerListAssetSummariesRequestEvent.GetInvocationList())
				{
					if (delegate512.Target == instance)
					{
						this.OnMultiplayerListAssetSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListAssetSummariesRequest>)delegate512;
					}
				}
			}
			if (this.OnMultiplayerListAssetSummariesResultEvent != null)
			{
				foreach (Delegate delegate513 in this.OnMultiplayerListAssetSummariesResultEvent.GetInvocationList())
				{
					if (delegate513.Target == instance)
					{
						this.OnMultiplayerListAssetSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListAssetSummariesResponse>)delegate513;
					}
				}
			}
			if (this.OnMultiplayerListBuildAliasesRequestEvent != null)
			{
				foreach (Delegate delegate514 in this.OnMultiplayerListBuildAliasesRequestEvent.GetInvocationList())
				{
					if (delegate514.Target == instance)
					{
						this.OnMultiplayerListBuildAliasesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<MultiplayerEmptyRequest>)delegate514;
					}
				}
			}
			if (this.OnMultiplayerListBuildAliasesResultEvent != null)
			{
				foreach (Delegate delegate515 in this.OnMultiplayerListBuildAliasesResultEvent.GetInvocationList())
				{
					if (delegate515.Target == instance)
					{
						this.OnMultiplayerListBuildAliasesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListBuildAliasesForTitleResponse>)delegate515;
					}
				}
			}
			if (this.OnMultiplayerListBuildSummariesRequestEvent != null)
			{
				foreach (Delegate delegate516 in this.OnMultiplayerListBuildSummariesRequestEvent.GetInvocationList())
				{
					if (delegate516.Target == instance)
					{
						this.OnMultiplayerListBuildSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListBuildSummariesRequest>)delegate516;
					}
				}
			}
			if (this.OnMultiplayerListBuildSummariesResultEvent != null)
			{
				foreach (Delegate delegate517 in this.OnMultiplayerListBuildSummariesResultEvent.GetInvocationList())
				{
					if (delegate517.Target == instance)
					{
						this.OnMultiplayerListBuildSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListBuildSummariesResponse>)delegate517;
					}
				}
			}
			if (this.OnMultiplayerListCertificateSummariesRequestEvent != null)
			{
				foreach (Delegate delegate518 in this.OnMultiplayerListCertificateSummariesRequestEvent.GetInvocationList())
				{
					if (delegate518.Target == instance)
					{
						this.OnMultiplayerListCertificateSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListCertificateSummariesRequest>)delegate518;
					}
				}
			}
			if (this.OnMultiplayerListCertificateSummariesResultEvent != null)
			{
				foreach (Delegate delegate519 in this.OnMultiplayerListCertificateSummariesResultEvent.GetInvocationList())
				{
					if (delegate519.Target == instance)
					{
						this.OnMultiplayerListCertificateSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListCertificateSummariesResponse>)delegate519;
					}
				}
			}
			if (this.OnMultiplayerListContainerImagesRequestEvent != null)
			{
				foreach (Delegate delegate520 in this.OnMultiplayerListContainerImagesRequestEvent.GetInvocationList())
				{
					if (delegate520.Target == instance)
					{
						this.OnMultiplayerListContainerImagesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListContainerImagesRequest>)delegate520;
					}
				}
			}
			if (this.OnMultiplayerListContainerImagesResultEvent != null)
			{
				foreach (Delegate delegate521 in this.OnMultiplayerListContainerImagesResultEvent.GetInvocationList())
				{
					if (delegate521.Target == instance)
					{
						this.OnMultiplayerListContainerImagesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListContainerImagesResponse>)delegate521;
					}
				}
			}
			if (this.OnMultiplayerListContainerImageTagsRequestEvent != null)
			{
				foreach (Delegate delegate522 in this.OnMultiplayerListContainerImageTagsRequestEvent.GetInvocationList())
				{
					if (delegate522.Target == instance)
					{
						this.OnMultiplayerListContainerImageTagsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListContainerImageTagsRequest>)delegate522;
					}
				}
			}
			if (this.OnMultiplayerListContainerImageTagsResultEvent != null)
			{
				foreach (Delegate delegate523 in this.OnMultiplayerListContainerImageTagsResultEvent.GetInvocationList())
				{
					if (delegate523.Target == instance)
					{
						this.OnMultiplayerListContainerImageTagsResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListContainerImageTagsResponse>)delegate523;
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingQueuesRequestEvent != null)
			{
				foreach (Delegate delegate524 in this.OnMultiplayerListMatchmakingQueuesRequestEvent.GetInvocationList())
				{
					if (delegate524.Target == instance)
					{
						this.OnMultiplayerListMatchmakingQueuesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMatchmakingQueuesRequest>)delegate524;
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingQueuesResultEvent != null)
			{
				foreach (Delegate delegate525 in this.OnMultiplayerListMatchmakingQueuesResultEvent.GetInvocationList())
				{
					if (delegate525.Target == instance)
					{
						this.OnMultiplayerListMatchmakingQueuesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMatchmakingQueuesResult>)delegate525;
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent != null)
			{
				foreach (Delegate delegate526 in this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent.GetInvocationList())
				{
					if (delegate526.Target == instance)
					{
						this.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMatchmakingTicketsForPlayerRequest>)delegate526;
					}
				}
			}
			if (this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent != null)
			{
				foreach (Delegate delegate527 in this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent.GetInvocationList())
				{
					if (delegate527.Target == instance)
					{
						this.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMatchmakingTicketsForPlayerResult>)delegate527;
					}
				}
			}
			if (this.OnMultiplayerListMultiplayerServersRequestEvent != null)
			{
				foreach (Delegate delegate528 in this.OnMultiplayerListMultiplayerServersRequestEvent.GetInvocationList())
				{
					if (delegate528.Target == instance)
					{
						this.OnMultiplayerListMultiplayerServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListMultiplayerServersRequest>)delegate528;
					}
				}
			}
			if (this.OnMultiplayerListMultiplayerServersResultEvent != null)
			{
				foreach (Delegate delegate529 in this.OnMultiplayerListMultiplayerServersResultEvent.GetInvocationList())
				{
					if (delegate529.Target == instance)
					{
						this.OnMultiplayerListMultiplayerServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListMultiplayerServersResponse>)delegate529;
					}
				}
			}
			if (this.OnMultiplayerListPartyQosServersRequestEvent != null)
			{
				foreach (Delegate delegate530 in this.OnMultiplayerListPartyQosServersRequestEvent.GetInvocationList())
				{
					if (delegate530.Target == instance)
					{
						this.OnMultiplayerListPartyQosServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListPartyQosServersRequest>)delegate530;
					}
				}
			}
			if (this.OnMultiplayerListPartyQosServersResultEvent != null)
			{
				foreach (Delegate delegate531 in this.OnMultiplayerListPartyQosServersResultEvent.GetInvocationList())
				{
					if (delegate531.Target == instance)
					{
						this.OnMultiplayerListPartyQosServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListPartyQosServersResponse>)delegate531;
					}
				}
			}
			if (this.OnMultiplayerListQosServersRequestEvent != null)
			{
				foreach (Delegate delegate532 in this.OnMultiplayerListQosServersRequestEvent.GetInvocationList())
				{
					if (delegate532.Target == instance)
					{
						this.OnMultiplayerListQosServersRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListQosServersRequest>)delegate532;
					}
				}
			}
			if (this.OnMultiplayerListQosServersResultEvent != null)
			{
				foreach (Delegate delegate533 in this.OnMultiplayerListQosServersResultEvent.GetInvocationList())
				{
					if (delegate533.Target == instance)
					{
						this.OnMultiplayerListQosServersResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListQosServersResponse>)delegate533;
					}
				}
			}
			if (this.OnMultiplayerListQosServersForTitleRequestEvent != null)
			{
				foreach (Delegate delegate534 in this.OnMultiplayerListQosServersForTitleRequestEvent.GetInvocationList())
				{
					if (delegate534.Target == instance)
					{
						this.OnMultiplayerListQosServersForTitleRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListQosServersForTitleRequest>)delegate534;
					}
				}
			}
			if (this.OnMultiplayerListQosServersForTitleResultEvent != null)
			{
				foreach (Delegate delegate535 in this.OnMultiplayerListQosServersForTitleResultEvent.GetInvocationList())
				{
					if (delegate535.Target == instance)
					{
						this.OnMultiplayerListQosServersForTitleResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListQosServersForTitleResponse>)delegate535;
					}
				}
			}
			if (this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent != null)
			{
				foreach (Delegate delegate536 in this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent.GetInvocationList())
				{
					if (delegate536.Target == instance)
					{
						this.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListServerBackfillTicketsForPlayerRequest>)delegate536;
					}
				}
			}
			if (this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent != null)
			{
				foreach (Delegate delegate537 in this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent.GetInvocationList())
				{
					if (delegate537.Target == instance)
					{
						this.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListServerBackfillTicketsForPlayerResult>)delegate537;
					}
				}
			}
			if (this.OnMultiplayerListVirtualMachineSummariesRequestEvent != null)
			{
				foreach (Delegate delegate538 in this.OnMultiplayerListVirtualMachineSummariesRequestEvent.GetInvocationList())
				{
					if (delegate538.Target == instance)
					{
						this.OnMultiplayerListVirtualMachineSummariesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ListVirtualMachineSummariesRequest>)delegate538;
					}
				}
			}
			if (this.OnMultiplayerListVirtualMachineSummariesResultEvent != null)
			{
				foreach (Delegate delegate539 in this.OnMultiplayerListVirtualMachineSummariesResultEvent.GetInvocationList())
				{
					if (delegate539.Target == instance)
					{
						this.OnMultiplayerListVirtualMachineSummariesResultEvent -= (PlayFabEvents.PlayFabResultEvent<ListVirtualMachineSummariesResponse>)delegate539;
					}
				}
			}
			if (this.OnMultiplayerRemoveMatchmakingQueueRequestEvent != null)
			{
				foreach (Delegate delegate540 in this.OnMultiplayerRemoveMatchmakingQueueRequestEvent.GetInvocationList())
				{
					if (delegate540.Target == instance)
					{
						this.OnMultiplayerRemoveMatchmakingQueueRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RemoveMatchmakingQueueRequest>)delegate540;
					}
				}
			}
			if (this.OnMultiplayerRemoveMatchmakingQueueResultEvent != null)
			{
				foreach (Delegate delegate541 in this.OnMultiplayerRemoveMatchmakingQueueResultEvent.GetInvocationList())
				{
					if (delegate541.Target == instance)
					{
						this.OnMultiplayerRemoveMatchmakingQueueResultEvent -= (PlayFabEvents.PlayFabResultEvent<RemoveMatchmakingQueueResult>)delegate541;
					}
				}
			}
			if (this.OnMultiplayerRequestMultiplayerServerRequestEvent != null)
			{
				foreach (Delegate delegate542 in this.OnMultiplayerRequestMultiplayerServerRequestEvent.GetInvocationList())
				{
					if (delegate542.Target == instance)
					{
						this.OnMultiplayerRequestMultiplayerServerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RequestMultiplayerServerRequest>)delegate542;
					}
				}
			}
			if (this.OnMultiplayerRequestMultiplayerServerResultEvent != null)
			{
				foreach (Delegate delegate543 in this.OnMultiplayerRequestMultiplayerServerResultEvent.GetInvocationList())
				{
					if (delegate543.Target == instance)
					{
						this.OnMultiplayerRequestMultiplayerServerResultEvent -= (PlayFabEvents.PlayFabResultEvent<RequestMultiplayerServerResponse>)delegate543;
					}
				}
			}
			if (this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent != null)
			{
				foreach (Delegate delegate544 in this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent.GetInvocationList())
				{
					if (delegate544.Target == instance)
					{
						this.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<RolloverContainerRegistryCredentialsRequest>)delegate544;
					}
				}
			}
			if (this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent != null)
			{
				foreach (Delegate delegate545 in this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent.GetInvocationList())
				{
					if (delegate545.Target == instance)
					{
						this.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent -= (PlayFabEvents.PlayFabResultEvent<RolloverContainerRegistryCredentialsResponse>)delegate545;
					}
				}
			}
			if (this.OnMultiplayerSetMatchmakingQueueRequestEvent != null)
			{
				foreach (Delegate delegate546 in this.OnMultiplayerSetMatchmakingQueueRequestEvent.GetInvocationList())
				{
					if (delegate546.Target == instance)
					{
						this.OnMultiplayerSetMatchmakingQueueRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetMatchmakingQueueRequest>)delegate546;
					}
				}
			}
			if (this.OnMultiplayerSetMatchmakingQueueResultEvent != null)
			{
				foreach (Delegate delegate547 in this.OnMultiplayerSetMatchmakingQueueResultEvent.GetInvocationList())
				{
					if (delegate547.Target == instance)
					{
						this.OnMultiplayerSetMatchmakingQueueResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetMatchmakingQueueResult>)delegate547;
					}
				}
			}
			if (this.OnMultiplayerShutdownMultiplayerServerRequestEvent != null)
			{
				foreach (Delegate delegate548 in this.OnMultiplayerShutdownMultiplayerServerRequestEvent.GetInvocationList())
				{
					if (delegate548.Target == instance)
					{
						this.OnMultiplayerShutdownMultiplayerServerRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<ShutdownMultiplayerServerRequest>)delegate548;
					}
				}
			}
			if (this.OnMultiplayerShutdownMultiplayerServerResultEvent != null)
			{
				foreach (Delegate delegate549 in this.OnMultiplayerShutdownMultiplayerServerResultEvent.GetInvocationList())
				{
					if (delegate549.Target == instance)
					{
						this.OnMultiplayerShutdownMultiplayerServerResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate549;
					}
				}
			}
			if (this.OnMultiplayerUntagContainerImageRequestEvent != null)
			{
				foreach (Delegate delegate550 in this.OnMultiplayerUntagContainerImageRequestEvent.GetInvocationList())
				{
					if (delegate550.Target == instance)
					{
						this.OnMultiplayerUntagContainerImageRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UntagContainerImageRequest>)delegate550;
					}
				}
			}
			if (this.OnMultiplayerUntagContainerImageResultEvent != null)
			{
				foreach (Delegate delegate551 in this.OnMultiplayerUntagContainerImageResultEvent.GetInvocationList())
				{
					if (delegate551.Target == instance)
					{
						this.OnMultiplayerUntagContainerImageResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate551;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildAliasRequestEvent != null)
			{
				foreach (Delegate delegate552 in this.OnMultiplayerUpdateBuildAliasRequestEvent.GetInvocationList())
				{
					if (delegate552.Target == instance)
					{
						this.OnMultiplayerUpdateBuildAliasRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateBuildAliasRequest>)delegate552;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildAliasResultEvent != null)
			{
				foreach (Delegate delegate553 in this.OnMultiplayerUpdateBuildAliasResultEvent.GetInvocationList())
				{
					if (delegate553.Target == instance)
					{
						this.OnMultiplayerUpdateBuildAliasResultEvent -= (PlayFabEvents.PlayFabResultEvent<BuildAliasDetailsResponse>)delegate553;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionRequestEvent != null)
			{
				foreach (Delegate delegate554 in this.OnMultiplayerUpdateBuildRegionRequestEvent.GetInvocationList())
				{
					if (delegate554.Target == instance)
					{
						this.OnMultiplayerUpdateBuildRegionRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateBuildRegionRequest>)delegate554;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionResultEvent != null)
			{
				foreach (Delegate delegate555 in this.OnMultiplayerUpdateBuildRegionResultEvent.GetInvocationList())
				{
					if (delegate555.Target == instance)
					{
						this.OnMultiplayerUpdateBuildRegionResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate555;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionsRequestEvent != null)
			{
				foreach (Delegate delegate556 in this.OnMultiplayerUpdateBuildRegionsRequestEvent.GetInvocationList())
				{
					if (delegate556.Target == instance)
					{
						this.OnMultiplayerUpdateBuildRegionsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UpdateBuildRegionsRequest>)delegate556;
					}
				}
			}
			if (this.OnMultiplayerUpdateBuildRegionsResultEvent != null)
			{
				foreach (Delegate delegate557 in this.OnMultiplayerUpdateBuildRegionsResultEvent.GetInvocationList())
				{
					if (delegate557.Target == instance)
					{
						this.OnMultiplayerUpdateBuildRegionsResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate557;
					}
				}
			}
			if (this.OnMultiplayerUploadCertificateRequestEvent != null)
			{
				foreach (Delegate delegate558 in this.OnMultiplayerUploadCertificateRequestEvent.GetInvocationList())
				{
					if (delegate558.Target == instance)
					{
						this.OnMultiplayerUploadCertificateRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<UploadCertificateRequest>)delegate558;
					}
				}
			}
			if (this.OnMultiplayerUploadCertificateResultEvent != null)
			{
				foreach (Delegate delegate559 in this.OnMultiplayerUploadCertificateResultEvent.GetInvocationList())
				{
					if (delegate559.Target == instance)
					{
						this.OnMultiplayerUploadCertificateResultEvent -= (PlayFabEvents.PlayFabResultEvent<PlayFab.MultiplayerModels.EmptyResponse>)delegate559;
					}
				}
			}
			if (this.OnProfilesGetGlobalPolicyRequestEvent != null)
			{
				foreach (Delegate delegate560 in this.OnProfilesGetGlobalPolicyRequestEvent.GetInvocationList())
				{
					if (delegate560.Target == instance)
					{
						this.OnProfilesGetGlobalPolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetGlobalPolicyRequest>)delegate560;
					}
				}
			}
			if (this.OnProfilesGetGlobalPolicyResultEvent != null)
			{
				foreach (Delegate delegate561 in this.OnProfilesGetGlobalPolicyResultEvent.GetInvocationList())
				{
					if (delegate561.Target == instance)
					{
						this.OnProfilesGetGlobalPolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetGlobalPolicyResponse>)delegate561;
					}
				}
			}
			if (this.OnProfilesGetProfileRequestEvent != null)
			{
				foreach (Delegate delegate562 in this.OnProfilesGetProfileRequestEvent.GetInvocationList())
				{
					if (delegate562.Target == instance)
					{
						this.OnProfilesGetProfileRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetEntityProfileRequest>)delegate562;
					}
				}
			}
			if (this.OnProfilesGetProfileResultEvent != null)
			{
				foreach (Delegate delegate563 in this.OnProfilesGetProfileResultEvent.GetInvocationList())
				{
					if (delegate563.Target == instance)
					{
						this.OnProfilesGetProfileResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetEntityProfileResponse>)delegate563;
					}
				}
			}
			if (this.OnProfilesGetProfilesRequestEvent != null)
			{
				foreach (Delegate delegate564 in this.OnProfilesGetProfilesRequestEvent.GetInvocationList())
				{
					if (delegate564.Target == instance)
					{
						this.OnProfilesGetProfilesRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetEntityProfilesRequest>)delegate564;
					}
				}
			}
			if (this.OnProfilesGetProfilesResultEvent != null)
			{
				foreach (Delegate delegate565 in this.OnProfilesGetProfilesResultEvent.GetInvocationList())
				{
					if (delegate565.Target == instance)
					{
						this.OnProfilesGetProfilesResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetEntityProfilesResponse>)delegate565;
					}
				}
			}
			if (this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent != null)
			{
				foreach (Delegate delegate566 in this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent.GetInvocationList())
				{
					if (delegate566.Target == instance)
					{
						this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<GetTitlePlayersFromMasterPlayerAccountIdsRequest>)delegate566;
					}
				}
			}
			if (this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent != null)
			{
				foreach (Delegate delegate567 in this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent.GetInvocationList())
				{
					if (delegate567.Target == instance)
					{
						this.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent -= (PlayFabEvents.PlayFabResultEvent<GetTitlePlayersFromMasterPlayerAccountIdsResponse>)delegate567;
					}
				}
			}
			if (this.OnProfilesSetGlobalPolicyRequestEvent != null)
			{
				foreach (Delegate delegate568 in this.OnProfilesSetGlobalPolicyRequestEvent.GetInvocationList())
				{
					if (delegate568.Target == instance)
					{
						this.OnProfilesSetGlobalPolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetGlobalPolicyRequest>)delegate568;
					}
				}
			}
			if (this.OnProfilesSetGlobalPolicyResultEvent != null)
			{
				foreach (Delegate delegate569 in this.OnProfilesSetGlobalPolicyResultEvent.GetInvocationList())
				{
					if (delegate569.Target == instance)
					{
						this.OnProfilesSetGlobalPolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetGlobalPolicyResponse>)delegate569;
					}
				}
			}
			if (this.OnProfilesSetProfileLanguageRequestEvent != null)
			{
				foreach (Delegate delegate570 in this.OnProfilesSetProfileLanguageRequestEvent.GetInvocationList())
				{
					if (delegate570.Target == instance)
					{
						this.OnProfilesSetProfileLanguageRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetProfileLanguageRequest>)delegate570;
					}
				}
			}
			if (this.OnProfilesSetProfileLanguageResultEvent != null)
			{
				foreach (Delegate delegate571 in this.OnProfilesSetProfileLanguageResultEvent.GetInvocationList())
				{
					if (delegate571.Target == instance)
					{
						this.OnProfilesSetProfileLanguageResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetProfileLanguageResponse>)delegate571;
					}
				}
			}
			if (this.OnProfilesSetProfilePolicyRequestEvent != null)
			{
				foreach (Delegate delegate572 in this.OnProfilesSetProfilePolicyRequestEvent.GetInvocationList())
				{
					if (delegate572.Target == instance)
					{
						this.OnProfilesSetProfilePolicyRequestEvent -= (PlayFabEvents.PlayFabRequestEvent<SetEntityProfilePolicyRequest>)delegate572;
					}
				}
			}
			if (this.OnProfilesSetProfilePolicyResultEvent != null)
			{
				foreach (Delegate delegate573 in this.OnProfilesSetProfilePolicyResultEvent.GetInvocationList())
				{
					if (delegate573.Target == instance)
					{
						this.OnProfilesSetProfilePolicyResultEvent -= (PlayFabEvents.PlayFabResultEvent<SetEntityProfilePolicyResponse>)delegate573;
					}
				}
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002FA6D File Offset: 0x0002DC6D
		private void OnProcessingErrorEvent(PlayFabRequestCommon request, PlayFabError error)
		{
			if (PlayFabEvents._instance.OnGlobalErrorEvent != null)
			{
				PlayFabEvents._instance.OnGlobalErrorEvent(request, error);
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002FA8C File Offset: 0x0002DC8C
		private void OnProcessingEvent(ApiProcessingEventArgs e)
		{
			if (e.EventType == ApiProcessingEventType.Pre)
			{
				Type type = e.Request.GetType();
				if (type == typeof(AcceptTradeRequest) && PlayFabEvents._instance.OnAcceptTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnAcceptTradeRequestEvent((AcceptTradeRequest)e.Request);
					return;
				}
				if (type == typeof(AddFriendRequest) && PlayFabEvents._instance.OnAddFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddFriendRequestEvent((AddFriendRequest)e.Request);
					return;
				}
				if (type == typeof(AddGenericIDRequest) && PlayFabEvents._instance.OnAddGenericIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddGenericIDRequestEvent((AddGenericIDRequest)e.Request);
					return;
				}
				if (type == typeof(AddOrUpdateContactEmailRequest) && PlayFabEvents._instance.OnAddOrUpdateContactEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddOrUpdateContactEmailRequestEvent((AddOrUpdateContactEmailRequest)e.Request);
					return;
				}
				if (type == typeof(AddSharedGroupMembersRequest) && PlayFabEvents._instance.OnAddSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddSharedGroupMembersRequestEvent((AddSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(AddUsernamePasswordRequest) && PlayFabEvents._instance.OnAddUsernamePasswordRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddUsernamePasswordRequestEvent((AddUsernamePasswordRequest)e.Request);
					return;
				}
				if (type == typeof(AddUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnAddUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnAddUserVirtualCurrencyRequestEvent((AddUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(AndroidDevicePushNotificationRegistrationRequest) && PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationRequestEvent != null)
				{
					PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationRequestEvent((AndroidDevicePushNotificationRegistrationRequest)e.Request);
					return;
				}
				if (type == typeof(AttributeInstallRequest) && PlayFabEvents._instance.OnAttributeInstallRequestEvent != null)
				{
					PlayFabEvents._instance.OnAttributeInstallRequestEvent((AttributeInstallRequest)e.Request);
					return;
				}
				if (type == typeof(CancelTradeRequest) && PlayFabEvents._instance.OnCancelTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnCancelTradeRequestEvent((CancelTradeRequest)e.Request);
					return;
				}
				if (type == typeof(ConfirmPurchaseRequest) && PlayFabEvents._instance.OnConfirmPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnConfirmPurchaseRequestEvent((ConfirmPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(ConsumeItemRequest) && PlayFabEvents._instance.OnConsumeItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumeItemRequestEvent((ConsumeItemRequest)e.Request);
					return;
				}
				if (type == typeof(ConsumePSNEntitlementsRequest) && PlayFabEvents._instance.OnConsumePSNEntitlementsRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumePSNEntitlementsRequestEvent((ConsumePSNEntitlementsRequest)e.Request);
					return;
				}
				if (type == typeof(ConsumeXboxEntitlementsRequest) && PlayFabEvents._instance.OnConsumeXboxEntitlementsRequestEvent != null)
				{
					PlayFabEvents._instance.OnConsumeXboxEntitlementsRequestEvent((ConsumeXboxEntitlementsRequest)e.Request);
					return;
				}
				if (type == typeof(CreateSharedGroupRequest) && PlayFabEvents._instance.OnCreateSharedGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnCreateSharedGroupRequestEvent((CreateSharedGroupRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteCloudScriptRequest) && PlayFabEvents._instance.OnExecuteCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnExecuteCloudScriptRequestEvent((ExecuteCloudScriptRequest)e.Request);
					return;
				}
				if (type == typeof(GetAccountInfoRequest) && PlayFabEvents._instance.OnGetAccountInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAccountInfoRequestEvent((GetAccountInfoRequest)e.Request);
					return;
				}
				if (type == typeof(GetAdPlacementsRequest) && PlayFabEvents._instance.OnGetAdPlacementsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAdPlacementsRequestEvent((GetAdPlacementsRequest)e.Request);
					return;
				}
				if (type == typeof(ListUsersCharactersRequest) && PlayFabEvents._instance.OnGetAllUsersCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetAllUsersCharactersRequestEvent((ListUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(GetCatalogItemsRequest) && PlayFabEvents._instance.OnGetCatalogItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCatalogItemsRequestEvent((GetCatalogItemsRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterDataRequest) && PlayFabEvents._instance.OnGetCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterDataRequestEvent((GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterInventoryRequest) && PlayFabEvents._instance.OnGetCharacterInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterInventoryRequestEvent((GetCharacterInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterLeaderboardRequest) && PlayFabEvents._instance.OnGetCharacterLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterLeaderboardRequestEvent((GetCharacterLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterDataRequest) && PlayFabEvents._instance.OnGetCharacterReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterReadOnlyDataRequestEvent((GetCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetCharacterStatisticsRequest) && PlayFabEvents._instance.OnGetCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterStatisticsRequestEvent((GetCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(GetContentDownloadUrlRequest) && PlayFabEvents._instance.OnGetContentDownloadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetContentDownloadUrlRequestEvent((GetContentDownloadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(CurrentGamesRequest) && PlayFabEvents._instance.OnGetCurrentGamesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetCurrentGamesRequestEvent((CurrentGamesRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendLeaderboardRequest) && PlayFabEvents._instance.OnGetFriendLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardRequestEvent((GetFriendLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendLeaderboardAroundPlayerRequest) && PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerRequestEvent((GetFriendLeaderboardAroundPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(GetFriendsListRequest) && PlayFabEvents._instance.OnGetFriendsListRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendsListRequestEvent((GetFriendsListRequest)e.Request);
					return;
				}
				if (type == typeof(GameServerRegionsRequest) && PlayFabEvents._instance.OnGetGameServerRegionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetGameServerRegionsRequestEvent((GameServerRegionsRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardRequest) && PlayFabEvents._instance.OnGetLeaderboardRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardRequestEvent((GetLeaderboardRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardAroundCharacterRequest) && PlayFabEvents._instance.OnGetLeaderboardAroundCharacterRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundCharacterRequestEvent((GetLeaderboardAroundCharacterRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardAroundPlayerRequest) && PlayFabEvents._instance.OnGetLeaderboardAroundPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundPlayerRequestEvent((GetLeaderboardAroundPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(GetLeaderboardForUsersCharactersRequest) && PlayFabEvents._instance.OnGetLeaderboardForUserCharactersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardForUserCharactersRequestEvent((GetLeaderboardForUsersCharactersRequest)e.Request);
					return;
				}
				if (type == typeof(GetPaymentTokenRequest) && PlayFabEvents._instance.OnGetPaymentTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPaymentTokenRequestEvent((GetPaymentTokenRequest)e.Request);
					return;
				}
				if (type == typeof(GetPhotonAuthenticationTokenRequest) && PlayFabEvents._instance.OnGetPhotonAuthenticationTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPhotonAuthenticationTokenRequestEvent((GetPhotonAuthenticationTokenRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerCombinedInfoRequest) && PlayFabEvents._instance.OnGetPlayerCombinedInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerCombinedInfoRequestEvent((GetPlayerCombinedInfoRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerProfileRequest) && PlayFabEvents._instance.OnGetPlayerProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerProfileRequestEvent((GetPlayerProfileRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerSegmentsRequest) && PlayFabEvents._instance.OnGetPlayerSegmentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerSegmentsRequestEvent((GetPlayerSegmentsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerStatisticsRequest) && PlayFabEvents._instance.OnGetPlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticsRequestEvent((GetPlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerStatisticVersionsRequest) && PlayFabEvents._instance.OnGetPlayerStatisticVersionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticVersionsRequestEvent((GetPlayerStatisticVersionsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerTagsRequest) && PlayFabEvents._instance.OnGetPlayerTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTagsRequestEvent((GetPlayerTagsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayerTradesRequest) && PlayFabEvents._instance.OnGetPlayerTradesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTradesRequestEvent((GetPlayerTradesRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromFacebookIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsRequestEvent((GetPlayFabIDsFromFacebookIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromFacebookInstantGamesIdsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsRequestEvent((GetPlayFabIDsFromFacebookInstantGamesIdsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGameCenterIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsRequestEvent((GetPlayFabIDsFromGameCenterIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGenericIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsRequestEvent((GetPlayFabIDsFromGenericIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromGoogleIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsRequestEvent((GetPlayFabIDsFromGoogleIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromKongregateIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsRequestEvent((GetPlayFabIDsFromKongregateIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsRequestEvent((GetPlayFabIDsFromNintendoSwitchDeviceIdsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromPSNAccountIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsRequestEvent((GetPlayFabIDsFromPSNAccountIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromSteamIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsRequestEvent((GetPlayFabIDsFromSteamIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromTwitchIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsRequestEvent((GetPlayFabIDsFromTwitchIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPlayFabIDsFromXboxLiveIDsRequest) && PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsRequestEvent((GetPlayFabIDsFromXboxLiveIDsRequest)e.Request);
					return;
				}
				if (type == typeof(GetPublisherDataRequest) && PlayFabEvents._instance.OnGetPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPublisherDataRequestEvent((GetPublisherDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetPurchaseRequest) && PlayFabEvents._instance.OnGetPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetPurchaseRequestEvent((GetPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(GetSharedGroupDataRequest) && PlayFabEvents._instance.OnGetSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetSharedGroupDataRequestEvent((GetSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetStoreItemsRequest) && PlayFabEvents._instance.OnGetStoreItemsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetStoreItemsRequestEvent((GetStoreItemsRequest)e.Request);
					return;
				}
				if (type == typeof(GetTimeRequest) && PlayFabEvents._instance.OnGetTimeRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTimeRequestEvent((GetTimeRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleDataRequest) && PlayFabEvents._instance.OnGetTitleDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleDataRequestEvent((GetTitleDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleNewsRequest) && PlayFabEvents._instance.OnGetTitleNewsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleNewsRequestEvent((GetTitleNewsRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitlePublicKeyRequest) && PlayFabEvents._instance.OnGetTitlePublicKeyRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTitlePublicKeyRequestEvent((GetTitlePublicKeyRequest)e.Request);
					return;
				}
				if (type == typeof(GetTradeStatusRequest) && PlayFabEvents._instance.OnGetTradeStatusRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetTradeStatusRequestEvent((GetTradeStatusRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserInventoryRequest) && PlayFabEvents._instance.OnGetUserInventoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserInventoryRequestEvent((GetUserInventoryRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetUserDataRequest) && PlayFabEvents._instance.OnGetUserReadOnlyDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetUserReadOnlyDataRequestEvent((GetUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(GetWindowsHelloChallengeRequest) && PlayFabEvents._instance.OnGetWindowsHelloChallengeRequestEvent != null)
				{
					PlayFabEvents._instance.OnGetWindowsHelloChallengeRequestEvent((GetWindowsHelloChallengeRequest)e.Request);
					return;
				}
				if (type == typeof(GrantCharacterToUserRequest) && PlayFabEvents._instance.OnGrantCharacterToUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnGrantCharacterToUserRequestEvent((GrantCharacterToUserRequest)e.Request);
					return;
				}
				if (type == typeof(LinkAndroidDeviceIDRequest) && PlayFabEvents._instance.OnLinkAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkAndroidDeviceIDRequestEvent((LinkAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkAppleRequest) && PlayFabEvents._instance.OnLinkAppleRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkAppleRequestEvent((LinkAppleRequest)e.Request);
					return;
				}
				if (type == typeof(LinkCustomIDRequest) && PlayFabEvents._instance.OnLinkCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkCustomIDRequestEvent((LinkCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkFacebookAccountRequest) && PlayFabEvents._instance.OnLinkFacebookAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookAccountRequestEvent((LinkFacebookAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkFacebookInstantGamesIdRequest) && PlayFabEvents._instance.OnLinkFacebookInstantGamesIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookInstantGamesIdRequestEvent((LinkFacebookInstantGamesIdRequest)e.Request);
					return;
				}
				if (type == typeof(LinkGameCenterAccountRequest) && PlayFabEvents._instance.OnLinkGameCenterAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkGameCenterAccountRequestEvent((LinkGameCenterAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkGoogleAccountRequest) && PlayFabEvents._instance.OnLinkGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkGoogleAccountRequestEvent((LinkGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkIOSDeviceIDRequest) && PlayFabEvents._instance.OnLinkIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkIOSDeviceIDRequestEvent((LinkIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LinkKongregateAccountRequest) && PlayFabEvents._instance.OnLinkKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkKongregateRequestEvent((LinkKongregateAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkNintendoServiceAccountRequest) && PlayFabEvents._instance.OnLinkNintendoServiceAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkNintendoServiceAccountRequestEvent((LinkNintendoServiceAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkNintendoSwitchDeviceIdRequest) && PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdRequestEvent((LinkNintendoSwitchDeviceIdRequest)e.Request);
					return;
				}
				if (type == typeof(LinkOpenIdConnectRequest) && PlayFabEvents._instance.OnLinkOpenIdConnectRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkOpenIdConnectRequestEvent((LinkOpenIdConnectRequest)e.Request);
					return;
				}
				if (type == typeof(LinkPSNAccountRequest) && PlayFabEvents._instance.OnLinkPSNAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkPSNAccountRequestEvent((LinkPSNAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkSteamAccountRequest) && PlayFabEvents._instance.OnLinkSteamAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkSteamAccountRequestEvent((LinkSteamAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkTwitchAccountRequest) && PlayFabEvents._instance.OnLinkTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkTwitchRequestEvent((LinkTwitchAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkWindowsHelloAccountRequest) && PlayFabEvents._instance.OnLinkWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkWindowsHelloRequestEvent((LinkWindowsHelloAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LinkXboxAccountRequest) && PlayFabEvents._instance.OnLinkXboxAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLinkXboxAccountRequestEvent((LinkXboxAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithAndroidDeviceIDRequest) && PlayFabEvents._instance.OnLoginWithAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithAndroidDeviceIDRequestEvent((LoginWithAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithAppleRequest) && PlayFabEvents._instance.OnLoginWithAppleRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithAppleRequestEvent((LoginWithAppleRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithCustomIDRequest) && PlayFabEvents._instance.OnLoginWithCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithCustomIDRequestEvent((LoginWithCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithEmailAddressRequest) && PlayFabEvents._instance.OnLoginWithEmailAddressRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithEmailAddressRequestEvent((LoginWithEmailAddressRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithFacebookRequest) && PlayFabEvents._instance.OnLoginWithFacebookRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithFacebookRequestEvent((LoginWithFacebookRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithFacebookInstantGamesIdRequest) && PlayFabEvents._instance.OnLoginWithFacebookInstantGamesIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithFacebookInstantGamesIdRequestEvent((LoginWithFacebookInstantGamesIdRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithGameCenterRequest) && PlayFabEvents._instance.OnLoginWithGameCenterRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithGameCenterRequestEvent((LoginWithGameCenterRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithGoogleAccountRequest) && PlayFabEvents._instance.OnLoginWithGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithGoogleAccountRequestEvent((LoginWithGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithIOSDeviceIDRequest) && PlayFabEvents._instance.OnLoginWithIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithIOSDeviceIDRequestEvent((LoginWithIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithKongregateRequest) && PlayFabEvents._instance.OnLoginWithKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithKongregateRequestEvent((LoginWithKongregateRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithNintendoServiceAccountRequest) && PlayFabEvents._instance.OnLoginWithNintendoServiceAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithNintendoServiceAccountRequestEvent((LoginWithNintendoServiceAccountRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithNintendoSwitchDeviceIdRequest) && PlayFabEvents._instance.OnLoginWithNintendoSwitchDeviceIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithNintendoSwitchDeviceIdRequestEvent((LoginWithNintendoSwitchDeviceIdRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithOpenIdConnectRequest) && PlayFabEvents._instance.OnLoginWithOpenIdConnectRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithOpenIdConnectRequestEvent((LoginWithOpenIdConnectRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithPlayFabRequest) && PlayFabEvents._instance.OnLoginWithPlayFabRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithPlayFabRequestEvent((LoginWithPlayFabRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithPSNRequest) && PlayFabEvents._instance.OnLoginWithPSNRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithPSNRequestEvent((LoginWithPSNRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithSteamRequest) && PlayFabEvents._instance.OnLoginWithSteamRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithSteamRequestEvent((LoginWithSteamRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithTwitchRequest) && PlayFabEvents._instance.OnLoginWithTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithTwitchRequestEvent((LoginWithTwitchRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithWindowsHelloRequest) && PlayFabEvents._instance.OnLoginWithWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithWindowsHelloRequestEvent((LoginWithWindowsHelloRequest)e.Request);
					return;
				}
				if (type == typeof(LoginWithXboxRequest) && PlayFabEvents._instance.OnLoginWithXboxRequestEvent != null)
				{
					PlayFabEvents._instance.OnLoginWithXboxRequestEvent((LoginWithXboxRequest)e.Request);
					return;
				}
				if (type == typeof(MatchmakeRequest) && PlayFabEvents._instance.OnMatchmakeRequestEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakeRequestEvent((MatchmakeRequest)e.Request);
					return;
				}
				if (type == typeof(OpenTradeRequest) && PlayFabEvents._instance.OnOpenTradeRequestEvent != null)
				{
					PlayFabEvents._instance.OnOpenTradeRequestEvent((OpenTradeRequest)e.Request);
					return;
				}
				if (type == typeof(PayForPurchaseRequest) && PlayFabEvents._instance.OnPayForPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnPayForPurchaseRequestEvent((PayForPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(PurchaseItemRequest) && PlayFabEvents._instance.OnPurchaseItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnPurchaseItemRequestEvent((PurchaseItemRequest)e.Request);
					return;
				}
				if (type == typeof(RedeemCouponRequest) && PlayFabEvents._instance.OnRedeemCouponRequestEvent != null)
				{
					PlayFabEvents._instance.OnRedeemCouponRequestEvent((RedeemCouponRequest)e.Request);
					return;
				}
				if (type == typeof(RefreshPSNAuthTokenRequest) && PlayFabEvents._instance.OnRefreshPSNAuthTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnRefreshPSNAuthTokenRequestEvent((RefreshPSNAuthTokenRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterForIOSPushNotificationRequest) && PlayFabEvents._instance.OnRegisterForIOSPushNotificationRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterForIOSPushNotificationRequestEvent((RegisterForIOSPushNotificationRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterPlayFabUserRequest) && PlayFabEvents._instance.OnRegisterPlayFabUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterPlayFabUserRequestEvent((RegisterPlayFabUserRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterWithWindowsHelloRequest) && PlayFabEvents._instance.OnRegisterWithWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnRegisterWithWindowsHelloRequestEvent((RegisterWithWindowsHelloRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveContactEmailRequest) && PlayFabEvents._instance.OnRemoveContactEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveContactEmailRequestEvent((RemoveContactEmailRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveFriendRequest) && PlayFabEvents._instance.OnRemoveFriendRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveFriendRequestEvent((RemoveFriendRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGenericIDRequest) && PlayFabEvents._instance.OnRemoveGenericIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveGenericIDRequestEvent((RemoveGenericIDRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveSharedGroupMembersRequest) && PlayFabEvents._instance.OnRemoveSharedGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnRemoveSharedGroupMembersRequestEvent((RemoveSharedGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(ReportAdActivityRequest) && PlayFabEvents._instance.OnReportAdActivityRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportAdActivityRequestEvent((ReportAdActivityRequest)e.Request);
					return;
				}
				if (type == typeof(DeviceInfoRequest) && PlayFabEvents._instance.OnReportDeviceInfoRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportDeviceInfoRequestEvent((DeviceInfoRequest)e.Request);
					return;
				}
				if (type == typeof(ReportPlayerClientRequest) && PlayFabEvents._instance.OnReportPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnReportPlayerRequestEvent((ReportPlayerClientRequest)e.Request);
					return;
				}
				if (type == typeof(RestoreIOSPurchasesRequest) && PlayFabEvents._instance.OnRestoreIOSPurchasesRequestEvent != null)
				{
					PlayFabEvents._instance.OnRestoreIOSPurchasesRequestEvent((RestoreIOSPurchasesRequest)e.Request);
					return;
				}
				if (type == typeof(RewardAdActivityRequest) && PlayFabEvents._instance.OnRewardAdActivityRequestEvent != null)
				{
					PlayFabEvents._instance.OnRewardAdActivityRequestEvent((RewardAdActivityRequest)e.Request);
					return;
				}
				if (type == typeof(SendAccountRecoveryEmailRequest) && PlayFabEvents._instance.OnSendAccountRecoveryEmailRequestEvent != null)
				{
					PlayFabEvents._instance.OnSendAccountRecoveryEmailRequestEvent((SendAccountRecoveryEmailRequest)e.Request);
					return;
				}
				if (type == typeof(SetFriendTagsRequest) && PlayFabEvents._instance.OnSetFriendTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnSetFriendTagsRequestEvent((SetFriendTagsRequest)e.Request);
					return;
				}
				if (type == typeof(SetPlayerSecretRequest) && PlayFabEvents._instance.OnSetPlayerSecretRequestEvent != null)
				{
					PlayFabEvents._instance.OnSetPlayerSecretRequestEvent((SetPlayerSecretRequest)e.Request);
					return;
				}
				if (type == typeof(StartGameRequest) && PlayFabEvents._instance.OnStartGameRequestEvent != null)
				{
					PlayFabEvents._instance.OnStartGameRequestEvent((StartGameRequest)e.Request);
					return;
				}
				if (type == typeof(StartPurchaseRequest) && PlayFabEvents._instance.OnStartPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnStartPurchaseRequestEvent((StartPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(SubtractUserVirtualCurrencyRequest) && PlayFabEvents._instance.OnSubtractUserVirtualCurrencyRequestEvent != null)
				{
					PlayFabEvents._instance.OnSubtractUserVirtualCurrencyRequestEvent((SubtractUserVirtualCurrencyRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkAndroidDeviceIDRequest) && PlayFabEvents._instance.OnUnlinkAndroidDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAndroidDeviceIDRequestEvent((UnlinkAndroidDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkAppleRequest) && PlayFabEvents._instance.OnUnlinkAppleRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAppleRequestEvent((UnlinkAppleRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkCustomIDRequest) && PlayFabEvents._instance.OnUnlinkCustomIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkCustomIDRequestEvent((UnlinkCustomIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkFacebookAccountRequest) && PlayFabEvents._instance.OnUnlinkFacebookAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookAccountRequestEvent((UnlinkFacebookAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkFacebookInstantGamesIdRequest) && PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdRequestEvent((UnlinkFacebookInstantGamesIdRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkGameCenterAccountRequest) && PlayFabEvents._instance.OnUnlinkGameCenterAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGameCenterAccountRequestEvent((UnlinkGameCenterAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkGoogleAccountRequest) && PlayFabEvents._instance.OnUnlinkGoogleAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGoogleAccountRequestEvent((UnlinkGoogleAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkIOSDeviceIDRequest) && PlayFabEvents._instance.OnUnlinkIOSDeviceIDRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkIOSDeviceIDRequestEvent((UnlinkIOSDeviceIDRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkKongregateAccountRequest) && PlayFabEvents._instance.OnUnlinkKongregateRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkKongregateRequestEvent((UnlinkKongregateAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkNintendoServiceAccountRequest) && PlayFabEvents._instance.OnUnlinkNintendoServiceAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkNintendoServiceAccountRequestEvent((UnlinkNintendoServiceAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkNintendoSwitchDeviceIdRequest) && PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdRequestEvent((UnlinkNintendoSwitchDeviceIdRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkOpenIdConnectRequest) && PlayFabEvents._instance.OnUnlinkOpenIdConnectRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkOpenIdConnectRequestEvent((UnlinkOpenIdConnectRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkPSNAccountRequest) && PlayFabEvents._instance.OnUnlinkPSNAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkPSNAccountRequestEvent((UnlinkPSNAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkSteamAccountRequest) && PlayFabEvents._instance.OnUnlinkSteamAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkSteamAccountRequestEvent((UnlinkSteamAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkTwitchAccountRequest) && PlayFabEvents._instance.OnUnlinkTwitchRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkTwitchRequestEvent((UnlinkTwitchAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkWindowsHelloAccountRequest) && PlayFabEvents._instance.OnUnlinkWindowsHelloRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkWindowsHelloRequestEvent((UnlinkWindowsHelloAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlinkXboxAccountRequest) && PlayFabEvents._instance.OnUnlinkXboxAccountRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkXboxAccountRequestEvent((UnlinkXboxAccountRequest)e.Request);
					return;
				}
				if (type == typeof(UnlockContainerInstanceRequest) && PlayFabEvents._instance.OnUnlockContainerInstanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerInstanceRequestEvent((UnlockContainerInstanceRequest)e.Request);
					return;
				}
				if (type == typeof(UnlockContainerItemRequest) && PlayFabEvents._instance.OnUnlockContainerItemRequestEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerItemRequestEvent((UnlockContainerItemRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateAvatarUrlRequest) && PlayFabEvents._instance.OnUpdateAvatarUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateAvatarUrlRequestEvent((UpdateAvatarUrlRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCharacterDataRequest) && PlayFabEvents._instance.OnUpdateCharacterDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterDataRequestEvent((UpdateCharacterDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateCharacterStatisticsRequest) && PlayFabEvents._instance.OnUpdateCharacterStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterStatisticsRequestEvent((UpdateCharacterStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(UpdatePlayerStatisticsRequest) && PlayFabEvents._instance.OnUpdatePlayerStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdatePlayerStatisticsRequestEvent((UpdatePlayerStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateSharedGroupDataRequest) && PlayFabEvents._instance.OnUpdateSharedGroupDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateSharedGroupDataRequestEvent((UpdateSharedGroupDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserDataRequest) && PlayFabEvents._instance.OnUpdateUserDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserDataRequestEvent((UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserDataRequest) && PlayFabEvents._instance.OnUpdateUserPublisherDataRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserPublisherDataRequestEvent((UpdateUserDataRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateUserTitleDisplayNameRequest) && PlayFabEvents._instance.OnUpdateUserTitleDisplayNameRequestEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserTitleDisplayNameRequestEvent((UpdateUserTitleDisplayNameRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateAmazonReceiptRequest) && PlayFabEvents._instance.OnValidateAmazonIAPReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateAmazonIAPReceiptRequestEvent((ValidateAmazonReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateGooglePlayPurchaseRequest) && PlayFabEvents._instance.OnValidateGooglePlayPurchaseRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateGooglePlayPurchaseRequestEvent((ValidateGooglePlayPurchaseRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateIOSReceiptRequest) && PlayFabEvents._instance.OnValidateIOSReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateIOSReceiptRequestEvent((ValidateIOSReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateWindowsReceiptRequest) && PlayFabEvents._instance.OnValidateWindowsStoreReceiptRequestEvent != null)
				{
					PlayFabEvents._instance.OnValidateWindowsStoreReceiptRequestEvent((ValidateWindowsReceiptRequest)e.Request);
					return;
				}
				if (type == typeof(WriteClientCharacterEventRequest) && PlayFabEvents._instance.OnWriteCharacterEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWriteCharacterEventRequestEvent((WriteClientCharacterEventRequest)e.Request);
					return;
				}
				if (type == typeof(WriteClientPlayerEventRequest) && PlayFabEvents._instance.OnWritePlayerEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWritePlayerEventRequestEvent((WriteClientPlayerEventRequest)e.Request);
					return;
				}
				if (type == typeof(WriteTitleEventRequest) && PlayFabEvents._instance.OnWriteTitleEventRequestEvent != null)
				{
					PlayFabEvents._instance.OnWriteTitleEventRequestEvent((WriteTitleEventRequest)e.Request);
					return;
				}
				if (type == typeof(GetEntityTokenRequest) && PlayFabEvents._instance.OnAuthenticationGetEntityTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnAuthenticationGetEntityTokenRequestEvent((GetEntityTokenRequest)e.Request);
					return;
				}
				if (type == typeof(ValidateEntityTokenRequest) && PlayFabEvents._instance.OnAuthenticationValidateEntityTokenRequestEvent != null)
				{
					PlayFabEvents._instance.OnAuthenticationValidateEntityTokenRequestEvent((ValidateEntityTokenRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteEntityCloudScriptRequest) && PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptRequestEvent((ExecuteEntityCloudScriptRequest)e.Request);
					return;
				}
				if (type == typeof(ExecuteFunctionRequest) && PlayFabEvents._instance.OnCloudScriptExecuteFunctionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptExecuteFunctionRequestEvent((ExecuteFunctionRequest)e.Request);
					return;
				}
				if (type == typeof(ListFunctionsRequest) && PlayFabEvents._instance.OnCloudScriptListFunctionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptListFunctionsRequestEvent((ListFunctionsRequest)e.Request);
					return;
				}
				if (type == typeof(ListFunctionsRequest) && PlayFabEvents._instance.OnCloudScriptListHttpFunctionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptListHttpFunctionsRequestEvent((ListFunctionsRequest)e.Request);
					return;
				}
				if (type == typeof(ListFunctionsRequest) && PlayFabEvents._instance.OnCloudScriptListQueuedFunctionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptListQueuedFunctionsRequestEvent((ListFunctionsRequest)e.Request);
					return;
				}
				if (type == typeof(PostFunctionResultForEntityTriggeredActionRequest) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionRequestEvent((PostFunctionResultForEntityTriggeredActionRequest)e.Request);
					return;
				}
				if (type == typeof(PostFunctionResultForFunctionExecutionRequest) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForFunctionExecutionRequestEvent((PostFunctionResultForFunctionExecutionRequest)e.Request);
					return;
				}
				if (type == typeof(PostFunctionResultForPlayerTriggeredActionRequest) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionRequestEvent((PostFunctionResultForPlayerTriggeredActionRequest)e.Request);
					return;
				}
				if (type == typeof(PostFunctionResultForScheduledTaskRequest) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForScheduledTaskRequestEvent((PostFunctionResultForScheduledTaskRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterHttpFunctionRequest) && PlayFabEvents._instance.OnCloudScriptRegisterHttpFunctionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptRegisterHttpFunctionRequestEvent((RegisterHttpFunctionRequest)e.Request);
					return;
				}
				if (type == typeof(RegisterQueuedFunctionRequest) && PlayFabEvents._instance.OnCloudScriptRegisterQueuedFunctionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptRegisterQueuedFunctionRequestEvent((RegisterQueuedFunctionRequest)e.Request);
					return;
				}
				if (type == typeof(UnregisterFunctionRequest) && PlayFabEvents._instance.OnCloudScriptUnregisterFunctionRequestEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptUnregisterFunctionRequestEvent((UnregisterFunctionRequest)e.Request);
					return;
				}
				if (type == typeof(AbortFileUploadsRequest) && PlayFabEvents._instance.OnDataAbortFileUploadsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataAbortFileUploadsRequestEvent((AbortFileUploadsRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteFilesRequest) && PlayFabEvents._instance.OnDataDeleteFilesRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataDeleteFilesRequestEvent((DeleteFilesRequest)e.Request);
					return;
				}
				if (type == typeof(FinalizeFileUploadsRequest) && PlayFabEvents._instance.OnDataFinalizeFileUploadsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataFinalizeFileUploadsRequestEvent((FinalizeFileUploadsRequest)e.Request);
					return;
				}
				if (type == typeof(GetFilesRequest) && PlayFabEvents._instance.OnDataGetFilesRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataGetFilesRequestEvent((GetFilesRequest)e.Request);
					return;
				}
				if (type == typeof(GetObjectsRequest) && PlayFabEvents._instance.OnDataGetObjectsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataGetObjectsRequestEvent((GetObjectsRequest)e.Request);
					return;
				}
				if (type == typeof(InitiateFileUploadsRequest) && PlayFabEvents._instance.OnDataInitiateFileUploadsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataInitiateFileUploadsRequestEvent((InitiateFileUploadsRequest)e.Request);
					return;
				}
				if (type == typeof(SetObjectsRequest) && PlayFabEvents._instance.OnDataSetObjectsRequestEvent != null)
				{
					PlayFabEvents._instance.OnDataSetObjectsRequestEvent((SetObjectsRequest)e.Request);
					return;
				}
				if (type == typeof(WriteEventsRequest) && PlayFabEvents._instance.OnEventsWriteEventsRequestEvent != null)
				{
					PlayFabEvents._instance.OnEventsWriteEventsRequestEvent((WriteEventsRequest)e.Request);
					return;
				}
				if (type == typeof(WriteEventsRequest) && PlayFabEvents._instance.OnEventsWriteTelemetryEventsRequestEvent != null)
				{
					PlayFabEvents._instance.OnEventsWriteTelemetryEventsRequestEvent((WriteEventsRequest)e.Request);
					return;
				}
				if (type == typeof(CreateExperimentRequest) && PlayFabEvents._instance.OnExperimentationCreateExperimentRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationCreateExperimentRequestEvent((CreateExperimentRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteExperimentRequest) && PlayFabEvents._instance.OnExperimentationDeleteExperimentRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationDeleteExperimentRequestEvent((DeleteExperimentRequest)e.Request);
					return;
				}
				if (type == typeof(GetExperimentsRequest) && PlayFabEvents._instance.OnExperimentationGetExperimentsRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationGetExperimentsRequestEvent((GetExperimentsRequest)e.Request);
					return;
				}
				if (type == typeof(GetLatestScorecardRequest) && PlayFabEvents._instance.OnExperimentationGetLatestScorecardRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationGetLatestScorecardRequestEvent((GetLatestScorecardRequest)e.Request);
					return;
				}
				if (type == typeof(GetTreatmentAssignmentRequest) && PlayFabEvents._instance.OnExperimentationGetTreatmentAssignmentRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationGetTreatmentAssignmentRequestEvent((GetTreatmentAssignmentRequest)e.Request);
					return;
				}
				if (type == typeof(StartExperimentRequest) && PlayFabEvents._instance.OnExperimentationStartExperimentRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationStartExperimentRequestEvent((StartExperimentRequest)e.Request);
					return;
				}
				if (type == typeof(StopExperimentRequest) && PlayFabEvents._instance.OnExperimentationStopExperimentRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationStopExperimentRequestEvent((StopExperimentRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateExperimentRequest) && PlayFabEvents._instance.OnExperimentationUpdateExperimentRequestEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationUpdateExperimentRequestEvent((UpdateExperimentRequest)e.Request);
					return;
				}
				if (type == typeof(InsightsEmptyRequest) && PlayFabEvents._instance.OnInsightsGetDetailsRequestEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetDetailsRequestEvent((InsightsEmptyRequest)e.Request);
					return;
				}
				if (type == typeof(InsightsEmptyRequest) && PlayFabEvents._instance.OnInsightsGetLimitsRequestEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetLimitsRequestEvent((InsightsEmptyRequest)e.Request);
					return;
				}
				if (type == typeof(InsightsGetOperationStatusRequest) && PlayFabEvents._instance.OnInsightsGetOperationStatusRequestEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetOperationStatusRequestEvent((InsightsGetOperationStatusRequest)e.Request);
					return;
				}
				if (type == typeof(InsightsGetPendingOperationsRequest) && PlayFabEvents._instance.OnInsightsGetPendingOperationsRequestEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetPendingOperationsRequestEvent((InsightsGetPendingOperationsRequest)e.Request);
					return;
				}
				if (type == typeof(InsightsSetPerformanceRequest) && PlayFabEvents._instance.OnInsightsSetPerformanceRequestEvent != null)
				{
					PlayFabEvents._instance.OnInsightsSetPerformanceRequestEvent((InsightsSetPerformanceRequest)e.Request);
					return;
				}
				if (type == typeof(InsightsSetStorageRetentionRequest) && PlayFabEvents._instance.OnInsightsSetStorageRetentionRequestEvent != null)
				{
					PlayFabEvents._instance.OnInsightsSetStorageRetentionRequestEvent((InsightsSetStorageRetentionRequest)e.Request);
					return;
				}
				if (type == typeof(AcceptGroupApplicationRequest) && PlayFabEvents._instance.OnGroupsAcceptGroupApplicationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupApplicationRequestEvent((AcceptGroupApplicationRequest)e.Request);
					return;
				}
				if (type == typeof(AcceptGroupInvitationRequest) && PlayFabEvents._instance.OnGroupsAcceptGroupInvitationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupInvitationRequestEvent((AcceptGroupInvitationRequest)e.Request);
					return;
				}
				if (type == typeof(AddMembersRequest) && PlayFabEvents._instance.OnGroupsAddMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAddMembersRequestEvent((AddMembersRequest)e.Request);
					return;
				}
				if (type == typeof(ApplyToGroupRequest) && PlayFabEvents._instance.OnGroupsApplyToGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsApplyToGroupRequestEvent((ApplyToGroupRequest)e.Request);
					return;
				}
				if (type == typeof(BlockEntityRequest) && PlayFabEvents._instance.OnGroupsBlockEntityRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsBlockEntityRequestEvent((BlockEntityRequest)e.Request);
					return;
				}
				if (type == typeof(ChangeMemberRoleRequest) && PlayFabEvents._instance.OnGroupsChangeMemberRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsChangeMemberRoleRequestEvent((ChangeMemberRoleRequest)e.Request);
					return;
				}
				if (type == typeof(CreateGroupRequest) && PlayFabEvents._instance.OnGroupsCreateGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateGroupRequestEvent((CreateGroupRequest)e.Request);
					return;
				}
				if (type == typeof(CreateGroupRoleRequest) && PlayFabEvents._instance.OnGroupsCreateRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateRoleRequestEvent((CreateGroupRoleRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteGroupRequest) && PlayFabEvents._instance.OnGroupsDeleteGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteGroupRequestEvent((DeleteGroupRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteRoleRequest) && PlayFabEvents._instance.OnGroupsDeleteRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteRoleRequestEvent((DeleteRoleRequest)e.Request);
					return;
				}
				if (type == typeof(GetGroupRequest) && PlayFabEvents._instance.OnGroupsGetGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsGetGroupRequestEvent((GetGroupRequest)e.Request);
					return;
				}
				if (type == typeof(InviteToGroupRequest) && PlayFabEvents._instance.OnGroupsInviteToGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsInviteToGroupRequestEvent((InviteToGroupRequest)e.Request);
					return;
				}
				if (type == typeof(IsMemberRequest) && PlayFabEvents._instance.OnGroupsIsMemberRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsIsMemberRequestEvent((IsMemberRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupApplicationsRequest) && PlayFabEvents._instance.OnGroupsListGroupApplicationsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupApplicationsRequestEvent((ListGroupApplicationsRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupBlocksRequest) && PlayFabEvents._instance.OnGroupsListGroupBlocksRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupBlocksRequestEvent((ListGroupBlocksRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupInvitationsRequest) && PlayFabEvents._instance.OnGroupsListGroupInvitationsRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupInvitationsRequestEvent((ListGroupInvitationsRequest)e.Request);
					return;
				}
				if (type == typeof(ListGroupMembersRequest) && PlayFabEvents._instance.OnGroupsListGroupMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupMembersRequestEvent((ListGroupMembersRequest)e.Request);
					return;
				}
				if (type == typeof(ListMembershipRequest) && PlayFabEvents._instance.OnGroupsListMembershipRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipRequestEvent((ListMembershipRequest)e.Request);
					return;
				}
				if (type == typeof(ListMembershipOpportunitiesRequest) && PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesRequestEvent((ListMembershipOpportunitiesRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGroupApplicationRequest) && PlayFabEvents._instance.OnGroupsRemoveGroupApplicationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupApplicationRequestEvent((RemoveGroupApplicationRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveGroupInvitationRequest) && PlayFabEvents._instance.OnGroupsRemoveGroupInvitationRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupInvitationRequestEvent((RemoveGroupInvitationRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveMembersRequest) && PlayFabEvents._instance.OnGroupsRemoveMembersRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveMembersRequestEvent((RemoveMembersRequest)e.Request);
					return;
				}
				if (type == typeof(UnblockEntityRequest) && PlayFabEvents._instance.OnGroupsUnblockEntityRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUnblockEntityRequestEvent((UnblockEntityRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateGroupRequest) && PlayFabEvents._instance.OnGroupsUpdateGroupRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateGroupRequestEvent((UpdateGroupRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateGroupRoleRequest) && PlayFabEvents._instance.OnGroupsUpdateRoleRequestEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateRoleRequestEvent((UpdateGroupRoleRequest)e.Request);
					return;
				}
				if (type == typeof(GetLanguageListRequest) && PlayFabEvents._instance.OnLocalizationGetLanguageListRequestEvent != null)
				{
					PlayFabEvents._instance.OnLocalizationGetLanguageListRequestEvent((GetLanguageListRequest)e.Request);
					return;
				}
				if (type == typeof(CancelAllMatchmakingTicketsForPlayerRequest) && PlayFabEvents._instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerRequestEvent((CancelAllMatchmakingTicketsForPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(CancelAllServerBackfillTicketsForPlayerRequest) && PlayFabEvents._instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerRequestEvent((CancelAllServerBackfillTicketsForPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(CancelMatchmakingTicketRequest) && PlayFabEvents._instance.OnMultiplayerCancelMatchmakingTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelMatchmakingTicketRequestEvent((CancelMatchmakingTicketRequest)e.Request);
					return;
				}
				if (type == typeof(CancelServerBackfillTicketRequest) && PlayFabEvents._instance.OnMultiplayerCancelServerBackfillTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelServerBackfillTicketRequestEvent((CancelServerBackfillTicketRequest)e.Request);
					return;
				}
				if (type == typeof(CreateBuildAliasRequest) && PlayFabEvents._instance.OnMultiplayerCreateBuildAliasRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildAliasRequestEvent((CreateBuildAliasRequest)e.Request);
					return;
				}
				if (type == typeof(CreateBuildWithCustomContainerRequest) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerRequestEvent((CreateBuildWithCustomContainerRequest)e.Request);
					return;
				}
				if (type == typeof(CreateBuildWithManagedContainerRequest) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerRequestEvent((CreateBuildWithManagedContainerRequest)e.Request);
					return;
				}
				if (type == typeof(CreateBuildWithProcessBasedServerRequest) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithProcessBasedServerRequestEvent((CreateBuildWithProcessBasedServerRequest)e.Request);
					return;
				}
				if (type == typeof(CreateMatchmakingTicketRequest) && PlayFabEvents._instance.OnMultiplayerCreateMatchmakingTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateMatchmakingTicketRequestEvent((CreateMatchmakingTicketRequest)e.Request);
					return;
				}
				if (type == typeof(CreateRemoteUserRequest) && PlayFabEvents._instance.OnMultiplayerCreateRemoteUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateRemoteUserRequestEvent((CreateRemoteUserRequest)e.Request);
					return;
				}
				if (type == typeof(CreateServerBackfillTicketRequest) && PlayFabEvents._instance.OnMultiplayerCreateServerBackfillTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateServerBackfillTicketRequestEvent((CreateServerBackfillTicketRequest)e.Request);
					return;
				}
				if (type == typeof(CreateServerMatchmakingTicketRequest) && PlayFabEvents._instance.OnMultiplayerCreateServerMatchmakingTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateServerMatchmakingTicketRequestEvent((CreateServerMatchmakingTicketRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteAssetRequest) && PlayFabEvents._instance.OnMultiplayerDeleteAssetRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteAssetRequestEvent((DeleteAssetRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteBuildRequest) && PlayFabEvents._instance.OnMultiplayerDeleteBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildRequestEvent((DeleteBuildRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteBuildAliasRequest) && PlayFabEvents._instance.OnMultiplayerDeleteBuildAliasRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildAliasRequestEvent((DeleteBuildAliasRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteBuildRegionRequest) && PlayFabEvents._instance.OnMultiplayerDeleteBuildRegionRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildRegionRequestEvent((DeleteBuildRegionRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteCertificateRequest) && PlayFabEvents._instance.OnMultiplayerDeleteCertificateRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteCertificateRequestEvent((DeleteCertificateRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteContainerImageRequest) && PlayFabEvents._instance.OnMultiplayerDeleteContainerImageRepositoryRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteContainerImageRepositoryRequestEvent((DeleteContainerImageRequest)e.Request);
					return;
				}
				if (type == typeof(DeleteRemoteUserRequest) && PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserRequestEvent((DeleteRemoteUserRequest)e.Request);
					return;
				}
				if (type == typeof(EnableMultiplayerServersForTitleRequest) && PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleRequestEvent((EnableMultiplayerServersForTitleRequest)e.Request);
					return;
				}
				if (type == typeof(GetAssetUploadUrlRequest) && PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlRequestEvent((GetAssetUploadUrlRequest)e.Request);
					return;
				}
				if (type == typeof(GetBuildRequest) && PlayFabEvents._instance.OnMultiplayerGetBuildRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetBuildRequestEvent((GetBuildRequest)e.Request);
					return;
				}
				if (type == typeof(GetBuildAliasRequest) && PlayFabEvents._instance.OnMultiplayerGetBuildAliasRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetBuildAliasRequestEvent((GetBuildAliasRequest)e.Request);
					return;
				}
				if (type == typeof(GetContainerRegistryCredentialsRequest) && PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsRequestEvent((GetContainerRegistryCredentialsRequest)e.Request);
					return;
				}
				if (type == typeof(GetMatchRequest) && PlayFabEvents._instance.OnMultiplayerGetMatchRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMatchRequestEvent((GetMatchRequest)e.Request);
					return;
				}
				if (type == typeof(GetMatchmakingQueueRequest) && PlayFabEvents._instance.OnMultiplayerGetMatchmakingQueueRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMatchmakingQueueRequestEvent((GetMatchmakingQueueRequest)e.Request);
					return;
				}
				if (type == typeof(GetMatchmakingTicketRequest) && PlayFabEvents._instance.OnMultiplayerGetMatchmakingTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMatchmakingTicketRequestEvent((GetMatchmakingTicketRequest)e.Request);
					return;
				}
				if (type == typeof(GetMultiplayerServerDetailsRequest) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsRequestEvent((GetMultiplayerServerDetailsRequest)e.Request);
					return;
				}
				if (type == typeof(GetMultiplayerServerLogsRequest) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerLogsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerLogsRequestEvent((GetMultiplayerServerLogsRequest)e.Request);
					return;
				}
				if (type == typeof(GetMultiplayerSessionLogsBySessionIdRequest) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdRequestEvent((GetMultiplayerSessionLogsBySessionIdRequest)e.Request);
					return;
				}
				if (type == typeof(GetQueueStatisticsRequest) && PlayFabEvents._instance.OnMultiplayerGetQueueStatisticsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetQueueStatisticsRequestEvent((GetQueueStatisticsRequest)e.Request);
					return;
				}
				if (type == typeof(GetRemoteLoginEndpointRequest) && PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointRequestEvent((GetRemoteLoginEndpointRequest)e.Request);
					return;
				}
				if (type == typeof(GetServerBackfillTicketRequest) && PlayFabEvents._instance.OnMultiplayerGetServerBackfillTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetServerBackfillTicketRequestEvent((GetServerBackfillTicketRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleEnabledForMultiplayerServersStatusRequest) && PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusRequestEvent((GetTitleEnabledForMultiplayerServersStatusRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitleMultiplayerServersQuotasRequest) && PlayFabEvents._instance.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetTitleMultiplayerServersQuotasRequestEvent((GetTitleMultiplayerServersQuotasRequest)e.Request);
					return;
				}
				if (type == typeof(JoinMatchmakingTicketRequest) && PlayFabEvents._instance.OnMultiplayerJoinMatchmakingTicketRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerJoinMatchmakingTicketRequestEvent((JoinMatchmakingTicketRequest)e.Request);
					return;
				}
				if (type == typeof(ListMultiplayerServersRequest) && PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersRequestEvent((ListMultiplayerServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListAssetSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListAssetSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListAssetSummariesRequestEvent((ListAssetSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(MultiplayerEmptyRequest) && PlayFabEvents._instance.OnMultiplayerListBuildAliasesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListBuildAliasesRequestEvent((MultiplayerEmptyRequest)e.Request);
					return;
				}
				if (type == typeof(ListBuildSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListBuildSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListBuildSummariesRequestEvent((ListBuildSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(ListCertificateSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListCertificateSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListCertificateSummariesRequestEvent((ListCertificateSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(ListContainerImagesRequest) && PlayFabEvents._instance.OnMultiplayerListContainerImagesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImagesRequestEvent((ListContainerImagesRequest)e.Request);
					return;
				}
				if (type == typeof(ListContainerImageTagsRequest) && PlayFabEvents._instance.OnMultiplayerListContainerImageTagsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImageTagsRequestEvent((ListContainerImageTagsRequest)e.Request);
					return;
				}
				if (type == typeof(ListMatchmakingQueuesRequest) && PlayFabEvents._instance.OnMultiplayerListMatchmakingQueuesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMatchmakingQueuesRequestEvent((ListMatchmakingQueuesRequest)e.Request);
					return;
				}
				if (type == typeof(ListMatchmakingTicketsForPlayerRequest) && PlayFabEvents._instance.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMatchmakingTicketsForPlayerRequestEvent((ListMatchmakingTicketsForPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(ListMultiplayerServersRequest) && PlayFabEvents._instance.OnMultiplayerListMultiplayerServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMultiplayerServersRequestEvent((ListMultiplayerServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListPartyQosServersRequest) && PlayFabEvents._instance.OnMultiplayerListPartyQosServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListPartyQosServersRequestEvent((ListPartyQosServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListQosServersRequest) && PlayFabEvents._instance.OnMultiplayerListQosServersRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListQosServersRequestEvent((ListQosServersRequest)e.Request);
					return;
				}
				if (type == typeof(ListQosServersForTitleRequest) && PlayFabEvents._instance.OnMultiplayerListQosServersForTitleRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListQosServersForTitleRequestEvent((ListQosServersForTitleRequest)e.Request);
					return;
				}
				if (type == typeof(ListServerBackfillTicketsForPlayerRequest) && PlayFabEvents._instance.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListServerBackfillTicketsForPlayerRequestEvent((ListServerBackfillTicketsForPlayerRequest)e.Request);
					return;
				}
				if (type == typeof(ListVirtualMachineSummariesRequest) && PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesRequestEvent((ListVirtualMachineSummariesRequest)e.Request);
					return;
				}
				if (type == typeof(RemoveMatchmakingQueueRequest) && PlayFabEvents._instance.OnMultiplayerRemoveMatchmakingQueueRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRemoveMatchmakingQueueRequestEvent((RemoveMatchmakingQueueRequest)e.Request);
					return;
				}
				if (type == typeof(RequestMultiplayerServerRequest) && PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerRequestEvent((RequestMultiplayerServerRequest)e.Request);
					return;
				}
				if (type == typeof(RolloverContainerRegistryCredentialsRequest) && PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsRequestEvent((RolloverContainerRegistryCredentialsRequest)e.Request);
					return;
				}
				if (type == typeof(SetMatchmakingQueueRequest) && PlayFabEvents._instance.OnMultiplayerSetMatchmakingQueueRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerSetMatchmakingQueueRequestEvent((SetMatchmakingQueueRequest)e.Request);
					return;
				}
				if (type == typeof(ShutdownMultiplayerServerRequest) && PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerRequestEvent((ShutdownMultiplayerServerRequest)e.Request);
					return;
				}
				if (type == typeof(UntagContainerImageRequest) && PlayFabEvents._instance.OnMultiplayerUntagContainerImageRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUntagContainerImageRequestEvent((UntagContainerImageRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateBuildAliasRequest) && PlayFabEvents._instance.OnMultiplayerUpdateBuildAliasRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildAliasRequestEvent((UpdateBuildAliasRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateBuildRegionRequest) && PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionRequestEvent((UpdateBuildRegionRequest)e.Request);
					return;
				}
				if (type == typeof(UpdateBuildRegionsRequest) && PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsRequestEvent((UpdateBuildRegionsRequest)e.Request);
					return;
				}
				if (type == typeof(UploadCertificateRequest) && PlayFabEvents._instance.OnMultiplayerUploadCertificateRequestEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUploadCertificateRequestEvent((UploadCertificateRequest)e.Request);
					return;
				}
				if (type == typeof(GetGlobalPolicyRequest) && PlayFabEvents._instance.OnProfilesGetGlobalPolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetGlobalPolicyRequestEvent((GetGlobalPolicyRequest)e.Request);
					return;
				}
				if (type == typeof(GetEntityProfileRequest) && PlayFabEvents._instance.OnProfilesGetProfileRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfileRequestEvent((GetEntityProfileRequest)e.Request);
					return;
				}
				if (type == typeof(GetEntityProfilesRequest) && PlayFabEvents._instance.OnProfilesGetProfilesRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfilesRequestEvent((GetEntityProfilesRequest)e.Request);
					return;
				}
				if (type == typeof(GetTitlePlayersFromMasterPlayerAccountIdsRequest) && PlayFabEvents._instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsRequestEvent((GetTitlePlayersFromMasterPlayerAccountIdsRequest)e.Request);
					return;
				}
				if (type == typeof(SetGlobalPolicyRequest) && PlayFabEvents._instance.OnProfilesSetGlobalPolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetGlobalPolicyRequestEvent((SetGlobalPolicyRequest)e.Request);
					return;
				}
				if (type == typeof(SetProfileLanguageRequest) && PlayFabEvents._instance.OnProfilesSetProfileLanguageRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfileLanguageRequestEvent((SetProfileLanguageRequest)e.Request);
					return;
				}
				if (type == typeof(SetEntityProfilePolicyRequest) && PlayFabEvents._instance.OnProfilesSetProfilePolicyRequestEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfilePolicyRequestEvent((SetEntityProfilePolicyRequest)e.Request);
					return;
				}
			}
			else
			{
				Type type2 = e.Result.GetType();
				if (type2 == typeof(LoginResult) && PlayFabEvents._instance.OnLoginResultEvent != null)
				{
					PlayFabEvents._instance.OnLoginResultEvent((LoginResult)e.Result);
					return;
				}
				if (type2 == typeof(AcceptTradeResponse) && PlayFabEvents._instance.OnAcceptTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnAcceptTradeResultEvent((AcceptTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(AddFriendResult) && PlayFabEvents._instance.OnAddFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnAddFriendResultEvent((AddFriendResult)e.Result);
					return;
				}
				if (type2 == typeof(AddGenericIDResult) && PlayFabEvents._instance.OnAddGenericIDResultEvent != null)
				{
					PlayFabEvents._instance.OnAddGenericIDResultEvent((AddGenericIDResult)e.Result);
					return;
				}
				if (type2 == typeof(AddOrUpdateContactEmailResult) && PlayFabEvents._instance.OnAddOrUpdateContactEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnAddOrUpdateContactEmailResultEvent((AddOrUpdateContactEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(AddSharedGroupMembersResult) && PlayFabEvents._instance.OnAddSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnAddSharedGroupMembersResultEvent((AddSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(AddUsernamePasswordResult) && PlayFabEvents._instance.OnAddUsernamePasswordResultEvent != null)
				{
					PlayFabEvents._instance.OnAddUsernamePasswordResultEvent((AddUsernamePasswordResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnAddUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnAddUserVirtualCurrencyResultEvent((ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(AndroidDevicePushNotificationRegistrationResult) && PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationResultEvent != null)
				{
					PlayFabEvents._instance.OnAndroidDevicePushNotificationRegistrationResultEvent((AndroidDevicePushNotificationRegistrationResult)e.Result);
					return;
				}
				if (type2 == typeof(AttributeInstallResult) && PlayFabEvents._instance.OnAttributeInstallResultEvent != null)
				{
					PlayFabEvents._instance.OnAttributeInstallResultEvent((AttributeInstallResult)e.Result);
					return;
				}
				if (type2 == typeof(CancelTradeResponse) && PlayFabEvents._instance.OnCancelTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnCancelTradeResultEvent((CancelTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(ConfirmPurchaseResult) && PlayFabEvents._instance.OnConfirmPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnConfirmPurchaseResultEvent((ConfirmPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ConsumeItemResult) && PlayFabEvents._instance.OnConsumeItemResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumeItemResultEvent((ConsumeItemResult)e.Result);
					return;
				}
				if (type2 == typeof(ConsumePSNEntitlementsResult) && PlayFabEvents._instance.OnConsumePSNEntitlementsResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumePSNEntitlementsResultEvent((ConsumePSNEntitlementsResult)e.Result);
					return;
				}
				if (type2 == typeof(ConsumeXboxEntitlementsResult) && PlayFabEvents._instance.OnConsumeXboxEntitlementsResultEvent != null)
				{
					PlayFabEvents._instance.OnConsumeXboxEntitlementsResultEvent((ConsumeXboxEntitlementsResult)e.Result);
					return;
				}
				if (type2 == typeof(CreateSharedGroupResult) && PlayFabEvents._instance.OnCreateSharedGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnCreateSharedGroupResultEvent((CreateSharedGroupResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.ExecuteCloudScriptResult) && PlayFabEvents._instance.OnExecuteCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnExecuteCloudScriptResultEvent((PlayFab.ClientModels.ExecuteCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(GetAccountInfoResult) && PlayFabEvents._instance.OnGetAccountInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAccountInfoResultEvent((GetAccountInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(GetAdPlacementsResult) && PlayFabEvents._instance.OnGetAdPlacementsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAdPlacementsResultEvent((GetAdPlacementsResult)e.Result);
					return;
				}
				if (type2 == typeof(ListUsersCharactersResult) && PlayFabEvents._instance.OnGetAllUsersCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnGetAllUsersCharactersResultEvent((ListUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCatalogItemsResult) && PlayFabEvents._instance.OnGetCatalogItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCatalogItemsResultEvent((GetCatalogItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterDataResult) && PlayFabEvents._instance.OnGetCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterDataResultEvent((GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterInventoryResult) && PlayFabEvents._instance.OnGetCharacterInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterInventoryResultEvent((GetCharacterInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterLeaderboardResult) && PlayFabEvents._instance.OnGetCharacterLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterLeaderboardResultEvent((GetCharacterLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterDataResult) && PlayFabEvents._instance.OnGetCharacterReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterReadOnlyDataResultEvent((GetCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetCharacterStatisticsResult) && PlayFabEvents._instance.OnGetCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCharacterStatisticsResultEvent((GetCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetContentDownloadUrlResult) && PlayFabEvents._instance.OnGetContentDownloadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnGetContentDownloadUrlResultEvent((GetContentDownloadUrlResult)e.Result);
					return;
				}
				if (type2 == typeof(CurrentGamesResult) && PlayFabEvents._instance.OnGetCurrentGamesResultEvent != null)
				{
					PlayFabEvents._instance.OnGetCurrentGamesResultEvent((CurrentGamesResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardResult) && PlayFabEvents._instance.OnGetFriendLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardResultEvent((GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetFriendLeaderboardAroundPlayerResult) && PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendLeaderboardAroundPlayerResultEvent((GetFriendLeaderboardAroundPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(GetFriendsListResult) && PlayFabEvents._instance.OnGetFriendsListResultEvent != null)
				{
					PlayFabEvents._instance.OnGetFriendsListResultEvent((GetFriendsListResult)e.Result);
					return;
				}
				if (type2 == typeof(GameServerRegionsResult) && PlayFabEvents._instance.OnGetGameServerRegionsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetGameServerRegionsResultEvent((GameServerRegionsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardResult) && PlayFabEvents._instance.OnGetLeaderboardResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardResultEvent((GetLeaderboardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardAroundCharacterResult) && PlayFabEvents._instance.OnGetLeaderboardAroundCharacterResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundCharacterResultEvent((GetLeaderboardAroundCharacterResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardAroundPlayerResult) && PlayFabEvents._instance.OnGetLeaderboardAroundPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardAroundPlayerResultEvent((GetLeaderboardAroundPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLeaderboardForUsersCharactersResult) && PlayFabEvents._instance.OnGetLeaderboardForUserCharactersResultEvent != null)
				{
					PlayFabEvents._instance.OnGetLeaderboardForUserCharactersResultEvent((GetLeaderboardForUsersCharactersResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPaymentTokenResult) && PlayFabEvents._instance.OnGetPaymentTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPaymentTokenResultEvent((GetPaymentTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPhotonAuthenticationTokenResult) && PlayFabEvents._instance.OnGetPhotonAuthenticationTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPhotonAuthenticationTokenResultEvent((GetPhotonAuthenticationTokenResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerCombinedInfoResult) && PlayFabEvents._instance.OnGetPlayerCombinedInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerCombinedInfoResultEvent((GetPlayerCombinedInfoResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerProfileResult) && PlayFabEvents._instance.OnGetPlayerProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerProfileResultEvent((GetPlayerProfileResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerSegmentsResult) && PlayFabEvents._instance.OnGetPlayerSegmentsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerSegmentsResultEvent((GetPlayerSegmentsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerStatisticsResult) && PlayFabEvents._instance.OnGetPlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticsResultEvent((GetPlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerStatisticVersionsResult) && PlayFabEvents._instance.OnGetPlayerStatisticVersionsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerStatisticVersionsResultEvent((GetPlayerStatisticVersionsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerTagsResult) && PlayFabEvents._instance.OnGetPlayerTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTagsResultEvent((GetPlayerTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayerTradesResponse) && PlayFabEvents._instance.OnGetPlayerTradesResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayerTradesResultEvent((GetPlayerTradesResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromFacebookIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookIDsResultEvent((GetPlayFabIDsFromFacebookIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromFacebookInstantGamesIdsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromFacebookInstantGamesIdsResultEvent((GetPlayFabIDsFromFacebookInstantGamesIdsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGameCenterIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGameCenterIDsResultEvent((GetPlayFabIDsFromGameCenterIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGenericIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGenericIDsResultEvent((GetPlayFabIDsFromGenericIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromGoogleIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromGoogleIDsResultEvent((GetPlayFabIDsFromGoogleIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromKongregateIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromKongregateIDsResultEvent((GetPlayFabIDsFromKongregateIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromNintendoSwitchDeviceIdsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromNintendoSwitchDeviceIdsResultEvent((GetPlayFabIDsFromNintendoSwitchDeviceIdsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromPSNAccountIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromPSNAccountIDsResultEvent((GetPlayFabIDsFromPSNAccountIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromSteamIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromSteamIDsResultEvent((GetPlayFabIDsFromSteamIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromTwitchIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromTwitchIDsResultEvent((GetPlayFabIDsFromTwitchIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPlayFabIDsFromXboxLiveIDsResult) && PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPlayFabIDsFromXboxLiveIDsResultEvent((GetPlayFabIDsFromXboxLiveIDsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPublisherDataResult) && PlayFabEvents._instance.OnGetPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPublisherDataResultEvent((GetPublisherDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetPurchaseResult) && PlayFabEvents._instance.OnGetPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnGetPurchaseResultEvent((GetPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(GetSharedGroupDataResult) && PlayFabEvents._instance.OnGetSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetSharedGroupDataResultEvent((GetSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetStoreItemsResult) && PlayFabEvents._instance.OnGetStoreItemsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetStoreItemsResultEvent((GetStoreItemsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTimeResult) && PlayFabEvents._instance.OnGetTimeResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTimeResultEvent((GetTimeResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleDataResult) && PlayFabEvents._instance.OnGetTitleDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleDataResultEvent((GetTitleDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleNewsResult) && PlayFabEvents._instance.OnGetTitleNewsResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitleNewsResultEvent((GetTitleNewsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitlePublicKeyResult) && PlayFabEvents._instance.OnGetTitlePublicKeyResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTitlePublicKeyResultEvent((GetTitlePublicKeyResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTradeStatusResponse) && PlayFabEvents._instance.OnGetTradeStatusResultEvent != null)
				{
					PlayFabEvents._instance.OnGetTradeStatusResultEvent((GetTradeStatusResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserInventoryResult) && PlayFabEvents._instance.OnGetUserInventoryResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserInventoryResultEvent((GetUserInventoryResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserPublisherReadOnlyDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetUserDataResult) && PlayFabEvents._instance.OnGetUserReadOnlyDataResultEvent != null)
				{
					PlayFabEvents._instance.OnGetUserReadOnlyDataResultEvent((GetUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(GetWindowsHelloChallengeResponse) && PlayFabEvents._instance.OnGetWindowsHelloChallengeResultEvent != null)
				{
					PlayFabEvents._instance.OnGetWindowsHelloChallengeResultEvent((GetWindowsHelloChallengeResponse)e.Result);
					return;
				}
				if (type2 == typeof(GrantCharacterToUserResult) && PlayFabEvents._instance.OnGrantCharacterToUserResultEvent != null)
				{
					PlayFabEvents._instance.OnGrantCharacterToUserResultEvent((GrantCharacterToUserResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkAndroidDeviceIDResult) && PlayFabEvents._instance.OnLinkAndroidDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkAndroidDeviceIDResultEvent((LinkAndroidDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && PlayFabEvents._instance.OnLinkAppleResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkAppleResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkCustomIDResult) && PlayFabEvents._instance.OnLinkCustomIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkCustomIDResultEvent((LinkCustomIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkFacebookAccountResult) && PlayFabEvents._instance.OnLinkFacebookAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookAccountResultEvent((LinkFacebookAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkFacebookInstantGamesIdResult) && PlayFabEvents._instance.OnLinkFacebookInstantGamesIdResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkFacebookInstantGamesIdResultEvent((LinkFacebookInstantGamesIdResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkGameCenterAccountResult) && PlayFabEvents._instance.OnLinkGameCenterAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkGameCenterAccountResultEvent((LinkGameCenterAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkGoogleAccountResult) && PlayFabEvents._instance.OnLinkGoogleAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkGoogleAccountResultEvent((LinkGoogleAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkIOSDeviceIDResult) && PlayFabEvents._instance.OnLinkIOSDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkIOSDeviceIDResultEvent((LinkIOSDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkKongregateAccountResult) && PlayFabEvents._instance.OnLinkKongregateResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkKongregateResultEvent((LinkKongregateAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && PlayFabEvents._instance.OnLinkNintendoServiceAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkNintendoServiceAccountResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkNintendoSwitchDeviceIdResult) && PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkNintendoSwitchDeviceIdResultEvent((LinkNintendoSwitchDeviceIdResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResult) && PlayFabEvents._instance.OnLinkOpenIdConnectResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkOpenIdConnectResultEvent((PlayFab.ClientModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkPSNAccountResult) && PlayFabEvents._instance.OnLinkPSNAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkPSNAccountResultEvent((LinkPSNAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkSteamAccountResult) && PlayFabEvents._instance.OnLinkSteamAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkSteamAccountResultEvent((LinkSteamAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkTwitchAccountResult) && PlayFabEvents._instance.OnLinkTwitchResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkTwitchResultEvent((LinkTwitchAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(LinkWindowsHelloAccountResponse) && PlayFabEvents._instance.OnLinkWindowsHelloResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkWindowsHelloResultEvent((LinkWindowsHelloAccountResponse)e.Result);
					return;
				}
				if (type2 == typeof(LinkXboxAccountResult) && PlayFabEvents._instance.OnLinkXboxAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnLinkXboxAccountResultEvent((LinkXboxAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(MatchmakeResult) && PlayFabEvents._instance.OnMatchmakeResultEvent != null)
				{
					PlayFabEvents._instance.OnMatchmakeResultEvent((MatchmakeResult)e.Result);
					return;
				}
				if (type2 == typeof(OpenTradeResponse) && PlayFabEvents._instance.OnOpenTradeResultEvent != null)
				{
					PlayFabEvents._instance.OnOpenTradeResultEvent((OpenTradeResponse)e.Result);
					return;
				}
				if (type2 == typeof(PayForPurchaseResult) && PlayFabEvents._instance.OnPayForPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnPayForPurchaseResultEvent((PayForPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(PurchaseItemResult) && PlayFabEvents._instance.OnPurchaseItemResultEvent != null)
				{
					PlayFabEvents._instance.OnPurchaseItemResultEvent((PurchaseItemResult)e.Result);
					return;
				}
				if (type2 == typeof(RedeemCouponResult) && PlayFabEvents._instance.OnRedeemCouponResultEvent != null)
				{
					PlayFabEvents._instance.OnRedeemCouponResultEvent((RedeemCouponResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnRefreshPSNAuthTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnRefreshPSNAuthTokenResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(RegisterForIOSPushNotificationResult) && PlayFabEvents._instance.OnRegisterForIOSPushNotificationResultEvent != null)
				{
					PlayFabEvents._instance.OnRegisterForIOSPushNotificationResultEvent((RegisterForIOSPushNotificationResult)e.Result);
					return;
				}
				if (type2 == typeof(RegisterPlayFabUserResult) && PlayFabEvents._instance.OnRegisterPlayFabUserResultEvent != null)
				{
					PlayFabEvents._instance.OnRegisterPlayFabUserResultEvent((RegisterPlayFabUserResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveContactEmailResult) && PlayFabEvents._instance.OnRemoveContactEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveContactEmailResultEvent((RemoveContactEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveFriendResult) && PlayFabEvents._instance.OnRemoveFriendResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveFriendResultEvent((RemoveFriendResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveGenericIDResult) && PlayFabEvents._instance.OnRemoveGenericIDResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveGenericIDResultEvent((RemoveGenericIDResult)e.Result);
					return;
				}
				if (type2 == typeof(RemoveSharedGroupMembersResult) && PlayFabEvents._instance.OnRemoveSharedGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnRemoveSharedGroupMembersResultEvent((RemoveSharedGroupMembersResult)e.Result);
					return;
				}
				if (type2 == typeof(ReportAdActivityResult) && PlayFabEvents._instance.OnReportAdActivityResultEvent != null)
				{
					PlayFabEvents._instance.OnReportAdActivityResultEvent((ReportAdActivityResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnReportDeviceInfoResultEvent != null)
				{
					PlayFabEvents._instance.OnReportDeviceInfoResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(ReportPlayerClientResult) && PlayFabEvents._instance.OnReportPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnReportPlayerResultEvent((ReportPlayerClientResult)e.Result);
					return;
				}
				if (type2 == typeof(RestoreIOSPurchasesResult) && PlayFabEvents._instance.OnRestoreIOSPurchasesResultEvent != null)
				{
					PlayFabEvents._instance.OnRestoreIOSPurchasesResultEvent((RestoreIOSPurchasesResult)e.Result);
					return;
				}
				if (type2 == typeof(RewardAdActivityResult) && PlayFabEvents._instance.OnRewardAdActivityResultEvent != null)
				{
					PlayFabEvents._instance.OnRewardAdActivityResultEvent((RewardAdActivityResult)e.Result);
					return;
				}
				if (type2 == typeof(SendAccountRecoveryEmailResult) && PlayFabEvents._instance.OnSendAccountRecoveryEmailResultEvent != null)
				{
					PlayFabEvents._instance.OnSendAccountRecoveryEmailResultEvent((SendAccountRecoveryEmailResult)e.Result);
					return;
				}
				if (type2 == typeof(SetFriendTagsResult) && PlayFabEvents._instance.OnSetFriendTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnSetFriendTagsResultEvent((SetFriendTagsResult)e.Result);
					return;
				}
				if (type2 == typeof(SetPlayerSecretResult) && PlayFabEvents._instance.OnSetPlayerSecretResultEvent != null)
				{
					PlayFabEvents._instance.OnSetPlayerSecretResultEvent((SetPlayerSecretResult)e.Result);
					return;
				}
				if (type2 == typeof(StartGameResult) && PlayFabEvents._instance.OnStartGameResultEvent != null)
				{
					PlayFabEvents._instance.OnStartGameResultEvent((StartGameResult)e.Result);
					return;
				}
				if (type2 == typeof(StartPurchaseResult) && PlayFabEvents._instance.OnStartPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnStartPurchaseResultEvent((StartPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ModifyUserVirtualCurrencyResult) && PlayFabEvents._instance.OnSubtractUserVirtualCurrencyResultEvent != null)
				{
					PlayFabEvents._instance.OnSubtractUserVirtualCurrencyResultEvent((ModifyUserVirtualCurrencyResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkAndroidDeviceIDResult) && PlayFabEvents._instance.OnUnlinkAndroidDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAndroidDeviceIDResultEvent((UnlinkAndroidDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnUnlinkAppleResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkAppleResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkCustomIDResult) && PlayFabEvents._instance.OnUnlinkCustomIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkCustomIDResultEvent((UnlinkCustomIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkFacebookAccountResult) && PlayFabEvents._instance.OnUnlinkFacebookAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookAccountResultEvent((UnlinkFacebookAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkFacebookInstantGamesIdResult) && PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkFacebookInstantGamesIdResultEvent((UnlinkFacebookInstantGamesIdResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkGameCenterAccountResult) && PlayFabEvents._instance.OnUnlinkGameCenterAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGameCenterAccountResultEvent((UnlinkGameCenterAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkGoogleAccountResult) && PlayFabEvents._instance.OnUnlinkGoogleAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkGoogleAccountResultEvent((UnlinkGoogleAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkIOSDeviceIDResult) && PlayFabEvents._instance.OnUnlinkIOSDeviceIDResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkIOSDeviceIDResultEvent((UnlinkIOSDeviceIDResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkKongregateAccountResult) && PlayFabEvents._instance.OnUnlinkKongregateResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkKongregateResultEvent((UnlinkKongregateAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnUnlinkNintendoServiceAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkNintendoServiceAccountResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkNintendoSwitchDeviceIdResult) && PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkNintendoSwitchDeviceIdResultEvent((UnlinkNintendoSwitchDeviceIdResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnUnlinkOpenIdConnectResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkOpenIdConnectResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkPSNAccountResult) && PlayFabEvents._instance.OnUnlinkPSNAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkPSNAccountResultEvent((UnlinkPSNAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkSteamAccountResult) && PlayFabEvents._instance.OnUnlinkSteamAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkSteamAccountResultEvent((UnlinkSteamAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkTwitchAccountResult) && PlayFabEvents._instance.OnUnlinkTwitchResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkTwitchResultEvent((UnlinkTwitchAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkWindowsHelloAccountResponse) && PlayFabEvents._instance.OnUnlinkWindowsHelloResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkWindowsHelloResultEvent((UnlinkWindowsHelloAccountResponse)e.Result);
					return;
				}
				if (type2 == typeof(UnlinkXboxAccountResult) && PlayFabEvents._instance.OnUnlinkXboxAccountResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlinkXboxAccountResultEvent((UnlinkXboxAccountResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlockContainerItemResult) && PlayFabEvents._instance.OnUnlockContainerInstanceResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerInstanceResultEvent((UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(UnlockContainerItemResult) && PlayFabEvents._instance.OnUnlockContainerItemResultEvent != null)
				{
					PlayFabEvents._instance.OnUnlockContainerItemResultEvent((UnlockContainerItemResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ClientModels.EmptyResponse) && PlayFabEvents._instance.OnUpdateAvatarUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateAvatarUrlResultEvent((PlayFab.ClientModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCharacterDataResult) && PlayFabEvents._instance.OnUpdateCharacterDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterDataResultEvent((UpdateCharacterDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateCharacterStatisticsResult) && PlayFabEvents._instance.OnUpdateCharacterStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateCharacterStatisticsResultEvent((UpdateCharacterStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdatePlayerStatisticsResult) && PlayFabEvents._instance.OnUpdatePlayerStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdatePlayerStatisticsResultEvent((UpdatePlayerStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateSharedGroupDataResult) && PlayFabEvents._instance.OnUpdateSharedGroupDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateSharedGroupDataResultEvent((UpdateSharedGroupDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateUserDataResult) && PlayFabEvents._instance.OnUpdateUserDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserDataResultEvent((UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateUserDataResult) && PlayFabEvents._instance.OnUpdateUserPublisherDataResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserPublisherDataResultEvent((UpdateUserDataResult)e.Result);
					return;
				}
				if (type2 == typeof(UpdateUserTitleDisplayNameResult) && PlayFabEvents._instance.OnUpdateUserTitleDisplayNameResultEvent != null)
				{
					PlayFabEvents._instance.OnUpdateUserTitleDisplayNameResultEvent((UpdateUserTitleDisplayNameResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateAmazonReceiptResult) && PlayFabEvents._instance.OnValidateAmazonIAPReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateAmazonIAPReceiptResultEvent((ValidateAmazonReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateGooglePlayPurchaseResult) && PlayFabEvents._instance.OnValidateGooglePlayPurchaseResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateGooglePlayPurchaseResultEvent((ValidateGooglePlayPurchaseResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateIOSReceiptResult) && PlayFabEvents._instance.OnValidateIOSReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateIOSReceiptResultEvent((ValidateIOSReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(ValidateWindowsReceiptResult) && PlayFabEvents._instance.OnValidateWindowsStoreReceiptResultEvent != null)
				{
					PlayFabEvents._instance.OnValidateWindowsStoreReceiptResultEvent((ValidateWindowsReceiptResult)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventResponse) && PlayFabEvents._instance.OnWriteCharacterEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWriteCharacterEventResultEvent((WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventResponse) && PlayFabEvents._instance.OnWritePlayerEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWritePlayerEventResultEvent((WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventResponse) && PlayFabEvents._instance.OnWriteTitleEventResultEvent != null)
				{
					PlayFabEvents._instance.OnWriteTitleEventResultEvent((WriteEventResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetEntityTokenResponse) && PlayFabEvents._instance.OnAuthenticationGetEntityTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnAuthenticationGetEntityTokenResultEvent((GetEntityTokenResponse)e.Result);
					return;
				}
				if (type2 == typeof(ValidateEntityTokenResponse) && PlayFabEvents._instance.OnAuthenticationValidateEntityTokenResultEvent != null)
				{
					PlayFabEvents._instance.OnAuthenticationValidateEntityTokenResultEvent((ValidateEntityTokenResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.ExecuteCloudScriptResult) && PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptExecuteEntityCloudScriptResultEvent((PlayFab.CloudScriptModels.ExecuteCloudScriptResult)e.Result);
					return;
				}
				if (type2 == typeof(ExecuteFunctionResult) && PlayFabEvents._instance.OnCloudScriptExecuteFunctionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptExecuteFunctionResultEvent((ExecuteFunctionResult)e.Result);
					return;
				}
				if (type2 == typeof(ListFunctionsResult) && PlayFabEvents._instance.OnCloudScriptListFunctionsResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptListFunctionsResultEvent((ListFunctionsResult)e.Result);
					return;
				}
				if (type2 == typeof(ListHttpFunctionsResult) && PlayFabEvents._instance.OnCloudScriptListHttpFunctionsResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptListHttpFunctionsResultEvent((ListHttpFunctionsResult)e.Result);
					return;
				}
				if (type2 == typeof(ListQueuedFunctionsResult) && PlayFabEvents._instance.OnCloudScriptListQueuedFunctionsResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptListQueuedFunctionsResultEvent((ListQueuedFunctionsResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForEntityTriggeredActionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForFunctionExecutionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForPlayerTriggeredActionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptPostFunctionResultForScheduledTaskResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptRegisterHttpFunctionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptRegisterHttpFunctionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptRegisterQueuedFunctionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptRegisterQueuedFunctionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.CloudScriptModels.EmptyResult) && PlayFabEvents._instance.OnCloudScriptUnregisterFunctionResultEvent != null)
				{
					PlayFabEvents._instance.OnCloudScriptUnregisterFunctionResultEvent((PlayFab.CloudScriptModels.EmptyResult)e.Result);
					return;
				}
				if (type2 == typeof(AbortFileUploadsResponse) && PlayFabEvents._instance.OnDataAbortFileUploadsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataAbortFileUploadsResultEvent((AbortFileUploadsResponse)e.Result);
					return;
				}
				if (type2 == typeof(DeleteFilesResponse) && PlayFabEvents._instance.OnDataDeleteFilesResultEvent != null)
				{
					PlayFabEvents._instance.OnDataDeleteFilesResultEvent((DeleteFilesResponse)e.Result);
					return;
				}
				if (type2 == typeof(FinalizeFileUploadsResponse) && PlayFabEvents._instance.OnDataFinalizeFileUploadsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataFinalizeFileUploadsResultEvent((FinalizeFileUploadsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetFilesResponse) && PlayFabEvents._instance.OnDataGetFilesResultEvent != null)
				{
					PlayFabEvents._instance.OnDataGetFilesResultEvent((GetFilesResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetObjectsResponse) && PlayFabEvents._instance.OnDataGetObjectsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataGetObjectsResultEvent((GetObjectsResponse)e.Result);
					return;
				}
				if (type2 == typeof(InitiateFileUploadsResponse) && PlayFabEvents._instance.OnDataInitiateFileUploadsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataInitiateFileUploadsResultEvent((InitiateFileUploadsResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetObjectsResponse) && PlayFabEvents._instance.OnDataSetObjectsResultEvent != null)
				{
					PlayFabEvents._instance.OnDataSetObjectsResultEvent((SetObjectsResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventsResponse) && PlayFabEvents._instance.OnEventsWriteEventsResultEvent != null)
				{
					PlayFabEvents._instance.OnEventsWriteEventsResultEvent((WriteEventsResponse)e.Result);
					return;
				}
				if (type2 == typeof(WriteEventsResponse) && PlayFabEvents._instance.OnEventsWriteTelemetryEventsResultEvent != null)
				{
					PlayFabEvents._instance.OnEventsWriteTelemetryEventsResultEvent((WriteEventsResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateExperimentResult) && PlayFabEvents._instance.OnExperimentationCreateExperimentResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationCreateExperimentResultEvent((CreateExperimentResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && PlayFabEvents._instance.OnExperimentationDeleteExperimentResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationDeleteExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetExperimentsResult) && PlayFabEvents._instance.OnExperimentationGetExperimentsResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationGetExperimentsResultEvent((GetExperimentsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetLatestScorecardResult) && PlayFabEvents._instance.OnExperimentationGetLatestScorecardResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationGetLatestScorecardResultEvent((GetLatestScorecardResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTreatmentAssignmentResult) && PlayFabEvents._instance.OnExperimentationGetTreatmentAssignmentResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationGetTreatmentAssignmentResultEvent((GetTreatmentAssignmentResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && PlayFabEvents._instance.OnExperimentationStartExperimentResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationStartExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && PlayFabEvents._instance.OnExperimentationStopExperimentResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationStopExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.ExperimentationModels.EmptyResponse) && PlayFabEvents._instance.OnExperimentationUpdateExperimentResultEvent != null)
				{
					PlayFabEvents._instance.OnExperimentationUpdateExperimentResultEvent((PlayFab.ExperimentationModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(InsightsGetDetailsResponse) && PlayFabEvents._instance.OnInsightsGetDetailsResultEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetDetailsResultEvent((InsightsGetDetailsResponse)e.Result);
					return;
				}
				if (type2 == typeof(InsightsGetLimitsResponse) && PlayFabEvents._instance.OnInsightsGetLimitsResultEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetLimitsResultEvent((InsightsGetLimitsResponse)e.Result);
					return;
				}
				if (type2 == typeof(InsightsGetOperationStatusResponse) && PlayFabEvents._instance.OnInsightsGetOperationStatusResultEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetOperationStatusResultEvent((InsightsGetOperationStatusResponse)e.Result);
					return;
				}
				if (type2 == typeof(InsightsGetPendingOperationsResponse) && PlayFabEvents._instance.OnInsightsGetPendingOperationsResultEvent != null)
				{
					PlayFabEvents._instance.OnInsightsGetPendingOperationsResultEvent((InsightsGetPendingOperationsResponse)e.Result);
					return;
				}
				if (type2 == typeof(InsightsOperationResponse) && PlayFabEvents._instance.OnInsightsSetPerformanceResultEvent != null)
				{
					PlayFabEvents._instance.OnInsightsSetPerformanceResultEvent((InsightsOperationResponse)e.Result);
					return;
				}
				if (type2 == typeof(InsightsOperationResponse) && PlayFabEvents._instance.OnInsightsSetStorageRetentionResultEvent != null)
				{
					PlayFabEvents._instance.OnInsightsSetStorageRetentionResultEvent((InsightsOperationResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsAcceptGroupApplicationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupApplicationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsAcceptGroupInvitationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAcceptGroupInvitationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsAddMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsAddMembersResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(ApplyToGroupResponse) && PlayFabEvents._instance.OnGroupsApplyToGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsApplyToGroupResultEvent((ApplyToGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsBlockEntityResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsBlockEntityResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsChangeMemberRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsChangeMemberRoleResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateGroupResponse) && PlayFabEvents._instance.OnGroupsCreateGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateGroupResultEvent((CreateGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateGroupRoleResponse) && PlayFabEvents._instance.OnGroupsCreateRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsCreateRoleResultEvent((CreateGroupRoleResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsDeleteGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteGroupResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsDeleteRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsDeleteRoleResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetGroupResponse) && PlayFabEvents._instance.OnGroupsGetGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsGetGroupResultEvent((GetGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(InviteToGroupResponse) && PlayFabEvents._instance.OnGroupsInviteToGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsInviteToGroupResultEvent((InviteToGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(IsMemberResponse) && PlayFabEvents._instance.OnGroupsIsMemberResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsIsMemberResultEvent((IsMemberResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupApplicationsResponse) && PlayFabEvents._instance.OnGroupsListGroupApplicationsResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupApplicationsResultEvent((ListGroupApplicationsResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupBlocksResponse) && PlayFabEvents._instance.OnGroupsListGroupBlocksResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupBlocksResultEvent((ListGroupBlocksResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupInvitationsResponse) && PlayFabEvents._instance.OnGroupsListGroupInvitationsResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupInvitationsResultEvent((ListGroupInvitationsResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListGroupMembersResponse) && PlayFabEvents._instance.OnGroupsListGroupMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListGroupMembersResultEvent((ListGroupMembersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMembershipResponse) && PlayFabEvents._instance.OnGroupsListMembershipResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipResultEvent((ListMembershipResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMembershipOpportunitiesResponse) && PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsListMembershipOpportunitiesResultEvent((ListMembershipOpportunitiesResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsRemoveGroupApplicationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupApplicationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsRemoveGroupInvitationResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveGroupInvitationResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsRemoveMembersResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsRemoveMembersResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.GroupsModels.EmptyResponse) && PlayFabEvents._instance.OnGroupsUnblockEntityResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUnblockEntityResultEvent((PlayFab.GroupsModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateGroupResponse) && PlayFabEvents._instance.OnGroupsUpdateGroupResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateGroupResultEvent((UpdateGroupResponse)e.Result);
					return;
				}
				if (type2 == typeof(UpdateGroupRoleResponse) && PlayFabEvents._instance.OnGroupsUpdateRoleResultEvent != null)
				{
					PlayFabEvents._instance.OnGroupsUpdateRoleResultEvent((UpdateGroupRoleResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetLanguageListResponse) && PlayFabEvents._instance.OnLocalizationGetLanguageListResultEvent != null)
				{
					PlayFabEvents._instance.OnLocalizationGetLanguageListResultEvent((GetLanguageListResponse)e.Result);
					return;
				}
				if (type2 == typeof(CancelAllMatchmakingTicketsForPlayerResult) && PlayFabEvents._instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelAllMatchmakingTicketsForPlayerResultEvent((CancelAllMatchmakingTicketsForPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(CancelAllServerBackfillTicketsForPlayerResult) && PlayFabEvents._instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelAllServerBackfillTicketsForPlayerResultEvent((CancelAllServerBackfillTicketsForPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(CancelMatchmakingTicketResult) && PlayFabEvents._instance.OnMultiplayerCancelMatchmakingTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelMatchmakingTicketResultEvent((CancelMatchmakingTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(CancelServerBackfillTicketResult) && PlayFabEvents._instance.OnMultiplayerCancelServerBackfillTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCancelServerBackfillTicketResultEvent((CancelServerBackfillTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(BuildAliasDetailsResponse) && PlayFabEvents._instance.OnMultiplayerCreateBuildAliasResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildAliasResultEvent((BuildAliasDetailsResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateBuildWithCustomContainerResponse) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithCustomContainerResultEvent((CreateBuildWithCustomContainerResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateBuildWithManagedContainerResponse) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithManagedContainerResultEvent((CreateBuildWithManagedContainerResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateBuildWithProcessBasedServerResponse) && PlayFabEvents._instance.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateBuildWithProcessBasedServerResultEvent((CreateBuildWithProcessBasedServerResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateMatchmakingTicketResult) && PlayFabEvents._instance.OnMultiplayerCreateMatchmakingTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateMatchmakingTicketResultEvent((CreateMatchmakingTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(CreateRemoteUserResponse) && PlayFabEvents._instance.OnMultiplayerCreateRemoteUserResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateRemoteUserResultEvent((CreateRemoteUserResponse)e.Result);
					return;
				}
				if (type2 == typeof(CreateServerBackfillTicketResult) && PlayFabEvents._instance.OnMultiplayerCreateServerBackfillTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateServerBackfillTicketResultEvent((CreateServerBackfillTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(CreateMatchmakingTicketResult) && PlayFabEvents._instance.OnMultiplayerCreateServerMatchmakingTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerCreateServerMatchmakingTicketResultEvent((CreateMatchmakingTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteAssetResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteAssetResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteBuildAliasResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildAliasResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteBuildRegionResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteBuildRegionResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteCertificateResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteCertificateResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteContainerImageRepositoryResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteContainerImageRepositoryResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerDeleteRemoteUserResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(EnableMultiplayerServersForTitleResponse) && PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerEnableMultiplayerServersForTitleResultEvent((EnableMultiplayerServersForTitleResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetAssetUploadUrlResponse) && PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetAssetUploadUrlResultEvent((GetAssetUploadUrlResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetBuildResponse) && PlayFabEvents._instance.OnMultiplayerGetBuildResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetBuildResultEvent((GetBuildResponse)e.Result);
					return;
				}
				if (type2 == typeof(BuildAliasDetailsResponse) && PlayFabEvents._instance.OnMultiplayerGetBuildAliasResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetBuildAliasResultEvent((BuildAliasDetailsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetContainerRegistryCredentialsResponse) && PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetContainerRegistryCredentialsResultEvent((GetContainerRegistryCredentialsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetMatchResult) && PlayFabEvents._instance.OnMultiplayerGetMatchResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMatchResultEvent((GetMatchResult)e.Result);
					return;
				}
				if (type2 == typeof(GetMatchmakingQueueResult) && PlayFabEvents._instance.OnMultiplayerGetMatchmakingQueueResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMatchmakingQueueResultEvent((GetMatchmakingQueueResult)e.Result);
					return;
				}
				if (type2 == typeof(GetMatchmakingTicketResult) && PlayFabEvents._instance.OnMultiplayerGetMatchmakingTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMatchmakingTicketResultEvent((GetMatchmakingTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(GetMultiplayerServerDetailsResponse) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerDetailsResultEvent((GetMultiplayerServerDetailsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetMultiplayerServerLogsResponse) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerLogsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerServerLogsResultEvent((GetMultiplayerServerLogsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetMultiplayerServerLogsResponse) && PlayFabEvents._instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetMultiplayerSessionLogsBySessionIdResultEvent((GetMultiplayerServerLogsResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetQueueStatisticsResult) && PlayFabEvents._instance.OnMultiplayerGetQueueStatisticsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetQueueStatisticsResultEvent((GetQueueStatisticsResult)e.Result);
					return;
				}
				if (type2 == typeof(GetRemoteLoginEndpointResponse) && PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetRemoteLoginEndpointResultEvent((GetRemoteLoginEndpointResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetServerBackfillTicketResult) && PlayFabEvents._instance.OnMultiplayerGetServerBackfillTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetServerBackfillTicketResultEvent((GetServerBackfillTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleEnabledForMultiplayerServersStatusResponse) && PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetTitleEnabledForMultiplayerServersStatusResultEvent((GetTitleEnabledForMultiplayerServersStatusResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetTitleMultiplayerServersQuotasResponse) && PlayFabEvents._instance.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerGetTitleMultiplayerServersQuotasResultEvent((GetTitleMultiplayerServersQuotasResponse)e.Result);
					return;
				}
				if (type2 == typeof(JoinMatchmakingTicketResult) && PlayFabEvents._instance.OnMultiplayerJoinMatchmakingTicketResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerJoinMatchmakingTicketResultEvent((JoinMatchmakingTicketResult)e.Result);
					return;
				}
				if (type2 == typeof(ListMultiplayerServersResponse) && PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListArchivedMultiplayerServersResultEvent((ListMultiplayerServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListAssetSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListAssetSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListAssetSummariesResultEvent((ListAssetSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListBuildAliasesForTitleResponse) && PlayFabEvents._instance.OnMultiplayerListBuildAliasesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListBuildAliasesResultEvent((ListBuildAliasesForTitleResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListBuildSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListBuildSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListBuildSummariesResultEvent((ListBuildSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListCertificateSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListCertificateSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListCertificateSummariesResultEvent((ListCertificateSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListContainerImagesResponse) && PlayFabEvents._instance.OnMultiplayerListContainerImagesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImagesResultEvent((ListContainerImagesResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListContainerImageTagsResponse) && PlayFabEvents._instance.OnMultiplayerListContainerImageTagsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListContainerImageTagsResultEvent((ListContainerImageTagsResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListMatchmakingQueuesResult) && PlayFabEvents._instance.OnMultiplayerListMatchmakingQueuesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMatchmakingQueuesResultEvent((ListMatchmakingQueuesResult)e.Result);
					return;
				}
				if (type2 == typeof(ListMatchmakingTicketsForPlayerResult) && PlayFabEvents._instance.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMatchmakingTicketsForPlayerResultEvent((ListMatchmakingTicketsForPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(ListMultiplayerServersResponse) && PlayFabEvents._instance.OnMultiplayerListMultiplayerServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListMultiplayerServersResultEvent((ListMultiplayerServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListPartyQosServersResponse) && PlayFabEvents._instance.OnMultiplayerListPartyQosServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListPartyQosServersResultEvent((ListPartyQosServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListQosServersResponse) && PlayFabEvents._instance.OnMultiplayerListQosServersResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListQosServersResultEvent((ListQosServersResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListQosServersForTitleResponse) && PlayFabEvents._instance.OnMultiplayerListQosServersForTitleResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListQosServersForTitleResultEvent((ListQosServersForTitleResponse)e.Result);
					return;
				}
				if (type2 == typeof(ListServerBackfillTicketsForPlayerResult) && PlayFabEvents._instance.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListServerBackfillTicketsForPlayerResultEvent((ListServerBackfillTicketsForPlayerResult)e.Result);
					return;
				}
				if (type2 == typeof(ListVirtualMachineSummariesResponse) && PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerListVirtualMachineSummariesResultEvent((ListVirtualMachineSummariesResponse)e.Result);
					return;
				}
				if (type2 == typeof(RemoveMatchmakingQueueResult) && PlayFabEvents._instance.OnMultiplayerRemoveMatchmakingQueueResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRemoveMatchmakingQueueResultEvent((RemoveMatchmakingQueueResult)e.Result);
					return;
				}
				if (type2 == typeof(RequestMultiplayerServerResponse) && PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRequestMultiplayerServerResultEvent((RequestMultiplayerServerResponse)e.Result);
					return;
				}
				if (type2 == typeof(RolloverContainerRegistryCredentialsResponse) && PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerRolloverContainerRegistryCredentialsResultEvent((RolloverContainerRegistryCredentialsResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetMatchmakingQueueResult) && PlayFabEvents._instance.OnMultiplayerSetMatchmakingQueueResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerSetMatchmakingQueueResultEvent((SetMatchmakingQueueResult)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerShutdownMultiplayerServerResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerUntagContainerImageResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUntagContainerImageResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(BuildAliasDetailsResponse) && PlayFabEvents._instance.OnMultiplayerUpdateBuildAliasResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildAliasResultEvent((BuildAliasDetailsResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUpdateBuildRegionsResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(PlayFab.MultiplayerModels.EmptyResponse) && PlayFabEvents._instance.OnMultiplayerUploadCertificateResultEvent != null)
				{
					PlayFabEvents._instance.OnMultiplayerUploadCertificateResultEvent((PlayFab.MultiplayerModels.EmptyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetGlobalPolicyResponse) && PlayFabEvents._instance.OnProfilesGetGlobalPolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetGlobalPolicyResultEvent((GetGlobalPolicyResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetEntityProfileResponse) && PlayFabEvents._instance.OnProfilesGetProfileResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfileResultEvent((GetEntityProfileResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetEntityProfilesResponse) && PlayFabEvents._instance.OnProfilesGetProfilesResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetProfilesResultEvent((GetEntityProfilesResponse)e.Result);
					return;
				}
				if (type2 == typeof(GetTitlePlayersFromMasterPlayerAccountIdsResponse) && PlayFabEvents._instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesGetTitlePlayersFromMasterPlayerAccountIdsResultEvent((GetTitlePlayersFromMasterPlayerAccountIdsResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetGlobalPolicyResponse) && PlayFabEvents._instance.OnProfilesSetGlobalPolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetGlobalPolicyResultEvent((SetGlobalPolicyResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetProfileLanguageResponse) && PlayFabEvents._instance.OnProfilesSetProfileLanguageResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfileLanguageResultEvent((SetProfileLanguageResponse)e.Result);
					return;
				}
				if (type2 == typeof(SetEntityProfilePolicyResponse) && PlayFabEvents._instance.OnProfilesSetProfilePolicyResultEvent != null)
				{
					PlayFabEvents._instance.OnProfilesSetProfilePolicyResultEvent((SetEntityProfilePolicyResponse)e.Result);
					return;
				}
			}
		}

		// Token: 0x04001220 RID: 4640
		private static PlayFabEvents _instance;

		// Token: 0x0200035D RID: 861
		// (Invoke) Token: 0x06000BC0 RID: 3008
		public delegate void PlayFabErrorEvent(PlayFabRequestCommon request, PlayFabError error);

		// Token: 0x0200035E RID: 862
		// (Invoke) Token: 0x06000BC4 RID: 3012
		public delegate void PlayFabResultEvent<in TResult>(TResult result) where TResult : PlayFabResultCommon;

		// Token: 0x0200035F RID: 863
		// (Invoke) Token: 0x06000BC8 RID: 3016
		public delegate void PlayFabRequestEvent<in TRequest>(TRequest request) where TRequest : PlayFabRequestCommon;
	}
}
