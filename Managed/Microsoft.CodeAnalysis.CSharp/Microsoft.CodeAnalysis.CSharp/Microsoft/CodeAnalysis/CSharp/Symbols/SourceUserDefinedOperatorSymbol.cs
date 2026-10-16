using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000558 RID: 1368
	internal sealed class SourceUserDefinedOperatorSymbol : SourceUserDefinedOperatorSymbolBase
	{
		// Token: 0x060073F2 RID: 29682 RVA: 0x001B131C File Offset: 0x001AF51C
		public static SourceUserDefinedOperatorSymbol CreateUserDefinedOperatorSymbol(SourceMemberContainerTypeSymbol containingType, OperatorDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
		{
			Location location = syntax.OperatorToken.GetLocation();
			string text = OperatorFacts.OperatorNameFromDeclaration(syntax);
			return new SourceUserDefinedOperatorSymbol(containingType, text, location, syntax, isNullableAnalysisEnabled, diagnostics);
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x001B134C File Offset: 0x001AF54C
		private SourceUserDefinedOperatorSymbol(SourceMemberContainerTypeSymbol containingType, string name, Location location, OperatorDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(MethodKind.UserDefinedOperator, name, containingType, location, syntax, SourceUserDefinedOperatorSymbolBase.MakeDeclarationModifiers(syntax, location, diagnostics), syntax.HasAnyBody(), syntax.Body == null && syntax.ExpressionBody != null, SyntaxFacts.HasYieldOperations(syntax.Body), isNullableAnalysisEnabled, diagnostics)
		{
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
			if (name != "op_Equality" && name != "op_Inequality")
			{
				base.CheckFeatureAvailabilityAndRuntimeSupport(syntax, location, syntax.Body != null || syntax.ExpressionBody != null, diagnostics);
			}
		}

		// Token: 0x060073F4 RID: 29684 RVA: 0x001B13F0 File Offset: 0x001AF5F0
		internal OperatorDeclarationSyntax GetSyntax()
		{
			return (OperatorDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x001B1416 File Offset: 0x001AF616
		protected override int GetParameterCountFromSyntax()
		{
			return this.GetSyntax().ParameterList.ParameterCount;
		}

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x060073F6 RID: 29686 RVA: 0x001B1428 File Offset: 0x001AF628
		protected override Location ReturnTypeLocation
		{
			get
			{
				return this.GetSyntax().ReturnType.Location;
			}
		}

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x060073F7 RID: 29687 RVA: 0x001B143A File Offset: 0x001AF63A
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x001B143D File Offset: 0x001AF63D
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetSyntax().AttributeLists);
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x001B1450 File Offset: 0x001AF650
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters" })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			OperatorDeclarationSyntax syntax = this.GetSyntax();
			return base.MakeParametersAndBindReturnType(syntax, syntax.ReturnType, diagnostics);
		}
	}
}
