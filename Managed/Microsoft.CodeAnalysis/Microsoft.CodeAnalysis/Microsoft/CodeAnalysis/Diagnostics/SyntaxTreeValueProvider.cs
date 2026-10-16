using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000350 RID: 848
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SyntaxTreeValueProvider<[Nullable(2)] TValue>
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x00071BE9 File Offset: 0x0006FDE9
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x00071BF1 File Offset: 0x0006FDF1
		internal AnalysisValueProvider<SyntaxTree, TValue> CoreValueProvider { get; private set; }

		// Token: 0x06002475 RID: 9333 RVA: 0x00071BFA File Offset: 0x0006FDFA
		public SyntaxTreeValueProvider(Func<SyntaxTree, TValue> computeValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<SyntaxTree> syntaxTreeComparer = null)
		{
			this.CoreValueProvider = new AnalysisValueProvider<SyntaxTree, TValue>(computeValue, syntaxTreeComparer ?? SyntaxTreeComparer.Instance);
		}
	}
}
