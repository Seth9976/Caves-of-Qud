using System;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002C0 RID: 704
	internal interface INamespaceSymbolInternal : INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001D97 RID: 7575
		bool IsGlobalNamespace { get; }
	}
}
