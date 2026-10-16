using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000399 RID: 921
	public interface IAnonymousObjectCreationOperation : IOperation
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002575 RID: 9589
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Initializers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
