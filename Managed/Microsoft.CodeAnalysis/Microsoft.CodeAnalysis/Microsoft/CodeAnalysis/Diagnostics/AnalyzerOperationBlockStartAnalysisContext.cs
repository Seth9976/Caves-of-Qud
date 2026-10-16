using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000341 RID: 833
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AnalyzerOperationBlockStartAnalysisContext : OperationBlockStartAnalysisContext
	{
		// Token: 0x060023D8 RID: 9176 RVA: 0x00070405 File Offset: 0x0006E605
		internal AnalyzerOperationBlockStartAnalysisContext(DiagnosticAnalyzer analyzer, HostOperationBlockStartAnalysisScope scope, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> operationBlocks, ISymbol owningSymbol, Compilation compilation, AnalyzerOptions options, Func<IOperation, ControlFlowGraph> getControlFlowGraph, CancellationToken cancellationToken)
			: base(operationBlocks, owningSymbol, compilation, options, getControlFlowGraph, cancellationToken)
		{
			this._analyzer = analyzer;
			this._scope = scope;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00070426 File Offset: 0x0006E626
		public override void RegisterOperationBlockEndAction(Action<OperationBlockAnalysisContext> action)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationBlockAnalysisContext>(action);
			this._scope.RegisterOperationBlockEndAction(this._analyzer, action);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00070440 File Offset: 0x0006E640
		[NullableContext(0)]
		public override void RegisterOperationAction([Nullable(1)] Action<OperationAnalysisContext> action, ImmutableArray<OperationKind> operationKinds)
		{
			DiagnosticAnalysisContextHelpers.VerifyArguments<OperationAnalysisContext>(action, operationKinds);
			this._scope.RegisterOperationAction(this._analyzer, action, operationKinds);
		}

		// Token: 0x04000F5D RID: 3933
		private readonly DiagnosticAnalyzer _analyzer;

		// Token: 0x04000F5E RID: 3934
		private readonly HostOperationBlockStartAnalysisScope _scope;
	}
}
