using System;

namespace System.Web
{
	// Token: 0x020000BE RID: 190
	internal interface IHttpMapPath
	{
		// Token: 0x06000A8F RID: 2703
		string MapPath(string path);

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000A90 RID: 2704
		string MachineConfigPath { get; }
	}
}
