using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003BC RID: 956
	[NullableContext(1)]
	public interface IPatternCaseClauseOperation : ICaseClauseOperation, IOperation
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060025B2 RID: 9650
		ILabelSymbol Label { get; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060025B3 RID: 9651
		IPatternOperation Pattern { get; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060025B4 RID: 9652
		[Nullable(2)]
		IOperation Guard
		{
			[NullableContext(2)]
			get;
		}
	}
}
