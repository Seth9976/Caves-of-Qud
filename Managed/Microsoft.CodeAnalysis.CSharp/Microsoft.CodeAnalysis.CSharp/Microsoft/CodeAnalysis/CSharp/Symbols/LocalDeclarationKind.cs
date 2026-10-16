using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004EC RID: 1260
	internal enum LocalDeclarationKind : byte
	{
		// Token: 0x04001A7C RID: 6780
		None,
		// Token: 0x04001A7D RID: 6781
		RegularVariable,
		// Token: 0x04001A7E RID: 6782
		Constant,
		// Token: 0x04001A7F RID: 6783
		FixedVariable,
		// Token: 0x04001A80 RID: 6784
		UsingVariable,
		// Token: 0x04001A81 RID: 6785
		CatchVariable,
		// Token: 0x04001A82 RID: 6786
		ForEachIterationVariable,
		// Token: 0x04001A83 RID: 6787
		PatternVariable,
		// Token: 0x04001A84 RID: 6788
		DeconstructionVariable,
		// Token: 0x04001A85 RID: 6789
		OutVariable,
		// Token: 0x04001A86 RID: 6790
		DeclarationExpressionVariable
	}
}
