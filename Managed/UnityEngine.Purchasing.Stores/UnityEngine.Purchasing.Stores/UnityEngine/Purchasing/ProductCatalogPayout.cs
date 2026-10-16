using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000075 RID: 117
	[Serializable]
	public class ProductCatalogPayout
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008B20 File Offset: 0x00006D20
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00008B62 File Offset: 0x00006D62
		public ProductCatalogPayout.ProductCatalogPayoutType type
		{
			get
			{
				ProductCatalogPayout.ProductCatalogPayoutType productCatalogPayoutType = ProductCatalogPayout.ProductCatalogPayoutType.Other;
				if (Enum.IsDefined(typeof(ProductCatalogPayout.ProductCatalogPayoutType), this.t))
				{
					productCatalogPayoutType = (ProductCatalogPayout.ProductCatalogPayoutType)Enum.Parse(typeof(ProductCatalogPayout.ProductCatalogPayoutType), this.t);
				}
				return productCatalogPayoutType;
			}
			set
			{
				this.t = value.ToString();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008B77 File Offset: 0x00006D77
		public string typeString
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008B7F File Offset: 0x00006D7F
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x00008B87 File Offset: 0x00006D87
		public string subtype
		{
			get
			{
				return this.st;
			}
			set
			{
				if (value.Length > 64)
				{
					throw new ArgumentException(string.Format("subtype should be no longer than {0} characters", 64));
				}
				this.st = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00008BB1 File Offset: 0x00006DB1
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00008BB9 File Offset: 0x00006DB9
		public double quantity
		{
			get
			{
				return this.q;
			}
			set
			{
				this.q = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00008BC2 File Offset: 0x00006DC2
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00008BCA File Offset: 0x00006DCA
		public string data
		{
			get
			{
				return this.d;
			}
			set
			{
				if (value.Length > 1024)
				{
					throw new ArgumentException(string.Format("data should be no longer than {0} characters", 1024));
				}
				this.d = value;
			}
		}

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private string t = ProductCatalogPayout.ProductCatalogPayoutType.Other.ToString();

		// Token: 0x0400015E RID: 350
		public const int MaxSubtypeLength = 64;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private string st = string.Empty;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		private double q;

		// Token: 0x04000161 RID: 353
		public const int MaxDataLength = 1024;

		// Token: 0x04000162 RID: 354
		[SerializeField]
		private string d = string.Empty;

		// Token: 0x020000FF RID: 255
		public enum ProductCatalogPayoutType
		{
			// Token: 0x040002F4 RID: 756
			Other,
			// Token: 0x040002F5 RID: 757
			Currency,
			// Token: 0x040002F6 RID: 758
			Item,
			// Token: 0x040002F7 RID: 759
			Resource
		}
	}
}
