using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000384 RID: 900
	public interface IPropertyReferenceOperation : IMemberReferenceOperation, IOperation
	{
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002542 RID: 9538
		[Nullable(1)]
		IPropertySymbol Property
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002543 RID: 9539
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
