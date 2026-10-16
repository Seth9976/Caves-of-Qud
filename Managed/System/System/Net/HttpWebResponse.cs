using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides an HTTP-specific implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x0200069F RID: 1695
	[Serializable]
	public class HttpWebResponse : WebResponse, ISerializable, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class.</summary>
		// Token: 0x0600362B RID: 13867 RVA: 0x000BDC4E File Offset: 0x000BBE4E
		public HttpWebResponse()
		{
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000BDC58 File Offset: 0x000BBE58
		internal HttpWebResponse(Uri uri, string method, HttpStatusCode status, WebHeaderCollection headers)
		{
			this.uri = uri;
			this.method = method;
			this.statusCode = status;
			this.statusDescription = HttpStatusDescription.Get(status);
			this.webHeaders = headers;
			this.version = HttpVersion.Version10;
			this.contentLength = -1L;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000BDCA8 File Offset: 0x000BBEA8
		internal HttpWebResponse(Uri uri, string method, WebResponseStream stream, CookieContainer container)
		{
			this.uri = uri;
			this.method = method;
			this.stream = stream;
			this.webHeaders = stream.Headers ?? new WebHeaderCollection();
			this.version = stream.Version;
			this.statusCode = stream.StatusCode;
			this.statusDescription = stream.StatusDescription ?? HttpStatusDescription.Get(this.statusCode);
			this.contentLength = -1L;
			try
			{
				string text = this.webHeaders["Content-Length"];
				if (string.IsNullOrEmpty(text) || !long.TryParse(text, out this.contentLength))
				{
					this.contentLength = -1L;
				}
			}
			catch (Exception)
			{
				this.contentLength = -1L;
			}
			if (container != null)
			{
				this.cookie_container = container;
				this.FillCookies();
			}
			string text2 = this.webHeaders["Content-Encoding"];
			if (text2 == "gzip" && (stream.Request.AutomaticDecompression & DecompressionMethods.GZip) != DecompressionMethods.None)
			{
				this.stream = new GZipStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
				return;
			}
			if (text2 == "deflate" && (stream.Request.AutomaticDecompression & DecompressionMethods.Deflate) != DecompressionMethods.None)
			{
				this.stream = new DeflateStream(stream, CompressionMode.Decompress);
				this.webHeaders.Remove(HttpRequestHeader.ContentEncoding);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpWebResponse" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.HttpWebRequest" />. </param>
		// Token: 0x0600362E RID: 13870 RVA: 0x000BDDFC File Offset: 0x000BBFFC
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected HttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.uri = (Uri)serializationInfo.GetValue("uri", typeof(Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.contentType = serializationInfo.GetString("contentType");
			this.method = serializationInfo.GetString("method");
			this.statusDescription = serializationInfo.GetString("statusDescription");
			this.cookieCollection = (CookieCollection)serializationInfo.GetValue("cookieCollection", typeof(CookieCollection));
			this.version = (Version)serializationInfo.GetValue("version", typeof(Version));
			this.statusCode = (HttpStatusCode)serializationInfo.GetValue("statusCode", typeof(HttpStatusCode));
		}

		/// <summary>Gets the character set of the response.</summary>
		/// <returns>A string that contains the character set of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000BDED8 File Offset: 0x000BC0D8
		public string CharacterSet
		{
			get
			{
				string text = this.ContentType;
				if (text == null)
				{
					return "ISO-8859-1";
				}
				string text2 = text.ToLower();
				int num = text2.IndexOf("charset=", StringComparison.Ordinal);
				if (num == -1)
				{
					return "ISO-8859-1";
				}
				num += 8;
				int num2 = text2.IndexOf(';', num);
				if (num2 != -1)
				{
					return text.Substring(num, num2 - num);
				}
				return text.Substring(num);
			}
		}

		/// <summary>Gets the method that is used to encode the body of the response.</summary>
		/// <returns>A string that describes the method that is used to encode the body of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000BDF38 File Offset: 0x000BC138
		public string ContentEncoding
		{
			get
			{
				this.CheckDisposed();
				string text = this.webHeaders["Content-Encoding"];
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		/// <summary>Gets the length of the content returned by the request.</summary>
		/// <returns>The number of bytes returned by the request. Content length does not include header information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000BDF66 File Offset: 0x000BC166
		public override long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		/// <summary>Gets the content type of the response.</summary>
		/// <returns>A string that contains the content type of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000BDF6E File Offset: 0x000BC16E
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				if (this.contentType == null)
				{
					this.contentType = this.webHeaders["Content-Type"];
				}
				if (this.contentType == null)
				{
					this.contentType = string.Empty;
				}
				return this.contentType;
			}
		}

		/// <summary>Gets or sets the cookies that are associated with this response.</summary>
		/// <returns>A <see cref="T:System.Net.CookieCollection" /> that contains the cookies that are associated with this response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000BDFAD File Offset: 0x000BC1AD
		// (set) Token: 0x06003634 RID: 13876 RVA: 0x000BDFCE File Offset: 0x000BC1CE
		public virtual CookieCollection Cookies
		{
			get
			{
				this.CheckDisposed();
				if (this.cookieCollection == null)
				{
					this.cookieCollection = new CookieCollection();
				}
				return this.cookieCollection;
			}
			set
			{
				this.CheckDisposed();
				this.cookieCollection = value;
			}
		}

		/// <summary>Gets the headers that are associated with this response from the server.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header information returned with the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000BDFDD File Offset: 0x000BC1DD
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.webHeaders;
			}
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0001FD13 File Offset: 0x0001DF13
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both client and server were authenticated.</summary>
		/// <returns>true if mutual authentication occurred; otherwise, false.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000BDFE5 File Offset: 0x000BC1E5
		[MonoTODO]
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				throw HttpWebResponse.GetMustImplement();
			}
		}

		/// <summary>Gets the last date and time that the contents of the response were modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the date and time that the contents of the response were modified.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000BDFEC File Offset: 0x000BC1EC
		public DateTime LastModified
		{
			get
			{
				this.CheckDisposed();
				DateTime dateTime;
				try
				{
					dateTime = MonoHttpDate.Parse(this.webHeaders["Last-Modified"]);
				}
				catch (Exception)
				{
					dateTime = DateTime.Now;
				}
				return dateTime;
			}
		}

		/// <summary>Gets the method that is used to return the response.</summary>
		/// <returns>A string that contains the HTTP method that is used to return the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000BE034 File Offset: 0x000BC234
		public virtual string Method
		{
			get
			{
				this.CheckDisposed();
				return this.method;
			}
		}

		/// <summary>Gets the version of the HTTP protocol that is used in the response.</summary>
		/// <returns>A <see cref="T:System.Version" /> that contains the HTTP protocol version of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x000BE042 File Offset: 0x000BC242
		public Version ProtocolVersion
		{
			get
			{
				this.CheckDisposed();
				return this.version;
			}
		}

		/// <summary>Gets the URI of the Internet resource that responded to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the Internet resource that responded to the request.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000BE050 File Offset: 0x000BC250
		public override Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.uri;
			}
		}

		/// <summary>Gets the name of the server that sent the response.</summary>
		/// <returns>A string that contains the name of the server that sent the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000BE05E File Offset: 0x000BC25E
		public string Server
		{
			get
			{
				this.CheckDisposed();
				return this.webHeaders["Server"] ?? "";
			}
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.HttpStatusCode" /> values.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000BE07F File Offset: 0x000BC27F
		public virtual HttpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		/// <summary>Gets the status description returned with the response.</summary>
		/// <returns>A string that describes the status of the response.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x000BE087 File Offset: 0x000BC287
		public virtual string StatusDescription
		{
			get
			{
				this.CheckDisposed();
				return this.statusDescription;
			}
		}

		/// <summary>Gets a value that indicates if headers are supported.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if headers are supported; otherwise, false.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the contents of a header that was returned with the response.</summary>
		/// <returns>The contents of the specified header.</returns>
		/// <param name="headerName">The header value to return. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		// Token: 0x06003640 RID: 13888 RVA: 0x000BE098 File Offset: 0x000BC298
		public string GetResponseHeader(string headerName)
		{
			this.CheckDisposed();
			string text = this.webHeaders[headerName];
			if (text == null)
			{
				return "";
			}
			return text;
		}

		/// <summary>Gets the stream that is used to read the body of the response from the server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing the body of the response.</returns>
		/// <exception cref="T:System.Net.ProtocolViolationException">There is no response stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has been disposed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003641 RID: 13889 RVA: 0x000BE0C2 File Offset: 0x000BC2C2
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			if (this.stream == null)
			{
				return Stream.Null;
			}
			if (string.Equals(this.method, "HEAD", StringComparison.OrdinalIgnoreCase))
			{
				return Stream.Null;
			}
			return this.stream;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.HttpWebResponse" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x06003642 RID: 13890 RVA: 0x000ABB64 File Offset: 0x000A9D64
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06003643 RID: 13891 RVA: 0x000BE0F8 File Offset: 0x000BC2F8
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("uri", this.uri);
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("contentType", this.contentType);
			serializationInfo.AddValue("method", this.method);
			serializationInfo.AddValue("statusDescription", this.statusDescription);
			serializationInfo.AddValue("cookieCollection", this.cookieCollection);
			serializationInfo.AddValue("version", this.version);
			serializationInfo.AddValue("statusCode", this.statusCode);
		}

		/// <summary>Closes the response stream.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003644 RID: 13892 RVA: 0x000BE194 File Offset: 0x000BC394
		public override void Close()
		{
			Stream stream = Interlocked.Exchange<Stream>(ref this.stream, null);
			if (stream != null)
			{
				stream.Close();
			}
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000BE1B7 File Offset: 0x000BC3B7
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.HttpWebResponse" />, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to releases only unmanaged resources. </param>
		// Token: 0x06003646 RID: 13894 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		protected override void Dispose(bool disposing)
		{
			this.disposed = true;
			base.Dispose(true);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000BE1D0 File Offset: 0x000BC3D0
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000BE1EC File Offset: 0x000BC3EC
		private void FillCookies()
		{
			if (this.webHeaders == null)
			{
				return;
			}
			CookieCollection cookieCollection = null;
			try
			{
				string text = this.webHeaders.Get("Set-Cookie");
				if (text != null)
				{
					cookieCollection = this.cookie_container.CookieCutter(this.uri, "Set-Cookie", text, false);
				}
			}
			catch
			{
			}
			try
			{
				string text = this.webHeaders.Get("Set-Cookie2");
				if (text != null)
				{
					CookieCollection cookieCollection2 = this.cookie_container.CookieCutter(this.uri, "Set-Cookie2", text, false);
					if (cookieCollection != null && cookieCollection.Count != 0)
					{
						cookieCollection.Add(cookieCollection2);
					}
					else
					{
						cookieCollection = cookieCollection2;
					}
				}
			}
			catch
			{
			}
			this.cookieCollection = cookieCollection;
		}

		// Token: 0x04001FAD RID: 8109
		private Uri uri;

		// Token: 0x04001FAE RID: 8110
		private WebHeaderCollection webHeaders;

		// Token: 0x04001FAF RID: 8111
		private CookieCollection cookieCollection;

		// Token: 0x04001FB0 RID: 8112
		private string method;

		// Token: 0x04001FB1 RID: 8113
		private Version version;

		// Token: 0x04001FB2 RID: 8114
		private HttpStatusCode statusCode;

		// Token: 0x04001FB3 RID: 8115
		private string statusDescription;

		// Token: 0x04001FB4 RID: 8116
		private long contentLength;

		// Token: 0x04001FB5 RID: 8117
		private string contentType;

		// Token: 0x04001FB6 RID: 8118
		private CookieContainer cookie_container;

		// Token: 0x04001FB7 RID: 8119
		private bool disposed;

		// Token: 0x04001FB8 RID: 8120
		private Stream stream;
	}
}
