using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200037D RID: 893
	[NullableContext(1)]
	public interface IInvocationOperation : IOperation
	{
		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002533 RID: 9523
		IMethodSymbol TargetMethod { get; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002534 RID: 9524
		[Nullable(2)]
		IOperation Instance
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002535 RID: 9525
		bool IsVirtual { get; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002536 RID: 9526
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
