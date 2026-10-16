using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C4 RID: 964
	public interface IConstantPatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060025C0 RID: 9664
		[Nullable(1)]
		IOperation Value
		{
			[NullableContext(1)]
			get;
		}
	}
}
