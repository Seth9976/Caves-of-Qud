using System;
using System.Buffers;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>The WebSocket class allows applications to send and receive data after the WebSocket upgrade has completed.</summary>
	// Token: 0x02000839 RID: 2105
	public abstract class WebSocket : IDisposable
	{
		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x0600431E RID: 17182
		public abstract WebSocketCloseStatus? CloseStatus { get; }

		/// <summary>Allows the remote endpoint to describe the reason why the connection was closed.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x0600431F RID: 17183
		public abstract string CloseStatusDescription { get; }

		/// <summary>The subprotocol that was negotiated during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06004320 RID: 17184
		public abstract string SubProtocol { get; }

		/// <summary>Returns the current state of the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketState" />.</returns>
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06004321 RID: 17185
		public abstract WebSocketState State { get; }

		/// <summary>Aborts the WebSocket connection and cancels any pending IO operations.</summary>
		// Token: 0x06004322 RID: 17186
		public abstract void Abort();

		/// <summary>Closes the WebSocket connection as an asynchronous operation using the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Specifies a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		// Token: 0x06004323 RID: 17187
		public abstract Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Initiates or completes the close handshake defined in the WebSocket protocol specification section 7.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="closeStatus">Indicates the reason for closing the WebSocket connection.</param>
		/// <param name="statusDescription">Allows applications to specify a human readable explanation as to why the connection is closed.</param>
		/// <param name="cancellationToken">The token that can be used to propagate notification that operations should be canceled.</param>
		// Token: 0x06004324 RID: 17188
		public abstract Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken);

		/// <summary>Used to clean up unmanaged resources for ASP.NET and self-hosted implementations.</summary>
		// Token: 0x06004325 RID: 17189
		public abstract void Dispose();

		/// <summary>Receives data from the WebSocket connection asynchronously.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Byte" /> array containing the received data.</returns>
		/// <param name="buffer">References the application buffer that is the storage location for the received data.</param>
		/// <param name="cancellationToken">Propagate the notification that operations should be canceled.</param>
		// Token: 0x06004326 RID: 17190
		public abstract Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken);

		/// <summary>Sends data over the WebSocket connection asynchronously.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation. </returns>
		/// <param name="buffer">The buffer to be sent over the connection.</param>
		/// <param name="messageType">Indicates whether the application is sending a binary or text message.</param>
		/// <param name="endOfMessage">Indicates whether the data in “buffer” is the last part of a message.</param>
		/// <param name="cancellationToken">The token that propagates the notification that operations should be canceled.</param>
		// Token: 0x06004327 RID: 17191
		public abstract Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken);

		// Token: 0x06004328 RID: 17192 RVA: 0x000E9BE4 File Offset: 0x000E7DE4
		public virtual async ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			ArraySegment<byte> arraySegment;
			ValueWebSocketReceiveResult valueWebSocketReceiveResult;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				WebSocketReceiveResult webSocketReceiveResult = await this.ReceiveAsync(arraySegment, cancellationToken).ConfigureAwait(false);
				valueWebSocketReceiveResult = new ValueWebSocketReceiveResult(webSocketReceiveResult.Count, webSocketReceiveResult.MessageType, webSocketReceiveResult.EndOfMessage);
			}
			else
			{
				byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
				try
				{
					WebSocketReceiveResult webSocketReceiveResult2 = await this.ReceiveAsync(new ArraySegment<byte>(array, 0, buffer.Length), cancellationToken).ConfigureAwait(false);
					new Span<byte>(array, 0, webSocketReceiveResult2.Count).CopyTo(buffer.Span);
					valueWebSocketReceiveResult = new ValueWebSocketReceiveResult(webSocketReceiveResult2.Count, webSocketReceiveResult2.MessageType, webSocketReceiveResult2.EndOfMessage);
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return valueWebSocketReceiveResult;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x000E9C38 File Offset: 0x000E7E38
		public virtual ValueTask SendAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			ArraySegment<byte> arraySegment;
			return new ValueTask(MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment) ? this.SendAsync(arraySegment, messageType, endOfMessage, cancellationToken) : this.SendWithArrayPoolAsync(buffer, messageType, endOfMessage, cancellationToken));
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000E9C6C File Offset: 0x000E7E6C
		private async Task SendWithArrayPoolAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.Span.CopyTo(array);
				await this.SendAsync(new ArraySegment<byte>(array, 0, buffer.Length), messageType, endOfMessage, cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		/// <summary>Gets the default WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The default WebSocket protocol keep-alive interval in milliseconds. The typical value for this interval is 30 seconds.</returns>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x000E9CD0 File Offset: 0x000E7ED0
		public static TimeSpan DefaultKeepAliveInterval
		{
			get
			{
				return TimeSpan.FromSeconds(30.0);
			}
		}

		/// <summary>Verifies that the connection is in an expected state.</summary>
		/// <param name="state">The current state of the WebSocket to be tested against the list of valid states.</param>
		/// <param name="validStates">List of valid connection states.</param>
		// Token: 0x0600432C RID: 17196 RVA: 0x000E9CE0 File Offset: 0x000E7EE0
		protected static void ThrowOnInvalidState(WebSocketState state, params WebSocketState[] validStates)
		{
			string text = string.Empty;
			if (validStates != null && validStates.Length != 0)
			{
				foreach (WebSocketState webSocketState in validStates)
				{
					if (state == webSocketState)
					{
						return;
					}
				}
				text = string.Join<WebSocketState>(", ", validStates);
			}
			throw new WebSocketException(SR.Format("The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'", state, text));
		}

		/// <summary>Returns a value that indicates if the state of the WebSocket instance is closed or aborted.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="T:System.Net.WebSockets.WebSocket" /> is closed or aborted; otherwise false.</returns>
		/// <param name="state">The current state of the WebSocket.</param>
		// Token: 0x0600432D RID: 17197 RVA: 0x000E9D35 File Offset: 0x000E7F35
		protected static bool IsStateTerminal(WebSocketState state)
		{
			return state == WebSocketState.Closed || state == WebSocketState.Aborted;
		}

		/// <summary>Create client buffers to use with this <see cref="T:System.Net.WebSockets.WebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.ArraySegment`1" />.An array with the client buffers.</returns>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the send buffer.</param>
		// Token: 0x0600432E RID: 17198 RVA: 0x000E9D44 File Offset: 0x000E7F44
		public static ArraySegment<byte> CreateClientBuffer(int receiveBufferSize, int sendBufferSize)
		{
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			return new ArraySegment<byte>(new byte[Math.Max(receiveBufferSize, sendBufferSize)]);
		}

		/// <summary>Creates a WebSocket server buffer.</summary>
		/// <returns>Returns <see cref="T:System.ArraySegment`1" />.</returns>
		/// <param name="receiveBufferSize">The size, in bytes, of the desired buffer.</param>
		// Token: 0x0600432F RID: 17199 RVA: 0x000E9DAC File Offset: 0x000E7FAC
		public static ArraySegment<byte> CreateServerBuffer(int receiveBufferSize)
		{
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			return new ArraySegment<byte>(new byte[receiveBufferSize]);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000E9DE0 File Offset: 0x000E7FE0
		public static WebSocket CreateFromStream(Stream stream, bool isServer, string subProtocol, TimeSpan keepAliveInterval)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanWrite)
			{
				throw new ArgumentException((!stream.CanRead) ? "The base stream is not readable." : "The base stream is not writeable.", "stream");
			}
			if (subProtocol != null)
			{
				WebSocketValidate.ValidateSubprotocol(subProtocol);
			}
			if (keepAliveInterval != Timeout.InfiniteTimeSpan && keepAliveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			return ManagedWebSocket.CreateFromConnectedStream(stream, isServer, subProtocol, keepAliveInterval);
		}

		/// <summary>Returns a value that indicates if the WebSocket instance is targeting .NET Framework 4.5.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="T:System.Net.WebSockets.WebSocket" /> is targeting .NET Framework 4.5; otherwise false.</returns>
		// Token: 0x06004331 RID: 17201 RVA: 0x0000390E File Offset: 0x00001B0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.")]
		public static bool IsApplicationTargeting45()
		{
			return true;
		}

		/// <summary>This API supports the .NET Framework infrastructure and is not intended to be used directly from your code. Allows callers to register prefixes for WebSocket requests (ws and wss).</summary>
		// Token: 0x06004332 RID: 17202 RVA: 0x00011F38 File Offset: 0x00010138
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterPrefixes()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.Allows callers to create a client side WebSocket class which will use the WSPC for framing purposes.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		/// <param name="innerStream">The connection to be used for IO operations.</param>
		/// <param name="subProtocol">The subprotocol accepted by the client.</param>
		/// <param name="receiveBufferSize">The size in bytes of the client WebSocket receive buffer.</param>
		/// <param name="sendBufferSize">The size in bytes of the client WebSocket send buffer.</param>
		/// <param name="keepAliveInterval">Determines how regularly a frame is sent over the connection as a keep-alive. Applies only when the connection is idle.</param>
		/// <param name="useZeroMaskingKey">Indicates whether a random key or a static key (just zeros) should be used for the WebSocket masking.</param>
		/// <param name="internalBuffer">Will be used as the internal buffer in the WPC. The size has to be at least 2 * ReceiveBufferSize + SendBufferSize + 256 + 20 (16 on 32-bit).</param>
		// Token: 0x06004333 RID: 17203 RVA: 0x000E9E78 File Offset: 0x000E8078
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebSocket CreateClientWebSocket(Stream innerStream, string subProtocol, int receiveBufferSize, int sendBufferSize, TimeSpan keepAliveInterval, bool useZeroMaskingKey, ArraySegment<byte> internalBuffer)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException((!innerStream.CanRead) ? "The base stream is not readable." : "The base stream is not writeable.", "innerStream");
			}
			if (subProtocol != null)
			{
				WebSocketValidate.ValidateSubprotocol(subProtocol);
			}
			if (keepAliveInterval != Timeout.InfiniteTimeSpan && keepAliveInterval < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("keepAliveInterval", keepAliveInterval, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			if (receiveBufferSize <= 0 || sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException((receiveBufferSize <= 0) ? "receiveBufferSize" : "sendBufferSize", (receiveBufferSize <= 0) ? receiveBufferSize : sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 0));
			}
			return ManagedWebSocket.CreateFromConnectedStream(innerStream, false, subProtocol, keepAliveInterval);
		}
	}
}
