using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000381 RID: 897
	[NullableContext(1)]
	public interface IMemberReferenceOperation : IOperation
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600253C RID: 9532
		[Nullable(2)]
		IOperation Instance
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600253D RID: 9533
		ISymbol Member { get; }
	}
}
