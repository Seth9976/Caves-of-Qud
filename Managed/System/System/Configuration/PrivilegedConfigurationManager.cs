using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000199 RID: 409
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002DC0F File Offset: 0x0002BE0F
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002DC16 File Offset: 0x0002BE16
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
