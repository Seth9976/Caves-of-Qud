using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000AA RID: 170
	internal interface IGlobalMethodDefinition : IMethodDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IMethodReference, ISignature
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000473 RID: 1139
		[Nullable(1)]
		string Name
		{
			[NullableContext(1)]
			get;
		}
	}
}
