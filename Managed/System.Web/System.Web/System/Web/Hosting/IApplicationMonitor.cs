using System;

namespace System.Web.Hosting
{
	// Token: 0x0200069E RID: 1694
	public interface IApplicationMonitor : IDisposable
	{
		// Token: 0x060047BE RID: 18366
		void Start();

		// Token: 0x060047BF RID: 18367
		void Stop();
	}
}
