using System;

namespace Microsoft.Cci
{
	// Token: 0x020000D5 RID: 213
	internal interface INamespaceTypeDefinition : INamedTypeDefinition, ITypeDefinition, IDefinition, IReference, ITypeReference, INamedTypeReference, INamedEntity, INamespaceTypeReference
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600071D RID: 1821
		bool IsPublic { get; }
	}
}
