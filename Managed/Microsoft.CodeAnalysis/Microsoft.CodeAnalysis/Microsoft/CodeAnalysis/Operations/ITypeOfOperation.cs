using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A6 RID: 934
	[NullableContext(1)]
	public interface ITypeOfOperation : IOperation
	{
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600258B RID: 9611
		ITypeSymbol TypeOperand { get; }
	}
}
