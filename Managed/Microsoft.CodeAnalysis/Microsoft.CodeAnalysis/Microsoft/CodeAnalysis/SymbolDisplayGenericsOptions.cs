using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000207 RID: 519
	[Flags]
	public enum SymbolDisplayGenericsOptions
	{
		// Token: 0x04000733 RID: 1843
		None = 0,
		// Token: 0x04000734 RID: 1844
		IncludeTypeParameters = 1,
		// Token: 0x04000735 RID: 1845
		IncludeTypeConstraints = 2,
		// Token: 0x04000736 RID: 1846
		IncludeVariance = 4
	}
}
