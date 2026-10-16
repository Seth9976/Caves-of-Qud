using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003BA RID: 954
	[NullableContext(2)]
	public interface ICaseClauseOperation : IOperation
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060025B0 RID: 9648
		CaseKind CaseKind { get; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060025B1 RID: 9649
		ILabelSymbol Label { get; }
	}
}
