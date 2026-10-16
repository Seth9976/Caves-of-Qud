using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000391 RID: 913
	[NullableContext(1)]
	public interface IAssignmentOperation : IOperation
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002565 RID: 9573
		IOperation Target { get; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002566 RID: 9574
		IOperation Value { get; }
	}
}
