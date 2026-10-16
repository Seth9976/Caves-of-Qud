using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B3 RID: 947
	public interface IParameterInitializerOperation : ISymbolInitializerOperation, IOperation
	{
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600259B RID: 9627
		[Nullable(1)]
		IParameterSymbol Parameter
		{
			[NullableContext(1)]
			get;
		}
	}
}
