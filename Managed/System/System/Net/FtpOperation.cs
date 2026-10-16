using System;

namespace System.Net
{
	// Token: 0x0200058E RID: 1422
	internal enum FtpOperation
	{
		// Token: 0x0400196C RID: 6508
		DownloadFile,
		// Token: 0x0400196D RID: 6509
		ListDirectory,
		// Token: 0x0400196E RID: 6510
		ListDirectoryDetails,
		// Token: 0x0400196F RID: 6511
		UploadFile,
		// Token: 0x04001970 RID: 6512
		UploadFileUnique,
		// Token: 0x04001971 RID: 6513
		AppendFile,
		// Token: 0x04001972 RID: 6514
		DeleteFile,
		// Token: 0x04001973 RID: 6515
		GetDateTimestamp,
		// Token: 0x04001974 RID: 6516
		GetFileSize,
		// Token: 0x04001975 RID: 6517
		Rename,
		// Token: 0x04001976 RID: 6518
		MakeDirectory,
		// Token: 0x04001977 RID: 6519
		RemoveDirectory,
		// Token: 0x04001978 RID: 6520
		PrintWorkingDirectory,
		// Token: 0x04001979 RID: 6521
		Other
	}
}
