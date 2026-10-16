using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200037F RID: 895
	[NullableContext(1)]
	public interface ILocalReferenceOperation : IOperation
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002539 RID: 9529
		ILocalSymbol Local { get; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600253A RID: 9530
		bool IsDeclaration { get; }
	}
}
