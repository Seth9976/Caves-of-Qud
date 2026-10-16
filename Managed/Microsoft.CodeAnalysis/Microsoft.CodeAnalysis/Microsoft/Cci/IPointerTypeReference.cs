using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000DD RID: 221
	internal interface IPointerTypeReference : ITypeReference, IReference
	{
		// Token: 0x06000727 RID: 1831
		[NullableContext(1)]
		ITypeReference GetTargetType(EmitContext context);
	}
}
