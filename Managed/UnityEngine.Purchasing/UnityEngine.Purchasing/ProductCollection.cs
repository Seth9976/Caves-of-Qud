using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000020 RID: 32
	public class ProductCollection
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002888 File Offset: 0x00000A88
		internal ProductCollection(Product[] products)
		{
			this.AddProducts(products);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000028A4 File Offset: 0x00000AA4
		internal void AddProducts(IEnumerable<Product> products)
		{
			this.set.UnionWith(products);
			this.all = this.set.ToArray<Product>();
			this.m_IdToProduct = this.all.ToDictionary<Product, string>((Product x) => x.definition.id);
			this.m_StoreSpecificIdToProduct = this.all.ToDictionary<Product, string>((Product x) => x.definition.storeSpecificId);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000292E File Offset: 0x00000B2E
		public HashSet<Product> set { get; } = new HashSet<Product>();

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002936 File Offset: 0x00000B36
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000293E File Offset: 0x00000B3E
		public Product[] all { get; private set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00002948 File Offset: 0x00000B48
		public Product WithID(string id)
		{
			Product product;
			this.m_IdToProduct.TryGetValue(id, out product);
			return product;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002968 File Offset: 0x00000B68
		public Product WithStoreSpecificID(string id)
		{
			Product product = null;
			if (id != null)
			{
				this.m_StoreSpecificIdToProduct.TryGetValue(id, out product);
			}
			return product;
		}

		// Token: 0x0400002C RID: 44
		private Dictionary<string, Product> m_IdToProduct;

		// Token: 0x0400002D RID: 45
		private Dictionary<string, Product> m_StoreSpecificIdToProduct;
	}
}
