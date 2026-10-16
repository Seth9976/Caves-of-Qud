using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000086 RID: 134
	internal class UnixBinaryServerFormatterSinkProvider : IServerFormatterSinkProvider, IServerChannelSinkProvider
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0000E9BB File Offset: 0x0000CBBB
		public UnixBinaryServerFormatterSinkProvider()
		{
			this._binaryCore = UnixBinaryCore.DefaultInstance;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000E9CE File Offset: 0x0000CBCE
		public UnixBinaryServerFormatterSinkProvider(IDictionary properties, ICollection providerData)
		{
			this._binaryCore = new UnixBinaryCore(this, properties, UnixBinaryServerFormatterSinkProvider.AllowedProperties);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public IServerChannelSinkProvider Next
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

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			IServerChannelSink serverChannelSink = null;
			if (this.next != null)
			{
				serverChannelSink = this.next.CreateSink(channel);
			}
			return new UnixBinaryServerFormatterSink(serverChannelSink, channel)
			{
				BinaryCore = this._binaryCore
			};
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000EA33 File Offset: 0x0000CC33
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x040004C5 RID: 1221
		private IServerChannelSinkProvider next;

		// Token: 0x040004C6 RID: 1222
		private UnixBinaryCore _binaryCore;

		// Token: 0x040004C7 RID: 1223
		internal static string[] AllowedProperties = new string[] { "includeVersions", "strictBinding" };
	}
}
