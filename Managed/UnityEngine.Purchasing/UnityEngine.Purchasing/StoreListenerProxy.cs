using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class StoreListenerProxy : IInternalStoreListener
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00003689 File Offset: 0x00001889
		public StoreListenerProxy(IStoreListener forwardTo, IAnalyticsClient analytics, IExtensionProvider extensions)
		{
			this.m_ForwardTo = forwardTo;
			this.m_Analytics = analytics;
			this.m_Extensions = extensions;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000036A6 File Offset: 0x000018A6
		public void OnInitialized(IStoreController controller)
		{
			this.m_ForwardTo.OnInitialized(controller, this.m_Extensions);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000036BA File Offset: 0x000018BA
		[NullableContext(2)]
		public void OnInitializeFailed(InitializationFailureReason error, string message)
		{
			this.m_ForwardTo.OnInitializeFailed(error, message);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000036C9 File Offset: 0x000018C9
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
		{
			return this.m_ForwardTo.ProcessPurchase(e);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000036D8 File Offset: 0x000018D8
		public void OnPurchaseFailed(Product i, PurchaseFailureDescription p)
		{
			this.m_Analytics.OnPurchaseFailed(i, p);
			IDetailedStoreListener detailedStoreListener = this.m_ForwardTo as IDetailedStoreListener;
			if (detailedStoreListener != null)
			{
				detailedStoreListener.OnPurchaseFailed(i, p);
				return;
			}
			this.m_ForwardTo.OnPurchaseFailed(i, p.reason);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000371C File Offset: 0x0000191C
		public void SendTransactionEvent(Product product)
		{
			this.m_Analytics.OnPurchaseSucceeded(product);
		}

		// Token: 0x04000062 RID: 98
		private readonly IAnalyticsClient m_Analytics;

		// Token: 0x04000063 RID: 99
		private readonly IStoreListener m_ForwardTo;

		// Token: 0x04000064 RID: 100
		private readonly IExtensionProvider m_Extensions;
	}
}
