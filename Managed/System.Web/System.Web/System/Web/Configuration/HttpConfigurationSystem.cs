using System;
using System.Configuration.Internal;

namespace System.Web.Configuration
{
	// Token: 0x020005A5 RID: 1445
	internal class HttpConfigurationSystem : IInternalConfigSystem
	{
		// Token: 0x06003DD0 RID: 15824 RVA: 0x0000F5B7 File Offset: 0x0000D7B7
		object IInternalConfigSystem.GetSection(string configKey)
		{
			return WebConfigurationManager.GetSection(configKey);
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x00003946 File Offset: 0x00001B46
		void IInternalConfigSystem.RefreshConfig(string sectionName)
		{
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x00008B4A File Offset: 0x00006D4A
		bool IInternalConfigSystem.SupportsUserConfig
		{
			get
			{
				return true;
			}
		}
	}
}
