using System;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200038E RID: 910
	public interface IInstanceReferenceOperation : IOperation
	{
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002560 RID: 9568
		InstanceReferenceKind ReferenceKind { get; }
	}
}
