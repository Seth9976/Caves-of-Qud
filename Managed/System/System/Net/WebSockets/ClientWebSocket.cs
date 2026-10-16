using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	/// <summary>Provides a client for connecting to WebSocket services.</summary>
	// Token: 0x0200082C RID: 2092
	public sealed class ClientWebSocket : WebSocket
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> class.</summary>
		// Token: 0x060042BD RID: 17085 RVA: 0x000E80AC File Offset: 0x000E62AC
		public ClientWebSocket()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, ".ctor");
			}
			WebSocketHandle.CheckPlatformSupport();
			this._state = 0;
			this._options = new ClientWebSocketOptions
			{
				Proxy = ClientWebSocket.DefaultWebProxy.Instance
			};
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, null, ".ctor");
			}
		}

		/// <summary>Gets the WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.ClientWebSocketOptions" />.The WebSocket options for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x000E8107 File Offset: 0x000E6307
		public ClientWebSocketOptions Options
		{
			get
			{
				return this._options;
			}
		}

		/// <summary>Gets the reason why the close handshake was initiated on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.The reason why the close handshake was initiated.</returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x000E8110 File Offset: 0x000E6310
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.CloseStatus;
				}
				return null;
			}
		}

		/// <summary>Gets a description of the reason why the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance was closed.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The description of the reason why the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance was closed.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x000E813F File Offset: 0x000E633F
		public override string CloseStatusDescription
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.CloseStatusDescription;
				}
				return null;
			}
		}

		/// <summary>Gets the supported WebSocket sub-protocol for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The supported WebSocket sub-protocol.</returns>
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000E815B File Offset: 0x000E635B
		public override string SubProtocol
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.SubProtocol;
				}
				return null;
			}
		}

		/// <summary>Get the WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketState" />.The WebSocket state of the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</returns>
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x000E8178 File Offset: 0x000E6378
		public override WebSocketState State
		{
			get
			{
				if (WebSocketHandle.IsValid(this._innerWebSocket))
				{
					return this._innerWebSocket.State;
				}
				ClientWebSocket.InternalState state = (ClientWebSocket.InternalState)this._state;
				if (state == ClientWebSocket.InternalState.Created)
				{
					return WebSocketState.None;
				}
				if (state != ClientWebSocket.InternalState.Connecting)
				{
					return WebSocketState.Closed;
				}
				return WebSocketState.Connecting;
			}
		}

		/// <summary>Connect to a WebSocket server as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="uri">The URI of the WebSocket server to connect to.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that the  operation should be canceled.</param>
		// Token: 0x060042C3 RID: 17091 RVA: 0x000E81B4 File Offset: 0x000E63B4
		public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException("This operation is not supported for a relative URI.", "uri");
			}
			if (uri.Scheme != "ws" && uri.Scheme != "wss")
			{
				throw new ArgumentException("Only Uris starting with 'ws://' or 'wss://' are supported.", "uri");
			}
			ClientWebSocket.InternalState internalState = (ClientWebSocket.InternalState)Interlocked.CompareExchange(ref this._state, 1, 0);
			if (internalState == ClientWebSocket.InternalState.Disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (internalState != ClientWebSocket.InternalState.Created)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
			this._options.SetToReadOnly();
			return this.ConnectAsyncCore(uri, cancellationToken);
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000E8268 File Offset: 0x000E6468
		private async Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken)
		{
			this._innerWebSocket = WebSocketHandle.Create();
			try
			{
				if (Interlocked.CompareExchange(ref this._state, 2, 1) != 1)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				await this._innerWebSocket.ConnectAsyncCore(uri, cancellationToken, this._options).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "ConnectAsyncCore");
				}
				throw;
			}
		}

		/// <summary>Send data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="buffer">The buffer containing the message to be sent.</param>
		/// <param name="messageType">Specifies whether the buffer is clear text or in a binary format.</param>
		/// <param name="endOfMessage">Specifies whether this is the final asynchronous send. Set to true if this is the final send; false otherwise.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x060042C5 RID: 17093 RVA: 0x000E82BB File Offset: 0x000E64BB
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x000E82D3 File Offset: 0x000E64D3
		public override ValueTask SendAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		/// <summary>Receive data on <see cref="T:System.Net.WebSockets.ClientWebSocket" /> as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation.</returns>
		/// <param name="buffer">The buffer to receive the response.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x060042C7 RID: 17095 RVA: 0x000E82EB File Offset: 0x000E64EB
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.ReceiveAsync(buffer, cancellationToken);
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x000E8300 File Offset: 0x000E6500
		public override ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.ReceiveAsync(buffer, cancellationToken);
		}

		/// <summary>Close the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x060042C9 RID: 17097 RVA: 0x000E8315 File Offset: 0x000E6515
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Close the output for the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.The task object representing the asynchronous operation.</returns>
		/// <param name="closeStatus">The WebSocket close status.</param>
		/// <param name="statusDescription">A description of the close status.</param>
		/// <param name="cancellationToken">A cancellation token used to propagate notification that this  operation should be canceled.</param>
		// Token: 0x060042CA RID: 17098 RVA: 0x000E832B File Offset: 0x000E652B
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			this.ThrowIfNotConnected();
			return this._innerWebSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		/// <summary>Aborts the connection and cancels any pending IO operations.</summary>
		// Token: 0x060042CB RID: 17099 RVA: 0x000E8341 File Offset: 0x000E6541
		public override void Abort()
		{
			if (this._state == 3)
			{
				return;
			}
			if (WebSocketHandle.IsValid(this._innerWebSocket))
			{
				this._innerWebSocket.Abort();
			}
			this.Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.WebSockets.ClientWebSocket" /> instance.</summary>
		// Token: 0x060042CC RID: 17100 RVA: 0x000E836B File Offset: 0x000E656B
		public override void Dispose()
		{
			if (Interlocked.Exchange(ref this._state, 3) == 3)
			{
				return;
			}
			if (WebSocketHandle.IsValid(this._innerWebSocket))
			{
				this._innerWebSocket.Dispose();
			}
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000E8395 File Offset: 0x000E6595
		private void ThrowIfNotConnected()
		{
			if (this._state == 3)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this._state != 2)
			{
				throw new InvalidOperationException("The WebSocket is not connected.");
			}
		}

		// Token: 0x04002859 RID: 10329
		private readonly ClientWebSocketOptions _options;

		// Token: 0x0400285A RID: 10330
		private WebSocketHandle _innerWebSocket;

		// Token: 0x0400285B RID: 10331
		private int _state;

		// Token: 0x0200082D RID: 2093
		private enum InternalState
		{
			// Token: 0x0400285D RID: 10333
			Created,
			// Token: 0x0400285E RID: 10334
			Connecting,
			// Token: 0x0400285F RID: 10335
			Connected,
			// Token: 0x04002860 RID: 10336
			Disposed
		}

		// Token: 0x0200082E RID: 2094
		internal sealed class DefaultWebProxy : IWebProxy
		{
			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x060042CE RID: 17102 RVA: 0x000E83C5 File Offset: 0x000E65C5
			public static ClientWebSocket.DefaultWebProxy Instance { get; } = new ClientWebSocket.DefaultWebProxy();

			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x060042CF RID: 17103 RVA: 0x000044FA File Offset: 0x000026FA
			// (set) Token: 0x060042D0 RID: 17104 RVA: 0x000044FA File Offset: 0x000026FA
			public ICredentials Credentials
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x060042D1 RID: 17105 RVA: 0x000044FA File Offset: 0x000026FA
			public Uri GetProxy(Uri destination)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060042D2 RID: 17106 RVA: 0x000044FA File Offset: 0x000026FA
			public bool IsBypassed(Uri host)
			{
				throw new NotSupportedException();
			}
		}
	}
}
