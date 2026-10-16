using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200039A RID: 922
	public interface IObjectOrCollectionInitializerOperation : IOperation
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002576 RID: 9590
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Initializers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
