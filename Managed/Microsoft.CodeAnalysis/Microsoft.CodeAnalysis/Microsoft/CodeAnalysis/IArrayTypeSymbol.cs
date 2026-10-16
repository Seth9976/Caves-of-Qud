using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200023A RID: 570
	public interface IArrayTypeSymbol : ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600160C RID: 5644
		int Rank { get; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x0600160D RID: 5645
		bool IsSZArray { get; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600160E RID: 5646
		ImmutableArray<int> LowerBounds { get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600160F RID: 5647
		ImmutableArray<int> Sizes { get; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001610 RID: 5648
		[Nullable(1)]
		ITypeSymbol ElementType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001611 RID: 5649
		NullableAnnotation ElementNullableAnnotation { get; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001612 RID: 5650
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001613 RID: 5651
		[NullableContext(2)]
		bool Equals(IArrayTypeSymbol other);
	}
}
