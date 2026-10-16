using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x0200008F RID: 143
	[NullableContext(1)]
	internal interface IImportScope
	{
		// Token: 0x060003B5 RID: 949
		[NullableContext(0)]
		ImmutableArray<UsedNamespaceOrType> GetUsedNamespaces();

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003B6 RID: 950
		IImportScope Parent { get; }
	}
}
