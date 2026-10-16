using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000DE RID: 222
	internal interface IFunctionPointerTypeReference : ITypeReference, IReference
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000728 RID: 1832
		[Nullable(1)]
		ISignature Signature
		{
			[NullableContext(1)]
			get;
		}
	}
}
