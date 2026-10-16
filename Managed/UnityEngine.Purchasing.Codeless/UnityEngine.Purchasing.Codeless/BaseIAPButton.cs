using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000003 RID: 3
	public abstract class BaseIAPButton : MonoBehaviour
	{
		// Token: 0x06000003 RID: 3
		protected abstract bool ShouldConsumePurchase();

		// Token: 0x06000004 RID: 4
		protected abstract void OnTransactionsRestored(bool success, string error);

		// Token: 0x06000005 RID: 5
		protected abstract void OnPurchaseComplete(Product purchasedProduct);

		// Token: 0x06000006 RID: 6
		internal abstract void OnInitCompleted();

		// Token: 0x06000007 RID: 7
		protected abstract void AddButtonToCodelessListener();

		// Token: 0x06000008 RID: 8
		protected abstract void RemoveButtonToCodelessListener();

		// Token: 0x06000009 RID: 9
		protected abstract Button GetPurchaseButton();

		// Token: 0x0600000A RID: 10 RVA: 0x000020C0 File Offset: 0x000002C0
		private void Start()
		{
			Button purchaseButton = this.GetPurchaseButton();
			string productId = this.GetProductId();
			if (this.IsAPurchaseButton())
			{
				if (purchaseButton)
				{
					purchaseButton.onClick.AddListener(new UnityAction(this.PurchaseProduct));
				}
				if (string.IsNullOrEmpty(productId))
				{
					Debug.LogError("IAPButton productId is empty");
					return;
				}
				if (!CodelessIAPStoreListener.Instance.HasProductInCatalog(productId))
				{
					Debug.LogWarning("The product catalog has no product with the ID \"" + productId + "\"");
					return;
				}
			}
			else if (this.IsARestoreButton() && purchaseButton)
			{
				purchaseButton.onClick.AddListener(new UnityAction(this.Restore));
			}
		}

		// Token: 0x0600000B RID: 11
		internal abstract string GetProductId();

		// Token: 0x0600000C RID: 12
		internal abstract bool IsAPurchaseButton();

		// Token: 0x0600000D RID: 13
		protected abstract bool IsARestoreButton();

		// Token: 0x0600000E RID: 14 RVA: 0x0000215F File Offset: 0x0000035F
		private void OnEnable()
		{
			if (this.IsAPurchaseButton())
			{
				this.AddButtonToCodelessListener();
				if (CodelessIAPStoreListener.initializationComplete)
				{
					this.OnInitCompleted();
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000217C File Offset: 0x0000037C
		private void OnDisable()
		{
			if (this.IsAPurchaseButton())
			{
				this.RemoveButtonToCodelessListener();
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000218C File Offset: 0x0000038C
		private void PurchaseProduct()
		{
			if (this.IsAPurchaseButton())
			{
				CodelessIAPStoreListener.Instance.InitiatePurchase(this.GetProductId());
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021A6 File Offset: 0x000003A6
		protected PurchaseProcessingResult ProcessPurchaseInternal(PurchaseEventArgs args)
		{
			this.OnPurchaseComplete(args.purchasedProduct);
			if (!this.ShouldConsumePurchase())
			{
				return PurchaseProcessingResult.Pending;
			}
			return PurchaseProcessingResult.Complete;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021C0 File Offset: 0x000003C0
		private void Restore()
		{
			if (this.IsARestoreButton())
			{
				if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
				{
					CodelessIAPStoreListener.Instance.GetStoreExtensions<IMicrosoftExtensions>().RestoreTransactions();
					return;
				}
				if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.tvOS)
				{
					CodelessIAPStoreListener.Instance.GetStoreExtensions<IAppleExtensions>().RestoreTransactions(new Action<bool, string>(this.OnTransactionsRestored));
					return;
				}
				if (Application.platform == RuntimePlatform.Android && StandardPurchasingModule.Instance().appStore == AppStore.GooglePlay)
				{
					CodelessIAPStoreListener.Instance.GetStoreExtensions<IGooglePlayStoreExtensions>().RestoreTransactions(new Action<bool, string>(this.OnTransactionsRestored));
					return;
				}
				Debug.LogWarning(Application.platform.ToString() + " is not a supported platform for the Codeless IAP restore button");
			}
		}
	}
}
