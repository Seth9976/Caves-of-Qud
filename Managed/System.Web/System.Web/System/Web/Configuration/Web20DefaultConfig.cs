using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005ED RID: 1517
	internal class Web20DefaultConfig : IConfigurationSystem
	{
		// Token: 0x060041F3 RID: 16883 RVA: 0x000AA912 File Offset: 0x000A8B12
		public static Web20DefaultConfig GetInstance()
		{
			return Web20DefaultConfig.instance;
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000AA91C File Offset: 0x000A8B1C
		public object GetConfig(string sectionName)
		{
			object webApplicationSection = WebConfigurationManager.GetWebApplicationSection(sectionName);
			if (webApplicationSection == null || webApplicationSection is IgnoreSection)
			{
				object config = WebConfigurationManager.oldConfig.GetConfig(sectionName);
				if (config != null)
				{
					return config;
				}
			}
			return webApplicationSection;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x00003946 File Offset: 0x00001B46
		public void Init()
		{
		}

		// Token: 0x0400235E RID: 9054
		private static Web20DefaultConfig instance = new Web20DefaultConfig();
	}
}
