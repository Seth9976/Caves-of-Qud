using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E7 RID: 1255
	[Flags]
	internal enum FlowAnalysisAnnotations
	{
		// Token: 0x04001A5E RID: 6750
		None = 0,
		// Token: 0x04001A5F RID: 6751
		AllowNull = 1,
		// Token: 0x04001A60 RID: 6752
		DisallowNull = 2,
		// Token: 0x04001A61 RID: 6753
		MaybeNullWhenTrue = 4,
		// Token: 0x04001A62 RID: 6754
		MaybeNullWhenFalse = 8,
		// Token: 0x04001A63 RID: 6755
		MaybeNull = 12,
		// Token: 0x04001A64 RID: 6756
		NotNullWhenTrue = 16,
		// Token: 0x04001A65 RID: 6757
		NotNullWhenFalse = 32,
		// Token: 0x04001A66 RID: 6758
		NotNull = 48,
		// Token: 0x04001A67 RID: 6759
		DoesNotReturnIfFalse = 64,
		// Token: 0x04001A68 RID: 6760
		DoesNotReturnIfTrue = 128,
		// Token: 0x04001A69 RID: 6761
		DoesNotReturn = 192
	}
}
