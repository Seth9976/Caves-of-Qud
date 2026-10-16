using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200032D RID: 813
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SymbolStartAnalyzerAction : AnalyzerAction
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x0006FDD4 File Offset: 0x0006DFD4
		public Action<SymbolStartAnalysisContext> Action { get; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x0006FDDC File Offset: 0x0006DFDC
		public SymbolKind Kind { get; }

		// Token: 0x06002386 RID: 9094 RVA: 0x0006FDE4 File Offset: 0x0006DFE4
		public SymbolStartAnalyzerAction(Action<SymbolStartAnalysisContext> action, SymbolKind kind, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
			this.Kind = kind;
		}
	}
}
