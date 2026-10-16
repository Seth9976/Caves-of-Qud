using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C6 RID: 966
	[NullableContext(1)]
	public interface ITupleBinaryOperation : IOperation
	{
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060025C4 RID: 9668
		BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060025C5 RID: 9669
		IOperation LeftOperand { get; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060025C6 RID: 9670
		IOperation RightOperand { get; }
	}
}
