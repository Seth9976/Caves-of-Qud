using System;

namespace UnityEngine.Purchasing.Default
{
	// Token: 0x02000005 RID: 5
	public class WinProductDescription
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020C6 File Offset: 0x000002C6
		public string platformSpecificID { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020CF File Offset: 0x000002CF
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020D7 File Offset: 0x000002D7
		public string price { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000020E8 File Offset: 0x000002E8
		public string title { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000020F1 File Offset: 0x000002F1
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000020F9 File Offset: 0x000002F9
		public string description { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000210A File Offset: 0x0000030A
		public string ISOCurrencyCode { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000211B File Offset: 0x0000031B
		public decimal priceDecimal { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000212C File Offset: 0x0000032C
		public string receipt { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002135 File Offset: 0x00000335
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000213D File Offset: 0x0000033D
		public string transactionID { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002146 File Offset: 0x00000346
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000214E File Offset: 0x0000034E
		public bool consumable { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002158 File Offset: 0x00000358
		public WinProductDescription(string id, string price, string title, string description, string isoCode, decimal priceD, string receipt = null, string transactionId = null, bool consumable = false)
		{
			this.platformSpecificID = id;
			this.price = price;
			this.title = title;
			this.description = description;
			this.ISOCurrencyCode = isoCode;
			this.priceDecimal = priceD;
			this.receipt = receipt;
			this.transactionID = transactionId;
			this.consumable = consumable;
		}
	}
}
