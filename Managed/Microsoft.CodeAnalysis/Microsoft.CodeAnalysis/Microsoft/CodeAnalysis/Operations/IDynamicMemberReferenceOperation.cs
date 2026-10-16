using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003A0 RID: 928
	[NullableContext(2)]
	public interface IDynamicMemberReferenceOperation : IOperation
	{
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002581 RID: 9601
		IOperation Instance { get; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002582 RID: 9602
		[Nullable(1)]
		string MemberName
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002583 RID: 9603
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeSymbol> TypeArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002584 RID: 9604
		ITypeSymbol ContainingType { get; }
	}
}
