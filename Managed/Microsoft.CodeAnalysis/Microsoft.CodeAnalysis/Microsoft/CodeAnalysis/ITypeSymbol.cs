using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000252 RID: 594
	[NullableContext(1)]
	public interface ITypeSymbol : INamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060016F8 RID: 5880
		TypeKind TypeKind { get; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060016F9 RID: 5881
		[Nullable(2)]
		INamedTypeSymbol BaseType
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060016FA RID: 5882
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> Interfaces
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060016FB RID: 5883
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> AllInterfaces
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060016FC RID: 5884
		bool IsReferenceType { get; }

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060016FD RID: 5885
		bool IsValueType { get; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060016FE RID: 5886
		bool IsAnonymousType { get; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060016FF RID: 5887
		bool IsTupleType { get; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001700 RID: 5888
		bool IsNativeIntegerType { get; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001701 RID: 5889
		ITypeSymbol OriginalDefinition { get; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001702 RID: 5890
		SpecialType SpecialType { get; }

		// Token: 0x06001703 RID: 5891
		[return: Nullable(2)]
		ISymbol FindImplementationForInterfaceMember(ISymbol interfaceMember);

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001704 RID: 5892
		bool IsRefLikeType { get; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001705 RID: 5893
		bool IsUnmanagedType { get; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001706 RID: 5894
		bool IsReadOnly { get; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001707 RID: 5895
		bool IsRecord { get; }

		// Token: 0x06001708 RID: 5896
		string ToDisplayString(NullableFlowState topLevelNullability, [Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x06001709 RID: 5897
		[NullableContext(0)]
		ImmutableArray<SymbolDisplayPart> ToDisplayParts(NullableFlowState topLevelNullability, [Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x0600170A RID: 5898
		string ToMinimalDisplayString(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, [Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x0600170B RID: 5899
		[NullableContext(0)]
		ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts([Nullable(1)] SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, [Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600170C RID: 5900
		NullableAnnotation NullableAnnotation { get; }

		// Token: 0x0600170D RID: 5901
		ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation);
	}
}
