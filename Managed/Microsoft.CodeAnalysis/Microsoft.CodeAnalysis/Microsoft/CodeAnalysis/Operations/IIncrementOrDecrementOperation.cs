using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003AA RID: 938
	[NullableContext(1)]
	public interface IIncrementOrDecrementOperation : IOperation
	{
		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002590 RID: 9616
		bool IsPostfix { get; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002591 RID: 9617
		bool IsLifted { get; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002592 RID: 9618
		bool IsChecked { get; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002593 RID: 9619
		IOperation Target { get; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002594 RID: 9620
		[Nullable(2)]
		IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get;
		}
	}
}
