using System;
using System.IO;
using System.Net.Security;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Unity
{
	// Token: 0x0200007D RID: 125
	internal class UnityTlsStream : MobileAuthenticatedStream
	{
		// Token: 0x060001CE RID: 462 RVA: 0x000052F4 File Offset: 0x000034F4
		public UnityTlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005303 File Offset: 0x00003503
		protected override MobileTlsContext CreateContext(MonoSslAuthenticationOptions options)
		{
			return new UnityTlsContext(this, options);
		}
	}
}
