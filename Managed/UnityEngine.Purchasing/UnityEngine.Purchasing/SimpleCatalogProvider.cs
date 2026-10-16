using System;
using System.Collections.Generic;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002A RID: 42
	internal class SimpleCatalogProvider : ICatalogProvider
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003667 File Offset: 0x00001867
		internal SimpleCatalogProvider(Action<Action<HashSet<ProductDefinition>>> func)
		{
			this.m_Func = func;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003676 File Offset: 0x00001876
		public void FetchProducts(Action<HashSet<ProductDefinition>> callback)
		{
			Action<Action<HashSet<ProductDefinition>>> func = this.m_Func;
			if (func == null)
			{
				return;
			}
			func(callback);
		}

		// Token: 0x04000061 RID: 97
		private readonly Action<Action<HashSet<ProductDefinition>>> m_Func;
	}
}
