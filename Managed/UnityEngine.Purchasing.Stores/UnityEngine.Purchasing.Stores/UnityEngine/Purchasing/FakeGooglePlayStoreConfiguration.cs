using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000024 RID: 36
	public class FakeGooglePlayStoreConfiguration : IGooglePlayConfiguration, IStoreConfiguration
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003D43 File Offset: 0x00001F43
		public void SetServiceDisconnectAtInitializeListener(Action action)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003D45 File Offset: 0x00001F45
		public void SetQueryProductDetailsFailedListener(Action<int> action)
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003D47 File Offset: 0x00001F47
		public void SetDeferredPurchaseListener(Action<Product> action)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D49 File Offset: 0x00001F49
		public void SetDeferredProrationUpgradeDowngradeSubscriptionListener(Action<Product> action)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003D4B File Offset: 0x00001F4B
		public void SetObfuscatedAccountId(string accountId)
		{
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003D4D File Offset: 0x00001F4D
		public void SetObfuscatedProfileId(string profileId)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003D4F File Offset: 0x00001F4F
		public void SetFetchPurchasesAtInitialize(bool enable)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003D51 File Offset: 0x00001F51
		public void SetFetchPurchasesExcludeDeferred(bool exclude)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D53 File Offset: 0x00001F53
		public void SetMaxConnectionAttempts(int maxConnectionAttempts)
		{
		}
	}
}
