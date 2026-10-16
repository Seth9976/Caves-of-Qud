using System;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002BE RID: 702
	internal interface INamedTypeSymbolInternal : ITypeSymbolInternal, INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001D96 RID: 7574
		INamedTypeSymbolInternal EnumUnderlyingType { get; }
	}
}
