using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200034B RID: 843
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SourceTextValueProvider<[Nullable(2)] TValue>
	{
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x000712FD File Offset: 0x0006F4FD
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x00071305 File Offset: 0x0006F505
		internal AnalysisValueProvider<SourceText, TValue> CoreValueProvider { get; private set; }

		// Token: 0x0600244E RID: 9294 RVA: 0x0007130E File Offset: 0x0006F50E
		public SourceTextValueProvider(Func<SourceText, TValue> computeValue, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<SourceText> sourceTextComparer = null)
		{
			this.CoreValueProvider = new AnalysisValueProvider<SourceText, TValue>(computeValue, sourceTextComparer ?? SourceTextComparer.Instance);
		}
	}
}
