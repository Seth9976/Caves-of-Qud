using System;

namespace System.Net
{
	// Token: 0x020006C0 RID: 1728
	internal enum ReadState
	{
		// Token: 0x0400206E RID: 8302
		None,
		// Token: 0x0400206F RID: 8303
		Status,
		// Token: 0x04002070 RID: 8304
		Headers,
		// Token: 0x04002071 RID: 8305
		Content,
		// Token: 0x04002072 RID: 8306
		Aborted
	}
}
