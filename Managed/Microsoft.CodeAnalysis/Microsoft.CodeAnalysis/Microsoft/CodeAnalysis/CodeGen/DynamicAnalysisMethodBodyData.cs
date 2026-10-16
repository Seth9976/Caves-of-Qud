using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004C4 RID: 1220
	internal sealed class DynamicAnalysisMethodBodyData
	{
		// Token: 0x06003133 RID: 12595 RVA: 0x0008CB8D File Offset: 0x0008AD8D
		public DynamicAnalysisMethodBodyData(ImmutableArray<SourceSpan> spans)
		{
			this.Spans = spans;
		}

		// Token: 0x040012F8 RID: 4856
		public readonly ImmutableArray<SourceSpan> Spans;
	}
}
