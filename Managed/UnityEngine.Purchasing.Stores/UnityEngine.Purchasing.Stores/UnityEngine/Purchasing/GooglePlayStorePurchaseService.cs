using System;
using UnityEngine.Purchasing.Interfaces;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200003B RID: 59
	internal class GooglePlayStorePurchaseService : IGooglePlayStorePurchaseService
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000498C File Offset: 0x00002B8C
		internal GooglePlayStorePurchaseService(IGooglePlayStoreService googlePlayStoreService)
		{
			this.m_GooglePlayStoreService = googlePlayStoreService;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000499B File Offset: 0x00002B9B
		public void Purchase(ProductDefinition product)
		{
			this.m_GooglePlayStoreService.Purchase(product);
		}

		// Token: 0x0400008D RID: 141
		private readonly IGooglePlayStoreService m_GooglePlayStoreService;
	}
}
