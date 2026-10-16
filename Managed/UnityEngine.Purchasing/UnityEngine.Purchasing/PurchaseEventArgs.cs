using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000024 RID: 36
	public class PurchaseEventArgs
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00002BB0 File Offset: 0x00000DB0
		internal PurchaseEventArgs(Product purchasedProduct)
		{
			this.purchasedProduct = purchasedProduct;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002BBF File Offset: 0x00000DBF
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public Product purchasedProduct { get; private set; }
	}
}
