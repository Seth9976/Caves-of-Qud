using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI;
using System.Web.Util;
using Unity;

namespace System.Web
{
	/// <summary>Contains methods for setting cache-specific HTTP headers and for controlling the ASP.NET page output cache.</summary>
	// Token: 0x02000081 RID: 129
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCachePolicy
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0000E13A File Offset: 0x0000C33A
		internal HttpCachePolicy()
		{
		}

		/// <summary>Gets the list of Content-Encoding headers that will be used to vary the output cache.</summary>
		/// <returns>An object that specifies which Content-Encoding headers are used to select the cached response.</returns>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000E16A File Offset: 0x0000C36A
		public HttpCacheVaryByContentEncodings VaryByContentEncodings
		{
			get
			{
				return this.vary_by_content_encodings;
			}
		}

		/// <summary>Gets the list of all HTTP headers that will be used to vary cache output.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCacheVaryByHeaders" /> that specifies which HTTP headers are used to select the cached response.</returns>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000E172 File Offset: 0x0000C372
		public HttpCacheVaryByHeaders VaryByHeaders
		{
			get
			{
				return this.vary_by_headers;
			}
		}

		/// <summary>Gets the list of parameters received by an HTTP GET or HTTP POST that affect caching.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCacheVaryByParams" /> that specifies which cache-control headers are used to select the cached response.</returns>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000E17A File Offset: 0x0000C37A
		public HttpCacheVaryByParams VaryByParams
		{
			get
			{
				return this.vary_by_params;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000E182 File Offset: 0x0000C382
		internal bool AllowServerCaching
		{
			get
			{
				return this.allow_server_caching;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000E18A File Offset: 0x0000C38A
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000E192 File Offset: 0x0000C392
		internal int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000E19B File Offset: 0x0000C39B
		internal bool Sliding
		{
			get
			{
				return this.sliding_expiration;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000E1A3 File Offset: 0x0000C3A3
		internal DateTime Expires
		{
			get
			{
				return this.expire_date;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000E1AB File Offset: 0x0000C3AB
		internal ArrayList ValidationCallbacks
		{
			get
			{
				return this.validation_callbacks;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000E1B3 File Offset: 0x0000C3B3
		internal bool OmitVaryStar
		{
			get
			{
				return this.omit_vary_star;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000E1BB File Offset: 0x0000C3BB
		internal bool ValidUntilExpires
		{
			get
			{
				return this.valid_until_expires;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		internal int ExpireMinutes()
		{
			if (!this.have_expire_date)
			{
				return 0;
			}
			return (this.expire_date - DateTime.Now).Minutes;
		}

		/// <summary>Registers a validation callback for the current response.</summary>
		/// <param name="handler">The <see cref="T:System.Web.HttpCacheValidateHandler" /> value. </param>
		/// <param name="data">The arbitrary user-supplied data that is passed back to the <see cref="M:System.Web.HttpCachePolicy.AddValidationCallback(System.Web.HttpCacheValidateHandler,System.Object)" /> delegate. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="handler" /> is null. </exception>
		// Token: 0x060005B1 RID: 1457 RVA: 0x0000E1F3 File Offset: 0x0000C3F3
		public void AddValidationCallback(HttpCacheValidateHandler handler, object data)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.validation_callbacks == null)
			{
				this.validation_callbacks = new ArrayList();
			}
			this.validation_callbacks.Add(new Pair(handler, data));
		}

		/// <summary>Appends the specified text to the Cache-Control HTTP header.</summary>
		/// <param name="extension">The text to append to the Cache-Control header. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="extension" /> is null. </exception>
		// Token: 0x060005B2 RID: 1458 RVA: 0x0000E229 File Offset: 0x0000C429
		public void AppendCacheExtension(string extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.cache_extension == null)
			{
				this.cache_extension = new StringBuilder(extension);
				return;
			}
			this.cache_extension.Append(", " + extension);
		}

		/// <summary>Sets the Cache-Control header to one of the values of <see cref="T:System.Web.HttpCacheability" />.</summary>
		/// <param name="cacheability">An <see cref="T:System.Web.HttpCacheability" /> enumeration value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cacheability" /> is not one of the enumeration values. </exception>
		// Token: 0x060005B3 RID: 1459 RVA: 0x0000E265 File Offset: 0x0000C465
		public void SetCacheability(HttpCacheability cacheability)
		{
			if (cacheability < HttpCacheability.NoCache || cacheability > HttpCacheability.ServerAndPrivate)
			{
				throw new ArgumentOutOfRangeException("cacheability");
			}
			if (this.Cacheability > (HttpCacheability)0 && cacheability > this.Cacheability)
			{
				return;
			}
			this.Cacheability = cacheability;
		}

		/// <summary>Sets the Cache-Control header to one of the values of <see cref="T:System.Web.HttpCacheability" /> and appends an extension to the directive.</summary>
		/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to. </param>
		/// <param name="field">The cache control extension to add to the header. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="field" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="cacheability" /> is not <see cref="F:System.Web.HttpCacheability.Private" /> or <see cref="F:System.Web.HttpCacheability.NoCache" />. </exception>
		// Token: 0x060005B4 RID: 1460 RVA: 0x0000E294 File Offset: 0x0000C494
		public void SetCacheability(HttpCacheability cacheability, string field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (cacheability != HttpCacheability.NoCache && cacheability != HttpCacheability.Private)
			{
				throw new ArgumentException("Must be NoCache or Private", "cacheability");
			}
			if (this.fields == null)
			{
				this.fields = new ArrayList();
			}
			this.fields.Add(new Pair(cacheability, field));
		}

		/// <summary>Sets the ETag HTTP header to the specified string.</summary>
		/// <param name="etag">The text to use for the ETag header. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="etag" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The ETag header has already been set. - or -The <see cref="M:System.Web.HttpCachePolicy.SetETagFromFileDependencies" /> has already been called.</exception>
		// Token: 0x060005B5 RID: 1461 RVA: 0x0000E2F2 File Offset: 0x0000C4F2
		public void SetETag(string etag)
		{
			if (etag == null)
			{
				throw new ArgumentNullException("etag");
			}
			if (this.etag != null)
			{
				throw new InvalidOperationException("The ETag header has already been set");
			}
			if (this.etag_from_file_dependencies)
			{
				throw new InvalidOperationException("SetEtagFromFileDependencies has already been called");
			}
			this.etag = etag;
		}

		/// <summary>Sets the ETag HTTP header based on the time stamps of the handler's file dependencies.</summary>
		/// <exception cref="T:System.InvalidOperationException">The ETag header has already been set. </exception>
		// Token: 0x060005B6 RID: 1462 RVA: 0x0000E32F File Offset: 0x0000C52F
		public void SetETagFromFileDependencies()
		{
			if (this.etag != null)
			{
				throw new InvalidOperationException("The ETag header has already been set");
			}
			this.etag_from_file_dependencies = true;
		}

		/// <summary>Sets the Expires HTTP header to an absolute date and time.</summary>
		/// <param name="date">The absolute <see cref="T:System.DateTime" /> value to set the Expires header to. </param>
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000E34B File Offset: 0x0000C54B
		public void SetExpires(DateTime date)
		{
			if (this.have_expire_date && date > this.expire_date)
			{
				return;
			}
			this.have_expire_date = true;
			this.expire_date = date;
		}

		/// <summary>Sets the Last-Modified HTTP header to the <see cref="T:System.DateTime" /> value supplied.</summary>
		/// <param name="date">The new <see cref="T:System.DateTime" /> value for the Last-Modified header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="date" /> is later than the current DateTime. </exception>
		// Token: 0x060005B8 RID: 1464 RVA: 0x0000E372 File Offset: 0x0000C572
		public void SetLastModified(DateTime date)
		{
			if (date > DateTime.Now)
			{
				throw new ArgumentOutOfRangeException("date");
			}
			if (this.have_last_modified && date < this.last_modified)
			{
				return;
			}
			this.have_last_modified = true;
			this.last_modified = date;
		}

		/// <summary>Sets the Last-Modified HTTP header based on the time stamps of the handler's file dependencies.</summary>
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000E3B1 File Offset: 0x0000C5B1
		public void SetLastModifiedFromFileDependencies()
		{
			this.last_modified_from_file_dependencies = true;
		}

		/// <summary>Sets the Cache-Control: max-age HTTP header based on the specified time span.</summary>
		/// <param name="delta">The time span used to set the Cache - Control: max-age header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delta" /> is less than 0 or greater than one year. </exception>
		// Token: 0x060005BA RID: 1466 RVA: 0x0000E3BA File Offset: 0x0000C5BA
		public void SetMaxAge(TimeSpan delta)
		{
			if (delta < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			if (this.HaveMaxAge && this.MaxAge < delta)
			{
				return;
			}
			this.MaxAge = delta;
			this.HaveMaxAge = true;
		}

		/// <summary>Stops all origin-server caching for the current response.</summary>
		// Token: 0x060005BB RID: 1467 RVA: 0x0000E3F9 File Offset: 0x0000C5F9
		public void SetNoServerCaching()
		{
			this.allow_server_caching = false;
		}

		/// <summary>Sets the Cache-Control: no-store HTTP header.</summary>
		// Token: 0x060005BC RID: 1468 RVA: 0x0000E402 File Offset: 0x0000C602
		public void SetNoStore()
		{
			this.set_no_store = true;
		}

		/// <summary>Sets the Cache-Control: no-transform HTTP header.</summary>
		// Token: 0x060005BD RID: 1469 RVA: 0x0000E40B File Offset: 0x0000C60B
		public void SetNoTransforms()
		{
			this.set_no_transform = true;
		}

		/// <summary>Sets the Cache-Control: s-maxage HTTP header based on the specified time span.</summary>
		/// <param name="delta">The time span used to set the Cache-Control: s-maxage header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delta" /> is less than 0. </exception>
		// Token: 0x060005BE RID: 1470 RVA: 0x0000E414 File Offset: 0x0000C614
		public void SetProxyMaxAge(TimeSpan delta)
		{
			if (delta < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("delta");
			}
			if (this.HaveProxyMaxAge && this.ProxyMaxAge < delta)
			{
				return;
			}
			this.ProxyMaxAge = delta;
			this.HaveProxyMaxAge = true;
		}

		/// <summary>Sets the Cache-Control HTTP header to either the must-revalidate or the proxy-revalidate directives based on the supplied enumeration value.</summary>
		/// <param name="revalidation">The <see cref="T:System.Web.HttpCacheRevalidation" /> enumeration value to set the Cache-Control header to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="revalidation" /> is not one of the enumeration values. </exception>
		// Token: 0x060005BF RID: 1471 RVA: 0x0000E453 File Offset: 0x0000C653
		public void SetRevalidation(HttpCacheRevalidation revalidation)
		{
			if (revalidation < HttpCacheRevalidation.AllCaches || revalidation > HttpCacheRevalidation.None)
			{
				throw new ArgumentOutOfRangeException("revalidation");
			}
			if (this.revalidation > revalidation)
			{
				this.revalidation = revalidation;
			}
		}

		/// <summary>Sets cache expiration to from absolute to sliding.</summary>
		/// <param name="slide">true or false. </param>
		// Token: 0x060005C0 RID: 1472 RVA: 0x0000E478 File Offset: 0x0000C678
		public void SetSlidingExpiration(bool slide)
		{
			this.sliding_expiration = slide;
		}

		/// <summary>Specifies whether the ASP.NET cache should ignore HTTP Cache-Control headers sent by the client that invalidate the cache.</summary>
		/// <param name="validUntilExpires">true if the cache ignores Cache-Control invalidation headers; otherwise, false. </param>
		// Token: 0x060005C1 RID: 1473 RVA: 0x0000E481 File Offset: 0x0000C681
		public void SetValidUntilExpires(bool validUntilExpires)
		{
			this.valid_until_expires = validUntilExpires;
		}

		/// <summary>Specifies a custom text string to vary cached output responses by.</summary>
		/// <param name="custom">The text string to vary cached output by. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="custom" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpCachePolicy.SetVaryByCustom(System.String)" /> method has already been called. </exception>
		// Token: 0x060005C2 RID: 1474 RVA: 0x0000E48A File Offset: 0x0000C68A
		public void SetVaryByCustom(string custom)
		{
			if (custom == null)
			{
				throw new ArgumentNullException("custom");
			}
			if (this.vary_by_custom != null)
			{
				throw new InvalidOperationException("VaryByCustom has already been set.");
			}
			this.vary_by_custom = custom;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		internal string GetVaryByCustom()
		{
			return this.vary_by_custom;
		}

		/// <summary>Makes the response is available in the client browser History cache, regardless of the <see cref="T:System.Web.HttpCacheability" /> setting made on the server, when the <paramref name="allow" /> parameter is true.</summary>
		/// <param name="allow">true to direct the client browser to store responses in the History folder; otherwise false. The default is false. </param>
		// Token: 0x060005C4 RID: 1476 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public void SetAllowResponseInBrowserHistory(bool allow)
		{
			if (this.Cacheability == HttpCacheability.NoCache || this.Cacheability == HttpCacheability.Server)
			{
				this.allow_response_in_browser_history = allow;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		internal void SetHeaders(HttpResponse response, NameValueCollection headers)
		{
			bool flag = false;
			string text;
			switch (this.Cacheability)
			{
			case HttpCacheability.NoCache:
			case HttpCacheability.Server:
				flag = true;
				text = "no-cache";
				goto IL_0041;
			case HttpCacheability.Public:
				text = "public";
				goto IL_0041;
			}
			text = "private";
			IL_0041:
			if (flag)
			{
				response.CacheControl = text;
				if (!this.allow_response_in_browser_history)
				{
					headers.Add("Expires", "-1");
					headers.Add("Pragma", "no-cache");
				}
			}
			else
			{
				if (this.HaveMaxAge)
				{
					text = text + ", max-age=" + ((long)this.MaxAge.TotalSeconds).ToString();
				}
				if (this.have_expire_date)
				{
					string text2 = TimeUtil.ToUtcTimeString(this.expire_date);
					headers.Add("Expires", text2);
				}
			}
			if (this.set_no_store)
			{
				text += ", no-store";
			}
			if (this.set_no_transform)
			{
				text += ", no-transform";
			}
			if (this.cache_extension != null && this.cache_extension.Length > 0)
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += ", ";
				}
				text += this.cache_extension.ToString();
			}
			headers.Add("Cache-Control", text);
			if (this.last_modified_from_file_dependencies || this.etag_from_file_dependencies)
			{
				this.HeadersFromFileDependencies(response);
			}
			if (this.etag != null)
			{
				headers.Add("ETag", this.etag);
			}
			if (this.have_last_modified)
			{
				headers.Add("Last-Modified", TimeUtil.ToUtcTimeString(this.last_modified));
			}
			if (!this.vary_by_params.IgnoreParams)
			{
				string responseHeaderValue = this.vary_by_params.GetResponseHeaderValue();
				if (responseHeaderValue != null)
				{
					headers.Add("Vary", responseHeaderValue);
				}
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000E690 File Offset: 0x0000C890
		private void HeadersFromFileDependencies(HttpResponse response)
		{
			string[] fileDependencies = response.FileDependencies;
			if (fileDependencies == null || fileDependencies.Length == 0)
			{
				return;
			}
			bool flag = this.etag != null && this.etag_from_file_dependencies;
			if (!flag && !this.last_modified_from_file_dependencies)
			{
				return;
			}
			DateTime dateTime = DateTime.MinValue;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in fileDependencies)
			{
				if (File.Exists(text))
				{
					DateTime lastWriteTime;
					try
					{
						lastWriteTime = File.GetLastWriteTime(text);
					}
					catch
					{
						goto IL_0098;
					}
					if (this.last_modified_from_file_dependencies && lastWriteTime > dateTime)
					{
						dateTime = lastWriteTime;
					}
					if (flag)
					{
						stringBuilder.AppendFormat("{0}", lastWriteTime.Ticks.ToString("x"));
					}
				}
				IL_0098:;
			}
			if (this.last_modified_from_file_dependencies && dateTime > DateTime.MinValue)
			{
				this.last_modified = dateTime;
				this.have_last_modified = true;
			}
			if (flag && stringBuilder.Length > 0)
			{
				this.etag = stringBuilder.ToString();
			}
		}

		/// <summary>Specifies whether the response should contain the vary:* header when varying by parameters.</summary>
		/// <param name="omit">true to direct the <see cref="T:System.Web.HttpCachePolicy" /> to not use the * value for its <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property; otherwise, false.</param>
		// Token: 0x060005C7 RID: 1479 RVA: 0x0000E790 File Offset: 0x0000C990
		public void SetOmitVaryStar(bool omit)
		{
			this.omit_vary_star = omit;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000E79C File Offset: 0x0000C99C
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DateTime UtcTimestampCreated
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public HttpCacheability GetCacheability()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return (HttpCacheability)0;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetCacheExtensions()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetETag()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public bool GetETagFromFileDependencies()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		public DateTime GetExpires()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(DateTime);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000E814 File Offset: 0x0000CA14
		public bool GetIgnoreRangeRequests()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000E830 File Offset: 0x0000CA30
		public bool GetLastModifiedFromFileDependencies()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000E84C File Offset: 0x0000CA4C
		public TimeSpan GetMaxAge()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(TimeSpan);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000E868 File Offset: 0x0000CA68
		public bool GetNoServerCaching()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000E884 File Offset: 0x0000CA84
		public bool GetNoStore()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public bool GetNoTransforms()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000E8BC File Offset: 0x0000CABC
		public int GetOmitVaryStar()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		public TimeSpan GetProxyMaxAge()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(TimeSpan);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public HttpCacheRevalidation GetRevalidation()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return (HttpCacheRevalidation)0;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000E910 File Offset: 0x0000CB10
		public DateTime GetUtcLastModified()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(DateTime);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000E92C File Offset: 0x0000CB2C
		public bool HasSlidingExpiration()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000E948 File Offset: 0x0000CB48
		public bool IsModified()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000E964 File Offset: 0x0000CB64
		public bool IsValidUntilExpires()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04000EFC RID: 3836
		private HttpCacheVaryByContentEncodings vary_by_content_encodings = new HttpCacheVaryByContentEncodings();

		// Token: 0x04000EFD RID: 3837
		private HttpCacheVaryByHeaders vary_by_headers = new HttpCacheVaryByHeaders();

		// Token: 0x04000EFE RID: 3838
		private HttpCacheVaryByParams vary_by_params = new HttpCacheVaryByParams();

		// Token: 0x04000EFF RID: 3839
		private ArrayList validation_callbacks;

		// Token: 0x04000F00 RID: 3840
		private StringBuilder cache_extension;

		// Token: 0x04000F01 RID: 3841
		internal HttpCacheability Cacheability;

		// Token: 0x04000F02 RID: 3842
		private string etag;

		// Token: 0x04000F03 RID: 3843
		private bool etag_from_file_dependencies;

		// Token: 0x04000F04 RID: 3844
		private bool last_modified_from_file_dependencies;

		// Token: 0x04000F05 RID: 3845
		internal bool have_expire_date;

		// Token: 0x04000F06 RID: 3846
		internal DateTime expire_date;

		// Token: 0x04000F07 RID: 3847
		internal bool have_last_modified;

		// Token: 0x04000F08 RID: 3848
		internal DateTime last_modified;

		// Token: 0x04000F09 RID: 3849
		private HttpCacheRevalidation revalidation;

		// Token: 0x04000F0A RID: 3850
		private string vary_by_custom;

		// Token: 0x04000F0B RID: 3851
		private bool HaveMaxAge;

		// Token: 0x04000F0C RID: 3852
		private TimeSpan MaxAge;

		// Token: 0x04000F0D RID: 3853
		private bool HaveProxyMaxAge;

		// Token: 0x04000F0E RID: 3854
		private TimeSpan ProxyMaxAge;

		// Token: 0x04000F0F RID: 3855
		private ArrayList fields;

		// Token: 0x04000F10 RID: 3856
		private bool sliding_expiration;

		// Token: 0x04000F11 RID: 3857
		private int duration;

		// Token: 0x04000F12 RID: 3858
		private bool allow_response_in_browser_history;

		// Token: 0x04000F13 RID: 3859
		private bool allow_server_caching = true;

		// Token: 0x04000F14 RID: 3860
		private bool set_no_store;

		// Token: 0x04000F15 RID: 3861
		private bool set_no_transform;

		// Token: 0x04000F16 RID: 3862
		private bool valid_until_expires;

		// Token: 0x04000F17 RID: 3863
		private bool omit_vary_star;
	}
}
