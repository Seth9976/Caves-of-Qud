using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000054 RID: 84
	public enum AppStore
	{
		// Token: 0x040000D4 RID: 212
		NotSpecified,
		// Token: 0x040000D5 RID: 213
		GooglePlay,
		// Token: 0x040000D6 RID: 214
		AmazonAppStore,
		// Token: 0x040000D7 RID: 215
		[Obsolete("AppStore to be removed with UDP deprecation.")]
		UDP,
		// Token: 0x040000D8 RID: 216
		MacAppStore,
		// Token: 0x040000D9 RID: 217
		AppleAppStore,
		// Token: 0x040000DA RID: 218
		WinRT,
		// Token: 0x040000DB RID: 219
		fake
	}
}
