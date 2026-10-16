using System;

namespace Roslyn.Utilities
{
	// Token: 0x0200002C RID: 44
	internal enum PathKind
	{
		// Token: 0x04000023 RID: 35
		Empty,
		// Token: 0x04000024 RID: 36
		Relative,
		// Token: 0x04000025 RID: 37
		RelativeToCurrentDirectory,
		// Token: 0x04000026 RID: 38
		RelativeToCurrentParent,
		// Token: 0x04000027 RID: 39
		RelativeToCurrentRoot,
		// Token: 0x04000028 RID: 40
		RelativeToDriveDirectory,
		// Token: 0x04000029 RID: 41
		Absolute
	}
}
