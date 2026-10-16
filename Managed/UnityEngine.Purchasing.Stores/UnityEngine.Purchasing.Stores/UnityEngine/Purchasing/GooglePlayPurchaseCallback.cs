using System;
using System.Runtime.CompilerServices;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002A RID: 42
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GooglePlayPurchaseCallback : IGooglePurchaseCallback
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00003EE5 File Offset: 0x000020E5
		public GooglePlayPurchaseCallback(IUtil util)
		{
			this.m_Util = util;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003EF4 File Offset: 0x000020F4
		public void SetStoreCallback(IStoreCallback storeCallback)
		{
			this.m_StoreCallback = storeCallback;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003EFD File Offset: 0x000020FD
		public void SetStoreConfiguration(IGooglePlayConfigurationInternal configuration)
		{
			this.m_GooglePlayConfigurationInternal = configuration;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003F06 File Offset: 0x00002106
		public void OnPurchaseSuccessful(IGooglePurchase purchase, string receipt, string purchaseToken)
		{
			IStoreCallback storeCallback = this.m_StoreCallback;
			if (storeCallback == null)
			{
				return;
			}
			storeCallback.OnPurchaseSucceeded(purchase.sku ?? string.Empty, receipt, purchaseToken);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003F29 File Offset: 0x00002129
		public void OnPurchaseFailed(PurchaseFailureDescription purchaseFailureDescription)
		{
			IStoreCallback storeCallback = this.m_StoreCallback;
			if (storeCallback == null)
			{
				return;
			}
			storeCallback.OnPurchaseFailed(purchaseFailureDescription);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003F3C File Offset: 0x0000213C
		public void NotifyDeferredPurchase(IGooglePurchase purchase, string receipt, string purchaseToken)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				IGooglePlayConfigurationInternal googlePlayConfigurationInternal = this.m_GooglePlayConfigurationInternal;
				if (googlePlayConfigurationInternal == null)
				{
					return;
				}
				googlePlayConfigurationInternal.NotifyDeferredPurchase(this.m_StoreCallback, purchase, receipt, purchaseToken);
			});
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003F84 File Offset: 0x00002184
		public void NotifyDeferredProrationUpgradeDowngradeSubscription(string sku)
		{
			this.m_Util.RunOnMainThread(delegate
			{
				IGooglePlayConfigurationInternal googlePlayConfigurationInternal = this.m_GooglePlayConfigurationInternal;
				if (googlePlayConfigurationInternal == null)
				{
					return;
				}
				googlePlayConfigurationInternal.NotifyDeferredProrationUpgradeDowngradeSubscription(this.m_StoreCallback, sku);
			});
		}

		// Token: 0x0400005E RID: 94
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IStoreCallback m_StoreCallback;

		// Token: 0x0400005F RID: 95
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IGooglePlayConfigurationInternal m_GooglePlayConfigurationInternal;

		// Token: 0x04000060 RID: 96
		private readonly IUtil m_Util;
	}
}
