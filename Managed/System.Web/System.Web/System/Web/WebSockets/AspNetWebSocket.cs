using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace System.Web.WebSockets
{
	/// <summary>Represents a real-time full-duplex connection between a web server and a client in an ASP.NET application.</summary>
	// Token: 0x020006E2 RID: 1762
	public sealed class AspNetWebSocket : WebSocket
	{
		// Token: 0x06004A95 RID: 19093 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal AspNetWebSocket()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a status code that indicates why an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was closed.</summary>
		/// <returns>The status code.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x000C7F38 File Offset: 0x000C6138
		public override WebSocketCloseStatus? CloseStatus
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a status message that explains why an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was closed.</summary>
		/// <returns>The status message.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string CloseStatusDescription
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates the open or closed state of an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object.</summary>
		/// <returns>The current state.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x000C7F54 File Offset: 0x000C6154
		public override WebSocketState State
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return WebSocketState.None;
			}
		}

		/// <summary>Gets the name of an application-specific protocol that a remote client and a server can use to exchange data over an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> connection.</summary>
		/// <returns>The name of the protocol.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override string SubProtocol
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Cancels any pending I/O operations on the <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object and sets the state of the object so that it cannot be used to start additional I/O operations.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		// Token: 0x06004A9A RID: 19098 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Abort()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sends an asynchronous message to a client to close the connection. If the server initiates the request to close the connection, the method waits for the client to acknowledge the request before it returns.</summary>
		/// <returns>A reference to the operation.</returns>
		/// <param name="closeStatus">The status code of the close operation.</param>
		/// <param name="statusDescription">The status message of the close operation.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was previously disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Sending operations are unavailable.-or-Receiving operations are unavailable.</exception>
		// Token: 0x06004A9B RID: 19099 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sends an asynchronous message to a client to close the connection. If the server initiates the request to close the connection, the method returns without waiting for a response.</summary>
		/// <returns>A reference to the operation.</returns>
		/// <param name="closeStatus">The status code of the close operation.</param>
		/// <param name="statusDescription">The status message of the close operation.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The object was previously disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Sending operations are unavailable.</exception>
		// Token: 0x06004A9C RID: 19100 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Releases all resources used by an <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object.</summary>
		// Token: 0x06004A9D RID: 19101 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Receives a single message fragment from a remote client.</summary>
		/// <returns>A reference to the task of receiving a message.</returns>
		/// <param name="buffer">The array that contains the message data.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object was previously disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Receiving operations are unavailable.</exception>
		// Token: 0x06004A9E RID: 19102 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Sends a single message fragment to a remote client.</summary>
		/// <returns>A reference to the task of sending a message.</returns>
		/// <param name="buffer">The array that contains the message data.</param>
		/// <param name="messageType">The message type.</param>
		/// <param name="endOfMessage">true to indicate that a fragment is the end of a complete message; otherwise, false.</param>
		/// <param name="cancellationToken">The object that cancels a pending operation.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.WebSockets.AspNetWebSocket" /> object is in an aborted state.-or-Sending operations are unavailable.</exception>
		// Token: 0x06004A9F RID: 19103 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
