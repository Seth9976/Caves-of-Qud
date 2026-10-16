using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000390 RID: 912
	[NullableContext(1)]
	public interface IAwaitOperation : IOperation
	{
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002564 RID: 9572
		IOperation Operation { get; }
	}
}
