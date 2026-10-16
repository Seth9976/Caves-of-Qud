using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200031C RID: 796
	[NullableContext(1)]
	[Nullable(0)]
	public struct CompilationAnalysisContext
	{
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0006F048 File Offset: 0x0006D248
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x0006F050 File Offset: 0x0006D250
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0006F058 File Offset: 0x0006D258
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x0006F060 File Offset: 0x0006D260
		public CompilationAnalysisContext(Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this = new CompilationAnalysisContext(compilation, options, reportDiagnostic, isSupportedDiagnostic, null, cancellationToken);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x0006F070 File Offset: 0x0006D270
		internal CompilationAnalysisContext(Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, [Nullable(2)] CompilationAnalysisValueProviderFactory compilationAnalysisValueProviderFactoryOpt, CancellationToken cancellationToken)
		{
			this._compilation = compilation;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._compilationAnalysisValueProviderFactoryOpt = compilationAnalysisValueProviderFactoryOpt;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0006F0A0 File Offset: 0x0006D2A0
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0006F0F8 File Offset: 0x0006D2F8
		public bool TryGetValue<[Nullable(2)] TValue>(SourceText text, SourceTextValueProvider<TValue> valueProvider, [MaybeNullWhen(false)] out TValue value)
		{
			return this.TryGetValue<SourceText, TValue>(text, valueProvider.CoreValueProvider, out value);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0006F108 File Offset: 0x0006D308
		public bool TryGetValue<[Nullable(2)] TValue>(SyntaxTree tree, SyntaxTreeValueProvider<TValue> valueProvider, [MaybeNullWhen(false)] out TValue value)
		{
			return this.TryGetValue<SyntaxTree, TValue>(tree, valueProvider.CoreValueProvider, out value);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0006F118 File Offset: 0x0006D318
		private bool TryGetValue<TKey, [Nullable(2)] TValue>(TKey key, AnalysisValueProvider<TKey, TValue> valueProvider, [MaybeNullWhen(false)] out TValue value) where TKey : class
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<TKey, TValue>(key, valueProvider);
			if (this._compilationAnalysisValueProviderFactoryOpt != null)
			{
				return this._compilationAnalysisValueProviderFactoryOpt.GetValueProvider<TKey, TValue>(valueProvider).TryGetValue(key, out value);
			}
			return valueProvider.TryGetValue(key, out value);
		}

		// Token: 0x04000EEF RID: 3823
		private readonly Compilation _compilation;

		// Token: 0x04000EF0 RID: 3824
		private readonly AnalyzerOptions _options;

		// Token: 0x04000EF1 RID: 3825
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000EF2 RID: 3826
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000EF3 RID: 3827
		[Nullable(2)]
		private readonly CompilationAnalysisValueProviderFactory _compilationAnalysisValueProviderFactoryOpt;

		// Token: 0x04000EF4 RID: 3828
		private readonly CancellationToken _cancellationToken;
	}
}
