using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x020003AD RID: 941
	[NullableContext(1)]
	public interface IDeclarationExpressionOperation : IOperation
	{
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002596 RID: 9622
		IOperation Expression { get; }
	}
}
