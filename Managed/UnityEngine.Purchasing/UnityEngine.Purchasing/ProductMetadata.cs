using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000022 RID: 34
	public class ProductMetadata
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public ProductMetadata(string priceString, string title, string description, string currencyCode, decimal localizedPrice)
		{
			this.localizedPriceString = priceString;
			this.localizedTitle = title;
			this.localizedDescription = description;
			this.isoCurrencyCode = currencyCode;
			this.localizedPrice = localizedPrice;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002B04 File Offset: 0x00000D04
		public ProductMetadata(ProductMetadata productMetadata)
		{
			this.localizedPriceString = productMetadata.localizedPriceString;
			this.localizedTitle = productMetadata.localizedTitle;
			this.localizedDescription = productMetadata.localizedDescription;
			this.isoCurrencyCode = productMetadata.isoCurrencyCode;
			this.localizedPrice = productMetadata.localizedPrice;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002B53 File Offset: 0x00000D53
		public ProductMetadata()
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002B5B File Offset: 0x00000D5B
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002B63 File Offset: 0x00000D63
		public string localizedPriceString { get; internal set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002B6C File Offset: 0x00000D6C
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002B74 File Offset: 0x00000D74
		public string localizedTitle { get; internal set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002B7D File Offset: 0x00000D7D
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002B85 File Offset: 0x00000D85
		public string localizedDescription { get; internal set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002B8E File Offset: 0x00000D8E
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002B96 File Offset: 0x00000D96
		public string isoCurrencyCode { get; internal set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002B9F File Offset: 0x00000D9F
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public decimal localizedPrice { get; internal set; }
	}
}
