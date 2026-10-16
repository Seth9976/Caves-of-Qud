using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D8 RID: 984
	[NullableContext(1)]
	internal interface IPointerIndirectionReferenceOperation : IOperation
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060025E8 RID: 9704
		IOperation Pointer { get; }
	}
}
