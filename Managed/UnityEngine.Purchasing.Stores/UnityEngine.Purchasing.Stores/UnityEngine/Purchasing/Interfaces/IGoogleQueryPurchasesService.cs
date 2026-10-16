using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A6 RID: 166
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IGoogleQueryPurchasesService
	{
		// Token: 0x060003B6 RID: 950
		Task<List<IGooglePurchase>> QueryPurchases();

		// Token: 0x060003B7 RID: 951
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		IGooglePurchase GetPurchaseByToken(string token, string skuType);
	}
}
