using System;
using LaundryBear.PlatformServices;

namespace Platform.IO
{
	// Token: 0x02000343 RID: 835
	public struct ObjectLoadResult<T> : IStorageErrorable<ObjectLoadResult<T>>
	{
		// Token: 0x06001F54 RID: 8020 RVA: 0x00098CD3 File Offset: 0x00096ED3
		public ObjectLoadResult(PlatformIOResult result, T content)
		{
			this.result = result;
			this.content = content;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00098CE4 File Offset: 0x00096EE4
		public static ObjectLoadResult<T> CreateSuccess(T content)
		{
			return new ObjectLoadResult<T>
			{
				content = content
			};
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00098D02 File Offset: 0x00096F02
		public static ObjectLoadResult<T> CreateError(StorageResult result, string errorDetails = null)
		{
			return ObjectLoadResult<T>.CreateError(new PlatformIOResult(result, errorDetails));
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00098D10 File Offset: 0x00096F10
		public static ObjectLoadResult<T> CreateError(PlatformIOResult result)
		{
			return new ObjectLoadResult<T>
			{
				result = result
			};
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00098D2E File Offset: 0x00096F2E
		public bool WasSuccessful()
		{
			return this.result.WasSuccessful();
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00098D3B File Offset: 0x00096F3B
		public ObjectLoadResult<T> ThrowIfFailed()
		{
			this.result.ThrowIfFailed();
			return this;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00098D4F File Offset: 0x00096F4F
		public ObjectLoadResult<T> LogErrorIfFailed()
		{
			this.result.LogIfErrored();
			return this;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00098D63 File Offset: 0x00096F63
		public override string ToString()
		{
			return this.result.ToString();
		}

		// Token: 0x04001018 RID: 4120
		public T content;

		// Token: 0x04001019 RID: 4121
		public PlatformIOResult result;
	}
}
