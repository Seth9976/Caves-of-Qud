using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000058 RID: 88
	internal enum OperatorAnalysisResultKind : byte
	{
		// Token: 0x04000148 RID: 328
		Undefined,
		// Token: 0x04000149 RID: 329
		Inapplicable,
		// Token: 0x0400014A RID: 330
		Worse,
		// Token: 0x0400014B RID: 331
		Applicable
	}
}
