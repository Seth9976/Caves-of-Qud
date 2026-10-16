using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000322 RID: 802
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class OperationBlockStartAnalysisContext
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x0006F41C File Offset: 0x0006D61C
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IOperation> OperationBlocks
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationBlocks;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x0006F424 File Offset: 0x0006D624
		public ISymbol OwningSymbol
		{
			get
			{
				return this._owningSymbol;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x0006F42C File Offset: 0x0006D62C
		public Compilation Compilation
		{
			get
			{
				return this._compilation;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0006F434 File Offset: 0x0006D634
		public AnalyzerOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x0006F43C File Offset: 0x0006D63C
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x0006F444 File Offset: 0x0006D644
		protected OperationBlockStartAnalysisContext([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, ISymbol owningSymbol, Compilation compilation, AnalyzerOptions options, CancellationToken cancellationToken)
		{
			this._operationBlocks = operationBlocks;
			this._owningSymbol = owningSymbol;
			this._compilation = compilation;
			this._options = options;
			this._cancellationToken = cancellationToken;
			this._getControlFlowGraph = null;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0006F478 File Offset: 0x0006D678
		internal OperationBlockStartAnalysisContext([Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, ISymbol owningSymbol, Compilation compilation, AnalyzerOptions options, Func<IOperation, ControlFlowGraph> getControlFlowGraph, CancellationToken cancellationToken)
		{
			this._operationBlocks = operationBlocks;
			this._owningSymbol = owningSymbol;
			this._compilation = compilation;
			this._options = options;
			this._getControlFlowGraph = getControlFlowGraph;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06002339 RID: 9017
		public abstract void RegisterOperationBlockEndAction(Action<OperationBlockAnalysisContext> action);

		// Token: 0x0600233A RID: 9018 RVA: 0x0006F4AD File Offset: 0x0006D6AD
		public void RegisterOperationAction(Action<OperationAnalysisContext> action, params OperationKind[] operationKinds)
		{
			this.RegisterOperationAction(action, operationKinds.AsImmutableOrEmpty<OperationKind>());
		}

		// Token: 0x0600233B RID: 9019
		[NullableContext(0)]
		public abstract void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds);

		// Token: 0x0600233C RID: 9020 RVA: 0x0006F4BC File Offset: 0x0006D6BC
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

		// Token: 0x04000F10 RID: 3856
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<IOperation> _operationBlocks;

		// Token: 0x04000F11 RID: 3857
		private readonly ISymbol _owningSymbol;

		// Token: 0x04000F12 RID: 3858
		private readonly Compilation _compilation;

		// Token: 0x04000F13 RID: 3859
		private readonly AnalyzerOptions _options;

		// Token: 0x04000F14 RID: 3860
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly Func<IOperation, ControlFlowGraph> _getControlFlowGraph;

		// Token: 0x04000F15 RID: 3861
		private readonly CancellationToken _cancellationToken;
	}
}
