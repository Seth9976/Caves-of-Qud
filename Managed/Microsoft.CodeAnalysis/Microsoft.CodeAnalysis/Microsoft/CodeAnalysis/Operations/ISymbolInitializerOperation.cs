using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003AF RID: 943
	[NullableContext(1)]
	public interface ISymbolInitializerOperation : IOperation
	{
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002597 RID: 9623
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002598 RID: 9624
		IOperation Value { get; }
	}
}
