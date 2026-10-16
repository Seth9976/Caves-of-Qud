using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.Cci
{
	// Token: 0x020000D6 RID: 214
	[NullableContext(1)]
	internal interface INamespace : INamedEntity
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600071E RID: 1822
		INamespace ContainingNamespace { get; }

		// Token: 0x0600071F RID: 1823
		INamespaceSymbolInternal GetInternalSymbol();
	}
}
