using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005CF RID: 1487
	internal abstract class Symbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x06007C44 RID: 31812
		internal abstract Symbol UnderlyingSymbol { get; }

		// Token: 0x06007C45 RID: 31813 RVA: 0x001C1264 File Offset: 0x001BF464
		protected static ImmutableArray<TypeWithAnnotations> ConstructTypeArguments(ITypeSymbol[] typeArguments)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(typeArguments.Length);
			foreach (ITypeSymbol typeSymbol in typeArguments)
			{
				TypeSymbol typeSymbol2 = typeSymbol.EnsureCSharpSymbolOrNull("typeArguments");
				instance.Add(TypeWithAnnotations.Create(typeSymbol2, (typeSymbol != null) ? typeSymbol.NullableAnnotation.ToInternalAnnotation() : NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x001C12CC File Offset: 0x001BF4CC
		protected static ImmutableArray<TypeWithAnnotations> ConstructTypeArguments(ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
		{
			if (typeArguments.IsDefault)
			{
				throw new ArgumentException("typeArguments");
			}
			int length = typeArguments.Length;
			if (!typeArgumentNullableAnnotations.IsDefault && typeArgumentNullableAnnotations.Length != length)
			{
				throw new ArgumentException("typeArgumentNullableAnnotations");
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(length);
			for (int i = 0; i < length; i++)
			{
				TypeSymbol typeSymbol = typeArguments[i].EnsureCSharpSymbolOrNull("typeArguments");
				NullableAnnotation nullableAnnotation = (typeArgumentNullableAnnotations.IsDefault ? NullableAnnotation.Oblivious : typeArgumentNullableAnnotations[i].ToInternalAnnotation());
				instance.Add(TypeWithAnnotations.Create(typeSymbol, nullableAnnotation, default(ImmutableArray<CustomModifier>)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17002457 RID: 9303
		// (get) Token: 0x06007C47 RID: 31815 RVA: 0x001C1372 File Offset: 0x001BF572
		ISymbol ISymbol.OriginalDefinition
		{
			get
			{
				return this.UnderlyingSymbol.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x06007C48 RID: 31816 RVA: 0x001C1384 File Offset: 0x001BF584
		ISymbol ISymbol.ContainingSymbol
		{
			get
			{
				return this.UnderlyingSymbol.ContainingSymbol.GetPublicSymbol();
			}
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x06007C49 RID: 31817 RVA: 0x001C1396 File Offset: 0x001BF596
		INamedTypeSymbol ISymbol.ContainingType
		{
			get
			{
				return this.UnderlyingSymbol.ContainingType.GetPublicSymbol();
			}
		}

		// Token: 0x06007C4A RID: 31818 RVA: 0x001C13A8 File Offset: 0x001BF5A8
		public sealed override int GetHashCode()
		{
			return this.UnderlyingSymbol.GetHashCode();
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x001C13B5 File Offset: 0x001BF5B5
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as Symbol, SymbolEqualityComparer.Default);
		}

		// Token: 0x06007C4C RID: 31820 RVA: 0x001C13C8 File Offset: 0x001BF5C8
		bool IEquatable<ISymbol>.Equals(ISymbol other)
		{
			return this.Equals(other as Symbol, SymbolEqualityComparer.Default);
		}

		// Token: 0x06007C4D RID: 31821 RVA: 0x001C13DB File Offset: 0x001BF5DB
		bool ISymbol.Equals(ISymbol other, SymbolEqualityComparer equalityComparer)
		{
			return this.Equals(other as Symbol, equalityComparer);
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x001C13EA File Offset: 0x001BF5EA
		protected bool Equals(Symbol other, SymbolEqualityComparer equalityComparer)
		{
			return other != null && this.UnderlyingSymbol.Equals(other.UnderlyingSymbol, equalityComparer.CompareKind);
		}

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x06007C4F RID: 31823 RVA: 0x001C1408 File Offset: 0x001BF608
		ImmutableArray<Location> ISymbol.Locations
		{
			get
			{
				return this.UnderlyingSymbol.Locations;
			}
		}

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x06007C50 RID: 31824 RVA: 0x001C1415 File Offset: 0x001BF615
		ImmutableArray<SyntaxReference> ISymbol.DeclaringSyntaxReferences
		{
			get
			{
				return this.UnderlyingSymbol.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x06007C51 RID: 31825 RVA: 0x001C1422 File Offset: 0x001BF622
		ImmutableArray<AttributeData> ISymbol.GetAttributes()
		{
			return StaticCast<AttributeData>.From<CSharpAttributeData>(this.UnderlyingSymbol.GetAttributes());
		}

		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x06007C52 RID: 31826 RVA: 0x001C1434 File Offset: 0x001BF634
		Accessibility ISymbol.DeclaredAccessibility
		{
			get
			{
				return this.UnderlyingSymbol.DeclaredAccessibility;
			}
		}

		// Token: 0x06007C53 RID: 31827 RVA: 0x001C1441 File Offset: 0x001BF641
		void ISymbol.Accept(SymbolVisitor visitor)
		{
			this.Accept(visitor);
		}

		// Token: 0x06007C54 RID: 31828
		protected abstract void Accept(SymbolVisitor visitor);

		// Token: 0x06007C55 RID: 31829 RVA: 0x001C144A File Offset: 0x001BF64A
		TResult ISymbol.Accept<[global::System.Runtime.CompilerServices.Nullable(2)] TResult>(SymbolVisitor<TResult> visitor)
		{
			return this.Accept<TResult>(visitor);
		}

		// Token: 0x06007C56 RID: 31830
		protected abstract TResult Accept<TResult>(SymbolVisitor<TResult> visitor);

		// Token: 0x06007C57 RID: 31831 RVA: 0x001C1453 File Offset: 0x001BF653
		string ISymbol.GetDocumentationCommentId()
		{
			return this.UnderlyingSymbol.GetDocumentationCommentId();
		}

		// Token: 0x06007C58 RID: 31832 RVA: 0x001C1460 File Offset: 0x001BF660
		string ISymbol.GetDocumentationCommentXml(CultureInfo preferredCulture, bool expandIncludes, CancellationToken cancellationToken)
		{
			return this.UnderlyingSymbol.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x06007C59 RID: 31833 RVA: 0x001C1470 File Offset: 0x001BF670
		string ISymbol.ToDisplayString(SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToDisplayString(this, format);
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x001C1479 File Offset: 0x001BF679
		ImmutableArray<SymbolDisplayPart> ISymbol.ToDisplayParts(SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToDisplayParts(this, format);
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x001C1482 File Offset: 0x001BF682
		string ISymbol.ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToMinimalDisplayString(this, Symbol.GetCSharpSemanticModel(semanticModel), position, format);
		}

		// Token: 0x06007C5C RID: 31836 RVA: 0x001C1492 File Offset: 0x001BF692
		ImmutableArray<SymbolDisplayPart> ISymbol.ToMinimalDisplayParts(SemanticModel semanticModel, int position, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToMinimalDisplayParts(this, Symbol.GetCSharpSemanticModel(semanticModel), position, format);
		}

		// Token: 0x06007C5D RID: 31837 RVA: 0x001C14A2 File Offset: 0x001BF6A2
		internal static CSharpSemanticModel GetCSharpSemanticModel(SemanticModel semanticModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				throw new ArgumentException(CSharpResources.WrongSemanticModelType, "C#");
			}
			return csharpSemanticModel;
		}

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x06007C5E RID: 31838 RVA: 0x001C14BD File Offset: 0x001BF6BD
		SymbolKind ISymbol.Kind
		{
			get
			{
				return this.UnderlyingSymbol.Kind;
			}
		}

		// Token: 0x1700245E RID: 9310
		// (get) Token: 0x06007C5F RID: 31839 RVA: 0x001C14CA File Offset: 0x001BF6CA
		string ISymbol.Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x1700245F RID: 9311
		// (get) Token: 0x06007C60 RID: 31840 RVA: 0x001C14D1 File Offset: 0x001BF6D1
		string ISymbol.Name
		{
			get
			{
				return this.UnderlyingSymbol.Name;
			}
		}

		// Token: 0x17002460 RID: 9312
		// (get) Token: 0x06007C61 RID: 31841 RVA: 0x001C14DE File Offset: 0x001BF6DE
		string ISymbol.MetadataName
		{
			get
			{
				return this.UnderlyingSymbol.MetadataName;
			}
		}

		// Token: 0x17002461 RID: 9313
		// (get) Token: 0x06007C62 RID: 31842 RVA: 0x001C14EB File Offset: 0x001BF6EB
		IAssemblySymbol ISymbol.ContainingAssembly
		{
			get
			{
				return this.UnderlyingSymbol.ContainingAssembly.GetPublicSymbol();
			}
		}

		// Token: 0x17002462 RID: 9314
		// (get) Token: 0x06007C63 RID: 31843 RVA: 0x001C14FD File Offset: 0x001BF6FD
		IModuleSymbol ISymbol.ContainingModule
		{
			get
			{
				return this.UnderlyingSymbol.ContainingModule.GetPublicSymbol();
			}
		}

		// Token: 0x17002463 RID: 9315
		// (get) Token: 0x06007C64 RID: 31844 RVA: 0x001C150F File Offset: 0x001BF70F
		INamespaceSymbol ISymbol.ContainingNamespace
		{
			get
			{
				return this.UnderlyingSymbol.ContainingNamespace.GetPublicSymbol();
			}
		}

		// Token: 0x17002464 RID: 9316
		// (get) Token: 0x06007C65 RID: 31845 RVA: 0x001C1521 File Offset: 0x001BF721
		bool ISymbol.IsDefinition
		{
			get
			{
				return this.UnderlyingSymbol.IsDefinition;
			}
		}

		// Token: 0x17002465 RID: 9317
		// (get) Token: 0x06007C66 RID: 31846 RVA: 0x001C152E File Offset: 0x001BF72E
		bool ISymbol.IsStatic
		{
			get
			{
				return this.UnderlyingSymbol.IsStatic;
			}
		}

		// Token: 0x17002466 RID: 9318
		// (get) Token: 0x06007C67 RID: 31847 RVA: 0x001C153B File Offset: 0x001BF73B
		bool ISymbol.IsVirtual
		{
			get
			{
				return this.UnderlyingSymbol.IsVirtual;
			}
		}

		// Token: 0x17002467 RID: 9319
		// (get) Token: 0x06007C68 RID: 31848 RVA: 0x001C1548 File Offset: 0x001BF748
		bool ISymbol.IsOverride
		{
			get
			{
				return this.UnderlyingSymbol.IsOverride;
			}
		}

		// Token: 0x17002468 RID: 9320
		// (get) Token: 0x06007C69 RID: 31849 RVA: 0x001C1555 File Offset: 0x001BF755
		bool ISymbol.IsAbstract
		{
			get
			{
				return this.UnderlyingSymbol.IsAbstract;
			}
		}

		// Token: 0x17002469 RID: 9321
		// (get) Token: 0x06007C6A RID: 31850 RVA: 0x001C1562 File Offset: 0x001BF762
		bool ISymbol.IsSealed
		{
			get
			{
				return this.UnderlyingSymbol.IsSealed;
			}
		}

		// Token: 0x1700246A RID: 9322
		// (get) Token: 0x06007C6B RID: 31851 RVA: 0x001C156F File Offset: 0x001BF76F
		bool ISymbol.IsExtern
		{
			get
			{
				return this.UnderlyingSymbol.IsExtern;
			}
		}

		// Token: 0x1700246B RID: 9323
		// (get) Token: 0x06007C6C RID: 31852 RVA: 0x001C157C File Offset: 0x001BF77C
		bool ISymbol.IsImplicitlyDeclared
		{
			get
			{
				return this.UnderlyingSymbol.IsImplicitlyDeclared;
			}
		}

		// Token: 0x1700246C RID: 9324
		// (get) Token: 0x06007C6D RID: 31853 RVA: 0x001C1589 File Offset: 0x001BF789
		bool ISymbol.CanBeReferencedByName
		{
			get
			{
				return this.UnderlyingSymbol.CanBeReferencedByName;
			}
		}

		// Token: 0x1700246D RID: 9325
		// (get) Token: 0x06007C6E RID: 31854 RVA: 0x001C1596 File Offset: 0x001BF796
		bool ISymbol.HasUnsupportedMetadata
		{
			get
			{
				return this.UnderlyingSymbol.HasUnsupportedMetadata;
			}
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x001C15A3 File Offset: 0x001BF7A3
		public sealed override string ToString()
		{
			return SymbolDisplay.ToDisplayString(this, null);
		}
	}
}
