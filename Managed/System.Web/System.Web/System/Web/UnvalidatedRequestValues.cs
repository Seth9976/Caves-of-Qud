using System;
using System.Collections.Specialized;

namespace System.Web
{
	/// <summary>Provides access to HTTP request values without triggering ASP.NET request validation.</summary>
	// Token: 0x020000E5 RID: 229
	public sealed class UnvalidatedRequestValues
	{
		/// <summary>Gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
		/// <returns>The cookies from the HTTP request.</returns>
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00020754 File Offset: 0x0001E954
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x0002075C File Offset: 0x0001E95C
		public HttpCookieCollection Cookies { get; internal set; }

		/// <summary>Gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
		/// <returns>The files from the HTTP request.</returns>
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00020765 File Offset: 0x0001E965
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0002076D File Offset: 0x0001E96D
		public HttpFileCollection Files { get; internal set; }

		/// <summary>Gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The form variables from the HTTP request.</returns>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00020776 File Offset: 0x0001E976
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0002077E File Offset: 0x0001E97E
		public NameValueCollection Form { get; internal set; }

		/// <summary>Gets the collection of HTTP headers that the client sent, without triggering request validation.</summary>
		/// <returns>The headers from the HTTP request.</returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00020787 File Offset: 0x0001E987
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0002078F File Offset: 0x0001E98F
		public NameValueCollection Headers { get; internal set; }

		/// <summary>Gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
		/// <returns>The virtual path.</returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00020798 File Offset: 0x0001E998
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x000207A0 File Offset: 0x0001E9A0
		public string Path { get; internal set; }

		/// <summary>Gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
		/// <returns>A string that contains additional path information for a resource.</returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000207A9 File Offset: 0x0001E9A9
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000207B1 File Offset: 0x0001E9B1
		public string PathInfo { get; internal set; }

		/// <summary>Gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
		/// <returns>The collection of query string variables sent by the client.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000207BA File Offset: 0x0001E9BA
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000207C2 File Offset: 0x0001E9C2
		public NameValueCollection QueryString { get; internal set; }

		/// <summary>Gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
		/// <returns>The part of the URL that follows the website name. </returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000207CB File Offset: 0x0001E9CB
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x000207D3 File Offset: 0x0001E9D3
		public string RawUrl { get; internal set; }

		/// <summary>Gets the URL data for the request without triggering ASP.NET request validation.</summary>
		/// <returns>An object that contains the URL data for the request. </returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000207DC File Offset: 0x0001E9DC
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x000207E4 File Offset: 0x0001E9E4
		public Uri Url { get; internal set; }

		/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
		/// <returns>The requested object, or null if the object is not found.</returns>
		/// <param name="field">The key of the object to retrieve.</param>
		// Token: 0x1700044C RID: 1100
		public string this[string field]
		{
			get
			{
				if (this.Form != null && this.Form[field] != null)
				{
					return this.Form[field];
				}
				if (this.Cookies != null && this.Cookies[field] != null)
				{
					return this.Cookies[field].Value;
				}
				if (this.QueryString != null && this.QueryString[field] != null)
				{
					return this.QueryString[field];
				}
				return null;
			}
		}
	}
}
