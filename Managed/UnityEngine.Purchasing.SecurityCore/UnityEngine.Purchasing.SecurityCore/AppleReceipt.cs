using System;

namespace UnityEngine.Purchasing.Security
{
	// Token: 0x02000003 RID: 3
	public class AppleReceipt
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public string bundleID { get; internal set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020CF File Offset: 0x000002CF
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020D7 File Offset: 0x000002D7
		public string appVersion { get; internal set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020E8 File Offset: 0x000002E8
		public DateTime expirationDate { get; internal set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020F1 File Offset: 0x000002F1
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020F9 File Offset: 0x000002F9
		public byte[] opaque { get; internal set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000210A File Offset: 0x0000030A
		public byte[] hash { get; internal set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000211B File Offset: 0x0000031B
		public string originalApplicationVersion { get; internal set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000212C File Offset: 0x0000032C
		public DateTime receiptCreationDate { get; internal set; }

		// Token: 0x04000008 RID: 8
		public AppleInAppPurchaseReceipt[] inAppPurchaseReceipts;
	}
}
