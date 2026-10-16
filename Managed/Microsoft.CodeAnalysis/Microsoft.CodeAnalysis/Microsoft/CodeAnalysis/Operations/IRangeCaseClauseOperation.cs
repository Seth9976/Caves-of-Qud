using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003BD RID: 957
	[NullableContext(1)]
	public interface IRangeCaseClauseOperation : ICaseClauseOperation, IOperation
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060025B5 RID: 9653
		IOperation MinimumValue { get; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060025B6 RID: 9654
		IOperation MaximumValue { get; }
	}
}
