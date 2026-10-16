using System;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x02000345 RID: 837
	public struct LoadBytesResult : IStorageErrorable<LoadBytesResult>
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x00098DCB File Offset: 0x00096FCB
		public LoadBytesResult LogErrorIfFailed()
		{
			this.result.LogIfFailedWithDetails("Path: " + this.path);
			return this;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00098DEF File Offset: 0x00096FEF
		public LoadBytesResult ThrowIfFailed()
		{
			this.result.ThrowIfFailedWithDetails("Path: " + this.path);
			return this;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00098E13 File Offset: 0x00097013
		public bool WasSuccessful()
		{
			return this.result.WasSuccessful();
		}

		// Token: 0x0400101D RID: 4125
		public StorageResult result;

		// Token: 0x0400101E RID: 4126
		public string path;

		// Token: 0x0400101F RID: 4127
		public byte[] content;
	}
}
