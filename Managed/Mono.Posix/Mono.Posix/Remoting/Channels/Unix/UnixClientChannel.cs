using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000088 RID: 136
	public class UnixClientChannel : IChannelSender, IChannel
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0000EC05 File Offset: 0x0000CE05
		public UnixClientChannel()
		{
			this._sinkProvider = new UnixBinaryClientFormatterSinkProvider();
			this._sinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public UnixClientChannel(IDictionary properties, IClientChannelSinkProvider sinkProvider)
		{
			object obj = properties["name"];
			if (obj != null)
			{
				this.name = obj as string;
			}
			obj = properties["priority"];
			if (obj != null)
			{
				this.priority = Convert.ToInt32(obj);
			}
			if (sinkProvider != null)
			{
				this._sinkProvider = sinkProvider;
				IClientChannelSinkProvider clientChannelSinkProvider = sinkProvider;
				while (clientChannelSinkProvider.Next != null)
				{
					clientChannelSinkProvider = clientChannelSinkProvider.Next;
				}
				clientChannelSinkProvider.Next = new UnixClientTransportSinkProvider();
				return;
			}
			this._sinkProvider = new UnixBinaryClientFormatterSinkProvider();
			this._sinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		public UnixClientChannel(string name, IClientChannelSinkProvider sinkProvider)
		{
			this.name = name;
			this._sinkProvider = sinkProvider;
			IClientChannelSinkProvider clientChannelSinkProvider = sinkProvider;
			while (clientChannelSinkProvider.Next != null)
			{
				clientChannelSinkProvider = clientChannelSinkProvider.Next;
			}
			clientChannelSinkProvider.Next = new UnixClientTransportSinkProvider();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000ED2D File Offset: 0x0000CF2D
		public string ChannelName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000ED35 File Offset: 0x0000CF35
		public int ChannelPriority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI)
		{
			if (url != null && this.Parse(url, out objectURI) != null)
			{
				return (IMessageSink)this._sinkProvider.CreateSink(this, url, remoteChannelData);
			}
			if (remoteChannelData != null)
			{
				IChannelDataStore channelDataStore = remoteChannelData as IChannelDataStore;
				if (channelDataStore == null || channelDataStore.ChannelUris.Length == 0)
				{
					objectURI = null;
					return null;
				}
				url = channelDataStore.ChannelUris[0];
			}
			if (this.Parse(url, out objectURI) == null)
			{
				return null;
			}
			return (IMessageSink)this._sinkProvider.CreateSink(this, url, remoteChannelData);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x040004CC RID: 1228
		private int priority = 1;

		// Token: 0x040004CD RID: 1229
		private string name = "unix";

		// Token: 0x040004CE RID: 1230
		private IClientChannelSinkProvider _sinkProvider;
	}
}
