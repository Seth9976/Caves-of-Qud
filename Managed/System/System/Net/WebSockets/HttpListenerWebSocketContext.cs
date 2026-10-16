using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using Unity;

namespace System.Net.WebSockets
{
	/// <summary>Provides access to information received by the <see cref="T:System.Net.HttpListener" /> class when accepting WebSocket connections.</summary>
	// Token: 0x0200082B RID: 2091
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x060042AE RID: 17070 RVA: 0x000E7F78 File Offset: 0x000E6178
		internal HttpListenerWebSocketContext(Uri requestUri, NameValueCollection headers, CookieCollection cookieCollection, IPrincipal user, bool isAuthenticated, bool isLocal, bool isSecureConnection, string origin, IEnumerable<string> secWebSocketProtocols, string secWebSocketVersion, string secWebSocketKey, WebSocket webSocket)
		{
			this._cookieCollection = new CookieCollection();
			this._cookieCollection.Add(cookieCollection);
			this._headers = new NameValueCollection(headers);
			this._user = HttpListenerWebSocketContext.CopyPrincipal(user);
			this._requestUri = requestUri;
			this._isAuthenticated = isAuthenticated;
			this._isLocal = isLocal;
			this._isSecureConnection = isSecureConnection;
			this._origin = origin;
			this._secWebSocketProtocols = secWebSocketProtocols;
			this._secWebSocketVersion = secWebSocketVersion;
			this._secWebSocketKey = secWebSocketKey;
			this._webSocket = webSocket;
		}

		/// <summary>Gets the URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.The URI requested by the WebSocket client.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x000E8002 File Offset: 0x000E6202
		public override Uri RequestUri
		{
			get
			{
				return this._requestUri;
			}
		}

		/// <summary>Gets the HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.The HTTP headers received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x000E800A File Offset: 0x000E620A
		public override NameValueCollection Headers
		{
			get
			{
				return this._headers;
			}
		}

		/// <summary>Gets the value of the Origin HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the Origin HTTP header.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x000E8012 File Offset: 0x000E6212
		public override string Origin
		{
			get
			{
				return this._origin;
			}
		}

		/// <summary>Gets the list of the Secure WebSocket protocols included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.The list of the Secure WebSocket protocols.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x000E801A File Offset: 0x000E621A
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				return this._secWebSocketProtocols;
			}
		}

		/// <summary>Gets the list of sub-protocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The list of sub-protocols requested by the WebSocket client.</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x000E8022 File Offset: 0x000E6222
		public override string SecWebSocketVersion
		{
			get
			{
				return this._secWebSocketVersion;
			}
		}

		/// <summary>Gets the value of the SecWebSocketKey HTTP header included in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.The value of the SecWebSocketKey HTTP header.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000E802A File Offset: 0x000E622A
		public override string SecWebSocketKey
		{
			get
			{
				return this._secWebSocketKey;
			}
		}

		/// <summary>Gets the cookies received by the <see cref="T:System.Net.HttpListener" /> object in the WebSocket opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.The cookies received by the <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000E8032 File Offset: 0x000E6232
		public override CookieCollection CookieCollection
		{
			get
			{
				return this._cookieCollection;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.The identity, authentication information, and security roles for the WebSocket client.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x000E803A File Offset: 0x000E623A
		public override IPrincipal User
		{
			get
			{
				return this._user;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket client is authenticated; otherwise false.</returns>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000E8042 File Offset: 0x000E6242
		public override bool IsAuthenticated
		{
			get
			{
				return this._isAuthenticated;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket client connected from the local machine; otherwise false.</returns>
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x000E804A File Offset: 0x000E624A
		public override bool IsLocal
		{
			get
			{
				return this._isLocal;
			}
		}

		/// <summary>Gets a value that indicates if the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the WebSocket connection is secured using SSL; otherwise false.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x060042B9 RID: 17081 RVA: 0x000E8052 File Offset: 0x000E6252
		public override bool IsSecureConnection
		{
			get
			{
				return this._isSecureConnection;
			}
		}

		/// <summary>Gets the WebSocket instance used to send and receive data over the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.The WebSocket instance.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x000E805A File Offset: 0x000E625A
		public override WebSocket WebSocket
		{
			get
			{
				return this._webSocket;
			}
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x000E8064 File Offset: 0x000E6264
		private static IPrincipal CopyPrincipal(IPrincipal user)
		{
			if (user != null)
			{
				if (user is WindowsPrincipal)
				{
					throw new PlatformNotSupportedException();
				}
				HttpListenerBasicIdentity httpListenerBasicIdentity = user.Identity as HttpListenerBasicIdentity;
				if (httpListenerBasicIdentity != null)
				{
					return new GenericPrincipal(new HttpListenerBasicIdentity(httpListenerBasicIdentity.Name, httpListenerBasicIdentity.Password), null);
				}
			}
			return null;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal HttpListenerWebSocketContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400284D RID: 10317
		private readonly Uri _requestUri;

		// Token: 0x0400284E RID: 10318
		private readonly NameValueCollection _headers;

		// Token: 0x0400284F RID: 10319
		private readonly CookieCollection _cookieCollection;

		// Token: 0x04002850 RID: 10320
		private readonly IPrincipal _user;

		// Token: 0x04002851 RID: 10321
		private readonly bool _isAuthenticated;

		// Token: 0x04002852 RID: 10322
		private readonly bool _isLocal;

		// Token: 0x04002853 RID: 10323
		private readonly bool _isSecureConnection;

		// Token: 0x04002854 RID: 10324
		private readonly string _origin;

		// Token: 0x04002855 RID: 10325
		private readonly IEnumerable<string> _secWebSocketProtocols;

		// Token: 0x04002856 RID: 10326
		private readonly string _secWebSocketVersion;

		// Token: 0x04002857 RID: 10327
		private readonly string _secWebSocketKey;

		// Token: 0x04002858 RID: 10328
		private readonly WebSocket _webSocket;
	}
}
