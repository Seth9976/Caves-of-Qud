using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D1 RID: 977
	[NullableContext(1)]
	public interface ISwitchExpressionOperation : IOperation
	{
		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060025D9 RID: 9689
		IOperation Value { get; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060025DA RID: 9690
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ISwitchExpressionArmOperation> Arms
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
