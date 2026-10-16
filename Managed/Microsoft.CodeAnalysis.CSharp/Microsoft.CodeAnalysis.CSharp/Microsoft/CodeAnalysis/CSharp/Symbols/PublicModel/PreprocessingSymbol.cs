using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005CB RID: 1483
	internal sealed class PreprocessingSymbol : IPreprocessingSymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x06007C01 RID: 31745 RVA: 0x001C0F7A File Offset: 0x001BF17A
		internal PreprocessingSymbol(string name)
		{
			this._name = name;
		}

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06007C02 RID: 31746 RVA: 0x001C0F89 File Offset: 0x001BF189
		ISymbol ISymbol.OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x06007C03 RID: 31747 RVA: 0x001C0F8C File Offset: 0x001BF18C
		ISymbol ISymbol.ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x06007C04 RID: 31748 RVA: 0x001C0F8F File Offset: 0x001BF18F
		INamedTypeSymbol ISymbol.ContainingType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x001C0F92 File Offset: 0x001BF192
		public sealed override int GetHashCode()
		{
			return this._name.GetHashCode();
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x001C0FA0 File Offset: 0x001BF1A0
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			PreprocessingSymbol preprocessingSymbol = obj as PreprocessingSymbol;
			return preprocessingSymbol != null && this._name.Equals(preprocessingSymbol._name);
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x001C0FD5 File Offset: 0x001BF1D5
		bool IEquatable<ISymbol>.Equals(ISymbol other)
		{
			return this.Equals(other);
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x001C0FDE File Offset: 0x001BF1DE
		bool ISymbol.Equals(ISymbol other, SymbolEqualityComparer equalityComparer)
		{
			return this.Equals(other);
		}

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x06007C09 RID: 31753 RVA: 0x001C0FE7 File Offset: 0x001BF1E7
		ImmutableArray<Location> ISymbol.Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x06007C0A RID: 31754 RVA: 0x001C0FEE File Offset: 0x001BF1EE
		ImmutableArray<SyntaxReference> ISymbol.DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x001C0FF5 File Offset: 0x001BF1F5
		ImmutableArray<AttributeData> ISymbol.GetAttributes()
		{
			return ImmutableArray<AttributeData>.Empty;
		}

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x06007C0C RID: 31756 RVA: 0x001C0FFC File Offset: 0x001BF1FC
		Accessibility ISymbol.DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x001C0FFF File Offset: 0x001BF1FF
		void ISymbol.Accept(SymbolVisitor visitor)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x001C1008 File Offset: 0x001BF208
		TResult ISymbol.Accept<[global::System.Runtime.CompilerServices.Nullable(2)] TResult>(SymbolVisitor<TResult> visitor)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x001C101A File Offset: 0x001BF21A
		string ISymbol.GetDocumentationCommentId()
		{
			return null;
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x001C101D File Offset: 0x001BF21D
		string ISymbol.GetDocumentationCommentXml(CultureInfo preferredCulture, bool expandIncludes, CancellationToken cancellationToken)
		{
			return null;
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x001C1020 File Offset: 0x001BF220
		string ISymbol.ToDisplayString(SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToDisplayString(this, format);
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x001C1029 File Offset: 0x001BF229
		ImmutableArray<SymbolDisplayPart> ISymbol.ToDisplayParts(SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToDisplayParts(this, format);
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x001C1032 File Offset: 0x001BF232
		string ISymbol.ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToMinimalDisplayString(this, Symbol.GetCSharpSemanticModel(semanticModel), position, format);
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x001C1042 File Offset: 0x001BF242
		ImmutableArray<SymbolDisplayPart> ISymbol.ToMinimalDisplayParts(SemanticModel semanticModel, int position, SymbolDisplayFormat format)
		{
			return SymbolDisplay.ToMinimalDisplayParts(this, Symbol.GetCSharpSemanticModel(semanticModel), position, format);
		}

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x06007C15 RID: 31765 RVA: 0x001C1052 File Offset: 0x001BF252
		SymbolKind ISymbol.Kind
		{
			get
			{
				return SymbolKind.Preprocessing;
			}
		}

		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x06007C16 RID: 31766 RVA: 0x001C1056 File Offset: 0x001BF256
		string ISymbol.Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x06007C17 RID: 31767 RVA: 0x001C105D File Offset: 0x001BF25D
		string ISymbol.Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x06007C18 RID: 31768 RVA: 0x001C1065 File Offset: 0x001BF265
		string ISymbol.MetadataName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x06007C19 RID: 31769 RVA: 0x001C106D File Offset: 0x001BF26D
		IAssemblySymbol ISymbol.ContainingAssembly
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x06007C1A RID: 31770 RVA: 0x001C1070 File Offset: 0x001BF270
		IModuleSymbol ISymbol.ContainingModule
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x06007C1B RID: 31771 RVA: 0x001C1073 File Offset: 0x001BF273
		INamespaceSymbol ISymbol.ContainingNamespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x06007C1C RID: 31772 RVA: 0x001C1076 File Offset: 0x001BF276
		bool ISymbol.IsDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x06007C1D RID: 31773 RVA: 0x001C1079 File Offset: 0x001BF279
		bool ISymbol.IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x06007C1E RID: 31774 RVA: 0x001C107C File Offset: 0x001BF27C
		bool ISymbol.IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x06007C1F RID: 31775 RVA: 0x001C107F File Offset: 0x001BF27F
		bool ISymbol.IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x06007C20 RID: 31776 RVA: 0x001C1082 File Offset: 0x001BF282
		bool ISymbol.IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x06007C21 RID: 31777 RVA: 0x001C1085 File Offset: 0x001BF285
		bool ISymbol.IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x06007C22 RID: 31778 RVA: 0x001C1088 File Offset: 0x001BF288
		bool ISymbol.IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x06007C23 RID: 31779 RVA: 0x001C108B File Offset: 0x001BF28B
		bool ISymbol.IsImplicitlyDeclared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x06007C24 RID: 31780 RVA: 0x001C108E File Offset: 0x001BF28E
		bool ISymbol.CanBeReferencedByName
		{
			get
			{
				return SyntaxFacts.IsValidIdentifier(this._name) && !SyntaxFacts.ContainsDroppedIdentifierCharacters(this._name);
			}
		}

		// Token: 0x1700243F RID: 9279
		// (get) Token: 0x06007C25 RID: 31781 RVA: 0x001C10AD File Offset: 0x001BF2AD
		bool ISymbol.HasUnsupportedMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x001C10B0 File Offset: 0x001BF2B0
		public sealed override string ToString()
		{
			return SymbolDisplay.ToDisplayString(this, null);
		}

		// Token: 0x04001D4F RID: 7503
		private readonly string _name;
	}
}
