using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200003D RID: 61
	public static class StoreCallbackExtensionMethods
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00004C4E File Offset: 0x00002E4E
		public static Product FindProductById(this IStoreCallback storeCallback, string sku)
		{
			if (sku != null && storeCallback.products != null)
			{
				return storeCallback.products.WithID(sku) ?? storeCallback.products.WithStoreSpecificID(sku);
			}
			return null;
		}
	}
}
