using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200031D RID: 797
	[NullableContext(1)]
	[Nullable(0)]
	public struct SemanticModelAnalysisContext
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0006F145 File Offset: 0x0006D345
		public SemanticModel SemanticModel
		{
			get
			{
				return this._semanticModel;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0006F14D File Offset: 0x0006D34D
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x0006F155 File Offset: 0x0006D355
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0006F15D File Offset: 0x0006D35D
		public SemanticModelAnalysisContext(SemanticModel semanticModel, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._semanticModel = semanticModel;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0006F184 File Offset: 0x0006D384
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._semanticModel.Compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x04000EF5 RID: 3829
		private readonly SemanticModel _semanticModel;

		// Token: 0x04000EF6 RID: 3830
		private readonly AnalyzerOptions _options;

		// Token: 0x04000EF7 RID: 3831
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000EF8 RID: 3832
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000EF9 RID: 3833
		private readonly CancellationToken _cancellationToken;
	}
}
