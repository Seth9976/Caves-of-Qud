using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200036A RID: 874
	[NullableContext(1)]
	public interface ILoopOperation : IOperation
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002502 RID: 9474
		LoopKind LoopKind { get; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002503 RID: 9475
		IOperation Body { get; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002504 RID: 9476
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002505 RID: 9477
		ILabelSymbol ContinueLabel { get; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002506 RID: 9478
		ILabelSymbol ExitLabel { get; }
	}
}
