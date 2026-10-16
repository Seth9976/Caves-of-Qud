using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000090 RID: 144
	public interface IMicrosoftConfiguration : IStoreConfiguration
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000337 RID: 823
		// (set) Token: 0x06000338 RID: 824
		bool useMockBillingSystem { get; set; }
	}
}
