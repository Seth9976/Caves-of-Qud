using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000367 RID: 871
	public interface IBlockOperation : IOperation
	{
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060024FB RID: 9467
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Operations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060024FC RID: 9468
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
