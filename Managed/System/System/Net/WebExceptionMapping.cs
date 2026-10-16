using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200060B RID: 1547
	internal static class WebExceptionMapping
	{
		// Token: 0x060030C3 RID: 12483 RVA: 0x000A7818 File Offset: 0x000A5A18
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			int num = (int)status;
			if (num >= WebExceptionMapping.s_Mapping.Length || num < 0)
			{
				throw new InternalException();
			}
			string text = Volatile.Read<string>(ref WebExceptionMapping.s_Mapping[num]);
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				Volatile.Write<string>(ref WebExceptionMapping.s_Mapping[num], text);
			}
			return text;
		}

		// Token: 0x04001C80 RID: 7296
		private static readonly string[] s_Mapping = new string[21];
	}
}
