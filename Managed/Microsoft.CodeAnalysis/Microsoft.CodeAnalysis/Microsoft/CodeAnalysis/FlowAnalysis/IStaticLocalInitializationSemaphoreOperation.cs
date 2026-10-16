using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D4 RID: 724
	[NullableContext(1)]
	public interface IStaticLocalInitializationSemaphoreOperation : IOperation
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001E30 RID: 7728
		ILocalSymbol Local { get; }
	}
}
