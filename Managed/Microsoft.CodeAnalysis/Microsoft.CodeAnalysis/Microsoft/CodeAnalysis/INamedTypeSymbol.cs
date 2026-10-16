using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000246 RID: 582
	[NullableContext(2)]
	public interface INamedTypeSymbol : ITypeSymbol, INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001676 RID: 5750
		int Arity { get; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001677 RID: 5751
		bool IsGenericType { get; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001678 RID: 5752
		bool IsUnboundGenericType { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001679 RID: 5753
		bool IsScriptClass { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600167A RID: 5754
		bool IsImplicitClass { get; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600167B RID: 5755
		bool IsComImport { get; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600167C RID: 5756
		[Nullable(1)]
		IEnumerable<string> MemberNames
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600167D RID: 5757
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeParameterSymbol> TypeParameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600167E RID: 5758
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeSymbol> TypeArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600167F RID: 5759
		[Nullable(0)]
		ImmutableArray<NullableAnnotation> TypeArgumentNullableAnnotations
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x06001680 RID: 5760
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> GetTypeArgumentCustomModifiers(int ordinal);

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001681 RID: 5761
		[Nullable(1)]
		INamedTypeSymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001682 RID: 5762
		IMethodSymbol DelegateInvokeMethod { get; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001683 RID: 5763
		INamedTypeSymbol EnumUnderlyingType { get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001684 RID: 5764
		[Nullable(1)]
		INamedTypeSymbol ConstructedFrom
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06001685 RID: 5765
		[NullableContext(1)]
		INamedTypeSymbol Construct(params ITypeSymbol[] typeArguments);

		// Token: 0x06001686 RID: 5766
		[NullableContext(0)]
		[return: Nullable(1)]
		INamedTypeSymbol Construct([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations);

		// Token: 0x06001687 RID: 5767
		[NullableContext(1)]
		INamedTypeSymbol ConstructUnboundGenericType();

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001688 RID: 5768
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IMethodSymbol> InstanceConstructors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001689 RID: 5769
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IMethodSymbol> StaticConstructors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600168A RID: 5770
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IMethodSymbol> Constructors
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600168B RID: 5771
		ISymbol AssociatedSymbol { get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600168C RID: 5772
		bool MightContainExtensionMethods { get; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600168D RID: 5773
		INamedTypeSymbol TupleUnderlyingType { get; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600168E RID: 5774
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IFieldSymbol> TupleElements
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600168F RID: 5775
		bool IsSerializable { get; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001690 RID: 5776
		INamedTypeSymbol NativeIntegerUnderlyingType { get; }
	}
}
