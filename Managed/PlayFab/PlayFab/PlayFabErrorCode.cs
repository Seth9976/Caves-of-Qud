using System;

namespace PlayFab
{
	// Token: 0x0200001A RID: 26
	public enum PlayFabErrorCode
	{
		// Token: 0x04000029 RID: 41
		Unknown = 1,
		// Token: 0x0400002A RID: 42
		ConnectionError,
		// Token: 0x0400002B RID: 43
		JsonParseError,
		// Token: 0x0400002C RID: 44
		Success = 0,
		// Token: 0x0400002D RID: 45
		UnkownError = 500,
		// Token: 0x0400002E RID: 46
		InvalidParams = 1000,
		// Token: 0x0400002F RID: 47
		AccountNotFound,
		// Token: 0x04000030 RID: 48
		AccountBanned,
		// Token: 0x04000031 RID: 49
		InvalidUsernameOrPassword,
		// Token: 0x04000032 RID: 50
		InvalidTitleId,
		// Token: 0x04000033 RID: 51
		InvalidEmailAddress,
		// Token: 0x04000034 RID: 52
		EmailAddressNotAvailable,
		// Token: 0x04000035 RID: 53
		InvalidUsername,
		// Token: 0x04000036 RID: 54
		InvalidPassword,
		// Token: 0x04000037 RID: 55
		UsernameNotAvailable,
		// Token: 0x04000038 RID: 56
		InvalidSteamTicket,
		// Token: 0x04000039 RID: 57
		AccountAlreadyLinked,
		// Token: 0x0400003A RID: 58
		LinkedAccountAlreadyClaimed,
		// Token: 0x0400003B RID: 59
		InvalidFacebookToken,
		// Token: 0x0400003C RID: 60
		AccountNotLinked,
		// Token: 0x0400003D RID: 61
		FailedByPaymentProvider,
		// Token: 0x0400003E RID: 62
		CouponCodeNotFound,
		// Token: 0x0400003F RID: 63
		InvalidContainerItem,
		// Token: 0x04000040 RID: 64
		ContainerNotOwned,
		// Token: 0x04000041 RID: 65
		KeyNotOwned,
		// Token: 0x04000042 RID: 66
		InvalidItemIdInTable,
		// Token: 0x04000043 RID: 67
		InvalidReceipt,
		// Token: 0x04000044 RID: 68
		ReceiptAlreadyUsed,
		// Token: 0x04000045 RID: 69
		ReceiptCancelled,
		// Token: 0x04000046 RID: 70
		GameNotFound,
		// Token: 0x04000047 RID: 71
		GameModeNotFound,
		// Token: 0x04000048 RID: 72
		InvalidGoogleToken,
		// Token: 0x04000049 RID: 73
		UserIsNotPartOfDeveloper,
		// Token: 0x0400004A RID: 74
		InvalidTitleForDeveloper,
		// Token: 0x0400004B RID: 75
		TitleNameConflicts,
		// Token: 0x0400004C RID: 76
		UserisNotValid,
		// Token: 0x0400004D RID: 77
		ValueAlreadyExists,
		// Token: 0x0400004E RID: 78
		BuildNotFound,
		// Token: 0x0400004F RID: 79
		PlayerNotInGame,
		// Token: 0x04000050 RID: 80
		InvalidTicket,
		// Token: 0x04000051 RID: 81
		InvalidDeveloper,
		// Token: 0x04000052 RID: 82
		InvalidOrderInfo,
		// Token: 0x04000053 RID: 83
		RegistrationIncomplete,
		// Token: 0x04000054 RID: 84
		InvalidPlatform,
		// Token: 0x04000055 RID: 85
		UnknownError,
		// Token: 0x04000056 RID: 86
		SteamApplicationNotOwned,
		// Token: 0x04000057 RID: 87
		WrongSteamAccount,
		// Token: 0x04000058 RID: 88
		TitleNotActivated,
		// Token: 0x04000059 RID: 89
		RegistrationSessionNotFound,
		// Token: 0x0400005A RID: 90
		NoSuchMod,
		// Token: 0x0400005B RID: 91
		FileNotFound,
		// Token: 0x0400005C RID: 92
		DuplicateEmail,
		// Token: 0x0400005D RID: 93
		ItemNotFound,
		// Token: 0x0400005E RID: 94
		ItemNotOwned,
		// Token: 0x0400005F RID: 95
		ItemNotRecycleable,
		// Token: 0x04000060 RID: 96
		ItemNotAffordable,
		// Token: 0x04000061 RID: 97
		InvalidVirtualCurrency,
		// Token: 0x04000062 RID: 98
		WrongVirtualCurrency,
		// Token: 0x04000063 RID: 99
		WrongPrice,
		// Token: 0x04000064 RID: 100
		NonPositiveValue,
		// Token: 0x04000065 RID: 101
		InvalidRegion,
		// Token: 0x04000066 RID: 102
		RegionAtCapacity,
		// Token: 0x04000067 RID: 103
		ServerFailedToStart,
		// Token: 0x04000068 RID: 104
		NameNotAvailable,
		// Token: 0x04000069 RID: 105
		InsufficientFunds,
		// Token: 0x0400006A RID: 106
		InvalidDeviceID,
		// Token: 0x0400006B RID: 107
		InvalidPushNotificationToken,
		// Token: 0x0400006C RID: 108
		NoRemainingUses,
		// Token: 0x0400006D RID: 109
		InvalidPaymentProvider,
		// Token: 0x0400006E RID: 110
		PurchaseInitializationFailure,
		// Token: 0x0400006F RID: 111
		DuplicateUsername,
		// Token: 0x04000070 RID: 112
		InvalidBuyerInfo,
		// Token: 0x04000071 RID: 113
		NoGameModeParamsSet,
		// Token: 0x04000072 RID: 114
		BodyTooLarge,
		// Token: 0x04000073 RID: 115
		ReservedWordInBody,
		// Token: 0x04000074 RID: 116
		InvalidTypeInBody,
		// Token: 0x04000075 RID: 117
		InvalidRequest,
		// Token: 0x04000076 RID: 118
		ReservedEventName,
		// Token: 0x04000077 RID: 119
		InvalidUserStatistics,
		// Token: 0x04000078 RID: 120
		NotAuthenticated,
		// Token: 0x04000079 RID: 121
		StreamAlreadyExists,
		// Token: 0x0400007A RID: 122
		ErrorCreatingStream,
		// Token: 0x0400007B RID: 123
		StreamNotFound,
		// Token: 0x0400007C RID: 124
		InvalidAccount,
		// Token: 0x0400007D RID: 125
		PurchaseDoesNotExist = 1080,
		// Token: 0x0400007E RID: 126
		InvalidPurchaseTransactionStatus,
		// Token: 0x0400007F RID: 127
		APINotEnabledForGameClientAccess,
		// Token: 0x04000080 RID: 128
		NoPushNotificationARNForTitle,
		// Token: 0x04000081 RID: 129
		BuildAlreadyExists,
		// Token: 0x04000082 RID: 130
		BuildPackageDoesNotExist,
		// Token: 0x04000083 RID: 131
		CustomAnalyticsEventsNotEnabledForTitle = 1087,
		// Token: 0x04000084 RID: 132
		InvalidSharedGroupId,
		// Token: 0x04000085 RID: 133
		NotAuthorized,
		// Token: 0x04000086 RID: 134
		MissingTitleGoogleProperties,
		// Token: 0x04000087 RID: 135
		InvalidItemProperties,
		// Token: 0x04000088 RID: 136
		InvalidPSNAuthCode,
		// Token: 0x04000089 RID: 137
		InvalidItemId,
		// Token: 0x0400008A RID: 138
		PushNotEnabledForAccount,
		// Token: 0x0400008B RID: 139
		PushServiceError,
		// Token: 0x0400008C RID: 140
		ReceiptDoesNotContainInAppItems,
		// Token: 0x0400008D RID: 141
		ReceiptContainsMultipleInAppItems,
		// Token: 0x0400008E RID: 142
		InvalidBundleID,
		// Token: 0x0400008F RID: 143
		JavascriptException,
		// Token: 0x04000090 RID: 144
		InvalidSessionTicket,
		// Token: 0x04000091 RID: 145
		UnableToConnectToDatabase,
		// Token: 0x04000092 RID: 146
		InternalServerError = 1110,
		// Token: 0x04000093 RID: 147
		InvalidReportDate,
		// Token: 0x04000094 RID: 148
		ReportNotAvailable,
		// Token: 0x04000095 RID: 149
		DatabaseThroughputExceeded,
		// Token: 0x04000096 RID: 150
		InvalidGameTicket = 1115,
		// Token: 0x04000097 RID: 151
		ExpiredGameTicket,
		// Token: 0x04000098 RID: 152
		GameTicketDoesNotMatchLobby,
		// Token: 0x04000099 RID: 153
		LinkedDeviceAlreadyClaimed,
		// Token: 0x0400009A RID: 154
		DeviceAlreadyLinked,
		// Token: 0x0400009B RID: 155
		DeviceNotLinked,
		// Token: 0x0400009C RID: 156
		PartialFailure,
		// Token: 0x0400009D RID: 157
		PublisherNotSet,
		// Token: 0x0400009E RID: 158
		ServiceUnavailable,
		// Token: 0x0400009F RID: 159
		VersionNotFound,
		// Token: 0x040000A0 RID: 160
		RevisionNotFound,
		// Token: 0x040000A1 RID: 161
		InvalidPublisherId,
		// Token: 0x040000A2 RID: 162
		DownstreamServiceUnavailable,
		// Token: 0x040000A3 RID: 163
		APINotIncludedInTitleUsageTier,
		// Token: 0x040000A4 RID: 164
		DAULimitExceeded,
		// Token: 0x040000A5 RID: 165
		APIRequestLimitExceeded,
		// Token: 0x040000A6 RID: 166
		InvalidAPIEndpoint,
		// Token: 0x040000A7 RID: 167
		BuildNotAvailable,
		// Token: 0x040000A8 RID: 168
		ConcurrentEditError,
		// Token: 0x040000A9 RID: 169
		ContentNotFound,
		// Token: 0x040000AA RID: 170
		CharacterNotFound,
		// Token: 0x040000AB RID: 171
		CloudScriptNotFound,
		// Token: 0x040000AC RID: 172
		ContentQuotaExceeded,
		// Token: 0x040000AD RID: 173
		InvalidCharacterStatistics,
		// Token: 0x040000AE RID: 174
		PhotonNotEnabledForTitle,
		// Token: 0x040000AF RID: 175
		PhotonApplicationNotFound,
		// Token: 0x040000B0 RID: 176
		PhotonApplicationNotAssociatedWithTitle,
		// Token: 0x040000B1 RID: 177
		InvalidEmailOrPassword,
		// Token: 0x040000B2 RID: 178
		FacebookAPIError,
		// Token: 0x040000B3 RID: 179
		InvalidContentType,
		// Token: 0x040000B4 RID: 180
		KeyLengthExceeded,
		// Token: 0x040000B5 RID: 181
		DataLengthExceeded,
		// Token: 0x040000B6 RID: 182
		TooManyKeys,
		// Token: 0x040000B7 RID: 183
		FreeTierCannotHaveVirtualCurrency,
		// Token: 0x040000B8 RID: 184
		MissingAmazonSharedKey,
		// Token: 0x040000B9 RID: 185
		AmazonValidationError,
		// Token: 0x040000BA RID: 186
		InvalidPSNIssuerId,
		// Token: 0x040000BB RID: 187
		PSNInaccessible,
		// Token: 0x040000BC RID: 188
		ExpiredAuthToken,
		// Token: 0x040000BD RID: 189
		FailedToGetEntitlements,
		// Token: 0x040000BE RID: 190
		FailedToConsumeEntitlement,
		// Token: 0x040000BF RID: 191
		TradeAcceptingUserNotAllowed,
		// Token: 0x040000C0 RID: 192
		TradeInventoryItemIsAssignedToCharacter,
		// Token: 0x040000C1 RID: 193
		TradeInventoryItemIsBundle,
		// Token: 0x040000C2 RID: 194
		TradeStatusNotValidForCancelling,
		// Token: 0x040000C3 RID: 195
		TradeStatusNotValidForAccepting,
		// Token: 0x040000C4 RID: 196
		TradeDoesNotExist,
		// Token: 0x040000C5 RID: 197
		TradeCancelled,
		// Token: 0x040000C6 RID: 198
		TradeAlreadyFilled,
		// Token: 0x040000C7 RID: 199
		TradeWaitForStatusTimeout,
		// Token: 0x040000C8 RID: 200
		TradeInventoryItemExpired,
		// Token: 0x040000C9 RID: 201
		TradeMissingOfferedAndAcceptedItems,
		// Token: 0x040000CA RID: 202
		TradeAcceptedItemIsBundle,
		// Token: 0x040000CB RID: 203
		TradeAcceptedItemIsStackable,
		// Token: 0x040000CC RID: 204
		TradeInventoryItemInvalidStatus,
		// Token: 0x040000CD RID: 205
		TradeAcceptedCatalogItemInvalid,
		// Token: 0x040000CE RID: 206
		TradeAllowedUsersInvalid,
		// Token: 0x040000CF RID: 207
		TradeInventoryItemDoesNotExist,
		// Token: 0x040000D0 RID: 208
		TradeInventoryItemIsConsumed,
		// Token: 0x040000D1 RID: 209
		TradeInventoryItemIsStackable,
		// Token: 0x040000D2 RID: 210
		TradeAcceptedItemsMismatch,
		// Token: 0x040000D3 RID: 211
		InvalidKongregateToken,
		// Token: 0x040000D4 RID: 212
		FeatureNotConfiguredForTitle,
		// Token: 0x040000D5 RID: 213
		NoMatchingCatalogItemForReceipt,
		// Token: 0x040000D6 RID: 214
		InvalidCurrencyCode,
		// Token: 0x040000D7 RID: 215
		NoRealMoneyPriceForCatalogItem,
		// Token: 0x040000D8 RID: 216
		TradeInventoryItemIsNotTradable,
		// Token: 0x040000D9 RID: 217
		TradeAcceptedCatalogItemIsNotTradable,
		// Token: 0x040000DA RID: 218
		UsersAlreadyFriends,
		// Token: 0x040000DB RID: 219
		LinkedIdentifierAlreadyClaimed,
		// Token: 0x040000DC RID: 220
		CustomIdNotLinked,
		// Token: 0x040000DD RID: 221
		TotalDataSizeExceeded,
		// Token: 0x040000DE RID: 222
		DeleteKeyConflict,
		// Token: 0x040000DF RID: 223
		InvalidXboxLiveToken,
		// Token: 0x040000E0 RID: 224
		ExpiredXboxLiveToken,
		// Token: 0x040000E1 RID: 225
		ResettableStatisticVersionRequired,
		// Token: 0x040000E2 RID: 226
		NotAuthorizedByTitle,
		// Token: 0x040000E3 RID: 227
		NoPartnerEnabled,
		// Token: 0x040000E4 RID: 228
		InvalidPartnerResponse,
		// Token: 0x040000E5 RID: 229
		APINotEnabledForGameServerAccess,
		// Token: 0x040000E6 RID: 230
		StatisticNotFound,
		// Token: 0x040000E7 RID: 231
		StatisticNameConflict,
		// Token: 0x040000E8 RID: 232
		StatisticVersionClosedForWrites,
		// Token: 0x040000E9 RID: 233
		StatisticVersionInvalid,
		// Token: 0x040000EA RID: 234
		APIClientRequestRateLimitExceeded,
		// Token: 0x040000EB RID: 235
		InvalidJSONContent,
		// Token: 0x040000EC RID: 236
		InvalidDropTable,
		// Token: 0x040000ED RID: 237
		StatisticVersionAlreadyIncrementedForScheduledInterval,
		// Token: 0x040000EE RID: 238
		StatisticCountLimitExceeded,
		// Token: 0x040000EF RID: 239
		StatisticVersionIncrementRateExceeded,
		// Token: 0x040000F0 RID: 240
		ContainerKeyInvalid,
		// Token: 0x040000F1 RID: 241
		CloudScriptExecutionTimeLimitExceeded,
		// Token: 0x040000F2 RID: 242
		NoWritePermissionsForEvent,
		// Token: 0x040000F3 RID: 243
		CloudScriptFunctionArgumentSizeExceeded,
		// Token: 0x040000F4 RID: 244
		CloudScriptAPIRequestCountExceeded,
		// Token: 0x040000F5 RID: 245
		CloudScriptAPIRequestError,
		// Token: 0x040000F6 RID: 246
		CloudScriptHTTPRequestError,
		// Token: 0x040000F7 RID: 247
		InsufficientGuildRole,
		// Token: 0x040000F8 RID: 248
		GuildNotFound,
		// Token: 0x040000F9 RID: 249
		OverLimit,
		// Token: 0x040000FA RID: 250
		EventNotFound,
		// Token: 0x040000FB RID: 251
		InvalidEventField,
		// Token: 0x040000FC RID: 252
		InvalidEventName,
		// Token: 0x040000FD RID: 253
		CatalogNotConfigured,
		// Token: 0x040000FE RID: 254
		OperationNotSupportedForPlatform,
		// Token: 0x040000FF RID: 255
		SegmentNotFound,
		// Token: 0x04000100 RID: 256
		StoreNotFound,
		// Token: 0x04000101 RID: 257
		InvalidStatisticName,
		// Token: 0x04000102 RID: 258
		TitleNotQualifiedForLimit,
		// Token: 0x04000103 RID: 259
		InvalidServiceLimitLevel,
		// Token: 0x04000104 RID: 260
		ServiceLimitLevelInTransition,
		// Token: 0x04000105 RID: 261
		CouponAlreadyRedeemed,
		// Token: 0x04000106 RID: 262
		GameServerBuildSizeLimitExceeded,
		// Token: 0x04000107 RID: 263
		GameServerBuildCountLimitExceeded,
		// Token: 0x04000108 RID: 264
		VirtualCurrencyCountLimitExceeded,
		// Token: 0x04000109 RID: 265
		VirtualCurrencyCodeExists,
		// Token: 0x0400010A RID: 266
		TitleNewsItemCountLimitExceeded,
		// Token: 0x0400010B RID: 267
		InvalidTwitchToken,
		// Token: 0x0400010C RID: 268
		TwitchResponseError,
		// Token: 0x0400010D RID: 269
		ProfaneDisplayName,
		// Token: 0x0400010E RID: 270
		UserAlreadyAdded,
		// Token: 0x0400010F RID: 271
		InvalidVirtualCurrencyCode,
		// Token: 0x04000110 RID: 272
		VirtualCurrencyCannotBeDeleted,
		// Token: 0x04000111 RID: 273
		IdentifierAlreadyClaimed,
		// Token: 0x04000112 RID: 274
		IdentifierNotLinked,
		// Token: 0x04000113 RID: 275
		InvalidContinuationToken,
		// Token: 0x04000114 RID: 276
		ExpiredContinuationToken,
		// Token: 0x04000115 RID: 277
		InvalidSegment,
		// Token: 0x04000116 RID: 278
		InvalidSessionId,
		// Token: 0x04000117 RID: 279
		SessionLogNotFound,
		// Token: 0x04000118 RID: 280
		InvalidSearchTerm,
		// Token: 0x04000119 RID: 281
		TwoFactorAuthenticationTokenRequired,
		// Token: 0x0400011A RID: 282
		GameServerHostCountLimitExceeded,
		// Token: 0x0400011B RID: 283
		PlayerTagCountLimitExceeded,
		// Token: 0x0400011C RID: 284
		RequestAlreadyRunning,
		// Token: 0x0400011D RID: 285
		ActionGroupNotFound,
		// Token: 0x0400011E RID: 286
		MaximumSegmentBulkActionJobsRunning,
		// Token: 0x0400011F RID: 287
		NoActionsOnPlayersInSegmentJob,
		// Token: 0x04000120 RID: 288
		DuplicateStatisticName,
		// Token: 0x04000121 RID: 289
		ScheduledTaskNameConflict,
		// Token: 0x04000122 RID: 290
		ScheduledTaskCreateConflict,
		// Token: 0x04000123 RID: 291
		InvalidScheduledTaskName,
		// Token: 0x04000124 RID: 292
		InvalidTaskSchedule,
		// Token: 0x04000125 RID: 293
		SteamNotEnabledForTitle,
		// Token: 0x04000126 RID: 294
		LimitNotAnUpgradeOption,
		// Token: 0x04000127 RID: 295
		NoSecretKeyEnabledForCloudScript,
		// Token: 0x04000128 RID: 296
		TaskNotFound,
		// Token: 0x04000129 RID: 297
		TaskInstanceNotFound,
		// Token: 0x0400012A RID: 298
		InvalidIdentityProviderId,
		// Token: 0x0400012B RID: 299
		MisconfiguredIdentityProvider,
		// Token: 0x0400012C RID: 300
		InvalidScheduledTaskType,
		// Token: 0x0400012D RID: 301
		BillingInformationRequired,
		// Token: 0x0400012E RID: 302
		LimitedEditionItemUnavailable,
		// Token: 0x0400012F RID: 303
		InvalidAdPlacementAndReward,
		// Token: 0x04000130 RID: 304
		AllAdPlacementViewsAlreadyConsumed,
		// Token: 0x04000131 RID: 305
		GoogleOAuthNotConfiguredForTitle,
		// Token: 0x04000132 RID: 306
		GoogleOAuthError,
		// Token: 0x04000133 RID: 307
		UserNotFriend,
		// Token: 0x04000134 RID: 308
		InvalidSignature,
		// Token: 0x04000135 RID: 309
		InvalidPublicKey,
		// Token: 0x04000136 RID: 310
		GoogleOAuthNoIdTokenIncludedInResponse,
		// Token: 0x04000137 RID: 311
		StatisticUpdateInProgress,
		// Token: 0x04000138 RID: 312
		LeaderboardVersionNotAvailable,
		// Token: 0x04000139 RID: 313
		StatisticAlreadyHasPrizeTable = 1279,
		// Token: 0x0400013A RID: 314
		PrizeTableHasOverlappingRanks,
		// Token: 0x0400013B RID: 315
		PrizeTableHasMissingRanks,
		// Token: 0x0400013C RID: 316
		PrizeTableRankStartsAtZero,
		// Token: 0x0400013D RID: 317
		InvalidStatistic,
		// Token: 0x0400013E RID: 318
		ExpressionParseFailure,
		// Token: 0x0400013F RID: 319
		ExpressionInvokeFailure,
		// Token: 0x04000140 RID: 320
		ExpressionTooLong,
		// Token: 0x04000141 RID: 321
		DataUpdateRateExceeded,
		// Token: 0x04000142 RID: 322
		RestrictedEmailDomain,
		// Token: 0x04000143 RID: 323
		EncryptionKeyDisabled,
		// Token: 0x04000144 RID: 324
		EncryptionKeyMissing,
		// Token: 0x04000145 RID: 325
		EncryptionKeyBroken,
		// Token: 0x04000146 RID: 326
		NoSharedSecretKeyConfigured,
		// Token: 0x04000147 RID: 327
		SecretKeyNotFound,
		// Token: 0x04000148 RID: 328
		PlayerSecretAlreadyConfigured,
		// Token: 0x04000149 RID: 329
		APIRequestsDisabledForTitle,
		// Token: 0x0400014A RID: 330
		InvalidSharedSecretKey,
		// Token: 0x0400014B RID: 331
		PrizeTableHasNoRanks,
		// Token: 0x0400014C RID: 332
		ProfileDoesNotExist,
		// Token: 0x0400014D RID: 333
		ContentS3OriginBucketNotConfigured,
		// Token: 0x0400014E RID: 334
		InvalidEnvironmentForReceipt,
		// Token: 0x0400014F RID: 335
		EncryptedRequestNotAllowed,
		// Token: 0x04000150 RID: 336
		SignedRequestNotAllowed,
		// Token: 0x04000151 RID: 337
		RequestViewConstraintParamsNotAllowed,
		// Token: 0x04000152 RID: 338
		BadPartnerConfiguration,
		// Token: 0x04000153 RID: 339
		XboxBPCertificateFailure,
		// Token: 0x04000154 RID: 340
		XboxXASSExchangeFailure,
		// Token: 0x04000155 RID: 341
		InvalidEntityId,
		// Token: 0x04000156 RID: 342
		StatisticValueAggregationOverflow,
		// Token: 0x04000157 RID: 343
		EmailMessageFromAddressIsMissing,
		// Token: 0x04000158 RID: 344
		EmailMessageToAddressIsMissing,
		// Token: 0x04000159 RID: 345
		SmtpServerAuthenticationError,
		// Token: 0x0400015A RID: 346
		SmtpServerLimitExceeded,
		// Token: 0x0400015B RID: 347
		SmtpServerInsufficientStorage,
		// Token: 0x0400015C RID: 348
		SmtpServerCommunicationError,
		// Token: 0x0400015D RID: 349
		SmtpServerGeneralFailure,
		// Token: 0x0400015E RID: 350
		EmailClientTimeout,
		// Token: 0x0400015F RID: 351
		EmailClientCanceledTask,
		// Token: 0x04000160 RID: 352
		EmailTemplateMissing,
		// Token: 0x04000161 RID: 353
		InvalidHostForTitleId,
		// Token: 0x04000162 RID: 354
		EmailConfirmationTokenDoesNotExist,
		// Token: 0x04000163 RID: 355
		EmailConfirmationTokenExpired,
		// Token: 0x04000164 RID: 356
		AccountDeleted,
		// Token: 0x04000165 RID: 357
		PlayerSecretNotConfigured,
		// Token: 0x04000166 RID: 358
		InvalidSignatureTime,
		// Token: 0x04000167 RID: 359
		NoContactEmailAddressFound,
		// Token: 0x04000168 RID: 360
		InvalidAuthToken,
		// Token: 0x04000169 RID: 361
		AuthTokenDoesNotExist,
		// Token: 0x0400016A RID: 362
		AuthTokenExpired,
		// Token: 0x0400016B RID: 363
		AuthTokenAlreadyUsedToResetPassword,
		// Token: 0x0400016C RID: 364
		MembershipNameTooLong,
		// Token: 0x0400016D RID: 365
		MembershipNotFound,
		// Token: 0x0400016E RID: 366
		GoogleServiceAccountInvalid,
		// Token: 0x0400016F RID: 367
		GoogleServiceAccountParseFailure,
		// Token: 0x04000170 RID: 368
		EntityTokenMissing,
		// Token: 0x04000171 RID: 369
		EntityTokenInvalid,
		// Token: 0x04000172 RID: 370
		EntityTokenExpired,
		// Token: 0x04000173 RID: 371
		EntityTokenRevoked,
		// Token: 0x04000174 RID: 372
		InvalidProductForSubscription,
		// Token: 0x04000175 RID: 373
		XboxInaccessible,
		// Token: 0x04000176 RID: 374
		SubscriptionAlreadyTaken,
		// Token: 0x04000177 RID: 375
		SmtpAddonNotEnabled,
		// Token: 0x04000178 RID: 376
		APIConcurrentRequestLimitExceeded,
		// Token: 0x04000179 RID: 377
		XboxRejectedXSTSExchangeRequest,
		// Token: 0x0400017A RID: 378
		VariableNotDefined,
		// Token: 0x0400017B RID: 379
		TemplateVersionNotDefined,
		// Token: 0x0400017C RID: 380
		FileTooLarge,
		// Token: 0x0400017D RID: 381
		TitleDeleted,
		// Token: 0x0400017E RID: 382
		TitleContainsUserAccounts,
		// Token: 0x0400017F RID: 383
		TitleDeletionPlayerCleanupFailure,
		// Token: 0x04000180 RID: 384
		EntityFileOperationPending,
		// Token: 0x04000181 RID: 385
		NoEntityFileOperationPending,
		// Token: 0x04000182 RID: 386
		EntityProfileVersionMismatch,
		// Token: 0x04000183 RID: 387
		TemplateVersionTooOld,
		// Token: 0x04000184 RID: 388
		MembershipDefinitionInUse,
		// Token: 0x04000185 RID: 389
		PaymentPageNotConfigured,
		// Token: 0x04000186 RID: 390
		FailedLoginAttemptRateLimitExceeded,
		// Token: 0x04000187 RID: 391
		EntityBlockedByGroup,
		// Token: 0x04000188 RID: 392
		RoleDoesNotExist,
		// Token: 0x04000189 RID: 393
		EntityIsAlreadyMember,
		// Token: 0x0400018A RID: 394
		DuplicateRoleId,
		// Token: 0x0400018B RID: 395
		GroupInvitationNotFound,
		// Token: 0x0400018C RID: 396
		GroupApplicationNotFound,
		// Token: 0x0400018D RID: 397
		OutstandingInvitationAcceptedInstead,
		// Token: 0x0400018E RID: 398
		OutstandingApplicationAcceptedInstead,
		// Token: 0x0400018F RID: 399
		RoleIsGroupDefaultMember,
		// Token: 0x04000190 RID: 400
		RoleIsGroupAdmin,
		// Token: 0x04000191 RID: 401
		RoleNameNotAvailable,
		// Token: 0x04000192 RID: 402
		GroupNameNotAvailable,
		// Token: 0x04000193 RID: 403
		EmailReportAlreadySent,
		// Token: 0x04000194 RID: 404
		EmailReportRecipientBlacklisted,
		// Token: 0x04000195 RID: 405
		EventNamespaceNotAllowed,
		// Token: 0x04000196 RID: 406
		EventEntityNotAllowed,
		// Token: 0x04000197 RID: 407
		InvalidEntityType,
		// Token: 0x04000198 RID: 408
		NullTokenResultFromAad,
		// Token: 0x04000199 RID: 409
		InvalidTokenResultFromAad,
		// Token: 0x0400019A RID: 410
		NoValidCertificateForAad,
		// Token: 0x0400019B RID: 411
		InvalidCertificateForAad,
		// Token: 0x0400019C RID: 412
		DuplicateDropTableId,
		// Token: 0x0400019D RID: 413
		MultiplayerServerError,
		// Token: 0x0400019E RID: 414
		MultiplayerServerTooManyRequests,
		// Token: 0x0400019F RID: 415
		MultiplayerServerNoContent,
		// Token: 0x040001A0 RID: 416
		MultiplayerServerBadRequest,
		// Token: 0x040001A1 RID: 417
		MultiplayerServerUnauthorized,
		// Token: 0x040001A2 RID: 418
		MultiplayerServerForbidden,
		// Token: 0x040001A3 RID: 419
		MultiplayerServerNotFound,
		// Token: 0x040001A4 RID: 420
		MultiplayerServerConflict,
		// Token: 0x040001A5 RID: 421
		MultiplayerServerInternalServerError,
		// Token: 0x040001A6 RID: 422
		MultiplayerServerUnavailable,
		// Token: 0x040001A7 RID: 423
		ExplicitContentDetected,
		// Token: 0x040001A8 RID: 424
		PIIContentDetected,
		// Token: 0x040001A9 RID: 425
		InvalidScheduledTaskParameter,
		// Token: 0x040001AA RID: 426
		PerEntityEventRateLimitExceeded,
		// Token: 0x040001AB RID: 427
		TitleDefaultLanguageNotSet,
		// Token: 0x040001AC RID: 428
		EmailTemplateMissingDefaultVersion,
		// Token: 0x040001AD RID: 429
		FacebookInstantGamesIdNotLinked,
		// Token: 0x040001AE RID: 430
		InvalidFacebookInstantGamesSignature,
		// Token: 0x040001AF RID: 431
		FacebookInstantGamesAuthNotConfiguredForTitle,
		// Token: 0x040001B0 RID: 432
		EntityProfileConstraintValidationFailed,
		// Token: 0x040001B1 RID: 433
		TelemetryIngestionKeyPending,
		// Token: 0x040001B2 RID: 434
		TelemetryIngestionKeyNotFound,
		// Token: 0x040001B3 RID: 435
		StatisticChildNameInvalid = 1402,
		// Token: 0x040001B4 RID: 436
		DataIntegrityError,
		// Token: 0x040001B5 RID: 437
		VirtualCurrencyCannotBeSetToOlderVersion,
		// Token: 0x040001B6 RID: 438
		VirtualCurrencyMustBeWithinIntegerRange,
		// Token: 0x040001B7 RID: 439
		EmailTemplateInvalidSyntax,
		// Token: 0x040001B8 RID: 440
		EmailTemplateMissingCallback,
		// Token: 0x040001B9 RID: 441
		PushNotificationTemplateInvalidPayload,
		// Token: 0x040001BA RID: 442
		InvalidLocalizedPushNotificationLanguage,
		// Token: 0x040001BB RID: 443
		MissingLocalizedPushNotificationMessage,
		// Token: 0x040001BC RID: 444
		PushNotificationTemplateMissingPlatformPayload,
		// Token: 0x040001BD RID: 445
		PushNotificationTemplatePayloadContainsInvalidJson,
		// Token: 0x040001BE RID: 446
		PushNotificationTemplateContainsInvalidIosPayload,
		// Token: 0x040001BF RID: 447
		PushNotificationTemplateContainsInvalidAndroidPayload,
		// Token: 0x040001C0 RID: 448
		PushNotificationTemplateIosPayloadMissingNotificationBody,
		// Token: 0x040001C1 RID: 449
		PushNotificationTemplateAndroidPayloadMissingNotificationBody,
		// Token: 0x040001C2 RID: 450
		PushNotificationTemplateNotFound,
		// Token: 0x040001C3 RID: 451
		PushNotificationTemplateMissingDefaultVersion,
		// Token: 0x040001C4 RID: 452
		PushNotificationTemplateInvalidSyntax,
		// Token: 0x040001C5 RID: 453
		PushNotificationTemplateNoCustomPayloadForV1,
		// Token: 0x040001C6 RID: 454
		NoLeaderboardForStatistic,
		// Token: 0x040001C7 RID: 455
		TitleNewsMissingDefaultLanguage,
		// Token: 0x040001C8 RID: 456
		TitleNewsNotFound,
		// Token: 0x040001C9 RID: 457
		TitleNewsDuplicateLanguage,
		// Token: 0x040001CA RID: 458
		TitleNewsMissingTitleOrBody,
		// Token: 0x040001CB RID: 459
		TitleNewsInvalidLanguage,
		// Token: 0x040001CC RID: 460
		EmailRecipientBlacklisted,
		// Token: 0x040001CD RID: 461
		InvalidGameCenterAuthRequest,
		// Token: 0x040001CE RID: 462
		GameCenterAuthenticationFailed,
		// Token: 0x040001CF RID: 463
		CannotEnablePartiesForTitle,
		// Token: 0x040001D0 RID: 464
		PartyError,
		// Token: 0x040001D1 RID: 465
		PartyRequests,
		// Token: 0x040001D2 RID: 466
		PartyNoContent,
		// Token: 0x040001D3 RID: 467
		PartyBadRequest,
		// Token: 0x040001D4 RID: 468
		PartyUnauthorized,
		// Token: 0x040001D5 RID: 469
		PartyForbidden,
		// Token: 0x040001D6 RID: 470
		PartyNotFound,
		// Token: 0x040001D7 RID: 471
		PartyConflict,
		// Token: 0x040001D8 RID: 472
		PartyInternalServerError,
		// Token: 0x040001D9 RID: 473
		PartyUnavailable,
		// Token: 0x040001DA RID: 474
		PartyTooManyRequests,
		// Token: 0x040001DB RID: 475
		PushNotificationTemplateMissingName,
		// Token: 0x040001DC RID: 476
		CannotEnableMultiplayerServersForTitle,
		// Token: 0x040001DD RID: 477
		WriteAttemptedDuringExport,
		// Token: 0x040001DE RID: 478
		MultiplayerServerTitleQuotaCoresExceeded,
		// Token: 0x040001DF RID: 479
		AutomationRuleNotFound,
		// Token: 0x040001E0 RID: 480
		EntityAPIKeyLimitExceeded,
		// Token: 0x040001E1 RID: 481
		EntityAPIKeyNotFound,
		// Token: 0x040001E2 RID: 482
		EntityAPIKeyOrSecretInvalid,
		// Token: 0x040001E3 RID: 483
		EconomyServiceUnavailable,
		// Token: 0x040001E4 RID: 484
		EconomyServiceInternalError,
		// Token: 0x040001E5 RID: 485
		QueryRateLimitExceeded,
		// Token: 0x040001E6 RID: 486
		EntityAPIKeyCreationDisabledForEntity,
		// Token: 0x040001E7 RID: 487
		ForbiddenByEntityPolicy,
		// Token: 0x040001E8 RID: 488
		UpdateInventoryRateLimitExceeded,
		// Token: 0x040001E9 RID: 489
		StudioCreationRateLimited,
		// Token: 0x040001EA RID: 490
		StudioCreationInProgress,
		// Token: 0x040001EB RID: 491
		DuplicateStudioName,
		// Token: 0x040001EC RID: 492
		StudioNotFound,
		// Token: 0x040001ED RID: 493
		StudioDeleted,
		// Token: 0x040001EE RID: 494
		StudioDeactivated,
		// Token: 0x040001EF RID: 495
		StudioActivated,
		// Token: 0x040001F0 RID: 496
		TitleCreationRateLimited,
		// Token: 0x040001F1 RID: 497
		TitleCreationInProgress,
		// Token: 0x040001F2 RID: 498
		DuplicateTitleName,
		// Token: 0x040001F3 RID: 499
		TitleActivationRateLimited,
		// Token: 0x040001F4 RID: 500
		TitleActivationInProgress,
		// Token: 0x040001F5 RID: 501
		TitleDeactivated,
		// Token: 0x040001F6 RID: 502
		TitleActivated,
		// Token: 0x040001F7 RID: 503
		CloudScriptAzureFunctionsExecutionTimeLimitExceeded,
		// Token: 0x040001F8 RID: 504
		CloudScriptAzureFunctionsArgumentSizeExceeded,
		// Token: 0x040001F9 RID: 505
		CloudScriptAzureFunctionsReturnSizeExceeded,
		// Token: 0x040001FA RID: 506
		CloudScriptAzureFunctionsHTTPRequestError,
		// Token: 0x040001FB RID: 507
		VirtualCurrencyBetaGetError,
		// Token: 0x040001FC RID: 508
		VirtualCurrencyBetaCreateError,
		// Token: 0x040001FD RID: 509
		VirtualCurrencyBetaInitialDepositSaveError,
		// Token: 0x040001FE RID: 510
		VirtualCurrencyBetaSaveError,
		// Token: 0x040001FF RID: 511
		VirtualCurrencyBetaDeleteError,
		// Token: 0x04000200 RID: 512
		VirtualCurrencyBetaRestoreError,
		// Token: 0x04000201 RID: 513
		VirtualCurrencyBetaSaveConflict,
		// Token: 0x04000202 RID: 514
		VirtualCurrencyBetaUpdateError,
		// Token: 0x04000203 RID: 515
		InsightsManagementDatabaseNotFound,
		// Token: 0x04000204 RID: 516
		InsightsManagementOperationNotFound,
		// Token: 0x04000205 RID: 517
		InsightsManagementErrorPendingOperationExists,
		// Token: 0x04000206 RID: 518
		InsightsManagementSetPerformanceLevelInvalidParameter,
		// Token: 0x04000207 RID: 519
		InsightsManagementSetStorageRetentionInvalidParameter,
		// Token: 0x04000208 RID: 520
		InsightsManagementGetStorageUsageInvalidParameter,
		// Token: 0x04000209 RID: 521
		InsightsManagementGetOperationStatusInvalidParameter,
		// Token: 0x0400020A RID: 522
		DuplicatePurchaseTransactionId,
		// Token: 0x0400020B RID: 523
		EvaluationModePlayerCountExceeded,
		// Token: 0x0400020C RID: 524
		GetPlayersInSegmentRateLimitExceeded,
		// Token: 0x0400020D RID: 525
		CloudScriptFunctionNameSizeExceeded,
		// Token: 0x0400020E RID: 526
		InsightsManagementTitleInEvaluationMode,
		// Token: 0x0400020F RID: 527
		CloudScriptAzureFunctionsQueueRequestError,
		// Token: 0x04000210 RID: 528
		EvaluationModeTitleCountExceeded,
		// Token: 0x04000211 RID: 529
		InsightsManagementTitleNotInFlight,
		// Token: 0x04000212 RID: 530
		LimitNotFound,
		// Token: 0x04000213 RID: 531
		LimitNotAvailableViaAPI,
		// Token: 0x04000214 RID: 532
		InsightsManagementSetStorageRetentionBelowMinimum,
		// Token: 0x04000215 RID: 533
		InsightsManagementSetStorageRetentionAboveMaximum,
		// Token: 0x04000216 RID: 534
		AppleNotEnabledForTitle,
		// Token: 0x04000217 RID: 535
		InsightsManagementNewActiveEventExportLimitInvalid,
		// Token: 0x04000218 RID: 536
		InsightsManagementSetPerformanceRateLimited,
		// Token: 0x04000219 RID: 537
		PartyRequestsThrottledFromRateLimiter,
		// Token: 0x0400021A RID: 538
		XboxServiceTooManyRequests,
		// Token: 0x0400021B RID: 539
		NintendoSwitchNotEnabledForTitle,
		// Token: 0x0400021C RID: 540
		RequestMultiplayerServersThrottledFromRateLimiter,
		// Token: 0x0400021D RID: 541
		TitleDataOverrideNotFound,
		// Token: 0x0400021E RID: 542
		DuplicateKeys,
		// Token: 0x0400021F RID: 543
		WasNotCreatedWithCloudRoot,
		// Token: 0x04000220 RID: 544
		MatchmakingEntityInvalid = 2001,
		// Token: 0x04000221 RID: 545
		MatchmakingPlayerAttributesInvalid,
		// Token: 0x04000222 RID: 546
		MatchmakingQueueNotFound = 2016,
		// Token: 0x04000223 RID: 547
		MatchmakingMatchNotFound,
		// Token: 0x04000224 RID: 548
		MatchmakingTicketNotFound,
		// Token: 0x04000225 RID: 549
		MatchmakingAlreadyJoinedTicket = 2028,
		// Token: 0x04000226 RID: 550
		MatchmakingTicketAlreadyCompleted,
		// Token: 0x04000227 RID: 551
		MatchmakingQueueConfigInvalid = 2031,
		// Token: 0x04000228 RID: 552
		MatchmakingMemberProfileInvalid,
		// Token: 0x04000229 RID: 553
		NintendoSwitchDeviceIdNotLinked = 2034,
		// Token: 0x0400022A RID: 554
		MatchmakingNotEnabled,
		// Token: 0x0400022B RID: 555
		MatchmakingPlayerAttributesTooLarge = 2043,
		// Token: 0x0400022C RID: 556
		MatchmakingNumberOfPlayersInTicketTooLarge,
		// Token: 0x0400022D RID: 557
		MatchmakingAttributeInvalid = 2046,
		// Token: 0x0400022E RID: 558
		MatchmakingPlayerHasNotJoinedTicket = 2053,
		// Token: 0x0400022F RID: 559
		MatchmakingRateLimitExceeded,
		// Token: 0x04000230 RID: 560
		MatchmakingTicketMembershipLimitExceeded,
		// Token: 0x04000231 RID: 561
		MatchmakingUnauthorized,
		// Token: 0x04000232 RID: 562
		MatchmakingQueueLimitExceeded,
		// Token: 0x04000233 RID: 563
		MatchmakingRequestTypeMismatch,
		// Token: 0x04000234 RID: 564
		MatchmakingBadRequest,
		// Token: 0x04000235 RID: 565
		TitleConfigNotFound = 3001,
		// Token: 0x04000236 RID: 566
		TitleConfigUpdateConflict,
		// Token: 0x04000237 RID: 567
		TitleConfigSerializationError,
		// Token: 0x04000238 RID: 568
		CatalogEntityInvalid = 4001,
		// Token: 0x04000239 RID: 569
		CatalogTitleIdMissing,
		// Token: 0x0400023A RID: 570
		CatalogPlayerIdMissing,
		// Token: 0x0400023B RID: 571
		CatalogClientIdentityInvalid,
		// Token: 0x0400023C RID: 572
		CatalogOneOrMoreFilesInvalid,
		// Token: 0x0400023D RID: 573
		CatalogItemMetadataInvalid,
		// Token: 0x0400023E RID: 574
		CatalogItemIdInvalid,
		// Token: 0x0400023F RID: 575
		CatalogSearchParameterInvalid,
		// Token: 0x04000240 RID: 576
		CatalogFeatureDisabled,
		// Token: 0x04000241 RID: 577
		CatalogConfigInvalid,
		// Token: 0x04000242 RID: 578
		CatalogUnauthorized,
		// Token: 0x04000243 RID: 579
		CatalogItemTypeInvalid,
		// Token: 0x04000244 RID: 580
		CatalogBadRequest,
		// Token: 0x04000245 RID: 581
		CatalogTooManyRequests,
		// Token: 0x04000246 RID: 582
		ExportInvalidStatusUpdate = 5000,
		// Token: 0x04000247 RID: 583
		ExportInvalidPrefix,
		// Token: 0x04000248 RID: 584
		ExportBlobContainerDoesNotExist,
		// Token: 0x04000249 RID: 585
		ExportNotFound = 5004,
		// Token: 0x0400024A RID: 586
		ExportCouldNotUpdate,
		// Token: 0x0400024B RID: 587
		ExportInvalidStorageType,
		// Token: 0x0400024C RID: 588
		ExportAmazonBucketDoesNotExist,
		// Token: 0x0400024D RID: 589
		ExportInvalidBlobStorage,
		// Token: 0x0400024E RID: 590
		ExportKustoException,
		// Token: 0x0400024F RID: 591
		ExportKustoConnectionFailed = 5012,
		// Token: 0x04000250 RID: 592
		ExportUnknownError,
		// Token: 0x04000251 RID: 593
		ExportCantEditPendingExport,
		// Token: 0x04000252 RID: 594
		ExportLimitExports,
		// Token: 0x04000253 RID: 595
		ExportLimitEvents,
		// Token: 0x04000254 RID: 596
		ExportInvalidPartitionStatusModification,
		// Token: 0x04000255 RID: 597
		ExportCouldNotCreate,
		// Token: 0x04000256 RID: 598
		ExportNoBackingDatabaseFound,
		// Token: 0x04000257 RID: 599
		ExportCouldNotDelete,
		// Token: 0x04000258 RID: 600
		ExportCannotDetermineEventQuery,
		// Token: 0x04000259 RID: 601
		ExportInvalidQuerySchemaModification,
		// Token: 0x0400025A RID: 602
		ExportQuerySchemaMissingRequiredColumns,
		// Token: 0x0400025B RID: 603
		ExportCannotParseQuery,
		// Token: 0x0400025C RID: 604
		ExportControlCommandsNotAllowed,
		// Token: 0x0400025D RID: 605
		ExportQueryMissingTableReference,
		// Token: 0x0400025E RID: 606
		TitleNotEnabledForParty = 6000,
		// Token: 0x0400025F RID: 607
		PartyVersionNotFound,
		// Token: 0x04000260 RID: 608
		MultiplayerServerBuildReferencedByMatchmakingQueue,
		// Token: 0x04000261 RID: 609
		ExperimentationExperimentStopped = 7000,
		// Token: 0x04000262 RID: 610
		ExperimentationExperimentRunning,
		// Token: 0x04000263 RID: 611
		ExperimentationExperimentNotFound,
		// Token: 0x04000264 RID: 612
		ExperimentationExperimentNeverStarted,
		// Token: 0x04000265 RID: 613
		ExperimentationExperimentDeleted,
		// Token: 0x04000266 RID: 614
		ExperimentationClientTimeout,
		// Token: 0x04000267 RID: 615
		ExperimentationInvalidVariantConfiguration,
		// Token: 0x04000268 RID: 616
		ExperimentationInvalidVariableConfiguration,
		// Token: 0x04000269 RID: 617
		ExperimentInvalidId,
		// Token: 0x0400026A RID: 618
		ExperimentationNoScorecard,
		// Token: 0x0400026B RID: 619
		ExperimentationTreatmentAssignmentFailed,
		// Token: 0x0400026C RID: 620
		ExperimentationTreatmentAssignmentDisabled,
		// Token: 0x0400026D RID: 621
		ExperimentationInvalidDuration,
		// Token: 0x0400026E RID: 622
		ExperimentationMaxExperimentsReached,
		// Token: 0x0400026F RID: 623
		ExperimentationExperimentSchedulingInProgress,
		// Token: 0x04000270 RID: 624
		ExperimentationExistingCodelessScheduled,
		// Token: 0x04000271 RID: 625
		MaxActionDepthExceeded = 8000,
		// Token: 0x04000272 RID: 626
		TitleNotOnUpdatedPricingPlan = 9000,
		// Token: 0x04000273 RID: 627
		SnapshotNotFound = 11000
	}
}
