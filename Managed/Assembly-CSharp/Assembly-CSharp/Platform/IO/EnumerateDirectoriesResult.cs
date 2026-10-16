using System;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x02000347 RID: 839
	public struct EnumerateDirectoriesResult : IStorageErrorable<EnumerateDirectoriesResult>
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x00098E6A File Offset: 0x0009706A
		public EnumerateDirectoriesResult(StorageResult result, string[] directories)
		{
			this.result = result;
			this.directories = directories;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00098E7A File Offset: 0x0009707A
		public bool WasSuccessful()
		{
			return this.result.WasSuccessful();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00098E87 File Offset: 0x00097087
		public EnumerateDirectoriesResult ThrowIfFailed()
		{
			this.result.ThrowIfFailedWithDetails("Enumerate Directories issue");
			return this;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00098EA0 File Offset: 0x000970A0
		public EnumerateDirectoriesResult LogErrorIfFailed()
		{
			this.result.LogIfFailedWithDetails("Enumerate Directories issue");
			return this;
		}

		// Token: 0x04001022 RID: 4130
		public StorageResult result;

		// Token: 0x04001023 RID: 4131
		public string[] directories;
	}
}
