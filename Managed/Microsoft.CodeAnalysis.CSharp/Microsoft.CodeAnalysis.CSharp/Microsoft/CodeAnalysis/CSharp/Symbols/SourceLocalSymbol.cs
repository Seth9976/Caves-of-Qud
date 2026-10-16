using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200053C RID: 1340
	internal class SourceLocalSymbol : LocalSymbol
	{
		// Token: 0x06007043 RID: 28739 RVA: 0x0019C280 File Offset: 0x0019A480
		private SourceLocalSymbol(Symbol containingSymbol, Binder scopeBinder, bool allowRefKind, TypeSyntax typeSyntax, SyntaxToken identifierToken, LocalDeclarationKind declarationKind)
		{
			this._scopeBinder = scopeBinder;
			this._containingSymbol = containingSymbol;
			this._identifierToken = identifierToken;
			this._typeSyntax = (allowRefKind ? ((typeSyntax != null) ? typeSyntax.SkipRef(out this._refKind) : null) : typeSyntax);
			this._declarationKind = declarationKind;
			this._locations = ImmutableArray.Create<Location>(identifierToken.GetLocation());
			this._refEscapeScope = ((this._refKind == RefKind.None) ? scopeBinder.LocalScopeDepth : 0U);
			this._valEscapeScope = 0U;
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06007044 RID: 28740 RVA: 0x0019C302 File Offset: 0x0019A502
		internal Binder ScopeBinder
		{
			get
			{
				return this._scopeBinder;
			}
		}

		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06007045 RID: 28741 RVA: 0x0019C30A File Offset: 0x0019A50A
		internal override SyntaxNode ScopeDesignatorOpt
		{
			get
			{
				return this._scopeBinder.ScopeDesignator;
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06007046 RID: 28742 RVA: 0x0019C317 File Offset: 0x0019A517
		internal override uint RefEscapeScope
		{
			get
			{
				return this._refEscapeScope;
			}
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06007047 RID: 28743 RVA: 0x0019C31F File Offset: 0x0019A51F
		internal override uint ValEscapeScope
		{
			get
			{
				return this._valEscapeScope;
			}
		}

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x06007048 RID: 28744 RVA: 0x0019C327 File Offset: 0x0019A527
		internal Binder TypeSyntaxBinder
		{
			get
			{
				return this._scopeBinder;
			}
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x0019C32F File Offset: 0x0019A52F
		internal override string GetDebuggerDisplay()
		{
			if (this._type == null)
			{
				return string.Format("{0} <var> ${1}", this.Kind, this.Name);
			}
			return base.GetDebuggerDisplay();
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x0019C35B File Offset: 0x0019A55B
		public static SourceLocalSymbol MakeForeachLocal(MethodSymbol containingMethod, ForEachLoopBinder binder, TypeSyntax typeSyntax, SyntaxToken identifierToken, ExpressionSyntax collection)
		{
			return new SourceLocalSymbol.ForEachLocalSymbol(containingMethod, binder, typeSyntax, identifierToken, collection, LocalDeclarationKind.ForEachIterationVariable);
		}

		// Token: 0x0600704B RID: 28747 RVA: 0x0019C369 File Offset: 0x0019A569
		public static SourceLocalSymbol MakeDeconstructionLocal(Symbol containingSymbol, Binder scopeBinder, Binder nodeBinder, TypeSyntax closestTypeSyntax, SyntaxToken identifierToken, LocalDeclarationKind kind, SyntaxNode deconstruction)
		{
			if (!closestTypeSyntax.IsVar)
			{
				return new SourceLocalSymbol(containingSymbol, scopeBinder, false, closestTypeSyntax, identifierToken, kind);
			}
			return new SourceLocalSymbol.DeconstructionLocalSymbol(containingSymbol, scopeBinder, nodeBinder, closestTypeSyntax, identifierToken, kind, deconstruction);
		}

		// Token: 0x0600704C RID: 28748 RVA: 0x0019C390 File Offset: 0x0019A590
		internal static LocalSymbol MakeLocalSymbolWithEnclosingContext(Symbol containingSymbol, Binder scopeBinder, Binder nodeBinder, TypeSyntax typeSyntax, SyntaxToken identifierToken, LocalDeclarationKind kind, SyntaxNode nodeToBind, SyntaxNode forbiddenZone)
		{
			if ((typeSyntax != null && !typeSyntax.IsVar) || kind == LocalDeclarationKind.DeclarationExpressionVariable)
			{
				return new SourceLocalSymbol(containingSymbol, scopeBinder, false, typeSyntax, identifierToken, kind);
			}
			return new SourceLocalSymbol.LocalSymbolWithEnclosingContext(containingSymbol, scopeBinder, nodeBinder, typeSyntax, identifierToken, kind, nodeToBind, forbiddenZone);
		}

		// Token: 0x0600704D RID: 28749 RVA: 0x0019C3C5 File Offset: 0x0019A5C5
		public static SourceLocalSymbol MakeLocal(Symbol containingSymbol, Binder scopeBinder, bool allowRefKind, TypeSyntax typeSyntax, SyntaxToken identifierToken, LocalDeclarationKind declarationKind, EqualsValueClauseSyntax initializer = null, Binder initializerBinderOpt = null)
		{
			if (initializer == null)
			{
				return new SourceLocalSymbol(containingSymbol, scopeBinder, allowRefKind, typeSyntax, identifierToken, declarationKind);
			}
			return new SourceLocalSymbol.LocalWithInitializer(containingSymbol, scopeBinder, typeSyntax, identifierToken, initializer, initializerBinderOpt ?? scopeBinder, declarationKind);
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x0600704E RID: 28750 RVA: 0x0019C3EE File Offset: 0x0019A5EE
		internal override bool IsImportedFromMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x0600704F RID: 28751 RVA: 0x0019C3F1 File Offset: 0x0019A5F1
		internal override LocalDeclarationKind DeclarationKind
		{
			get
			{
				return this._declarationKind;
			}
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x06007050 RID: 28752 RVA: 0x0019C3F9 File Offset: 0x0019A5F9
		internal override SynthesizedLocalKind SynthesizedKind
		{
			get
			{
				return SynthesizedLocalKind.UserDefined;
			}
		}

		// Token: 0x06007051 RID: 28753 RVA: 0x0019C3FC File Offset: 0x0019A5FC
		internal override LocalSymbol WithSynthesizedLocalKindAndSyntax(SynthesizedLocalKind kind, SyntaxNode syntax)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x06007052 RID: 28754 RVA: 0x0019C403 File Offset: 0x0019A603
		internal override bool IsPinned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x0019C406 File Offset: 0x0019A606
		internal virtual void SetRefEscape(uint value)
		{
			this._refEscapeScope = value;
		}

		// Token: 0x06007054 RID: 28756 RVA: 0x0019C40F File Offset: 0x0019A60F
		internal virtual void SetValEscape(uint value)
		{
			this._valEscapeScope = value;
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x06007055 RID: 28757 RVA: 0x0019C418 File Offset: 0x0019A618
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x06007056 RID: 28758 RVA: 0x0019C420 File Offset: 0x0019A620
		public override string Name
		{
			get
			{
				return this._identifierToken.ValueText;
			}
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x06007057 RID: 28759 RVA: 0x0019C42D File Offset: 0x0019A62D
		internal override SyntaxToken IdentifierToken
		{
			get
			{
				return this._identifierToken;
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x06007058 RID: 28760 RVA: 0x0019C438 File Offset: 0x0019A638
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				if (this._type == null)
				{
					TypeWithAnnotations typeSymbol = this.GetTypeSymbol();
					this.SetTypeWithAnnotations(typeSymbol);
				}
				return this._type.Value;
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x06007059 RID: 28761 RVA: 0x0019C468 File Offset: 0x0019A668
		public bool IsVar
		{
			get
			{
				if (this._typeSyntax == null)
				{
					return true;
				}
				if (this._typeSyntax.IsVar)
				{
					bool flag;
					this.TypeSyntaxBinder.BindTypeOrVarKeyword(this._typeSyntax, BindingDiagnosticBag.Discarded, out flag);
					return flag;
				}
				return false;
			}
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x0019C4A8 File Offset: 0x0019A6A8
		private TypeWithAnnotations GetTypeSymbol()
		{
			BindingDiagnosticBag discarded = BindingDiagnosticBag.Discarded;
			Binder typeSyntaxBinder = this.TypeSyntaxBinder;
			bool flag;
			TypeWithAnnotations typeWithAnnotations;
			if (this._typeSyntax == null)
			{
				flag = true;
				typeWithAnnotations = default(TypeWithAnnotations);
			}
			else
			{
				RefKind refKind;
				typeWithAnnotations = typeSyntaxBinder.BindTypeOrVarKeyword(this._typeSyntax.SkipRef(out refKind), discarded, out flag);
			}
			if (flag)
			{
				TypeWithAnnotations typeWithAnnotations2 = this.InferTypeOfVarVariable(discarded);
				if (typeWithAnnotations2.HasType && !typeWithAnnotations2.IsVoidType())
				{
					typeWithAnnotations = typeWithAnnotations2;
				}
				else
				{
					typeWithAnnotations = TypeWithAnnotations.Create(typeSyntaxBinder.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}
			return typeWithAnnotations;
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x0019C530 File Offset: 0x0019A730
		protected virtual TypeWithAnnotations InferTypeOfVarVariable(BindingDiagnosticBag diagnostics)
		{
			TypeWithAnnotations.Boxed type = this._type;
			if (type == null)
			{
				return default(TypeWithAnnotations);
			}
			return type.Value;
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x0019C556 File Offset: 0x0019A756
		internal void SetTypeWithAnnotations(TypeWithAnnotations newType)
		{
			TypeWithAnnotations.Boxed type = this._type;
			if (type != null)
			{
				TypeWithAnnotations value = type.Value;
			}
			if (this._type == null)
			{
				Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._type, new TypeWithAnnotations.Boxed(newType), null);
			}
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x0600705D RID: 28765 RVA: 0x0019C585 File Offset: 0x0019A785
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._locations;
			}
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x0019C58D File Offset: 0x0019A78D
		internal sealed override SyntaxNode GetDeclaratorSyntax()
		{
			return this._identifierToken.Parent;
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x0600705F RID: 28767 RVA: 0x0019C59A File Offset: 0x0019A79A
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray.Create<SyntaxReference>(this._identifierToken.Parent.GetReference());
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x06007060 RID: 28768 RVA: 0x0019C5B1 File Offset: 0x0019A7B1
		internal override bool IsCompilerGenerated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007061 RID: 28769 RVA: 0x0019C5B4 File Offset: 0x0019A7B4
		internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics)
		{
			return null;
		}

		// Token: 0x06007062 RID: 28770 RVA: 0x0019C5B7 File Offset: 0x0019A7B7
		internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
		{
			return ImmutableBindingDiagnostic<AssemblySymbol>.Empty;
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x06007063 RID: 28771 RVA: 0x0019C5BE File Offset: 0x0019A7BE
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x06007064 RID: 28772 RVA: 0x0019C5C8 File Offset: 0x0019A7C8
		public sealed override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (obj == this)
			{
				return true;
			}
			UpdatedContainingSymbolAndNullableAnnotationLocal updatedContainingSymbolAndNullableAnnotationLocal = obj as UpdatedContainingSymbolAndNullableAnnotationLocal;
			if (updatedContainingSymbolAndNullableAnnotationLocal != null)
			{
				return updatedContainingSymbolAndNullableAnnotationLocal.Equals(this, compareKind);
			}
			SourceLocalSymbol sourceLocalSymbol = obj as SourceLocalSymbol;
			return sourceLocalSymbol != null && sourceLocalSymbol._identifierToken.Equals(this._identifierToken) && sourceLocalSymbol._containingSymbol.Equals(this._containingSymbol, compareKind);
		}

		// Token: 0x06007065 RID: 28773 RVA: 0x0019C61F File Offset: 0x0019A81F
		public sealed override int GetHashCode()
		{
			return Hash.Combine(this._identifierToken.GetHashCode(), this._containingSymbol.GetHashCode());
		}

		// Token: 0x04001B83 RID: 7043
		private readonly Binder _scopeBinder;

		// Token: 0x04001B84 RID: 7044
		private readonly Symbol _containingSymbol;

		// Token: 0x04001B85 RID: 7045
		private readonly SyntaxToken _identifierToken;

		// Token: 0x04001B86 RID: 7046
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x04001B87 RID: 7047
		private readonly RefKind _refKind;

		// Token: 0x04001B88 RID: 7048
		private readonly TypeSyntax _typeSyntax;

		// Token: 0x04001B89 RID: 7049
		private readonly LocalDeclarationKind _declarationKind;

		// Token: 0x04001B8A RID: 7050
		private TypeWithAnnotations.Boxed _type;

		// Token: 0x04001B8B RID: 7051
		protected uint _refEscapeScope;

		// Token: 0x04001B8C RID: 7052
		protected uint _valEscapeScope;

		// Token: 0x0200091C RID: 2332
		private sealed class LocalWithInitializer : SourceLocalSymbol
		{
			// Token: 0x06008FCB RID: 36811 RVA: 0x001F9B34 File Offset: 0x001F7D34
			public LocalWithInitializer(Symbol containingSymbol, Binder scopeBinder, TypeSyntax typeSyntax, SyntaxToken identifierToken, EqualsValueClauseSyntax initializer, Binder initializerBinder, LocalDeclarationKind declarationKind)
				: base(containingSymbol, scopeBinder, true, typeSyntax, identifierToken, declarationKind)
			{
				this._initializer = initializer;
				this._initializerBinder = initializerBinder;
				this._refEscapeScope = this._scopeBinder.LocalScopeDepth;
				this._valEscapeScope = this._scopeBinder.LocalScopeDepth;
			}

			// Token: 0x06008FCC RID: 36812 RVA: 0x001F9B84 File Offset: 0x001F7D84
			protected override TypeWithAnnotations InferTypeOfVarVariable(BindingDiagnosticBag diagnostics)
			{
				BoundExpression boundExpression = this._initializerBinder.BindInferredVariableInitializer(diagnostics, this.RefKind, this._initializer, this._initializer);
				return TypeWithAnnotations.Create((boundExpression != null) ? boundExpression.Type : null, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
			}

			// Token: 0x17002899 RID: 10393
			// (get) Token: 0x06008FCD RID: 36813 RVA: 0x001F9BCA File Offset: 0x001F7DCA
			internal override SyntaxNode ForbiddenZone
			{
				get
				{
					return this._initializer;
				}
			}

			// Token: 0x06008FCE RID: 36814 RVA: 0x001F9BD4 File Offset: 0x001F7DD4
			private void MakeConstantTuple(LocalSymbol inProgress, BoundExpression boundInitValue)
			{
				if (base.IsConst && this._constantTuple == null)
				{
					ConstantValue constantValue = Microsoft.CodeAnalysis.ConstantValue.Bad;
					Location location = this._initializer.Value.Location;
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					TypeSymbol type = base.Type;
					if (boundInitValue == null)
					{
						boundInitValue = new LocalInProgressBinder(this, this._initializerBinder).BindVariableOrAutoPropInitializerValue(this._initializer, this.RefKind, type, instance);
					}
					constantValue = ConstantValueUtils.GetAndValidateConstantValue(boundInitValue, this, type, location, instance);
					Interlocked.CompareExchange<EvaluatedConstant>(ref this._constantTuple, new EvaluatedConstant(constantValue, instance.ToReadOnlyAndFree()), null);
				}
			}

			// Token: 0x06008FCF RID: 36815 RVA: 0x001F9C60 File Offset: 0x001F7E60
			internal override ConstantValue GetConstantValue(SyntaxNode node, LocalSymbol inProgress, BindingDiagnosticBag diagnostics = null)
			{
				if (base.IsConst && inProgress == this)
				{
					if (diagnostics != null)
					{
						diagnostics.Add(ErrorCode.ERR_CircConstValue, node.GetLocation(), new object[] { this });
					}
					return Microsoft.CodeAnalysis.ConstantValue.Bad;
				}
				this.MakeConstantTuple(inProgress, null);
				if (this._constantTuple != null)
				{
					return this._constantTuple.Value;
				}
				return null;
			}

			// Token: 0x06008FD0 RID: 36816 RVA: 0x001F9CBD File Offset: 0x001F7EBD
			internal override ImmutableBindingDiagnostic<AssemblySymbol> GetConstantValueDiagnostics(BoundExpression boundInitValue)
			{
				this.MakeConstantTuple(null, boundInitValue);
				if (this._constantTuple != null)
				{
					return this._constantTuple.Diagnostics;
				}
				return ImmutableBindingDiagnostic<AssemblySymbol>.Empty;
			}

			// Token: 0x06008FD1 RID: 36817 RVA: 0x001F9CE0 File Offset: 0x001F7EE0
			internal override void SetRefEscape(uint value)
			{
				this._refEscapeScope = value;
			}

			// Token: 0x06008FD2 RID: 36818 RVA: 0x001F9CE9 File Offset: 0x001F7EE9
			internal override void SetValEscape(uint value)
			{
				this._valEscapeScope = value;
			}

			// Token: 0x04002771 RID: 10097
			private readonly EqualsValueClauseSyntax _initializer;

			// Token: 0x04002772 RID: 10098
			private readonly Binder _initializerBinder;

			// Token: 0x04002773 RID: 10099
			private EvaluatedConstant _constantTuple;
		}

		// Token: 0x0200091D RID: 2333
		private sealed class ForEachLocalSymbol : SourceLocalSymbol
		{
			// Token: 0x06008FD3 RID: 36819 RVA: 0x001F9CF2 File Offset: 0x001F7EF2
			public ForEachLocalSymbol(Symbol containingSymbol, ForEachLoopBinder scopeBinder, TypeSyntax typeSyntax, SyntaxToken identifierToken, ExpressionSyntax collection, LocalDeclarationKind declarationKind)
				: base(containingSymbol, scopeBinder, true, typeSyntax, identifierToken, declarationKind)
			{
				this._collection = collection;
			}

			// Token: 0x1700289A RID: 10394
			// (get) Token: 0x06008FD4 RID: 36820 RVA: 0x001F9D0A File Offset: 0x001F7F0A
			private ForEachLoopBinder ForEachLoopBinder
			{
				get
				{
					return (ForEachLoopBinder)base.ScopeBinder;
				}
			}

			// Token: 0x06008FD5 RID: 36821 RVA: 0x001F9D17 File Offset: 0x001F7F17
			protected override TypeWithAnnotations InferTypeOfVarVariable(BindingDiagnosticBag diagnostics)
			{
				return this.ForEachLoopBinder.InferCollectionElementType(diagnostics, this._collection);
			}

			// Token: 0x1700289B RID: 10395
			// (get) Token: 0x06008FD6 RID: 36822 RVA: 0x001F9D2B File Offset: 0x001F7F2B
			internal override SyntaxNode ForbiddenZone
			{
				get
				{
					return null;
				}
			}

			// Token: 0x04002774 RID: 10100
			private readonly ExpressionSyntax _collection;
		}

		// Token: 0x0200091E RID: 2334
		private class DeconstructionLocalSymbol : SourceLocalSymbol
		{
			// Token: 0x06008FD7 RID: 36823 RVA: 0x001F9D2E File Offset: 0x001F7F2E
			public DeconstructionLocalSymbol(Symbol containingSymbol, Binder scopeBinder, Binder nodeBinder, TypeSyntax typeSyntax, SyntaxToken identifierToken, LocalDeclarationKind declarationKind, SyntaxNode deconstruction)
				: base(containingSymbol, scopeBinder, false, typeSyntax, identifierToken, declarationKind)
			{
				this._deconstruction = deconstruction;
				this._nodeBinder = nodeBinder;
			}

			// Token: 0x06008FD8 RID: 36824 RVA: 0x001F9D50 File Offset: 0x001F7F50
			protected override TypeWithAnnotations InferTypeOfVarVariable(BindingDiagnosticBag diagnostics)
			{
				SyntaxKind syntaxKind = this._deconstruction.Kind();
				if (syntaxKind != SyntaxKind.SimpleAssignmentExpression)
				{
					if (syntaxKind != SyntaxKind.ForEachVariableStatement)
					{
						return TypeWithAnnotations.Create(this._nodeBinder.CreateErrorType(""), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					}
					this._nodeBinder.BindForEachDeconstruction(diagnostics, this._nodeBinder);
				}
				else
				{
					AssignmentExpressionSyntax assignmentExpressionSyntax = (AssignmentExpressionSyntax)this._deconstruction;
					DeclarationExpressionSyntax declarationExpressionSyntax = null;
					ExpressionSyntax expressionSyntax = null;
					this._nodeBinder.BindDeconstruction(assignmentExpressionSyntax, assignmentExpressionSyntax.Left, assignmentExpressionSyntax.Right, diagnostics, ref declarationExpressionSyntax, ref expressionSyntax, false, null);
				}
				return this._type.Value;
			}

			// Token: 0x1700289C RID: 10396
			// (get) Token: 0x06008FD9 RID: 36825 RVA: 0x001F9DF0 File Offset: 0x001F7FF0
			internal override SyntaxNode ForbiddenZone
			{
				get
				{
					SyntaxKind syntaxKind = this._deconstruction.Kind();
					if (syntaxKind == SyntaxKind.SimpleAssignmentExpression)
					{
						return this._deconstruction;
					}
					if (syntaxKind != SyntaxKind.ForEachVariableStatement)
					{
						return null;
					}
					return null;
				}
			}

			// Token: 0x04002775 RID: 10101
			private readonly SyntaxNode _deconstruction;

			// Token: 0x04002776 RID: 10102
			private readonly Binder _nodeBinder;
		}

		// Token: 0x0200091F RID: 2335
		private class LocalSymbolWithEnclosingContext : SourceLocalSymbol
		{
			// Token: 0x06008FDA RID: 36826 RVA: 0x001F9E25 File Offset: 0x001F8025
			public LocalSymbolWithEnclosingContext(Symbol containingSymbol, Binder scopeBinder, Binder nodeBinder, TypeSyntax typeSyntax, SyntaxToken identifierToken, LocalDeclarationKind declarationKind, SyntaxNode nodeToBind, SyntaxNode forbiddenZone)
				: base(containingSymbol, scopeBinder, false, typeSyntax, identifierToken, declarationKind)
			{
				this._nodeBinder = nodeBinder;
				this._nodeToBind = nodeToBind;
				this._forbiddenZone = forbiddenZone;
			}

			// Token: 0x1700289D RID: 10397
			// (get) Token: 0x06008FDB RID: 36827 RVA: 0x001F9E4D File Offset: 0x001F804D
			internal override SyntaxNode ForbiddenZone
			{
				get
				{
					return this._forbiddenZone;
				}
			}

			// Token: 0x1700289E RID: 10398
			// (get) Token: 0x06008FDC RID: 36828 RVA: 0x001F9E55 File Offset: 0x001F8055
			internal override ErrorCode ForbiddenDiagnostic
			{
				get
				{
					return ErrorCode.ERR_ImplicitlyTypedOutVariableUsedInTheSameArgumentList;
				}
			}

			// Token: 0x06008FDD RID: 36829 RVA: 0x001F9E5C File Offset: 0x001F805C
			protected override TypeWithAnnotations InferTypeOfVarVariable(BindingDiagnosticBag diagnostics)
			{
				SyntaxKind syntaxKind = this._nodeToBind.Kind();
				if (syntaxKind <= SyntaxKind.GotoCaseStatement)
				{
					if (syntaxKind != SyntaxKind.ArgumentList)
					{
						if (syntaxKind == SyntaxKind.VariableDeclarator)
						{
							this._nodeBinder.BindDeclaratorArguments((VariableDeclaratorSyntax)this._nodeToBind, diagnostics);
							goto IL_0195;
						}
						if (syntaxKind == SyntaxKind.GotoCaseStatement)
						{
							this._nodeBinder.BindStatement((GotoStatementSyntax)this._nodeToBind, diagnostics);
							goto IL_0195;
						}
					}
					else
					{
						SyntaxNode parent = this._nodeToBind.Parent;
						ConstructorInitializerSyntax constructorInitializerSyntax = parent as ConstructorInitializerSyntax;
						if (constructorInitializerSyntax != null)
						{
							this._nodeBinder.BindConstructorInitializer(constructorInitializerSyntax, diagnostics);
							goto IL_0195;
						}
						PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = parent as PrimaryConstructorBaseTypeSyntax;
						if (primaryConstructorBaseTypeSyntax == null)
						{
							throw ExceptionUtilities.UnexpectedValue(this._nodeToBind.Parent);
						}
						this._nodeBinder.BindConstructorInitializer(primaryConstructorBaseTypeSyntax, diagnostics);
						goto IL_0195;
					}
				}
				else if (syntaxKind <= SyntaxKind.CasePatternSwitchLabel)
				{
					if (syntaxKind - SyntaxKind.BaseConstructorInitializer <= 1)
					{
						ConstructorInitializerSyntax constructorInitializerSyntax2 = (ConstructorInitializerSyntax)this._nodeToBind;
						this._nodeBinder.BindConstructorInitializer(constructorInitializerSyntax2, diagnostics);
						goto IL_0195;
					}
					if (syntaxKind == SyntaxKind.CasePatternSwitchLabel)
					{
						this._nodeBinder.BindPatternSwitchLabelForInference((CasePatternSwitchLabelSyntax)this._nodeToBind, diagnostics);
						goto IL_0195;
					}
				}
				else
				{
					if (syntaxKind == SyntaxKind.SwitchExpressionArm)
					{
						SwitchExpressionArmSyntax switchExpressionArmSyntax = (SwitchExpressionArmSyntax)this._nodeToBind;
						((SwitchExpressionArmBinder)this._nodeBinder).BindSwitchExpressionArm(switchExpressionArmSyntax, diagnostics);
						goto IL_0195;
					}
					if (syntaxKind == SyntaxKind.PrimaryConstructorBaseType)
					{
						this._nodeBinder.BindConstructorInitializer((PrimaryConstructorBaseTypeSyntax)this._nodeToBind, diagnostics);
						goto IL_0195;
					}
				}
				this._nodeBinder.BindExpression((ExpressionSyntax)this._nodeToBind, diagnostics);
				IL_0195:
				if (this._type == null)
				{
					base.SetTypeWithAnnotations(TypeWithAnnotations.Create(this._nodeBinder.CreateErrorType("var"), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
				}
				return this._type.Value;
			}

			// Token: 0x04002777 RID: 10103
			private readonly SyntaxNode _forbiddenZone;

			// Token: 0x04002778 RID: 10104
			private readonly Binder _nodeBinder;

			// Token: 0x04002779 RID: 10105
			private readonly SyntaxNode _nodeToBind;
		}
	}
}
