using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000557 RID: 1367
	internal sealed class SourceUserDefinedConversionSymbol : SourceUserDefinedOperatorSymbolBase
	{
		// Token: 0x060073EA RID: 29674 RVA: 0x001B11BC File Offset: 0x001AF3BC
		public static SourceUserDefinedConversionSymbol CreateUserDefinedConversionSymbol(SourceMemberContainerTypeSymbol containingType, ConversionOperatorDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
		{
			Location location = syntax.Type.Location;
			string text = (syntax.ImplicitOrExplicitKeyword.IsKind(SyntaxKind.ImplicitKeyword) ? "op_Implicit" : "op_Explicit");
			return new SourceUserDefinedConversionSymbol(containingType, text, location, syntax, isNullableAnalysisEnabled, diagnostics);
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x001B1200 File Offset: 0x001AF400
		private SourceUserDefinedConversionSymbol(SourceMemberContainerTypeSymbol containingType, string name, Location location, ConversionOperatorDeclarationSyntax syntax, bool isNullableAnalysisEnabled, BindingDiagnosticBag diagnostics)
			: base(MethodKind.Conversion, name, containingType, location, syntax, SourceUserDefinedOperatorSymbolBase.MakeDeclarationModifiers(syntax, location, diagnostics), syntax.HasAnyBody(), syntax.Body == null && syntax.ExpressionBody != null, SyntaxFacts.HasYieldOperations(syntax.Body), isNullableAnalysisEnabled, diagnostics)
		{
			Symbol.CheckForBlockAndExpressionBody(syntax.Body, syntax.ExpressionBody, syntax, diagnostics);
			if (syntax.ParameterList.Parameters.Count != 1)
			{
				diagnostics.Add(ErrorCode.ERR_OvlUnaryOperatorExpected, syntax.ParameterList.GetLocation());
			}
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x001B1298 File Offset: 0x001AF498
		internal ConversionOperatorDeclarationSyntax GetSyntax()
		{
			return (ConversionOperatorDeclarationSyntax)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x001B12BE File Offset: 0x001AF4BE
		protected override int GetParameterCountFromSyntax()
		{
			return this.GetSyntax().ParameterList.ParameterCount;
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x060073EE RID: 29678 RVA: 0x001B12D0 File Offset: 0x001AF4D0
		protected override Location ReturnTypeLocation
		{
			get
			{
				return this.GetSyntax().Type.Location;
			}
		}

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x060073EF RID: 29679 RVA: 0x001B12E2 File Offset: 0x001AF4E2
		internal override bool GenerateDebugInfo
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060073F0 RID: 29680 RVA: 0x001B12E5 File Offset: 0x001AF4E5
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this.GetSyntax().AttributeLists);
		}

		// Token: 0x060073F1 RID: 29681 RVA: 0x001B12F8 File Offset: 0x001AF4F8
		[return: TupleElementNames(new string[] { "ReturnType", "Parameters" })]
		protected override ValueTuple<TypeWithAnnotations, ImmutableArray<ParameterSymbol>> MakeParametersAndBindReturnType(BindingDiagnosticBag diagnostics)
		{
			ConversionOperatorDeclarationSyntax syntax = this.GetSyntax();
			return base.MakeParametersAndBindReturnType(syntax, syntax.Type, diagnostics);
		}
	}
}
