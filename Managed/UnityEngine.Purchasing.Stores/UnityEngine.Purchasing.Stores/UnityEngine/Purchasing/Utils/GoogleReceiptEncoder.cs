using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing.Utils
{
	// Token: 0x02000098 RID: 152
	internal static class GoogleReceiptEncoder
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0000B5FC File Offset: 0x000097FC
		internal static string EncodeReceipt(string purchaseOriginalJson, string purchaseSignature, List<string> productDetailsJson)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["json"] = purchaseOriginalJson;
			dictionary["signature"] = purchaseSignature;
			dictionary["skuDetails"] = productDetailsJson;
			return MiniJson.JsonEncode(dictionary);
		}
	}
}
