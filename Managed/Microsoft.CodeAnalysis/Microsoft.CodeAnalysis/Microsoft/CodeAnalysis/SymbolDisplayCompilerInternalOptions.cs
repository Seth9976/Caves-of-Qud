using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000202 RID: 514
	[Flags]
	internal enum SymbolDisplayCompilerInternalOptions
	{
		// Token: 0x04000708 RID: 1800
		None = 0,
		// Token: 0x04000709 RID: 1801
		UseMetadataMethodNames = 1,
		// Token: 0x0400070A RID: 1802
		UseArityForGenericTypes = 2,
		// Token: 0x0400070B RID: 1803
		FlagMissingMetadataTypes = 4,
		// Token: 0x0400070C RID: 1804
		IncludeScriptType = 8,
		// Token: 0x0400070D RID: 1805
		IncludeCustomModifiers = 16,
		// Token: 0x0400070E RID: 1806
		ReverseArrayRankSpecifiers = 32,
		// Token: 0x0400070F RID: 1807
		UseValueTuple = 64,
		// Token: 0x04000710 RID: 1808
		UseNativeIntegerUnderlyingType = 128
	}
}
