using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000025 RID: 37
	public class PurchaseFailedEventArgs
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00002BD0 File Offset: 0x00000DD0
		internal PurchaseFailedEventArgs(Product purchasedProduct, PurchaseFailureReason reason, string message)
		{
			this.purchasedProduct = purchasedProduct;
			this.reason = reason;
			this.message = message;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002BED File Offset: 0x00000DED
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00002BF5 File Offset: 0x00000DF5
		public Product purchasedProduct { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002BFE File Offset: 0x00000DFE
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002C06 File Offset: 0x00000E06
		public PurchaseFailureReason reason { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002C0F File Offset: 0x00000E0F
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002C17 File Offset: 0x00000E17
		public string message { get; private set; }
	}
}
