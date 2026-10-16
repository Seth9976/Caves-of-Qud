using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000336 RID: 822
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SyntaxTreeAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0006FED9 File Offset: 0x0006E0D9
		public Action<SyntaxTreeAnalysisContext> Action { get; }

		// Token: 0x0600239A RID: 9114 RVA: 0x0006FEE1 File Offset: 0x0006E0E1
		public SyntaxTreeAnalyzerAction(Action<SyntaxTreeAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
