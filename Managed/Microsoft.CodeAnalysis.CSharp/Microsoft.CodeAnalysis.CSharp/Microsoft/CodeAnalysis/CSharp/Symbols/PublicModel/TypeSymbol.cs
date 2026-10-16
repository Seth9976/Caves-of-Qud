using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005D1 RID: 1489
	internal abstract class TypeSymbol : NamespaceOrTypeSymbol, ISymbol, IEquatable<ISymbol>, ITypeSymbol, INamespaceOrTypeSymbol
	{
		// Token: 0x06007C88 RID: 31880 RVA: 0x001C1721 File Offset: 0x001BF921
		protected TypeSymbol(NullableAnnotation nullableAnnotation)
		{
			this.NullableAnnotation = nullableAnnotation;
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x06007C89 RID: 31881 RVA: 0x001C1730 File Offset: 0x001BF930
		protected NullableAnnotation NullableAnnotation { get; }

		// Token: 0x06007C8A RID: 31882
		protected abstract ITypeSymbol WithNullableAnnotation(NullableAnnotation nullableAnnotation);

		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x06007C8B RID: 31883
		internal abstract TypeSymbol UnderlyingTypeSymbol { get; }

		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06007C8C RID: 31884 RVA: 0x001C1738 File Offset: 0x001BF938
		NullableAnnotation ITypeSymbol.NullableAnnotation
		{
			get
			{
				return this.NullableAnnotation;
			}
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x001C1740 File Offset: 0x001BF940
		ITypeSymbol ITypeSymbol.WithNullableAnnotation(NullableAnnotation nullableAnnotation)
		{
			if (this.NullableAnnotation == nullableAnnotation)
			{
				return this;
			}
			if (nullableAnnotation == this.UnderlyingTypeSymbol.DefaultNullableAnnotation)
			{
				return (ITypeSymbol)this.UnderlyingSymbol.ISymbol;
			}
			return this.WithNullableAnnotation(nullableAnnotation);
		}

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06007C8E RID: 31886 RVA: 0x001C1773 File Offset: 0x001BF973
		bool ISymbol.IsDefinition
		{
			get
			{
				return this == ((ISymbol)this).OriginalDefinition;
			}
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x001C177E File Offset: 0x001BF97E
		bool ISymbol.Equals(ISymbol other, SymbolEqualityComparer equalityComparer)
		{
			return this.Equals(other as TypeSymbol, equalityComparer);
		}

		// Token: 0x06007C90 RID: 31888 RVA: 0x001C1790 File Offset: 0x001BF990
		protected bool Equals(TypeSymbol otherType, SymbolEqualityComparer equalityComparer)
		{
			if (otherType == null)
			{
				return false;
			}
			if (otherType == this)
			{
				return true;
			}
			TypeCompareKind compareKind = equalityComparer.CompareKind;
			return (this.NullableAnnotation == otherType.NullableAnnotation || (compareKind & TypeCompareKind.IgnoreNullableModifiersForReferenceTypes) != TypeCompareKind.ConsiderEverything || ((compareKind & TypeCompareKind.ObliviousNullableModifierMatchesAny) != TypeCompareKind.ConsiderEverything && (this.NullableAnnotation == NullableAnnotation.None || otherType.NullableAnnotation == NullableAnnotation.None)) || (this.UnderlyingTypeSymbol.IsValueType && !this.UnderlyingTypeSymbol.IsNullableType())) && this.UnderlyingTypeSymbol.Equals(otherType.UnderlyingTypeSymbol, compareKind);
		}

		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06007C91 RID: 31889 RVA: 0x001C1806 File Offset: 0x001BFA06
		ITypeSymbol ITypeSymbol.OriginalDefinition
		{
			get
			{
				return this.UnderlyingTypeSymbol.OriginalDefinition.GetPublicSymbol();
			}
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x06007C92 RID: 31890 RVA: 0x001C1818 File Offset: 0x001BFA18
		INamedTypeSymbol ITypeSymbol.BaseType
		{
			get
			{
				return this.UnderlyingTypeSymbol.BaseTypeNoUseSiteDiagnostics.GetPublicSymbol();
			}
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x06007C93 RID: 31891 RVA: 0x001C182A File Offset: 0x001BFA2A
		ImmutableArray<INamedTypeSymbol> ITypeSymbol.Interfaces
		{
			get
			{
				return this.UnderlyingTypeSymbol.InterfacesNoUseSiteDiagnostics(null).GetPublicSymbols();
			}
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x06007C94 RID: 31892 RVA: 0x001C183D File Offset: 0x001BFA3D
		ImmutableArray<INamedTypeSymbol> ITypeSymbol.AllInterfaces
		{
			get
			{
				return this.UnderlyingTypeSymbol.AllInterfacesNoUseSiteDiagnostics.GetPublicSymbols();
			}
		}

		// Token: 0x06007C95 RID: 31893 RVA: 0x001C1850 File Offset: 0x001BFA50
		ISymbol ITypeSymbol.FindImplementationForInterfaceMember(ISymbol interfaceMember)
		{
			Symbol symbol = interfaceMember as Symbol;
			if (symbol == null)
			{
				return null;
			}
			return this.UnderlyingTypeSymbol.FindImplementationForInterfaceMember(symbol.UnderlyingSymbol).GetPublicSymbol();
		}

		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x06007C96 RID: 31894 RVA: 0x001C187F File Offset: 0x001BFA7F
		bool ITypeSymbol.IsUnmanagedType
		{
			get
			{
				return !this.UnderlyingTypeSymbol.IsManagedTypeNoUseSiteDiagnostics;
			}
		}

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x06007C97 RID: 31895 RVA: 0x001C188F File Offset: 0x001BFA8F
		bool ITypeSymbol.IsReferenceType
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsReferenceType;
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x06007C98 RID: 31896 RVA: 0x001C189C File Offset: 0x001BFA9C
		bool ITypeSymbol.IsValueType
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsValueType;
			}
		}

		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x06007C99 RID: 31897 RVA: 0x001C18A9 File Offset: 0x001BFAA9
		TypeKind ITypeSymbol.TypeKind
		{
			get
			{
				return this.UnderlyingTypeSymbol.TypeKind;
			}
		}

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x06007C9A RID: 31898 RVA: 0x001C18B6 File Offset: 0x001BFAB6
		bool ITypeSymbol.IsTupleType
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsTupleType;
			}
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x06007C9B RID: 31899 RVA: 0x001C18C3 File Offset: 0x001BFAC3
		bool ITypeSymbol.IsNativeIntegerType
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsNativeIntegerType;
			}
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x001C18D0 File Offset: 0x001BFAD0
		string ITypeSymbol.ToDisplayString(NullableFlowState topLevelNullability, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToDisplayString(this, topLevelNullability, format);
		}

		// Token: 0x06007C9D RID: 31901 RVA: 0x001C18DA File Offset: 0x001BFADA
		ImmutableArray<SymbolDisplayPart> ITypeSymbol.ToDisplayParts(NullableFlowState topLevelNullability, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToDisplayParts(this, topLevelNullability, format);
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x001C18E4 File Offset: 0x001BFAE4
		string ITypeSymbol.ToMinimalDisplayString(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToMinimalDisplayString(this, topLevelNullability, semanticModel, position, format);
		}

		// Token: 0x06007C9F RID: 31903 RVA: 0x001C18F1 File Offset: 0x001BFAF1
		ImmutableArray<SymbolDisplayPart> ITypeSymbol.ToMinimalDisplayParts(SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToMinimalDisplayParts(this, topLevelNullability, semanticModel, position, format);
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x06007CA0 RID: 31904 RVA: 0x001C18FE File Offset: 0x001BFAFE
		bool ITypeSymbol.IsAnonymousType
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsAnonymousType;
			}
		}

		// Token: 0x17002490 RID: 9360
		// (get) Token: 0x06007CA1 RID: 31905 RVA: 0x001C190B File Offset: 0x001BFB0B
		SpecialType ITypeSymbol.SpecialType
		{
			get
			{
				return this.UnderlyingTypeSymbol.SpecialType;
			}
		}

		// Token: 0x17002491 RID: 9361
		// (get) Token: 0x06007CA2 RID: 31906 RVA: 0x001C1918 File Offset: 0x001BFB18
		bool ITypeSymbol.IsRefLikeType
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsRefLikeType;
			}
		}

		// Token: 0x17002492 RID: 9362
		// (get) Token: 0x06007CA3 RID: 31907 RVA: 0x001C1925 File Offset: 0x001BFB25
		bool ITypeSymbol.IsReadOnly
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsReadOnly;
			}
		}

		// Token: 0x17002493 RID: 9363
		// (get) Token: 0x06007CA4 RID: 31908 RVA: 0x001C1932 File Offset: 0x001BFB32
		bool ITypeSymbol.IsRecord
		{
			get
			{
				return this.UnderlyingTypeSymbol.IsRecord || this.UnderlyingTypeSymbol.IsRecordStruct;
			}
		}
	}
}
