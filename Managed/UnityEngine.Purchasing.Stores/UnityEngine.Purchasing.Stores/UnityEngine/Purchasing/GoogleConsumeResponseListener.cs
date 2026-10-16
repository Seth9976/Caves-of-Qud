using System;
using UnityEngine.Purchasing.Models;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001B RID: 27
	internal class GoogleConsumeResponseListener : AndroidJavaProxy
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002FE3 File Offset: 0x000011E3
		internal GoogleConsumeResponseListener(Action<IGoogleBillingResult> onConsumeResponseAction)
			: base("com.android.billingclient.api.ConsumeResponseListener")
		{
			this.m_OnConsumeResponse = onConsumeResponseAction;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FF7 File Offset: 0x000011F7
		[Preserve]
		private void onConsumeResponse(AndroidJavaObject billingResult, string purchaseToken)
		{
			this.m_OnConsumeResponse(new GoogleBillingResult(billingResult));
			billingResult.Dispose();
		}

		// Token: 0x04000031 RID: 49
		private const string k_AndroidConsumeResponseListenerClassName = "com.android.billingclient.api.ConsumeResponseListener";

		// Token: 0x04000032 RID: 50
		private readonly Action<IGoogleBillingResult> m_OnConsumeResponse;
	}
}
