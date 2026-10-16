using System;

namespace Mono.Net.Security
{
	// Token: 0x02000096 RID: 150
	internal class AsyncShutdownRequest : AsyncProtocolRequest
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00006C35 File Offset: 0x00004E35
		public AsyncShutdownRequest(MobileAuthenticatedStream parent)
			: base(parent, false)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006C3F File Offset: 0x00004E3F
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessShutdown(status);
		}
	}
}
