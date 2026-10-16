using System;
using UnityEngine.Purchasing.Security;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000031 RID: 49
	public interface IGooglePlayStoreExtensions : IStoreExtension
	{
		// Token: 0x060000FD RID: 253
		void UpgradeDowngradeSubscription(string oldSku, string newSku);

		// Token: 0x060000FE RID: 254
		void UpgradeDowngradeSubscription(string oldSku, string newSku, int desiredProrationMode);

		// Token: 0x060000FF RID: 255
		[Obsolete("Use UpgradeDowngradeSubscription(Product oldProduct, Product newProduct, GooglePlayReplacementMode desiredReplacementMode) instead.")]
		void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayProrationMode desiredProrationMode);

		// Token: 0x06000100 RID: 256
		void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayReplacementMode desiredReplacementMode);

		// Token: 0x06000101 RID: 257
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		void RestoreTransactions(Action<bool> callback);

		// Token: 0x06000102 RID: 258
		void RestoreTransactions(Action<bool, string> callback);

		// Token: 0x06000103 RID: 259
		[Obsolete("No longer functional. This calls launchPriceChangeConfirmationFlow which was removed from Google Play Billing Library 6.0.0")]
		void ConfirmSubscriptionPriceChange(string productId, Action<bool> callback);

		// Token: 0x06000104 RID: 260
		bool IsPurchasedProductDeferred(Product product);

		// Token: 0x06000105 RID: 261
		GooglePurchaseState GetPurchaseState(Product product);

		// Token: 0x06000106 RID: 262
		string GetObfuscatedAccountId(Product product);

		// Token: 0x06000107 RID: 263
		string GetObfuscatedProfileId(Product product);
	}
}
