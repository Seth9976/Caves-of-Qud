using System;
using LaundryBear.PlatformServices;
using UnityEngine;

namespace Platform.IO
{
	// Token: 0x02000341 RID: 833
	public struct PlatformIOResult : IStorageErrorable<PlatformIOResult>
	{
		// Token: 0x06001F4E RID: 8014 RVA: 0x00098C05 File Offset: 0x00096E05
		public PlatformIOResult(StorageResult result, string errorDetails = null)
		{
			this.result = result;
			this.errorDetails = errorDetails;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00098C15 File Offset: 0x00096E15
		public bool WasSuccessful()
		{
			return this.result.WasSuccessful();
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00098C22 File Offset: 0x00096E22
		public PlatformIOResult ThrowIfFailed()
		{
			if (this.WasSuccessful())
			{
				return this;
			}
			throw this.result.StorageResultToException(this.errorDetails);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00098C44 File Offset: 0x00096E44
		public PlatformIOResult LogErrorIfFailed()
		{
			if (!this.WasSuccessful())
			{
				Debug.LogError("Platform.IO Error:: " + this.ToString());
			}
			return this;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00098C6F File Offset: 0x00096E6F
		public PlatformIOResult LogIfErrored()
		{
			if (!this.WasSuccessful())
			{
				Debug.LogError("Platform.IO Error:: " + this.ToString());
			}
			return this;
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00098C9A File Offset: 0x00096E9A
		public override string ToString()
		{
			return string.Format("{0}: {1}, {2}: {3}", new object[] { "result", this.result, "errorDetails", this.errorDetails });
		}

		// Token: 0x04001013 RID: 4115
		public StorageResult result;

		// Token: 0x04001014 RID: 4116
		public string errorDetails;
	}
}
