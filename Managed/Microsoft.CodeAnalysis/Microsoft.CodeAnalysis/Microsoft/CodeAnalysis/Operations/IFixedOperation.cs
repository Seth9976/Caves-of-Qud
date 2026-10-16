using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003D5 RID: 981
	[NullableContext(1)]
	internal interface IFixedOperation : IOperation
	{
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060025E3 RID: 9699
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ILocalSymbol> Locals
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060025E4 RID: 9700
		IVariableDeclarationGroupOperation Variables { get; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060025E5 RID: 9701
		IOperation Body { get; }
	}
}
