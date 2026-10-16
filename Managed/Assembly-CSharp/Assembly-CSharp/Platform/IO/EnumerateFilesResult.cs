using System;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x02000346 RID: 838
	public struct EnumerateFilesResult : IStorageErrorable<EnumerateFilesResult>
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x00098E20 File Offset: 0x00097020
		public EnumerateFilesResult(StorageResult result, string[] files)
		{
			this.result = result;
			this.files = files;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00098E30 File Offset: 0x00097030
		public EnumerateFilesResult ThrowIfFailed()
		{
			this.result.ThrowIfFailedWithDetails("Enumerate Files issue");
			return this;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00098E49 File Offset: 0x00097049
		public bool WasSuccessful()
		{
			return this.result.WasSuccessful();
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00098E56 File Offset: 0x00097056
		public EnumerateFilesResult LogErrorIfFailed()
		{
			this.result.LogIfErrored();
			return this;
		}

		// Token: 0x04001020 RID: 4128
		public StorageResult result;

		// Token: 0x04001021 RID: 4129
		public string[] files;
	}
}
