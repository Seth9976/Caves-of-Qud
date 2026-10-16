using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000084 RID: 132
	internal class UnixBinaryServerFormatterSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public UnixBinaryServerFormatterSink(IServerChannelSink nextSink, IChannelReceiver receiver)
		{
			this.next_sink = nextSink;
			this.receiver = receiver;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000E7D9 File Offset: 0x0000C9D9
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000E7E1 File Offset: 0x0000C9E1
		internal UnixBinaryCore BinaryCore
		{
			get
			{
				return this._binaryCore;
			}
			set
			{
				this._binaryCore = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000E7EA File Offset: 0x0000C9EA
		public IServerChannelSink NextChannelSink
		{
			get
			{
				return this.next_sink;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000E7F2 File Offset: 0x0000C9F2
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage message, ITransportHeaders headers, Stream stream)
		{
			ITransportHeaders transportHeaders = new TransportHeaders();
			if (sinkStack != null)
			{
				stream = sinkStack.GetResponseStream(message, transportHeaders);
			}
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			this._binaryCore.Serializer.Serialize(stream, message, null);
			if (stream is MemoryStream)
			{
				stream.Position = 0L;
			}
			sinkStack.AsyncProcessResponse(message, transportHeaders, stream);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000E853 File Offset: 0x0000CA53
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000E858 File Offset: 0x0000CA58
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			sinkStack.Push(this, null);
			ServerProcessing serverProcessing;
			try
			{
				string text = (string)requestHeaders["__RequestUri"];
				string text2;
				this.receiver.Parse(text, out text2);
				if (text2 == null)
				{
					text2 = text;
				}
				MethodCallHeaderHandler methodCallHeaderHandler = new MethodCallHeaderHandler(text2);
				requestMsg = (IMessage)this._binaryCore.Deserializer.Deserialize(requestStream, new HeaderHandler(methodCallHeaderHandler.HandleHeaders));
				serverProcessing = this.next_sink.ProcessMessage(sinkStack, requestMsg, requestHeaders, null, out responseMsg, out responseHeaders, out responseStream);
			}
			catch (Exception ex)
			{
				responseMsg = new ReturnMessage(ex, (IMethodCallMessage)requestMsg);
				serverProcessing = ServerProcessing.Complete;
				responseHeaders = null;
				responseStream = null;
			}
			if (serverProcessing == ServerProcessing.Complete)
			{
				for (int i = 0; i < 3; i++)
				{
					responseStream = null;
					responseHeaders = new TransportHeaders();
					if (sinkStack != null)
					{
						responseStream = sinkStack.GetResponseStream(responseMsg, responseHeaders);
					}
					if (responseStream == null)
					{
						responseStream = new MemoryStream();
					}
					try
					{
						this._binaryCore.Serializer.Serialize(responseStream, responseMsg);
						break;
					}
					catch (Exception ex2)
					{
						if (i == 2)
						{
							throw ex2;
						}
						responseMsg = new ReturnMessage(ex2, (IMethodCallMessage)requestMsg);
					}
				}
				if (responseStream is MemoryStream)
				{
					responseStream.Position = 0L;
				}
				sinkStack.Pop(this);
			}
			return serverProcessing;
		}

		// Token: 0x040004C1 RID: 1217
		private UnixBinaryCore _binaryCore = UnixBinaryCore.DefaultInstance;

		// Token: 0x040004C2 RID: 1218
		private IServerChannelSink next_sink;

		// Token: 0x040004C3 RID: 1219
		private IChannelReceiver receiver;
	}
}
