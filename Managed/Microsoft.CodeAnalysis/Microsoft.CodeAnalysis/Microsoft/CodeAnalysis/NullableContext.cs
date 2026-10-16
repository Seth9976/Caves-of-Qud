using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000144 RID: 324
	[Flags]
	public enum NullableContext
	{
		// Token: 0x04000396 RID: 918
		Disabled = 0,
		// Token: 0x04000397 RID: 919
		WarningsEnabled = 1,
		// Token: 0x04000398 RID: 920
		AnnotationsEnabled = 2,
		// Token: 0x04000399 RID: 921
		Enabled = 3,
		// Token: 0x0400039A RID: 922
		WarningsContextInherited = 4,
		// Token: 0x0400039B RID: 923
		AnnotationsContextInherited = 8,
		// Token: 0x0400039C RID: 924
		ContextInherited = 12
	}
}
