using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200024F RID: 591
	[InternalImplementationOnly]
	public interface ISymbol : IEquatable<ISymbol>
	{
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060016C0 RID: 5824
		SymbolKind Kind { get; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060016C1 RID: 5825
		[Nullable(1)]
		string Language
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060016C2 RID: 5826
		[Nullable(1)]
		string Name
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060016C3 RID: 5827
		[Nullable(1)]
		string MetadataName
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060016C4 RID: 5828
		ISymbol ContainingSymbol { get; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060016C5 RID: 5829
		IAssemblySymbol ContainingAssembly { get; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060016C6 RID: 5830
		IModuleSymbol ContainingModule { get; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060016C7 RID: 5831
		INamedTypeSymbol ContainingType { get; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060016C8 RID: 5832
		INamespaceSymbol ContainingNamespace { get; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060016C9 RID: 5833
		bool IsDefinition { get; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060016CA RID: 5834
		bool IsStatic { get; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060016CB RID: 5835
		bool IsVirtual { get; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060016CC RID: 5836
		bool IsOverride { get; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060016CD RID: 5837
		bool IsAbstract { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060016CE RID: 5838
		bool IsSealed { get; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060016CF RID: 5839
		bool IsExtern { get; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060016D0 RID: 5840
		bool IsImplicitlyDeclared { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060016D1 RID: 5841
		bool CanBeReferencedByName { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060016D2 RID: 5842
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<Location> Locations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060016D3 RID: 5843
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060016D4 RID: 5844
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<AttributeData> GetAttributes();

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060016D5 RID: 5845
		Accessibility DeclaredAccessibility { get; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060016D6 RID: 5846
		[Nullable(1)]
		ISymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x060016D7 RID: 5847
		[NullableContext(1)]
		void Accept(SymbolVisitor visitor);

		// Token: 0x060016D8 RID: 5848
		[NullableContext(2)]
		TResult Accept<TResult>([Nullable(1)] SymbolVisitor<TResult> visitor);

		// Token: 0x060016D9 RID: 5849
		[NullableContext(2)]
		string GetDocumentationCommentId();

		// Token: 0x060016DA RID: 5850
		[NullableContext(2)]
		string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060016DB RID: 5851
		[NullableContext(1)]
		string ToDisplayString([Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x060016DC RID: 5852
		ImmutableArray<SymbolDisplayPart> ToDisplayParts([Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x060016DD RID: 5853
		[NullableContext(1)]
		string ToMinimalDisplayString(SemanticModel semanticModel, int position, [Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x060016DE RID: 5854
		ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts([Nullable(1)] SemanticModel semanticModel, int position, [Nullable(2)] SymbolDisplayFormat format = null);

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060016DF RID: 5855
		bool HasUnsupportedMetadata { get; }

		// Token: 0x060016E0 RID: 5856
		[NullableContext(1)]
		bool Equals([Nullable(2)] [NotNullWhen(true)] ISymbol other, SymbolEqualityComparer equalityComparer);
	}
}
