using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000386 RID: 902
	[NullableContext(1)]
	public interface IUnaryOperation : IOperation
	{
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002545 RID: 9541
		UnaryOperatorKind OperatorKind { get; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002546 RID: 9542
		IOperation Operand { get; }

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002547 RID: 9543
		bool IsLifted { get; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002548 RID: 9544
		bool IsChecked { get; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002549 RID: 9545
		[Nullable(2)]
		IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get;
		}
	}
}
