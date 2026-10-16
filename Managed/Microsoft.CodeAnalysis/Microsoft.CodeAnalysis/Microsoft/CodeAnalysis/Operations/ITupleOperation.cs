using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200039E RID: 926
	[NullableContext(2)]
	public interface ITupleOperation : IOperation
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600257D RID: 9597
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Elements
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600257E RID: 9598
		ITypeSymbol NaturalType { get; }
	}
}
