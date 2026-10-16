using System;

namespace Microsoft.CodeAnalysis.FlowAnalysis
{
	// Token: 0x020002DA RID: 730
	public enum ControlFlowBranchSemantics
	{
		// Token: 0x04000DA1 RID: 3489
		None,
		// Token: 0x04000DA2 RID: 3490
		Regular,
		// Token: 0x04000DA3 RID: 3491
		Return,
		// Token: 0x04000DA4 RID: 3492
		StructuredExceptionHandling,
		// Token: 0x04000DA5 RID: 3493
		ProgramTermination,
		// Token: 0x04000DA6 RID: 3494
		Throw,
		// Token: 0x04000DA7 RID: 3495
		Rethrow,
		// Token: 0x04000DA8 RID: 3496
		Error
	}
}
