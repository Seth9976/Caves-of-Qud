using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000008 RID: 8
	public static class IAPConfigurationHelper
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public static void PopulateConfigurationBuilder(ref ConfigurationBuilder builder, ProductCatalog catalog)
		{
			foreach (ProductCatalogItem productCatalogItem in catalog.allValidProducts)
			{
				IDs ds = null;
				if (productCatalogItem.allStoreIDs.Count > 0)
				{
					ds = new IDs();
					foreach (StoreID storeID in productCatalogItem.allStoreIDs)
					{
						ds.Add(storeID.id, new string[] { storeID.store });
					}
				}
				List<PayoutDefinition> list = new List<PayoutDefinition>();
				foreach (ProductCatalogPayout productCatalogPayout in productCatalogItem.Payouts)
				{
					list.Add(new PayoutDefinition(productCatalogPayout.typeString, productCatalogPayout.subtype, productCatalogPayout.quantity, productCatalogPayout.data));
				}
				builder.AddProduct(productCatalogItem.id, productCatalogItem.type, ds, list.ToArray());
			}
		}
	}
}
