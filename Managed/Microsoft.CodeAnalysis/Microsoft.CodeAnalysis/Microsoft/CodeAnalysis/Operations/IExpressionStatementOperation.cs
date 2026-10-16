using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000376 RID: 886
	[NullableContext(1)]
	public interface IExpressionStatementOperation : IOperation
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002528 RID: 9512
		IOperation Operation { get; }
	}
}
