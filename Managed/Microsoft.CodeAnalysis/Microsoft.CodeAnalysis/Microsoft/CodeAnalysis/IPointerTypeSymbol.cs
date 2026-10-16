using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200024A RID: 586
	public interface IPointerTypeSymbol : ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060016AC RID: 5804
		[Nullable(1)]
		ITypeSymbol PointedAtType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060016AD RID: 5805
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
