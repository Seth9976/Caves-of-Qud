using System;
using UnityEngine.Purchasing.Security;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000025 RID: 37
	public class FakeGooglePlayStoreExtensions : IGooglePlayStoreExtensions, IStoreExtension
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003D5D File Offset: 0x00001F5D
		public void UpgradeDowngradeSubscription(string oldSku, string newSku)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D5F File Offset: 0x00001F5F
		public void UpgradeDowngradeSubscription(string oldSku, string newSku, int desiredProrationMode)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003D61 File Offset: 0x00001F61
		[Obsolete("Use UpgradeDowngradeSubscription(Product oldProduct, Product newProduct, GooglePlayReplacementMode desiredReplacementMode) instead.")]
		public void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayProrationMode desiredProrationMode)
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003D63 File Offset: 0x00001F63
		public void UpgradeDowngradeSubscription(string oldSku, string newSku, GooglePlayReplacementMode desiredReplacementMode)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D65 File Offset: 0x00001F65
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		public void RestoreTransactions(Action<bool> callback)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D67 File Offset: 0x00001F67
		public void RestoreTransactions(Action<bool, string> callback)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D69 File Offset: 0x00001F69
		public void ConfirmSubscriptionPriceChange(string productId, Action<bool> callback)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003D6B File Offset: 0x00001F6B
		public bool IsPurchasedProductDeferred(Product product)
		{
			return false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003D6E File Offset: 0x00001F6E
		public GooglePurchaseState GetPurchaseState(Product product)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003D75 File Offset: 0x00001F75
		public string GetObfuscatedAccountId(Product product)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D7C File Offset: 0x00001F7C
		public string GetObfuscatedProfileId(Product product)
		{
			throw new NotImplementedException();
		}
	}
}
