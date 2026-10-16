using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003C9 RID: 969
	public interface IConstructorBodyOperation : IMethodBodyBaseOperation, IOperation
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060025C9 RID: 9673
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x060025CA RID: 9674
		[Nullable(2)]
		IOperation Initializer
		{
			[NullableContext(2)]
			get;
		}
	}
}
