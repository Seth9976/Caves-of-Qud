using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000818 RID: 2072
	internal sealed class ManagedWebSocket : WebSocket
	{
		// Token: 0x06004258 RID: 16984 RVA: 0x000E4E5C File Offset: 0x000E305C
		public static ManagedWebSocket CreateFromConnectedStream(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			return new ManagedWebSocket(stream, isServer, subprotocol, keepAliveInterval);
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06004259 RID: 16985 RVA: 0x000E4E67 File Offset: 0x000E3067
		private object StateUpdateLock
		{
			get
			{
				return this._abortSource;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x0600425A RID: 16986 RVA: 0x000E4E6F File Offset: 0x000E306F
		private object ReceiveAsyncLock
		{
			get
			{
				return this._utf8TextState;
			}
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x000E4E78 File Offset: 0x000E3078
		private ManagedWebSocket(Stream stream, bool isServer, string subprotocol, TimeSpan keepAliveInterval)
		{
			this._stream = stream;
			this._isServer = isServer;
			this._subprotocol = subprotocol;
			this._receiveBuffer = new byte[125];
			this._abortSource.Token.Register(delegate(object s)
			{
				ManagedWebSocket managedWebSocket = (ManagedWebSocket)s;
				object stateUpdateLock = managedWebSocket.StateUpdateLock;
				lock (stateUpdateLock)
				{
					WebSocketState state = managedWebSocket._state;
					if (state != WebSocketState.Closed && state != WebSocketState.Aborted)
					{
						managedWebSocket._state = ((state != WebSocketState.None && state != WebSocketState.Connecting) ? WebSocketState.Aborted : WebSocketState.Closed);
					}
				}
			}, this);
			if (keepAliveInterval > TimeSpan.Zero)
			{
				this._keepAliveTimer = new Timer(delegate(object s)
				{
					((ManagedWebSocket)s).SendKeepAliveFrameAsync();
				}, this, keepAliveInterval, keepAliveInterval);
			}
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x000E4F78 File Offset: 0x000E3178
		public override void Dispose()
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this.DisposeCore();
			}
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x000E4FB8 File Offset: 0x000E31B8
		private void DisposeCore()
		{
			if (!this._disposed)
			{
				this._disposed = true;
				Timer keepAliveTimer = this._keepAliveTimer;
				if (keepAliveTimer != null)
				{
					keepAliveTimer.Dispose();
				}
				Stream stream = this._stream;
				if (stream != null)
				{
					stream.Dispose();
				}
				if (this._state < WebSocketState.Aborted)
				{
					this._state = WebSocketState.Closed;
				}
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x000E5006 File Offset: 0x000E3206
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				return this._closeStatus;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600425F RID: 16991 RVA: 0x000E500E File Offset: 0x000E320E
		public override string CloseStatusDescription
		{
			get
			{
				return this._closeStatusDescription;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x000E5016 File Offset: 0x000E3216
		public override WebSocketState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x000E501E File Offset: 0x000E321E
		public override string SubProtocol
		{
			get
			{
				return this._subprotocol;
			}
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x000E5028 File Offset: 0x000E3228
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[] { "Close", "SendAsync", "Binary", "Text", "CloseOutputAsync" }), "messageType");
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			return this.SendPrivateAsync(buffer, messageType, endOfMessage, cancellationToken).AsTask();
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x000E50A4 File Offset: 0x000E32A4
		private ValueTask SendPrivateAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			if (messageType != WebSocketMessageType.Text && messageType != WebSocketMessageType.Binary)
			{
				throw new ArgumentException(SR.Format("The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ", new object[] { "Close", "SendAsync", "Binary", "Text", "CloseOutputAsync" }), "messageType");
			}
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validSendStates);
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException(ex));
			}
			ManagedWebSocket.MessageOpcode messageOpcode = (this._lastSendWasFragment ? ManagedWebSocket.MessageOpcode.Continuation : ((messageType == WebSocketMessageType.Binary) ? ManagedWebSocket.MessageOpcode.Binary : ManagedWebSocket.MessageOpcode.Text));
			ValueTask valueTask = this.SendFrameAsync(messageOpcode, endOfMessage, buffer, cancellationToken);
			this._lastSendWasFragment = !endOfMessage;
			return valueTask;
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000E515C File Offset: 0x000E335C
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			Task<WebSocketReceiveResult> task2;
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validReceiveStates);
				object receiveAsyncLock = this.ReceiveAsyncLock;
				lock (receiveAsyncLock)
				{
					this.ThrowIfOperationInProgress(this._lastReceiveAsync.IsCompleted, "ReceiveAsync");
					Task<WebSocketReceiveResult> task = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(buffer, cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
					this._lastReceiveAsync = task;
					task2 = task;
				}
			}
			catch (Exception ex)
			{
				task2 = Task.FromException<WebSocketReceiveResult>(ex);
			}
			return task2;
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x000E5210 File Offset: 0x000E3410
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseStates);
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
			return this.CloseAsyncPrivate(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x000E5260 File Offset: 0x000E3460
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			WebSocketValidate.ValidateCloseStatus(closeStatus, statusDescription);
			try
			{
				WebSocketValidate.ThrowIfInvalidState(this._state, this._disposed, ManagedWebSocket.s_validCloseOutputStates);
			}
			catch (Exception ex)
			{
				return Task.FromException(ex);
			}
			return this.SendCloseFrameAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x000E52B0 File Offset: 0x000E34B0
		public override void Abort()
		{
			this._abortSource.Cancel();
			this.Dispose();
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x000E52C3 File Offset: 0x000E34C3
		private ValueTask SendFrameAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			if (!cancellationToken.CanBeCanceled && this._sendFrameAsyncLock.Wait(0))
			{
				return this.SendFrameLockAcquiredNonCancelableAsync(opcode, endOfMessage, payloadBuffer);
			}
			return new ValueTask(this.SendFrameFallbackAsync(opcode, endOfMessage, payloadBuffer, cancellationToken));
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000E52F8 File Offset: 0x000E34F8
		private ValueTask SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer)
		{
			ValueTask valueTask = default(ValueTask);
			bool flag = true;
			try
			{
				int num = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				valueTask = this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, num), default(CancellationToken));
				if (valueTask.IsCompleted)
				{
					return valueTask;
				}
				flag = false;
			}
			catch (Exception ex)
			{
				return new ValueTask(Task.FromException((ex is OperationCanceledException) ? ex : ((this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex))));
			}
			finally
			{
				if (flag)
				{
					this.ReleaseSendBuffer();
					this._sendFrameAsyncLock.Release();
				}
			}
			return new ValueTask(this.WaitForWriteTaskAsync(valueTask));
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000E53D4 File Offset: 0x000E35D4
		private async Task WaitForWriteTaskAsync(ValueTask writeTask)
		{
			try
			{
				await writeTask.ConfigureAwait(false);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw (this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, default(CancellationToken)) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			finally
			{
				this.ReleaseSendBuffer();
				this._sendFrameAsyncLock.Release();
			}
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x000E5420 File Offset: 0x000E3620
		private async Task SendFrameFallbackAsync(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlyMemory<byte> payloadBuffer, CancellationToken cancellationToken)
		{
			await this._sendFrameAsyncLock.WaitAsync().ConfigureAwait(false);
			try
			{
				int num = this.WriteFrameToSendBuffer(opcode, endOfMessage, payloadBuffer.Span);
				using (cancellationToken.Register(delegate(object s)
				{
					((ManagedWebSocket)s).Abort();
				}, this))
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._sendBuffer, 0, num), cancellationToken).ConfigureAwait(false);
				}
				CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			}
			catch (Exception ex) when (!(ex is OperationCanceledException))
			{
				throw (this._state == WebSocketState.Aborted) ? ManagedWebSocket.CreateOperationCanceledException(ex, cancellationToken) : new WebSocketException(WebSocketError.ConnectionClosedPrematurely, ex);
			}
			finally
			{
				this.ReleaseSendBuffer();
				this._sendFrameAsyncLock.Release();
			}
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000E5484 File Offset: 0x000E3684
		private int WriteFrameToSendBuffer(ManagedWebSocket.MessageOpcode opcode, bool endOfMessage, ReadOnlySpan<byte> payloadBuffer)
		{
			this.AllocateSendBuffer(payloadBuffer.Length + 14);
			int? num = null;
			int num2;
			if (this._isServer)
			{
				num2 = ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, false);
			}
			else
			{
				num = new int?(ManagedWebSocket.WriteHeader(opcode, this._sendBuffer, payloadBuffer, endOfMessage, true));
				num2 = num.GetValueOrDefault() + 4;
			}
			if (payloadBuffer.Length > 0)
			{
				payloadBuffer.CopyTo(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length));
				if (num != null)
				{
					ManagedWebSocket.ApplyMask(new Span<byte>(this._sendBuffer, num2, payloadBuffer.Length), this._sendBuffer, num.Value, 0);
				}
			}
			return num2 + payloadBuffer.Length;
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000E5540 File Offset: 0x000E3740
		private void SendKeepAliveFrameAsync()
		{
			if (this._sendFrameAsyncLock.Wait(0))
			{
				ValueTask valueTask = this.SendFrameLockAcquiredNonCancelableAsync(ManagedWebSocket.MessageOpcode.Ping, true, Memory<byte>.Empty);
				if (valueTask.IsCompletedSuccessfully)
				{
					valueTask.GetAwaiter().GetResult();
					return;
				}
				valueTask.AsTask().ContinueWith(delegate(Task p)
				{
					AggregateException exception = p.Exception;
				}, CancellationToken.None, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000E55C4 File Offset: 0x000E37C4
		private static int WriteHeader(ManagedWebSocket.MessageOpcode opcode, byte[] sendBuffer, ReadOnlySpan<byte> payload, bool endOfMessage, bool useMask)
		{
			sendBuffer[0] = (byte)opcode;
			if (endOfMessage)
			{
				int num = 0;
				sendBuffer[num] |= 128;
			}
			int num2;
			if (payload.Length <= 125)
			{
				sendBuffer[1] = (byte)payload.Length;
				num2 = 2;
			}
			else if (payload.Length <= 65535)
			{
				sendBuffer[1] = 126;
				sendBuffer[2] = (byte)(payload.Length / 256);
				sendBuffer[3] = (byte)payload.Length;
				num2 = 4;
			}
			else
			{
				sendBuffer[1] = 127;
				int num3 = payload.Length;
				for (int i = 9; i >= 2; i--)
				{
					sendBuffer[i] = (byte)num3;
					num3 /= 256;
				}
				num2 = 10;
			}
			if (useMask)
			{
				int num4 = 1;
				sendBuffer[num4] |= 128;
				ManagedWebSocket.WriteRandomMask(sendBuffer, num2);
			}
			return num2;
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x000E567D File Offset: 0x000E387D
		private static void WriteRandomMask(byte[] buffer, int offset)
		{
			ManagedWebSocket.s_random.GetBytes(buffer, offset, 4);
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x000E568C File Offset: 0x000E388C
		private ValueTask<TWebSocketReceiveResult> ReceiveAsyncPrivate<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>(Memory<byte> payloadBuffer, CancellationToken cancellationToken, TWebSocketReceiveResultGetter resultGetter = default(TWebSocketReceiveResultGetter)) where TWebSocketReceiveResultGetter : struct, ManagedWebSocket.IWebSocketReceiveResultGetter<TWebSocketReceiveResult>
		{
			ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult> <ReceiveAsyncPrivate>d__;
			<ReceiveAsyncPrivate>d__.<>4__this = this;
			<ReceiveAsyncPrivate>d__.payloadBuffer = payloadBuffer;
			<ReceiveAsyncPrivate>d__.cancellationToken = cancellationToken;
			<ReceiveAsyncPrivate>d__.resultGetter = resultGetter;
			<ReceiveAsyncPrivate>d__.<>t__builder = AsyncValueTaskMethodBuilder<TWebSocketReceiveResult>.Create();
			<ReceiveAsyncPrivate>d__.<>1__state = -1;
			<ReceiveAsyncPrivate>d__.<>t__builder.Start<ManagedWebSocket.<ReceiveAsyncPrivate>d__61<TWebSocketReceiveResultGetter, TWebSocketReceiveResult>>(ref <ReceiveAsyncPrivate>d__);
			return <ReceiveAsyncPrivate>d__.<>t__builder.Task;
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x000E56E8 File Offset: 0x000E38E8
		private unsafe async Task HandleReceivedCloseAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this._receivedCloseFrame = true;
				if (this._state < WebSocketState.CloseReceived)
				{
					this._state = WebSocketState.CloseReceived;
				}
			}
			WebSocketCloseStatus closeStatus = WebSocketCloseStatus.NormalClosure;
			string closeStatusDescription = string.Empty;
			if (header.PayloadLength == 1L)
			{
				await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false);
			}
			else if (header.PayloadLength >= 2L)
			{
				if ((long)this._receiveBufferCount < header.PayloadLength)
				{
					await this.EnsureBufferContainsAsync((int)header.PayloadLength, cancellationToken, true).ConfigureAwait(false);
				}
				if (this._isServer)
				{
					ManagedWebSocket.ApplyMask(this._receiveBuffer.Span.Slice(this._receiveBufferOffset, (int)header.PayloadLength), header.Mask, 0);
				}
				closeStatus = (WebSocketCloseStatus)(((int)(*this._receiveBuffer.Span[this._receiveBufferOffset]) << 8) | (int)(*this._receiveBuffer.Span[this._receiveBufferOffset + 1]));
				if (!ManagedWebSocket.IsValidCloseStatus(closeStatus))
				{
					await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, null).ConfigureAwait(false);
				}
				if (header.PayloadLength > 2L)
				{
					int num = 0;
					try
					{
						closeStatusDescription = ManagedWebSocket.s_textEncoding.GetString(this._receiveBuffer.Span.Slice(this._receiveBufferOffset + 2, (int)header.PayloadLength - 2));
					}
					catch (DecoderFallbackException stateUpdateLock)
					{
						num = 1;
					}
					if (num == 1)
					{
						await this.CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus.ProtocolError, WebSocketError.Faulted, (DecoderFallbackException)stateUpdateLock).ConfigureAwait(false);
					}
				}
				this.ConsumeFromBuffer((int)header.PayloadLength);
			}
			this._closeStatus = new WebSocketCloseStatus?(closeStatus);
			this._closeStatusDescription = closeStatusDescription;
			if (!this._isServer && this._sentCloseFrame)
			{
				await this.WaitForServerToCloseConnectionAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x000E573C File Offset: 0x000E393C
		private async Task WaitForServerToCloseConnectionAsync(CancellationToken cancellationToken)
		{
			ValueTask<int> valueTask = this._stream.ReadAsync(this._receiveBuffer, cancellationToken);
			if (!valueTask.IsCompletedSuccessfully)
			{
				using (CancellationTokenSource finalCts = new CancellationTokenSource(1000))
				{
					using (finalCts.Token.Register(delegate(object s)
					{
						((ManagedWebSocket)s).Abort();
					}, this))
					{
						try
						{
							await valueTask.ConfigureAwait(false);
						}
						catch
						{
						}
					}
					CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
				}
				CancellationTokenSource finalCts = null;
			}
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x000E5788 File Offset: 0x000E3988
		private async Task HandleReceivedPingPongAsync(ManagedWebSocket.MessageHeader header, CancellationToken cancellationToken)
		{
			if (header.PayloadLength > 0L && (long)this._receiveBufferCount < header.PayloadLength)
			{
				await this.EnsureBufferContainsAsync((int)header.PayloadLength, cancellationToken, true).ConfigureAwait(false);
			}
			if (header.Opcode == ManagedWebSocket.MessageOpcode.Ping)
			{
				if (this._isServer)
				{
					ManagedWebSocket.ApplyMask(this._receiveBuffer.Span.Slice(this._receiveBufferOffset, (int)header.PayloadLength), header.Mask, 0);
				}
				await this.SendFrameAsync(ManagedWebSocket.MessageOpcode.Pong, true, this._receiveBuffer.Slice(this._receiveBufferOffset, (int)header.PayloadLength), default(CancellationToken)).ConfigureAwait(false);
			}
			if (header.PayloadLength > 0L)
			{
				this.ConsumeFromBuffer((int)header.PayloadLength);
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000E57DB File Offset: 0x000E39DB
		private static bool IsValidCloseStatus(WebSocketCloseStatus closeStatus)
		{
			return closeStatus >= WebSocketCloseStatus.NormalClosure && closeStatus < (WebSocketCloseStatus)5000 && (closeStatus >= (WebSocketCloseStatus)3000 || (closeStatus - WebSocketCloseStatus.NormalClosure <= 3 || closeStatus - WebSocketCloseStatus.InvalidPayloadData <= 4));
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x000E5810 File Offset: 0x000E3A10
		private async Task CloseWithReceiveErrorAndThrowAsync(WebSocketCloseStatus closeStatus, WebSocketError error, Exception innerException = null)
		{
			if (!this._sentCloseFrame)
			{
				await this.CloseOutputAsync(closeStatus, string.Empty, default(CancellationToken)).ConfigureAwait(false);
			}
			this._receiveBufferCount = 0;
			throw new WebSocketException(error, innerException);
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x000E586C File Offset: 0x000E3A6C
		private unsafe bool TryParseMessageHeaderFromReceiveBuffer(out ManagedWebSocket.MessageHeader resultHeader)
		{
			ManagedWebSocket.MessageHeader messageHeader = default(ManagedWebSocket.MessageHeader);
			Span<byte> span = this._receiveBuffer.Span;
			messageHeader.Fin = (*span[this._receiveBufferOffset] & 128) > 0;
			bool flag = (*span[this._receiveBufferOffset] & 112) > 0;
			messageHeader.Opcode = (ManagedWebSocket.MessageOpcode)(*span[this._receiveBufferOffset] & 15);
			bool flag2 = (*span[this._receiveBufferOffset + 1] & 128) > 0;
			messageHeader.PayloadLength = (long)(*span[this._receiveBufferOffset + 1] & 127);
			this.ConsumeFromBuffer(2);
			if (messageHeader.PayloadLength == 126L)
			{
				messageHeader.PayloadLength = (long)(((int)(*span[this._receiveBufferOffset]) << 8) | (int)(*span[this._receiveBufferOffset + 1]));
				this.ConsumeFromBuffer(2);
			}
			else if (messageHeader.PayloadLength == 127L)
			{
				messageHeader.PayloadLength = 0L;
				for (int i = 0; i < 8; i++)
				{
					messageHeader.PayloadLength = (messageHeader.PayloadLength << 8) | (long)((ulong)(*span[this._receiveBufferOffset + i]));
				}
				this.ConsumeFromBuffer(8);
			}
			bool flag3 = flag;
			if (flag2)
			{
				if (!this._isServer)
				{
					flag3 = true;
				}
				messageHeader.Mask = ManagedWebSocket.CombineMaskBytes(span, this._receiveBufferOffset);
				this.ConsumeFromBuffer(4);
			}
			switch (messageHeader.Opcode)
			{
			case ManagedWebSocket.MessageOpcode.Continuation:
				if (this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			case ManagedWebSocket.MessageOpcode.Text:
			case ManagedWebSocket.MessageOpcode.Binary:
				if (!this._lastReceiveHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			case ManagedWebSocket.MessageOpcode.Close:
			case ManagedWebSocket.MessageOpcode.Ping:
			case ManagedWebSocket.MessageOpcode.Pong:
				if (messageHeader.PayloadLength > 125L || !messageHeader.Fin)
				{
					flag3 = true;
					goto IL_01CD;
				}
				goto IL_01CD;
			}
			flag3 = true;
			IL_01CD:
			resultHeader = messageHeader;
			return !flag3;
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000E5A54 File Offset: 0x000E3C54
		private async Task CloseAsyncPrivate(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			if (!this._sentCloseFrame)
			{
				await this.SendCloseFrameAsync(closeStatus, statusDescription, cancellationToken).ConfigureAwait(false);
			}
			byte[] closeBuffer = ArrayPool<byte>.Shared.Rent(139);
			object obj;
			try
			{
				while (!this._receivedCloseFrame)
				{
					obj = this.ReceiveAsyncLock;
					Task task;
					lock (obj)
					{
						if (this._receivedCloseFrame)
						{
							break;
						}
						task = this._lastReceiveAsync;
						task = (this._lastReceiveAsync = this.ValidateAndReceiveAsync(task, closeBuffer, cancellationToken));
					}
					await task.ConfigureAwait(false);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(closeBuffer, false);
			}
			obj = this.StateUpdateLock;
			lock (obj)
			{
				this.DisposeCore();
				if (this._state < WebSocketState.Closed)
				{
					this._state = WebSocketState.Closed;
				}
			}
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000E5AB0 File Offset: 0x000E3CB0
		private async Task SendCloseFrameAsync(WebSocketCloseStatus closeStatus, string closeStatusDescription, CancellationToken cancellationToken)
		{
			byte[] buffer = null;
			try
			{
				int num = 2;
				if (string.IsNullOrEmpty(closeStatusDescription))
				{
					buffer = ArrayPool<byte>.Shared.Rent(num);
				}
				else
				{
					num += ManagedWebSocket.s_textEncoding.GetByteCount(closeStatusDescription);
					buffer = ArrayPool<byte>.Shared.Rent(num);
					ManagedWebSocket.s_textEncoding.GetBytes(closeStatusDescription, 0, closeStatusDescription.Length, buffer, 2);
				}
				ushort num2 = (ushort)closeStatus;
				buffer[0] = (byte)(num2 >> 8);
				buffer[1] = (byte)(num2 & 255);
				await this.SendFrameAsync(ManagedWebSocket.MessageOpcode.Close, true, new Memory<byte>(buffer, 0, num), cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				if (buffer != null)
				{
					ArrayPool<byte>.Shared.Return(buffer, false);
				}
			}
			object stateUpdateLock = this.StateUpdateLock;
			lock (stateUpdateLock)
			{
				this._sentCloseFrame = true;
				if (this._state <= WebSocketState.CloseReceived)
				{
					this._state = WebSocketState.CloseSent;
				}
			}
			if (!this._isServer && this._receivedCloseFrame)
			{
				await this.WaitForServerToCloseConnectionAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x000E5B0B File Offset: 0x000E3D0B
		private void ConsumeFromBuffer(int count)
		{
			this._receiveBufferCount -= count;
			this._receiveBufferOffset += count;
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x000E5B2C File Offset: 0x000E3D2C
		private async Task EnsureBufferContainsAsync(int minimumRequiredBytes, CancellationToken cancellationToken, bool throwOnPrematureClosure = true)
		{
			if (this._receiveBufferCount < minimumRequiredBytes)
			{
				if (this._receiveBufferCount > 0)
				{
					this._receiveBuffer.Span.Slice(this._receiveBufferOffset, this._receiveBufferCount).CopyTo(this._receiveBuffer.Span);
				}
				this._receiveBufferOffset = 0;
				while (this._receiveBufferCount < minimumRequiredBytes)
				{
					int num = await this._stream.ReadAsync(this._receiveBuffer.Slice(this._receiveBufferCount, this._receiveBuffer.Length - this._receiveBufferCount), cancellationToken).ConfigureAwait(false);
					if (num <= 0)
					{
						this.ThrowIfEOFUnexpected(throwOnPrematureClosure);
						break;
					}
					this._receiveBufferCount += num;
				}
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000E5B87 File Offset: 0x000E3D87
		private void ThrowIfEOFUnexpected(bool throwOnPrematureClosure)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("WebSocket");
			}
			if (throwOnPrematureClosure)
			{
				throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely);
			}
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000E5BA6 File Offset: 0x000E3DA6
		private void AllocateSendBuffer(int minLength)
		{
			this._sendBuffer = ArrayPool<byte>.Shared.Rent(minLength);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000E5BBC File Offset: 0x000E3DBC
		private void ReleaseSendBuffer()
		{
			byte[] sendBuffer = this._sendBuffer;
			if (sendBuffer != null)
			{
				this._sendBuffer = null;
				ArrayPool<byte>.Shared.Return(sendBuffer, false);
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000E5BE6 File Offset: 0x000E3DE6
		private static int CombineMaskBytes(Span<byte> buffer, int maskOffset)
		{
			return BitConverter.ToInt32(buffer.Slice(maskOffset));
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000E5BFA File Offset: 0x000E3DFA
		private static int ApplyMask(Span<byte> toMask, byte[] mask, int maskOffset, int maskOffsetIndex)
		{
			return ManagedWebSocket.ApplyMask(toMask, ManagedWebSocket.CombineMaskBytes(mask, maskOffset), maskOffsetIndex);
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000E5C10 File Offset: 0x000E3E10
		private unsafe static int ApplyMask(Span<byte> toMask, int mask, int maskIndex)
		{
			int num = maskIndex * 8;
			int num2 = (int)(((uint)mask >> num) | (uint)((uint)mask << 32 - num));
			int i = toMask.Length;
			if (i > 0)
			{
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(toMask))
				{
					byte* ptr = reference;
					if (ptr % 4L == null)
					{
						while (i >= 4)
						{
							i -= 4;
							*(int*)ptr ^= num2;
							ptr += 4;
						}
					}
					if (i > 0)
					{
						byte* ptr2 = (byte*)(&mask);
						byte* ptr3 = ptr + i;
						while (ptr < ptr3)
						{
							byte* ptr4 = ptr++;
							*ptr4 ^= ptr2[maskIndex];
							maskIndex = (maskIndex + 1) & 3;
						}
					}
				}
			}
			return maskIndex;
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x000E5C9B File Offset: 0x000E3E9B
		private void ThrowIfOperationInProgress(bool operationCompleted, [CallerMemberName] string methodName = null)
		{
			if (!operationCompleted)
			{
				this.Abort();
				this.ThrowOperationInProgress(methodName);
			}
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000E5CAD File Offset: 0x000E3EAD
		private void ThrowOperationInProgress(string methodName)
		{
			throw new InvalidOperationException(SR.Format("There is already one outstanding '{0}' call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time.", methodName));
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x000E5CBF File Offset: 0x000E3EBF
		private static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new OperationCanceledException(new OperationCanceledException().Message, innerException, cancellationToken);
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x000E5CD4 File Offset: 0x000E3ED4
		private unsafe static bool TryValidateUtf8(Span<byte> span, bool endOfMessage, ManagedWebSocket.Utf8MessageState state)
		{
			int i = 0;
			while (i < span.Length)
			{
				if (!state.SequenceInProgress)
				{
					state.SequenceInProgress = true;
					byte b = *span[i];
					i++;
					if ((b & 128) == 0)
					{
						state.AdditionalBytesExpected = 0;
						state.CurrentDecodeBits = (int)(b & 127);
						state.ExpectedValueMin = 0;
					}
					else
					{
						if ((b & 192) == 128)
						{
							return false;
						}
						if ((b & 224) == 192)
						{
							state.AdditionalBytesExpected = 1;
							state.CurrentDecodeBits = (int)(b & 31);
							state.ExpectedValueMin = 128;
						}
						else if ((b & 240) == 224)
						{
							state.AdditionalBytesExpected = 2;
							state.CurrentDecodeBits = (int)(b & 15);
							state.ExpectedValueMin = 2048;
						}
						else
						{
							if ((b & 248) != 240)
							{
								return false;
							}
							state.AdditionalBytesExpected = 3;
							state.CurrentDecodeBits = (int)(b & 7);
							state.ExpectedValueMin = 65536;
						}
					}
				}
				while (state.AdditionalBytesExpected > 0 && i < span.Length)
				{
					byte b2 = *span[i];
					if ((b2 & 192) != 128)
					{
						return false;
					}
					i++;
					state.AdditionalBytesExpected--;
					state.CurrentDecodeBits = (state.CurrentDecodeBits << 6) | (int)(b2 & 63);
					if (state.AdditionalBytesExpected == 1 && state.CurrentDecodeBits >= 864 && state.CurrentDecodeBits <= 895)
					{
						return false;
					}
					if (state.AdditionalBytesExpected == 2 && state.CurrentDecodeBits >= 272)
					{
						return false;
					}
				}
				if (state.AdditionalBytesExpected == 0)
				{
					state.SequenceInProgress = false;
					if (state.CurrentDecodeBits < state.ExpectedValueMin)
					{
						return false;
					}
				}
			}
			return !endOfMessage || !state.SequenceInProgress;
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x000E5E98 File Offset: 0x000E4098
		private Task ValidateAndReceiveAsync(Task receiveTask, byte[] buffer, CancellationToken cancellationToken)
		{
			if (receiveTask != null)
			{
				if (receiveTask.Status != TaskStatus.RanToCompletion)
				{
					return receiveTask;
				}
				Task<WebSocketReceiveResult> task = receiveTask as Task<WebSocketReceiveResult>;
				if (task != null && task.Result.MessageType == WebSocketMessageType.Close)
				{
					return receiveTask;
				}
			}
			receiveTask = this.ReceiveAsyncPrivate<ManagedWebSocket.WebSocketReceiveResultGetter, WebSocketReceiveResult>(new ArraySegment<byte>(buffer), cancellationToken, default(ManagedWebSocket.WebSocketReceiveResultGetter)).AsTask();
			return receiveTask;
		}

		// Token: 0x040027C2 RID: 10178
		private static readonly RandomNumberGenerator s_random = RandomNumberGenerator.Create();

		// Token: 0x040027C3 RID: 10179
		private static readonly UTF8Encoding s_textEncoding = new UTF8Encoding(false, true);

		// Token: 0x040027C4 RID: 10180
		private static readonly WebSocketState[] s_validSendStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x040027C5 RID: 10181
		private static readonly WebSocketState[] s_validReceiveStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseSent
		};

		// Token: 0x040027C6 RID: 10182
		private static readonly WebSocketState[] s_validCloseOutputStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived
		};

		// Token: 0x040027C7 RID: 10183
		private static readonly WebSocketState[] s_validCloseStates = new WebSocketState[]
		{
			WebSocketState.Open,
			WebSocketState.CloseReceived,
			WebSocketState.CloseSent
		};

		// Token: 0x040027C8 RID: 10184
		private static readonly Task<WebSocketReceiveResult> s_cachedCloseTask = Task.FromResult<WebSocketReceiveResult>(new WebSocketReceiveResult(0, WebSocketMessageType.Close, true));

		// Token: 0x040027C9 RID: 10185
		internal const int MaxMessageHeaderLength = 14;

		// Token: 0x040027CA RID: 10186
		private const int MaxControlPayloadLength = 125;

		// Token: 0x040027CB RID: 10187
		private const int MaskLength = 4;

		// Token: 0x040027CC RID: 10188
		private readonly Stream _stream;

		// Token: 0x040027CD RID: 10189
		private readonly bool _isServer;

		// Token: 0x040027CE RID: 10190
		private readonly string _subprotocol;

		// Token: 0x040027CF RID: 10191
		private readonly Timer _keepAliveTimer;

		// Token: 0x040027D0 RID: 10192
		private readonly CancellationTokenSource _abortSource = new CancellationTokenSource();

		// Token: 0x040027D1 RID: 10193
		private Memory<byte> _receiveBuffer;

		// Token: 0x040027D2 RID: 10194
		private readonly ManagedWebSocket.Utf8MessageState _utf8TextState = new ManagedWebSocket.Utf8MessageState();

		// Token: 0x040027D3 RID: 10195
		private readonly SemaphoreSlim _sendFrameAsyncLock = new SemaphoreSlim(1, 1);

		// Token: 0x040027D4 RID: 10196
		private WebSocketState _state = WebSocketState.Open;

		// Token: 0x040027D5 RID: 10197
		private bool _disposed;

		// Token: 0x040027D6 RID: 10198
		private bool _sentCloseFrame;

		// Token: 0x040027D7 RID: 10199
		private bool _receivedCloseFrame;

		// Token: 0x040027D8 RID: 10200
		private WebSocketCloseStatus? _closeStatus;

		// Token: 0x040027D9 RID: 10201
		private string _closeStatusDescription;

		// Token: 0x040027DA RID: 10202
		private ManagedWebSocket.MessageHeader _lastReceiveHeader = new ManagedWebSocket.MessageHeader
		{
			Opcode = ManagedWebSocket.MessageOpcode.Text,
			Fin = true
		};

		// Token: 0x040027DB RID: 10203
		private int _receiveBufferOffset;

		// Token: 0x040027DC RID: 10204
		private int _receiveBufferCount;

		// Token: 0x040027DD RID: 10205
		private int _receivedMaskOffsetOffset;

		// Token: 0x040027DE RID: 10206
		private byte[] _sendBuffer;

		// Token: 0x040027DF RID: 10207
		private bool _lastSendWasFragment;

		// Token: 0x040027E0 RID: 10208
		private Task _lastReceiveAsync = Task.CompletedTask;

		// Token: 0x02000819 RID: 2073
		private sealed class Utf8MessageState
		{
			// Token: 0x040027E1 RID: 10209
			internal bool SequenceInProgress;

			// Token: 0x040027E2 RID: 10210
			internal int AdditionalBytesExpected;

			// Token: 0x040027E3 RID: 10211
			internal int ExpectedValueMin;

			// Token: 0x040027E4 RID: 10212
			internal int CurrentDecodeBits;
		}

		// Token: 0x0200081A RID: 2074
		private enum MessageOpcode : byte
		{
			// Token: 0x040027E6 RID: 10214
			Continuation,
			// Token: 0x040027E7 RID: 10215
			Text,
			// Token: 0x040027E8 RID: 10216
			Binary,
			// Token: 0x040027E9 RID: 10217
			Close = 8,
			// Token: 0x040027EA RID: 10218
			Ping,
			// Token: 0x040027EB RID: 10219
			Pong
		}

		// Token: 0x0200081B RID: 2075
		[StructLayout(LayoutKind.Auto)]
		private struct MessageHeader
		{
			// Token: 0x040027EC RID: 10220
			internal ManagedWebSocket.MessageOpcode Opcode;

			// Token: 0x040027ED RID: 10221
			internal bool Fin;

			// Token: 0x040027EE RID: 10222
			internal long PayloadLength;

			// Token: 0x040027EF RID: 10223
			internal int Mask;
		}

		// Token: 0x0200081C RID: 2076
		private interface IWebSocketReceiveResultGetter<TResult>
		{
			// Token: 0x06004288 RID: 17032
			TResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription);
		}

		// Token: 0x0200081D RID: 2077
		private readonly struct WebSocketReceiveResultGetter : ManagedWebSocket.IWebSocketReceiveResultGetter<WebSocketReceiveResult>
		{
			// Token: 0x06004289 RID: 17033 RVA: 0x000E5F74 File Offset: 0x000E4174
			public WebSocketReceiveResult GetResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeDescription)
			{
				return new WebSocketReceiveResult(count, messageType, endOfMessage, closeStatus, closeDescription);
			}
		}
	}
}
