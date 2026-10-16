using System;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x0200004D RID: 77
	public class PurchaseFailureDescription
	{
		// Token: 0x06000159 RID: 345 RVA: 0x000041EB File Offset: 0x000023EB
		public PurchaseFailureDescription(string productId, PurchaseFailureReason reason, string message)
		{
			this.productId = productId;
			this.reason = reason;
			this.message = message;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004208 File Offset: 0x00002408
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004210 File Offset: 0x00002410
		public string productId { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004219 File Offset: 0x00002419
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004221 File Offset: 0x00002421
		public PurchaseFailureReason reason { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000422A File Offset: 0x0000242A
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00004232 File Offset: 0x00002432
		public string message { get; private set; }
	}
}
