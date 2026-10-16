using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000080 RID: 128
	internal class UnixBinaryClientFormatterSink : IClientFormatterSink, IMessageSink, IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x0000E32F File Offset: 0x0000C52F
		public UnixBinaryClientFormatterSink(IClientChannelSink nextSink)
		{
			this._nextInChain = nextSink;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000E349 File Offset: 0x0000C549
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0000E351 File Offset: 0x0000C551
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000E35A File Offset: 0x0000C55A
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return this._nextInChain;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000E362 File Offset: 0x0000C562
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000E365 File Offset: 0x0000C565
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000E368 File Offset: 0x0000C568
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException("UnixBinaryClientFormatterSink must be the first sink in the IClientChannelSink chain");
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000E374 File Offset: 0x0000C574
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			IMessage message = (IMessage)this._binaryCore.Deserializer.DeserializeMethodResponse(stream, null, (IMethodCallMessage)state);
			sinkStack.DispatchReplyMessage(message);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000E3AE File Offset: 0x0000C5AE
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ITransportHeaders transportHeaders = new TransportHeaders();
			Stream stream = this._nextInChain.GetRequestStream(msg, transportHeaders);
			if (stream == null)
			{
				stream = new MemoryStream();
			}
			this._binaryCore.Serializer.Serialize(stream, msg, null);
			if (stream is MemoryStream)
			{
				stream.Position = 0L;
			}
			ClientChannelSinkStack clientChannelSinkStack = new ClientChannelSinkStack(replySink);
			clientChannelSinkStack.Push(this, msg);
			this._nextInChain.AsyncProcessRequest(clientChannelSinkStack, msg, transportHeaders, stream);
			return null;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000E424 File Offset: 0x0000C624
		public IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message;
			try
			{
				ITransportHeaders transportHeaders = new TransportHeaders();
				transportHeaders["__RequestUri"] = ((IMethodCallMessage)msg).Uri;
				transportHeaders["Content-Type"] = "application/octet-stream";
				Stream stream = this._nextInChain.GetRequestStream(msg, transportHeaders);
				if (stream == null)
				{
					stream = new MemoryStream();
				}
				this._binaryCore.Serializer.Serialize(stream, msg, null);
				if (stream is MemoryStream)
				{
					stream.Position = 0L;
				}
				ITransportHeaders transportHeaders2;
				Stream stream2;
				this._nextInChain.ProcessMessage(msg, transportHeaders, stream, out transportHeaders2, out stream2);
				message = (IMessage)this._binaryCore.Deserializer.DeserializeMethodResponse(stream2, null, (IMethodCallMessage)msg);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)msg);
			}
			return message;
		}

		// Token: 0x040004B5 RID: 1205
		private UnixBinaryCore _binaryCore = UnixBinaryCore.DefaultInstance;

		// Token: 0x040004B6 RID: 1206
		private IClientChannelSink _nextInChain;
	}
}
