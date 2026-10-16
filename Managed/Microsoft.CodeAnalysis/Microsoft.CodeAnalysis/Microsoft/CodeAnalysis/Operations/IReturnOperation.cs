using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000372 RID: 882
	[NullableContext(2)]
	public interface IReturnOperation : IOperation
	{
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600251D RID: 9501
		IOperation ReturnedValue { get; }
	}
}
