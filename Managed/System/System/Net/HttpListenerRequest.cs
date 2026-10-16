using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Describes an incoming HTTP request to an <see cref="T:System.Net.HttpListener" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000690 RID: 1680
	public sealed class HttpListenerRequest
	{
		// Token: 0x06003504 RID: 13572 RVA: 0x000B9282 File Offset: 0x000B7482
		internal HttpListenerRequest(HttpListenerContext context)
		{
			this.context = context;
			this.headers = new WebHeaderCollection();
			this.version = HttpVersion.Version10;
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000B92A8 File Offset: 0x000B74A8
		internal void SetRequestLine(string req)
		{
			string[] array = req.Split(HttpListenerRequest.separators, 3);
			if (array.Length != 3)
			{
				this.context.ErrorMessage = "Invalid request line (parts).";
				return;
			}
			this.method = array[0];
			foreach (char c in this.method)
			{
				int num = (int)c;
				if ((num < 65 || num > 90) && (num <= 32 || c >= '\u007f' || c == '(' || c == ')' || c == '<' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}'))
				{
					this.context.ErrorMessage = "(Invalid verb)";
					return;
				}
			}
			this.raw_url = array[1];
			if (array[2].Length != 8 || !array[2].StartsWith("HTTP/"))
			{
				this.context.ErrorMessage = "Invalid request line (version).";
				return;
			}
			try
			{
				this.version = new Version(array[2].Substring(5));
				if (this.version.Major < 1)
				{
					throw new Exception();
				}
			}
			catch
			{
				this.context.ErrorMessage = "Invalid request line (version).";
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000B9410 File Offset: 0x000B7610
		private void CreateQueryString(string query)
		{
			if (query == null || query.Length == 0)
			{
				this.query_string = new NameValueCollection(1);
				return;
			}
			this.query_string = new NameValueCollection();
			if (query[0] == '?')
			{
				query = query.Substring(1);
			}
			foreach (string text in query.Split('&', StringSplitOptions.None))
			{
				int num = text.IndexOf('=');
				if (num == -1)
				{
					this.query_string.Add(null, WebUtility.UrlDecode(text));
				}
				else
				{
					string text2 = WebUtility.UrlDecode(text.Substring(0, num));
					string text3 = WebUtility.UrlDecode(text.Substring(num + 1));
					this.query_string.Add(text2, text3);
				}
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000B94C0 File Offset: 0x000B76C0
		private static bool MaybeUri(string s)
		{
			int num = s.IndexOf(':');
			return num != -1 && num < 10 && HttpListenerRequest.IsPredefinedScheme(s.Substring(0, num));
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000B94F0 File Offset: 0x000B76F0
		private static bool IsPredefinedScheme(string scheme)
		{
			if (scheme == null || scheme.Length < 3)
			{
				return false;
			}
			char c = scheme[0];
			if (c == 'h')
			{
				return scheme == "http" || scheme == "https";
			}
			if (c == 'f')
			{
				return scheme == "file" || scheme == "ftp";
			}
			if (c != 'n')
			{
				return (c == 'g' && scheme == "gopher") || (c == 'm' && scheme == "mailto");
			}
			c = scheme[1];
			if (c == 'e')
			{
				return scheme == "news" || scheme == "net.pipe" || scheme == "net.tcp";
			}
			return scheme == "nntp";
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000B95C8 File Offset: 0x000B77C8
		internal bool FinishInitialization()
		{
			string text = this.UserHostName;
			if (this.version > HttpVersion.Version10 && (text == null || text.Length == 0))
			{
				this.context.ErrorMessage = "Invalid host name";
				return true;
			}
			Uri uri = null;
			string pathAndQuery;
			if (HttpListenerRequest.MaybeUri(this.raw_url.ToLowerInvariant()) && Uri.TryCreate(this.raw_url, UriKind.Absolute, out uri))
			{
				pathAndQuery = uri.PathAndQuery;
			}
			else
			{
				pathAndQuery = this.raw_url;
			}
			if (text == null || text.Length == 0)
			{
				text = this.UserHostAddress;
			}
			if (uri != null)
			{
				text = uri.Host;
			}
			int num = text.IndexOf(':');
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			string text2 = string.Format("{0}://{1}:{2}", this.IsSecureConnection ? "https" : "http", text, this.LocalEndPoint.Port);
			if (!Uri.TryCreate(text2 + pathAndQuery, UriKind.Absolute, out this.url))
			{
				this.context.ErrorMessage = WebUtility.HtmlEncode("Invalid url: " + text2 + pathAndQuery);
				return true;
			}
			this.CreateQueryString(this.url.Query);
			this.url = HttpListenerRequestUriBuilder.GetRequestUri(this.raw_url, this.url.Scheme, this.url.Authority, this.url.LocalPath, this.url.Query);
			if (this.version >= HttpVersion.Version11)
			{
				string text3 = this.Headers["Transfer-Encoding"];
				this.is_chunked = text3 != null && string.Compare(text3, "chunked", StringComparison.OrdinalIgnoreCase) == 0;
				if (text3 != null && !this.is_chunked)
				{
					this.context.Connection.SendError(null, 501);
					return false;
				}
			}
			if (!this.is_chunked && !this.cl_set && (string.Compare(this.method, "POST", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.method, "PUT", StringComparison.OrdinalIgnoreCase) == 0))
			{
				this.context.Connection.SendError(null, 411);
				return false;
			}
			if (string.Compare(this.Headers["Expect"], "100-continue", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.context.Connection.GetResponseStream().InternalWrite(HttpListenerRequest._100continue, 0, HttpListenerRequest._100continue.Length);
			}
			return true;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000B9820 File Offset: 0x000B7A20
		internal static string Unquote(string str)
		{
			int num = str.IndexOf('"');
			int num2 = str.LastIndexOf('"');
			if (num >= 0 && num2 >= 0)
			{
				str = str.Substring(num + 1, num2 - 1);
			}
			return str.Trim();
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000B985C File Offset: 0x000B7A5C
		internal void AddHeader(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1 || num == 0)
			{
				this.context.ErrorMessage = "Bad Request";
				this.context.ErrorStatus = 400;
				return;
			}
			string text = header.Substring(0, num).Trim();
			string text2 = header.Substring(num + 1).Trim();
			string text3 = text.ToLower(CultureInfo.InvariantCulture);
			this.headers.SetInternal(text, text2);
			if (text3 == "accept-language")
			{
				this.user_languages = text2.Split(',', StringSplitOptions.None);
				return;
			}
			if (!(text3 == "accept"))
			{
				if (!(text3 == "content-length"))
				{
					if (!(text3 == "referer"))
					{
						if (!(text3 == "cookie"))
						{
							return;
						}
						goto IL_0142;
					}
				}
				else
				{
					try
					{
						this.content_length = long.Parse(text2.Trim());
						if (this.content_length < 0L)
						{
							this.context.ErrorMessage = "Invalid Content-Length.";
						}
						this.cl_set = true;
						return;
					}
					catch
					{
						this.context.ErrorMessage = "Invalid Content-Length.";
						return;
					}
				}
				try
				{
					this.referrer = new Uri(text2);
					return;
				}
				catch
				{
					this.referrer = new Uri("http://someone.is.screwing.with.the.headers.com/");
					return;
				}
				IL_0142:
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				string[] array = text2.Split(new char[] { ',', ';' });
				Cookie cookie = null;
				int num2 = 0;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text4 = array2[i].Trim();
					if (text4.Length != 0)
					{
						if (text4.StartsWith("$Version"))
						{
							num2 = int.Parse(HttpListenerRequest.Unquote(text4.Substring(text4.IndexOf('=') + 1)));
						}
						else if (text4.StartsWith("$Path"))
						{
							if (cookie != null)
							{
								cookie.Path = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Domain"))
						{
							if (cookie != null)
							{
								cookie.Domain = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else if (text4.StartsWith("$Port"))
						{
							if (cookie != null)
							{
								cookie.Port = text4.Substring(text4.IndexOf('=') + 1).Trim();
							}
						}
						else
						{
							if (cookie != null)
							{
								this.cookies.Add(cookie);
							}
							try
							{
								cookie = new Cookie();
								int num3 = text4.IndexOf('=');
								if (num3 > 0)
								{
									cookie.Name = text4.Substring(0, num3).Trim();
									cookie.Value = text4.Substring(num3 + 1).Trim();
								}
								else
								{
									cookie.Name = text4.Trim();
									cookie.Value = string.Empty;
								}
								cookie.Version = num2;
							}
							catch (CookieException)
							{
								cookie = null;
							}
						}
					}
				}
				if (cookie != null)
				{
					this.cookies.Add(cookie);
				}
				return;
			}
			this.accept_types = text2.Split(',', StringSplitOptions.None);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000B9BA4 File Offset: 0x000B7DA4
		internal bool FlushInput()
		{
			if (!this.HasEntityBody)
			{
				return true;
			}
			int num = 2048;
			if (this.content_length > 0L)
			{
				num = (int)Math.Min(this.content_length, (long)num);
			}
			byte[] array = new byte[num];
			bool flag;
			for (;;)
			{
				try
				{
					IAsyncResult asyncResult = this.InputStream.BeginRead(array, 0, num, null, null);
					if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(1000))
					{
						flag = false;
					}
					else
					{
						if (this.InputStream.EndRead(asyncResult) > 0)
						{
							continue;
						}
						flag = true;
					}
				}
				catch (ObjectDisposedException)
				{
					this.input_stream = null;
					flag = true;
				}
				catch
				{
					flag = false;
				}
				break;
			}
			return flag;
		}

		/// <summary>Gets the MIME types accepted by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the type names specified in the request's Accept header or null if the client request did not include an Accept header.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000B9C54 File Offset: 0x000B7E54
		public string[] AcceptTypes
		{
			get
			{
				return this.accept_types;
			}
		}

		/// <summary>Gets an error code that identifies a problem with the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> provided by the client.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains a Windows error code.</returns>
		/// <exception cref="T:System.InvalidOperationException">The client certificate has not been initialized yet by a call to the <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /> or <see cref="M:System.Net.HttpListenerRequest.GetClientCertificate" /> methods-or - The operation is still in progress.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x000B9C5C File Offset: 0x000B7E5C
		public int ClientCertificateError
		{
			get
			{
				HttpConnection connection = this.context.Connection;
				if (connection.ClientCertificate == null)
				{
					throw new InvalidOperationException("No client certificate");
				}
				int[] clientCertificateErrors = connection.ClientCertificateErrors;
				if (clientCertificateErrors != null && clientCertificateErrors.Length != 0)
				{
					return clientCertificateErrors[0];
				}
				return 0;
			}
		}

		/// <summary>Gets the content encoding that can be used with data sent with the request</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object suitable for use with the data in the <see cref="P:System.Net.HttpListenerRequest.InputStream" /> property.</returns>
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600350F RID: 13583 RVA: 0x000B9C99 File Offset: 0x000B7E99
		public Encoding ContentEncoding
		{
			get
			{
				if (this.content_encoding == null)
				{
					this.content_encoding = Encoding.Default;
				}
				return this.content_encoding;
			}
		}

		/// <summary>Gets the length of the body data included in the request.</summary>
		/// <returns>The value from the request's Content-Length header. This value is -1 if the content length is not known.</returns>
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000B9CB4 File Offset: 0x000B7EB4
		public long ContentLength64
		{
			get
			{
				if (!this.is_chunked)
				{
					return this.content_length;
				}
				return -1L;
			}
		}

		/// <summary>Gets the MIME type of the body data included in the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the request's Content-Type header.</returns>
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003511 RID: 13585 RVA: 0x000B9CC7 File Offset: 0x000B7EC7
		public string ContentType
		{
			get
			{
				return this.headers["content-type"];
			}
		}

		/// <summary>Gets the cookies sent with the request.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains cookies that accompany the request. This property returns an empty collection if the request does not contain cookies.</returns>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000B9CD9 File Offset: 0x000B7ED9
		public CookieCollection Cookies
		{
			get
			{
				if (this.cookies == null)
				{
					this.cookies = new CookieCollection();
				}
				return this.cookies;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request has associated body data.</summary>
		/// <returns>true if the request has associated body data; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000B9CF4 File Offset: 0x000B7EF4
		public bool HasEntityBody
		{
			get
			{
				return this.content_length > 0L || this.is_chunked;
			}
		}

		/// <summary>Gets the collection of header name/value pairs sent in the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the HTTP headers included in the request.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003514 RID: 13588 RVA: 0x000B9D08 File Offset: 0x000B7F08
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets the HTTP method specified by the client. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the method used in the request.</returns>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000B9D10 File Offset: 0x000B7F10
		public string HttpMethod
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Gets a stream that contains the body data sent by the client.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> object that contains the bytes sent by the client in the body of the request. This property returns <see cref="F:System.IO.Stream.Null" /> if no data is sent with the request.</returns>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003516 RID: 13590 RVA: 0x000B9D18 File Offset: 0x000B7F18
		public Stream InputStream
		{
			get
			{
				if (this.input_stream == null)
				{
					if (this.is_chunked || this.content_length > 0L)
					{
						this.input_stream = this.context.Connection.GetRequestStream(this.is_chunked, this.content_length);
					}
					else
					{
						this.input_stream = Stream.Null;
					}
				}
				return this.input_stream;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client sending this request is authenticated.</summary>
		/// <returns>true if the client was authenticated; otherwise, false.</returns>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO("Always returns false")]
		public bool IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the request is sent from the local computer.</summary>
		/// <returns>true if the request originated on the same computer as the <see cref="T:System.Net.HttpListener" /> object that provided the request; otherwise, false.</returns>
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000B9D74 File Offset: 0x000B7F74
		public bool IsLocal
		{
			get
			{
				return this.LocalEndPoint.Address.Equals(this.RemoteEndPoint.Address);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection used to send the request is using the Secure Sockets Layer (SSL) protocol.</summary>
		/// <returns>true if the TCP connection is using SSL; otherwise, false.</returns>
		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000B9D91 File Offset: 0x000B7F91
		public bool IsSecureConnection
		{
			get
			{
				return this.context.Connection.IsSecure;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the client requests a persistent connection.</summary>
		/// <returns>true if the connection should be kept open; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600351A RID: 13594 RVA: 0x000B9DA4 File Offset: 0x000B7FA4
		public bool KeepAlive
		{
			get
			{
				if (this.ka_set)
				{
					return this.keep_alive;
				}
				this.ka_set = true;
				string text = this.headers["Connection"];
				if (!string.IsNullOrEmpty(text))
				{
					this.keep_alive = string.Compare(text, "keep-alive", StringComparison.OrdinalIgnoreCase) == 0;
				}
				else if (this.version == HttpVersion.Version11)
				{
					this.keep_alive = true;
				}
				else
				{
					text = this.headers["keep-alive"];
					if (!string.IsNullOrEmpty(text))
					{
						this.keep_alive = string.Compare(text, "closed", StringComparison.OrdinalIgnoreCase) != 0;
					}
				}
				return this.keep_alive;
			}
		}

		/// <summary>Get the server IP address and port number to which the request is directed.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address that the request is sent to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000B9E46 File Offset: 0x000B8046
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.context.Connection.LocalEndPoint;
			}
		}

		/// <summary>Gets the HTTP version used by the requesting client.</summary>
		/// <returns>A <see cref="T:System.Version" /> that identifies the client's version of HTTP.</returns>
		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600351C RID: 13596 RVA: 0x000B9E58 File Offset: 0x000B8058
		public Version ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Gets the query string included in the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the query data included in the request <see cref="P:System.Net.HttpListenerRequest.Url" />.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000B9E60 File Offset: 0x000B8060
		public NameValueCollection QueryString
		{
			get
			{
				return this.query_string;
			}
		}

		/// <summary>Gets the URL information (without the host and port) requested by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the raw URL for this request.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600351E RID: 13598 RVA: 0x000B9E68 File Offset: 0x000B8068
		public string RawUrl
		{
			get
			{
				return this.raw_url;
			}
		}

		/// <summary>Gets the client IP address and port number from which the request originated.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> that represents the IP address and port number from which the request originated.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000B9E70 File Offset: 0x000B8070
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.context.Connection.RemoteEndPoint;
			}
		}

		/// <summary>Gets the request identifier of the incoming HTTP request.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object that contains the identifier of the HTTP request.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000B9E82 File Offset: 0x000B8082
		[MonoTODO("Always returns Guid.Empty")]
		public Guid RequestTraceIdentifier
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> object requested by the client.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that identifies the resource requested by the client.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000B9E89 File Offset: 0x000B8089
		public Uri Url
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the resource that referred the client to the server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> object that contains the text of the request's <see cref="F:System.Net.HttpRequestHeader.Referer" /> header, or null if the header was not included in the request.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000B9E91 File Offset: 0x000B8091
		public Uri UrlReferrer
		{
			get
			{
				return this.referrer;
			}
		}

		/// <summary>Gets the user agent presented by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the text of the request's User-Agent header.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000B9E99 File Offset: 0x000B8099
		public string UserAgent
		{
			get
			{
				return this.headers["user-agent"];
			}
		}

		/// <summary>Gets the server IP address and port number to which the request is directed.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the host address information.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000B9EAB File Offset: 0x000B80AB
		public string UserHostAddress
		{
			get
			{
				return this.LocalEndPoint.ToString();
			}
		}

		/// <summary>Gets the DNS name and, if provided, the port number specified by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the text of the request's Host header.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000B9EB8 File Offset: 0x000B80B8
		public string UserHostName
		{
			get
			{
				return this.headers["host"];
			}
		}

		/// <summary>Gets the natural languages that are preferred for the response.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains the languages specified in the request's <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header or null if the client request did not include an <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000B9ECA File Offset: 0x000B80CA
		public string[] UserLanguages
		{
			get
			{
				return this.user_languages;
			}
		}

		/// <summary>Begins an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that indicates the status of the operation.</returns>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the callback delegate when the operation completes.</param>
		// Token: 0x06003527 RID: 13607 RVA: 0x000B9ED2 File Offset: 0x000B80D2
		public IAsyncResult BeginGetClientCertificate(AsyncCallback requestCallback, object state)
		{
			if (this.gcc_delegate == null)
			{
				this.gcc_delegate = new HttpListenerRequest.GCCDelegate(this.GetClientCertificate);
			}
			return this.gcc_delegate.BeginInvoke(requestCallback, state);
		}

		/// <summary>Ends an asynchronous request for the client's X.509 v.3 certificate.</summary>
		/// <returns>The <see cref="T:System.IAsyncResult" /> object that is returned when the operation started.</returns>
		/// <param name="asyncResult">The pending request for the certificate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.HttpListenerRequest.BeginGetClientCertificate(System.AsyncCallback,System.Object)" /><paramref name="e." /></exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		// Token: 0x06003528 RID: 13608 RVA: 0x000B9EFB File Offset: 0x000B80FB
		public X509Certificate2 EndGetClientCertificate(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.gcc_delegate == null)
			{
				throw new InvalidOperationException();
			}
			return this.gcc_delegate.EndInvoke(asyncResult);
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate.</summary>
		/// <returns>A <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		/// <exception cref="T:System.InvalidOperationException">A call to this method to retrieve the client's X.509 v.3 certificate is in progress and therefore another call to this method cannot be made.</exception>
		// Token: 0x06003529 RID: 13609 RVA: 0x000B9F25 File Offset: 0x000B8125
		public X509Certificate2 GetClientCertificate()
		{
			return this.context.Connection.ClientCertificate;
		}

		/// <summary>Gets the Service Provider Name (SPN) that the client sent on the request.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the SPN the client sent on the request. </returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x00002F6A File Offset: 0x0000116A
		[MonoTODO]
		public string ServiceName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.TransportContext" /> for the client request.</summary>
		/// <returns>A <see cref="T:System.Net.TransportContext" /> object for the client request.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x000B9F37 File Offset: 0x000B8137
		public TransportContext TransportContext
		{
			get
			{
				return new HttpListenerRequest.Context();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the TCP connection was  a WebSocket request.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the TCP connection is a WebSocket request; otherwise, false.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO]
		public bool IsWebSocketRequest
		{
			get
			{
				return false;
			}
		}

		/// <summary>Retrieves the client's X.509 v.3 certificate as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="N:System.Security.Cryptography.X509Certificates" /> object that contains the client's X.509 v.3 certificate.</returns>
		// Token: 0x0600352D RID: 13613 RVA: 0x000B9F3E File Offset: 0x000B813E
		public Task<X509Certificate2> GetClientCertificateAsync()
		{
			return Task<X509Certificate2>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetClientCertificate), new Func<IAsyncResult, X509Certificate2>(this.EndGetClientCertificate), null);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal HttpListenerRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F0B RID: 7947
		private string[] accept_types;

		// Token: 0x04001F0C RID: 7948
		private Encoding content_encoding;

		// Token: 0x04001F0D RID: 7949
		private long content_length;

		// Token: 0x04001F0E RID: 7950
		private bool cl_set;

		// Token: 0x04001F0F RID: 7951
		private CookieCollection cookies;

		// Token: 0x04001F10 RID: 7952
		private WebHeaderCollection headers;

		// Token: 0x04001F11 RID: 7953
		private string method;

		// Token: 0x04001F12 RID: 7954
		private Stream input_stream;

		// Token: 0x04001F13 RID: 7955
		private Version version;

		// Token: 0x04001F14 RID: 7956
		private NameValueCollection query_string;

		// Token: 0x04001F15 RID: 7957
		private string raw_url;

		// Token: 0x04001F16 RID: 7958
		private Uri url;

		// Token: 0x04001F17 RID: 7959
		private Uri referrer;

		// Token: 0x04001F18 RID: 7960
		private string[] user_languages;

		// Token: 0x04001F19 RID: 7961
		private HttpListenerContext context;

		// Token: 0x04001F1A RID: 7962
		private bool is_chunked;

		// Token: 0x04001F1B RID: 7963
		private bool ka_set;

		// Token: 0x04001F1C RID: 7964
		private bool keep_alive;

		// Token: 0x04001F1D RID: 7965
		private HttpListenerRequest.GCCDelegate gcc_delegate;

		// Token: 0x04001F1E RID: 7966
		private static byte[] _100continue = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");

		// Token: 0x04001F1F RID: 7967
		private static char[] separators = new char[] { ' ' };

		// Token: 0x02000691 RID: 1681
		private class Context : TransportContext
		{
			// Token: 0x06003530 RID: 13616 RVA: 0x0000829A File Offset: 0x0000649A
			public override ChannelBinding GetChannelBinding(ChannelBindingKind kind)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x02000692 RID: 1682
		// (Invoke) Token: 0x06003533 RID: 13619
		private delegate X509Certificate2 GCCDelegate();
	}
}
