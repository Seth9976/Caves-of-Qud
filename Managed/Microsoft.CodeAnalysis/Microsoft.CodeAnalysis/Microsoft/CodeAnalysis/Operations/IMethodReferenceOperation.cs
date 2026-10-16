using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000383 RID: 899
	public interface IMethodReferenceOperation : IMemberReferenceOperation, IOperation
	{
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002540 RID: 9536
		[Nullable(1)]
		IMethodSymbol Method
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002541 RID: 9537
		bool IsVirtual { get; }
	}
}
