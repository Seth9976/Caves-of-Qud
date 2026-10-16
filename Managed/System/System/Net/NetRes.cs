using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000634 RID: 1588
	internal class NetRes
	{
		// Token: 0x06003224 RID: 12836 RVA: 0x0000219B File Offset: 0x0000039B
		private NetRes()
		{
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000AD71C File Offset: 0x000AB91C
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, @string);
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000AD748 File Offset: 0x000AB948
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x000AD758 File Offset: 0x000AB958
		public static string GetWebStatusCodeString(HttpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = SR.GetString("net_httpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000AD7F0 File Offset: 0x000AB9F0
		public static string GetWebStatusCodeString(FtpStatusCode statusCode, string statusDescription)
		{
			string text = "(";
			int num = (int)statusCode;
			string text2 = text + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text3 = null;
			try
			{
				text3 = SR.GetString("net_ftpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text3 != null && text3.Length > 0)
			{
				text2 = text2 + " " + text3;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text2 = text2 + " " + statusDescription;
			}
			return text2;
		}
	}
}
