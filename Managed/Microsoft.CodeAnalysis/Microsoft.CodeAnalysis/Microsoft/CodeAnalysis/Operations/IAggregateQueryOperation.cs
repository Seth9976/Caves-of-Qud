using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D4 RID: 980
	[NullableContext(1)]
	internal interface IAggregateQueryOperation : IOperation
	{
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060025E1 RID: 9697
		IOperation Group { get; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060025E2 RID: 9698
		IOperation Aggregation { get; }
	}
}
