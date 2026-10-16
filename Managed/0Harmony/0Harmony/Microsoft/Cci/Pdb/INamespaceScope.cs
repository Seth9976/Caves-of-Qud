using System;
using System.Collections.Generic;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F9 RID: 1017
	internal interface INamespaceScope
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060015A9 RID: 5545
		IEnumerable<IUsedNamespace> UsedNamespaces { get; }
	}
}
