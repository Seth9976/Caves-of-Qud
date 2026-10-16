using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000092 RID: 146
	internal class ClientConnection
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		public ClientConnection(UnixServerChannel serverChannel, Socket client, UnixServerTransportSink sink)
		{
			this._serverChannel = serverChannel;
			this._client = client;
			this._sink = sink;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0000FF29 File Offset: 0x0000E129
		public Socket Client
		{
			get
			{
				return this._client;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000FF31 File Offset: 0x0000E131
		public byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000FF3C File Offset: 0x0000E13C
		public void ProcessMessages()
		{
			byte[] array = new byte[256];
			this._stream = new BufferedStream(new NetworkStream(this._client));
			try
			{
				bool flag = false;
				while (!flag)
				{
					MessageStatus messageStatus = UnixMessageIO.ReceiveMessageStatus(this._stream, array);
					if (messageStatus != MessageStatus.MethodMessage)
					{
						if (messageStatus == MessageStatus.CancelSignal || messageStatus == MessageStatus.Unknown)
						{
							flag = true;
						}
					}
					else
					{
						this._sink.InternalProcessMessage(this, this._stream);
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					this._serverChannel.ReleaseConnection(Thread.CurrentThread);
					this._stream.Close();
					this._client.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		public bool IsLocal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040004F0 RID: 1264
		private Socket _client;

		// Token: 0x040004F1 RID: 1265
		private UnixServerTransportSink _sink;

		// Token: 0x040004F2 RID: 1266
		private Stream _stream;

		// Token: 0x040004F3 RID: 1267
		private UnixServerChannel _serverChannel;

		// Token: 0x040004F4 RID: 1268
		private byte[] _buffer = new byte[UnixMessageIO.DefaultStreamBufferSize];
	}
}
