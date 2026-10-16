using System;
using UnityEngine.Events;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000009 RID: 9
	[AddComponentMenu("In-App Purchasing/IAP Listener")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.purchasing@latest")]
	public class IAPListener : MonoBehaviour
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002E14 File Offset: 0x00001014
		private void OnEnable()
		{
			if (this.dontDestroyOnLoad)
			{
				Object.DontDestroyOnLoad(base.gameObject);
			}
			CodelessIAPStoreListener.Instance.AddListener(this);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E34 File Offset: 0x00001034
		private void OnDisable()
		{
			CodelessIAPStoreListener.Instance.RemoveListener(this);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E41 File Offset: 0x00001041
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
		{
			this.onPurchaseComplete.Invoke(e.purchasedProduct);
			if (!this.consumePurchase)
			{
				return PurchaseProcessingResult.Pending;
			}
			return PurchaseProcessingResult.Complete;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E5F File Offset: 0x0000105F
		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
		{
			this.onPurchaseFailed.Invoke(product, reason);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E6E File Offset: 0x0000106E
		public void OnPurchaseFailed(Product product, PurchaseFailureDescription description)
		{
			this.onPurchaseDetailedFailedEvent.Invoke(product, description);
		}

		// Token: 0x0400001F RID: 31
		[Tooltip("Consume successful purchases immediately.")]
		public bool consumePurchase = true;

		// Token: 0x04000020 RID: 32
		[Tooltip("Preserve this GameObject when a new scene is loaded.")]
		public bool dontDestroyOnLoad = true;

		// Token: 0x04000021 RID: 33
		[Tooltip("Event fired after a successful fetching the products from the store.")]
		public IAPListener.OnProductsFetchedEvent onProductsFetched;

		// Token: 0x04000022 RID: 34
		[Tooltip("Event fired after a successful purchase of this product.")]
		public IAPListener.OnPurchaseCompletedEvent onPurchaseComplete;

		// Token: 0x04000023 RID: 35
		[Tooltip("Event fired after a failed purchase of this product.")]
		public IAPListener.OnPurchaseFailedEvent onPurchaseFailed;

		// Token: 0x04000024 RID: 36
		[Tooltip("Event fired after a failed purchase of this product.")]
		public IAPListener.OnPurchaseDetailedFailedEvent onPurchaseDetailedFailedEvent;

		// Token: 0x02000019 RID: 25
		[Serializable]
		public class OnProductsFetchedEvent : UnityEvent<ProductCollection>
		{
		}

		// Token: 0x0200001A RID: 26
		[Serializable]
		public class OnPurchaseCompletedEvent : UnityEvent<Product>
		{
		}

		// Token: 0x0200001B RID: 27
		[Serializable]
		public class OnPurchaseFailedEvent : UnityEvent<Product, PurchaseFailureReason>
		{
		}

		// Token: 0x0200001C RID: 28
		[Serializable]
		public class OnPurchaseDetailedFailedEvent : UnityEvent<Product, PurchaseFailureDescription>
		{
		}
	}
}
