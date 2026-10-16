using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003DF RID: 991
	[NullableContext(1)]
	public interface IWithOperation : IOperation
	{
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060025F4 RID: 9716
		IOperation Operand { get; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060025F5 RID: 9717
		[Nullable(2)]
		IMethodSymbol CloneMethod
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060025F6 RID: 9718
		IObjectOrCollectionInitializerOperation Initializer { get; }
	}
}
