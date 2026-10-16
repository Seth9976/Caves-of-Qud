using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003DB RID: 987
	public interface INegatedPatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060025ED RID: 9709
		[Nullable(1)]
		IPatternOperation Pattern
		{
			[NullableContext(1)]
			get;
		}
	}
}
