using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D4 RID: 1236
	internal readonly struct TypeParameterDiagnosticInfo
	{
		// Token: 0x060068B6 RID: 26806 RVA: 0x00188BB5 File Offset: 0x00186DB5
		public TypeParameterDiagnosticInfo(TypeParameterSymbol typeParameter, UseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.TypeParameter = typeParameter;
			this.UseSiteInfo = useSiteInfo;
		}

		// Token: 0x04001A36 RID: 6710
		public readonly TypeParameterSymbol TypeParameter;

		// Token: 0x04001A37 RID: 6711
		public readonly UseSiteInfo<AssemblySymbol> UseSiteInfo;
	}
}
