using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000012 RID: 18
	internal class GoogleCachedQueryProductDetailsService : IGoogleCachedQueryProductDetailsService
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000024F0 File Offset: 0x000006F0
		protected override void Finalize()
		{
			try
			{
				foreach (KeyValuePair<string, AndroidJavaObject> keyValuePair in this.m_CachedQueriedProductDetails)
				{
					AndroidJavaObject value = keyValuePair.Value;
					if (value != null)
					{
						value.Dispose();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002564 File Offset: 0x00000764
		public IEnumerable<AndroidJavaObject> GetCachedQueriedProducts()
		{
			return this.m_CachedQueriedProductDetails.Values;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002571 File Offset: 0x00000771
		private AndroidJavaObject GetCachedQueriedProductDetails(string productId)
		{
			return this.m_CachedQueriedProductDetails[productId];
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000257F File Offset: 0x0000077F
		private IEnumerable<AndroidJavaObject> GetCachedQueriedProductDetails(IEnumerable<string> productIds)
		{
			return productIds.Select<string, AndroidJavaObject>(new Func<string, AndroidJavaObject>(this.GetCachedQueriedProductDetails));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002593 File Offset: 0x00000793
		public IEnumerable<AndroidJavaObject> GetCachedQueriedProductDetails(IEnumerable<ProductDefinition> products)
		{
			return this.GetCachedQueriedProductDetails(products.Select<ProductDefinition, string>((ProductDefinition product) => product.storeSpecificId).ToList<string>());
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025C5 File Offset: 0x000007C5
		private bool Contains(string productId)
		{
			return this.m_CachedQueriedProductDetails.ContainsKey(productId);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025D3 File Offset: 0x000007D3
		public bool Contains(ProductDefinition products)
		{
			return this.Contains(products.storeSpecificId);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025E4 File Offset: 0x000007E4
		public void AddCachedQueriedProductDetails(IEnumerable<AndroidJavaObject> queriedProducts)
		{
			foreach (AndroidJavaObject androidJavaObject in queriedProducts)
			{
				string text = androidJavaObject.Call<string>("getProductId", Array.Empty<object>());
				this.m_CachedQueriedProductDetails[text] = androidJavaObject.CloneReference();
			}
		}

		// Token: 0x04000012 RID: 18
		private readonly Dictionary<string, AndroidJavaObject> m_CachedQueriedProductDetails = new Dictionary<string, AndroidJavaObject>();
	}
}
