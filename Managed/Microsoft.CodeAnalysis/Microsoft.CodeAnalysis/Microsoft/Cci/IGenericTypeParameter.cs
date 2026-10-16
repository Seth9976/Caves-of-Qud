using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000D1 RID: 209
	internal interface IGenericTypeParameter : IGenericParameter, IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry, IGenericTypeParameterReference
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000719 RID: 1817
		[Nullable(1)]
		ITypeDefinition DefiningType
		{
			[NullableContext(1)]
			get;
		}
	}
}
