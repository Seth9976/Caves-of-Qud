using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000093 RID: 147
	internal class UnixServerTransportSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x0000FFFF File Offset: 0x0000E1FF
		public UnixServerTransportSink(IServerChannelSink next)
		{
			this.next_sink = next;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001000E File Offset: 0x0000E20E
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return this.next_sink;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00010016 File Offset: 0x0000E216
		public IDictionary Properties
		{
			get
			{
				if (this.next_sink != null)
				{
					return this.next_sink.Properties;
				}
				return null;
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00010030 File Offset: 0x0000E230
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream responseStream)
		{
			ClientConnection clientConnection = (ClientConnection)state;
			NetworkStream networkStream = new NetworkStream(clientConnection.Client);
			UnixMessageIO.SendMessageStream(networkStream, responseStream, headers, clientConnection.Buffer);
			networkStream.Flush();
			networkStream.Close();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001006A File Offset: 0x0000E26A
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001006D File Offset: 0x0000E26D
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00010074 File Offset: 0x0000E274
		internal void InternalProcessMessage(ClientConnection connection, Stream stream)
		{
			ITransportHeaders transportHeaders;
			Stream stream2 = UnixMessageIO.ReceiveMessageStream(stream, out transportHeaders, connection.Buffer);
			ServerChannelSinkStack serverChannelSinkStack = new ServerChannelSinkStack();
			serverChannelSinkStack.Push(this, connection);
			IMessage message;
			ITransportHeaders transportHeaders2;
			Stream stream3;
			ServerProcessing serverProcessing = this.next_sink.ProcessMessage(serverChannelSinkStack, null, transportHeaders, stream2, out message, out transportHeaders2, out stream3);
			if (serverProcessing != ServerProcessing.Complete)
			{
				int num = serverProcessing - ServerProcessing.OneWay;
				return;
			}
			UnixMessageIO.SendMessageStream(stream, stream3, transportHeaders2, connection.Buffer);
			stream.Flush();
		}

		// Token: 0x040004F5 RID: 1269
		private IServerChannelSink next_sink;
	}
}
