using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000AA RID: 170
	internal interface IProductDetailsQueryResponse
	{
		// Token: 0x060003BD RID: 957
		void AddResponse(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> productDetails);

		// Token: 0x060003BE RID: 958
		List<AndroidJavaObject> ProductDetails();

		// Token: 0x060003BF RID: 959
		bool IsRecoverable();

		// Token: 0x060003C0 RID: 960
		IGoogleBillingResult GetGoogleBillingResult();
	}
}
