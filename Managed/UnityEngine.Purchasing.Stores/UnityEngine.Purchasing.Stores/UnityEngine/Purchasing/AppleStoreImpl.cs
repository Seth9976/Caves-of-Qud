using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AOT;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing.Telemetry;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000059 RID: 89
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class AppleStoreImpl : JSONStore, IAppleExtensions, IStoreExtension, IAppleConfiguration, IStoreConfiguration
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00006430 File Offset: 0x00004630
		protected AppleStoreImpl(IUtil util, ITelemetryDiagnostics telemetryDiagnostics)
		{
			AppleStoreImpl.s_Util = util;
			AppleStoreImpl.s_Instance = this;
			this.m_TelemetryDiagnostics = telemetryDiagnostics;
			this.m_ProductDescriptionsDeserializer = new AppleJsonProductDescriptionsDeserializer();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006456 File Offset: 0x00004656
		public void SetNativeStore(INativeAppleStore apple)
		{
			base.SetNativeStore(apple);
			this.m_Native = apple;
			apple.SetUnityPurchasingCallback(new UnityPurchasingCallback(AppleStoreImpl.MessageCallback));
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006478 File Offset: 0x00004678
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public string appReceipt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				double? appReceiptModificationDate = this.appReceiptModificationDate;
				if (!this.m_CachedAppReceiptModificationDate.Equals(appReceiptModificationDate))
				{
					INativeAppleStore native = this.m_Native;
					this.m_CachedAppReceiptModificationDate = ((native != null) ? new double?(native.appReceiptModificationDate) : null);
					INativeAppleStore native2 = this.m_Native;
					this.m_CachedAppReceipt = ((native2 != null) ? native2.appReceipt : null);
				}
				return this.m_CachedAppReceipt;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000064E8 File Offset: 0x000046E8
		private double? appReceiptModificationDate
		{
			get
			{
				INativeAppleStore native = this.m_Native;
				if (native == null)
				{
					return null;
				}
				return new double?(native.appReceiptModificationDate);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006514 File Offset: 0x00004714
		public bool canMakePayments
		{
			get
			{
				INativeAppleStore native = this.m_Native;
				return native != null && native.canMakePayments;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006533 File Offset: 0x00004733
		public void SetApplePromotionalPurchaseInterceptorCallback(Action<Product> callback)
		{
			this.m_PromotionalPurchaseCallback = callback;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000653C File Offset: 0x0000473C
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000655B File Offset: 0x0000475B
		public bool simulateAskToBuy
		{
			get
			{
				INativeAppleStore native = this.m_Native;
				return native != null && native.simulateAskToBuy;
			}
			set
			{
				if (this.m_Native != null)
				{
					this.m_Native.simulateAskToBuy = value;
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006571 File Offset: 0x00004771
		public virtual void FetchStorePromotionOrder(Action<List<Product>> successCallback, Action errorCallback)
		{
			this.m_FetchStorePromotionOrderError = errorCallback;
			this.m_FetchStorePromotionOrderSuccess = successCallback;
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.FetchStorePromotionOrder();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006591 File Offset: 0x00004791
		public virtual void FetchStorePromotionVisibility(Product product, Action<string, AppleStorePromotionVisibility> successCallback, Action errorCallback)
		{
			this.m_FetchStorePromotionVisibilityError = errorCallback;
			this.m_FetchStorePromotionVisibilitySuccess = successCallback;
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.FetchStorePromotionVisibility(product.definition.id);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000065BC File Offset: 0x000047BC
		public virtual void SetStorePromotionOrder(List<Product> products)
		{
			List<string> list = new List<string>();
			foreach (Product product in products)
			{
				if (product != null && !string.IsNullOrEmpty(product.definition.storeSpecificId))
				{
					list.Add(product.definition.storeSpecificId);
				}
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object> { { "products", list } };
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.SetStorePromotionOrder(MiniJson.JsonEncode(dictionary));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006658 File Offset: 0x00004858
		public void SetStorePromotionVisibility(Product product, AppleStorePromotionVisibility visibility)
		{
			if (product == null)
			{
				ArgumentNullException ex = new ArgumentNullException("product");
				this.m_TelemetryDiagnostics.SendDiagnostic("invalid_product_error", ex);
				throw ex;
			}
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.SetStorePromotionVisibility(product.definition.storeSpecificId, visibility.ToString());
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000066AE File Offset: 0x000048AE
		public string GetTransactionReceiptForProduct(Product product)
		{
			INativeAppleStore native = this.m_Native;
			return ((native != null) ? native.GetTransactionReceiptForProductId(product.definition.storeSpecificId) : null) ?? string.Empty;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000066D6 File Offset: 0x000048D6
		public void SetApplicationUsername(string applicationUsername)
		{
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.SetApplicationUsername(applicationUsername);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000066EC File Offset: 0x000048EC
		public override void OnProductsRetrieved(string json)
		{
			List<ProductDescription> list = this.m_ProductDescriptionsDeserializer.DeserializeProductDescriptions(json);
			List<ProductDescription> list2 = null;
			this.m_ProductsJson = json;
			AppleReceipt appleReceiptFromBase64String = this.GetAppleReceiptFromBase64String(this.appReceipt);
			if (this.HasInAppPurchaseReceipts(appleReceiptFromBase64String))
			{
				list2 = this.EnrichProductDescriptions(list, appleReceiptFromBase64String);
			}
			this.unity.OnProductsRetrieved(list2 ?? list);
			if (this.m_PromotionalPurchaseCallback != null)
			{
				INativeAppleStore native = this.m_Native;
				if (native != null)
				{
					native.InterceptPromotionalPurchases();
				}
			}
			INativeAppleStore native2 = this.m_Native;
			if (native2 == null)
			{
				return;
			}
			native2.AddTransactionObserver();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006768 File Offset: 0x00004968
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool HasInAppPurchaseReceipts(AppleReceipt appleReceipt)
		{
			if (appleReceipt != null)
			{
				AppleInAppPurchaseReceipt[] inAppPurchaseReceipts = appleReceipt.inAppPurchaseReceipts;
				return inAppPurchaseReceipts != null && inAppPurchaseReceipts.Length != 0;
			}
			return false;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006780 File Offset: 0x00004980
		private List<ProductDescription> EnrichProductDescriptions(List<ProductDescription> productDescriptions, AppleReceipt appleReceipt)
		{
			List<ProductDescription> list = new List<ProductDescription>();
			foreach (ProductDescription productDescription in productDescriptions)
			{
				AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = AppleStoreImpl.FindMostRecentReceipt(appleReceipt, productDescription.storeSpecificId);
				if (appleInAppPurchaseReceipt == null)
				{
					list.Add(productDescription);
				}
				else
				{
					AppleStoreProductType appleStoreProductType = (AppleStoreProductType)Enum.Parse(typeof(AppleStoreProductType), appleInAppPurchaseReceipt.productType.ToString());
					if (appleStoreProductType == AppleStoreProductType.AutoRenewingSubscription)
					{
						if (new SubscriptionInfo(appleInAppPurchaseReceipt, null).isExpired() == Result.True)
						{
							list.Add(productDescription);
						}
						else
						{
							list.Add(new ProductDescription(productDescription.storeSpecificId, productDescription.metadata, this.appReceipt, appleInAppPurchaseReceipt.transactionID));
							this.UpdateAppleProductFields(productDescription.storeSpecificId, appleInAppPurchaseReceipt.originalTransactionIdentifier, true);
						}
					}
					else if (appleStoreProductType == AppleStoreProductType.Consumable)
					{
						list.Add(productDescription);
					}
					else
					{
						list.Add(new ProductDescription(productDescription.storeSpecificId, productDescription.metadata, this.appReceipt, appleInAppPurchaseReceipt.transactionID));
						this.UpdateAppleProductFields(productDescription.storeSpecificId, appleInAppPurchaseReceipt.originalTransactionIdentifier, true);
					}
				}
			}
			return list;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000068B0 File Offset: 0x00004AB0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static AppleInAppPurchaseReceipt FindMostRecentReceipt(AppleReceipt appleReceipt, [global::System.Runtime.CompilerServices.Nullable(1)] string productId)
		{
			if (appleReceipt == null)
			{
				return null;
			}
			AppleInAppPurchaseReceipt[] array = Array.FindAll<AppleInAppPurchaseReceipt>(appleReceipt.inAppPurchaseReceipts, (AppleInAppPurchaseReceipt r) => r.productID == productId);
			Array.Sort<AppleInAppPurchaseReceipt>(array, (AppleInAppPurchaseReceipt b, AppleInAppPurchaseReceipt a) => a.purchaseDate.CompareTo(b.purchaseDate));
			return AppleStoreImpl.FirstNonCancelledReceipt(array);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006910 File Offset: 0x00004B10
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static AppleInAppPurchaseReceipt FirstNonCancelledReceipt(AppleInAppPurchaseReceipt[] foundReceipts)
		{
			foreach (AppleInAppPurchaseReceipt appleInAppPurchaseReceipt in foundReceipts)
			{
				if (appleInAppPurchaseReceipt.cancellationDate == DateTime.MinValue)
				{
					return appleInAppPurchaseReceipt;
				}
			}
			return null;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006946 File Offset: 0x00004B46
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[Obsolete("RestoreTransactions(Action<bool> callback) is deprecated, please use RestoreTransactions(Action<bool, string> callback) instead.")]
		public virtual void RestoreTransactions(Action<bool> callback)
		{
			this.m_ObsoleteRestoreCallback = callback;
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.RestoreTransactions();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000695F File Offset: 0x00004B5F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public virtual void RestoreTransactions(Action<bool, string> callback)
		{
			this.m_RestoreCallback = callback;
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.RestoreTransactions();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006978 File Offset: 0x00004B78
		public virtual void RefreshAppReceipt(Action<string> successCallback, Action<string> errorCallback)
		{
			this.m_RefreshReceiptSuccess = successCallback;
			this.m_RefreshReceiptError = errorCallback;
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.RefreshAppReceipt();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006998 File Offset: 0x00004B98
		public virtual void RefreshAppReceipt(Action<string> successCallback, Action errorCallback)
		{
			this.m_RefreshReceiptSuccess = successCallback;
			this.m_RefreshReceiptError = delegate(string _)
			{
				errorCallback();
			};
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.RefreshAppReceipt();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000069DB File Offset: 0x00004BDB
		public void RegisterPurchaseDeferredListener(Action<Product> callback)
		{
			this.m_DeferredCallback = callback;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000069E4 File Offset: 0x00004BE4
		public void SetEntitlementsRevokedListener(Action<List<Product>> callback)
		{
			this.m_RevokedCallback = callback;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000069ED File Offset: 0x00004BED
		public virtual void ContinuePromotionalPurchases()
		{
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.ContinuePromotionalPurchases();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000069FF File Offset: 0x00004BFF
		public Dictionary<string, string> GetIntroductoryPriceDictionary()
		{
			return JSONSerializer.DeserializeSubscriptionDescriptions(this.m_ProductsJson);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006A0C File Offset: 0x00004C0C
		public Dictionary<string, string> GetProductDetails()
		{
			return JSONSerializer.DeserializeProductDetails(this.m_ProductsJson);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006A19 File Offset: 0x00004C19
		public virtual void PresentCodeRedemptionSheet()
		{
			INativeAppleStore native = this.m_Native;
			if (native == null)
			{
				return;
			}
			native.PresentCodeRedemptionSheet();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006A2C File Offset: 0x00004C2C
		public void OnPurchaseDeferred(string productId)
		{
			if (this.m_DeferredCallback != null)
			{
				Product product = this.unity.products.WithStoreSpecificID(productId);
				if (product != null)
				{
					this.m_DeferredCallback(product);
				}
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006A64 File Offset: 0x00004C64
		public void OnPromotionalPurchaseAttempted(string productId)
		{
			if (this.m_PromotionalPurchaseCallback != null)
			{
				Product product = this.unity.products.WithStoreSpecificID(productId);
				if (product != null)
				{
					this.m_PromotionalPurchaseCallback(product);
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006A9A File Offset: 0x00004C9A
		public void OnTransactionsRestoredSuccess()
		{
			Action<bool> obsoleteRestoreCallback = this.m_ObsoleteRestoreCallback;
			if (obsoleteRestoreCallback != null)
			{
				obsoleteRestoreCallback(true);
			}
			Action<bool, string> restoreCallback = this.m_RestoreCallback;
			if (restoreCallback == null)
			{
				return;
			}
			restoreCallback(true, null);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public void OnTransactionsRestoredFail(string error)
		{
			Action<bool> obsoleteRestoreCallback = this.m_ObsoleteRestoreCallback;
			if (obsoleteRestoreCallback != null)
			{
				obsoleteRestoreCallback(false);
			}
			Action<bool, string> restoreCallback = this.m_RestoreCallback;
			if (restoreCallback == null)
			{
				return;
			}
			restoreCallback(false, error);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006AE6 File Offset: 0x00004CE6
		public void OnAppReceiptRetrieved(string receipt)
		{
			Action<string> refreshReceiptSuccess = this.m_RefreshReceiptSuccess;
			if (refreshReceiptSuccess == null)
			{
				return;
			}
			refreshReceiptSuccess(receipt);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006AF9 File Offset: 0x00004CF9
		public void OnAppReceiptRefreshedFailed(string error)
		{
			Action<string> refreshReceiptError = this.m_RefreshReceiptError;
			if (refreshReceiptError == null)
			{
				return;
			}
			refreshReceiptError(error);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006B0C File Offset: 0x00004D0C
		private void OnEntitlementsRevoked(string productIds)
		{
			List<Product> list = new List<Product>();
			AppleReceipt appleReceiptFromBase64String = this.GetAppleReceiptFromBase64String(this.appReceipt);
			foreach (object obj in productIds.ArrayListFromJson())
			{
				string text = (string)obj;
				Product product = this.unity.products.WithStoreSpecificID(text);
				if (product != null)
				{
					this.RevokeEntitlement(appleReceiptFromBase64String, text, list, product);
				}
			}
			Action<List<Product>> revokedCallback = this.m_RevokedCallback;
			if (revokedCallback == null)
			{
				return;
			}
			revokedCallback(list);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006BA4 File Offset: 0x00004DA4
		private void RevokeEntitlement([global::System.Runtime.CompilerServices.Nullable(2)] AppleReceipt appleReceipt, string productId, List<Product> revokedProducts, Product product)
		{
			if (this.HasInAppPurchaseReceipts(appleReceipt) && this.RestoreActiveEntitlement(appleReceipt, productId))
			{
				return;
			}
			revokedProducts.Add(product);
			PurchasingManager.OnEntitlementRevoked(product);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006BCC File Offset: 0x00004DCC
		private bool RestoreActiveEntitlement(AppleReceipt appleReceipt, string productId)
		{
			AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = AppleStoreImpl.FindMostRecentReceipt(appleReceipt, productId);
			if (appleInAppPurchaseReceipt != null)
			{
				this.UpdateAppleProductFields(productId, appleInAppPurchaseReceipt.originalTransactionIdentifier, true);
				this.unity.OnPurchaseSucceeded(productId, this.appReceipt, appleInAppPurchaseReceipt.transactionID);
				return true;
			}
			return false;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006C10 File Offset: 0x00004E10
		public void OnFetchStorePromotionOrderSucceeded(string productIds)
		{
			if (this.m_FetchStorePromotionOrderSuccess != null)
			{
				List<object> list = productIds.ArrayListFromJson();
				List<Product> list2 = new List<Product>();
				foreach (object obj in list)
				{
					Product product = this.unity.products.WithStoreSpecificID(obj as string);
					list2.Add(product);
				}
				this.m_FetchStorePromotionOrderSuccess(list2);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006C94 File Offset: 0x00004E94
		public void OnFetchStorePromotionOrderFailed()
		{
			Action fetchStorePromotionOrderError = this.m_FetchStorePromotionOrderError;
			if (fetchStorePromotionOrderError == null)
			{
				return;
			}
			fetchStorePromotionOrderError();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006CA8 File Offset: 0x00004EA8
		public void OnFetchStorePromotionVisibilitySucceeded(string result)
		{
			if (this.m_FetchStorePromotionVisibilitySuccess != null)
			{
				Dictionary<string, object> dictionary = Json.Deserialize(result) as Dictionary<string, object>;
				object obj;
				if (dictionary == null)
				{
					obj = null;
				}
				else
				{
					obj = dictionary.ToDictionary<KeyValuePair<string, object>, string, string>((KeyValuePair<string, object> k) => k.Key, (KeyValuePair<string, object> k) => k.Value.ToString());
				}
				object obj2 = obj;
				string text = ((obj2 != null) ? obj2["productId"] : null);
				AppleStorePromotionVisibility appleStorePromotionVisibility;
				Enum.TryParse<AppleStorePromotionVisibility>((obj2 != null) ? obj2["visibility"] : null, out appleStorePromotionVisibility);
				if (text != null)
				{
					this.m_FetchStorePromotionVisibilitySuccess(text, appleStorePromotionVisibility);
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006D51 File Offset: 0x00004F51
		public void OnFetchStorePromotionVisibilityFailed()
		{
			Action fetchStorePromotionVisibilityError = this.m_FetchStorePromotionVisibilityError;
			if (fetchStorePromotionVisibilityError == null)
			{
				return;
			}
			fetchStorePromotionVisibilityError();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006D64 File Offset: 0x00004F64
		[MonoPInvokeCallback(typeof(UnityPurchasingCallback))]
		private static void MessageCallback(string subject, string payload, string receipt, string transactionId, string originalTransactionId, bool isRestored)
		{
			IUtil util = AppleStoreImpl.s_Util;
			if (util == null)
			{
				return;
			}
			util.RunOnMainThread(delegate
			{
				AppleStoreImpl appleStoreImpl = AppleStoreImpl.s_Instance;
				if (appleStoreImpl == null)
				{
					return;
				}
				appleStoreImpl.ProcessMessage(subject, payload, receipt, transactionId, originalTransactionId, isRestored);
			});
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006DC0 File Offset: 0x00004FC0
		private void ProcessMessage(string subject, string payload, string receipt, string transactionId, string originalTransactionId, bool isRestored)
		{
			if (string.IsNullOrEmpty(receipt))
			{
				receipt = this.appReceipt ?? "";
			}
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(subject);
			if (num <= 1674652619U)
			{
				if (num <= 1093263071U)
				{
					if (num != 18839595U)
					{
						if (num != 430974421U)
						{
							if (num != 1093263071U)
							{
								return;
							}
							if (!(subject == "onTransactionsRestoredFail"))
							{
								return;
							}
							this.OnTransactionsRestoredFail(payload);
							return;
						}
						else
						{
							if (!(subject == "onPromotionalPurchaseAttempted"))
							{
								return;
							}
							this.OnPromotionalPurchaseAttempted(payload);
							return;
						}
					}
					else
					{
						if (!(subject == "onFetchStorePromotionVisibilitySucceeded"))
						{
							return;
						}
						this.OnFetchStorePromotionVisibilitySucceeded(payload);
						return;
					}
				}
				else if (num <= 1516251567U)
				{
					if (num != 1372923298U)
					{
						if (num != 1516251567U)
						{
							return;
						}
						if (!(subject == "onFetchStorePromotionOrderFailed"))
						{
							return;
						}
						this.OnFetchStorePromotionOrderFailed();
						return;
					}
					else
					{
						if (!(subject == "onEntitlementsRevoked"))
						{
							return;
						}
						this.OnEntitlementsRevoked(payload);
						return;
					}
				}
				else if (num != 1523640433U)
				{
					if (num != 1674652619U)
					{
						return;
					}
					if (!(subject == "onProductPurchaseDeferred"))
					{
						return;
					}
					this.OnPurchaseDeferred(payload);
					return;
				}
				else
				{
					if (!(subject == "onAppReceiptRefreshFailed"))
					{
						return;
					}
					this.OnAppReceiptRefreshedFailed(payload);
					return;
				}
			}
			else if (num <= 3622431981U)
			{
				if (num <= 3320864102U)
				{
					if (num != 2411276062U)
					{
						if (num != 3320864102U)
						{
							return;
						}
						if (!(subject == "OnPurchaseFailed"))
						{
							return;
						}
						base.OnPurchaseFailed(payload);
						return;
					}
					else
					{
						if (!(subject == "onTransactionsRestoredSuccess"))
						{
							return;
						}
						this.OnTransactionsRestoredSuccess();
						return;
					}
				}
				else if (num != 3385076384U)
				{
					if (num != 3622431981U)
					{
						return;
					}
					if (!(subject == "onAppReceiptRefreshed"))
					{
						return;
					}
					this.OnAppReceiptRetrieved(payload);
					return;
				}
				else
				{
					if (!(subject == "OnPurchaseSucceeded"))
					{
						return;
					}
					this.OnPurchaseSucceeded(payload, receipt, transactionId, originalTransactionId, isRestored);
					return;
				}
			}
			else if (num <= 3750884092U)
			{
				if (num != 3685547548U)
				{
					if (num != 3750884092U)
					{
						return;
					}
					if (!(subject == "OnSetupFailed"))
					{
						return;
					}
					base.OnSetupFailed(payload);
					return;
				}
				else
				{
					if (!(subject == "OnProductsRetrieved"))
					{
						return;
					}
					this.OnProductsRetrieved(payload);
					return;
				}
			}
			else if (num != 4220197767U)
			{
				if (num != 4288445955U)
				{
					return;
				}
				if (!(subject == "onFetchStorePromotionOrderSucceeded"))
				{
					return;
				}
				this.OnFetchStorePromotionOrderSucceeded(payload);
				return;
			}
			else
			{
				if (!(subject == "onFetchStorePromotionVisibilityFailed"))
				{
					return;
				}
				this.OnFetchStorePromotionVisibilityFailed();
				return;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007030 File Offset: 0x00005230
		public void OnPurchaseSucceeded(string id, string receipt, string transactionId, string originalTransactionId, bool isRestored)
		{
			AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = AppleStoreImpl.FindMostRecentReceipt(this.GetAppleReceiptFromBase64String(receipt), id);
			if (AppleStoreImpl.IsValidPurchaseState(appleInAppPurchaseReceipt, id))
			{
				isRestored = isRestored || this.IsRestored(id, appleInAppPurchaseReceipt, transactionId, originalTransactionId);
				this.UpdateAppleProductFields(id, originalTransactionId, isRestored);
				base.OnPurchaseSucceeded(id, receipt, transactionId);
				return;
			}
			base.FinishTransaction(null, transactionId);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007088 File Offset: 0x00005288
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private AppleReceipt GetAppleReceiptFromBase64String(string receipt)
		{
			AppleReceipt appleReceipt = null;
			if (!string.IsNullOrEmpty(receipt))
			{
				AppleReceiptParser appleReceiptParser = new AppleReceiptParser();
				try
				{
					appleReceipt = appleReceiptParser.Parse(Convert.FromBase64String(receipt));
				}
				catch (Exception ex)
				{
					this.m_TelemetryDiagnostics.SendDiagnostic("parse_receipt_transaction_error", ex);
				}
			}
			return appleReceipt;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000070DC File Offset: 0x000052DC
		private static bool IsValidPurchaseState([global::System.Runtime.CompilerServices.Nullable(2)] AppleInAppPurchaseReceipt mostRecentReceipt, string productId)
		{
			bool flag = true;
			if (mostRecentReceipt != null && (AppleStoreProductType)Enum.Parse(typeof(AppleStoreProductType), mostRecentReceipt.productType.ToString()) == AppleStoreProductType.AutoRenewingSubscription && new SubscriptionInfo(mostRecentReceipt, null).isExpired() == Result.True)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007124 File Offset: 0x00005324
		private bool IsRestored(string productId, [global::System.Runtime.CompilerServices.Nullable(2)] AppleInAppPurchaseReceipt productReceipt, string transactionId, string originalTransactionId)
		{
			Product product = this.unity.products.WithStoreSpecificID(productId);
			return product != null && ((product.definition.type == ProductType.Subscription) ? AppleStoreImpl.IsSubscriptionRestored(productReceipt, product) : AppleStoreImpl.IsNonSubscriptionRestored(transactionId, originalTransactionId));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000716C File Offset: 0x0000536C
		private static bool IsSubscriptionRestored([global::System.Runtime.CompilerServices.Nullable(2)] AppleInAppPurchaseReceipt productReceipt, Product previousProduct)
		{
			bool flag = false;
			if (previousProduct.hasReceipt)
			{
				DateTime? dateTime = ((productReceipt != null) ? new DateTime?(productReceipt.subscriptionExpirationDate) : null);
				SubscriptionInfo subscriptionInfo = new SubscriptionManager(previousProduct, null).getSubscriptionInfo();
				if (subscriptionInfo != null && subscriptionInfo.isCancelled() == Result.False && subscriptionInfo.getExpireDate() >= dateTime)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000071DF File Offset: 0x000053DF
		private static bool IsNonSubscriptionRestored(string transactionId, [global::System.Runtime.CompilerServices.Nullable(2)] string originalTransactionId)
		{
			return originalTransactionId != null && originalTransactionId != transactionId;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000071F0 File Offset: 0x000053F0
		private void UpdateAppleProductFields(string id, string originalTransactionId, bool isRestored)
		{
			Product product = this.unity.products.WithStoreSpecificID(id);
			if (product != null)
			{
				product.appleProductIsRestored = isRestored;
				product.appleOriginalTransactionID = originalTransactionId;
			}
		}

		// Token: 0x040000E1 RID: 225
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<Product> m_DeferredCallback;

		// Token: 0x040000E2 RID: 226
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private Action<List<Product>> m_RevokedCallback;

		// Token: 0x040000E3 RID: 227
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<string> m_RefreshReceiptError;

		// Token: 0x040000E4 RID: 228
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<string> m_RefreshReceiptSuccess;

		// Token: 0x040000E5 RID: 229
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action<bool> m_ObsoleteRestoreCallback;

		// Token: 0x040000E6 RID: 230
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action<bool, string> m_RestoreCallback;

		// Token: 0x040000E7 RID: 231
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action m_FetchStorePromotionOrderError;

		// Token: 0x040000E8 RID: 232
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private Action<List<Product>> m_FetchStorePromotionOrderSuccess;

		// Token: 0x040000E9 RID: 233
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<Product> m_PromotionalPurchaseCallback;

		// Token: 0x040000EA RID: 234
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Action m_FetchStorePromotionVisibilityError;

		// Token: 0x040000EB RID: 235
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private Action<string, AppleStorePromotionVisibility> m_FetchStorePromotionVisibilitySuccess;

		// Token: 0x040000EC RID: 236
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private INativeAppleStore m_Native;

		// Token: 0x040000ED RID: 237
		private readonly ITelemetryDiagnostics m_TelemetryDiagnostics;

		// Token: 0x040000EE RID: 238
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private static IUtil s_Util;

		// Token: 0x040000EF RID: 239
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private static AppleStoreImpl s_Instance;

		// Token: 0x040000F0 RID: 240
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private string m_CachedAppReceipt;

		// Token: 0x040000F1 RID: 241
		private double? m_CachedAppReceiptModificationDate;

		// Token: 0x040000F2 RID: 242
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private string m_ProductsJson;
	}
}
