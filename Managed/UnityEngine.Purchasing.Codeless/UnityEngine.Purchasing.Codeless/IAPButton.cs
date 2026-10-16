using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000007 RID: 7
	[Obsolete("IAPButton is deprecated, please use CodelessIAPButton instead.", false)]
	[RequireComponent(typeof(Button))]
	[AddComponentMenu("In-App Purchasing/IAP Button (legacy)", 2147483647)]
	[HelpURL("https://docs.unity3d.com/Manual/UnityIAP.html")]
	public class IAPButton : BaseIAPButton
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002B98 File Offset: 0x00000D98
		internal override string GetProductId()
		{
			return this.productId;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BA0 File Offset: 0x00000DA0
		internal override bool IsAPurchaseButton()
		{
			return this.buttonType == IAPButton.ButtonType.Purchase;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BAB File Offset: 0x00000DAB
		protected override bool IsARestoreButton()
		{
			return this.buttonType == IAPButton.ButtonType.Restore;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BB6 File Offset: 0x00000DB6
		protected override bool ShouldConsumePurchase()
		{
			return this.consumePurchase;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BBE File Offset: 0x00000DBE
		protected override void OnTransactionsRestored(bool success, string error)
		{
			IAPButton.OnTransactionsRestoredEvent onTransactionsRestoredEvent = this.onTransactionsRestored;
			if (onTransactionsRestoredEvent == null)
			{
				return;
			}
			onTransactionsRestoredEvent.Invoke(success, error);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BD2 File Offset: 0x00000DD2
		protected override void OnPurchaseComplete(Product purchasedProduct)
		{
			IAPButton.OnPurchaseCompletedEvent onPurchaseCompletedEvent = this.onPurchaseComplete;
			if (onPurchaseCompletedEvent == null)
			{
				return;
			}
			onPurchaseCompletedEvent.Invoke(purchasedProduct);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BE5 File Offset: 0x00000DE5
		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
		{
			IAPButton.OnPurchaseFailedEvent onPurchaseFailedEvent = this.onPurchaseFailed;
			if (onPurchaseFailedEvent == null)
			{
				return;
			}
			onPurchaseFailedEvent.Invoke(product, reason);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BF9 File Offset: 0x00000DF9
		protected override Button GetPurchaseButton()
		{
			return base.GetComponent<Button>();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C01 File Offset: 0x00000E01
		protected override void AddButtonToCodelessListener()
		{
			CodelessIAPStoreListener.Instance.AddButton(this);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C0E File Offset: 0x00000E0E
		protected override void RemoveButtonToCodelessListener()
		{
			CodelessIAPStoreListener.Instance.RemoveButton(this);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C1B File Offset: 0x00000E1B
		internal override void OnInitCompleted()
		{
			this.UpdateAllTexts();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C24 File Offset: 0x00000E24
		private void UpdateAllTexts()
		{
			Product product = CodelessIAPStoreListener.Instance.GetProduct(this.productId);
			if (product != null)
			{
				if (this.titleText != null)
				{
					this.titleText.text = product.metadata.localizedTitle;
				}
				if (this.descriptionText != null)
				{
					this.descriptionText.text = product.metadata.localizedDescription;
				}
				if (this.priceText != null)
				{
					this.priceText.text = product.metadata.localizedPriceString;
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
		{
			return base.ProcessPurchaseInternal(e);
		}

		// Token: 0x04000016 RID: 22
		[HideInInspector]
		public string productId = "";

		// Token: 0x04000017 RID: 23
		[Tooltip("The type of this button, can be either a purchase or a restore button.")]
		public IAPButton.ButtonType buttonType;

		// Token: 0x04000018 RID: 24
		[Tooltip("Consume the product immediately after a successful purchase.")]
		public bool consumePurchase = true;

		// Token: 0x04000019 RID: 25
		[Tooltip("Event fired after a restore transactions.")]
		public IAPButton.OnTransactionsRestoredEvent onTransactionsRestored;

		// Token: 0x0400001A RID: 26
		[Tooltip("Event fired after a successful purchase of this product.")]
		public IAPButton.OnPurchaseCompletedEvent onPurchaseComplete;

		// Token: 0x0400001B RID: 27
		[Tooltip("Event fired after a failed purchase of this product.")]
		public IAPButton.OnPurchaseFailedEvent onPurchaseFailed;

		// Token: 0x0400001C RID: 28
		[Tooltip("[Optional] Displays the localized title from the app store.")]
		public Text titleText;

		// Token: 0x0400001D RID: 29
		[Tooltip("[Optional] Displays the localized description from the app store.")]
		public Text descriptionText;

		// Token: 0x0400001E RID: 30
		[Tooltip("[Optional] Displays the localized price from the app store.")]
		public Text priceText;

		// Token: 0x02000015 RID: 21
		public enum ButtonType
		{
			// Token: 0x0400003A RID: 58
			Purchase,
			// Token: 0x0400003B RID: 59
			Restore
		}

		// Token: 0x02000016 RID: 22
		[Serializable]
		public class OnPurchaseCompletedEvent : UnityEvent<Product>
		{
		}

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class OnPurchaseFailedEvent : UnityEvent<Product, PurchaseFailureReason>
		{
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		public class OnTransactionsRestoredEvent : UnityEvent<bool, string>
		{
		}
	}
}
