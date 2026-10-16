using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class ProductCatalogItem
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00008C3A File Offset: 0x00006E3A
		public void AddPayout()
		{
			this.payouts.Add(new ProductCatalogPayout());
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008C4C File Offset: 0x00006E4C
		public void RemovePayout(ProductCatalogPayout payout)
		{
			this.payouts.Remove(payout);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00008C5B File Offset: 0x00006E5B
		public IList<ProductCatalogPayout> Payouts
		{
			get
			{
				return this.payouts;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008C64 File Offset: 0x00006E64
		public ProductCatalogItem Clone()
		{
			ProductCatalogItem productCatalogItem = new ProductCatalogItem
			{
				id = this.id,
				type = this.type
			};
			productCatalogItem.SetStoreIDs(this.allStoreIDs);
			productCatalogItem.defaultDescription = this.defaultDescription.Clone();
			productCatalogItem.screenshotPath = this.screenshotPath;
			productCatalogItem.applePriceTier = this.applePriceTier;
			productCatalogItem.googlePrice.value = this.googlePrice.value;
			productCatalogItem.pricingTemplateID = this.pricingTemplateID;
			foreach (LocalizedProductDescription localizedProductDescription in this.descriptions)
			{
				productCatalogItem.descriptions.Add(localizedProductDescription.Clone());
			}
			return productCatalogItem;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00008D38 File Offset: 0x00006F38
		public void SetStoreID(string aStore, string aId)
		{
			this.storeIDs.RemoveAll((StoreID obj) => obj.store == aStore);
			if (!string.IsNullOrEmpty(aId))
			{
				this.storeIDs.Add(new StoreID(aStore, aId));
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008D8C File Offset: 0x00006F8C
		public string GetStoreID(string store)
		{
			StoreID storeID = this.storeIDs.Find((StoreID obj) => obj.store == store);
			if (storeID != null)
			{
				return storeID.id;
			}
			return null;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00008DC9 File Offset: 0x00006FC9
		public ICollection<StoreID> allStoreIDs
		{
			get
			{
				return this.storeIDs;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public void SetStoreIDs(ICollection<StoreID> storeIds)
		{
			using (IEnumerator<StoreID> enumerator = storeIds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StoreID storeId = enumerator.Current;
					this.storeIDs.RemoveAll((StoreID obj) => obj.store == storeId.store);
					if (!string.IsNullOrEmpty(storeId.id))
					{
						this.storeIDs.Add(new StoreID(storeId.store, storeId.id));
					}
				}
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008E70 File Offset: 0x00007070
		public LocalizedProductDescription GetDescription(TranslationLocale locale)
		{
			return this.descriptions.Find((LocalizedProductDescription obj) => obj.googleLocale == locale);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008EA1 File Offset: 0x000070A1
		public LocalizedProductDescription GetOrCreateDescription(TranslationLocale locale)
		{
			return this.GetDescription(locale) ?? this.AddDescription(locale);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008EB8 File Offset: 0x000070B8
		public LocalizedProductDescription AddDescription(TranslationLocale locale)
		{
			this.RemoveDescription(locale);
			LocalizedProductDescription localizedProductDescription = new LocalizedProductDescription
			{
				googleLocale = locale
			};
			this.descriptions.Add(localizedProductDescription);
			return localizedProductDescription;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008EE8 File Offset: 0x000070E8
		public void RemoveDescription(TranslationLocale locale)
		{
			this.descriptions.RemoveAll((LocalizedProductDescription obj) => obj.googleLocale == locale);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008F1A File Offset: 0x0000711A
		public bool HasAvailableLocale
		{
			get
			{
				return Enum.GetValues(typeof(TranslationLocale)).Length > this.descriptions.Count + 1;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00008F40 File Offset: 0x00007140
		public TranslationLocale NextAvailableLocale
		{
			get
			{
				foreach (object obj in Enum.GetValues(typeof(TranslationLocale)))
				{
					TranslationLocale translationLocale = (TranslationLocale)obj;
					if (this.GetDescription(translationLocale) == null && this.defaultDescription.googleLocale != translationLocale)
					{
						return translationLocale;
					}
				}
				return TranslationLocale.en_US;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00008FBC File Offset: 0x000071BC
		public ICollection<LocalizedProductDescription> translatedDescriptions
		{
			get
			{
				return this.descriptions;
			}
		}

		// Token: 0x04000163 RID: 355
		public string id;

		// Token: 0x04000164 RID: 356
		public ProductType type;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		private List<StoreID> storeIDs = new List<StoreID>();

		// Token: 0x04000166 RID: 358
		public LocalizedProductDescription defaultDescription = new LocalizedProductDescription();

		// Token: 0x04000167 RID: 359
		public string screenshotPath;

		// Token: 0x04000168 RID: 360
		public int applePriceTier;

		// Token: 0x04000169 RID: 361
		public Price googlePrice = new Price();

		// Token: 0x0400016A RID: 362
		public string pricingTemplateID;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private List<LocalizedProductDescription> descriptions = new List<LocalizedProductDescription>();

		// Token: 0x0400016C RID: 364
		public Price udpPrice = new Price();

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private List<ProductCatalogPayout> payouts = new List<ProductCatalogPayout>();
	}
}
