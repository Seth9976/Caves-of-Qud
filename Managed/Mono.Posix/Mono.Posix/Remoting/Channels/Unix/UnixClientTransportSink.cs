using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000089 RID: 137
	internal class UnixClientTransportSink : IClientChannelSink, IChannelSinkBase
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		public UnixClientTransportSink(string url)
		{
			string text;
			this._path = UnixChannel.ParseUnixURL(url, out text);
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000EDE1 File Offset: 0x0000CFE1
		public IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		public IClientChannelSink NextChannelSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream requestStream)
		{
			UnixConnection unixConnection = null;
			bool flag = RemotingServices.IsOneWay(((IMethodMessage)msg).MethodBase);
			try
			{
				if (headers == null)
				{
					headers = new TransportHeaders();
				}
				headers["__RequestUri"] = ((IMethodMessage)msg).Uri;
				unixConnection = UnixConnectionPool.GetConnection(this._path);
				UnixMessageIO.SendMessageStream(unixConnection.Stream, requestStream, headers, unixConnection.Buffer);
				unixConnection.Stream.Flush();
				if (!flag)
				{
					sinkStack.Push(this, unixConnection);
					ThreadPool.QueueUserWorkItem(delegate(object data)
					{
						try
						{
							this.ReadAsyncUnixMessage(data);
						}
						catch
						{
						}
					}, sinkStack);
				}
				else
				{
					unixConnection.Release();
				}
			}
			catch
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
				if (!flag)
				{
					throw;
				}
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000EE9C File Offset: 0x0000D09C
		private void ReadAsyncUnixMessage(object data)
		{
			IClientChannelSinkStack clientChannelSinkStack = (IClientChannelSinkStack)data;
			UnixConnection unixConnection = (UnixConnection)clientChannelSinkStack.Pop(this);
			try
			{
				if (UnixMessageIO.ReceiveMessageStatus(unixConnection.Stream, unixConnection.Buffer) != MessageStatus.MethodMessage)
				{
					throw new RemotingException("Unknown response message from server");
				}
				ITransportHeaders transportHeaders;
				Stream stream = UnixMessageIO.ReceiveMessageStream(unixConnection.Stream, out transportHeaders, unixConnection.Buffer);
				unixConnection.Release();
				unixConnection = null;
				clientChannelSinkStack.AsyncProcessResponse(transportHeaders, stream);
			}
			catch
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
				throw;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000EF20 File Offset: 0x0000D120
		public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000EF27 File Offset: 0x0000D127
		public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
		{
			return null;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			UnixConnection unixConnection = null;
			try
			{
				if (requestHeaders == null)
				{
					requestHeaders = new TransportHeaders();
				}
				requestHeaders["__RequestUri"] = ((IMethodMessage)msg).Uri;
				unixConnection = UnixConnectionPool.GetConnection(this._path);
				UnixMessageIO.SendMessageStream(unixConnection.Stream, requestStream, requestHeaders, unixConnection.Buffer);
				unixConnection.Stream.Flush();
				if (UnixMessageIO.ReceiveMessageStatus(unixConnection.Stream, unixConnection.Buffer) != MessageStatus.MethodMessage)
				{
					throw new RemotingException("Unknown response message from server");
				}
				responseStream = UnixMessageIO.ReceiveMessageStream(unixConnection.Stream, out responseHeaders, unixConnection.Buffer);
			}
			finally
			{
				if (unixConnection != null)
				{
					unixConnection.Release();
				}
			}
		}

		// Token: 0x040004CF RID: 1231
		private string _path;
	}
}
