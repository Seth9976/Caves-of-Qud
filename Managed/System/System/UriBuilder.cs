using System;

namespace System
{
	/// <summary>Provides a custom constructor for uniform resource identifiers (URIs) and modifies URIs for the <see cref="T:System.Uri" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000149 RID: 329
	public class UriBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class.</summary>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public UriBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified URI.</summary>
		/// <param name="uri">A URI string. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.<paramref name="uri" /> is a zero length string or contains only spaces.-or- The parsing routine detected a scheme in an invalid form.-or- The parser detected more than two consecutive slashes in a URI that does not use the "file" scheme.-or- <paramref name="uri" /> is not a valid URI. </exception>
		// Token: 0x060008B1 RID: 2225 RVA: 0x0001FF54 File Offset: 0x0001E154
		public UriBuilder(string uri)
		{
			Uri uri2 = new Uri(uri, UriKind.RelativeOrAbsolute);
			if (uri2.IsAbsoluteUri)
			{
				this.Init(uri2);
				return;
			}
			uri = Uri.UriSchemeHttp + Uri.SchemeDelimiter + uri;
			this.Init(new Uri(uri));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="uri">An instance of the <see cref="T:System.Uri" /> class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uri" /> is null. </exception>
		// Token: 0x060008B2 RID: 2226 RVA: 0x00020004 File Offset: 0x0001E204
		public UriBuilder(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.Init(uri);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00020094 File Offset: 0x0001E294
		private void Init(Uri uri)
		{
			this._fragment = uri.Fragment;
			this._query = uri.Query;
			this._host = uri.Host;
			this._path = uri.AbsolutePath;
			this._port = uri.Port;
			this._scheme = uri.Scheme;
			this._schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (!string.IsNullOrEmpty(userInfo))
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this._password = userInfo.Substring(num + 1);
					this._username = userInfo.Substring(0, num);
				}
				else
				{
					this._username = userInfo;
				}
			}
			this.SetFieldsFromUri(uri);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme and host.</summary>
		/// <param name="schemeName">An Internet access protocol. </param>
		/// <param name="hostName">A DNS-style domain name or IP address. </param>
		// Token: 0x060008B4 RID: 2228 RVA: 0x0002014C File Offset: 0x0001E34C
		public UriBuilder(string schemeName, string hostName)
		{
			this.Scheme = schemeName;
			this.Host = hostName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, and port.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="portNumber">An IP port number for the service. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="portNumber" /> is less than -1 or greater than 65,535. </exception>
		// Token: 0x060008B5 RID: 2229 RVA: 0x000201D3 File Offset: 0x0001E3D3
		public UriBuilder(string scheme, string host, int portNumber)
			: this(scheme, host)
		{
			this.Port = portNumber;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, and path.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="port">An IP port number for the service. </param>
		/// <param name="pathValue">The path to the Internet resource. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than -1 or greater than 65,535. </exception>
		// Token: 0x060008B6 RID: 2230 RVA: 0x000201E4 File Offset: 0x0001E3E4
		public UriBuilder(string scheme, string host, int port, string pathValue)
			: this(scheme, host, port)
		{
			this.Path = pathValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.UriBuilder" /> class with the specified scheme, host, port number, path and query string or fragment identifier.</summary>
		/// <param name="scheme">An Internet access protocol. </param>
		/// <param name="host">A DNS-style domain name or IP address. </param>
		/// <param name="port">An IP port number for the service. </param>
		/// <param name="path">The path to the Internet resource. </param>
		/// <param name="extraValue">A query string or fragment identifier. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="extraValue" /> is neither null nor <see cref="F:System.String.Empty" />, nor does a valid fragment identifier begin with a number sign (#), nor a valid query string begin with a question mark (?). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than -1 or greater than 65,535. </exception>
		// Token: 0x060008B7 RID: 2231 RVA: 0x000201F8 File Offset: 0x0001E3F8
		public UriBuilder(string scheme, string host, int port, string path, string extraValue)
			: this(scheme, host, port, path)
		{
			try
			{
				this.Extra = extraValue;
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException("Extra portion of URI not valid.", "extraValue");
			}
		}

		// Token: 0x1700014D RID: 333
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00020244 File Offset: 0x0001E444
		private string Extra
		{
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length <= 0)
				{
					this.Fragment = string.Empty;
					this.Query = string.Empty;
					return;
				}
				if (value[0] == '#')
				{
					this.Fragment = value.Substring(1);
					return;
				}
				if (value[0] == '?')
				{
					int num = value.IndexOf('#');
					if (num == -1)
					{
						num = value.Length;
					}
					else
					{
						this.Fragment = value.Substring(num + 1);
					}
					this.Query = value.Substring(1, num - 1);
					return;
				}
				throw new ArgumentException("Extra portion of URI not valid.", "value");
			}
		}

		/// <summary>Gets or sets the fragment portion of the URI.</summary>
		/// <returns>The fragment portion of the URI. The fragment identifier ("#") is added to the beginning of the fragment.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000202E4 File Offset: 0x0001E4E4
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x000202EC File Offset: 0x0001E4EC
		public string Fragment
		{
			get
			{
				return this._fragment;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0 && value[0] != '#')
				{
					value = "#" + value;
				}
				this._fragment = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the Domain Name System (DNS) host name or IP address of a server.</summary>
		/// <returns>The DNS host name or IP address of the server.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00020327 File Offset: 0x0001E527
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00020330 File Offset: 0x0001E530
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._host = value;
				if (this._host.IndexOf(':') >= 0 && this._host[0] != '[')
				{
					this._host = "[" + this._host + "]";
				}
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the password associated with the user that accesses the URI.</summary>
		/// <returns>The password of the user that accesses the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00020390 File Offset: 0x0001E590
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00020398 File Offset: 0x0001E598
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._password = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the path to the resource referenced by the URI.</summary>
		/// <returns>The path to the resource referenced by the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000203B2 File Offset: 0x0001E5B2
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x000203BA File Offset: 0x0001E5BA
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = "/";
				}
				this._path = Uri.InternalEscapeString(value.Replace('\\', '/'));
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the port number of the URI.</summary>
		/// <returns>The port number of the URI.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The port cannot be set to a value less than -1 or greater than 65,535. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000203EA File Offset: 0x0001E5EA
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x000203F2 File Offset: 0x0001E5F2
		public int Port
		{
			get
			{
				return this._port;
			}
			set
			{
				if (value < -1 || value > 65535)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._port = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets any query information included in the URI.</summary>
		/// <returns>The query information included in the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00020419 File Offset: 0x0001E619
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00020421 File Offset: 0x0001E621
		public string Query
		{
			get
			{
				return this._query;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 0 && value[0] != '?')
				{
					value = "?" + value;
				}
				this._query = value;
				this._changed = true;
			}
		}

		/// <summary>Gets or sets the scheme name of the URI.</summary>
		/// <returns>The scheme of the URI.</returns>
		/// <exception cref="T:System.ArgumentException">The scheme cannot be set to an invalid scheme name. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002045C File Offset: 0x0001E65C
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00020464 File Offset: 0x0001E664
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				int num = value.IndexOf(':');
				if (num != -1)
				{
					value = value.Substring(0, num);
				}
				if (value.Length != 0)
				{
					if (!Uri.CheckSchemeName(value))
					{
						throw new ArgumentException("Invalid URI: The URI scheme is not valid.", "value");
					}
					value = value.ToLowerInvariant();
				}
				this._scheme = value;
				this._changed = true;
			}
		}

		/// <summary>Gets the <see cref="T:System.Uri" /> instance constructed by the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI constructed by the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI constructed by the <see cref="T:System.UriBuilder" /> properties is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000204C8 File Offset: 0x0001E6C8
		public Uri Uri
		{
			get
			{
				if (this._changed)
				{
					this._uri = new Uri(this.ToString());
					this.SetFieldsFromUri(this._uri);
					this._changed = false;
				}
				return this._uri;
			}
		}

		/// <summary>The user name associated with the user that accesses the URI.</summary>
		/// <returns>The user name of the user that accesses the URI.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x000204FC File Offset: 0x0001E6FC
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x00020504 File Offset: 0x0001E704
		public string UserName
		{
			get
			{
				return this._username;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this._username = value;
				this._changed = true;
			}
		}

		/// <summary>Compares an existing <see cref="T:System.Uri" /> instance with the contents of the <see cref="T:System.UriBuilder" /> for equality.</summary>
		/// <returns>true if <paramref name="rparam" /> represents the same <see cref="T:System.Uri" /> as the <see cref="T:System.Uri" /> constructed by this <see cref="T:System.UriBuilder" /> instance; otherwise, false.</returns>
		/// <param name="rparam">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008CA RID: 2250 RVA: 0x0002051E File Offset: 0x0001E71E
		public override bool Equals(object rparam)
		{
			return rparam != null && this.Uri.Equals(rparam.ToString());
		}

		/// <summary>Returns the hash code for the URI.</summary>
		/// <returns>The hash code generated for the URI.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060008CB RID: 2251 RVA: 0x00020536 File Offset: 0x0001E736
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00020544 File Offset: 0x0001E744
		private void SetFieldsFromUri(Uri uri)
		{
			this._fragment = uri.Fragment;
			this._query = uri.Query;
			this._host = uri.Host;
			this._path = uri.AbsolutePath;
			this._port = uri.Port;
			this._scheme = uri.Scheme;
			this._schemeDelimiter = (uri.HasAuthority ? Uri.SchemeDelimiter : ":");
			string userInfo = uri.UserInfo;
			if (userInfo.Length > 0)
			{
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					this._password = userInfo.Substring(num + 1);
					this._username = userInfo.Substring(0, num);
					return;
				}
				this._username = userInfo;
			}
		}

		/// <summary>Returns the display string for the specified <see cref="T:System.UriBuilder" /> instance.</summary>
		/// <returns>The string that contains the unescaped display string of the <see cref="T:System.UriBuilder" />.</returns>
		/// <exception cref="T:System.UriFormatException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The <see cref="T:System.UriBuilder" /> instance has a bad password. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008CD RID: 2253 RVA: 0x000205F8 File Offset: 0x0001E7F8
		public override string ToString()
		{
			if (this._username.Length == 0 && this._password.Length > 0)
			{
				throw new UriFormatException("Invalid URI: The username:password construct is badly formed.");
			}
			if (this._scheme.Length != 0)
			{
				UriParser syntax = UriParser.GetSyntax(this._scheme);
				if (syntax != null)
				{
					this._schemeDelimiter = ((syntax.InFact(UriSyntaxFlags.MustHaveAuthority) || (this._host.Length != 0 && syntax.NotAny(UriSyntaxFlags.MailToLikeUri) && syntax.InFact(UriSyntaxFlags.OptionalAuthority))) ? Uri.SchemeDelimiter : ":");
				}
				else
				{
					this._schemeDelimiter = ((this._host.Length != 0) ? Uri.SchemeDelimiter : ":");
				}
			}
			string text = ((this._scheme.Length != 0) ? (this._scheme + this._schemeDelimiter) : string.Empty);
			return string.Concat(new string[]
			{
				text,
				this._username,
				(this._password.Length > 0) ? (":" + this._password) : string.Empty,
				(this._username.Length > 0) ? "@" : string.Empty,
				this._host,
				(this._port != -1 && this._host.Length > 0) ? (":" + this._port.ToString()) : string.Empty,
				(this._host.Length > 0 && this._path.Length != 0 && this._path[0] != '/') ? "/" : string.Empty,
				this._path,
				this._query,
				this._fragment
			});
		}

		// Token: 0x0400056A RID: 1386
		private bool _changed = true;

		// Token: 0x0400056B RID: 1387
		private string _fragment = string.Empty;

		// Token: 0x0400056C RID: 1388
		private string _host = "localhost";

		// Token: 0x0400056D RID: 1389
		private string _password = string.Empty;

		// Token: 0x0400056E RID: 1390
		private string _path = "/";

		// Token: 0x0400056F RID: 1391
		private int _port = -1;

		// Token: 0x04000570 RID: 1392
		private string _query = string.Empty;

		// Token: 0x04000571 RID: 1393
		private string _scheme = "http";

		// Token: 0x04000572 RID: 1394
		private string _schemeDelimiter = Uri.SchemeDelimiter;

		// Token: 0x04000573 RID: 1395
		private Uri _uri;

		// Token: 0x04000574 RID: 1396
		private string _username = string.Empty;
	}
}
