using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A1 RID: 161
	internal interface IGooglePurchaseBuilder
	{
		// Token: 0x060003AB RID: 939
		IEnumerable<IGooglePurchase> BuildPurchases(IEnumerable<AndroidJavaObject> purchases);

		// Token: 0x060003AC RID: 940
		IGooglePurchase BuildPurchase(AndroidJavaObject purchase);
	}
}
