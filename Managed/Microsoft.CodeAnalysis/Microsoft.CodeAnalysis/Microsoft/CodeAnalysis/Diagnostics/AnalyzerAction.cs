using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200032B RID: 811
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class AnalyzerAction
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x0006FD96 File Offset: 0x0006DF96
		internal DiagnosticAnalyzer Analyzer { get; }

		// Token: 0x06002380 RID: 9088 RVA: 0x0006FD9E File Offset: 0x0006DF9E
		internal AnalyzerAction(DiagnosticAnalyzer analyzer)
		{
			this.Analyzer = analyzer;
		}
	}
}
