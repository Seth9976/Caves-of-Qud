using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Uniject;
using UnityEngine.Purchasing.Default;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000092 RID: 146
	internal class WinRTStore : AbstractStore, IWindowsIAPCallback, IMicrosoftExtensions, IStoreExtension
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000ADBA File Offset: 0x00008FBA
		public WinRTStore(IWindowsIAP win8, IUtil util, ILogger logger)
		{
			this.win8 = win8;
			this.util = util;
			this.logger = logger;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000ADD7 File Offset: 0x00008FD7
		public void SetWindowsIAP(IWindowsIAP iap)
		{
			this.win8 = iap;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public override void Initialize(IStoreCallback biller)
		{
			this.callback = biller;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> productDefs)
		{
			IEnumerable<WinProductDescription> enumerable = from def in productDefs
				where def.type != ProductType.Subscription
				select new WinProductDescription(def.storeSpecificId, "$0.01", "Fake title - " + def.storeSpecificId, "Fake description - " + def.storeSpecificId, "USD", 0.01m, null, null, def.type == ProductType.Consumable);
			this.win8.BuildDummyProducts(enumerable.ToList<WinProductDescription>());
			this.init(0);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000AE5B File Offset: 0x0000905B
		public override void FinishTransaction(ProductDefinition product, string transactionId)
		{
			this.win8.FinaliseTransaction(transactionId);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000AE69 File Offset: 0x00009069
		private void init(int delay)
		{
			this.win8.Initialize(this);
			this.win8.RetrieveProducts(true);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000AE83 File Offset: 0x00009083
		public override void Purchase(ProductDefinition product, string developerPayload)
		{
			this.win8.Purchase(product.storeSpecificId);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000AE96 File Offset: 0x00009096
		public void restoreTransactions(bool pausing)
		{
			if (!pausing && this.m_CanReceivePurchases)
			{
				this.win8.RetrieveProducts(false);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000AEAF File Offset: 0x000090AF
		public void RestoreTransactions()
		{
			this.win8.RetrieveProducts(false);
			this.m_CanReceivePurchases = true;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public void logError(string error)
		{
			this.logger.LogError("Unity Purchasing", error);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000AED8 File Offset: 0x000090D8
		public void OnProductListReceived(WinProductDescription[] winProducts)
		{
			this.util.RunOnMainThread(delegate
			{
				IEnumerable<ProductDescription> enumerable = from product in winProducts
					let metadata = new ProductMetadata(product.price, product.title, product.description, product.ISOCurrencyCode, product.priceDecimal)
					select new ProductDescription(product.platformSpecificID, metadata, product.receipt, product.transactionID);
				this.callback.OnProductsRetrieved(enumerable.ToList<ProductDescription>());
			});
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000AF10 File Offset: 0x00009110
		public void log(string message)
		{
			this.util.RunOnMainThread(delegate
			{
				this.logger.Log(message);
			});
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000AF48 File Offset: 0x00009148
		public void OnPurchaseFailed(string productId, string error)
		{
			this.util.RunOnMainThread(delegate
			{
				this.logger.LogFormat(LogType.Error, "Purchase failed: {0}, {1}", new object[] { productId, error });
				if ("AlreadyPurchased" == error)
				{
					try
					{
						this.callback.OnPurchaseFailed(new PurchaseFailureDescription(productId, (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), "DuplicateTransaction"), error));
						return;
					}
					catch
					{
						this.callback.OnPurchaseFailed(new PurchaseFailureDescription(productId, (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), "Unknown"), error));
						return;
					}
				}
				if ("NotPurchased" == error)
				{
					this.callback.OnPurchaseFailed(new PurchaseFailureDescription(productId, (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), "UserCancelled"), error));
					return;
				}
				this.callback.OnPurchaseFailed(new PurchaseFailureDescription(productId, (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), "Unknown"), error));
			});
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000AF88 File Offset: 0x00009188
		public void OnPurchaseSucceeded(string productId, string receipt, string tranId)
		{
			this.util.RunOnMainThread(delegate
			{
				this.m_CanReceivePurchases = true;
				this.callback.OnPurchaseSucceeded(productId, receipt, tranId);
			});
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public void OnProductListError(string message)
		{
			this.util.RunOnMainThread(delegate
			{
				if (message.Contains("801900CC"))
				{
					this.callback.OnSetupFailed(InitializationFailureReason.AppNotKnown, null);
					return;
				}
				this.logError("Unable to retrieve product listings. UnityIAP will automatically retry...");
				this.logError(message);
				this.init(3000);
			});
		}

		// Token: 0x040001D9 RID: 473
		private IWindowsIAP win8;

		// Token: 0x040001DA RID: 474
		private IStoreCallback callback;

		// Token: 0x040001DB RID: 475
		private readonly IUtil util;

		// Token: 0x040001DC RID: 476
		private readonly ILogger logger;

		// Token: 0x040001DD RID: 477
		private bool m_CanReceivePurchases;
	}
}
