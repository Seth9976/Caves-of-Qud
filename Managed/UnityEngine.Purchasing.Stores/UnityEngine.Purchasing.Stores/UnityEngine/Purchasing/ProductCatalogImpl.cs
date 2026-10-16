using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000079 RID: 121
	internal class ProductCatalogImpl : IProductCatalogImpl
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00009134 File Offset: 0x00007334
		public ProductCatalog LoadDefaultCatalog()
		{
			TextAsset textAsset = Resources.Load("IAPProductCatalog") as TextAsset;
			if (textAsset != null)
			{
				return ProductCatalog.FromTextAsset(textAsset);
			}
			return new ProductCatalog();
		}
	}
}
