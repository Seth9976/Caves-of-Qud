using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200039F RID: 927
	[NullableContext(2)]
	public interface IDynamicObjectCreationOperation : IOperation
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600257F RID: 9599
		IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002580 RID: 9600
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
