using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200020C RID: 524
	[Flags]
	public enum SymbolDisplayMiscellaneousOptions
	{
		// Token: 0x04000750 RID: 1872
		None = 0,
		// Token: 0x04000751 RID: 1873
		UseSpecialTypes = 1,
		// Token: 0x04000752 RID: 1874
		EscapeKeywordIdentifiers = 2,
		// Token: 0x04000753 RID: 1875
		UseAsterisksInMultiDimensionalArrays = 4,
		// Token: 0x04000754 RID: 1876
		UseErrorTypeSymbolName = 8,
		// Token: 0x04000755 RID: 1877
		RemoveAttributeSuffix = 16,
		// Token: 0x04000756 RID: 1878
		ExpandNullable = 32,
		// Token: 0x04000757 RID: 1879
		IncludeNullableReferenceTypeModifier = 64,
		// Token: 0x04000758 RID: 1880
		AllowDefaultLiteral = 128,
		// Token: 0x04000759 RID: 1881
		IncludeNotNullableReferenceTypeModifier = 256
	}
}
