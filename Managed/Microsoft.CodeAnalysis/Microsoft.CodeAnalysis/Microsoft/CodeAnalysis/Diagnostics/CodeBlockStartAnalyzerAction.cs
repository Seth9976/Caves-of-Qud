using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000338 RID: 824
	internal sealed class CodeBlockStartAnalyzerAction<TLanguageKindEnum> : AnalyzerAction where TLanguageKindEnum : struct
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x0006FF09 File Offset: 0x0006E109
		[Nullable(new byte[] { 1, 1, 0 })]
		public Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> Action
		{
			[return: Nullable(new byte[] { 1, 1, 0 })]
			get;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0006FF11 File Offset: 0x0006E111
		[NullableContext(1)]
		public CodeBlockStartAnalyzerAction([Nullable(new byte[] { 1, 1, 0 })] Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action, DiagnosticAnalyzer analyzer)
			: base(analyzer)
		{
			this.Action = action;
		}
	}
}
