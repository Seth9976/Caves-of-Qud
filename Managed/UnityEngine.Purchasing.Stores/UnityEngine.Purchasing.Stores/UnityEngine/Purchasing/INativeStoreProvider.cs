using System;
using Uniject;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000062 RID: 98
	internal interface INativeStoreProvider
	{
		// Token: 0x0600023F RID: 575
		INativeStore GetAndroidStore(IUnityCallback callback, AppStore store, IPurchasingBinder binder, IUtil util);

		// Token: 0x06000240 RID: 576
		INativeAppleStore GetStorekit(IUnityCallback callback);
	}
}
