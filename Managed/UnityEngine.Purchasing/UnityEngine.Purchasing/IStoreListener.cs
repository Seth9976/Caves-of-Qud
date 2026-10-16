using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200001B RID: 27
	public interface IStoreListener
	{
		// Token: 0x06000067 RID: 103
		[Obsolete]
		void OnInitializeFailed(InitializationFailureReason error);

		// Token: 0x06000068 RID: 104
		void OnInitializeFailed(InitializationFailureReason error, string message);

		// Token: 0x06000069 RID: 105
		PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent);

		// Token: 0x0600006A RID: 106
		[Obsolete("Use IDetailedStoreListener.OnPurchaseFailed for more detailed callback.", false)]
		void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason);

		// Token: 0x0600006B RID: 107
		void OnInitialized(IStoreController controller, IExtensionProvider extensions);
	}
}
