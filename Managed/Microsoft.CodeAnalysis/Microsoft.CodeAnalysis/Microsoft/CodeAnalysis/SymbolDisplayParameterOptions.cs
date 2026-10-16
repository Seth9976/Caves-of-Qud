using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200020D RID: 525
	[Flags]
	public enum SymbolDisplayParameterOptions
	{
		// Token: 0x0400075B RID: 1883
		None = 0,
		// Token: 0x0400075C RID: 1884
		IncludeExtensionThis = 1,
		// Token: 0x0400075D RID: 1885
		IncludeParamsRefOut = 2,
		// Token: 0x0400075E RID: 1886
		IncludeType = 4,
		// Token: 0x0400075F RID: 1887
		IncludeName = 8,
		// Token: 0x04000760 RID: 1888
		IncludeDefaultValue = 16,
		// Token: 0x04000761 RID: 1889
		IncludeOptionalBrackets = 32
	}
}
