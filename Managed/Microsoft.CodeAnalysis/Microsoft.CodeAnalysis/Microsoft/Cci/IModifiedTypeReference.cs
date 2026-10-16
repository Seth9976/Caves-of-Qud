using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000DC RID: 220
	internal interface IModifiedTypeReference : ITypeReference, IReference
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000725 RID: 1829
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000726 RID: 1830
		[Nullable(1)]
		ITypeReference UnmodifiedType
		{
			[NullableContext(1)]
			get;
		}
	}
}
