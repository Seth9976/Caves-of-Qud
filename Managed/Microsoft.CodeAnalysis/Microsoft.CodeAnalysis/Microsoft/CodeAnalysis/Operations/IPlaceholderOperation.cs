using System;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D7 RID: 983
	internal interface IPlaceholderOperation : IOperation
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060025E7 RID: 9703
		PlaceholderKind PlaceholderKind { get; }
	}
}
