using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000052 RID: 82
	internal enum UserDefinedConversionResultKind : byte
	{
		// Token: 0x0400012F RID: 303
		NoApplicableOperators,
		// Token: 0x04000130 RID: 304
		NoBestSourceType,
		// Token: 0x04000131 RID: 305
		NoBestTargetType,
		// Token: 0x04000132 RID: 306
		Ambiguous,
		// Token: 0x04000133 RID: 307
		Valid
	}
}
