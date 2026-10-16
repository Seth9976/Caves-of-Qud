using System;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200008A RID: 138
	internal class UnixClientTransportSinkProvider : IClientChannelSinkProvider
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0000F00C File Offset: 0x0000D20C
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0000F00F File Offset: 0x0000D20F
		public IClientChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000F011 File Offset: 0x0000D211
		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			return new UnixClientTransportSink(url);
		}
	}
}
