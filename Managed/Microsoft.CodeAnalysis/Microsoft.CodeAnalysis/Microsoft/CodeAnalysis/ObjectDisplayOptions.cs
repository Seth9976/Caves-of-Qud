using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000201 RID: 513
	[Flags]
	internal enum ObjectDisplayOptions
	{
		// Token: 0x04000701 RID: 1793
		None = 0,
		// Token: 0x04000702 RID: 1794
		IncludeCodePoints = 1,
		// Token: 0x04000703 RID: 1795
		IncludeTypeSuffix = 2,
		// Token: 0x04000704 RID: 1796
		UseHexadecimalNumbers = 4,
		// Token: 0x04000705 RID: 1797
		UseQuotes = 8,
		// Token: 0x04000706 RID: 1798
		EscapeNonPrintableCharacters = 16
	}
}
