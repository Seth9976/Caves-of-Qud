using System;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020005DC RID: 1500
	internal interface IWebProxyFinder : IDisposable
	{
		// Token: 0x0600303A RID: 12346
		bool GetProxies(Uri destination, out IList<string> proxyList);

		// Token: 0x0600303B RID: 12347
		void Abort();

		// Token: 0x0600303C RID: 12348
		void Reset();

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600303D RID: 12349
		bool IsValid { get; }
	}
}
