using System;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A3 RID: 163
	internal interface IGooglePurchaseService
	{
		// Token: 0x060003B3 RID: 947
		void Purchase(ProductDefinition product, Product oldProduct, GooglePlayReplacementMode? desiredReplacementMode);
	}
}
