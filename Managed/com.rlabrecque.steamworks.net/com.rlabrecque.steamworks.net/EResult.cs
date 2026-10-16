using System;

namespace Steamworks
{
	// Token: 0x0200013D RID: 317
	public enum EResult
	{
		// Token: 0x040007FA RID: 2042
		k_EResultNone,
		// Token: 0x040007FB RID: 2043
		k_EResultOK,
		// Token: 0x040007FC RID: 2044
		k_EResultFail,
		// Token: 0x040007FD RID: 2045
		k_EResultNoConnection,
		// Token: 0x040007FE RID: 2046
		k_EResultInvalidPassword = 5,
		// Token: 0x040007FF RID: 2047
		k_EResultLoggedInElsewhere,
		// Token: 0x04000800 RID: 2048
		k_EResultInvalidProtocolVer,
		// Token: 0x04000801 RID: 2049
		k_EResultInvalidParam,
		// Token: 0x04000802 RID: 2050
		k_EResultFileNotFound,
		// Token: 0x04000803 RID: 2051
		k_EResultBusy,
		// Token: 0x04000804 RID: 2052
		k_EResultInvalidState,
		// Token: 0x04000805 RID: 2053
		k_EResultInvalidName,
		// Token: 0x04000806 RID: 2054
		k_EResultInvalidEmail,
		// Token: 0x04000807 RID: 2055
		k_EResultDuplicateName,
		// Token: 0x04000808 RID: 2056
		k_EResultAccessDenied,
		// Token: 0x04000809 RID: 2057
		k_EResultTimeout,
		// Token: 0x0400080A RID: 2058
		k_EResultBanned,
		// Token: 0x0400080B RID: 2059
		k_EResultAccountNotFound,
		// Token: 0x0400080C RID: 2060
		k_EResultInvalidSteamID,
		// Token: 0x0400080D RID: 2061
		k_EResultServiceUnavailable,
		// Token: 0x0400080E RID: 2062
		k_EResultNotLoggedOn,
		// Token: 0x0400080F RID: 2063
		k_EResultPending,
		// Token: 0x04000810 RID: 2064
		k_EResultEncryptionFailure,
		// Token: 0x04000811 RID: 2065
		k_EResultInsufficientPrivilege,
		// Token: 0x04000812 RID: 2066
		k_EResultLimitExceeded,
		// Token: 0x04000813 RID: 2067
		k_EResultRevoked,
		// Token: 0x04000814 RID: 2068
		k_EResultExpired,
		// Token: 0x04000815 RID: 2069
		k_EResultAlreadyRedeemed,
		// Token: 0x04000816 RID: 2070
		k_EResultDuplicateRequest,
		// Token: 0x04000817 RID: 2071
		k_EResultAlreadyOwned,
		// Token: 0x04000818 RID: 2072
		k_EResultIPNotFound,
		// Token: 0x04000819 RID: 2073
		k_EResultPersistFailed,
		// Token: 0x0400081A RID: 2074
		k_EResultLockingFailed,
		// Token: 0x0400081B RID: 2075
		k_EResultLogonSessionReplaced,
		// Token: 0x0400081C RID: 2076
		k_EResultConnectFailed,
		// Token: 0x0400081D RID: 2077
		k_EResultHandshakeFailed,
		// Token: 0x0400081E RID: 2078
		k_EResultIOFailure,
		// Token: 0x0400081F RID: 2079
		k_EResultRemoteDisconnect,
		// Token: 0x04000820 RID: 2080
		k_EResultShoppingCartNotFound,
		// Token: 0x04000821 RID: 2081
		k_EResultBlocked,
		// Token: 0x04000822 RID: 2082
		k_EResultIgnored,
		// Token: 0x04000823 RID: 2083
		k_EResultNoMatch,
		// Token: 0x04000824 RID: 2084
		k_EResultAccountDisabled,
		// Token: 0x04000825 RID: 2085
		k_EResultServiceReadOnly,
		// Token: 0x04000826 RID: 2086
		k_EResultAccountNotFeatured,
		// Token: 0x04000827 RID: 2087
		k_EResultAdministratorOK,
		// Token: 0x04000828 RID: 2088
		k_EResultContentVersion,
		// Token: 0x04000829 RID: 2089
		k_EResultTryAnotherCM,
		// Token: 0x0400082A RID: 2090
		k_EResultPasswordRequiredToKickSession,
		// Token: 0x0400082B RID: 2091
		k_EResultAlreadyLoggedInElsewhere,
		// Token: 0x0400082C RID: 2092
		k_EResultSuspended,
		// Token: 0x0400082D RID: 2093
		k_EResultCancelled,
		// Token: 0x0400082E RID: 2094
		k_EResultDataCorruption,
		// Token: 0x0400082F RID: 2095
		k_EResultDiskFull,
		// Token: 0x04000830 RID: 2096
		k_EResultRemoteCallFailed,
		// Token: 0x04000831 RID: 2097
		k_EResultPasswordUnset,
		// Token: 0x04000832 RID: 2098
		k_EResultExternalAccountUnlinked,
		// Token: 0x04000833 RID: 2099
		k_EResultPSNTicketInvalid,
		// Token: 0x04000834 RID: 2100
		k_EResultExternalAccountAlreadyLinked,
		// Token: 0x04000835 RID: 2101
		k_EResultRemoteFileConflict,
		// Token: 0x04000836 RID: 2102
		k_EResultIllegalPassword,
		// Token: 0x04000837 RID: 2103
		k_EResultSameAsPreviousValue,
		// Token: 0x04000838 RID: 2104
		k_EResultAccountLogonDenied,
		// Token: 0x04000839 RID: 2105
		k_EResultCannotUseOldPassword,
		// Token: 0x0400083A RID: 2106
		k_EResultInvalidLoginAuthCode,
		// Token: 0x0400083B RID: 2107
		k_EResultAccountLogonDeniedNoMail,
		// Token: 0x0400083C RID: 2108
		k_EResultHardwareNotCapableOfIPT,
		// Token: 0x0400083D RID: 2109
		k_EResultIPTInitError,
		// Token: 0x0400083E RID: 2110
		k_EResultParentalControlRestricted,
		// Token: 0x0400083F RID: 2111
		k_EResultFacebookQueryError,
		// Token: 0x04000840 RID: 2112
		k_EResultExpiredLoginAuthCode,
		// Token: 0x04000841 RID: 2113
		k_EResultIPLoginRestrictionFailed,
		// Token: 0x04000842 RID: 2114
		k_EResultAccountLockedDown,
		// Token: 0x04000843 RID: 2115
		k_EResultAccountLogonDeniedVerifiedEmailRequired,
		// Token: 0x04000844 RID: 2116
		k_EResultNoMatchingURL,
		// Token: 0x04000845 RID: 2117
		k_EResultBadResponse,
		// Token: 0x04000846 RID: 2118
		k_EResultRequirePasswordReEntry,
		// Token: 0x04000847 RID: 2119
		k_EResultValueOutOfRange,
		// Token: 0x04000848 RID: 2120
		k_EResultUnexpectedError,
		// Token: 0x04000849 RID: 2121
		k_EResultDisabled,
		// Token: 0x0400084A RID: 2122
		k_EResultInvalidCEGSubmission,
		// Token: 0x0400084B RID: 2123
		k_EResultRestrictedDevice,
		// Token: 0x0400084C RID: 2124
		k_EResultRegionLocked,
		// Token: 0x0400084D RID: 2125
		k_EResultRateLimitExceeded,
		// Token: 0x0400084E RID: 2126
		k_EResultAccountLoginDeniedNeedTwoFactor,
		// Token: 0x0400084F RID: 2127
		k_EResultItemDeleted,
		// Token: 0x04000850 RID: 2128
		k_EResultAccountLoginDeniedThrottle,
		// Token: 0x04000851 RID: 2129
		k_EResultTwoFactorCodeMismatch,
		// Token: 0x04000852 RID: 2130
		k_EResultTwoFactorActivationCodeMismatch,
		// Token: 0x04000853 RID: 2131
		k_EResultAccountAssociatedToMultiplePartners,
		// Token: 0x04000854 RID: 2132
		k_EResultNotModified,
		// Token: 0x04000855 RID: 2133
		k_EResultNoMobileDevice,
		// Token: 0x04000856 RID: 2134
		k_EResultTimeNotSynced,
		// Token: 0x04000857 RID: 2135
		k_EResultSmsCodeFailed,
		// Token: 0x04000858 RID: 2136
		k_EResultAccountLimitExceeded,
		// Token: 0x04000859 RID: 2137
		k_EResultAccountActivityLimitExceeded,
		// Token: 0x0400085A RID: 2138
		k_EResultPhoneActivityLimitExceeded,
		// Token: 0x0400085B RID: 2139
		k_EResultRefundToWallet,
		// Token: 0x0400085C RID: 2140
		k_EResultEmailSendFailure,
		// Token: 0x0400085D RID: 2141
		k_EResultNotSettled,
		// Token: 0x0400085E RID: 2142
		k_EResultNeedCaptcha,
		// Token: 0x0400085F RID: 2143
		k_EResultGSLTDenied,
		// Token: 0x04000860 RID: 2144
		k_EResultGSOwnerDenied,
		// Token: 0x04000861 RID: 2145
		k_EResultInvalidItemType,
		// Token: 0x04000862 RID: 2146
		k_EResultIPBanned,
		// Token: 0x04000863 RID: 2147
		k_EResultGSLTExpired,
		// Token: 0x04000864 RID: 2148
		k_EResultInsufficientFunds,
		// Token: 0x04000865 RID: 2149
		k_EResultTooManyPending,
		// Token: 0x04000866 RID: 2150
		k_EResultNoSiteLicensesFound,
		// Token: 0x04000867 RID: 2151
		k_EResultWGNetworkSendExceeded,
		// Token: 0x04000868 RID: 2152
		k_EResultAccountNotFriends,
		// Token: 0x04000869 RID: 2153
		k_EResultLimitedUserAccount,
		// Token: 0x0400086A RID: 2154
		k_EResultCantRemoveItem,
		// Token: 0x0400086B RID: 2155
		k_EResultAccountDeleted,
		// Token: 0x0400086C RID: 2156
		k_EResultExistingUserCancelledLicense,
		// Token: 0x0400086D RID: 2157
		k_EResultCommunityCooldown,
		// Token: 0x0400086E RID: 2158
		k_EResultNoLauncherSpecified,
		// Token: 0x0400086F RID: 2159
		k_EResultMustAgreeToSSA,
		// Token: 0x04000870 RID: 2160
		k_EResultLauncherMigrated,
		// Token: 0x04000871 RID: 2161
		k_EResultSteamRealmMismatch,
		// Token: 0x04000872 RID: 2162
		k_EResultInvalidSignature,
		// Token: 0x04000873 RID: 2163
		k_EResultParseFailure,
		// Token: 0x04000874 RID: 2164
		k_EResultNoVerifiedPhone,
		// Token: 0x04000875 RID: 2165
		k_EResultInsufficientBattery,
		// Token: 0x04000876 RID: 2166
		k_EResultChargerRequired,
		// Token: 0x04000877 RID: 2167
		k_EResultCachedCredentialInvalid,
		// Token: 0x04000878 RID: 2168
		K_EResultPhoneNumberIsVOIP,
		// Token: 0x04000879 RID: 2169
		k_EResultNotSupported,
		// Token: 0x0400087A RID: 2170
		k_EResultFamilySizeLimitExceeded
	}
}
