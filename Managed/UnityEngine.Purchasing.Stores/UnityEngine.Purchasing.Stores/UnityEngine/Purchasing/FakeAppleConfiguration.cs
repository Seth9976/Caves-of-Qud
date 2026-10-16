using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200005A RID: 90
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class FakeAppleConfiguration : IAppleConfiguration, IStoreConfiguration
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007220 File Offset: 0x00005420
		public string appReceipt
		{
			get
			{
				return "This is a fake receipt. When running on an Apple store, a base64 encoded App Receipt would be returned";
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007227 File Offset: 0x00005427
		public bool canMakePayments
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000722A File Offset: 0x0000542A
		public void SetApplePromotionalPurchaseInterceptorCallback(Action<Product> callback)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000722C File Offset: 0x0000542C
		public void SetEntitlementsRevokedListener(Action<List<Product>> callback)
		{
		}
	}
}
