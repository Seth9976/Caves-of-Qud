using System;

namespace System.Web.Util
{
	// Token: 0x0200011C RID: 284
	internal interface ISyncContext
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000DF9 RID: 3577
		HttpContext HttpContext { get; }

		// Token: 0x06000DFA RID: 3578
		ISyncContextLock Enter();
	}
}
