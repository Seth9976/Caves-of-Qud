using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000CF RID: 207
	internal interface IGenericMethodParameterReference : IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000716 RID: 1814
		[Nullable(1)]
		IMethodReference DefiningMethod
		{
			[NullableContext(1)]
			get;
		}
	}
}
