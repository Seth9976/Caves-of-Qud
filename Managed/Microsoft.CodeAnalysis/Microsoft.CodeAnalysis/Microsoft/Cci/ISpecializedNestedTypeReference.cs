using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000DA RID: 218
	internal interface ISpecializedNestedTypeReference : INestedTypeReference, INamedTypeReference, ITypeReference, IReference, INamedEntity, ITypeMemberReference
	{
		// Token: 0x06000722 RID: 1826
		[NullableContext(1)]
		[return: NotNull]
		INestedTypeReference GetUnspecializedVersion(EmitContext context);
	}
}
