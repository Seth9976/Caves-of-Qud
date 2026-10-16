using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000011 RID: 17
	public class ConfigurationBuilder
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000237E File Offset: 0x0000057E
		internal ConfigurationBuilder(PurchasingFactory factory)
		{
			this.factory = factory;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000239F File Offset: 0x0000059F
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000023A7 File Offset: 0x000005A7
		public bool useCatalogProvider { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000023B0 File Offset: 0x000005B0
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000023B8 File Offset: 0x000005B8
		public bool logUnavailableProducts { get; set; } = true;

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000023C1 File Offset: 0x000005C1
		public HashSet<ProductDefinition> products { get; } = new HashSet<ProductDefinition>();

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000023C9 File Offset: 0x000005C9
		internal PurchasingFactory factory { get; }

		// Token: 0x06000045 RID: 69 RVA: 0x000023D1 File Offset: 0x000005D1
		public T Configure<T>() where T : IStoreConfiguration
		{
			return this.factory.GetConfig<T>();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000023DE File Offset: 0x000005DE
		public static ConfigurationBuilder Instance(IPurchasingModule first, params IPurchasingModule[] rest)
		{
			return new ConfigurationBuilder(new PurchasingFactory(first, rest));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000023EC File Offset: 0x000005EC
		public ConfigurationBuilder AddProduct(string id, ProductType type)
		{
			return this.AddProduct(id, type, null);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000023F7 File Offset: 0x000005F7
		public ConfigurationBuilder AddProduct(string id, ProductType type, IDs storeIDs)
		{
			return this.AddProduct(id, type, storeIDs, null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002403 File Offset: 0x00000603
		public ConfigurationBuilder AddProduct(string id, ProductType type, IDs storeIDs, PayoutDefinition payout)
		{
			return this.AddProduct(id, type, storeIDs, new List<PayoutDefinition> { payout });
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000241C File Offset: 0x0000061C
		public ConfigurationBuilder AddProduct(string id, ProductType type, IDs storeIDs, IEnumerable<PayoutDefinition> payouts)
		{
			string text = id;
			if (storeIDs != null)
			{
				text = storeIDs.SpecificIDForStore(this.factory.storeName, id);
			}
			ProductDefinition productDefinition = new ProductDefinition(id, text, type);
			productDefinition.SetPayouts(payouts);
			this.products.Add(productDefinition);
			return this;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002460 File Offset: 0x00000660
		public ConfigurationBuilder AddProducts(IEnumerable<ProductDefinition> products)
		{
			foreach (ProductDefinition productDefinition in products)
			{
				this.products.Add(productDefinition);
			}
			return this;
		}
	}
}
