using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000394 RID: 916
	[NullableContext(1)]
	public interface IParenthesizedOperation : IOperation
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600256E RID: 9582
		IOperation Operand { get; }
	}
}
