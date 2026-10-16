using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000D2 RID: 210
	internal interface IGenericTypeParameterReference : IGenericParameterReference, ITypeReference, IReference, INamedEntity, IParameterListEntry
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600071A RID: 1818
		[Nullable(1)]
		ITypeReference DefiningType
		{
			[NullableContext(1)]
			get;
		}
	}
}
