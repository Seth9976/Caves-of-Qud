using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000340 RID: 832
	internal sealed class AnalyzerCodeBlockStartAnalysisContext<TLanguageKindEnum> : CodeBlockStartAnalysisContext<TLanguageKindEnum> where TLanguageKindEnum : struct
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x000703B0 File Offset: 0x0006E5B0
		[NullableContext(1)]
		internal AnalyzerCodeBlockStartAnalysisContext(DiagnosticAnalyzer analyzer, [Nullable(new byte[] { 1, 0 })] HostCodeBlockStartAnalysisScope<TLanguageKindEnum> scope, SyntaxNode codeBlock, ISymbol owningSymbol, SemanticModel semanticModel, AnalyzerOptions options, CancellationToken cancellationToken)
			: base(codeBlock, owningSymbol, semanticModel, options, cancellationToken)
		{
			this._analyzer = analyzer;
			this._scope = scope;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000703CF File Offset: 0x0006E5CF
		[NullableContext(1)]
		public override void RegisterCodeBlockEndAction(Action<CodeBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockAnalysisContext>(action);
			this._scope.RegisterCodeBlockEndAction(this._analyzer, action);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000703E9 File Offset: 0x0006E5E9
		public override void RegisterSyntaxNodeAction([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SyntaxNodeAnalysisContext, TLanguageKindEnum>(action, syntaxKinds);
			this._scope.RegisterSyntaxNodeAction(this._analyzer, action, syntaxKinds);
		}

		// Token: 0x04000F5B RID: 3931
		[Nullable(1)]
		private readonly DiagnosticAnalyzer _analyzer;

		// Token: 0x04000F5C RID: 3932
		[Nullable(new byte[] { 1, 0 })]
		private readonly HostCodeBlockStartAnalysisScope<TLanguageKindEnum> _scope;
	}
}
