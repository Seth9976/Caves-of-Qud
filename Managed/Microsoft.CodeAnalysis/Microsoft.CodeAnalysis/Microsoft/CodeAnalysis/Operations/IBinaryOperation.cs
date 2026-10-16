using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000387 RID: 903
	[NullableContext(1)]
	public interface IBinaryOperation : IOperation
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600254A RID: 9546
		BinaryOperatorKind OperatorKind { get; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600254B RID: 9547
		IOperation LeftOperand { get; }

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600254C RID: 9548
		IOperation RightOperand { get; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600254D RID: 9549
		bool IsLifted { get; }

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600254E RID: 9550
		bool IsChecked { get; }

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600254F RID: 9551
		bool IsCompareText { get; }

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002550 RID: 9552
		[Nullable(2)]
		IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get;
		}
	}
}
