using System;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E0 RID: 1248
	internal enum ScopeType
	{
		// Token: 0x04001360 RID: 4960
		Variable,
		// Token: 0x04001361 RID: 4961
		TryCatchFinally,
		// Token: 0x04001362 RID: 4962
		Try,
		// Token: 0x04001363 RID: 4963
		Catch,
		// Token: 0x04001364 RID: 4964
		Filter,
		// Token: 0x04001365 RID: 4965
		Finally,
		// Token: 0x04001366 RID: 4966
		Fault,
		// Token: 0x04001367 RID: 4967
		StateMachineVariable
	}
}
