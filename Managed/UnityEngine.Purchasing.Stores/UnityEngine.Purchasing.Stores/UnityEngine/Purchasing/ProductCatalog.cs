using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000077 RID: 119
	[Serializable]
	public class ProductCatalog
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00009019 File Offset: 0x00007219
		public ICollection<ProductCatalogItem> allProducts
		{
			get
			{
				return this.products;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00009021 File Offset: 0x00007221
		public ICollection<ProductCatalogItem> allValidProducts
		{
			get
			{
				return this.products.Where<ProductCatalogItem>((ProductCatalogItem x) => !string.IsNullOrEmpty(x.id) && x.id.Trim().Length != 0).ToList<ProductCatalogItem>();
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009052 File Offset: 0x00007252
		internal static void Initialize()
		{
			if (ProductCatalog.instance == null)
			{
				ProductCatalog.Initialize(new ProductCatalogImpl());
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009065 File Offset: 0x00007265
		public static void Initialize(IProductCatalogImpl productCatalogImpl)
		{
			ProductCatalog.instance = productCatalogImpl;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000906D File Offset: 0x0000726D
		public void Add(ProductCatalogItem item)
		{
			this.products.Add(item);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000907B File Offset: 0x0000727B
		public void Remove(ProductCatalogItem item)
		{
			this.products.Remove(item);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000908C File Offset: 0x0000728C
		public bool IsEmpty()
		{
			using (List<ProductCatalogItem>.Enumerator enumerator = this.products.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!string.IsNullOrEmpty(enumerator.Current.id))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000090EC File Offset: 0x000072EC
		public static string Serialize(ProductCatalog catalog)
		{
			return JsonUtility.ToJson(catalog);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000090F4 File Offset: 0x000072F4
		public static ProductCatalog Deserialize(string catalogJSON)
		{
			return JsonUtility.FromJson<ProductCatalog>(catalogJSON);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000090FC File Offset: 0x000072FC
		public static ProductCatalog FromTextAsset(TextAsset asset)
		{
			return ProductCatalog.Deserialize(asset.text);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00009109 File Offset: 0x00007309
		public static ProductCatalog LoadDefaultCatalog()
		{
			ProductCatalog.Initialize();
			return ProductCatalog.instance.LoadDefaultCatalog();
		}

		// Token: 0x0400016E RID: 366
		private static IProductCatalogImpl instance;

		// Token: 0x0400016F RID: 367
		public string appleSKU;

		// Token: 0x04000170 RID: 368
		public string appleTeamID;

		// Token: 0x04000171 RID: 369
		public bool enableCodelessAutoInitialization = true;

		// Token: 0x04000172 RID: 370
		public bool enableUnityGamingServicesAutoInitialization;

		// Token: 0x04000173 RID: 371
		[SerializeField]
		private List<ProductCatalogItem> products = new List<ProductCatalogItem>();

		// Token: 0x04000174 RID: 372
		public const string kCatalogPath = "Assets/Resources/IAPProductCatalog.json";

		// Token: 0x04000175 RID: 373
		public const string kPrevCatalogPath = "Assets/Plugins/UnityPurchasing/Resources/IAPProductCatalog.json";
	}
}
