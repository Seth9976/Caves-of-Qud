using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000251 RID: 593
	[NullableContext(2)]
	public interface ITypeParameterSymbol : ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060016E9 RID: 5865
		int Ordinal { get; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060016EA RID: 5866
		VarianceKind Variance { get; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060016EB RID: 5867
		TypeParameterKind TypeParameterKind { get; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060016EC RID: 5868
		IMethodSymbol DeclaringMethod { get; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060016ED RID: 5869
		INamedTypeSymbol DeclaringType { get; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060016EE RID: 5870
		bool HasReferenceTypeConstraint { get; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060016EF RID: 5871
		NullableAnnotation ReferenceTypeConstraintNullableAnnotation { get; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060016F0 RID: 5872
		bool HasValueTypeConstraint { get; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060016F1 RID: 5873
		bool HasUnmanagedTypeConstraint { get; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060016F2 RID: 5874
		bool HasNotNullConstraint { get; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060016F3 RID: 5875
		bool HasConstructorConstraint { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060016F4 RID: 5876
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeSymbol> ConstraintTypes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060016F5 RID: 5877
		[Nullable(0)]
		ImmutableArray<NullableAnnotation> ConstraintNullableAnnotations
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060016F6 RID: 5878
		[Nullable(1)]
		ITypeParameterSymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060016F7 RID: 5879
		ITypeParameterSymbol ReducedFrom { get; }
	}
}
