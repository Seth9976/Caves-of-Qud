using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000081 RID: 129
	internal class UnixBinaryClientFormatterSinkProvider : IClientFormatterSinkProvider, IClientChannelSinkProvider
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public UnixBinaryClientFormatterSinkProvider()
		{
			this._binaryCore = UnixBinaryCore.DefaultInstance;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000E4FF File Offset: 0x0000C6FF
		public UnixBinaryClientFormatterSinkProvider(IDictionary properties, ICollection providerData)
		{
			this._binaryCore = new UnixBinaryCore(this, properties, UnixBinaryClientFormatterSinkProvider.allowedProperties);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000E519 File Offset: 0x0000C719
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0000E521 File Offset: 0x0000C721
		public IClientChannelSinkProvider Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000E52C File Offset: 0x0000C72C
		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			IClientChannelSink clientChannelSink = null;
			if (this.next != null)
			{
				clientChannelSink = this.next.CreateSink(channel, url, remoteChannelData);
			}
			return new UnixBinaryClientFormatterSink(clientChannelSink)
			{
				BinaryCore = this._binaryCore
			};
		}

		// Token: 0x040004B7 RID: 1207
		private IClientChannelSinkProvider next;

		// Token: 0x040004B8 RID: 1208
		private UnixBinaryCore _binaryCore;

		// Token: 0x040004B9 RID: 1209
		private static string[] allowedProperties = new string[] { "includeVersions", "strictBinding" };
	}
}
