using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000CE RID: 206
	internal interface IGenericMethodParameter : IGenericParameter, IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry, IGenericMethodParameterReference
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000715 RID: 1813
		[Nullable(1)]
		IMethodDefinition DefiningMethod
		{
			[NullableContext(1)]
			get;
		}
	}
}
