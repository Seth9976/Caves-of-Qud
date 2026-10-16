using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000396 RID: 918
	[NullableContext(1)]
	public interface IConditionalAccessOperation : IOperation
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002572 RID: 9586
		IOperation Operation { get; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002573 RID: 9587
		IOperation WhenNotNull { get; }
	}
}
