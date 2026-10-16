using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class PurchasingManager : IStoreCallback, IStoreController
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002DF0 File Offset: 0x00000FF0
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public bool useTransactionLog { get; set; }

		// Token: 0x060000CF RID: 207 RVA: 0x00002E04 File Offset: 0x00001004
		internal PurchasingManager(TransactionLog tDb, ILogger logger, IStore store, string storeName, IUnityServicesInitializationChecker unityServicesInitializationChecker, bool logUnavailableProducts)
		{
			this.m_TransactionLog = tDb;
			this.m_Store = store;
			this.m_Logger = logger;
			this.m_StoreName = storeName;
			this.useTransactionLog = true;
			this.m_logUnavailableProducts = logUnavailableProducts;
			this.m_UnityServicesInitializationChecker = unityServicesInitializationChecker;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002E56 File Offset: 0x00001056
		public void InitiatePurchase(Product product)
		{
			this.InitiatePurchase(product, string.Empty);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002E64 File Offset: 0x00001064
		[NullableContext(2)]
		public void InitiatePurchase(string productId)
		{
			this.InitiatePurchase(productId, string.Empty);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002E74 File Offset: 0x00001074
		public void InitiatePurchase([global::System.Runtime.CompilerServices.Nullable(2)] Product product, string developerPayload)
		{
			this.m_UnityServicesInitializationChecker.CheckAndLogWarning();
			if (product == null)
			{
				this.m_Logger.LogIAPWarning("Trying to purchase null Product");
				return;
			}
			if (product.availableToPurchase)
			{
				this.m_Store.Purchase(product.definition, developerPayload);
				return;
			}
			IInternalStoreListener listener = this.m_Listener;
			if (listener == null)
			{
				return;
			}
			listener.OnPurchaseFailed(product, new PurchaseFailureDescription(product.definition.id, PurchaseFailureReason.ProductUnavailable, "No products were found when fetching from the store"));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002EE4 File Offset: 0x000010E4
		public void InitiatePurchase([global::System.Runtime.CompilerServices.Nullable(2)] string purchasableId, string developerPayload)
		{
			Product product = this.products.WithID(purchasableId);
			if (product == null)
			{
				this.m_Logger.LogFormat(LogType.Warning, "Unable to purchase unknown product with id: {0}", new object[] { purchasableId });
			}
			this.InitiatePurchase(product, developerPayload);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002F24 File Offset: 0x00001124
		public void ConfirmPendingPurchase(Product product)
		{
			if (product == null)
			{
				this.m_Logger.LogIAPError("Unable to confirm purchase with null Product");
				return;
			}
			if (string.IsNullOrEmpty(product.transactionID))
			{
				this.m_Logger.LogIAPError("Unable to confirm purchase; Product has missing or empty transactionID");
				return;
			}
			if (this.useTransactionLog)
			{
				this.m_TransactionLog.Record(product.transactionID);
			}
			this.m_Store.FinishTransaction(product.definition, product.transactionID);
			if (!product.appleProductIsRestored)
			{
				IInternalStoreListener listener = this.m_Listener;
				if (listener == null)
				{
					return;
				}
				listener.SendTransactionEvent(product);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002FAC File Offset: 0x000011AC
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002FB4 File Offset: 0x000011B4
		public ProductCollection products { get; private set; }

		// Token: 0x060000D7 RID: 215 RVA: 0x00002FC0 File Offset: 0x000011C0
		public void OnPurchaseSucceeded(string id, [global::System.Runtime.CompilerServices.Nullable(2)] string receipt, string transactionId)
		{
			Product product = this.products.WithStoreSpecificID(id);
			if (product == null)
			{
				product = new Product(new ProductDefinition(id, ProductType.NonConsumable), new ProductMetadata());
			}
			this.UpdateProductReceiptAndTransactionID(product, receipt, transactionId);
			this.ProcessPurchaseIfNew(product);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002FFF File Offset: 0x000011FF
		private void UpdateProductReceiptAndTransactionID(Product product, [global::System.Runtime.CompilerServices.Nullable(2)] string receipt, string transactionId)
		{
			if (product != null)
			{
				product.receipt = this.CreateUnifiedReceipt(receipt, transactionId);
				product.transactionID = transactionId;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000301C File Offset: 0x0000121C
		public void OnAllPurchasesRetrieved(List<Product> purchasedProducts)
		{
			if (this.products != null)
			{
				Product[] all = this.products.all;
				for (int i = 0; i < all.Length; i++)
				{
					Product product = all[i];
					Product product2 = ((purchasedProducts != null) ? purchasedProducts.FirstOrDefault<Product>((Product firstPurchasedProduct) => firstPurchasedProduct.definition.id == product.definition.id) : null);
					if (product2 != null)
					{
						this.HandlePurchaseRetrieved(product, product2);
					}
					else
					{
						PurchasingManager.ClearProductReceipt(product);
					}
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003090 File Offset: 0x00001290
		internal static void OnEntitlementRevoked(Product revokedProduct)
		{
			PurchasingManager.ClearProductReceipt(revokedProduct);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003098 File Offset: 0x00001298
		private void HandlePurchaseRetrieved(Product product, Product purchasedProduct)
		{
			this.UpdateProductReceiptAndTransactionID(product, purchasedProduct.receipt, purchasedProduct.transactionID);
			if (this.initialized && !this.WasPurchaseAlreadyProcessed(purchasedProduct.transactionID))
			{
				this.ProcessPurchaseIfNew(product);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000030CA File Offset: 0x000012CA
		private bool WasPurchaseAlreadyProcessed(string transactionId)
		{
			return this.purchasesProcessedInSession.Contains(transactionId);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000030D8 File Offset: 0x000012D8
		private static void ClearProductReceipt(Product product)
		{
			product.receipt = null;
			product.transactionID = null;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000030E8 File Offset: 0x000012E8
		[Obsolete]
		public void OnSetupFailed(InitializationFailureReason reason)
		{
			this.OnSetupFailed(reason, null);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000030F4 File Offset: 0x000012F4
		[NullableContext(2)]
		public void OnSetupFailed(InitializationFailureReason reason, string message)
		{
			if (this.initialized)
			{
				Action<InitializationFailureReason> additionalProductsFailCallback = this.m_AdditionalProductsFailCallback;
				if (additionalProductsFailCallback != null)
				{
					additionalProductsFailCallback(reason);
				}
				Action<InitializationFailureReason, string> additionalProductsDetailedFailCallback = this.m_AdditionalProductsDetailedFailCallback;
				if (additionalProductsDetailedFailCallback == null)
				{
					return;
				}
				additionalProductsDetailedFailCallback(reason, message);
				return;
			}
			else
			{
				IInternalStoreListener listener = this.m_Listener;
				if (listener == null)
				{
					return;
				}
				listener.OnInitializeFailed(reason, message);
				return;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003140 File Offset: 0x00001340
		public void OnPurchaseFailed(PurchaseFailureDescription description)
		{
			if (description != null)
			{
				Product product = this.products.WithStoreSpecificID(description.productId);
				if (product == null)
				{
					this.m_Logger.LogFormat(LogType.Error, "Failed to purchase unknown product {0}", new object[] { string.Concat(new string[]
					{
						"productId:",
						description.productId,
						" reason:",
						description.reason.ToString(),
						" message:",
						description.message
					}) });
					return;
				}
				this.m_Logger.LogFormat(LogType.Warning, "onPurchaseFailedEvent({0})", new object[] { "productId:" + product.definition.id + " message:" + description.message });
				IInternalStoreListener listener = this.m_Listener;
				if (listener == null)
				{
					return;
				}
				listener.OnPurchaseFailed(product, description);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000321C File Offset: 0x0000141C
		public void OnProductsRetrieved(List<ProductDescription> products)
		{
			HashSet<Product> hashSet = new HashSet<Product>();
			foreach (ProductDescription productDescription in products)
			{
				Product product = this.products.WithStoreSpecificID(productDescription.storeSpecificId);
				if (product == null)
				{
					product = new Product(new ProductDefinition(productDescription.storeSpecificId, productDescription.storeSpecificId, productDescription.type), productDescription.metadata);
					hashSet.Add(product);
				}
				product.availableToPurchase = true;
				product.metadata = productDescription.metadata;
				product.transactionID = productDescription.transactionId;
				if (!string.IsNullOrEmpty(productDescription.receipt))
				{
					product.receipt = this.CreateUnifiedReceipt(productDescription.receipt, productDescription.transactionId);
				}
			}
			if (hashSet.Count > 0)
			{
				this.products.AddProducts(hashSet);
			}
			this.CheckForInitialization(products.Count);
			this.ProcessPurchaseOnStart();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000331C File Offset: 0x0000151C
		private string CreateUnifiedReceipt([global::System.Runtime.CompilerServices.Nullable(2)] string rawReceipt, string transactionId)
		{
			return UnifiedReceiptFormatter.FormatUnifiedReceipt(rawReceipt, transactionId, this.m_StoreName);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000332C File Offset: 0x0000152C
		private void ProcessPurchaseOnStart()
		{
			foreach (Product product in this.products.set)
			{
				if (!string.IsNullOrEmpty(product.receipt) && !string.IsNullOrEmpty(product.transactionID))
				{
					this.ProcessPurchaseIfNew(product);
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000033A0 File Offset: 0x000015A0
		[Obsolete]
		public void FetchAdditionalProducts(HashSet<ProductDefinition> additionalProducts, Action successCallback, Action<InitializationFailureReason> failCallback)
		{
			this.m_AdditionalProductsCallback = successCallback;
			this.m_AdditionalProductsFailCallback = failCallback;
			this.products.AddProducts(additionalProducts.Select<ProductDefinition, Product>((ProductDefinition x) => new Product(x, new ProductMetadata())));
			this.m_Store.RetrieveProducts(new ReadOnlyCollection<ProductDefinition>(additionalProducts.ToList<ProductDefinition>()));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003404 File Offset: 0x00001604
		public void FetchAdditionalProducts(HashSet<ProductDefinition> additionalProducts, Action successCallback, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] Action<InitializationFailureReason, string> failCallback)
		{
			this.m_AdditionalProductsCallback = successCallback;
			this.m_AdditionalProductsDetailedFailCallback = failCallback;
			this.products.AddProducts(additionalProducts.Select<ProductDefinition, Product>((ProductDefinition x) => new Product(x, new ProductMetadata())));
			this.m_Store.RetrieveProducts(new ReadOnlyCollection<ProductDefinition>(additionalProducts.ToList<ProductDefinition>()));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003468 File Offset: 0x00001668
		private void ProcessPurchaseIfNew(Product product)
		{
			if (this.HasRecordedTransaction(product.transactionID))
			{
				this.m_Store.FinishTransaction(product.definition, product.transactionID);
				return;
			}
			this.purchasesProcessedInSession.Add(product.transactionID);
			PurchaseEventArgs e = new PurchaseEventArgs(product);
			IInternalStoreListener listener = this.m_Listener;
			if (listener != null && listener.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
			{
				this.ConfirmPendingPurchase(product);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000034D3 File Offset: 0x000016D3
		private bool HasRecordedTransaction(string transactionId)
		{
			return this.useTransactionLog && this.m_TransactionLog.HasRecordOf(transactionId);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000034EC File Offset: 0x000016EC
		private void CheckForInitialization(int productCount)
		{
			if (!this.initialized)
			{
				this.initialized = true;
				if (productCount > 0 && this.HasAvailableProductsToPurchase())
				{
					IInternalStoreListener listener = this.m_Listener;
					if (listener == null)
					{
						return;
					}
					listener.OnInitialized(this);
					return;
				}
				else
				{
					string text = ((productCount == 0) ? "No product returned from the store." : "Products returned from the store don't match.");
					IInternalStoreListener listener2 = this.m_Listener;
					if (listener2 == null)
					{
						return;
					}
					listener2.OnInitializeFailed(InitializationFailureReason.NoProductsAvailable, text);
					return;
				}
			}
			else
			{
				Action additionalProductsCallback = this.m_AdditionalProductsCallback;
				if (additionalProductsCallback == null)
				{
					return;
				}
				additionalProductsCallback();
				return;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000355C File Offset: 0x0000175C
		private bool HasAvailableProductsToPurchase()
		{
			bool flag = false;
			foreach (Product product in this.products.set)
			{
				if (product.availableToPurchase)
				{
					flag = true;
				}
				else if (this.m_logUnavailableProducts)
				{
					this.m_Logger.LogFormat(LogType.Warning, "Unavailable product {0}-{1}", new object[]
					{
						product.definition.id,
						product.definition.storeSpecificId
					});
				}
			}
			return flag;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000035F8 File Offset: 0x000017F8
		public void Initialize(IInternalStoreListener listener, HashSet<ProductDefinition> products)
		{
			this.m_Listener = listener;
			this.m_Store.Initialize(this);
			Product[] array = products.Select<ProductDefinition, Product>((ProductDefinition x) => new Product(x, new ProductMetadata())).ToArray<Product>();
			this.products = new ProductCollection(array);
			ReadOnlyCollection<ProductDefinition> readOnlyCollection = new ReadOnlyCollection<ProductDefinition>(products.ToList<ProductDefinition>());
			this.m_Store.RetrieveProducts(readOnlyCollection);
		}

		// Token: 0x04000053 RID: 83
		private readonly IStore m_Store;

		// Token: 0x04000054 RID: 84
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IInternalStoreListener m_Listener;

		// Token: 0x04000055 RID: 85
		private readonly ILogger m_Logger;

		// Token: 0x04000056 RID: 86
		private readonly TransactionLog m_TransactionLog;

		// Token: 0x04000057 RID: 87
		private readonly string m_StoreName;

		// Token: 0x04000058 RID: 88
		private readonly bool m_logUnavailableProducts;

		// Token: 0x04000059 RID: 89
		private readonly IUnityServicesInitializationChecker m_UnityServicesInitializationChecker;

		// Token: 0x0400005A RID: 90
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action m_AdditionalProductsCallback;

		// Token: 0x0400005B RID: 91
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action<InitializationFailureReason> m_AdditionalProductsFailCallback;

		// Token: 0x0400005C RID: 92
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action<InitializationFailureReason, string> m_AdditionalProductsDetailedFailCallback;

		// Token: 0x0400005D RID: 93
		private readonly HashSet<string> purchasesProcessedInSession = new HashSet<string>();

		// Token: 0x04000060 RID: 96
		private bool initialized;
	}
}
