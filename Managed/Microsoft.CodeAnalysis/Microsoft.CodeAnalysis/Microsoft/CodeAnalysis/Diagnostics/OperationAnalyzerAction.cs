using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000332 RID: 818
	internal sealed class OperationAnalyzerAction : AnalyzerAction
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0006FE6A File Offset: 0x0006E06A
		[Nullable(1)]
		public Action<OperationAnalysisContext> Action
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0006FE72 File Offset: 0x0006E072
		public ImmutableArray<OperationKind> Kinds { get; }

		// Token: 0x06002392 RID: 9106 RVA: 0x0006FE7A File Offset: 0x0006E07A
		[NullableContext(1)]
		public OperationAnalyzerAction(Action<OperationAnalysisContext> action, [Nullable(0)] ImmutableArray<OperationKind> kinds, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
			this.Kinds = kinds;
		}
	}
}
