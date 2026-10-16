using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000A8 RID: 168
	internal interface IGenericMethodInstanceReference : IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x06000471 RID: 1137
		[NullableContext(1)]
		IEnumerable<ITypeReference> GetGenericArguments(EmitContext context);

		// Token: 0x06000472 RID: 1138
		[NullableContext(1)]
		IMethodReference GetGenericMethod(EmitContext context);
	}
}
