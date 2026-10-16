using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000A2 RID: 162
	internal interface ITypeMemberReference : IReference, INamedEntity
	{
		// Token: 0x06000465 RID: 1125
		[NullableContext(1)]
		ITypeReference GetContainingType(EmitContext context);
	}
}
