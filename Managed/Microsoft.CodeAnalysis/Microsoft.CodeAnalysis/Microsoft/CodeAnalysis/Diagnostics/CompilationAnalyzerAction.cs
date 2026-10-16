using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000334 RID: 820
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilationAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x0006FEA9 File Offset: 0x0006E0A9
		public Action<CompilationAnalysisContext> Action { get; }

		// Token: 0x06002396 RID: 9110 RVA: 0x0006FEB1 File Offset: 0x0006E0B1
		public CompilationAnalyzerAction(Action<CompilationAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
