using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000241 RID: 577
	public interface IFunctionPointerTypeSymbol : ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600163A RID: 5690
		[Nullable(1)]
		IMethodSymbol Signature
		{
			[NullableContext(1)]
			get;
		}
	}
}
