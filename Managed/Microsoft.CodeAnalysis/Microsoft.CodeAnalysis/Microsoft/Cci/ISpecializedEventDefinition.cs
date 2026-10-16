using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000A3 RID: 163
	internal interface ISpecializedEventDefinition : IEventDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000466 RID: 1126
		[Nullable(1)]
		IEventDefinition UnspecializedVersion
		{
			[NullableContext(1)]
			[return: NotNull]
			get;
		}
	}
}
