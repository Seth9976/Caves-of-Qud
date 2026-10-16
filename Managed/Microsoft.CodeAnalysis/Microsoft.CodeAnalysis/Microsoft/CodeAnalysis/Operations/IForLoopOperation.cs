using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200036C RID: 876
	public interface IForLoopOperation : ILoopOperation, IOperation
	{
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600250B RID: 9483
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Before
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x0600250C RID: 9484
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> ConditionLocals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600250D RID: 9485
		[Nullable(2)]
		IOperation Condition
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600250E RID: 9486
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> AtLoopBottom
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
