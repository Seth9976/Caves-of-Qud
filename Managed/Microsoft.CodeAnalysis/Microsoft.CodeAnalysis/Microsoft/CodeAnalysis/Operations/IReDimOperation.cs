using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003CD RID: 973
	public interface IReDimOperation : IOperation
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060025D0 RID: 9680
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IReDimClauseOperation> Clauses
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060025D1 RID: 9681
		bool Preserve { get; }
	}
}
