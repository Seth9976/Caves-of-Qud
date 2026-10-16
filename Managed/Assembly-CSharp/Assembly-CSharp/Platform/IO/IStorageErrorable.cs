using System;

namespace Platform.IO
{
	// Token: 0x02000340 RID: 832
	public interface IStorageErrorable<T>
	{
		// Token: 0x06001F4B RID: 8011
		bool WasSuccessful();

		// Token: 0x06001F4C RID: 8012
		T ThrowIfFailed();

		// Token: 0x06001F4D RID: 8013
		T LogErrorIfFailed();
	}
}
