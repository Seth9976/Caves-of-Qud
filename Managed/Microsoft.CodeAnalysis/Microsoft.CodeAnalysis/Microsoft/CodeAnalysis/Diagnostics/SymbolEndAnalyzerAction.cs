using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200032E RID: 814
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SymbolEndAnalyzerAction : AnalyzerAction
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x0006FDFB File Offset: 0x0006DFFB
		public Action<SymbolAnalysisContext> Action { get; }

		// Token: 0x06002388 RID: 9096 RVA: 0x0006FE03 File Offset: 0x0006E003
		public SymbolEndAnalyzerAction(Action<SymbolAnalysisContext> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
