using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000038 RID: 56
	public class GoogleProductMetadata : ProductMetadata
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000044EA File Offset: 0x000026EA
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000044F2 File Offset: 0x000026F2
		public string originalJson { get; internal set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000044FB File Offset: 0x000026FB
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004503 File Offset: 0x00002703
		public string subscriptionPeriod { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000450C File Offset: 0x0000270C
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004514 File Offset: 0x00002714
		public string freeTrialPeriod { get; internal set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000451D File Offset: 0x0000271D
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004525 File Offset: 0x00002725
		public string introductoryPrice { get; internal set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000452E File Offset: 0x0000272E
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004536 File Offset: 0x00002736
		public string introductoryPricePeriod { get; internal set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000453F File Offset: 0x0000273F
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004547 File Offset: 0x00002747
		public int introductoryPriceCycles { get; internal set; }

		// Token: 0x06000126 RID: 294 RVA: 0x00004550 File Offset: 0x00002750
		internal GoogleProductMetadata(string priceString, string title, string description, string currencyCode, decimal localizedPrice)
			: base(priceString, title, description, currencyCode, localizedPrice)
		{
		}
	}
}
