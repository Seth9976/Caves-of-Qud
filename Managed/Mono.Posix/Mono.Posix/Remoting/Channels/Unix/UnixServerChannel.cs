using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Mono.Unix;
using Mono.Unix.Native;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000091 RID: 145
	public class UnixServerChannel : IChannelReceiver, IChannel
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		private void Init(IServerChannelSinkProvider serverSinkProvider)
		{
			if (serverSinkProvider == null)
			{
				serverSinkProvider = new UnixBinaryServerFormatterSinkProvider();
			}
			this.channel_data = new ChannelDataStore(null);
			for (IServerChannelSinkProvider serverChannelSinkProvider = serverSinkProvider; serverChannelSinkProvider != null; serverChannelSinkProvider = serverChannelSinkProvider.Next)
			{
				serverChannelSinkProvider.GetChannelData(this.channel_data);
			}
			IServerChannelSink serverChannelSink = ChannelServices.CreateServerChannelSinkChain(serverSinkProvider, this);
			this.sink = new UnixServerTransportSink(serverChannelSink);
			this.StartListening(null);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public UnixServerChannel(string path)
		{
			this.path = path;
			this.Init(null);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public UnixServerChannel(IDictionary properties, IServerChannelSinkProvider serverSinkProvider)
		{
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (!(text == "path"))
				{
					if (!(text == "priority"))
					{
						if (text == "supressChannelData")
						{
							this.supressChannelData = Convert.ToBoolean(dictionaryEntry.Value);
						}
					}
					else
					{
						this.priority = Convert.ToInt32(dictionaryEntry.Value);
					}
				}
				else
				{
					this.path = dictionaryEntry.Value as string;
				}
			}
			this.Init(serverSinkProvider);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0000FB48 File Offset: 0x0000DD48
		public UnixServerChannel(string name, string path, IServerChannelSinkProvider serverSinkProvider)
		{
			this.name = name;
			this.path = path;
			this.Init(serverSinkProvider);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public UnixServerChannel(string name, string path)
		{
			this.name = name;
			this.path = path;
			this.Init(null);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
		public object ChannelData
		{
			get
			{
				if (this.supressChannelData)
				{
					return null;
				}
				return this.channel_data;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000FBF7 File Offset: 0x0000DDF7
		public string ChannelName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0000FBFF File Offset: 0x0000DDFF
		public int ChannelPriority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000FC07 File Offset: 0x0000DE07
		public string GetChannelUri()
		{
			return "unix://" + this.path;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		public string[] GetUrlsForUri(string uri)
		{
			if (!uri.StartsWith("/"))
			{
				uri = "/" + uri;
			}
			string[] channelUris = this.channel_data.ChannelUris;
			string[] array = new string[channelUris.Length];
			for (int i = 0; i < channelUris.Length; i++)
			{
				array[i] = channelUris[i] + "?" + uri;
			}
			return array;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000FC78 File Offset: 0x0000DE78
		public string Parse(string url, out string objectURI)
		{
			return UnixChannel.ParseUnixURL(url, out objectURI);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000FC84 File Offset: 0x0000DE84
		private void WaitForConnections()
		{
			try
			{
				for (;;)
				{
					Socket socket = this.listener.AcceptSocket();
					this.CreateListenerConnection(socket);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		internal void CreateListenerConnection(Socket client)
		{
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				if (this._activeConnections.Count >= this._maxConcurrentConnections)
				{
					Monitor.Wait(this._activeConnections);
				}
				if (this.server_thread != null)
				{
					Thread thread = new Thread(new ThreadStart(new ClientConnection(this, client, this.sink).ProcessMessages));
					thread.Start();
					thread.IsBackground = true;
					this._activeConnections.Add(thread);
				}
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000FD58 File Offset: 0x0000DF58
		internal void ReleaseConnection(Thread thread)
		{
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				this._activeConnections.Remove(thread);
				Monitor.Pulse(this._activeConnections);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		public void StartListening(object data)
		{
			this.listener = new UnixListener(this.path);
			Syscall.chmod(this.path, FilePermissions.DEFFILEMODE);
			if (this.server_thread == null)
			{
				this.listener.Start();
				string[] array = new string[1];
				array = new string[] { this.GetChannelUri() };
				this.channel_data.ChannelUris = array;
				this.server_thread = new Thread(new ThreadStart(this.WaitForConnections));
				this.server_thread.IsBackground = true;
				this.server_thread.Start();
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000FE40 File Offset: 0x0000E040
		public void StopListening(object data)
		{
			if (this.server_thread == null)
			{
				return;
			}
			ArrayList activeConnections = this._activeConnections;
			lock (activeConnections)
			{
				this.server_thread.Abort();
				this.server_thread = null;
				this.listener.Stop();
				foreach (object obj in this._activeConnections)
				{
					((Thread)obj).Abort();
				}
				this._activeConnections.Clear();
				Monitor.PulseAll(this._activeConnections);
			}
		}

		// Token: 0x040004E6 RID: 1254
		private string path;

		// Token: 0x040004E7 RID: 1255
		private string name = "unix";

		// Token: 0x040004E8 RID: 1256
		private int priority = 1;

		// Token: 0x040004E9 RID: 1257
		private bool supressChannelData;

		// Token: 0x040004EA RID: 1258
		private Thread server_thread;

		// Token: 0x040004EB RID: 1259
		private UnixListener listener;

		// Token: 0x040004EC RID: 1260
		private UnixServerTransportSink sink;

		// Token: 0x040004ED RID: 1261
		private ChannelDataStore channel_data;

		// Token: 0x040004EE RID: 1262
		private int _maxConcurrentConnections = 100;

		// Token: 0x040004EF RID: 1263
		private ArrayList _activeConnections = new ArrayList();
	}
}
