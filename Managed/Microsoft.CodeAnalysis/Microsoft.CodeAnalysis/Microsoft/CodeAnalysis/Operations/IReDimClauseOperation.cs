using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003CE RID: 974
	[NullableContext(1)]
	public interface IReDimClauseOperation : IOperation
	{
		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060025D2 RID: 9682
		IOperation Operand { get; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060025D3 RID: 9683
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> DimensionSizes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
