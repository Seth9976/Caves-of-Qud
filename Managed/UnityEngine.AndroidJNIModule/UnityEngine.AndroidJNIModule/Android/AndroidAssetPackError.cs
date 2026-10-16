using System;

namespace UnityEngine.Android
{
	// Token: 0x0200001A RID: 26
	public enum AndroidAssetPackError
	{
		// Token: 0x04000055 RID: 85
		NoError,
		// Token: 0x04000056 RID: 86
		AppUnavailable = -1,
		// Token: 0x04000057 RID: 87
		PackUnavailable = -2,
		// Token: 0x04000058 RID: 88
		InvalidRequest = -3,
		// Token: 0x04000059 RID: 89
		DownloadNotFound = -4,
		// Token: 0x0400005A RID: 90
		ApiNotAvailable = -5,
		// Token: 0x0400005B RID: 91
		NetworkError = -6,
		// Token: 0x0400005C RID: 92
		AccessDenied = -7,
		// Token: 0x0400005D RID: 93
		InsufficientStorage = -10,
		// Token: 0x0400005E RID: 94
		PlayStoreNotFound = -11,
		// Token: 0x0400005F RID: 95
		NetworkUnrestricted = -12,
		// Token: 0x04000060 RID: 96
		AppNotOwned = -13,
		// Token: 0x04000061 RID: 97
		InternalError = -100
	}
}
