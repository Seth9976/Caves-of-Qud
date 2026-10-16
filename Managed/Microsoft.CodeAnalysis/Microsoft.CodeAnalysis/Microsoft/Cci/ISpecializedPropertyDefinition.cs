using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000A6 RID: 166
	internal interface ISpecializedPropertyDefinition : IPropertyDefinition, ISignature, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000469 RID: 1129
		[Nullable(1)]
		IPropertyDefinition UnspecializedVersion
		{
			[NullableContext(1)]
			[return: NotNull]
			get;
		}
	}
}
