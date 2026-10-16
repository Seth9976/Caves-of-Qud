using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200052C RID: 1324
	internal sealed class SourceConstructorSymbol : SourceConstructorSymbolBase
	{
		// Token: 0x06006F46 RID: 28486 RVA: 0x0019901C File Offset: 0x0019721C
		public static SourceConstructorSymbol CreateConstructorSymbol(SourceMemberContainerTypeSymbol containingType, ConstructorDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
		{
			MethodKind methodKind = (syntax.Modifiers.Any(SyntaxKind.StaticKeyword) ? MethodKind.StaticConstructor : MethodKind.Constructor);
			return new SourceConstructorSymbol(containingType, syntax.Identifier.GetLocation(), syntax, methodKind, isNullableAnalysisEnabled, diagnostics);
		}

		// Token: 0x06006F47 RID: 28487 RVA: 0x0019905C File Offset: 0x0019725C
		private SourceConstructorSymbol(SourceMemberContainerTypeSymbol containingType, Location location, ConstructorDeclarationSyntax syntax, MethodKind methodKind, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(containingType, location, syntax, SyntaxFacts.HasYieldOperations(syntax))
		{
			bool flag = syntax.Body != null;
			this._isExpressionBodied = !flag && syntax.ExpressionBody != null;
			bool flag2 = flag || this._isExpressionBodied;
			ConstructorInitializerSyntax initializer = syntax.Initializer;
			this._hasThisInitializer = initializer != null && initializer.Kind() == SyntaxKind.ThisConstructorInitializer;
			bool flag3;
			DeclarationModifiers declarationModifiers = this.MakeModifiers(syntax.Modifiers, methodKind, flag2, location, diagnostics, out flag3);
			base.MakeFlags(methodKind, declarationModifiers, true, false, isNullableAnalysisEnabled, false);
			if (syntax.Identifier.ValueText != containingType.Name)
			{
				diagnostics.Add(ErrorCode.ERR_MemberNeedsType, location);
			}
			if (this.IsExtern)
			{
				if (methodKind == MethodKind.Constructor && syntax.Initializer != null)
				{
					diagnostics.Add(ErrorCode.ERR_ExternHasConstructorInitializer, location, new object[] { this });
				}
				if (flag2)
				{
					diagnostics.Add(ErrorCode.ERR_ExternHasBody, location, new object[] { this });
				}
			}
			if (methodKind == MethodKind.StaticConstructor)
			{
				base.CheckFeatureAvailabilityAndRuntimeSupport(syntax, location, flag2, diagnostics);
			}
			CSDiagnosticInfo csdiagnosticInfo = ModifierUtils.CheckAccessibility(this.DeclarationModifiers, this, false);
			if (csdiagnosticInfo != null)
			{
				diagnostics.Add(csdiagnosticInfo, location);
			}
			if (!flag3)
			{
				this.CheckModifiers(methodKind, flag2, location, diagnostics);
			}
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
		}

		// Token: 0x06006F48 RID: 28488 RVA: 0x001991AC File Offset: 0x001973AC
		internal ConstructorDeclarationSyntax GetSyntax()
		{
			return (ConstructorDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x06006F49 RID: 28489 RVA: 0x001991D2 File Offset: 0x001973D2
		protected override ParameterListSyntax GetParameterList()
		{
			return this.GetSyntax().ParameterList;
		}

		// Token: 0x06006F4A RID: 28490 RVA: 0x001991DF File Offset: 0x001973DF
		protected override CSharpSyntaxNode GetInitializer()
		{
			return this.GetSyntax().Initializer;
		}

		// Token: 0x06006F4B RID: 28491 RVA: 0x001991EC File Offset: 0x001973EC
		private DeclarationModifiers MakeModifiers(SyntaxTokenList modifiers, MethodKind methodKind, bool hasBody, Location location, BindingDiagnosticBag diagnostics, out bool modifierErrors)
		{
			DeclarationModifiers declarationModifiers = ((methodKind == MethodKind.StaticConstructor) ? DeclarationModifiers.None : DeclarationModifiers.Private);
			DeclarationModifiers declarationModifiers2 = ModifierUtils.MakeAndCheckNontypeMemberModifiers(modifiers, declarationModifiers, DeclarationModifiers.Static | DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected | DeclarationModifiers.Extern | DeclarationModifiers.Unsafe, location, diagnostics, out modifierErrors);
			this.CheckUnsafeModifier(declarationModifiers2, diagnostics);
			if (methodKind == MethodKind.StaticConstructor)
			{
				if ((declarationModifiers2 & DeclarationModifiers.AccessibilityMask) != DeclarationModifiers.None && this.ContainingType.Name == ((ConstructorDeclarationSyntax)this.SyntaxNode).Identifier.ValueText)
				{
					diagnostics.Add(ErrorCode.ERR_StaticConstructorWithAccessModifiers, location, new object[] { this });
					declarationModifiers2 &= ~(DeclarationModifiers.Public | DeclarationModifiers.Protected | DeclarationModifiers.Internal | DeclarationModifiers.ProtectedInternal | DeclarationModifiers.Private | DeclarationModifiers.PrivateProtected);
					modifierErrors = true;
				}
				declarationModifiers2 |= DeclarationModifiers.Private;
				if (this.ContainingType.IsInterface)
				{
					ModifierUtils.ReportDefaultInterfaceImplementationModifiers(hasBody, declarationModifiers2, DeclarationModifiers.Extern, location, diagnostics);
				}
			}
			return declarationModifiers2;
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x001992A8 File Offset: 0x001974A8
		private void CheckModifiers(MethodKind methodKind, bool hasBody, Location location, BindingDiagnosticBag diagnostics)
		{
			if (!hasBody && !this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_ConcreteMissingBody, location, new object[] { this });
				return;
			}
			if (this.ContainingType.IsSealed && this.DeclaredAccessibility.HasProtected() && !this.IsOverride)
			{
				diagnostics.Add(AccessCheck.GetProtectedMemberInSealedTypeError(this.ContainingType), location, new object[] { this });
				return;
			}
			if (this.ContainingType.IsStatic && methodKind == MethodKind.Constructor)
			{
				diagnostics.Add(ErrorCode.ERR_ConstructorInStaticClass, location);
			}
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x00199339 File Offset: 0x00197539
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(((ConstructorDeclarationSyntax)this.SyntaxNode).AttributeLists);
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06006F4E RID: 28494 RVA: 0x00199350 File Offset: 0x00197550
		internal override bool IsExpressionBodied
		{
			get
			{
				return this._isExpressionBodied;
			}
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x00199358 File Offset: 0x00197558
		internal override bool IsNullableAnalysisEnabled()
		{
			if (!this._hasThisInitializer)
			{
				return ((SourceMemberContainerTypeSymbol)this.ContainingType).IsNullableEnabledForConstructorsAndInitializers(this.IsStatic);
			}
			return this.flags.IsNullableAnalysisEnabled;
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06006F50 RID: 28496 RVA: 0x00199384 File Offset: 0x00197584
		protected override bool AllowRefOrOut
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006F51 RID: 28497 RVA: 0x00199388 File Offset: 0x00197588
		protected override bool IsWithinExpressionOrBlockBody(int position, out int offset)
		{
			ConstructorDeclarationSyntax syntax = this.GetSyntax();
			BlockSyntax body = syntax.Body;
			if (body != null && body.Span.Contains(position))
			{
				offset = position - syntax.Body.Span.Start;
				return true;
			}
			ArrowExpressionClauseSyntax expressionBody = syntax.ExpressionBody;
			if (expressionBody != null && expressionBody.Span.Contains(position))
			{
				offset = position - syntax.ExpressionBody.Span.Start;
				return true;
			}
			offset = -1;
			return false;
		}

		// Token: 0x04001B4D RID: 6989
		private readonly bool _isExpressionBodied;

		// Token: 0x04001B4E RID: 6990
		private readonly bool _hasThisInitializer;
	}
}
