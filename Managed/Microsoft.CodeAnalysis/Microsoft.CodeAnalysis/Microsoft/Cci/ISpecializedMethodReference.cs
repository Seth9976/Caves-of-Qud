using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000A5 RID: 165
	internal interface ISpecializedMethodReference : IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000468 RID: 1128
		[Nullable(1)]
		IMethodReference UnspecializedVersion
		{
			[NullableContext(1)]
			get;
		}
	}
}
