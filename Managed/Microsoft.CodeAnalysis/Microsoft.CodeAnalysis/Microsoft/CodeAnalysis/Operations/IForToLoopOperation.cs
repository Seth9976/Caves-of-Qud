using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200036D RID: 877
	[NullableContext(1)]
	public interface IForToLoopOperation : ILoopOperation, IOperation
	{
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600250F RID: 9487
		IOperation LoopControlVariable { get; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002510 RID: 9488
		IOperation InitialValue { get; }

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002511 RID: 9489
		IOperation LimitValue { get; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002512 RID: 9490
		IOperation StepValue { get; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002513 RID: 9491
		bool IsChecked { get; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002514 RID: 9492
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> NextVariables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
