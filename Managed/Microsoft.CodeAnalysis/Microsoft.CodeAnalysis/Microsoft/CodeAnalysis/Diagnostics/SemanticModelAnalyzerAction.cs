using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000335 RID: 821
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SemanticModelAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0006FEC1 File Offset: 0x0006E0C1
		public Action<SemanticModelAnalysisContext> Action { get; }

		// Token: 0x06002398 RID: 9112 RVA: 0x0006FEC9 File Offset: 0x0006E0C9
		public SemanticModelAnalyzerAction(Action<SemanticModelAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
