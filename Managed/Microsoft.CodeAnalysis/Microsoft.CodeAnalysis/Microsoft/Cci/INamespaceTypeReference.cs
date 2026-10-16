using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000D7 RID: 215
	internal interface INamespaceTypeReference : INamedTypeReference, ITypeReference, IReference, INamedEntity
	{
		// Token: 0x06000720 RID: 1824
		[NullableContext(1)]
		IUnitReference GetUnit(EmitContext context);

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000721 RID: 1825
		[Nullable(1)]
		string NamespaceName
		{
			[NullableContext(1)]
			get;
		}
	}
}
