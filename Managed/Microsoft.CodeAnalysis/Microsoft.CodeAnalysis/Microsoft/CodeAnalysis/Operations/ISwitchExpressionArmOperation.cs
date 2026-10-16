using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D2 RID: 978
	[NullableContext(1)]
	public interface ISwitchExpressionArmOperation : IOperation
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060025DB RID: 9691
		IPatternOperation Pattern { get; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060025DC RID: 9692
		[Nullable(2)]
		IOperation Guard
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060025DD RID: 9693
		IOperation Value { get; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060025DE RID: 9694
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
