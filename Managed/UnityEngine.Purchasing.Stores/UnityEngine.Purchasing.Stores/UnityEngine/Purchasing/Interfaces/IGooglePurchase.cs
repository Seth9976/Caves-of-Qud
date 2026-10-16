using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x020000A0 RID: 160
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IGooglePurchase
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600039E RID: 926
		int purchaseState { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600039F RID: 927
		List<string> skus { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003A0 RID: 928
		string orderId { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003A1 RID: 929
		string receipt { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003A2 RID: 930
		string signature { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003A3 RID: 931
		[global::System.Runtime.CompilerServices.Nullable(2)]
		string obfuscatedAccountId
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003A4 RID: 932
		[global::System.Runtime.CompilerServices.Nullable(2)]
		string obfuscatedProfileId
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060003A5 RID: 933
		string originalJson { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003A6 RID: 934
		string purchaseToken { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060003A7 RID: 935
		[global::System.Runtime.CompilerServices.Nullable(2)]
		string sku
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x060003A8 RID: 936
		bool IsAcknowledged();

		// Token: 0x060003A9 RID: 937
		bool IsPurchased();

		// Token: 0x060003AA RID: 938
		bool IsPending();
	}
}
