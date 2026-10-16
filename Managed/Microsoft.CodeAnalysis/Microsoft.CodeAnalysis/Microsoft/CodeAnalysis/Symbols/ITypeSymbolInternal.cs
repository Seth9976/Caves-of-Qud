using System;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002C6 RID: 710
	internal interface ITypeSymbolInternal : INamespaceOrTypeSymbolInternal, ISymbolInternal
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001DB2 RID: 7602
		TypeKind TypeKind { get; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001DB3 RID: 7603
		SpecialType SpecialType { get; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001DB4 RID: 7604
		bool IsReferenceType { get; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001DB5 RID: 7605
		bool IsValueType { get; }

		// Token: 0x06001DB6 RID: 7606
		ITypeSymbol GetITypeSymbol();
	}
}
