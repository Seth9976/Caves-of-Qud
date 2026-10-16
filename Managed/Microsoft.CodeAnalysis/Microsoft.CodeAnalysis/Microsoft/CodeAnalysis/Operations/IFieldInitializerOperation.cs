using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003B0 RID: 944
	public interface IFieldInitializerOperation : ISymbolInitializerOperation, IOperation
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002599 RID: 9625
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IFieldSymbol> InitializedFields
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
