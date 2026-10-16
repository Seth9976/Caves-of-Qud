using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200000B RID: 11
	public interface IAmazonConfiguration : IStoreConfiguration
	{
		// Token: 0x06000019 RID: 25
		void WriteSandboxJSON(HashSet<ProductDefinition> products);
	}
}
