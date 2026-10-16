using System;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200019E RID: 414
	[StructLayout(3)]
	internal struct ModifierInfo<TypeSymbol> where TypeSymbol : class
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x0003025C File Offset: 0x0002E45C
		public ModifierInfo(bool isOptional, TypeSymbol modifier)
		{
			this.IsOptional = isOptional;
			this.Modifier = modifier;
		}

		// Token: 0x0400051D RID: 1309
		internal readonly bool IsOptional;

		// Token: 0x0400051E RID: 1310
		internal readonly TypeSymbol Modifier;
	}
}
