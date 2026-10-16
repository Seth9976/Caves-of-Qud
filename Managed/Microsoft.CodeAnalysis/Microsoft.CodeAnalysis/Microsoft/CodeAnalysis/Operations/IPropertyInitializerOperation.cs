using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B2 RID: 946
	public interface IPropertyInitializerOperation : ISymbolInitializerOperation, IOperation
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600259A RID: 9626
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IPropertySymbol> InitializedProperties
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
