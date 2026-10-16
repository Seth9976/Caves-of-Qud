using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000326 RID: 806
	[NullableContext(1)]
	[Nullable(0)]
	public struct SyntaxNodeAnalysisContext
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x0006F7D4 File Offset: 0x0006D9D4
		public SyntaxNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x0006F7DC File Offset: 0x0006D9DC
		[Nullable(2)]
		public ISymbol ContainingSymbol
		{
			[NullableContext(2)]
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x0006F7E4 File Offset: 0x0006D9E4
		public SemanticModel SemanticModel
		{
			get
			{
				return this._semanticModel;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x0006F7EC File Offset: 0x0006D9EC
		public Compilation Compilation
		{
			get
			{
				SemanticModel semanticModel = this._semanticModel;
				Compilation compilation = ((semanticModel != null) ? semanticModel.Compilation : null);
				if (compilation == null)
				{
					throw new InvalidOperationException();
				}
				return compilation;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x0006F80A File Offset: 0x0006DA0A
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x0006F812 File Offset: 0x0006DA12
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0006F81A File Offset: 0x0006DA1A
		public SyntaxNodeAnalysisContext(SyntaxNode node, [Nullable(2)] ISymbol containingSymbol, SemanticModel semanticModel, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._node = node;
			this._containingSymbol = containingSymbol;
			this._semanticModel = semanticModel;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0006F851 File Offset: 0x0006DA51
		public SyntaxNodeAnalysisContext(SyntaxNode node, SemanticModel semanticModel, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this = new SyntaxNodeAnalysisContext(node, null, semanticModel, options, reportDiagnostic, isSupportedDiagnostic, cancellationToken);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0006F864 File Offset: 0x0006DA64
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._semanticModel.Compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x04000F2A RID: 3882
		private readonly SyntaxNode _node;

		// Token: 0x04000F2B RID: 3883
		[Nullable(2)]
		private readonly ISymbol _containingSymbol;

		// Token: 0x04000F2C RID: 3884
		private readonly SemanticModel _semanticModel;

		// Token: 0x04000F2D RID: 3885
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F2E RID: 3886
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000F2F RID: 3887
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000F30 RID: 3888
		private readonly CancellationToken _cancellationToken;
	}
}
