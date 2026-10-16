using System;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x0200014B RID: 331
	internal class WebEncoding
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00028500 File Offset: 0x00026700
		private static GlobalizationSection GlobalizationConfig
		{
			get
			{
				if (!WebEncoding.cached)
				{
					try
					{
						WebEncoding.sect = (GlobalizationSection)WebConfigurationManager.GetWebApplicationSection("system.web/globalization");
					}
					catch
					{
					}
					WebEncoding.cached = true;
				}
				return WebEncoding.sect;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00028548 File Offset: 0x00026748
		public static Encoding FileEncoding
		{
			get
			{
				if (WebEncoding.GlobalizationConfig == null)
				{
					return Encoding.Default;
				}
				return WebEncoding.GlobalizationConfig.FileEncoding;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00028561 File Offset: 0x00026761
		public static Encoding ResponseEncoding
		{
			get
			{
				if (WebEncoding.GlobalizationConfig == null)
				{
					return Encoding.Default;
				}
				return WebEncoding.GlobalizationConfig.ResponseEncoding;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0002857A File Offset: 0x0002677A
		public static Encoding RequestEncoding
		{
			get
			{
				if (WebEncoding.GlobalizationConfig == null)
				{
					return Encoding.Default;
				}
				return WebEncoding.GlobalizationConfig.RequestEncoding;
			}
		}

		// Token: 0x04001219 RID: 4633
		private static bool cached;

		// Token: 0x0400121A RID: 4634
		private static GlobalizationSection sect;
	}
}
