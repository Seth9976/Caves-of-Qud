using System;

namespace Mono.Net.Security
{
	// Token: 0x02000097 RID: 151
	internal class AsyncRenegotiateRequest : AsyncProtocolRequest
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00006C35 File Offset: 0x00004E35
		public AsyncRenegotiateRequest(MobileAuthenticatedStream parent)
			: base(parent, false)
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006C4D File Offset: 0x00004E4D
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, true);
		}
	}
}
