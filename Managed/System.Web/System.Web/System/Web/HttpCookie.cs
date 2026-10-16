using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using Unity;

namespace System.Web
{
	/// <summary>Provides a type-safe way to create and manipulate individual HTTP cookies.</summary>
	// Token: 0x0200008F RID: 143
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class HttpCookie
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x0000FE1C File Offset: 0x0000E01C
		[Obsolete]
		internal HttpCookie(string name, string value, string path, DateTime expires)
		{
			this.name = name;
			this.values = new HttpCookie.CookieNVC();
			this.Value = value;
			this.path = path;
			this.expires = expires;
		}

		/// <summary>Creates and names a new cookie.</summary>
		/// <param name="name">The name of the new cookie. </param>
		// Token: 0x060006EC RID: 1772 RVA: 0x0000FE70 File Offset: 0x0000E070
		public HttpCookie(string name)
		{
			this.name = name;
			this.values = new HttpCookie.CookieNVC();
			this.Value = "";
			HttpCookiesSection httpCookiesSection = (HttpCookiesSection)WebConfigurationManager.GetSection("system.web/httpCookies");
			if (!string.IsNullOrWhiteSpace(httpCookiesSection.Domain))
			{
				this.domain = httpCookiesSection.Domain;
			}
			if (httpCookiesSection.HttpOnlyCookies)
			{
				this.flags |= CookieFlags.HttpOnly;
			}
			if (httpCookiesSection.RequireSSL)
			{
				this.flags |= CookieFlags.Secure;
			}
		}

		/// <summary>Creates, names, and assigns a value to a new cookie.</summary>
		/// <param name="name">The name of the new cookie. </param>
		/// <param name="value">The value of the new cookie. </param>
		// Token: 0x060006ED RID: 1773 RVA: 0x0000FF0B File Offset: 0x0000E10B
		public HttpCookie(string name, string value)
			: this(name)
		{
			this.Value = value;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000FF1C File Offset: 0x0000E11C
		internal string GetCookieHeaderValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.name);
			stringBuilder.Append("=");
			stringBuilder.Append(this.Value);
			if (this.domain != null)
			{
				stringBuilder.Append("; domain=");
				stringBuilder.Append(this.domain);
			}
			if (this.path != null)
			{
				stringBuilder.Append("; path=");
				stringBuilder.Append(this.path);
			}
			if (this.expires != DateTime.MinValue)
			{
				stringBuilder.Append("; expires=");
				stringBuilder.Append(this.expires.ToUniversalTime().ToString("r"));
			}
			if ((this.flags & CookieFlags.Secure) != (CookieFlags)0)
			{
				stringBuilder.Append("; secure");
			}
			if ((this.flags & CookieFlags.HttpOnly) != (CookieFlags)0)
			{
				stringBuilder.Append("; HttpOnly");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Gets or sets the domain to associate the cookie with.</summary>
		/// <returns>The name of the domain to associate the cookie with. The default value is the current domain.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00010006 File Offset: 0x0000E206
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0001000E File Offset: 0x0000E20E
		public string Domain
		{
			get
			{
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets the expiration date and time for the cookie.</summary>
		/// <returns>The time of day (on the client) at which the cookie expires.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00010017 File Offset: 0x0000E217
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x0001001F File Offset: 0x0000E21F
		public DateTime Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		/// <summary>Gets a value indicating whether a cookie has subkeys.</summary>
		/// <returns>true if the cookie has subkeys, otherwise, false. The default value is false.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00010028 File Offset: 0x0000E228
		public bool HasKeys
		{
			get
			{
				return this.values.HasKeys();
			}
		}

		/// <summary>Gets a shortcut to the <see cref="P:System.Web.HttpCookie.Values" /> property. This property is provided for compatibility with previous versions of Active Server Pages (ASP).</summary>
		/// <returns>The cookie value.</returns>
		/// <param name="key">The key (index) of the cookie value. </param>
		// Token: 0x170002B0 RID: 688
		public string this[string key]
		{
			get
			{
				return this.values[key];
			}
			set
			{
				this.values[key] = value;
			}
		}

		/// <summary>Gets or sets the name of a cookie.</summary>
		/// <returns>The default value is a null reference (Nothing in Visual Basic) unless the constructor specifies otherwise.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00010052 File Offset: 0x0000E252
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001005A File Offset: 0x0000E25A
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the virtual path to transmit with the current cookie.</summary>
		/// <returns>The virtual path to transmit with the cookie. The default is /, which is the server root.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00010063 File Offset: 0x0000E263
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001006B File Offset: 0x0000E26B
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to transmit the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.</summary>
		/// <returns>true to transmit the cookie over an SSL connection (HTTPS); otherwise, false. The default value is false.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00010074 File Offset: 0x0000E274
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00010081 File Offset: 0x0000E281
		public bool Secure
		{
			get
			{
				return (this.flags & CookieFlags.Secure) == CookieFlags.Secure;
			}
			set
			{
				if (value)
				{
					this.flags |= CookieFlags.Secure;
					return;
				}
				this.flags &= ~CookieFlags.Secure;
			}
		}

		/// <summary>Gets or sets an individual cookie value.</summary>
		/// <returns>The value of the cookie. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x000100A7 File Offset: 0x0000E2A7
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x000100BC File Offset: 0x0000E2BC
		public string Value
		{
			get
			{
				return HttpUtility.UrlDecode(this.values.ToString());
			}
			set
			{
				this.values.Clear();
				if (value != null && value != "")
				{
					foreach (string text in value.Split('&', StringSplitOptions.None))
					{
						int num = text.IndexOf('=');
						if (num == -1)
						{
							this.values.Add(null, text);
						}
						else
						{
							string text2 = text.Substring(0, num);
							string text3 = text.Substring(num + 1);
							this.values.Add(text2, text3);
						}
					}
				}
			}
		}

		/// <summary>Gets a collection of key/value pairs that are contained within a single cookie object.</summary>
		/// <returns>A collection of cookie values.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00010140 File Offset: 0x0000E340
		public NameValueCollection Values
		{
			get
			{
				return this.values;
			}
		}

		/// <summary>Gets or sets a value that specifies whether a cookie is accessible by client-side script.</summary>
		/// <returns>true if the cookie has the HttpOnly attribute and cannot be accessed through a client-side script; otherwise, false. The default is false.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00010148 File Offset: 0x0000E348
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00010155 File Offset: 0x0000E355
		public bool HttpOnly
		{
			get
			{
				return (this.flags & CookieFlags.HttpOnly) == CookieFlags.HttpOnly;
			}
			set
			{
				if (value)
				{
					this.flags |= CookieFlags.HttpOnly;
					return;
				}
				this.flags &= ~CookieFlags.HttpOnly;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001017C File Offset: 0x0000E37C
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SameSiteMode SameSite
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SameSiteMode.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Determines whether the cookie is allowed to participate in output caching.</summary>
		/// <returns>true to specify that output caching will not be suppressed for a give <see cref="T:System.Web.HttpResponse" /> containing one or more outbound cookies; otherwise, false.</returns>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00010198 File Offset: 0x0000E398
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool Shareable
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public static bool TryParse(string input, out HttpCookie result)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x04000F51 RID: 3921
		private string path = "/";

		// Token: 0x04000F52 RID: 3922
		private string domain;

		// Token: 0x04000F53 RID: 3923
		private DateTime expires = DateTime.MinValue;

		// Token: 0x04000F54 RID: 3924
		private string name;

		// Token: 0x04000F55 RID: 3925
		private CookieFlags flags;

		// Token: 0x04000F56 RID: 3926
		private NameValueCollection values;

		// Token: 0x02000090 RID: 144
		[Serializable]
		private sealed class CookieNVC : NameValueCollection
		{
			// Token: 0x06000706 RID: 1798 RVA: 0x000101CF File Offset: 0x0000E3CF
			public CookieNVC()
				: base(StringComparer.OrdinalIgnoreCase)
			{
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x000101DC File Offset: 0x0000E3DC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("");
				bool flag = true;
				foreach (object obj in this.Keys)
				{
					string text = (string)obj;
					if (!flag)
					{
						stringBuilder.Append("&");
					}
					string[] array = this.GetValues(text);
					if (array == null)
					{
						array = new string[] { string.Empty };
					}
					bool flag2 = true;
					foreach (string text2 in array)
					{
						if (!flag2)
						{
							stringBuilder.Append("&");
						}
						if (text != null && text.Length > 0)
						{
							stringBuilder.Append(HttpUtility.UrlEncode(text));
							stringBuilder.Append("=");
						}
						if (text2 != null && text2.Length > 0)
						{
							stringBuilder.Append(HttpUtility.UrlEncode(text2));
						}
						flag2 = false;
					}
					flag = false;
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x000102F0 File Offset: 0x0000E4F0
			public override void Set(string name, string value)
			{
				if (base.IsReadOnly)
				{
					throw new NotSupportedException("Collection is read-only");
				}
				if (name == null)
				{
					this.Clear();
					name = string.Empty;
				}
				base.Set(name, value);
			}
		}
	}
}
