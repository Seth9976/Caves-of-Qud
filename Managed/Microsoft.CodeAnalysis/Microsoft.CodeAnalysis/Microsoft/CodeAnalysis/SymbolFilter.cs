using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000155 RID: 341
	[Flags]
	public enum SymbolFilter
	{
		// Token: 0x040003BE RID: 958
		None = 0,
		// Token: 0x040003BF RID: 959
		Namespace = 1,
		// Token: 0x040003C0 RID: 960
		Type = 2,
		// Token: 0x040003C1 RID: 961
		Member = 4,
		// Token: 0x040003C2 RID: 962
		TypeAndMember = 6,
		// Token: 0x040003C3 RID: 963
		All = 7
	}
}
