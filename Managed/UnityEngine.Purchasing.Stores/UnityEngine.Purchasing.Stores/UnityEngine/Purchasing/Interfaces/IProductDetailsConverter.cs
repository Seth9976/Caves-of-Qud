using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A7 RID: 167
	internal interface IProductDetailsConverter
	{
		// Token: 0x060003B8 RID: 952
		List<ProductDescription> ConvertOnQueryProductDetailsResponse(IEnumerable<AndroidJavaObject> productDetails);
	}
}
