using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000331 RID: 817
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class OperationBlockAnalyzerAction : AnalyzerAction
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x0006FE52 File Offset: 0x0006E052
		public Action<OperationBlockAnalysisContext> Action { get; }

		// Token: 0x0600238F RID: 9103 RVA: 0x0006FE5A File Offset: 0x0006E05A
		public OperationBlockAnalyzerAction(Action<OperationBlockAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
