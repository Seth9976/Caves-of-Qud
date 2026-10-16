using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000531 RID: 1329
	internal sealed class SourceDestructorSymbol : SourceMemberMethodSymbol
	{
		// Token: 0x06006F8D RID: 28557 RVA: 0x0019A0B0 File Offset: 0x001982B0
		internal SourceDestructorSymbol(SourceMemberContainerTypeSymbol containingType, DestructorDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax.GetReference(), syntax.Identifier.GetLocation(), SyntaxFacts.HasYieldOperations(syntax.Body))
		{
			Location location = this.Locations[0];
			bool flag;
			DeclarationModifiers declarationModifiers = this.MakeModifiers(syntax.Modifiers, location, diagnostics, out flag);
			base.MakeFlags(MethodKind.Destructor, declarationModifiers, true, false, isNullableAnalysisEnabled, false);
			if (syntax.Identifier.ValueText != containingType.Name)
			{
				diagnostics.Add(ErrorCode.ERR_BadDestructorName, syntax.Identifier.GetLocation());
			}
			bool flag2 = syntax.Body != null;
			this._isExpressionBodied = !flag2 && syntax.ExpressionBody != null;
			if ((flag2 || this._isExpressionBodied) && this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_ExternHasBody, location, new object[] { this });
			}
			if (!flag && !flag2 && !this._isExpressionBodied && !this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_ConcreteMissingBody, location, new object[] { this });
			}
			if (containingType.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_DestructorInStaticClass, location, new object[] { this });
			}
			else if (!containingType.IsReferenceType)
			{
				diagnostics.Add(ErrorCode.ERR_OnlyClassesCanContainDestructors, location, new object[] { this });
			}
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
		}

		// Token: 0x06006F8E RID: 28558 RVA: 0x0019A21C File Offset: 0x0019841C
		protected override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			DestructorDeclarationSyntax syntax = this.GetSyntax();
			Binder binder = this.DeclaringCompilation.GetBinderFactory(this.syntaxReferenceOpt.SyntaxTree, false).GetBinder(syntax, syntax, this);
			this._lazyReturnType = TypeWithAnnotations.Create(binder.GetSpecialType(SpecialType.System_Void, diagnostics, syntax), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x06006F8F RID: 28559 RVA: 0x0019A270 File Offset: 0x00198470
		internal DestructorDeclarationSyntax GetSyntax()
		{
			return (DestructorDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x17001DC7 RID: 7623
		// (get) Token: 0x06006F90 RID: 28560 RVA: 0x0019A296 File Offset: 0x00198496
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DC8 RID: 7624
		// (get) Token: 0x06006F91 RID: 28561 RVA: 0x0019A299 File Offset: 0x00198499
		internal override int ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001DC9 RID: 7625
		// (get) Token: 0x06006F92 RID: 28562 RVA: 0x0019A29C File Offset: 0x0019849C
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x06006F93 RID: 28563 RVA: 0x0019A2A3 File Offset: 0x001984A3
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x0019A2AA File Offset: 0x001984AA
		public override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x0019A2B1 File Offset: 0x001984B1
		public override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x06006F96 RID: 28566 RVA: 0x0019A2B8 File Offset: 0x001984B8
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06006F97 RID: 28567 RVA: 0x0019A2BB File Offset: 0x001984BB
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyReturnType;
			}
		}

		// Token: 0x06006F98 RID: 28568 RVA: 0x0019A2CC File Offset: 0x001984CC
		private DeclarationModifiers MakeModifiers(SyntaxTokenList modifiers, Location location, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			DeclarationModifiers declarationModifiers = ModifierUtils.MakeAndCheckNontypeMemberModifiers(modifiers, DeclarationModifiers.None, DeclarationModifiers.Extern | DeclarationModifiers.Unsafe, location, diagnostics, out modifierErrors);
			this.CheckUnsafeModifier(declarationModifiers, diagnostics);
			return (declarationModifiers & ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected)) | DeclarationModifiers.Protected;
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06006F99 RID: 28569 RVA: 0x0019A2FE File Offset: 0x001984FE
		public override string Name
		{
			get
			{
				return "Finalize";
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06006F9A RID: 28570 RVA: 0x0019A305 File Offset: 0x00198505
		internal override bool IsExpressionBodied
		{
			get
			{
				return this._isExpressionBodied;
			}
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x0019A30D File Offset: 0x0019850D
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetSyntax().AttributeLists);
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x0019A320 File Offset: 0x00198520
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetReturnTypeAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x0019A33B File Offset: 0x0019853B
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return true;
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06006F9E RID: 28574 RVA: 0x0019A33E File Offset: 0x0019853E
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006F9F RID: 28575 RVA: 0x0019A341 File Offset: 0x00198541
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return this.ContainingType.BaseTypeNoUseSiteDiagnostics == null;
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x0019A351 File Offset: 0x00198551
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001B5A RID: 7002
		private TypeWithAnnotations _lazyReturnType;

		// Token: 0x04001B5B RID: 7003
		private readonly bool _isExpressionBodied;
	}
}
