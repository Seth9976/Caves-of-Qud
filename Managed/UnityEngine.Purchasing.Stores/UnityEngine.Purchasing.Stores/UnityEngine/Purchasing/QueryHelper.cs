using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200006F RID: 111
	internal static class QueryHelper
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00008684 File Offset: 0x00006884
		internal static string ToQueryString(this Dictionary<string, object> parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in parameters.Keys)
			{
				string text2 = parameters[text].ToString();
				if (text2 != null)
				{
					stringBuilder.Append((stringBuilder.Length == 0) ? "?" : "&");
					stringBuilder.AppendFormat("{0}={1}", Uri.EscapeDataString(text), Uri.EscapeDataString(text2));
				}
			}
			return stringBuilder.ToString();
		}
	}
}
