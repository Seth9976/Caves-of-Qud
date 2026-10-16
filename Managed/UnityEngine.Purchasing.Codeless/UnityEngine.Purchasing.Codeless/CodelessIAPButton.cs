using System;
using UnityEngine.Events;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000005 RID: 5
	[AddComponentMenu("In-App Purchasing/IAP Button")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.purchasing@latest")]
	public class CodelessIAPButton : BaseIAPButton
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002296 File Offset: 0x00000496
		internal override string GetProductId()
		{
			return this.productId;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000229E File Offset: 0x0000049E
		internal override bool IsAPurchaseButton()
		{
			return this.buttonType == CodelessButtonType.Purchase;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022A9 File Offset: 0x000004A9
		protected override bool IsARestoreButton()
		{
			return this.buttonType == CodelessButtonType.Restore;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022B4 File Offset: 0x000004B4
		protected override bool ShouldConsumePurchase()
		{
			return this.consumePurchase;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022BC File Offset: 0x000004BC
		protected override void OnTransactionsRestored(bool success, string error)
		{
			CodelessIAPButton.OnTransactionsRestoredEvent onTransactionsRestoredEvent = this.onTransactionsRestored;
			if (onTransactionsRestoredEvent == null)
			{
				return;
			}
			onTransactionsRestoredEvent.Invoke(success, error);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022D0 File Offset: 0x000004D0
		protected override void OnPurchaseComplete(Product purchasedProduct)
		{
			CodelessIAPButton.OnPurchaseCompletedEvent onPurchaseCompletedEvent = this.onPurchaseComplete;
			if (onPurchaseCompletedEvent == null)
			{
				return;
			}
			onPurchaseCompletedEvent.Invoke(purchasedProduct);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022E3 File Offset: 0x000004E3
		public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
		{
			CodelessIAPButton.OnPurchaseFailedEvent onPurchaseFailedEvent = this.onPurchaseFailed;
			if (onPurchaseFailedEvent == null)
			{
				return;
			}
			onPurchaseFailedEvent.Invoke(product, failureDescription);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022F7 File Offset: 0x000004F7
		protected override Button GetPurchaseButton()
		{
			return this.button;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022FF File Offset: 0x000004FF
		protected override void AddButtonToCodelessListener()
		{
			CodelessIAPStoreListener.Instance.AddButton(this);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000230C File Offset: 0x0000050C
		protected override void RemoveButtonToCodelessListener()
		{
			CodelessIAPStoreListener.Instance.RemoveButton(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000231C File Offset: 0x0000051C
		internal override void OnInitCompleted()
		{
			Product product = CodelessIAPStoreListener.Instance.GetProduct(this.productId);
			if (product != null)
			{
				this.onProductFetched.Invoke(product);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002349 File Offset: 0x00000549
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			return base.ProcessPurchaseInternal(args);
		}

		// Token: 0x04000004 RID: 4
		[HideInInspector]
		public string productId;

		// Token: 0x04000005 RID: 5
		[Tooltip("The type of this button, can be either a purchase or a restore button.")]
		public CodelessButtonType buttonType;

		// Token: 0x04000006 RID: 6
		[Tooltip("Consume the product immediately after a successful purchase.")]
		public bool consumePurchase = true;

		// Token: 0x04000007 RID: 7
		[Tooltip("Event fired after a restore transactions.")]
		public CodelessIAPButton.OnTransactionsRestoredEvent onTransactionsRestored;

		// Token: 0x04000008 RID: 8
		[Tooltip("Event fired after a successful purchase of this product.")]
		public CodelessIAPButton.OnPurchaseCompletedEvent onPurchaseComplete;

		// Token: 0x04000009 RID: 9
		[Tooltip("Event fired after a failed purchase of this product.")]
		public CodelessIAPButton.OnPurchaseFailedEvent onPurchaseFailed;

		// Token: 0x0400000A RID: 10
		[Tooltip("Event fired after a successful fetching the product information from the store.")]
		public CodelessIAPButton.OnProductFetchedEvent onProductFetched;

		// Token: 0x0400000B RID: 11
		[Tooltip("Button that triggers purchase.")]
		public Button button;

		// Token: 0x0200000C RID: 12
		[Serializable]
		public class OnProductFetchedEvent : UnityEvent<Product>
		{
		}

		// Token: 0x0200000D RID: 13
		[Serializable]
		public class OnPurchaseCompletedEvent : UnityEvent<Product>
		{
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public class OnPurchaseFailedEvent : UnityEvent<Product, PurchaseFailureDescription>
		{
		}

		// Token: 0x0200000F RID: 15
		[Serializable]
		public class OnTransactionsRestoredEvent : UnityEvent<bool, string>
		{
		}
	}
}
