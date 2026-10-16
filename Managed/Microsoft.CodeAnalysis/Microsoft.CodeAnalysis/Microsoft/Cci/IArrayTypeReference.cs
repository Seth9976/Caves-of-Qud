using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000C9 RID: 201
	internal interface IArrayTypeReference : ITypeReference, IReference
	{
		// Token: 0x06000703 RID: 1795
		[NullableContext(1)]
		ITypeReference GetElementType(EmitContext context);

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000704 RID: 1796
		bool IsSZArray { get; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000705 RID: 1797
		ImmutableArray<int> LowerBounds { get; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000706 RID: 1798
		int Rank { get; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000707 RID: 1799
		ImmutableArray<int> Sizes { get; }
	}
}
