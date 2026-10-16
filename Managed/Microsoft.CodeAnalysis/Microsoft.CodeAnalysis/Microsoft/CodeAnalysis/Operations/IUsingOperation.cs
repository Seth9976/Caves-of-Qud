using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000375 RID: 885
	[NullableContext(1)]
	public interface IUsingOperation : IOperation
	{
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002524 RID: 9508
		IOperation Resources { get; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002525 RID: 9509
		IOperation Body { get; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002526 RID: 9510
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002527 RID: 9511
		bool IsAsynchronous { get; }
	}
}
