using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000388 RID: 904
	[NullableContext(1)]
	public interface IConditionalOperation : IOperation
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002551 RID: 9553
		IOperation Condition { get; }

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002552 RID: 9554
		IOperation WhenTrue { get; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002553 RID: 9555
		[Nullable(2)]
		IOperation WhenFalse
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002554 RID: 9556
		bool IsRef { get; }
	}
}
