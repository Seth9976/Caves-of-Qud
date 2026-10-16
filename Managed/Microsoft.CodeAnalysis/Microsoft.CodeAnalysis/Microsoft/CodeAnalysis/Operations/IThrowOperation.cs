using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003AB RID: 939
	[NullableContext(2)]
	public interface IThrowOperation : IOperation
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002595 RID: 9621
		IOperation Exception { get; }
	}
}
