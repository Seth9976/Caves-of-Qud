using System;

namespace UnityEngine.Purchasing.Security
{
	// Token: 0x02000004 RID: 4
	public class AppleInAppPurchaseReceipt : IPurchaseReceipt
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002145 File Offset: 0x00000345
		public int quantity { get; internal set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002156 File Offset: 0x00000356
		public string productID { get; internal set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002167 File Offset: 0x00000367
		public string transactionID { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002178 File Offset: 0x00000378
		public string originalTransactionIdentifier { get; internal set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002181 File Offset: 0x00000381
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002189 File Offset: 0x00000389
		public DateTime purchaseDate { get; internal set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002192 File Offset: 0x00000392
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000219A File Offset: 0x0000039A
		public DateTime originalPurchaseDate { get; internal set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021A3 File Offset: 0x000003A3
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021AB File Offset: 0x000003AB
		public DateTime subscriptionExpirationDate { get; internal set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021B4 File Offset: 0x000003B4
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021BC File Offset: 0x000003BC
		public DateTime cancellationDate { get; internal set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021C5 File Offset: 0x000003C5
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021CD File Offset: 0x000003CD
		public int isFreeTrial { get; internal set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021D6 File Offset: 0x000003D6
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000021DE File Offset: 0x000003DE
		public int productType { get; internal set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000021EF File Offset: 0x000003EF
		public int isIntroductoryPricePeriod { get; internal set; }
	}
}
