using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000380 RID: 896
	[NullableContext(1)]
	public interface IParameterReferenceOperation : IOperation
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600253B RID: 9531
		IParameterSymbol Parameter { get; }
	}
}
