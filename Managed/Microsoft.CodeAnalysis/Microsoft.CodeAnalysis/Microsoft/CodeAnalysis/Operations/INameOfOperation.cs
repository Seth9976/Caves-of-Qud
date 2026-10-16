using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200039D RID: 925
	[NullableContext(1)]
	public interface INameOfOperation : IOperation
	{
		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600257C RID: 9596
		IOperation Argument { get; }
	}
}
