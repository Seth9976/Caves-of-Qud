using System;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000085 RID: 133
	internal class MethodCallHeaderHandler
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public MethodCallHeaderHandler(string uri)
		{
			this._uri = uri;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000E9B3 File Offset: 0x0000CBB3
		public object HandleHeaders(Header[] headers)
		{
			return this._uri;
		}

		// Token: 0x040004C4 RID: 1220
		private string _uri;
	}
}
