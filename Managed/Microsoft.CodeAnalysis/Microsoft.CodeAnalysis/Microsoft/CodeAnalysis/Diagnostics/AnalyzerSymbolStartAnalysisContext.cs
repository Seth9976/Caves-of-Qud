using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200033F RID: 831
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AnalyzerSymbolStartAnalysisContext : SymbolStartAnalysisContext
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x000702D9 File Offset: 0x0006E4D9
		internal AnalyzerSymbolStartAnalysisContext(DiagnosticAnalyzer analyzer, HostSymbolStartAnalysisScope scope, ISymbol owningSymbol, Compilation compilation, AnalyzerOptions options, CancellationToken cancellationToken)
			: base(owningSymbol, compilation, options, cancellationToken)
		{
			this._analyzer = analyzer;
			this._scope = scope;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000702F6 File Offset: 0x0006E4F6
		public override void RegisterSymbolEndAction(Action<SymbolAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SymbolAnalysisContext>(action);
			this._scope.RegisterSymbolEndAction(this._analyzer, action);
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00070310 File Offset: 0x0006E510
		[NullableContext(0)]
		public override void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockStartAnalysisContext<TLanguageKindEnum>>(action);
			this._scope.RegisterCodeBlockStartAction<TLanguageKindEnum>(this._analyzer, action);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x0007032A File Offset: 0x0006E52A
		public override void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockAnalysisContext>(action);
			this._scope.RegisterCodeBlockAction(this._analyzer, action);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00070344 File Offset: 0x0006E544
		[NullableContext(0)]
		public override void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SyntaxNodeAnalysisContext, TLanguageKindEnum>(action, syntaxKinds);
			this._scope.RegisterSyntaxNodeAction<TLanguageKindEnum>(this._analyzer, action, syntaxKinds);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00070360 File Offset: 0x0006E560
		public override void RegisterOperationBlockStartAction(Action<OperationBlockStartAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockStartAnalysisContext>(action);
			this._scope.RegisterOperationBlockStartAction(this._analyzer, action);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x0007037A File Offset: 0x0006E57A
		public override void RegisterOperationBlockAction(Action<OperationBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockAnalysisContext>(action);
			this._scope.RegisterOperationBlockAction(this._analyzer, action);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00070394 File Offset: 0x0006E594
		[NullableContext(0)]
		public override void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationAnalysisContext>(action, operationKinds);
			this._scope.RegisterOperationAction(this._analyzer, action, operationKinds);
		}

		// Token: 0x04000F59 RID: 3929
		private readonly DiagnosticAnalyzer _analyzer;

		// Token: 0x04000F5A RID: 3930
		private readonly HostSymbolStartAnalysisScope _scope;
	}
}
