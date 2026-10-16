using System;

namespace System.Net
{
	// Token: 0x0200058F RID: 1423
	[Flags]
	internal enum FtpMethodFlags
	{
		// Token: 0x0400197B RID: 6523
		None = 0,
		// Token: 0x0400197C RID: 6524
		IsDownload = 1,
		// Token: 0x0400197D RID: 6525
		IsUpload = 2,
		// Token: 0x0400197E RID: 6526
		TakesParameter = 4,
		// Token: 0x0400197F RID: 6527
		MayTakeParameter = 8,
		// Token: 0x04001980 RID: 6528
		DoesNotTakeParameter = 16,
		// Token: 0x04001981 RID: 6529
		ParameterIsDirectory = 32,
		// Token: 0x04001982 RID: 6530
		ShouldParseForResponseUri = 64,
		// Token: 0x04001983 RID: 6531
		HasHttpCommand = 128,
		// Token: 0x04001984 RID: 6532
		MustChangeWorkingDirectoryToPath = 256
	}
}
