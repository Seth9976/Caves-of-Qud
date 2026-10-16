using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000373 RID: 883
	[NullableContext(1)]
	public interface ILockOperation : IOperation
	{
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600251E RID: 9502
		IOperation LockedValue { get; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600251F RID: 9503
		IOperation Body { get; }
	}
}
