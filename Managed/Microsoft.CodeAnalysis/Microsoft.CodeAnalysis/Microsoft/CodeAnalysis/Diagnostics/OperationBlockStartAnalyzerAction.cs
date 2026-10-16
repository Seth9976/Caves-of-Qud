using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000330 RID: 816
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OperationBlockStartAnalyzerAction : AnalyzerAction
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0006FE3A File Offset: 0x0006E03A
		public Action<OperationBlockStartAnalysisContext> Action { get; }

		// Token: 0x0600238D RID: 9101 RVA: 0x0006FE42 File Offset: 0x0006E042
		public OperationBlockStartAnalyzerAction(Action<OperationBlockStartAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
