using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000324 RID: 804
	[NullableContext(1)]
	[Nullable(0)]
	public struct SyntaxTreeAnalysisContext
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x0006F656 File Offset: 0x0006D856
		public SyntaxTree Tree
		{
			get
			{
				return this._tree;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0006F65E File Offset: 0x0006D85E
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x0006F666 File Offset: 0x0006D866
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0006F66E File Offset: 0x0006D86E
		[Nullable(2)]
		internal Compilation Compilation
		{
			[NullableContext(2)]
			get
			{
				return this._compilationOpt;
			}
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0006F676 File Offset: 0x0006D876
		public SyntaxTreeAnalysisContext(SyntaxTree tree, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._tree = tree;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._compilationOpt = null;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0006F6A4 File Offset: 0x0006D8A4
		internal SyntaxTreeAnalysisContext(SyntaxTree tree, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, Compilation compilation, CancellationToken cancellationToken)
		{
			this._tree = tree;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._compilationOpt = compilation;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0006F6D4 File Offset: 0x0006D8D4
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._compilationOpt, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x04000F1E RID: 3870
		private readonly SyntaxTree _tree;

		// Token: 0x04000F1F RID: 3871
		[Nullable(2)]
		private readonly Compilation _compilationOpt;

		// Token: 0x04000F20 RID: 3872
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F21 RID: 3873
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000F22 RID: 3874
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000F23 RID: 3875
		private readonly CancellationToken _cancellationToken;
	}
}
