using System;

namespace System.Net
{
	// Token: 0x02000551 RID: 1361
	internal static class GlobalSSPI
	{
		// Token: 0x040017E3 RID: 6115
		internal static readonly SSPIInterface SSPIAuth = new SSPIAuthType();

		// Token: 0x040017E4 RID: 6116
		internal static readonly SSPIInterface SSPISecureChannel = new SSPISecureChannelType();
	}
}
