using System;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000AE RID: 174
	internal class GoogleBillingResult : IGoogleBillingResult
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000C450 File Offset: 0x0000A650
		public GoogleBillingResponseCode responseCode { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000C458 File Offset: 0x0000A658
		public string debugMessage { get; }

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C460 File Offset: 0x0000A660
		internal GoogleBillingResult(AndroidJavaObject billingResult)
		{
			if (billingResult != null)
			{
				this.responseCode = (GoogleBillingResponseCode)billingResult.Call<int>("getResponseCode", Array.Empty<object>());
				this.debugMessage = billingResult.Call<string>("getDebugMessage", Array.Empty<object>());
			}
		}
	}
}
