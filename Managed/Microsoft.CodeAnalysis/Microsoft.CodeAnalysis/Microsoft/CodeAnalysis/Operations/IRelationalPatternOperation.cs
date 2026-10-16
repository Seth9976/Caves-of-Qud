using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003DE RID: 990
	public interface IRelationalPatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060025F2 RID: 9714
		BinaryOperatorKind OperatorKind { get; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060025F3 RID: 9715
		[Nullable(1)]
		IOperation Value
		{
			[NullableContext(1)]
			get;
		}
	}
}
