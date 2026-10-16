using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x0200038B RID: 907
	[NullableContext(2)]
	public interface IObjectCreationOperation : IOperation
	{
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600255A RID: 9562
		IMethodSymbol Constructor { get; }

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600255B RID: 9563
		IObjectOrCollectionInitializerOperation Initializer { get; }

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600255C RID: 9564
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IArgumentOperation> Arguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
