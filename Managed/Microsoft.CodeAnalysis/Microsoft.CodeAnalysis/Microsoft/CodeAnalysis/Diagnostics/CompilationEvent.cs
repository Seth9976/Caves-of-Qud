using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000312 RID: 786
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CompilationEvent
	{
		// Token: 0x0600226A RID: 8810 RVA: 0x0006D904 File Offset: 0x0006BB04
		internal CompilationEvent(Compilation compilation)
		{
			this.Compilation = compilation;
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0006D913 File Offset: 0x0006BB13
		public Compilation Compilation { get; }
	}
}
