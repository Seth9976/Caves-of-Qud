using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000047 RID: 71
	[Obsolete("UDP support will be removed in the next major update of In-App Purchasing. Right now, the UDP SDK will still function normally in tandem with IAP.")]
	public interface IUDPExtensions : IStoreExtension
	{
		// Token: 0x06000178 RID: 376
		object GetUserInfo();

		// Token: 0x06000179 RID: 377
		string GetLastInitializationError();

		// Token: 0x0600017A RID: 378
		void EnableDebugLog(bool enable);

		// Token: 0x0600017B RID: 379
		void RegisterPurchaseDeferredListener(Action<Product> action);
	}
}
