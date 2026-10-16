using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000311 RID: 785
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilationCompletedEvent : CompilationEvent
	{
		// Token: 0x06002268 RID: 8808 RVA: 0x0006D8F4 File Offset: 0x0006BAF4
		public CompilationCompletedEvent(Compilation compilation)
			: base(compilation)
		{
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x0006D8FD File Offset: 0x0006BAFD
		public override string ToString()
		{
			return "CompilationCompletedEvent";
		}
	}
}
