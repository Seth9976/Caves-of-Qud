using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A2 RID: 930
	[NullableContext(1)]
	public interface IDynamicIndexerAccessOperation : IOperation
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002587 RID: 9607
		IOperation Operation { get; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002588 RID: 9608
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
