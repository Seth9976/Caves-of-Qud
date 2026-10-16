using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000319 RID: 793
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AnalysisContext
	{
		// Token: 0x060022D3 RID: 8915
		public abstract void RegisterCompilationStartAction(Action<CompilationStartAnalysisContext> action);

		// Token: 0x060022D4 RID: 8916
		public abstract void RegisterCompilationAction(Action<CompilationAnalysisContext> action);

		// Token: 0x060022D5 RID: 8917
		public abstract void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action);

		// Token: 0x060022D6 RID: 8918 RVA: 0x0006EF03 File Offset: 0x0006D103
		public void RegisterSymbolAction(Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds)
		{
			this.RegisterSymbolAction(action, symbolKinds.AsImmutableOrEmpty<SymbolKind>());
		}

		// Token: 0x060022D7 RID: 8919
		[NullableContext(0)]
		public abstract void RegisterSymbolAction([Nullable(1)] Action<SymbolAnalysisContext> action, ImmutableArray<SymbolKind> symbolKinds);

		// Token: 0x060022D8 RID: 8920 RVA: 0x0006EF12 File Offset: 0x0006D112
		public virtual void RegisterSymbolStartAction(Action<SymbolStartAnalysisContext> action, SymbolKind symbolKind)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022D9 RID: 8921
		[NullableContext(0)]
		public abstract void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action) where TLanguageKindEnum : struct;

		// Token: 0x060022DA RID: 8922
		public abstract void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action);

		// Token: 0x060022DB RID: 8923
		public abstract void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action);

		// Token: 0x060022DC RID: 8924 RVA: 0x0006EF19 File Offset: 0x0006D119
		public virtual void RegisterAdditionalFileAction(Action<AdditionalFileAnalysisContext> action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0006EF20 File Offset: 0x0006D120
		[NullableContext(0)]
		public void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, [Nullable(new byte[] { 1, 0 })] params TLanguageKindEnum[] syntaxKinds) where TLanguageKindEnum : struct
		{
			this.RegisterSyntaxNodeAction<TLanguageKindEnum>(action, syntaxKinds.AsImmutableOrEmpty<TLanguageKindEnum>());
		}

		// Token: 0x060022DE RID: 8926
		[NullableContext(0)]
		public abstract void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds) where TLanguageKindEnum : struct;

		// Token: 0x060022DF RID: 8927 RVA: 0x0006EF2F File Offset: 0x0006D12F
		public virtual void RegisterOperationBlockStartAction(Action<OperationBlockStartAnalysisContext> action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0006EF36 File Offset: 0x0006D136
		public virtual void RegisterOperationBlockAction(Action<OperationBlockAnalysisContext> action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0006EF3D File Offset: 0x0006D13D
		public void RegisterOperationAction(Action<OperationAnalysisContext> action, params OperationKind[] operationKinds)
		{
			this.RegisterOperationAction(action, operationKinds.AsImmutableOrEmpty<OperationKind>());
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0006EF4C File Offset: 0x0006D14C
		[NullableContext(0)]
		public virtual void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0006EF53 File Offset: 0x0006D153
		public virtual void EnableConcurrentExecution()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0006EF5A File Offset: 0x0006D15A
		public virtual void ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags analysisMode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0006EF61 File Offset: 0x0006D161
		public bool TryGetValue<[Nullable(2)] TValue>(SourceText text, SourceTextValueProvider<TValue> valueProvider, [MaybeNullWhen(false)] out TValue value)
		{
			return this.TryGetValue<SourceText, TValue>(text, valueProvider.CoreValueProvider, out value);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0006EF71 File Offset: 0x0006D171
		private bool TryGetValue<TKey, [Nullable(2)] TValue>(TKey key, AnalysisValueProvider<TKey, TValue> valueProvider, [MaybeNullWhen(false)] out TValue value) where TKey : class
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<TKey, TValue>(key, valueProvider);
			return valueProvider.TryGetValue(key, out value);
		}
	}
}
