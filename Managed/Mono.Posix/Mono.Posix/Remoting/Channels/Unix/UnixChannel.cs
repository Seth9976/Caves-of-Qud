using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000087 RID: 135
	public class UnixChannel : IChannelReceiver, IChannel, IChannelSender
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0000EA52 File Offset: 0x0000CC52
		public UnixChannel()
			: this(null)
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		public UnixChannel(string path)
		{
			this._name = "unix";
			this._priority = 1;
			base..ctor();
			Hashtable hashtable = new Hashtable();
			hashtable["path"] = path;
			this.Init(hashtable, null, null);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		private void Init(IDictionary properties, IClientChannelSinkProvider clientSink, IServerChannelSinkProvider serverSink)
		{
			this._clientChannel = new UnixClientChannel(properties, clientSink);
			if (properties["path"] != null)
			{
				this._serverChannel = new UnixServerChannel(properties, serverSink);
			}
			object obj = properties["name"];
			if (obj != null)
			{
				this._name = obj as string;
			}
			obj = properties["priority"];
			if (obj != null)
			{
				this._priority = Convert.ToInt32(obj);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000EB06 File Offset: 0x0000CD06
		public UnixChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider, IServerChannelSinkProvider serverSinkProvider)
		{
			this._name = "unix";
			this._priority = 1;
			base..ctor();
			this.Init(properties, clientSinkProvider, serverSinkProvider);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000EB29 File Offset: 0x0000CD29
		public IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI)
		{
			return this._clientChannel.CreateMessageSink(url, remoteChannelData, out objectURI);
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000EB39 File Offset: 0x0000CD39
		public string ChannelName
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000EB41 File Offset: 0x0000CD41
		public int ChannelPriority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000EB49 File Offset: 0x0000CD49
		public void StartListening(object data)
		{
			if (this._serverChannel != null)
			{
				this._serverChannel.StartListening(data);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000EB5F File Offset: 0x0000CD5F
		public void StopListening(object data)
		{
			if (this._serverChannel != null)
			{
				this._serverChannel.StopListening(data);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public string[] GetUrlsForUri(string uri)
		{
			if (this._serverChannel != null)
			{
				return this._serverChannel.GetUrlsForUri(uri);
			}
			return null;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000EB8D File Offset: 0x0000CD8D
		public object ChannelData
		{
			get
			{
				if (this._serverChannel != null)
				{
					return this._serverChannel.ChannelData;
				}
				return null;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		internal static string ParseUnixURL(string url, out string objectURI)
		{
			objectURI = null;
			if (!url.StartsWith("unix://"))
			{
				return null;
			}
			int num = url.IndexOf('?');
			if (num == -1)
			{
				return url.Substring(7);
			}
			objectURI = url.Substring(num + 1);
			if (objectURI.Length == 0)
			{
				objectURI = null;
			}
			return url.Substring(7, num - 7);
		}

		// Token: 0x040004C8 RID: 1224
		private UnixClientChannel _clientChannel;

		// Token: 0x040004C9 RID: 1225
		private UnixServerChannel _serverChannel;

		// Token: 0x040004CA RID: 1226
		private string _name;

		// Token: 0x040004CB RID: 1227
		private int _priority;
	}
}
