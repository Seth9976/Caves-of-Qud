using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200024E RID: 590
	public interface ISourceAssemblySymbol : IAssemblySymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060016BF RID: 5823
		[Nullable(1)]
		Compilation Compilation
		{
			[NullableContext(1)]
			get;
		}
	}
}
