using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000302 RID: 770
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AnalyzerConfigOptionsProvider
	{
		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002122 RID: 8482
		public abstract AnalyzerConfigOptions GlobalOptions { get; }

		// Token: 0x06002123 RID: 8483
		public abstract AnalyzerConfigOptions GetOptions(SyntaxTree tree);

		// Token: 0x06002124 RID: 8484
		public abstract AnalyzerConfigOptions GetOptions(AdditionalText textFile);
	}
}
