using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing.Models
{
	// Token: 0x020000B5 RID: 181
	internal class ProductDescriptionQuery
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000C77A File Offset: 0x0000A97A
		internal ProductDescriptionQuery(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductsReceived, Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode> onRetrieveProductsFailed)
		{
			this.products = products;
			this.onProductsReceived = onProductsReceived;
			this.onRetrieveProductsFailed = onRetrieveProductsFailed;
		}

		// Token: 0x04000214 RID: 532
		internal ReadOnlyCollection<ProductDefinition> products;

		// Token: 0x04000215 RID: 533
		internal Action<List<ProductDescription>, IGoogleBillingResult> onProductsReceived;

		// Token: 0x04000216 RID: 534
		internal Action<GoogleRetrieveProductsFailureReason, GoogleBillingResponseCode> onRetrieveProductsFailed;
	}
}
