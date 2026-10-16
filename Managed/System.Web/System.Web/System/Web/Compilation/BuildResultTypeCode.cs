using System;

namespace System.Web.Compilation
{
	// Token: 0x0200065E RID: 1630
	internal enum BuildResultTypeCode
	{
		// Token: 0x04002512 RID: 9490
		Unknown,
		// Token: 0x04002513 RID: 9491
		AppCodeSubFolder,
		// Token: 0x04002514 RID: 9492
		Handler,
		// Token: 0x04002515 RID: 9493
		PageOrControl,
		// Token: 0x04002516 RID: 9494
		AppCode = 6,
		// Token: 0x04002517 RID: 9495
		Global = 8,
		// Token: 0x04002518 RID: 9496
		TopLevelAssembly
	}
}
