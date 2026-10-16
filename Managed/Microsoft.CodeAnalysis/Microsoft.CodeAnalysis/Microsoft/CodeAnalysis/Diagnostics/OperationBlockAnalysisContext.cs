using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000323 RID: 803
	[NullableContext(1)]
	[Nullable(0)]
	public struct OperationBlockAnalysisContext
	{
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0006F50A File Offset: 0x0006D70A
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> OperationBlocks
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationBlocks;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x0006F512 File Offset: 0x0006D712
		public ISymbol OwningSymbol
		{
			get
			{
				return this._owningSymbol;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x0006F51A File Offset: 0x0006D71A
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0006F522 File Offset: 0x0006D722
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x0006F52A File Offset: 0x0006D72A
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x0006F532 File Offset: 0x0006D732
		public OperationBlockAnalysisContext([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, ISymbol owningSymbol, Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, CancellationToken cancellationToken)
		{
			this._operationBlocks = operationBlocks;
			this._owningSymbol = owningSymbol;
			this._compilation = compilation;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._cancellationToken = cancellationToken;
			this._getControlFlowGraph = null;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0006F570 File Offset: 0x0006D770
		internal OperationBlockAnalysisContext([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, ISymbol owningSymbol, Compilation compilation, AnalyzerOptions options, Action<Diagnostic> reportDiagnostic, Func<Diagnostic, bool> isSupportedDiagnostic, Func<IOperation, ControlFlowGraph> getControlFlowGraph, CancellationToken cancellationToken)
		{
			this._operationBlocks = operationBlocks;
			this._owningSymbol = owningSymbol;
			this._compilation = compilation;
			this._options = options;
			this._reportDiagnostic = reportDiagnostic;
			this._isSupportedDiagnostic = isSupportedDiagnostic;
			this._getControlFlowGraph = getControlFlowGraph;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0006F5B0 File Offset: 0x0006D7B0
		public void ReportDiagnostic(Diagnostic diagnostic)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments(diagnostic, this.Compilation, this._isSupportedDiagnostic);
			Action<Diagnostic> reportDiagnostic = this._reportDiagnostic;
			lock (reportDiagnostic)
			{
				this._reportDiagnostic.Invoke(diagnostic);
			}
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0006F608 File Offset: 0x0006D808
		public ControlFlowGraph GetControlFlowGraph(IOperation operationBlock)
		{
			if (operationBlock == null)
			{
				throw new ArgumentNullException("operationBlock");
			}
			if (!this.OperationBlocks.Contains(operationBlock))
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidOperationBlockForAnalysisContext, "operationBlock");
			}
			return DiagnosticAnalysisContextHelpers.GetControlFlowGraph(operationBlock, this._getControlFlowGraph, this._cancellationToken);
		}

		// Token: 0x04000F16 RID: 3862
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<IOperation> _operationBlocks;

		// Token: 0x04000F17 RID: 3863
		private readonly ISymbol _owningSymbol;

		// Token: 0x04000F18 RID: 3864
		private readonly Compilation _compilation;

		// Token: 0x04000F19 RID: 3865
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F1A RID: 3866
		private readonly Action<Diagnostic> _reportDiagnostic;

		// Token: 0x04000F1B RID: 3867
		private readonly Func<Diagnostic, bool> _isSupportedDiagnostic;

		// Token: 0x04000F1C RID: 3868
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Func<IOperation, ControlFlowGraph> _getControlFlowGraph;

		// Token: 0x04000F1D RID: 3869
		private readonly CancellationToken _cancellationToken;
	}
}
