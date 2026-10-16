using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Purchasing.Interfaces;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000013 RID: 19
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GoogleFinishTransactionService : IGoogleFinishTransactionService
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000265B File Offset: 0x0000085B
		internal GoogleFinishTransactionService(IGoogleBillingClient billingClient, IGoogleQueryPurchasesService googleQueryPurchasesService)
		{
			this.m_BillingClient = billingClient;
			this.m_GoogleQueryPurchasesService = googleQueryPurchasesService;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002674 File Offset: 0x00000874
		public async void FinishTransaction(ProductDefinition product, string purchaseToken, Action<IGoogleBillingResult, IGooglePurchase> onTransactionFinished)
		{
			try
			{
				IGooglePurchase googlePurchase = await this.FindPurchase(purchaseToken);
				if (googlePurchase.IsPurchased())
				{
					this.FinishTransactionForPurchase(googlePurchase, product, purchaseToken, onTransactionFinished);
				}
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026C4 File Offset: 0x000008C4
		private async Task<IGooglePurchase> FindPurchase(string purchaseToken)
		{
			return (await this.m_GoogleQueryPurchasesService.QueryPurchases()).NonNull<IGooglePurchase>().First<IGooglePurchase>((IGooglePurchase purchase) => purchase.purchaseToken == purchaseToken);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002710 File Offset: 0x00000910
		private void FinishTransactionForPurchase(IGooglePurchase purchase, ProductDefinition product, string purchaseToken, Action<IGoogleBillingResult, IGooglePurchase> onTransactionFinished)
		{
			if (product.type == ProductType.Consumable)
			{
				this.m_BillingClient.ConsumeAsync(purchaseToken, delegate(IGoogleBillingResult result)
				{
					onTransactionFinished(result, purchase);
				});
				return;
			}
			if (!purchase.IsAcknowledged())
			{
				this.m_BillingClient.AcknowledgePurchase(purchaseToken, delegate(IGoogleBillingResult result)
				{
					onTransactionFinished(result, purchase);
				});
			}
		}

		// Token: 0x04000013 RID: 19
		private readonly IGoogleBillingClient m_BillingClient;

		// Token: 0x04000014 RID: 20
		private readonly IGoogleQueryPurchasesService m_GoogleQueryPurchasesService;
	}
}
