using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D5 RID: 725
	[NullableContext(1)]
	public interface IFlowAnonymousFunctionOperation : IOperation
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001E31 RID: 7729
		IMethodSymbol Symbol { get; }
	}
}
