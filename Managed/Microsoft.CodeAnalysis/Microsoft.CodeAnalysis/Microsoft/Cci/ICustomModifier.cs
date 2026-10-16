using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000CA RID: 202
	[NullableContext(1)]
	internal interface ICustomModifier
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000708 RID: 1800
		bool IsOptional { get; }

		// Token: 0x06000709 RID: 1801
		ITypeReference GetModifier(EmitContext context);
	}
}
