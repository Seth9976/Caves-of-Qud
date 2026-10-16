using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x020000A4 RID: 164
	internal interface ISpecializedFieldReference : IFieldReference, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000467 RID: 1127
		[Nullable(1)]
		IFieldReference UnspecializedVersion
		{
			[NullableContext(1)]
			get;
		}
	}
}
