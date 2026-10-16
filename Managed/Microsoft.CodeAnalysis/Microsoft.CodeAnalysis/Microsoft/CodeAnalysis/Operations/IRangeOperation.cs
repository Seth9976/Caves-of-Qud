using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003CC RID: 972
	[NullableContext(2)]
	public interface IRangeOperation : IOperation
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060025CC RID: 9676
		IOperation LeftOperand { get; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060025CD RID: 9677
		IOperation RightOperand { get; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060025CE RID: 9678
		bool IsLifted { get; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060025CF RID: 9679
		IMethodSymbol Method { get; }
	}
}
