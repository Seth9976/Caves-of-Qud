using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003DD RID: 989
	public interface ITypePatternOperation : IPatternOperation, IOperation
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060025F1 RID: 9713
		[Nullable(1)]
		ITypeSymbol MatchedType
		{
			[NullableContext(1)]
			get;
		}
	}
}
