using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200036E RID: 878
	[NullableContext(2)]
	public interface IWhileLoopOperation : ILoopOperation, IOperation
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002515 RID: 9493
		IOperation Condition { get; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002516 RID: 9494
		bool ConditionIsTop { get; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002517 RID: 9495
		bool ConditionIsUntil { get; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002518 RID: 9496
		IOperation IgnoredCondition { get; }
	}
}
