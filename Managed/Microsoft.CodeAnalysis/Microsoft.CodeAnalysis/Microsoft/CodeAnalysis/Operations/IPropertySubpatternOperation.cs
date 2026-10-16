using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D3 RID: 979
	[NullableContext(1)]
	public interface IPropertySubpatternOperation : IOperation
	{
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060025DF RID: 9695
		IOperation Member { get; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060025E0 RID: 9696
		IPatternOperation Pattern { get; }
	}
}
