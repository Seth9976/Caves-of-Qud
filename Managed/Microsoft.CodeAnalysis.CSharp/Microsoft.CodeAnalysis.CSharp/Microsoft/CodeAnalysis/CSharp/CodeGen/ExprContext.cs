using System;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005F8 RID: 1528
	internal enum ExprContext
	{
		// Token: 0x04001E4E RID: 7758
		None,
		// Token: 0x04001E4F RID: 7759
		Sideeffects,
		// Token: 0x04001E50 RID: 7760
		Value,
		// Token: 0x04001E51 RID: 7761
		Address,
		// Token: 0x04001E52 RID: 7762
		AssignmentTarget,
		// Token: 0x04001E53 RID: 7763
		Box
	}
}
