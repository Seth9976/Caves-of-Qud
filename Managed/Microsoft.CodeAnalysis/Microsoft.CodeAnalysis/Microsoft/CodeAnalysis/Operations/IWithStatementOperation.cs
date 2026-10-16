using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D9 RID: 985
	[NullableContext(1)]
	internal interface IWithStatementOperation : IOperation
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060025E9 RID: 9705
		IOperation Body { get; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060025EA RID: 9706
		IOperation Value { get; }
	}
}
