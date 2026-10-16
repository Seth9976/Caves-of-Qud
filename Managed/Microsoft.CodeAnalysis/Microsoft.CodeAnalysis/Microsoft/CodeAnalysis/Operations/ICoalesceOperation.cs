using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000389 RID: 905
	[NullableContext(1)]
	public interface ICoalesceOperation : IOperation
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002555 RID: 9557
		IOperation Value { get; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002556 RID: 9558
		IOperation WhenNull { get; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002557 RID: 9559
		CommonConversion ValueConversion { get; }
	}
}
