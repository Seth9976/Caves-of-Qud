using System;
using UnityEngine.Purchasing.Models;

namespace UnityEngine.Purchasing.Interfaces
{
	// Token: 0x0200009C RID: 156
	internal interface IGoogleFinishTransactionService
	{
		// Token: 0x0600038A RID: 906
		void FinishTransaction(ProductDefinition product, string purchaseToken, Action<IGoogleBillingResult, IGooglePurchase> onTransactionFinished);
	}
}
