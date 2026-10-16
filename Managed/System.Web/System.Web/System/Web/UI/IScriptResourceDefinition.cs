using System;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x0200017A RID: 378
	internal interface IScriptResourceDefinition
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000F47 RID: 3911
		string Path { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000F48 RID: 3912
		string DebugPath { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000F49 RID: 3913
		string CdnPath { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000F4A RID: 3914
		string CdnDebugPath { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000F4B RID: 3915
		string CdnPathSecureConnection { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000F4C RID: 3916
		string CdnDebugPathSecureConnection { get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000F4D RID: 3917
		string ResourceName { get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000F4E RID: 3918
		Assembly ResourceAssembly { get; }
	}
}
