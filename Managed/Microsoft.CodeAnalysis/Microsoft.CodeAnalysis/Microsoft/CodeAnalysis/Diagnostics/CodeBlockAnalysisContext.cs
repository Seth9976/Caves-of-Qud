using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000321 RID: 801
	[NullableContext(1)]
	[Nullable(0)]
	public struct CodeBlockAnalysisContext
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x0006F35B File Offset: 0x0006D55B
		public SyntaxNode CodeBlock
		{
			get
			{
				return this._codeBlock;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x0006F363 File Offset: 0x0006D563
		public ISymbol OwningSymbol
		{
			get
			{
				return this._owningSymbol;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x0006F36B File Offset: 0x0006D56B
		public SemanticModel SemanticModel
		{
			get
			{
				return this._semanticModel;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x0006F373 File Offset: 0x0006D573
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0006F37B File Offset: 0x0006D57B
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0006F383 File Offset: 0x0006D583
		public CodeBlockAnalysisContext(SyntaxNode codeBlock, ISymbol owningSymbol, SemanticModel semanticModel, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._codeBlock = codeBlock;
			this._owningSymbol = owningSymbol;
			this._semanticModel = semanticModel;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0006F3BC File Offset: 0x0006D5BC
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._semanticModel.Compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x04000F09 RID: 3849
		private readonly SyntaxNode _codeBlock;

		// Token: 0x04000F0A RID: 3850
		private readonly ISymbol _owningSymbol;

		// Token: 0x04000F0B RID: 3851
		private readonly SemanticModel _semanticModel;

		// Token: 0x04000F0C RID: 3852
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F0D RID: 3853
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000F0E RID: 3854
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000F0F RID: 3855
		private readonly CancellationToken _cancellationToken;
	}
}
