using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000A1 RID: 161
	internal interface ITypeDefinitionMember : ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000463 RID: 1123
		[Nullable(1)]
		ITypeDefinition ContainingTypeDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000464 RID: 1124
		TypeMemberVisibility Visibility { get; }
	}
}
