using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002C4 RID: 708
	[NullableContext(2)]
	internal interface ISynthesizedMethodBodyImplementationSymbol : ISymbolInternal
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001DB0 RID: 7600
		IMethodSymbolInternal Method { get; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001DB1 RID: 7601
		bool HasMethodBodyDependency { get; }
	}
}
