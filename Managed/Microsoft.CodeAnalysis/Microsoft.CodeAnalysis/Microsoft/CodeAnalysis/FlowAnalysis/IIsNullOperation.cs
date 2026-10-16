using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002D2 RID: 722
	[NullableContext(1)]
	public interface IIsNullOperation : IOperation
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001E2F RID: 7727
		IOperation Operand { get; }
	}
}
