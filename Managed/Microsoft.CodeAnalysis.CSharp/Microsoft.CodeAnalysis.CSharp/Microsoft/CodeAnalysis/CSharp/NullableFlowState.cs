using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000220 RID: 544
	internal enum NullableFlowState : byte
	{
		// Token: 0x0400113C RID: 4412
		NotNull,
		// Token: 0x0400113D RID: 4413
		MaybeNull,
		// Token: 0x0400113E RID: 4414
		MaybeDefault = 3
	}
}
