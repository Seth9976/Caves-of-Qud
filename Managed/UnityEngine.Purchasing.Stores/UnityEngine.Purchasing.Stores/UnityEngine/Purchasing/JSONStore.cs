using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Stores.Util;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000064 RID: 100
	internal class JSONStore : AbstractStore, IUnityCallback, IStoreInternal, ITransactionHistoryExtensions, IStoreExtension
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000075BC File Offset: 0x000057BC
		public Product[] storeCatalog
		{
			get
			{
				List<Product> list = new List<Product>();
				if (this.m_StoreCatalog != null && this.unity.products.all != null)
				{
					foreach (ProductDefinition productDefinition in this.m_StoreCatalog)
					{
						foreach (Product product in this.unity.products.all)
						{
							bool flag = false;
							if (product.definition.type != ProductType.Consumable && (product.hasReceipt || !string.IsNullOrEmpty(product.transactionID)))
							{
								flag = true;
							}
							if (product.availableToPurchase && !flag && product.definition.storeSpecificId == productDefinition.storeSpecificId)
							{
								list.Add(product);
							}
						}
					}
				}
				return list.ToArray();
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000076BC File Offset: 0x000058BC
		public JSONStore()
		{
			this.m_ProductDescriptionsDeserializer = new JsonProductDescriptionsDeserializer();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000076D7 File Offset: 0x000058D7
		public void SetNativeStore(INativeStore native)
		{
			this.m_Store = native;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000076E0 File Offset: 0x000058E0
		void IStoreInternal.SetModule(StandardPurchasingModule module)
		{
			if (module == null)
			{
				return;
			}
			this.m_Module = module;
			this.m_Logger = module.logger ?? Debug.unityLogger;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007702 File Offset: 0x00005902
		public override void Initialize(IStoreCallback callback)
		{
			this.unity = callback;
			if (this.m_Module != null)
			{
				string storeName = this.m_Module.storeInstance.storeName;
				return;
			}
			if (this.m_Logger != null)
			{
				this.m_Logger.LogIAPWarning("JSONStore init has no reference to SPM, can't start managed store");
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000773D File Offset: 0x0000593D
		public override void RetrieveProducts(ReadOnlyCollection<ProductDefinition> products)
		{
			this.m_Store.RetrieveProducts(JSONSerializer.SerializeProductDefs(products));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007750 File Offset: 0x00005950
		internal void ProcessManagedStoreResponse(List<ProductDefinition> storeProducts)
		{
			this.m_StoreCatalog = storeProducts;
			if (this.m_IsRefreshing)
			{
				this.m_IsRefreshing = false;
				if (this.storeCatalog.Length == 0 && this.m_RefreshCallback != null)
				{
					this.m_RefreshCallback();
					this.m_RefreshCallback = null;
					return;
				}
			}
			HashSet<ProductDefinition> hashSet = new HashSet<ProductDefinition>();
			if (storeProducts != null)
			{
				hashSet.UnionWith(storeProducts);
			}
			this.m_Store.RetrieveProducts(JSONSerializer.SerializeProductDefs(hashSet));
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000077B8 File Offset: 0x000059B8
		public override void Purchase(ProductDefinition product, string developerPayload)
		{
			this.m_Store.Purchase(JSONSerializer.SerializeProductDef(product), developerPayload);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000077CC File Offset: 0x000059CC
		public override void FinishTransaction(ProductDefinition product, string transactionId)
		{
			string text = ((product == null) ? null : JSONSerializer.SerializeProductDef(product));
			this.m_Store.FinishTransaction(text, transactionId);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000077F4 File Offset: 0x000059F4
		public void OnSetupFailed(string reason)
		{
			InitializationFailureReason initializationFailureReason = (InitializationFailureReason)Enum.Parse(typeof(InitializationFailureReason), reason, true);
			this.unity.OnSetupFailed(initializationFailureReason, null);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007825 File Offset: 0x00005A25
		public virtual void OnProductsRetrieved(string json)
		{
			this.unity.OnProductsRetrieved(this.m_ProductDescriptionsDeserializer.DeserializeProductDescriptions(json));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000783E File Offset: 0x00005A3E
		public virtual void OnPurchaseSucceeded(string id, string receipt, string transactionID)
		{
			this.unity.OnPurchaseSucceeded(id, receipt, transactionID);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000784E File Offset: 0x00005A4E
		public void OnPurchaseFailed(string json)
		{
			this.OnPurchaseFailed(JSONSerializer.DeserializeFailureReason(json), json);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000785D File Offset: 0x00005A5D
		public void OnPurchaseFailed(PurchaseFailureDescription failure, string json = null)
		{
			this.m_LastPurchaseFailureDescription = failure;
			this.m_LastPurchaseErrorCode = this.ParseStoreSpecificPurchaseErrorCode(json);
			this.unity.OnPurchaseFailed(failure);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000787F File Offset: 0x00005A7F
		public PurchaseFailureDescription GetLastPurchaseFailureDescription()
		{
			return this.m_LastPurchaseFailureDescription;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007887 File Offset: 0x00005A87
		public StoreSpecificPurchaseErrorCode GetLastStoreSpecificPurchaseErrorCode()
		{
			return this.m_LastPurchaseErrorCode;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007890 File Offset: 0x00005A90
		private StoreSpecificPurchaseErrorCode ParseStoreSpecificPurchaseErrorCode(string json)
		{
			if (json == null)
			{
				return StoreSpecificPurchaseErrorCode.Unknown;
			}
			Dictionary<string, object> dictionary = MiniJson.JsonDecode(json) as Dictionary<string, object>;
			if (dictionary != null && dictionary.ContainsKey("storeSpecificErrorCode") && Enum.IsDefined(typeof(StoreSpecificPurchaseErrorCode), (string)dictionary["storeSpecificErrorCode"]))
			{
				string text = (string)dictionary["storeSpecificErrorCode"];
				return (StoreSpecificPurchaseErrorCode)Enum.Parse(typeof(StoreSpecificPurchaseErrorCode), text);
			}
			return StoreSpecificPurchaseErrorCode.Unknown;
		}

		// Token: 0x040000FB RID: 251
		protected IStoreCallback unity;

		// Token: 0x040000FC RID: 252
		private INativeStore m_Store;

		// Token: 0x040000FD RID: 253
		private List<ProductDefinition> m_StoreCatalog;

		// Token: 0x040000FE RID: 254
		private bool m_IsRefreshing;

		// Token: 0x040000FF RID: 255
		private Action m_RefreshCallback;

		// Token: 0x04000100 RID: 256
		private StandardPurchasingModule m_Module;

		// Token: 0x04000101 RID: 257
		protected ILogger m_Logger;

		// Token: 0x04000102 RID: 258
		protected JsonProductDescriptionsDeserializer m_ProductDescriptionsDeserializer;

		// Token: 0x04000103 RID: 259
		protected PurchaseFailureDescription m_LastPurchaseFailureDescription;

		// Token: 0x04000104 RID: 260
		private StoreSpecificPurchaseErrorCode m_LastPurchaseErrorCode = StoreSpecificPurchaseErrorCode.Unknown;

		// Token: 0x04000105 RID: 261
		private const string k_StoreSpecificErrorCodeKey = "storeSpecificErrorCode";
	}
}
