using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000281 RID: 641
	[Flags]
	public enum SyntaxRemoveOptions
	{
		// Token: 0x04000A23 RID: 2595
		KeepNoTrivia = 0,
		// Token: 0x04000A24 RID: 2596
		KeepLeadingTrivia = 1,
		// Token: 0x04000A25 RID: 2597
		KeepTrailingTrivia = 2,
		// Token: 0x04000A26 RID: 2598
		KeepExteriorTrivia = 3,
		// Token: 0x04000A27 RID: 2599
		KeepUnbalancedDirectives = 4,
		// Token: 0x04000A28 RID: 2600
		KeepDirectives = 8,
		// Token: 0x04000A29 RID: 2601
		KeepEndOfLine = 16,
		// Token: 0x04000A2A RID: 2602
		AddElasticMarker = 32
	}
}
