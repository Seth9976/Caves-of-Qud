using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000395 RID: 917
	[NullableContext(1)]
	public interface IEventAssignmentOperation : IOperation
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600256F RID: 9583
		IOperation EventReference { get; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002570 RID: 9584
		IOperation HandlerValue { get; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002571 RID: 9585
		bool Adds { get; }
	}
}
