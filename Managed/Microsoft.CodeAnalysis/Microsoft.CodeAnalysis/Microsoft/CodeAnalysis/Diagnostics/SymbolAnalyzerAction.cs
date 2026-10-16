using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200032C RID: 812
	internal sealed class SymbolAnalyzerAction : AnalyzerAction
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x0006FDAD File Offset: 0x0006DFAD
		[Nullable(1)]
		public Action<SymbolAnalysisContext> Action
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x0006FDB5 File Offset: 0x0006DFB5
		public ImmutableArray<SymbolKind> Kinds { get; }

		// Token: 0x06002383 RID: 9091 RVA: 0x0006FDBD File Offset: 0x0006DFBD
		[NullableContext(1)]
		public SymbolAnalyzerAction(Action<SymbolAnalysisContext> action, [Nullable(0)] ImmutableArray<SymbolKind> kinds, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
			this.Kinds = kinds;
		}
	}
}
