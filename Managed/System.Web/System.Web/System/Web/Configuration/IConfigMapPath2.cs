using System;

namespace System.Web.Configuration
{
	// Token: 0x02000563 RID: 1379
	internal interface IConfigMapPath2
	{
		// Token: 0x06003B18 RID: 15128
		void GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName);

		// Token: 0x06003B19 RID: 15129
		string MapPath(string siteID, VirtualPath path);

		// Token: 0x06003B1A RID: 15130
		VirtualPath GetAppPathForPath(string siteID, VirtualPath path);
	}
}
