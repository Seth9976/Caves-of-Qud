using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200032F RID: 815
	internal sealed class SyntaxNodeAnalyzerAction<TLanguageKindEnum> : AnalyzerAction where TLanguageKindEnum : struct
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0006FE13 File Offset: 0x0006E013
		[Nullable(1)]
		public Action<SyntaxNodeAnalysisContext> Action
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x0006FE1B File Offset: 0x0006E01B
		public ImmutableArray<TLanguageKindEnum> Kinds { get; }

		// Token: 0x0600238B RID: 9099 RVA: 0x0006FE23 File Offset: 0x0006E023
		[NullableContext(1)]
		public SyntaxNodeAnalyzerAction(Action<SyntaxNodeAnalysisContext> action, [Nullable(0)] ImmutableArray<TLanguageKindEnum> kinds, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
			this.Kinds = kinds;
		}
	}
}
