using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000398 RID: 920
	public interface IInterpolatedStringOperation : IOperation
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002574 RID: 9588
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IInterpolatedStringContentOperation> Parts
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
