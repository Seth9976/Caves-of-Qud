using System;

namespace Mono.Net.Security
{
	// Token: 0x02000092 RID: 146
	internal class AsyncHandshakeRequest : AsyncProtocolRequest
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00006A9E File Offset: 0x00004C9E
		public AsyncHandshakeRequest(MobileAuthenticatedStream parent, bool sync)
			: base(parent, sync)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006AA8 File Offset: 0x00004CA8
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			return base.Parent.ProcessHandshake(status, false);
		}
	}
}
