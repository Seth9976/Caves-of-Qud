using System;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000AC RID: 172
	internal interface IGoogleBillingResult
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003C3 RID: 963
		GoogleBillingResponseCode responseCode { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003C4 RID: 964
		string debugMessage { get; }
	}
}
