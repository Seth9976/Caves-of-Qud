using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200003F RID: 63
	internal interface IUnityCallback
	{
		// Token: 0x06000148 RID: 328
		void OnSetupFailed(string json);

		// Token: 0x06000149 RID: 329
		void OnProductsRetrieved(string json);

		// Token: 0x0600014A RID: 330
		void OnPurchaseSucceeded(string id, string receipt, string transactionID);

		// Token: 0x0600014B RID: 331
		void OnPurchaseFailed(string json);
	}
}
