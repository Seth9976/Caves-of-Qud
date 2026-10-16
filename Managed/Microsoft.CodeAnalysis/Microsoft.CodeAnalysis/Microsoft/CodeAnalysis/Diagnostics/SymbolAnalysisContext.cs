using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200031E RID: 798
	[NullableContext(1)]
	[Nullable(0)]
	public struct SymbolAnalysisContext
	{
		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x0006F1E4 File Offset: 0x0006D3E4
		public ISymbol Symbol
		{
			get
			{
				return this._symbol;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x0006F1EC File Offset: 0x0006D3EC
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x0006F1F4 File Offset: 0x0006D3F4
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x0006F1FC File Offset: 0x0006D3FC
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x0006F204 File Offset: 0x0006D404
		internal Func<Diagnostic, bool> IsSupportedDiagnostic
		{
			get
			{
				return this._isSupportedDiagnostic;
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0006F20C File Offset: 0x0006D40C
		public SymbolAnalysisContext(ISymbol symbol, Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._symbol = symbol;
			this._compilation = compilation;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0006F23C File Offset: 0x0006D43C
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x04000EFA RID: 3834
		private readonly ISymbol _symbol;

		// Token: 0x04000EFB RID: 3835
		private readonly Compilation _compilation;

		// Token: 0x04000EFC RID: 3836
		private readonly AnalyzerOptions _options;

		// Token: 0x04000EFD RID: 3837
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000EFE RID: 3838
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000EFF RID: 3839
		private readonly CancellationToken _cancellationToken;
	}
}
