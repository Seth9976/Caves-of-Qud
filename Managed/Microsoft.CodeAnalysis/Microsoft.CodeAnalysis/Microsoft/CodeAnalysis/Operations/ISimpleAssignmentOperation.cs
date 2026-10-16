using System;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000392 RID: 914
	public interface ISimpleAssignmentOperation : IAssignmentOperation, IOperation
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002567 RID: 9575
		bool IsRef { get; }
	}
}
