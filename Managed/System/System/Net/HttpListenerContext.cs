using System;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x0200068E RID: 1678
	public sealed class HttpListenerContext
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x000B8FB2 File Offset: 0x000B71B2
		internal HttpListenerContext(HttpConnection cnc)
		{
			this.err_status = 400;
			base..ctor();
			this.cnc = cnc;
			this.request = new HttpListenerRequest(this);
			this.response = new HttpListenerResponse(this);
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000B8FE4 File Offset: 0x000B71E4
		// (set) Token: 0x060034E8 RID: 13544 RVA: 0x000B8FEC File Offset: 0x000B71EC
		internal int ErrorStatus
		{
			get
			{
				return this.err_status;
			}
			set
			{
				this.err_status = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x000B8FF5 File Offset: 0x000B71F5
		// (set) Token: 0x060034EA RID: 13546 RVA: 0x000B8FFD File Offset: 0x000B71FD
		internal string ErrorMessage
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060034EB RID: 13547 RVA: 0x000B9006 File Offset: 0x000B7206
		internal bool HaveError
		{
			get
			{
				return this.error != null;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000B9011 File Offset: 0x000B7211
		internal HttpConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060034ED RID: 13549 RVA: 0x000B9019 File Offset: 0x000B7219
		public HttpListenerRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request. </summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000B9021 File Offset: 0x000B7221
		public HttpListenerResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the client whose request is represented by this <see cref="T:System.Net.HttpListenerContext" /> object. </summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object that describes the client, or null if the <see cref="T:System.Net.HttpListener" /> that supplied this <see cref="T:System.Net.HttpListenerContext" /> does not require authentication.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060034EF RID: 13551 RVA: 0x000B9029 File Offset: 0x000B7229
		public IPrincipal User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000B9034 File Offset: 0x000B7234
		internal void ParseAuthentication(AuthenticationSchemes expectedSchemes)
		{
			if (expectedSchemes == AuthenticationSchemes.Anonymous)
			{
				return;
			}
			string text = this.request.Headers["Authorization"];
			if (text == null || text.Length < 2)
			{
				return;
			}
			string[] array = text.Split(new char[] { ' ' }, 2);
			if (string.Compare(array[0], "basic", true) == 0)
			{
				this.user = this.ParseBasicAuthentication(array[1]);
			}
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000B90A0 File Offset: 0x000B72A0
		internal IPrincipal ParseBasicAuthentication(string authData)
		{
			IPrincipal principal;
			try
			{
				string text = Encoding.Default.GetString(Convert.FromBase64String(authData));
				int num = text.IndexOf(':');
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				num = text.IndexOf('\\');
				string text3;
				if (num > 0)
				{
					text3 = text.Substring(num);
				}
				else
				{
					text3 = text;
				}
				principal = new GenericPrincipal(new HttpListenerBasicIdentity(text3, text2), new string[0]);
			}
			catch (Exception)
			{
				principal = null;
			}
			return principal;
		}

		/// <summary>Accept a WebSocket connection as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034F2 RID: 13554 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol  and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034F3 RID: 13555 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.-or- <paramref name="receiveBufferSize" /> is less than 16 bytes-or- <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034F4 RID: 13556 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, WebSocket keep-alive interval, and the internal buffer as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <param name="internalBuffer">An internal buffer to use for this operation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string-or- <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.-or- <paramref name="receiveBufferSize" /> is less than 16 bytes-or- <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034F5 RID: 13557 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal HttpListenerContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F02 RID: 7938
		private HttpListenerRequest request;

		// Token: 0x04001F03 RID: 7939
		private HttpListenerResponse response;

		// Token: 0x04001F04 RID: 7940
		private IPrincipal user;

		// Token: 0x04001F05 RID: 7941
		private HttpConnection cnc;

		// Token: 0x04001F06 RID: 7942
		private string error;

		// Token: 0x04001F07 RID: 7943
		private int err_status;

		// Token: 0x04001F08 RID: 7944
		internal HttpListener Listener;
	}
}
