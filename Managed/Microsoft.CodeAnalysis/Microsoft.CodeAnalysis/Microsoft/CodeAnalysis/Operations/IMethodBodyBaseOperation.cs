using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C7 RID: 967
	[NullableContext(2)]
	public interface IMethodBodyBaseOperation : IOperation
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060025C7 RID: 9671
		IBlockOperation BlockBody { get; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060025C8 RID: 9672
		IBlockOperation ExpressionBody { get; }
	}
}
