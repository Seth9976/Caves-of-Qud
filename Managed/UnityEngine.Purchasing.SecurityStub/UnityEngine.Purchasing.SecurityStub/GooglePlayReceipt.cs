using System;

namespace UnityEngine.Purchasing.Security
{
	// Token: 0x02000007 RID: 7
	public class GooglePlayReceipt : IPurchaseReceipt
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000210A File Offset: 0x0000030A
		public string productID { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002113 File Offset: 0x00000313
		public string transactionID
		{
			get
			{
				return this.orderID;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002123 File Offset: 0x00000323
		public string orderID { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002134 File Offset: 0x00000334
		public string packageName { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002145 File Offset: 0x00000345
		public string purchaseToken { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002156 File Offset: 0x00000356
		public DateTime purchaseDate { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002167 File Offset: 0x00000367
		public GooglePurchaseState purchaseState { get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x00002170 File Offset: 0x00000370
		public GooglePlayReceipt(string productID, string orderID, string packageName, string purchaseToken, DateTime purchaseTime, GooglePurchaseState purchaseState)
		{
			throw new NotImplementedException();
		}
	}
}
