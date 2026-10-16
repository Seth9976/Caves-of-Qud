using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000D0 RID: 208
	internal interface IGenericTypeInstanceReference : ITypeReference, IReference
	{
		// Token: 0x06000717 RID: 1815
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeReference> GetGenericArguments(EmitContext context);

		// Token: 0x06000718 RID: 1816
		[NullableContext(1)]
		INamedTypeReference GetGenericType(EmitContext context);
	}
}
