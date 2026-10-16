using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002F RID: 47
	public interface IGooglePlayConfiguration : IStoreConfiguration
	{
		// Token: 0x060000EE RID: 238
		void SetServiceDisconnectAtInitializeListener(Action action);

		// Token: 0x060000EF RID: 239
		void SetQueryProductDetailsFailedListener(Action<int> action);

		// Token: 0x060000F0 RID: 240
		void SetDeferredPurchaseListener(Action<Product> action);

		// Token: 0x060000F1 RID: 241
		void SetDeferredProrationUpgradeDowngradeSubscriptionListener(Action<Product> action);

		// Token: 0x060000F2 RID: 242
		void SetMaxConnectionAttempts(int maxConnectionAttempts);

		// Token: 0x060000F3 RID: 243
		void SetObfuscatedAccountId(string accountId);

		// Token: 0x060000F4 RID: 244
		void SetObfuscatedProfileId(string profileId);

		// Token: 0x060000F5 RID: 245
		void SetFetchPurchasesAtInitialize(bool enable);

		// Token: 0x060000F6 RID: 246
		void SetFetchPurchasesExcludeDeferred(bool exclude);
	}
}
