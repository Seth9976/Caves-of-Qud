using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005AE RID: 1454
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class UpdatedContainingSymbolAndNullableAnnotationLocal : LocalSymbol
	{
		// Token: 0x06007A0D RID: 31245 RVA: 0x001BEAAC File Offset: 0x001BCCAC
		internal static UpdatedContainingSymbolAndNullableAnnotationLocal CreateForTest(SourceLocalSymbol underlyingLocal, Symbol updatedContainingSymbol, TypeWithAnnotations updatedType)
		{
			return new UpdatedContainingSymbolAndNullableAnnotationLocal(underlyingLocal, updatedContainingSymbol, updatedType, false);
		}

		// Token: 0x06007A0E RID: 31246 RVA: 0x001BEAB7 File Offset: 0x001BCCB7
		private UpdatedContainingSymbolAndNullableAnnotationLocal(SourceLocalSymbol underlyingLocal, Symbol updatedContainingSymbol, TypeWithAnnotations updatedType, bool assertContaining)
		{
			this.ContainingSymbol = updatedContainingSymbol;
			this.TypeWithAnnotations = updatedType;
			this._underlyingLocal = underlyingLocal;
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x001BEAD4 File Offset: 0x001BCCD4
		internal UpdatedContainingSymbolAndNullableAnnotationLocal(SourceLocalSymbol underlyingLocal, Symbol updatedContainingSymbol, TypeWithAnnotations updatedType)
			: this(underlyingLocal, updatedContainingSymbol, updatedType, true)
		{
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x001BEAE0 File Offset: 0x001BCCE0
		public override Symbol ContainingSymbol { get; }

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x06007A11 RID: 31249 RVA: 0x001BEAE8 File Offset: 0x001BCCE8
		public override TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x06007A12 RID: 31250 RVA: 0x001BEAF0 File Offset: 0x001BCCF0
		public override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			if (other == this)
			{
				return true;
			}
			LocalSymbol localSymbol = other as LocalSymbol;
			if (localSymbol == null)
			{
				return false;
			}
			UpdatedContainingSymbolAndNullableAnnotationLocal updatedContainingSymbolAndNullableAnnotationLocal = localSymbol as UpdatedContainingSymbolAndNullableAnnotationLocal;
			SourceLocalSymbol sourceLocalSymbol2;
			if (updatedContainingSymbolAndNullableAnnotationLocal == null)
			{
				SourceLocalSymbol sourceLocalSymbol = localSymbol as SourceLocalSymbol;
				if (sourceLocalSymbol == null)
				{
					sourceLocalSymbol2 = null;
				}
				else
				{
					sourceLocalSymbol2 = sourceLocalSymbol;
				}
			}
			else
			{
				sourceLocalSymbol2 = updatedContainingSymbolAndNullableAnnotationLocal._underlyingLocal;
			}
			SourceLocalSymbol sourceLocalSymbol3 = sourceLocalSymbol2;
			return sourceLocalSymbol3 != null && this._underlyingLocal.Equals(sourceLocalSymbol3, compareKind) && ((compareKind & TypeCompareKind.AllNullableIgnoreOptions) > TypeCompareKind.ConsiderEverything || (this.TypeWithAnnotations.Equals(localSymbol.TypeWithAnnotations, compareKind) && this.ContainingSymbol.Equals(localSymbol.ContainingSymbol, compareKind)));
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x001BEB85 File Offset: 0x001BCD85
		public override int GetHashCode()
		{
			return this._underlyingLocal.GetHashCode();
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x001BEB92 File Offset: 0x001BCD92
		public override RefKind RefKind
		{
			get
			{
				return this._underlyingLocal.RefKind;
			}
		}

		// Token: 0x170022C0 RID: 8896
		// (get) Token: 0x06007A15 RID: 31253 RVA: 0x001BEB9F File Offset: 0x001BCD9F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlyingLocal.Locations;
			}
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x06007A16 RID: 31254 RVA: 0x001BEBAC File Offset: 0x001BCDAC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._underlyingLocal.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x06007A17 RID: 31255 RVA: 0x001BEBB9 File Offset: 0x001BCDB9
		public override string Name
		{
			get
			{
				return this._underlyingLocal.Name;
			}
		}

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x06007A18 RID: 31256 RVA: 0x001BEBC6 File Offset: 0x001BCDC6
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this._underlyingLocal.IsImplicitlyDeclared;
			}
		}

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x06007A19 RID: 31257 RVA: 0x001BEBD3 File Offset: 0x001BCDD3
		internal override LocalDeclarationKind DeclarationKind
		{
			get
			{
				return this._underlyingLocal.DeclarationKind;
			}
		}

		// Token: 0x170022C5 RID: 8901
		// (get) Token: 0x06007A1A RID: 31258 RVA: 0x001BEBE0 File Offset: 0x001BCDE0
		internal override SynthesizedLocalKind SynthesizedKind
		{
			get
			{
				return this._underlyingLocal.SynthesizedKind;
			}
		}

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06007A1B RID: 31259 RVA: 0x001BEBED File Offset: 0x001BCDED
		internal override SyntaxNode ScopeDesignatorOpt
		{
			get
			{
				return this._underlyingLocal.ScopeDesignatorOpt;
			}
		}

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x06007A1C RID: 31260 RVA: 0x001BEBFA File Offset: 0x001BCDFA
		internal override bool IsImportedFromMetadata
		{
			get
			{
				return this._underlyingLocal.IsImportedFromMetadata;
			}
		}

		// Token: 0x170022C8 RID: 8904
		// (get) Token: 0x06007A1D RID: 31261 RVA: 0x001BEC07 File Offset: 0x001BCE07
		internal override SyntaxToken IdentifierToken
		{
			get
			{
				return this._underlyingLocal.IdentifierToken;
			}
		}

		// Token: 0x170022C9 RID: 8905
		// (get) Token: 0x06007A1E RID: 31262 RVA: 0x001BEC14 File Offset: 0x001BCE14
		internal override bool IsPinned
		{
			get
			{
				return this._underlyingLocal.IsPinned;
			}
		}

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x06007A1F RID: 31263 RVA: 0x001BEC21 File Offset: 0x001BCE21
		internal override bool IsCompilerGenerated
		{
			get
			{
				return this._underlyingLocal.IsCompilerGenerated;
			}
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x06007A20 RID: 31264 RVA: 0x001BEC2E File Offset: 0x001BCE2E
		internal override uint RefEscapeScope
		{
			get
			{
				return this._underlyingLocal.RefEscapeScope;
			}
		}

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x06007A21 RID: 31265 RVA: 0x001BEC3B File Offset: 0x001BCE3B
		internal override uint ValEscapeScope
		{
			get
			{
				return this._underlyingLocal.ValEscapeScope;
			}
		}

		// Token: 0x06007A22 RID: 31266 RVA: 0x001BEC48 File Offset: 0x001BCE48
		internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics = null)
		{
			return this._underlyingLocal.GetConstantValue(node, inProgress, diagnostics);
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x001BEC58 File Offset: 0x001BCE58
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
		{
			return this._underlyingLocal.GetConstantValueDiagnostics(boundInitValue);
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x001BEC66 File Offset: 0x001BCE66
		internal override SyntaxNode GetDeclaratorSyntax()
		{
			return this._underlyingLocal.GetDeclaratorSyntax();
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x001BEC73 File Offset: 0x001BCE73
		internal override LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001D2A RID: 7466
		private readonly SourceLocalSymbol _underlyingLocal;
	}
}
