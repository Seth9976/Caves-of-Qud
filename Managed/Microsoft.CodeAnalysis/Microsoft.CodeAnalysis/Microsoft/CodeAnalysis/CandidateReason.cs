using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000139 RID: 313
	public enum CandidateReason
	{
		// Token: 0x04000356 RID: 854
		None,
		// Token: 0x04000357 RID: 855
		NotATypeOrNamespace,
		// Token: 0x04000358 RID: 856
		NotAnEvent,
		// Token: 0x04000359 RID: 857
		NotAWithEventsMember,
		// Token: 0x0400035A RID: 858
		NotAnAttributeType,
		// Token: 0x0400035B RID: 859
		WrongArity,
		// Token: 0x0400035C RID: 860
		NotCreatable,
		// Token: 0x0400035D RID: 861
		NotReferencable,
		// Token: 0x0400035E RID: 862
		Inaccessible,
		// Token: 0x0400035F RID: 863
		NotAValue,
		// Token: 0x04000360 RID: 864
		NotAVariable,
		// Token: 0x04000361 RID: 865
		NotInvocable,
		// Token: 0x04000362 RID: 866
		StaticInstanceMismatch,
		// Token: 0x04000363 RID: 867
		OverloadResolutionFailure,
		// Token: 0x04000364 RID: 868
		LateBound,
		// Token: 0x04000365 RID: 869
		Ambiguous,
		// Token: 0x04000366 RID: 870
		MemberGroup
	}
}
