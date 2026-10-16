using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.WebSockets
{
	/// <summary>Options to use with a  <see cref="T:System.Net.WebSockets.ClientWebSocket" /> object.</summary>
	// Token: 0x02000830 RID: 2096
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x060042D7 RID: 17111 RVA: 0x000E8512 File Offset: 0x000E6712
		internal ClientWebSocketOptions()
		{
			this._requestedSubProtocols = new List<string>();
			this._requestHeaders = new WebHeaderCollection();
		}

		/// <summary>Creates a HTTP request header and its value.</summary>
		/// <param name="headerName">The name of the HTTP header.</param>
		/// <param name="headerValue">The value of the HTTP header.</param>
		// Token: 0x060042D8 RID: 17112 RVA: 0x000E8551 File Offset: 0x000E6751
		public void SetRequestHeader(string headerName, string headerValue)
		{
			this.ThrowIfReadOnly();
			this._requestHeaders.Set(headerName, headerValue);
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x000E8566 File Offset: 0x000E6766
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this._requestHeaders;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x000E856E File Offset: 0x000E676E
		internal List<string> RequestedSubProtocols
		{
			get
			{
				return this._requestedSubProtocols;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates if default credentials should be used during WebSocket handshake.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if default credentials should be used during WebSocket handshake; otherwise false. The default is true.</returns>
		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x000E8576 File Offset: 0x000E6776
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x000E857E File Offset: 0x000E677E
		public bool UseDefaultCredentials
		{
			get
			{
				return this._useDefaultCredentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._useDefaultCredentials = value;
			}
		}

		/// <summary>Gets or sets the credential information for the client.</summary>
		/// <returns>Returns <see cref="T:System.Net.ICredentials" />.The credential information for the client.</returns>
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x000E858D File Offset: 0x000E678D
		// (set) Token: 0x060042DE RID: 17118 RVA: 0x000E8595 File Offset: 0x000E6795
		public ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._credentials = value;
			}
		}

		/// <summary>Gets or sets the proxy for WebSocket requests.</summary>
		/// <returns>Returns <see cref="T:System.Net.IWebProxy" />.The proxy for WebSocket requests.</returns>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x000E85A4 File Offset: 0x000E67A4
		// (set) Token: 0x060042E0 RID: 17120 RVA: 0x000E85AC File Offset: 0x000E67AC
		public IWebProxy Proxy
		{
			get
			{
				return this._proxy;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets a collection of client side certificates.</summary>
		/// <returns>Returns <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.A collection of client side certificates.</returns>
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x000E85BB File Offset: 0x000E67BB
		// (set) Token: 0x060042E2 RID: 17122 RVA: 0x000E85D6 File Offset: 0x000E67D6
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this._clientCertificates == null)
				{
					this._clientCertificates = new X509CertificateCollection();
				}
				return this._clientCertificates;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientCertificates = value;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x000E85F3 File Offset: 0x000E67F3
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x000E85FB File Offset: 0x000E67FB
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			get
			{
				return this._remoteCertificateValidationCallback;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._remoteCertificateValidationCallback = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieContainer" />.The cookies associated with the request.</returns>
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x000E860A File Offset: 0x000E680A
		// (set) Token: 0x060042E6 RID: 17126 RVA: 0x000E8612 File Offset: 0x000E6812
		public CookieContainer Cookies
		{
			get
			{
				return this._cookies;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._cookies = value;
			}
		}

		/// <summary>Adds a sub-protocol to be negotiated during the WebSocket connection handshake.</summary>
		/// <param name="subProtocol">The WebSocket sub-protocol to add.</param>
		// Token: 0x060042E7 RID: 17127 RVA: 0x000E8624 File Offset: 0x000E6824
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketValidate.ValidateSubprotocol(subProtocol);
			using (List<string>.Enumerator enumerator = this._requestedSubProtocols.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Equals(enumerator.Current, subProtocol, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(SR.Format("Duplicate protocols are not allowed: '{0}'.", subProtocol), "subProtocol");
					}
				}
			}
			this._requestedSubProtocols.Add(subProtocol);
		}

		/// <summary>Gets or sets the WebSocket protocol keep-alive interval in milliseconds.</summary>
		/// <returns>Returns <see cref="T:System.TimeSpan" />.The WebSocket protocol keep-alive interval in milliseconds.</returns>
		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x000E86A8 File Offset: 0x000E68A8
		// (set) Token: 0x060042E9 RID: 17129 RVA: 0x000E86B0 File Offset: 0x000E68B0
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this._keepAliveInterval;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value != Timeout.InfiniteTimeSpan && value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format("The argument must be a value greater than {0}.", Timeout.InfiniteTimeSpan.ToString()));
				}
				this._keepAliveInterval = value;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x000E870F File Offset: 0x000E690F
		internal int ReceiveBufferSize
		{
			get
			{
				return this._receiveBufferSize;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x000E8717 File Offset: 0x000E6917
		internal int SendBufferSize
		{
			get
			{
				return this._sendBufferSize;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x000E871F File Offset: 0x000E691F
		internal ArraySegment<byte>? Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Sets the client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		// Token: 0x060042ED RID: 17133 RVA: 0x000E8728 File Offset: 0x000E6928
		public void SetBuffer(int receiveBufferSize, int sendBufferSize)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = null;
		}

		/// <summary>Sets client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		/// <param name="buffer">The receive buffer to use.</param>
		// Token: 0x060042EE RID: 17134 RVA: 0x000E87A0 File Offset: 0x000E69A0
		public void SetBuffer(int receiveBufferSize, int sendBufferSize, ArraySegment<byte> buffer)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			if (buffer.Count == 0)
			{
				throw new ArgumentOutOfRangeException("buffer");
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = new ArraySegment<byte>?(buffer);
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000E8836 File Offset: 0x000E6A36
		internal void SetToReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000E883F File Offset: 0x000E6A3F
		private void ThrowIfReadOnly()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
		}

		// Token: 0x04002868 RID: 10344
		private bool _isReadOnly;

		// Token: 0x04002869 RID: 10345
		private readonly List<string> _requestedSubProtocols;

		// Token: 0x0400286A RID: 10346
		private readonly WebHeaderCollection _requestHeaders;

		// Token: 0x0400286B RID: 10347
		private TimeSpan _keepAliveInterval = WebSocket.DefaultKeepAliveInterval;

		// Token: 0x0400286C RID: 10348
		private bool _useDefaultCredentials;

		// Token: 0x0400286D RID: 10349
		private ICredentials _credentials;

		// Token: 0x0400286E RID: 10350
		private IWebProxy _proxy;

		// Token: 0x0400286F RID: 10351
		private X509CertificateCollection _clientCertificates;

		// Token: 0x04002870 RID: 10352
		private CookieContainer _cookies;

		// Token: 0x04002871 RID: 10353
		private int _receiveBufferSize = 4096;

		// Token: 0x04002872 RID: 10354
		private int _sendBufferSize = 4096;

		// Token: 0x04002873 RID: 10355
		private ArraySegment<byte>? _buffer;

		// Token: 0x04002874 RID: 10356
		private RemoteCertificateValidationCallback _remoteCertificateValidationCallback;
	}
}
