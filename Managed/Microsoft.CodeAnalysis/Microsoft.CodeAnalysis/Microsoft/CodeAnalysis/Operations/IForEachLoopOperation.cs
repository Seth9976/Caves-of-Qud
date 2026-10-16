using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200036B RID: 875
	[NullableContext(1)]
	public interface IForEachLoopOperation : ILoopOperation, IOperation
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002507 RID: 9479
		IOperation LoopControlVariable { get; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002508 RID: 9480
		IOperation Collection { get; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002509 RID: 9481
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> NextVariables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600250A RID: 9482
		bool IsAsynchronous { get; }
	}
}
