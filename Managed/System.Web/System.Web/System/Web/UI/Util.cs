using System;

namespace System.Web.UI
{
	// Token: 0x0200023F RID: 575
	internal static class Util
	{
		// Token: 0x060017B1 RID: 6065 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		internal static string GetUrlWithApplicationPath(HttpContextBase context, string url)
		{
			string text = context.Request.ApplicationPath ?? string.Empty;
			if (!text.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				text += "/";
			}
			return context.Response.ApplyAppPathModifier(text + url);
		}
	}
}
