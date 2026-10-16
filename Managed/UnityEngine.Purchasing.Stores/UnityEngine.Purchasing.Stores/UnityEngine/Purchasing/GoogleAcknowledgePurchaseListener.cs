using System;
using UnityEngine.Purchasing.Models;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001A RID: 26
	internal class GoogleAcknowledgePurchaseListener : AndroidJavaProxy
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002FB6 File Offset: 0x000011B6
		internal GoogleAcknowledgePurchaseListener(Action<IGoogleBillingResult> onAcknowledgePurchaseResponseAction)
			: base("com.android.billingclient.api.AcknowledgePurchaseResponseListener")
		{
			this.m_OnAcknowledgePurchaseResponse = onAcknowledgePurchaseResponseAction;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002FCA File Offset: 0x000011CA
		[Preserve]
		private void onAcknowledgePurchaseResponse(AndroidJavaObject billingResult)
		{
			this.m_OnAcknowledgePurchaseResponse(new GoogleBillingResult(billingResult));
			billingResult.Dispose();
		}

		// Token: 0x0400002F RID: 47
		private const string k_AndroidAcknowledgePurchaseResponseListenerClassName = "com.android.billingclient.api.AcknowledgePurchaseResponseListener";

		// Token: 0x04000030 RID: 48
		private readonly Action<IGoogleBillingResult> m_OnAcknowledgePurchaseResponse;
	}
}
