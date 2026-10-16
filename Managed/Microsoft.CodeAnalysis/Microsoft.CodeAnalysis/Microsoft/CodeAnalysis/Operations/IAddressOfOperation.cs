using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A8 RID: 936
	[NullableContext(1)]
	public interface IAddressOfOperation : IOperation
	{
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600258D RID: 9613
		IOperation Reference { get; }
	}
}
