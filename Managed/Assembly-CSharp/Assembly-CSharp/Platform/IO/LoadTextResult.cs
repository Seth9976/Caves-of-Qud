using System;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x02000344 RID: 836
	public struct LoadTextResult : IStorageErrorable<LoadTextResult>
	{
		// Token: 0x06001F5C RID: 8028 RVA: 0x00098D76 File Offset: 0x00096F76
		public LoadTextResult LogErrorIfFailed()
		{
			this.result.LogIfFailedWithDetails("Path: " + this.path);
			return this;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00098D9A File Offset: 0x00096F9A
		public LoadTextResult ThrowIfFailed()
		{
			this.result.ThrowIfFailedWithDetails("Path: " + this.path);
			return this;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00098DBE File Offset: 0x00096FBE
		public bool WasSuccessful()
		{
			return this.result.WasSuccessful();
		}

		// Token: 0x0400101A RID: 4122
		public StorageResult result;

		// Token: 0x0400101B RID: 4123
		public string path;

		// Token: 0x0400101C RID: 4124
		public string content;
	}
}
