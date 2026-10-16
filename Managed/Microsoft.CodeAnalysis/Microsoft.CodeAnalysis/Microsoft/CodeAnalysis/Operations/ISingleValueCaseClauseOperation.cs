using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003BF RID: 959
	public interface ISingleValueCaseClauseOperation : ICaseClauseOperation, IOperation
	{
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060025B9 RID: 9657
		[Nullable(1)]
		IOperation Value
		{
			[NullableContext(1)]
			get;
		}
	}
}
