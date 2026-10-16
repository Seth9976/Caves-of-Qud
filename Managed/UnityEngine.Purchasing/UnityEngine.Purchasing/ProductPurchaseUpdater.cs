using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000030 RID: 48
	internal static class ProductPurchaseUpdater
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00003A87 File Offset: 0x00001C87
		internal static void UpdateProductReceiptAndTransactionID(Product product, string receipt, string transactionId, string storeName)
		{
			product.receipt = UnifiedReceiptFormatter.FormatUnifiedReceipt(receipt, transactionId, storeName);
			product.transactionID = transactionId;
		}
	}
}
