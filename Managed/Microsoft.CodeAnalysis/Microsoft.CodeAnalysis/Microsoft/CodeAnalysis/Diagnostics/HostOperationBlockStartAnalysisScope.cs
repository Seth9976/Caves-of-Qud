using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000346 RID: 838
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class HostOperationBlockStartAnalysisScope
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000705ED File Offset: 0x0006E7ED
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<OperationBlockAnalyzerAction> OperationBlockEndActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationBlockEndActions;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060023EA RID: 9194 RVA: 0x000705F5 File Offset: 0x0006E7F5
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<OperationAnalyzerAction> OperationActions
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._operationActions;
			}
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000705FD File Offset: 0x0006E7FD
		internal HostOperationBlockStartAnalysisScope()
		{
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0007061B File Offset: 0x0006E81B
		public void RegisterOperationBlockEndAction(DiagnosticAnalyzer analyzer, Action<OperationBlockAnalysisContext> action)
		{
			this._operationBlockEndActions = this._operationBlockEndActions.Add(new OperationBlockAnalyzerAction(action, analyzer));
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x00070635 File Offset: 0x0006E835
		public void RegisterOperationAction(DiagnosticAnalyzer analyzer, Action<OperationAnalysisContext> action, [Nullable(0)] ImmutableArray<OperationKind> operationKinds)
		{
			this._operationActions = this._operationActions.Add(new OperationAnalyzerAction(action, operationKinds, analyzer));
		}

		// Token: 0x04000F64 RID: 3940
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<OperationBlockAnalyzerAction> _operationBlockEndActions = ImmutableArray<OperationBlockAnalyzerAction>.Empty;

		// Token: 0x04000F65 RID: 3941
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<OperationAnalyzerAction> _operationActions = ImmutableArray<OperationAnalyzerAction>.Empty;
	}
}
