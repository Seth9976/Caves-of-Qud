using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C1 RID: 961
	public interface IInterpolatedStringTextOperation : IInterpolatedStringContentOperation, IOperation
	{
		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060025BA RID: 9658
		[Nullable(1)]
		IOperation Text
		{
			[NullableContext(1)]
			get;
		}
	}
}
