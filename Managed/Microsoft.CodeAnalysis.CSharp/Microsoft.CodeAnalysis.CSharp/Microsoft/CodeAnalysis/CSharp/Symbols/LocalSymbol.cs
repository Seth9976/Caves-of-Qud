using System;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004ED RID: 1261
	internal abstract class LocalSymbol : Symbol, ILocalSymbolInternal, ISymbolInternal
	{
		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06006A8A RID: 27274
		internal abstract LocalDeclarationKind DeclarationKind { get; }

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x06006A8B RID: 27275
		internal abstract SynthesizedLocalKind SynthesizedKind { get; }

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06006A8C RID: 27276
		internal abstract SyntaxNode ScopeDesignatorOpt { get; }

		// Token: 0x06006A8D RID: 27277
		internal abstract LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax);

		// Token: 0x17001B6D RID: 7021
		// (get) Token: 0x06006A8E RID: 27278
		internal abstract bool IsImportedFromMetadata { get; }

		// Token: 0x17001B6E RID: 7022
		// (get) Token: 0x06006A8F RID: 27279 RVA: 0x0018C765 File Offset: 0x0018A965
		internal virtual bool CanScheduleToStack
		{
			get
			{
				return !this.IsConst && !this.IsPinned;
			}
		}

		// Token: 0x17001B6F RID: 7023
		// (get) Token: 0x06006A90 RID: 27280
		internal abstract SyntaxToken IdentifierToken { get; }

		// Token: 0x17001B70 RID: 7024
		// (get) Token: 0x06006A91 RID: 27281
		public abstract TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x17001B71 RID: 7025
		// (get) Token: 0x06006A92 RID: 27282 RVA: 0x0018C77C File Offset: 0x0018A97C
		public TypeSymbol Type
		{
			get
			{
				return this.TypeWithAnnotations.Type;
			}
		}

		// Token: 0x17001B72 RID: 7026
		// (get) Token: 0x06006A93 RID: 27283
		internal abstract bool IsPinned { get; }

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x0018C797 File Offset: 0x0018A997
		public sealed override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x06006A95 RID: 27285 RVA: 0x0018C79A File Offset: 0x0018A99A
		public sealed override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x06006A96 RID: 27286 RVA: 0x0018C79D File Offset: 0x0018A99D
		public sealed override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x06006A97 RID: 27287 RVA: 0x0018C7A0 File Offset: 0x0018A9A0
		public sealed override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x06006A98 RID: 27288 RVA: 0x0018C7A3 File Offset: 0x0018A9A3
		public sealed override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06006A99 RID: 27289 RVA: 0x0018C7A6 File Offset: 0x0018A9A6
		public sealed override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06006A9A RID: 27290 RVA: 0x0018C7A9 File Offset: 0x0018A9A9
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x06006A9B RID: 27291 RVA: 0x0018C7AC File Offset: 0x0018A9AC
		public sealed override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001B7B RID: 7035
		// (get) Token: 0x06006A9C RID: 27292 RVA: 0x0018C7AF File Offset: 0x0018A9AF
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Local;
			}
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x0018C7B2 File Offset: 0x0018A9B2
		internal sealed override TResult Accept<TArgument, TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitLocal(this, argument);
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x0018C7BC File Offset: 0x0018A9BC
		public sealed override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitLocal(this);
		}

		// Token: 0x06006A9F RID: 27295 RVA: 0x0018C7C5 File Offset: 0x0018A9C5
		public sealed override TResult Accept<TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitLocal(this);
		}

		// Token: 0x17001B7C RID: 7036
		// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x0018C7CE File Offset: 0x0018A9CE
		public bool IsCatch
		{
			get
			{
				return this.DeclarationKind == LocalDeclarationKind.CatchVariable;
			}
		}

		// Token: 0x17001B7D RID: 7037
		// (get) Token: 0x06006AA1 RID: 27297 RVA: 0x0018C7D9 File Offset: 0x0018A9D9
		public bool IsConst
		{
			get
			{
				return this.DeclarationKind == LocalDeclarationKind.Constant;
			}
		}

		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x06006AA2 RID: 27298 RVA: 0x0018C7E4 File Offset: 0x0018A9E4
		public bool IsUsing
		{
			get
			{
				return this.DeclarationKind == LocalDeclarationKind.UsingVariable;
			}
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06006AA3 RID: 27299 RVA: 0x0018C7EF File Offset: 0x0018A9EF
		public bool IsFixed
		{
			get
			{
				return this.DeclarationKind == LocalDeclarationKind.FixedVariable;
			}
		}

		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06006AA4 RID: 27300 RVA: 0x0018C7FA File Offset: 0x0018A9FA
		public bool IsForEach
		{
			get
			{
				return this.DeclarationKind == LocalDeclarationKind.ForEachIterationVariable;
			}
		}

		// Token: 0x06006AA5 RID: 27301
		internal abstract SyntaxNode GetDeclaratorSyntax();

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x0018C808 File Offset: 0x0018AA08
		internal virtual bool IsWritableVariable
		{
			get
			{
				LocalDeclarationKind declarationKind = this.DeclarationKind;
				return declarationKind - LocalDeclarationKind.Constant > 2 && declarationKind != LocalDeclarationKind.ForEachIterationVariable;
			}
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x06006AA7 RID: 27303 RVA: 0x0018C82C File Offset: 0x0018AA2C
		public bool HasConstantValue
		{
			get
			{
				if (!this.IsConst)
				{
					return false;
				}
				ConstantValue constantValue = this.GetConstantValue(null, null, null);
				return constantValue != null && !constantValue.IsBad;
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06006AA8 RID: 27304 RVA: 0x0018C861 File Offset: 0x0018AA61
		public object ConstantValue
		{
			get
			{
				if (!this.IsConst)
				{
					return null;
				}
				ConstantValue constantValue = this.GetConstantValue(null, null, null);
				if (constantValue == null)
				{
					return null;
				}
				return constantValue.Value;
			}
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06006AA9 RID: 27305
		internal abstract bool IsCompilerGenerated { get; }

		// Token: 0x06006AAA RID: 27306
		internal abstract ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics = null);

		// Token: 0x06006AAB RID: 27307
		internal abstract ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue);

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06006AAC RID: 27308 RVA: 0x0018C881 File Offset: 0x0018AA81
		public bool IsRef
		{
			get
			{
				return this.RefKind > RefKind.None;
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x06006AAD RID: 27309
		public abstract RefKind RefKind { get; }

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x06006AAE RID: 27310
		internal abstract uint RefEscapeScope { get; }

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x06006AAF RID: 27311
		internal abstract uint ValEscapeScope { get; }

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06006AB0 RID: 27312 RVA: 0x0018C88C File Offset: 0x0018AA8C
		internal virtual SyntaxNode ForbiddenZone
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06006AB1 RID: 27313 RVA: 0x0018C88F File Offset: 0x0018AA8F
		internal virtual ErrorCode ForbiddenDiagnostic
		{
			get
			{
				return ErrorCode.ERR_VariableUsedBeforeDeclaration;
			}
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x0018C896 File Offset: 0x0018AA96
		protected sealed override ISymbol CreateISymbol()
		{
			return new LocalSymbol(this);
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x06006AB3 RID: 27315 RVA: 0x0018C89E File Offset: 0x0018AA9E
		SynthesizedLocalKind ILocalSymbolInternal.SynthesizedKind
		{
			get
			{
				return this.SynthesizedKind;
			}
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x0018C8A6 File Offset: 0x0018AAA6
		bool ILocalSymbolInternal.IsImportedFromMetadata
		{
			get
			{
				return this.IsImportedFromMetadata;
			}
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x0018C8AE File Offset: 0x0018AAAE
		SyntaxNode ILocalSymbolInternal.GetDeclaratorSyntax()
		{
			return this.GetDeclaratorSyntax();
		}
	}
}
