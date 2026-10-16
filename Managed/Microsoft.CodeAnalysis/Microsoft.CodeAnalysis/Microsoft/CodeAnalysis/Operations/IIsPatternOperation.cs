using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A9 RID: 937
	[NullableContext(1)]
	public interface IIsPatternOperation : IOperation
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600258E RID: 9614
		IOperation Value { get; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600258F RID: 9615
		IPatternOperation Pattern { get; }
	}
}
