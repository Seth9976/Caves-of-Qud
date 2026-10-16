using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200057B RID: 1403
	internal class ApplicationSettingsConfigurationFileMap : ConfigurationFileMap
	{
		// Token: 0x06003B40 RID: 15168 RVA: 0x0009CB40 File Offset: 0x0009AD40
		public ApplicationSettingsConfigurationFileMap()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest != null)
			{
				base.MachineConfigFilename = WebConfigurationHost.GetWebConfigFileName(httpRequest.MapPath(WebConfigurationManager.FindWebConfig(httpRequest.CurrentExecutionFilePath)));
				return;
			}
			base.MachineConfigFilename = null;
		}
	}
}
