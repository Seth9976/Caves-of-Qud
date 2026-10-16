using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200037A RID: 890
	[NullableContext(1)]
	public interface IRaiseEventOperation : IOperation
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600252C RID: 9516
		IEventReferenceOperation EventReference { get; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600252D RID: 9517
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
