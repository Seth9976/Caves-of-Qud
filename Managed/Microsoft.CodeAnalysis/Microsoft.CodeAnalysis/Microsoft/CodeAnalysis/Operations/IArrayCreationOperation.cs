using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200038D RID: 909
	[NullableContext(2)]
	public interface IArrayCreationOperation : IOperation
	{
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600255E RID: 9566
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> DimensionSizes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600255F RID: 9567
		IArrayInitializerOperation Initializer { get; }
	}
}
