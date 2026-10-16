using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000337 RID: 823
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AdditionalFileAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x0006FEF1 File Offset: 0x0006E0F1
		public Action<AdditionalFileAnalysisContext> Action { get; }

		// Token: 0x0600239C RID: 9116 RVA: 0x0006FEF9 File Offset: 0x0006E0F9
		public AdditionalFileAnalyzerAction(Action<AdditionalFileAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
