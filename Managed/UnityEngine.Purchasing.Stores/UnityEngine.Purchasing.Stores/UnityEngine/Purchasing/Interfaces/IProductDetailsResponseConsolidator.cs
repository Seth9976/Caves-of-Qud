using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A8 RID: 168
	internal interface IProductDetailsResponseConsolidator
	{
		// Token: 0x060003B9 RID: 953
		void Consolidate(IGoogleBillingResult billingResult, IEnumerable<AndroidJavaObject> productDetails);
	}
}
