using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000339 RID: 825
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CodeBlockAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x0006FF21 File Offset: 0x0006E121
		public Action<CodeBlockAnalysisContext> Action { get; }

		// Token: 0x060023A0 RID: 9120 RVA: 0x0006FF29 File Offset: 0x0006E129
		public CodeBlockAnalyzerAction(Action<CodeBlockAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
