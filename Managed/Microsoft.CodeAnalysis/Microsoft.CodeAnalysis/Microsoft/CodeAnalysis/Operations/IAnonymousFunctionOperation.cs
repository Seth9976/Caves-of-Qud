using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200038A RID: 906
	[NullableContext(1)]
	public interface IAnonymousFunctionOperation : IOperation
	{
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002558 RID: 9560
		IMethodSymbol Symbol { get; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002559 RID: 9561
		IBlockOperation Body { get; }
	}
}
