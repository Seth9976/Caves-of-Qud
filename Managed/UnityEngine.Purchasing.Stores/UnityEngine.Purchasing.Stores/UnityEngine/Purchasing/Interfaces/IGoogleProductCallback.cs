using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x0200009F RID: 159
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IGoogleProductCallback
	{
		// Token: 0x0600039C RID: 924
		void SetStoreConfiguration(IGooglePlayConfigurationInternal configuration);

		// Token: 0x0600039D RID: 925
		void NotifyQueryProductDetailsFailed(int retryCount);
	}
}
