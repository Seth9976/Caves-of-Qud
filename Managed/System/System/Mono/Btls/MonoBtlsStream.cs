using System;
using System.IO;
using System.Net.Security;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Btls
{
	// Token: 0x020000F2 RID: 242
	internal class MonoBtlsStream : MobileAuthenticatedStream
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x000052F4 File Offset: 0x000034F4
		public MonoBtlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000F601 File Offset: 0x0000D801
		protected override MobileTlsContext CreateContext(MonoSslAuthenticationOptions options)
		{
			return new MonoBtlsContext(this, options);
		}
	}
}
