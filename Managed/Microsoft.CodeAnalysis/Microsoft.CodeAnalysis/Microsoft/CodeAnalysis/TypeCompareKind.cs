using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000264 RID: 612
	[Flags]
	internal enum TypeCompareKind
	{
		// Token: 0x04000971 RID: 2417
		ConsiderEverything = 0,
		// Token: 0x04000972 RID: 2418
		ConsiderEverything2 = 0,
		// Token: 0x04000973 RID: 2419
		IgnoreCustomModifiersAndArraySizesAndLowerBounds = 1,
		// Token: 0x04000974 RID: 2420
		IgnoreDynamic = 2,
		// Token: 0x04000975 RID: 2421
		IgnoreTupleNames = 4,
		// Token: 0x04000976 RID: 2422
		IgnoreDynamicAndTupleNames = 6,
		// Token: 0x04000977 RID: 2423
		IgnoreNullableModifiersForReferenceTypes = 8,
		// Token: 0x04000978 RID: 2424
		ObliviousNullableModifierMatchesAny = 16,
		// Token: 0x04000979 RID: 2425
		IgnoreNativeIntegers = 32,
		// Token: 0x0400097A RID: 2426
		FunctionPointerRefMatchesOutInRefReadonly = 64,
		// Token: 0x0400097B RID: 2427
		AllNullableIgnoreOptions = 24,
		// Token: 0x0400097C RID: 2428
		AllIgnoreOptions = 63,
		// Token: 0x0400097D RID: 2429
		AllIgnoreOptionsForVB = 5,
		// Token: 0x0400097E RID: 2430
		CLRSignatureCompareOptions = 62
	}
}
