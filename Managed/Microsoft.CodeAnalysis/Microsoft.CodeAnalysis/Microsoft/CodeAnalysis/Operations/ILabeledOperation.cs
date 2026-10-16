using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200036F RID: 879
	[NullableContext(1)]
	public interface ILabeledOperation : IOperation
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002519 RID: 9497
		ILabelSymbol Label { get; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600251A RID: 9498
		[Nullable(2)]
		IOperation Operation
		{
			[NullableContext(2)]
			get;
		}
	}
}
