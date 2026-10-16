using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200033D RID: 829
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AnalyzerAnalysisContext : AnalysisContext
	{
		// Token: 0x060023AF RID: 9135 RVA: 0x0006FFCC File Offset: 0x0006E1CC
		public AnalyzerAnalysisContext(DiagnosticAnalyzer analyzer, HostSessionStartAnalysisScope scope)
		{
			this._analyzer = analyzer;
			this._scope = scope;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x0006FFE2 File Offset: 0x0006E1E2
		public override void RegisterCompilationStartAction(Action<CompilationStartAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CompilationStartAnalysisContext>(action);
			this._scope.RegisterCompilationStartAction(this._analyzer, action);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x0006FFFC File Offset: 0x0006E1FC
		public override void RegisterCompilationAction(Action<CompilationAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CompilationAnalysisContext>(action);
			this._scope.RegisterCompilationAction(this._analyzer, action);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00070016 File Offset: 0x0006E216
		public override void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SyntaxTreeAnalysisContext>(action);
			this._scope.RegisterSyntaxTreeAction(this._analyzer, action);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00070030 File Offset: 0x0006E230
		public override void RegisterAdditionalFileAction(Action<AdditionalFileAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<AdditionalFileAnalysisContext>(action);
			this._scope.RegisterAdditionalFileAction(this._analyzer, action);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0007004A File Offset: 0x0006E24A
		public override void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SemanticModelAnalysisContext>(action);
			this._scope.RegisterSemanticModelAction(this._analyzer, action);
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00070064 File Offset: 0x0006E264
		[NullableContext(0)]
		public override void RegisterSymbolAction([Nullable(1)] Action<SymbolAnalysisContext> action, ImmutableArray<SymbolKind> symbolKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SymbolAnalysisContext>(action, symbolKinds);
			this._scope.RegisterSymbolAction(this._analyzer, action, symbolKinds);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00070080 File Offset: 0x0006E280
		public override void RegisterSymbolStartAction(Action<SymbolStartAnalysisContext> action, SymbolKind symbolKind)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SymbolStartAnalysisContext>(action);
			this._scope.RegisterSymbolStartAction(this._analyzer, action, symbolKind);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0007009B File Offset: 0x0006E29B
		[NullableContext(0)]
		public override void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockStartAnalysisContext<TLanguageKindEnum>>(action);
			this._scope.RegisterCodeBlockStartAction<TLanguageKindEnum>(this._analyzer, action);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000700B5 File Offset: 0x0006E2B5
		public override void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<CodeBlockAnalysisContext>(action);
			this._scope.RegisterCodeBlockAction(this._analyzer, action);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000700CF File Offset: 0x0006E2CF
		[NullableContext(0)]
		public override void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<SyntaxNodeAnalysisContext, TLanguageKindEnum>(action, syntaxKinds);
			this._scope.RegisterSyntaxNodeAction<TLanguageKindEnum>(this._analyzer, action, syntaxKinds);
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000700EB File Offset: 0x0006E2EB
		[NullableContext(0)]
		public override void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationAnalysisContext>(action, operationKinds);
			this._scope.RegisterOperationAction(this._analyzer, action, operationKinds);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00070107 File Offset: 0x0006E307
		public override void RegisterOperationBlockStartAction(Action<OperationBlockStartAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockStartAnalysisContext>(action);
			this._scope.RegisterOperationBlockStartAction(this._analyzer, action);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00070121 File Offset: 0x0006E321
		public override void RegisterOperationBlockAction(Action<OperationBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockAnalysisContext>(action);
			this._scope.RegisterOperationBlockAction(this._analyzer, action);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0007013B File Offset: 0x0006E33B
		public override void EnableConcurrentExecution()
		{
			this._scope.EnableConcurrentExecution(this._analyzer);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x0007014E File Offset: 0x0006E34E
		public override void ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags mode)
		{
			this._scope.ConfigureGeneratedCodeAnalysis(this._analyzer, mode);
		}

		// Token: 0x04000F54 RID: 3924
		private readonly DiagnosticAnalyzer _analyzer;

		// Token: 0x04000F55 RID: 3925
		private readonly HostSessionStartAnalysisScope _scope;
	}
}
