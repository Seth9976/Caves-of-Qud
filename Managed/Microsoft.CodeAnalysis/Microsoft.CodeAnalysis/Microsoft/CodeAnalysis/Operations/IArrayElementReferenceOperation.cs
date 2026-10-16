using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200037E RID: 894
	[NullableContext(1)]
	public interface IArrayElementReferenceOperation : IOperation
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002537 RID: 9527
		IOperation ArrayReference { get; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002538 RID: 9528
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Indices
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
