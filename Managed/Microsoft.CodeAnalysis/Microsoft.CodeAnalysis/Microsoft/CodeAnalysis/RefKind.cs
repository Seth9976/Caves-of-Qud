using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200025D RID: 605
	public enum RefKind : byte
	{
		// Token: 0x0400094E RID: 2382
		None,
		// Token: 0x0400094F RID: 2383
		Ref,
		// Token: 0x04000950 RID: 2384
		Out,
		// Token: 0x04000951 RID: 2385
		In,
		// Token: 0x04000952 RID: 2386
		RefReadOnly = 3
	}
}
