using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x02000232 RID: 562
	internal class ServerSideScript
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x0003BDF4 File Offset: 0x00039FF4
		public ServerSideScript(string script, ILocation location)
		{
			this.Script = script;
			this.Location = location;
		}

		// Token: 0x040015A2 RID: 5538
		public readonly string Script;

		// Token: 0x040015A3 RID: 5539
		public readonly ILocation Location;
	}
}
