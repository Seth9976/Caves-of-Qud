using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B4 RID: 948
	public interface IArrayInitializerOperation : IOperation
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600259C RID: 9628
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> ElementValues
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
