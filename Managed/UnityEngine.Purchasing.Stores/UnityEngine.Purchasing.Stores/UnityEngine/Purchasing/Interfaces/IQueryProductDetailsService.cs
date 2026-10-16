using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A9 RID: 169
	internal interface IQueryProductDetailsService
	{
		// Token: 0x060003BA RID: 954
		void QueryAsyncProduct(ProductDefinition product, Action<List<AndroidJavaObject>, IGoogleBillingResult> onProductDetailsResponse);

		// Token: 0x060003BB RID: 955
		void QueryAsyncProduct(ReadOnlyCollection<ProductDefinition> products, Action<List<AndroidJavaObject>, IGoogleBillingResult> onProductDetailsResponse);

		// Token: 0x060003BC RID: 956
		void QueryAsyncProduct(ReadOnlyCollection<ProductDefinition> products, Action<List<ProductDescription>, IGoogleBillingResult> onProductDetailsResponse);
	}
}
