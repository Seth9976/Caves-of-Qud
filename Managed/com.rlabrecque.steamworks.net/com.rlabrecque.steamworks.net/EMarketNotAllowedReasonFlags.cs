using System;

namespace Steamworks
{
	// Token: 0x02000149 RID: 329
	[Flags]
	public enum EMarketNotAllowedReasonFlags
	{
		// Token: 0x040008FA RID: 2298
		k_EMarketNotAllowedReason_None = 0,
		// Token: 0x040008FB RID: 2299
		k_EMarketNotAllowedReason_TemporaryFailure = 1,
		// Token: 0x040008FC RID: 2300
		k_EMarketNotAllowedReason_AccountDisabled = 2,
		// Token: 0x040008FD RID: 2301
		k_EMarketNotAllowedReason_AccountLockedDown = 4,
		// Token: 0x040008FE RID: 2302
		k_EMarketNotAllowedReason_AccountLimited = 8,
		// Token: 0x040008FF RID: 2303
		k_EMarketNotAllowedReason_TradeBanned = 16,
		// Token: 0x04000900 RID: 2304
		k_EMarketNotAllowedReason_AccountNotTrusted = 32,
		// Token: 0x04000901 RID: 2305
		k_EMarketNotAllowedReason_SteamGuardNotEnabled = 64,
		// Token: 0x04000902 RID: 2306
		k_EMarketNotAllowedReason_SteamGuardOnlyRecentlyEnabled = 128,
		// Token: 0x04000903 RID: 2307
		k_EMarketNotAllowedReason_RecentPasswordReset = 256,
		// Token: 0x04000904 RID: 2308
		k_EMarketNotAllowedReason_NewPaymentMethod = 512,
		// Token: 0x04000905 RID: 2309
		k_EMarketNotAllowedReason_InvalidCookie = 1024,
		// Token: 0x04000906 RID: 2310
		k_EMarketNotAllowedReason_UsingNewDevice = 2048,
		// Token: 0x04000907 RID: 2311
		k_EMarketNotAllowedReason_RecentSelfRefund = 4096,
		// Token: 0x04000908 RID: 2312
		k_EMarketNotAllowedReason_NewPaymentMethodCannotBeVerified = 8192,
		// Token: 0x04000909 RID: 2313
		k_EMarketNotAllowedReason_NoRecentPurchases = 16384,
		// Token: 0x0400090A RID: 2314
		k_EMarketNotAllowedReason_AcceptedWalletGift = 32768
	}
}
