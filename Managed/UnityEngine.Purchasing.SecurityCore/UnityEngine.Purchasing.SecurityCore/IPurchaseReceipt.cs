using System;

namespace UnityEngine.Purchasing.Security
{
	// Token: 0x02000007 RID: 7
	public interface IPurchaseReceipt
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002C RID: 44
		string transactionID { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45
		string productID { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46
		DateTime purchaseDate { get; }
	}
}
