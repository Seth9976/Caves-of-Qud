using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000006 RID: 6
	public class CodelessIAPStoreListener : IDetailedStoreListener, IStoreListener
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002364 File Offset: 0x00000564
		[RuntimeInitializeOnLoadMethod]
		private static void InitializeCodelessPurchasingOnLoad()
		{
			ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog();
			if (productCatalog.enableCodelessAutoInitialization && !productCatalog.IsEmpty() && CodelessIAPStoreListener.instance == null)
			{
				CodelessIAPStoreListener.CreateCodelessIAPStoreListenerInstance();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002394 File Offset: 0x00000594
		private static void InitializePurchasing()
		{
			StandardPurchasingModule standardPurchasingModule = StandardPurchasingModule.Instance();
			standardPurchasingModule.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(standardPurchasingModule, Array.Empty<IPurchasingModule>());
			IAPConfigurationHelper.PopulateConfigurationBuilder(ref configurationBuilder, CodelessIAPStoreListener.Instance.catalog);
			CodelessIAPStoreListener.Instance.m_Builder = configurationBuilder;
			UnityPurchasing.Initialize(CodelessIAPStoreListener.Instance, configurationBuilder);
			CodelessIAPStoreListener.unityPurchasingInitialized = true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023E5 File Offset: 0x000005E5
		public T GetStoreConfiguration<T>() where T : IStoreConfiguration
		{
			return this.m_Builder.Configure<T>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023F2 File Offset: 0x000005F2
		public T GetStoreExtensions<T>() where T : IStoreExtension
		{
			return this.extensions.GetExtension<T>();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000023FF File Offset: 0x000005FF
		private CodelessIAPStoreListener()
		{
			this.catalog = ProductCatalog.LoadDefaultCatalog();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002433 File Offset: 0x00000633
		public static CodelessIAPStoreListener Instance
		{
			get
			{
				if (CodelessIAPStoreListener.instance == null)
				{
					CodelessIAPStoreListener.CreateCodelessIAPStoreListenerInstance();
				}
				return CodelessIAPStoreListener.instance;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002448 File Offset: 0x00000648
		private static async void CreateCodelessIAPStoreListenerInstance()
		{
			CodelessIAPStoreListener.instance = new CodelessIAPStoreListener();
			if (!CodelessIAPStoreListener.unityPurchasingInitialized)
			{
				await CodelessIAPStoreListener.AutoInitializeUnityGamingServicesIfEnabled();
				CodelessIAPStoreListener.InitializePurchasing();
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002477 File Offset: 0x00000677
		private static Task AutoInitializeUnityGamingServicesIfEnabled()
		{
			if (!CodelessIAPStoreListener.ShouldAutoInitUgs())
			{
				return Task.CompletedTask;
			}
			return UnityServices.InitializeAsync();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000248B File Offset: 0x0000068B
		private static bool ShouldAutoInitUgs()
		{
			return CodelessIAPStoreListener.Instance.catalog.enableCodelessAutoInitialization && CodelessIAPStoreListener.Instance.catalog.enableUnityGamingServicesAutoInitialization;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024AF File Offset: 0x000006AF
		public IStoreController StoreController
		{
			get
			{
				return this.controller;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024B8 File Offset: 0x000006B8
		public bool HasProductInCatalog(string productID)
		{
			using (IEnumerator<ProductCatalogItem> enumerator = this.catalog.allProducts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == productID)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002518 File Offset: 0x00000718
		public Product GetProduct(string productID)
		{
			if (this.controller != null && this.controller.products != null && !string.IsNullOrEmpty(productID))
			{
				return this.controller.products.WithID(productID);
			}
			Debug.LogError("CodelessIAPStoreListener attempted to get unknown product " + productID);
			return null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002565 File Offset: 0x00000765
		[Obsolete("CodelessIAPStoreListener.AddButton(IAPButton button) is deprecated, please use CodelessIAPStoreListener.AddButton(CodelessIAPButton button) instead.", false)]
		public void AddButton(IAPButton button)
		{
			this.activeButtons.Add(button);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002573 File Offset: 0x00000773
		public void AddButton(CodelessIAPButton button)
		{
			this.activeCodelessButtons.Add(button);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002581 File Offset: 0x00000781
		[Obsolete("CodelessIAPStoreListener.RemoveButton(IAPButton button) is deprecated, please use CodelessIAPStoreListener.RemoveButton(CodelessIAPButton button) instead.", false)]
		public void RemoveButton(IAPButton button)
		{
			this.activeButtons.Remove(button);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002590 File Offset: 0x00000790
		public void RemoveButton(CodelessIAPButton button)
		{
			this.activeCodelessButtons.Remove(button);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000259F File Offset: 0x0000079F
		public void AddListener(IAPListener listener)
		{
			this.activeListeners.Add(listener);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025AD File Offset: 0x000007AD
		public void RemoveListener(IAPListener listener)
		{
			this.activeListeners.Remove(listener);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025BC File Offset: 0x000007BC
		public void InitiatePurchase(string productID)
		{
			if (this.controller == null)
			{
				Debug.LogError("Purchase failed because Purchasing was not initialized correctly");
				this.SendPurchaseFailedEventsToAllButtons(productID);
				return;
			}
			this.controller.InitiatePurchase(productID);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000025E4 File Offset: 0x000007E4
		private void SendPurchaseFailedEventsToAllButtons(string productID)
		{
			IEnumerable<IAPButton> enumerable = this.activeButtons;
			Func<IAPButton, bool> <>9__0;
			Func<IAPButton, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (IAPButton button) => button.productId == productID);
			}
			foreach (IAPButton iapbutton in enumerable.Where<IAPButton>(func))
			{
				iapbutton.OnPurchaseFailed(null, PurchaseFailureReason.PurchasingUnavailable);
			}
			IEnumerable<CodelessIAPButton> enumerable2 = this.activeCodelessButtons;
			Func<CodelessIAPButton, bool> <>9__1;
			Func<CodelessIAPButton, bool> func2;
			if ((func2 = <>9__1) == null)
			{
				func2 = (<>9__1 = (CodelessIAPButton button) => button.productId == productID);
			}
			foreach (CodelessIAPButton codelessIAPButton in enumerable2.Where<CodelessIAPButton>(func2))
			{
				PurchaseFailureDescription purchaseFailureDescription = new PurchaseFailureDescription(productID, PurchaseFailureReason.PurchasingUnavailable, "PurchasingUnavailable");
				codelessIAPButton.OnPurchaseFailed(null, purchaseFailureDescription);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026D8 File Offset: 0x000008D8
		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			CodelessIAPStoreListener.initializationComplete = true;
			this.controller = controller;
			this.extensions = extensions;
			foreach (IAPListener iaplistener in this.activeListeners)
			{
				iaplistener.onProductsFetched.Invoke(controller.products);
			}
			this.HandleOnInitForAllButtons();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002750 File Offset: 0x00000950
		private void HandleOnInitForAllButtons()
		{
			foreach (IAPButton iapbutton in this.activeButtons)
			{
				iapbutton.OnInitCompleted();
			}
			foreach (CodelessIAPButton codelessIAPButton in this.activeCodelessButtons)
			{
				codelessIAPButton.OnInitCompleted();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027E0 File Offset: 0x000009E0
		[Obsolete("OnInitializeFailed(InitializationFailureReason error) is deprecated, please use OnInitializeFailed(InitializationFailureReason error, string message) instead.")]
		public void OnInitializeFailed(InitializationFailureReason error)
		{
			this.OnInitializeFailed(error, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027EC File Offset: 0x000009EC
		public void OnInitializeFailed(InitializationFailureReason error, string message)
		{
			string text = "Purchasing failed to initialize. Reason: " + error.ToString() + ".";
			if (message != null)
			{
				text = text + " More details: " + message;
			}
			Debug.LogError(text);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000282C File Offset: 0x00000A2C
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
		{
			bool flag = false;
			bool flag2 = false;
			IEnumerable<IAPButton> enumerable = this.activeButtons;
			Func<IAPButton, bool> <>9__0;
			Func<IAPButton, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (IAPButton button) => button.productId == e.purchasedProduct.definition.id);
			}
			using (IEnumerator<IAPButton> enumerator = enumerable.Where<IAPButton>(func).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
					{
						flag = true;
					}
					flag2 = true;
				}
			}
			IEnumerable<CodelessIAPButton> enumerable2 = this.activeCodelessButtons;
			Func<CodelessIAPButton, bool> <>9__1;
			Func<CodelessIAPButton, bool> func2;
			if ((func2 = <>9__1) == null)
			{
				func2 = (<>9__1 = (CodelessIAPButton button) => button.productId == e.purchasedProduct.definition.id);
			}
			using (IEnumerator<CodelessIAPButton> enumerator2 = enumerable2.Where<CodelessIAPButton>(func2).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
					{
						flag = true;
					}
					flag2 = true;
				}
			}
			using (List<IAPListener>.Enumerator enumerator3 = this.activeListeners.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.ProcessPurchase(e) == PurchaseProcessingResult.Complete)
					{
						flag = true;
					}
					flag2 = true;
				}
			}
			if (!flag2)
			{
				Debug.LogError("Purchase not correctly processed for product \"" + e.purchasedProduct.definition.id + "\". Add an active IAPButton to process this purchase, or add an IAPListener to receive any unhandled purchase events.");
			}
			if (!flag)
			{
				return PurchaseProcessingResult.Pending;
			}
			return PurchaseProcessingResult.Complete;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029A8 File Offset: 0x00000BA8
		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
		{
			bool flag = false;
			IEnumerable<IAPButton> enumerable = this.activeButtons;
			Func<IAPButton, bool> <>9__1;
			Func<IAPButton, bool> func;
			if ((func = <>9__1) == null)
			{
				func = (<>9__1 = (IAPButton button) => button.productId == product.definition.id);
			}
			foreach (IAPButton iapbutton in enumerable.Where<IAPButton>(func))
			{
				iapbutton.OnPurchaseFailed(product, reason);
				flag = true;
			}
			if (this.activeCodelessButtons.Exists((CodelessIAPButton button) => button.productId == product.definition.id))
			{
				flag = true;
			}
			foreach (IAPListener iaplistener in this.activeListeners)
			{
				iaplistener.OnPurchaseFailed(product, reason);
				flag = true;
			}
			if (!flag)
			{
				Debug.LogError("Failed purchase not correctly handled for product \"" + product.definition.id + "\". Add an active IAPButton to handle this failure, or add an IAPListener to receive any unhandled purchase failures.");
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void OnPurchaseFailed(Product product, PurchaseFailureDescription description)
		{
			this.OnPurchaseFailed(product, description.reason);
			IEnumerable<CodelessIAPButton> enumerable = this.activeCodelessButtons;
			Func<CodelessIAPButton, bool> <>9__0;
			Func<CodelessIAPButton, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (CodelessIAPButton button) => button.productId == product.definition.id);
			}
			foreach (CodelessIAPButton codelessIAPButton in enumerable.Where<CodelessIAPButton>(func))
			{
				codelessIAPButton.OnPurchaseFailed(product, description);
			}
			foreach (IAPListener iaplistener in this.activeListeners)
			{
				iaplistener.OnPurchaseFailed(product, description);
			}
		}

		// Token: 0x0400000C RID: 12
		private static CodelessIAPStoreListener instance;

		// Token: 0x0400000D RID: 13
		private readonly List<IAPButton> activeButtons = new List<IAPButton>();

		// Token: 0x0400000E RID: 14
		private readonly List<CodelessIAPButton> activeCodelessButtons = new List<CodelessIAPButton>();

		// Token: 0x0400000F RID: 15
		private readonly List<IAPListener> activeListeners = new List<IAPListener>();

		// Token: 0x04000010 RID: 16
		private static bool unityPurchasingInitialized;

		// Token: 0x04000011 RID: 17
		protected IStoreController controller;

		// Token: 0x04000012 RID: 18
		protected IExtensionProvider extensions;

		// Token: 0x04000013 RID: 19
		private ConfigurationBuilder m_Builder;

		// Token: 0x04000014 RID: 20
		protected ProductCatalog catalog;

		// Token: 0x04000015 RID: 21
		public static bool initializationComplete;
	}
}
