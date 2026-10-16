using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000393 RID: 915
	public interface ICompoundAssignmentOperation : IAssignmentOperation, IOperation
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002568 RID: 9576
		CommonConversion InConversion { get; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002569 RID: 9577
		CommonConversion OutConversion { get; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600256A RID: 9578
		BinaryOperatorKind OperatorKind { get; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600256B RID: 9579
		bool IsLifted { get; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600256C RID: 9580
		bool IsChecked { get; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600256D RID: 9581
		[Nullable(2)]
		IMethodSymbol OperatorMethod
		{
			[NullableContext(2)]
			get;
		}
	}
}
