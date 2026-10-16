using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	internal interface IInternalStoreListener
	{
		// Token: 0x0600005A RID: 90
		[NullableContext(2)]
		void OnInitializeFailed(InitializationFailureReason error, string message = null);

		// Token: 0x0600005B RID: 91
		PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e);

		// Token: 0x0600005C RID: 92
		void OnPurchaseFailed(Product i, PurchaseFailureDescription p);

		// Token: 0x0600005D RID: 93
		void OnInitialized(IStoreController controller);

		// Token: 0x0600005E RID: 94
		void SendTransactionEvent(Product product);
	}
}
