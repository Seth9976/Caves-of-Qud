using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that provides HTTP-response information from an ASP.NET operation.</summary>
	// Token: 0x020000B1 RID: 177
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpResponseWrapper : HttpResponseBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpResponseWrapper" /> class.</summary>
		/// <param name="httpResponse">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpResponse" /> parameter is null.</exception>
		// Token: 0x0600093F RID: 2367 RVA: 0x00016C6D File Offset: 0x00014E6D
		public HttpResponseWrapper(HttpResponse httpResponse)
		{
			if (httpResponse == null)
			{
				throw new ArgumentNullException("httpResponse");
			}
			this.w = httpResponse;
		}

		/// <summary>Gets or sets a value that indicates whether to buffer output and send it after the complete response has finished processing.</summary>
		/// <returns>true if the output is buffered; otherwise, false.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00016C8A File Offset: 0x00014E8A
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00016C97 File Offset: 0x00014E97
		public override bool Buffer
		{
			get
			{
				return this.w.Buffer;
			}
			set
			{
				this.w.Buffer = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to buffer output and send it after the complete page has finished processing.</summary>
		/// <returns>true if the output is buffered; otherwise false. The default is true.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00016CA5 File Offset: 0x00014EA5
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00016CB2 File Offset: 0x00014EB2
		public override bool BufferOutput
		{
			get
			{
				return this.w.BufferOutput;
			}
			set
			{
				this.w.BufferOutput = value;
			}
		}

		/// <summary>Gets the caching policy (such as expiration time, privacy settings, and vary clauses) of the current Web page.</summary>
		/// <returns>The caching policy of the current response.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00016CC0 File Offset: 0x00014EC0
		public override HttpCachePolicyBase Cache
		{
			get
			{
				return new HttpCachePolicyWrapper(this.w.Cache);
			}
		}

		/// <summary>Gets or sets the Cache-Control HTTP header that matches one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</summary>
		/// <returns>The caching policy of the current response.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00016CD2 File Offset: 0x00014ED2
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00016CDF File Offset: 0x00014EDF
		public override string CacheControl
		{
			get
			{
				return this.w.CacheControl;
			}
			set
			{
				this.w.CacheControl = value;
			}
		}

		/// <summary>Gets or sets the HTTP character set of the current response.</summary>
		/// <returns>The HTTP character set of the current response.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00016CED File Offset: 0x00014EED
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x00016CFA File Offset: 0x00014EFA
		public override string Charset
		{
			get
			{
				return this.w.Charset;
			}
			set
			{
				this.w.Charset = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when the client disconnects.</summary>
		/// <returns>The cancellation token.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00016D08 File Offset: 0x00014F08
		public override CancellationToken ClientDisconnectedToken
		{
			get
			{
				return CancellationToken.None;
			}
		}

		/// <summary>Gets or sets the content encoding of the current response.</summary>
		/// <returns>Information about the content encoding of the current response.</returns>
		/// <exception cref="T:System.ArgumentNullException">Attempted to set <see cref="P:System.Web.HttpResponse.ContentEncoding" /> to null.</exception>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00016D0F File Offset: 0x00014F0F
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00016D1C File Offset: 0x00014F1C
		public override Encoding ContentEncoding
		{
			get
			{
				return this.w.ContentEncoding;
			}
			set
			{
				this.w.ContentEncoding = value;
			}
		}

		/// <summary>Gets or sets the HTTP MIME type of the current response.</summary>
		/// <returns>The HTTP MIME type of the current response. The default value is "text/html".</returns>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.HttpResponse.ContentType" /> property is set to null.</exception>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00016D2A File Offset: 0x00014F2A
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00016D37 File Offset: 0x00014F37
		public override string ContentType
		{
			get
			{
				return this.w.ContentType;
			}
			set
			{
				this.w.ContentType = value;
			}
		}

		/// <summary>Gets the response cookie collection.</summary>
		/// <returns>The response cookie collection.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00016D45 File Offset: 0x00014F45
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this.w.Cookies;
			}
		}

		/// <summary>Gets or sets the number of minutes before a page that is cached on the client or proxy expires. If the user returns to the same page before it expires, the cached version is displayed. <see cref="P:System.Web.HttpResponseWrapper.Expires" /> is provided for compatibility with earlier versions of ASP.</summary>
		/// <returns>The number of minutes before the page expires.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00016D52 File Offset: 0x00014F52
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00016D5F File Offset: 0x00014F5F
		public override int Expires
		{
			get
			{
				return this.w.Expires;
			}
			set
			{
				this.w.Expires = value;
			}
		}

		/// <summary>Gets or sets the absolute date and time at which cached information expires in the cache. <see cref="P:System.Web.HttpResponseWrapper.ExpiresAbsolute" /> is provided for compatibility with earlier versions of ASP.</summary>
		/// <returns>The date and time at which the page expires.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00016D6D File Offset: 0x00014F6D
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00016D7A File Offset: 0x00014F7A
		public override DateTime ExpiresAbsolute
		{
			get
			{
				return this.w.ExpiresAbsolute;
			}
			set
			{
				this.w.ExpiresAbsolute = value;
			}
		}

		/// <summary>Gets or sets a filter object that is used to modify the HTTP entity body before transmission.</summary>
		/// <returns>An object that acts as the output filter.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00016D88 File Offset: 0x00014F88
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00016D95 File Offset: 0x00014F95
		public override Stream Filter
		{
			get
			{
				return this.w.Filter;
			}
			set
			{
				this.w.Filter = value;
			}
		}

		/// <summary>Gets or sets the encoding for the header of the current response.</summary>
		/// <returns>Information about the encoding for the current header.</returns>
		/// <exception cref="T:System.ArgumentNullException">The encoding value is null.</exception>
		/// <exception cref="T:System.Web.HttpException">The encoding value is <see cref="P:System.Text.Encoding.Unicode" />.- or -The headers have already been sent.</exception>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00016DA3 File Offset: 0x00014FA3
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00016DB0 File Offset: 0x00014FB0
		public override Encoding HeaderEncoding
		{
			get
			{
				return this.w.HeaderEncoding;
			}
			set
			{
				this.w.HeaderEncoding = value;
			}
		}

		/// <summary>Gets the collection of response headers.</summary>
		/// <returns>The response headers.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00016DBE File Offset: 0x00014FBE
		public override NameValueCollection Headers
		{
			get
			{
				return this.w.Headers;
			}
		}

		/// <summary>Gets a value that indicates whether the client is connected to the server.</summary>
		/// <returns>true if the client is currently connected; otherwise, false.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00016DCB File Offset: 0x00014FCB
		public override bool IsClientConnected
		{
			get
			{
				return this.w.IsClientConnected;
			}
		}

		/// <summary>Gets a value that indicates whether the client is being redirected to a new location.</summary>
		/// <returns>true if the value of the location response header differs from the current location; otherwise, false.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00016DD8 File Offset: 0x00014FD8
		public override bool IsRequestBeingRedirected
		{
			get
			{
				return this.w.IsRequestBeingRedirected;
			}
		}

		/// <summary>Gets the object that enables output of text to the outgoing HTTP response stream.</summary>
		/// <returns>An object that enables custom output to the client.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00016DE5 File Offset: 0x00014FE5
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00016DF2 File Offset: 0x00014FF2
		public override TextWriter Output
		{
			get
			{
				return this.w.Output;
			}
			set
			{
				this.w.Output = value;
			}
		}

		/// <summary>Provides binary output to the outgoing HTTP content body.</summary>
		/// <returns>An object that represents the raw contents of the outgoing HTTP content body.</returns>
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00016E00 File Offset: 0x00015000
		public override Stream OutputStream
		{
			get
			{
				return this.w.OutputStream;
			}
		}

		/// <summary>Gets or sets the value of the HTTP Location header.</summary>
		/// <returns>The absolute URL of the HTTP Location header.</returns>
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00016E0D File Offset: 0x0001500D
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00016E1A File Offset: 0x0001501A
		public override string RedirectLocation
		{
			get
			{
				return this.w.RedirectLocation;
			}
			set
			{
				this.w.RedirectLocation = value;
			}
		}

		/// <summary>Sets the Status value that is returned to the client.</summary>
		/// <returns>The status of the HTTP output. The default value is "200 (OK)". For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
		/// <exception cref="T:System.Web.HttpException">Status is set to an invalid status code.</exception>
		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x00016E28 File Offset: 0x00015028
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00016E35 File Offset: 0x00015035
		public override string Status
		{
			get
			{
				return this.w.Status;
			}
			set
			{
				this.w.Status = value;
			}
		}

		/// <summary>Gets or sets the HTTP status code of the output that is returned to the client.</summary>
		/// <returns>The status code of the HTTP output that is returned to the client. The default value is 200. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.HttpResponse.StatusCode" /> was set after the HTTP headers were sent.</exception>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00016E43 File Offset: 0x00015043
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x00016E50 File Offset: 0x00015050
		public override int StatusCode
		{
			get
			{
				return this.w.StatusCode;
			}
			set
			{
				this.w.StatusCode = value;
			}
		}

		/// <summary>Gets or sets the HTTP status message of the output that is returned to the client.</summary>
		/// <returns>The status message of the HTTP output that is returned to the client. The default value is "OK". For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
		/// <exception cref="T:System.Web.HttpException">StatusDescription was set after the HTTP headers were sent.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The status value is longer than 512 characters.</exception>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00016E5E File Offset: 0x0001505E
		// (set) Token: 0x06000964 RID: 2404 RVA: 0x00016E6B File Offset: 0x0001506B
		public override string StatusDescription
		{
			get
			{
				return this.w.StatusDescription;
			}
			set
			{
				this.w.StatusDescription = value;
			}
		}

		/// <summary>Gets or sets a value that qualifies the status code of the response.</summary>
		/// <returns>The IIS 7.0 substatus code.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The status code was set after all HTTP headers were sent.</exception>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00016E79 File Offset: 0x00015079
		// (set) Token: 0x06000966 RID: 2406 RVA: 0x00016E86 File Offset: 0x00015086
		public override int SubStatusCode
		{
			get
			{
				return this.w.SubStatusCode;
			}
			set
			{
				this.w.SubStatusCode = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to send HTTP content to the client.</summary>
		/// <returns>true if output is suppressed; otherwise, false.</returns>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00016E94 File Offset: 0x00015094
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x00016EA1 File Offset: 0x000150A1
		public override bool SuppressContent
		{
			get
			{
				return this.w.SuppressContent;
			}
			set
			{
				this.w.SuppressContent = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether forms authentication redirection to the login page should be suppressed.</summary>
		/// <returns>true if forms authentication redirection should be suppressed; otherwise, false.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00016EAF File Offset: 0x000150AF
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x00016EBC File Offset: 0x000150BC
		public override bool SuppressFormsAuthenticationRedirect
		{
			get
			{
				return this.w.SuppressFormsAuthenticationRedirect;
			}
			set
			{
				this.w.SuppressFormsAuthenticationRedirect = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether IIS 7.0 custom errors are disabled.</summary>
		/// <returns>true if IIS custom errors are disabled; otherwise, false.</returns>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00016ECA File Offset: 0x000150CA
		// (set) Token: 0x0600096C RID: 2412 RVA: 0x00016ED7 File Offset: 0x000150D7
		public override bool TrySkipIisCustomErrors
		{
			get
			{
				return this.w.TrySkipIisCustomErrors;
			}
			set
			{
				this.w.TrySkipIisCustomErrors = value;
			}
		}

		/// <summary>When overridden in a derived class, associates cache dependencies with the response that enable the response to be invalidated if it is cached and if the specified dependencies change.</summary>
		/// <param name="dependencies">A file, cache key, or <see cref="T:System.Web.Caching.CacheDependency" /> object to add to the list of application dependencies.</param>
		// Token: 0x0600096D RID: 2413 RVA: 0x00016EE5 File Offset: 0x000150E5
		public override void AddCacheDependency(params CacheDependency[] dependencies)
		{
			this.w.AddCacheDependency(dependencies);
		}

		/// <summary>Makes the validity of a cached response dependent on the specified items in the cache.</summary>
		/// <param name="cacheKeys">A collection that contains the keys of the items that the cached response is dependent on.</param>
		// Token: 0x0600096E RID: 2414 RVA: 0x00016EF3 File Offset: 0x000150F3
		public override void AddCacheItemDependencies(ArrayList cacheKeys)
		{
			this.w.AddCacheItemDependencies(cacheKeys);
		}

		/// <summary>Makes the validity of a cached item dependent on the specified items in the cache.</summary>
		/// <param name="cacheKeys">An array that contains the keys of the items that the cached response is dependent on.</param>
		// Token: 0x0600096F RID: 2415 RVA: 0x00016F01 File Offset: 0x00015101
		public override void AddCacheItemDependencies(string[] cacheKeys)
		{
			this.w.AddCacheItemDependencies(cacheKeys);
		}

		/// <summary>Makes the validity of a cached response dependent on the specified item in the cache.</summary>
		/// <param name="cacheKey">The key of the item that the cached response is dependent on.</param>
		// Token: 0x06000970 RID: 2416 RVA: 0x00016F0F File Offset: 0x0001510F
		public override void AddCacheItemDependency(string cacheKey)
		{
			this.w.AddCacheItemDependency(cacheKey);
		}

		/// <summary>Adds file names to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filenames">The names of the files to add.</param>
		// Token: 0x06000971 RID: 2417 RVA: 0x00016F1D File Offset: 0x0001511D
		public override void AddFileDependencies(ArrayList filenames)
		{
			this.w.AddFileDependencies(filenames);
		}

		/// <summary>Adds an array of file names to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filenames">An array of files names to add.</param>
		// Token: 0x06000972 RID: 2418 RVA: 0x00016F2B File Offset: 0x0001512B
		public override void AddFileDependencies(string[] filenames)
		{
			this.w.AddFileDependencies(filenames);
		}

		/// <summary>Adds a single file name to the collection of file names on which the current response is dependent.</summary>
		/// <param name="filename">The name of the file to add.</param>
		// Token: 0x06000973 RID: 2419 RVA: 0x00016F39 File Offset: 0x00015139
		public override void AddFileDependency(string filename)
		{
			this.w.AddFileDependency(filename);
		}

		/// <summary>Adds an HTTP header to the current response. This method is provided for compatibility with earlier versions of ASP.</summary>
		/// <param name="name">The name of the HTTP header to add <paramref name="value" /> to.</param>
		/// <param name="value">The string to add to the header.</param>
		// Token: 0x06000974 RID: 2420 RVA: 0x00016F47 File Offset: 0x00015147
		public override void AddHeader(string name, string value)
		{
			this.w.AddHeader(name, value);
		}

		/// <summary>Adds an HTTP cookie to the HTTP response cookie collection.</summary>
		/// <param name="cookie">The cookie to add to the response.</param>
		/// <exception cref="T:System.Web.HttpException">The cookie was added after the HTTP headers were sent.</exception>
		// Token: 0x06000975 RID: 2421 RVA: 0x00016F56 File Offset: 0x00015156
		public override void AppendCookie(HttpCookie cookie)
		{
			this.w.AppendCookie(cookie);
		}

		/// <summary>Adds an HTTP header to the current response.</summary>
		/// <param name="name">The name of the HTTP header to add to the current response.</param>
		/// <param name="value">The value of the header.</param>
		/// <exception cref="T:System.Web.HttpException">The header was appended after the HTTP headers were sent.</exception>
		// Token: 0x06000976 RID: 2422 RVA: 0x00016F64 File Offset: 0x00015164
		public override void AppendHeader(string name, string value)
		{
			this.w.AppendHeader(name, value);
		}

		/// <summary>Adds custom log information to the Internet Information Services (IIS) log file.</summary>
		/// <param name="param">The text to add to the log file.</param>
		// Token: 0x06000977 RID: 2423 RVA: 0x00016F73 File Offset: 0x00015173
		public override void AppendToLog(string param)
		{
			this.w.AppendToLog(param);
		}

		/// <summary>Adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state, and returns the combined path. </summary>
		/// <returns>The virtual path with the session ID inserted. If <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state is not used, returns the original <paramref name="virtualpath" /> value.</returns>
		/// <param name="virtualPath">The virtual path of a resource.</param>
		// Token: 0x06000978 RID: 2424 RVA: 0x00016F81 File Offset: 0x00015181
		public override string ApplyAppPathModifier(string virtualPath)
		{
			return this.w.ApplyAppPathModifier(virtualPath);
		}

		/// <summary>Writes a string of binary characters to the HTTP output stream.</summary>
		/// <param name="buffer">The binary characters to write to the current response.</param>
		// Token: 0x06000979 RID: 2425 RVA: 0x00016F8F File Offset: 0x0001518F
		public override void BinaryWrite(byte[] buffer)
		{
			this.w.BinaryWrite(buffer);
		}

		/// <summary>Clears all headers and content output from the current response.</summary>
		// Token: 0x0600097A RID: 2426 RVA: 0x00016F9D File Offset: 0x0001519D
		public override void Clear()
		{
			this.w.Clear();
		}

		/// <summary>Clears all content output from the current response.</summary>
		// Token: 0x0600097B RID: 2427 RVA: 0x00016FAA File Offset: 0x000151AA
		public override void ClearContent()
		{
			this.w.ClearContent();
		}

		/// <summary>Clears all headers from the current response.</summary>
		// Token: 0x0600097C RID: 2428 RVA: 0x00016FB7 File Offset: 0x000151B7
		public override void ClearHeaders()
		{
			this.w.ClearHeaders();
		}

		/// <summary>Closes the socket connection to a client.</summary>
		// Token: 0x0600097D RID: 2429 RVA: 0x00016FC4 File Offset: 0x000151C4
		public override void Close()
		{
			this.w.Close();
		}

		/// <summary>Disables kernel caching for the current response.</summary>
		// Token: 0x0600097E RID: 2430 RVA: 0x00016FD1 File Offset: 0x000151D1
		public override void DisableKernelCache()
		{
			this.w.DisableKernelCache();
		}

		/// <summary>Sends all currently buffered output to the client, stops execution of the requested process, and raises the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
		/// <exception cref="T:System.Threading.ThreadAbortException">The call to <see cref="M:System.Web.HttpResponse.End" /> has terminated the current request.</exception>
		// Token: 0x0600097F RID: 2431 RVA: 0x00016FDE File Offset: 0x000151DE
		public override void End()
		{
			this.w.End();
		}

		/// <summary>Sends all currently buffered output to the client.</summary>
		/// <exception cref="T:System.Web.HttpException">The method was called after the response was finished.</exception>
		// Token: 0x06000980 RID: 2432 RVA: 0x00016FEB File Offset: 0x000151EB
		public override void Flush()
		{
			this.w.Flush();
		}

		/// <summary>Appends an HTTP PICS-Label header to the current response.</summary>
		/// <param name="value">The string to add to the PICS-Label header.</param>
		// Token: 0x06000981 RID: 2433 RVA: 0x00016FF8 File Offset: 0x000151F8
		public override void Pics(string value)
		{
			this.w.Pics(value);
		}

		/// <summary>Redirects a request to the specified URL.</summary>
		/// <param name="url">The target location.</param>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers were sent.</exception>
		// Token: 0x06000982 RID: 2434 RVA: 0x00017006 File Offset: 0x00015206
		public override void Redirect(string url)
		{
			this.w.Redirect(url);
		}

		/// <summary>Redirects a request to the specified URL and specifies whether execution of the current process should terminate.</summary>
		/// <param name="url">The target location. </param>
		/// <param name="endResponse">true to terminate the current process.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> contains a newline character.</exception>
		/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers were sent.</exception>
		/// <exception cref="T:System.ApplicationException">The request is the result of a callback.</exception>
		// Token: 0x06000983 RID: 2435 RVA: 0x00017014 File Offset: 0x00015214
		public override void Redirect(string url, bool endResponse)
		{
			this.w.Redirect(url, endResponse);
		}

		/// <summary>Performs a permanent redirect from the requested URL to the specified URL.</summary>
		/// <param name="url">The URL to redirect the request to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> includes a newline character (\n).</exception>
		// Token: 0x06000984 RID: 2436 RVA: 0x00017023 File Offset: 0x00015223
		public override void RedirectPermanent(string url)
		{
			this.w.RedirectPermanent(url);
		}

		/// <summary>Performs a permanent redirect from the requested URL to the specified URL, and provides the option to complete the response.</summary>
		/// <param name="url">The URL to redirect the request to.</param>
		/// <param name="endResponse">true to terminate the response; otherwise false. The default is false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="url" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> includes a newline character (\n).</exception>
		// Token: 0x06000985 RID: 2437 RVA: 0x00017031 File Offset: 0x00015231
		public override void RedirectPermanent(string url, bool endResponse)
		{
			this.w.RedirectPermanent(url, endResponse);
		}

		/// <summary>Uses the specified output-cache provider to remove all output-cache artifacts that are associated with the specified path.</summary>
		/// <param name="path">The virtual absolute path of the items that are removed from the cache. </param>
		/// <param name="providerName">The provider that is used to remove the output-cache artifacts that are associated with the specified path.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an invalid path.</exception>
		// Token: 0x06000986 RID: 2438 RVA: 0x00017040 File Offset: 0x00015240
		public override void RemoveOutputCacheItem(string path, string providerName)
		{
			HttpResponse.RemoveOutputCacheItem(path, providerName);
		}

		/// <summary>Removes from the cache all cached items that are associated with the specified path.</summary>
		/// <param name="path">The virtual absolute path to the items to be removed from the cache.</param>
		// Token: 0x06000987 RID: 2439 RVA: 0x00017049 File Offset: 0x00015249
		public override void RemoveOutputCacheItem(string path)
		{
			HttpResponse.RemoveOutputCacheItem(path);
		}

		/// <summary>Updates an existing cookie in the cookie collection.</summary>
		/// <param name="cookie">The cookie in the collection to be updated.</param>
		/// <exception cref="T:System.Web.HttpException">The cookie was set after the HTTP headers were sent.</exception>
		// Token: 0x06000988 RID: 2440 RVA: 0x00017051 File Offset: 0x00015251
		public override void SetCookie(HttpCookie cookie)
		{
			this.w.SetCookie(cookie);
		}

		/// <summary>Writes the specified file to the HTTP response output stream, without buffering it in memory.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is null</exception>
		// Token: 0x06000989 RID: 2441 RVA: 0x0001705F File Offset: 0x0001525F
		public override void TransmitFile(string filename)
		{
			this.w.TransmitFile(filename);
		}

		/// <summary>Writes the specified part of a file to the HTTP response output stream, without buffering it in memory.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The position in the file where writing starts.</param>
		/// <param name="length">The number of bytes to write, starting at <paramref name="offset" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="offset" /> parameter is less than zero.- or -The <paramref name="length" /> parameter is less than -1.- or - The <paramref name="length" /> parameter is greater than the file size minus <paramref name="offset" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The out-of-process worker request is not supported.- or -The response is not using an <see cref="T:System.Web.HttpWriter" /> object.</exception>
		// Token: 0x0600098A RID: 2442 RVA: 0x0001706D File Offset: 0x0001526D
		public override void TransmitFile(string filename, long offset, long length)
		{
			this.w.TransmitFile(filename, offset, length);
		}

		/// <summary>Writes a character to an HTTP response output stream.</summary>
		/// <param name="ch">The character to write to the HTTP output stream.</param>
		// Token: 0x0600098B RID: 2443 RVA: 0x0001707D File Offset: 0x0001527D
		public override void Write(char ch)
		{
			this.w.Write(ch);
		}

		/// <summary>Writes the specified object to the HTTP response stream.</summary>
		/// <param name="obj">The object to write to the HTTP output stream.</param>
		// Token: 0x0600098C RID: 2444 RVA: 0x0001708B File Offset: 0x0001528B
		public override void Write(object obj)
		{
			this.w.Write(obj);
		}

		/// <summary>Writes the specified string to the HTTP response output stream.</summary>
		/// <param name="s">The string to write to the HTTP output stream.</param>
		// Token: 0x0600098D RID: 2445 RVA: 0x00017099 File Offset: 0x00015299
		public override void Write(string s)
		{
			this.w.Write(s);
		}

		/// <summary>Writes the specified array of characters to the HTTP response output stream.</summary>
		/// <param name="buffer">The character array to write.</param>
		/// <param name="index">The position in the character array where writing starts.</param>
		/// <param name="count">The number of characters to write, starting at <paramref name="index" />.</param>
		// Token: 0x0600098E RID: 2446 RVA: 0x000170A7 File Offset: 0x000152A7
		public override void Write(char[] buffer, int index, int count)
		{
			this.w.Write(buffer, index, count);
		}

		/// <summary>Writes the contents of the specified file to the HTTP response output stream as a file block.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is null.</exception>
		// Token: 0x0600098F RID: 2447 RVA: 0x000170B7 File Offset: 0x000152B7
		public override void WriteFile(string filename)
		{
			this.w.WriteFile(filename);
		}

		/// <summary>Writes the contents of the specified file to the HTTP response output stream and specifies whether the content is written as a memory block.</summary>
		/// <param name="filename">The name of the file to write to the current response.</param>
		/// <param name="readIntoMemory">true to write the file into a memory block.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is null.</exception>
		// Token: 0x06000990 RID: 2448 RVA: 0x000170C5 File Offset: 0x000152C5
		public override void WriteFile(string filename, bool readIntoMemory)
		{
			this.w.WriteFile(filename, readIntoMemory);
		}

		/// <summary>Writes the specified file to the HTTP response output stream.</summary>
		/// <param name="fileHandle">The file handle of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The position in the file where writing starts.</param>
		/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileHandle" /> is null.</exception>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="offset" /> is less than 0.- or -<paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
		// Token: 0x06000991 RID: 2449 RVA: 0x000170D4 File Offset: 0x000152D4
		public override void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			this.w.WriteFile(fileHandle, offset, size);
		}

		/// <summary>Writes the specified file to the HTTP response output stream.</summary>
		/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
		/// <param name="offset">The position in the file where writing starts.</param>
		/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="offset" /> is less than 0.- or -<paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
		// Token: 0x06000992 RID: 2450 RVA: 0x000170E4 File Offset: 0x000152E4
		public override void WriteFile(string filename, long offset, long size)
		{
			this.w.WriteFile(filename, offset, size);
		}

		/// <summary>Inserts substitution blocks into the response, which enables dynamic generation of regions for cached output responses.</summary>
		/// <param name="callback">The method, user control, or object to substitute.</param>
		/// <exception cref="T:System.ArgumentException">The target of the <paramref name="callback" /> parameter is of type <see cref="T:System.Web.UI.Control" />.</exception>
		// Token: 0x06000993 RID: 2451 RVA: 0x000170F4 File Offset: 0x000152F4
		public override void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			this.w.WriteSubstitution(callback);
		}

		// Token: 0x04001001 RID: 4097
		private HttpResponse w;
	}
}
