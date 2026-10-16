using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200039C RID: 924
	[NullableContext(1)]
	[Obsolete("ICollectionElementInitializerOperation has been replaced with IInvocationOperation and IDynamicInvocationOperation", true)]
	public interface ICollectionElementInitializerOperation : IOperation
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002579 RID: 9593
		IMethodSymbol AddMethod { get; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600257A RID: 9594
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600257B RID: 9595
		bool IsDynamic { get; }
	}
}
