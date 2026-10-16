using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B5 RID: 949
	[NullableContext(1)]
	public interface IVariableDeclaratorOperation : IOperation
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x0600259D RID: 9629
		ILocalSymbol Symbol { get; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600259E RID: 9630
		[Nullable(2)]
		IVariableInitializerOperation Initializer
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600259F RID: 9631
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> IgnoredArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
