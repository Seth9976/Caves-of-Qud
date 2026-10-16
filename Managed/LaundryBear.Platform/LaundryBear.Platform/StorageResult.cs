using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000033 RID: 51
	public enum StorageResult
	{
		// Token: 0x04000074 RID: 116
		Success,
		// Token: 0x04000075 RID: 117
		FileNotFound,
		// Token: 0x04000076 RID: 118
		DirectoryNotFound,
		// Token: 0x04000077 RID: 119
		DriveNotFound,
		// Token: 0x04000078 RID: 120
		AlreadyExists,
		// Token: 0x04000079 RID: 121
		PathTooLong,
		// Token: 0x0400007A RID: 122
		QuotaExceeded,
		// Token: 0x0400007B RID: 123
		InvalidPath,
		// Token: 0x0400007C RID: 124
		OperationCancelled,
		// Token: 0x0400007D RID: 125
		InvalidPermissions,
		// Token: 0x0400007E RID: 126
		InUse,
		// Token: 0x0400007F RID: 127
		StorageNameTaken,
		// Token: 0x04000080 RID: 128
		StorageCountExceeded,
		// Token: 0x04000081 RID: 129
		Corrupted,
		// Token: 0x04000082 RID: 130
		NotReady,
		// Token: 0x04000083 RID: 131
		ServiceNotReady,
		// Token: 0x04000084 RID: 132
		UnknownFailure
	}
}
