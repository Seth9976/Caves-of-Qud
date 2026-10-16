using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000325 RID: 805
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct AdditionalFileAnalysisContext
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x0006F72C File Offset: 0x0006D92C
		public AdditionalText AdditionalFile { get; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x0006F734 File Offset: 0x0006D934
		public AnalyzerOptions Options { get; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x0006F73C File Offset: 0x0006D93C
		public CancellationToken CancellationToken { get; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x0006F744 File Offset: 0x0006D944
		public Compilation Compilation { get; }

		// Token: 0x06002351 RID: 9041 RVA: 0x0006F74C File Offset: 0x0006D94C
		internal AdditionalFileAnalysisContext(AdditionalText additionalFile, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, Compilation compilation, CancellationToken cancellationToken)
		{
			this.AdditionalFile = additionalFile;
			this.Options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this.Compilation = compilation;
			this.CancellationToken = cancellationToken;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0006F77C File Offset: 0x0006D97C
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this.Compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x04000F24 RID: 3876
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000F25 RID: 3877
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;
	}
}
