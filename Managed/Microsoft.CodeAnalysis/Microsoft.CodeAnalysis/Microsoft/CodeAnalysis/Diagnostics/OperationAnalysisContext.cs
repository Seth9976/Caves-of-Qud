using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000327 RID: 807
	[NullableContext(1)]
	[Nullable(0)]
	public struct OperationAnalysisContext
	{
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x0006F8C4 File Offset: 0x0006DAC4
		public IOperation Operation
		{
			get
			{
				return this._operation;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x0006F8CC File Offset: 0x0006DACC
		public ISymbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x0006F8D4 File Offset: 0x0006DAD4
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0006F8DC File Offset: 0x0006DADC
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x0006F8E4 File Offset: 0x0006DAE4
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0006F8EC File Offset: 0x0006DAEC
		public OperationAnalysisContext(IOperation operation, ISymbol containingSymbol, Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._operation = operation;
			this._containingSymbol = containingSymbol;
			this._compilation = compilation;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._cancellationToken = cancellationToken;
			this._getControlFlowGraph = null;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x0006F92A File Offset: 0x0006DB2A
		internal OperationAnalysisContext(IOperation operation, ISymbol containingSymbol, Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, Func<IOperation, ControlFlowGraph> getControlFlowGraph, CancellationToken cancellationToken)
		{
			this._operation = operation;
			this._containingSymbol = containingSymbol;
			this._compilation = compilation;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._getControlFlowGraph = getControlFlowGraph;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0006F96C File Offset: 0x0006DB6C
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this._compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0006F9C4 File Offset: 0x0006DBC4
		public ControlFlowGraph GetControlFlowGraph()
		{
			return DiagnosticAnalysisContextHelpers.GetControlFlowGraph(this.Operation, this._getControlFlowGraph, this._cancellationToken);
		}

		// Token: 0x04000F31 RID: 3889
		private readonly IOperation _operation;

		// Token: 0x04000F32 RID: 3890
		private readonly ISymbol _containingSymbol;

		// Token: 0x04000F33 RID: 3891
		private readonly Compilation _compilation;

		// Token: 0x04000F34 RID: 3892
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F35 RID: 3893
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000F36 RID: 3894
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000F37 RID: 3895
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Func<IOperation, ControlFlowGraph> _getControlFlowGraph;

		// Token: 0x04000F38 RID: 3896
		private readonly CancellationToken _cancellationToken;
	}
}
