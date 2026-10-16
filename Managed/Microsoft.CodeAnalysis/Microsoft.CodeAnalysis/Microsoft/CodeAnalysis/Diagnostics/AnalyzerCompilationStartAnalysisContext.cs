using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200033E RID: 830
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AnalyzerCompilationStartAnalysisContext : CompilationStartAnalysisContext
	{
		// Token: 0x060023BF RID: 9151 RVA: 0x00070162 File Offset: 0x0006E362
		public AnalyzerCompilationStartAnalysisContext(DiagnosticAnalyzer analyzer, HostCompilationStartAnalysisScope scope, Compilation compilation, AnalyzerOptions options, CompilationAnalysisValueProviderFactory compilationAnalysisValueProviderFactory, CancellationToken cancellationToken)
			: base(compilation, options, cancellationToken)
		{
			this._analyzer = analyzer;
			this._scope = scope;
			this._compilationAnalysisValueProviderFactory = compilationAnalysisValueProviderFactory;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00070185 File Offset: 0x0006E385
		public override void RegisterCompilationEndAction(Action<CompilationAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CompilationAnalysisContext>(action);
			this._scope.RegisterCompilationEndAction(this._analyzer, action);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0007019F File Offset: 0x0006E39F
		public override void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SyntaxTreeAnalysisContext>(action);
			this._scope.RegisterSyntaxTreeAction(this._analyzer, action);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000701B9 File Offset: 0x0006E3B9
		public override void RegisterAdditionalFileAction(Action<AdditionalFileAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<AdditionalFileAnalysisContext>(action);
			this._scope.RegisterAdditionalFileAction(this._analyzer, action);
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000701D3 File Offset: 0x0006E3D3
		public override void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SemanticModelAnalysisContext>(action);
			this._scope.RegisterSemanticModelAction(this._analyzer, action);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000701ED File Offset: 0x0006E3ED
		[NullableContext(0)]
		public override void RegisterSymbolAction([Nullable(1)] Action<SymbolAnalysisContext> action, ImmutableArray<SymbolKind> symbolKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SymbolAnalysisContext>(action, symbolKinds);
			this._scope.RegisterSymbolAction(this._analyzer, action, symbolKinds);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00070209 File Offset: 0x0006E409
		public override void RegisterSymbolStartAction(Action<SymbolStartAnalysisContext> action, SymbolKind symbolKind)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SymbolStartAnalysisContext>(action);
			this._scope.RegisterSymbolStartAction(this._analyzer, action, symbolKind);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00070224 File Offset: 0x0006E424
		[NullableContext(0)]
		public override void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockStartAnalysisContext<TLanguageKindEnum>>(action);
			this._scope.RegisterCodeBlockStartAction<TLanguageKindEnum>(this._analyzer, action);
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0007023E File Offset: 0x0006E43E
		public override void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockAnalysisContext>(action);
			this._scope.RegisterCodeBlockAction(this._analyzer, action);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00070258 File Offset: 0x0006E458
		[NullableContext(0)]
		public override void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SyntaxNodeAnalysisContext, TLanguageKindEnum>(action, syntaxKinds);
			this._scope.RegisterSyntaxNodeAction<TLanguageKindEnum>(this._analyzer, action, syntaxKinds);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00070274 File Offset: 0x0006E474
		public override void RegisterOperationBlockStartAction(Action<OperationBlockStartAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockStartAnalysisContext>(action);
			this._scope.RegisterOperationBlockStartAction(this._analyzer, action);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0007028E File Offset: 0x0006E48E
		public override void RegisterOperationBlockAction(Action<OperationBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockAnalysisContext>(action);
			this._scope.RegisterOperationBlockAction(this._analyzer, action);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000702A8 File Offset: 0x0006E4A8
		[NullableContext(0)]
		public override void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationAnalysisContext>(action, operationKinds);
			this._scope.RegisterOperationAction(this._analyzer, action, operationKinds);
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000702C4 File Offset: 0x0006E4C4
		internal override bool TryGetValueCore<TKey, [Nullable(2)] TValue>(TKey key, AnalysisValueProvider<TKey, TValue> valueProvider, [MaybeNullWhen(false)] out TValue value)
		{
			return this._compilationAnalysisValueProviderFactory.GetValueProvider<TKey, TValue>(valueProvider).TryGetValue(key, out value);
		}

		// Token: 0x04000F56 RID: 3926
		private readonly DiagnosticAnalyzer _analyzer;

		// Token: 0x04000F57 RID: 3927
		private readonly HostCompilationStartAnalysisScope _scope;

		// Token: 0x04000F58 RID: 3928
		private readonly CompilationAnalysisValueProviderFactory _compilationAnalysisValueProviderFactory;
	}
}
