using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A0 RID: 416
	[StructLayout(3)]
	internal struct ParamInfo<TypeSymbol> where TypeSymbol : class
	{
		// Token: 0x0400051F RID: 1311
		internal bool IsByRef;

		// Token: 0x04000520 RID: 1312
		internal TypeSymbol Type;

		// Token: 0x04000521 RID: 1313
		internal ParameterHandle Handle;

		// Token: 0x04000522 RID: 1314
		internal ImmutableArray<ModifierInfo<TypeSymbol>> RefCustomModifiers;

		// Token: 0x04000523 RID: 1315
		internal ImmutableArray<ModifierInfo<TypeSymbol>> CustomModifiers;
	}
}
