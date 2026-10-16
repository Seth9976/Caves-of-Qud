using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200052E RID: 1326
	internal sealed class SourceCustomEventAccessorSymbol : SourceEventAccessorSymbol
	{
		// Token: 0x06006F68 RID: 28520 RVA: 0x00199788 File Offset: 0x00197988
		internal SourceCustomEventAccessorSymbol(SourceEventSymbol @event, AccessorDeclarationSyntax syntax, EventSymbol explicitlyImplementedEventOpt, string aliasQualifierOpt, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(@event, syntax.GetReference(), ImmutableArray.Create<Location>(syntax.Keyword.GetLocation()), explicitlyImplementedEventOpt, aliasQualifierOpt, syntax.Kind() == SyntaxKind.AddAccessorDeclaration, SyntaxFacts.HasYieldOperations(syntax.Body), isNullableAnalysisEnabled)
		{
			base.CheckFeatureAvailabilityAndRuntimeSupport(syntax, base.Location, true, diagnostics);
			if ((syntax.Body != null || syntax.ExpressionBody != null) && this.IsExtern && !this.IsAbstract)
			{
				diagnostics.Add(ErrorCode.ERR_ExternHasBody, base.Location, new object[] { this });
			}
			if (syntax.Modifiers.Count > 0)
			{
				diagnostics.Add(ErrorCode.ERR_NoModifiersOnAccessor, syntax.Modifiers[0].GetLocation());
			}
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x00199868 File Offset: 0x00197A68
		internal AccessorDeclarationSyntax GetSyntax()
		{
			return (AccessorDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x06006F6A RID: 28522 RVA: 0x0019988E File Offset: 0x00197A8E
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this.AssociatedSymbol.DeclaredAccessibility;
			}
		}

		// Token: 0x06006F6B RID: 28523 RVA: 0x0019989B File Offset: 0x00197A9B
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetSyntax().AttributeLists);
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x06006F6C RID: 28524 RVA: 0x001998AD File Offset: 0x00197AAD
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x06006F6D RID: 28525 RVA: 0x001998B0 File Offset: 0x00197AB0
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x06006F6E RID: 28526 RVA: 0x001998B4 File Offset: 0x00197AB4
		internal override bool IsExpressionBodied
		{
			get
			{
				AccessorDeclarationSyntax syntax = this.GetSyntax();
				bool flag = syntax.Body != null;
				bool flag2 = syntax.ExpressionBody != null;
				return !flag && flag2;
			}
		}
	}
}
