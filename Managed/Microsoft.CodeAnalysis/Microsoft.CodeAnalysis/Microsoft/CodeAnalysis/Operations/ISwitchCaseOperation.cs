using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B9 RID: 953
	public interface ISwitchCaseOperation : IOperation
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060025AD RID: 9645
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICaseClauseOperation> Clauses
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060025AE RID: 9646
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Body
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060025AF RID: 9647
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
