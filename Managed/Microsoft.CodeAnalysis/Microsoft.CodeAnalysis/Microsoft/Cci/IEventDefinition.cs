using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x02000098 RID: 152
	[NullableContext(1)]
	internal interface IEventDefinition : ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x060003FB RID: 1019
		IEnumerable<IMethodReference> GetAccessors(EmitContext context);

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003FC RID: 1020
		IMethodReference Adder { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003FD RID: 1021
		[Nullable(2)]
		IMethodReference Caller
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003FE RID: 1022
		bool IsRuntimeSpecial { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003FF RID: 1023
		bool IsSpecialName { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000400 RID: 1024
		IMethodReference Remover { get; }

		// Token: 0x06000401 RID: 1025
		ITypeReference GetType(EmitContext context);
	}
}
