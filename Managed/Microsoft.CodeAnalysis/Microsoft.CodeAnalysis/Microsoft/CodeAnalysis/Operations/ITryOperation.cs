using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000374 RID: 884
	[NullableContext(2)]
	public interface ITryOperation : IOperation
	{
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002520 RID: 9504
		[Nullable(1)]
		IBlockOperation Body
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002521 RID: 9505
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICatchClauseOperation> Catches
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002522 RID: 9506
		IBlockOperation Finally { get; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002523 RID: 9507
		ILabelSymbol ExitLabel { get; }
	}
}
