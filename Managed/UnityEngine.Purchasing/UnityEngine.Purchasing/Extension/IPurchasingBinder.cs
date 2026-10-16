using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Extension
{
	// Token: 0x02000047 RID: 71
	public interface IPurchasingBinder
	{
		// Token: 0x0600013B RID: 315
		void RegisterStore(string name, IStore store);

		// Token: 0x0600013C RID: 316
		void RegisterExtension<T>(T instance) where T : IStoreExtension;

		// Token: 0x0600013D RID: 317
		void RegisterConfiguration<T>(T instance) where T : IStoreConfiguration;

		// Token: 0x0600013E RID: 318
		void SetCatalogProvider(ICatalogProvider provider);

		// Token: 0x0600013F RID: 319
		void SetCatalogProviderFunction(Action<Action<HashSet<ProductDefinition>>> func);
	}
}
