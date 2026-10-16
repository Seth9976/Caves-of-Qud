using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A1 RID: 929
	[NullableContext(1)]
	public interface IDynamicInvocationOperation : IOperation
	{
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002585 RID: 9605
		IOperation Operation { get; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002586 RID: 9606
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
