using System;
using System.Web.SessionState;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200008B RID: 139
	internal class SyncSessionHandler : SyncSessionlessHandler, IRequiresSessionState
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00011C5D File Offset: 0x0000FE5D
		internal SyncSessionHandler(ServerProtocol protocol)
			: base(protocol)
		{
		}
	}
}
