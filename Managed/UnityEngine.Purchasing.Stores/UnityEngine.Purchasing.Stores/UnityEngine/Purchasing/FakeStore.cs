using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000069 RID: 105
	internal class FakeStore : JSONStore, IFakeExtensions, IStoreExtension, INativeStore
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007AF2 File Offset: 0x00005CF2
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00007AFA File Offset: 0x00005CFA
		public string unavailableProductId { get; set; }

		// Token: 0x0600025F RID: 607 RVA: 0x00007B03 File Offset: 0x00005D03
		public override void Initialize(IStoreCallback biller)
		{
			this.m_Biller = biller;
			base.Initialize(biller);
			base.SetNativeStore(this);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007B1C File Offset: 0x00005D1C
		public void RetrieveProducts(string json)
		{
			List<ProductDefinition> list = ((List<object>)MiniJson.JsonDecode(json)).DecodeJSON("fake");
			this.StoreRetrieveProducts(new ReadOnlyCollection<ProductDefinition>(list.ToList<ProductDefinition>()));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007B50 File Offset: 0x00005D50
		public void StoreRetrieveProducts(ReadOnlyCollection<ProductDefinition> productDefinitions)
		{
			FakeStore.<>c__DisplayClass13_0 CS$<>8__locals1 = new FakeStore.<>c__DisplayClass13_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.products = new List<ProductDescription>();
			foreach (ProductDefinition productDefinition in productDefinitions)
			{
				if (this.unavailableProductId != productDefinition.id)
				{
					ProductMetadata productMetadata = new ProductMetadata("$0.01", "Fake title for " + productDefinition.id, "Fake description", "USD", 0.01m);
					ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog();
					if (productCatalog != null)
					{
						foreach (ProductCatalogItem productCatalogItem in productCatalog.allProducts)
						{
							if (productCatalogItem.id == productDefinition.id)
							{
								productMetadata = new ProductMetadata(productCatalogItem.googlePrice.value.ToString(), productCatalogItem.defaultDescription.Title, productCatalogItem.defaultDescription.Description, "USD", productCatalogItem.googlePrice.value);
							}
						}
					}
					CS$<>8__locals1.products.Add(new ProductDescription(productDefinition.storeSpecificId, productMetadata));
				}
			}
			if (this.UIMode != FakeStoreUIMode.DeveloperUser || !this.StartUI<InitializationFailureReason>(productDefinitions, FakeStore.DialogType.RetrieveProducts, new Action<bool, InitializationFailureReason>(CS$<>8__locals1.<StoreRetrieveProducts>g__handleAllowInitializeOrRetrieveProducts|0)))
			{
				CS$<>8__locals1.<StoreRetrieveProducts>g__handleAllowInitializeOrRetrieveProducts|0(true, InitializationFailureReason.AppNotKnown);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public void Purchase(string productJSON, string developerPayload)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(productJSON);
			object obj;
			dictionary.TryGetValue("id", out obj);
			string text = obj.ToString();
			dictionary.TryGetValue("storeSpecificId", out obj);
			string text2 = obj.ToString();
			dictionary.TryGetValue("type", out obj);
			string text3 = obj.ToString();
			ProductType productType = (Enum.IsDefined(typeof(ProductType), text3) ? ((ProductType)Enum.Parse(typeof(ProductType), text3)) : ProductType.Consumable);
			ProductDefinition productDefinition = new ProductDefinition(text, text2, productType);
			this.FakePurchase(productDefinition, developerPayload);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007D80 File Offset: 0x00005F80
		private void FakePurchase(ProductDefinition product, string developerPayload)
		{
			FakeStore.<>c__DisplayClass15_0 CS$<>8__locals1 = new FakeStore.<>c__DisplayClass15_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.product = product;
			this.purchaseCalled = true;
			if (CS$<>8__locals1.product.type != ProductType.Consumable)
			{
				this.m_PurchasedProducts.Add(CS$<>8__locals1.product.storeSpecificId);
			}
			if (!this.StartUI<PurchaseFailureReason>(CS$<>8__locals1.product, FakeStore.DialogType.Purchase, new Action<bool, PurchaseFailureReason>(CS$<>8__locals1.<FakePurchase>g__handleAllowPurchase|0)))
			{
				CS$<>8__locals1.<FakePurchase>g__handleAllowPurchase|0(true, (PurchaseFailureReason)Enum.Parse(typeof(PurchaseFailureReason), "Unknown"));
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007E08 File Offset: 0x00006008
		public void RestoreTransactions(Action<bool, string> callback)
		{
			this.restoreCalled = true;
			foreach (string text in this.m_PurchasedProducts)
			{
				this.m_Biller.OnPurchaseSucceeded(text, "{ \"this\" : \"is a fake receipt\" }", "1");
			}
			if (callback != null)
			{
				callback(true, null);
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007E7C File Offset: 0x0000607C
		public void FinishTransaction(string productJSON, string transactionID)
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00007E7E File Offset: 0x0000607E
		public override void FinishTransaction(ProductDefinition product, string transactionId)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00007E80 File Offset: 0x00006080
		public void RegisterPurchaseForRestore(string productId)
		{
			this.m_PurchasedProducts.Add(productId);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00007E8E File Offset: 0x0000608E
		protected virtual bool StartUI<T>(object model, FakeStore.DialogType dialogType, Action<bool, T> callback)
		{
			return false;
		}

		// Token: 0x04000110 RID: 272
		public const string Name = "fake";

		// Token: 0x04000111 RID: 273
		private IStoreCallback m_Biller;

		// Token: 0x04000112 RID: 274
		private readonly List<string> m_PurchasedProducts = new List<string>();

		// Token: 0x04000113 RID: 275
		public bool purchaseCalled;

		// Token: 0x04000114 RID: 276
		public bool restoreCalled;

		// Token: 0x04000116 RID: 278
		public FakeStoreUIMode UIMode;

		// Token: 0x020000F9 RID: 249
		protected enum DialogType
		{
			// Token: 0x040002E8 RID: 744
			Purchase,
			// Token: 0x040002E9 RID: 745
			RetrieveProducts
		}
	}
}
