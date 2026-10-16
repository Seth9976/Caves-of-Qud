using System;

namespace UnityEngine.Android
{
	// Token: 0x02000019 RID: 25
	public enum AndroidAssetPackStatus
	{
		// Token: 0x0400004B RID: 75
		Unknown,
		// Token: 0x0400004C RID: 76
		Pending,
		// Token: 0x0400004D RID: 77
		Downloading,
		// Token: 0x0400004E RID: 78
		Transferring,
		// Token: 0x0400004F RID: 79
		Completed,
		// Token: 0x04000050 RID: 80
		Failed,
		// Token: 0x04000051 RID: 81
		Canceled,
		// Token: 0x04000052 RID: 82
		WaitingForWifi,
		// Token: 0x04000053 RID: 83
		NotInstalled
	}
}
