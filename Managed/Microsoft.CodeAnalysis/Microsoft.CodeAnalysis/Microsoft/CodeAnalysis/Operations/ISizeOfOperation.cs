using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A7 RID: 935
	[NullableContext(1)]
	public interface ISizeOfOperation : IOperation
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600258C RID: 9612
		ITypeSymbol TypeOperand { get; }
	}
}
