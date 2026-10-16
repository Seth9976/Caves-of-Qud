using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000031 RID: 49
	internal static class UnifiedReceiptFormatter
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00003A9E File Offset: 0x00001C9E
		internal static string FormatUnifiedReceipt(string platformReceipt, string transactionId, string storeName)
		{
			return JsonUtility.ToJson(new UnifiedReceipt
			{
				Store = storeName,
				TransactionID = transactionId,
				Payload = platformReceipt
			});
		}
	}
}
