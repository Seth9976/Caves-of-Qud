using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200031B RID: 795
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CompilationStartAnalysisContext
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x0006EF8A File Offset: 0x0006D18A
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0006EF92 File Offset: 0x0006D192
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0006EF9A File Offset: 0x0006D19A
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x0006EFA2 File Offset: 0x0006D1A2
		protected CompilationStartAnalysisContext(Compilation compilation, AnalyzerOptions options, CancellationToken cancellationToken)
		{
			this._compilation = compilation;
			this._options = options;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060022EC RID: 8940
		public abstract void RegisterCompilationEndAction(Action<CompilationAnalysisContext> action);

		// Token: 0x060022ED RID: 8941
		public abstract void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action);

		// Token: 0x060022EE RID: 8942 RVA: 0x0006EFBF File Offset: 0x0006D1BF
		public void RegisterSymbolAction(Action<SymbolAnalysisContext> action, params SymbolKind[] symbolKinds)
		{
			this.RegisterSymbolAction(action, symbolKinds.AsImmutableOrEmpty<SymbolKind>());
		}

		// Token: 0x060022EF RID: 8943
		[NullableContext(0)]
		public abstract void RegisterSymbolAction([Nullable(1)] Action<SymbolAnalysisContext> action, ImmutableArray<SymbolKind> symbolKinds);

		// Token: 0x060022F0 RID: 8944 RVA: 0x0006EFCE File Offset: 0x0006D1CE
		public virtual void RegisterSymbolStartAction(Action<SymbolStartAnalysisContext> action, SymbolKind symbolKind)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022F1 RID: 8945
		[NullableContext(0)]
		public abstract void RegisterCodeBlockStartAction<TLanguageKindEnum>([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action) where TLanguageKindEnum : struct;

		// Token: 0x060022F2 RID: 8946
		public abstract void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action);

		// Token: 0x060022F3 RID: 8947 RVA: 0x0006EFD5 File Offset: 0x0006D1D5
		public virtual void RegisterOperationBlockStartAction(Action<OperationBlockStartAnalysisContext> action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x0006EFDC File Offset: 0x0006D1DC
		public virtual void RegisterOperationBlockAction(Action<OperationBlockAnalysisContext> action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022F5 RID: 8949
		public abstract void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action);

		// Token: 0x060022F6 RID: 8950 RVA: 0x0006EFE3 File Offset: 0x0006D1E3
		public virtual void RegisterAdditionalFileAction(Action<AdditionalFileAnalysisContext> action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0006EFEA File Offset: 0x0006D1EA
		[NullableContext(0)]
		public void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, [Nullable(new byte[] { 1, 0 })] params TLanguageKindEnum[] syntaxKinds) where TLanguageKindEnum : struct
		{
			this.RegisterSyntaxNodeAction<TLanguageKindEnum>(action, syntaxKinds.AsImmutableOrEmpty<TLanguageKindEnum>());
		}

		// Token: 0x060022F8 RID: 8952
		[NullableContext(0)]
		public abstract void RegisterSyntaxNodeAction<TLanguageKindEnum>([Nullable(1)] Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds) where TLanguageKindEnum : struct;

		// Token: 0x060022F9 RID: 8953 RVA: 0x0006EFF9 File Offset: 0x0006D1F9
		public void RegisterOperationAction(Action<OperationAnalysisContext> action, params OperationKind[] operationKinds)
		{
			this.RegisterOperationAction(action, operationKinds.AsImmutableOrEmpty<OperationKind>());
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0006F008 File Offset: 0x0006D208
		[NullableContext(0)]
		public virtual void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0006F00F File Offset: 0x0006D20F
		public bool TryGetValue<[Nullable(2)] TValue>(SourceText text, SourceTextValueProvider<TValue> valueProvider, [MaybeNullWhen(false)] out TValue value)
		{
			return this.TryGetValue<SourceText, TValue>(text, valueProvider.CoreValueProvider, out value);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0006F01F File Offset: 0x0006D21F
		public bool TryGetValue<[Nullable(2)] TValue>(SyntaxTree tree, SyntaxTreeValueProvider<TValue> valueProvider, [MaybeNullWhen(false)] out TValue value)
		{
			return this.TryGetValue<SyntaxTree, TValue>(tree, valueProvider.CoreValueProvider, out value);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0006F02F File Offset: 0x0006D22F
		private bool TryGetValue<TKey, [Nullable(2)] TValue>(TKey key, AnalysisValueProvider<TKey, TValue> valueProvider, [MaybeNullWhen(false)] out TValue value) where TKey : class
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<TKey, TValue>(key, valueProvider);
			return this.TryGetValueCore<TKey, TValue>(key, valueProvider, out value);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0006F041 File Offset: 0x0006D241
		internal virtual bool TryGetValueCore<TKey, [Nullable(2)] TValue>(TKey key, AnalysisValueProvider<TKey, TValue> valueProvider, [MaybeNullWhen(false)] out TValue value) where TKey : class
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000EEC RID: 3820
		private readonly Compilation _compilation;

		// Token: 0x04000EED RID: 3821
		private readonly AnalyzerOptions _options;

		// Token: 0x04000EEE RID: 3822
		private readonly CancellationToken _cancellationToken;
	}
}
