using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000368 RID: 872
	public interface IVariableDeclarationGroupOperation : IOperation
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060024FD RID: 9469
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IVariableDeclarationOperation> Declarations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
