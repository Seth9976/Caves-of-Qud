using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000370 RID: 880
	[NullableContext(1)]
	public interface IBranchOperation : IOperation
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600251B RID: 9499
		ILabelSymbol Target { get; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600251C RID: 9500
		BranchKind BranchKind { get; }
	}
}
