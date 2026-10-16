using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000333 RID: 819
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilationStartAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0006FE91 File Offset: 0x0006E091
		public Action<CompilationStartAnalysisContext> Action { get; }

		// Token: 0x06002394 RID: 9108 RVA: 0x0006FE99 File Offset: 0x0006E099
		public CompilationStartAnalyzerAction(Action<CompilationStartAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
