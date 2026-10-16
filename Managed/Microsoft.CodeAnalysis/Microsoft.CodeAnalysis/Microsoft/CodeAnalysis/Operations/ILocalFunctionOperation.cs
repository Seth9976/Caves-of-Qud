using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000377 RID: 887
	[NullableContext(2)]
	public interface ILocalFunctionOperation : IOperation
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002529 RID: 9513
		[Nullable(1)]
		IMethodSymbol Symbol
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600252A RID: 9514
		IBlockOperation Body { get; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600252B RID: 9515
		IBlockOperation IgnoredBody { get; }
	}
}
