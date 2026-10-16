using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003DC RID: 988
	[NullableContext(1)]
	public interface IBinaryPatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060025EE RID: 9710
		BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060025EF RID: 9711
		IPatternOperation LeftPattern { get; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060025F0 RID: 9712
		IPatternOperation RightPattern { get; }
	}
}
