using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Purchasing.Models;
using UnityEngine.Scripting;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001D RID: 29
	internal class GooglePurchasesResponseListener : AndroidJavaProxy
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003547 File Offset: 0x00001747
		internal GooglePurchasesResponseListener(Action<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> onQueryPurchasesResponse)
			: base("com.android.billingclient.api.PurchasesResponseListener")
		{
			this.m_OnQueryPurchasesResponse = onQueryPurchasesResponse;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000355C File Offset: 0x0000175C
		[Preserve]
		public void onQueryPurchasesResponse(AndroidJavaObject billingResult, AndroidJavaObject purchases)
		{
			List<AndroidJavaObject> list = purchases.Enumerate().ToList<AndroidJavaObject>();
			this.m_OnQueryPurchasesResponse(new GoogleBillingResult(billingResult), list);
			foreach (AndroidJavaObject androidJavaObject in list)
			{
				if (androidJavaObject != null)
				{
					androidJavaObject.Dispose();
				}
			}
			billingResult.Dispose();
			if (purchases != null)
			{
				purchases.Dispose();
			}
		}

		// Token: 0x0400003A RID: 58
		private const string k_AndroidPurchasesResponseListenerClassName = "com.android.billingclient.api.PurchasesResponseListener";

		// Token: 0x0400003B RID: 59
		private readonly Action<IGoogleBillingResult, IEnumerable<AndroidJavaObject>> m_OnQueryPurchasesResponse;
	}
}
