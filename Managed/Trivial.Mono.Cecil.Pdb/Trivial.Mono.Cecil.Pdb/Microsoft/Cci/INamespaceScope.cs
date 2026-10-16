using System;
using System.Collections.Generic;

namespace Microsoft.Cci
{
	// Token: 0x02000013 RID: 19
	public interface INamespaceScope
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600014F RID: 335
		IEnumerable<IUsedNamespace> UsedNamespaces { get; }
	}
}
