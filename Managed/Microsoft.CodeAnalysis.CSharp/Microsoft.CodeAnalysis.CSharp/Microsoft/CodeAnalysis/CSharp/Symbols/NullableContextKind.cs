using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000506 RID: 1286
	internal enum NullableContextKind : byte
	{
		// Token: 0x04001AD9 RID: 6873
		Unknown,
		// Token: 0x04001ADA RID: 6874
		None,
		// Token: 0x04001ADB RID: 6875
		Oblivious,
		// Token: 0x04001ADC RID: 6876
		NotAnnotated,
		// Token: 0x04001ADD RID: 6877
		Annotated
	}
}
