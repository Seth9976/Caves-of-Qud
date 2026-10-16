using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003CA RID: 970
	[NullableContext(1)]
	public interface IDiscardOperation : IOperation
	{
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060025CB RID: 9675
		IDiscardSymbol DiscardSymbol { get; }
	}
}
