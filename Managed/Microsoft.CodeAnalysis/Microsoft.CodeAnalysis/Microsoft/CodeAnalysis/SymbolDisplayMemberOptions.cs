using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200020B RID: 523
	[Flags]
	public enum SymbolDisplayMemberOptions
	{
		// Token: 0x04000746 RID: 1862
		None = 0,
		// Token: 0x04000747 RID: 1863
		IncludeType = 1,
		// Token: 0x04000748 RID: 1864
		IncludeModifiers = 2,
		// Token: 0x04000749 RID: 1865
		IncludeAccessibility = 4,
		// Token: 0x0400074A RID: 1866
		IncludeExplicitInterface = 8,
		// Token: 0x0400074B RID: 1867
		IncludeParameters = 16,
		// Token: 0x0400074C RID: 1868
		IncludeContainingType = 32,
		// Token: 0x0400074D RID: 1869
		IncludeConstantValue = 64,
		// Token: 0x0400074E RID: 1870
		IncludeRef = 128
	}
}
