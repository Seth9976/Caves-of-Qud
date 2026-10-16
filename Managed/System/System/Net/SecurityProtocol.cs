using System;
using System.Security.Authentication;

namespace System.Net
{
	// Token: 0x0200056D RID: 1389
	internal static class SecurityProtocol
	{
		// Token: 0x04001860 RID: 6240
		public const SslProtocols DefaultSecurityProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

		// Token: 0x04001861 RID: 6241
		public const SslProtocols SystemDefaultSecurityProtocols = SslProtocols.None;
	}
}
