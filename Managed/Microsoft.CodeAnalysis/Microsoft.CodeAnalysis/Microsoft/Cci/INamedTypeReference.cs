using System;

namespace Microsoft.Cci
{
	// Token: 0x020000D3 RID: 211
	internal interface INamedTypeReference : ITypeReference, IReference, INamedEntity
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600071B RID: 1819
		ushort GenericParameterCount { get; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600071C RID: 1820
		bool MangleName { get; }
	}
}
