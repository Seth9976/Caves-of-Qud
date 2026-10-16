using System;

namespace UnityEngine.Android
{
	// Token: 0x02000016 RID: 22
	public enum ExitReason
	{
		// Token: 0x04000039 RID: 57
		Unknown,
		// Token: 0x0400003A RID: 58
		ExitSelf,
		// Token: 0x0400003B RID: 59
		Signaled,
		// Token: 0x0400003C RID: 60
		LowMemory,
		// Token: 0x0400003D RID: 61
		Crash,
		// Token: 0x0400003E RID: 62
		CrashNative,
		// Token: 0x0400003F RID: 63
		ANR,
		// Token: 0x04000040 RID: 64
		InititalizationFailure,
		// Token: 0x04000041 RID: 65
		PermissionChange,
		// Token: 0x04000042 RID: 66
		ExcessiveResourceUsage,
		// Token: 0x04000043 RID: 67
		UserRequested,
		// Token: 0x04000044 RID: 68
		UserStopped,
		// Token: 0x04000045 RID: 69
		DependencyDied,
		// Token: 0x04000046 RID: 70
		Other,
		// Token: 0x04000047 RID: 71
		Freezer,
		// Token: 0x04000048 RID: 72
		PackageStateChange,
		// Token: 0x04000049 RID: 73
		PackageUpdated
	}
}
