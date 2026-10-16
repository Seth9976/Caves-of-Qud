using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000369 RID: 873
	[NullableContext(1)]
	public interface ISwitchOperation : IOperation
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060024FE RID: 9470
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060024FF RID: 9471
		IOperation Value { get; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002500 RID: 9472
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISwitchCaseOperation> Cases
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002501 RID: 9473
		ILabelSymbol ExitLabel { get; }
	}
}
