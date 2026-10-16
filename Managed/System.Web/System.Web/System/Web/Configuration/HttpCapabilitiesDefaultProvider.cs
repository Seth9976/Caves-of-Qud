using System;
using System.Collections;

namespace System.Web.Configuration
{
	/// <summary>The default extension of the <see cref="T:System.Web.Configuration.HttpCapabilitiesProvider" /> class that is included with ASP.NET.</summary>
	// Token: 0x020005A2 RID: 1442
	public class HttpCapabilitiesDefaultProvider : HttpCapabilitiesProvider
	{
		/// <summary>Gets or sets the length of time in seconds to retain the <see cref="T:System.Web.HttpBrowserCapabilities" /> object in the cache.</summary>
		/// <returns>The length of time in seconds to retain the <see cref="T:System.Web.HttpBrowserCapabilities" /> object in the cache.</returns>
		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x000A1E33 File Offset: 0x000A0033
		// (set) Token: 0x06003DC1 RID: 15809 RVA: 0x000A1E3B File Offset: 0x000A003B
		public TimeSpan CacheTime { get; set; }

		/// <summary>Gets or sets the type of the class that is used to hold the results from parsing the browserCap element of the Web.config file.</summary>
		/// <returns>The type of the class that is used to hold the results from parsing the browserCaps element of the Web.config file.</returns>
		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x000A1E44 File Offset: 0x000A0044
		// (set) Token: 0x06003DC3 RID: 15811 RVA: 0x000A1E4C File Offset: 0x000A004C
		public Type ResultType { get; set; }

		/// <summary>Gets or sets the number of characters from the user agent string to use for caching of the <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</summary>
		/// <returns>The number of characters from the supplied user agent string to use for caching of the <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</returns>
		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x000A1E55 File Offset: 0x000A0055
		// (set) Token: 0x06003DC5 RID: 15813 RVA: 0x000A1E5D File Offset: 0x000A005D
		public int UserAgentCacheKeyLength { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> class.</summary>
		// Token: 0x06003DC6 RID: 15814 RVA: 0x000A1E66 File Offset: 0x000A0066
		public HttpCapabilitiesDefaultProvider()
		{
			this.UserAgentCacheKeyLength = 64;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> class with the values of the specified instance.</summary>
		/// <param name="parent">The <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> instance to use for initializing a new instance.</param>
		// Token: 0x06003DC7 RID: 15815 RVA: 0x000A1E76 File Offset: 0x000A0076
		public HttpCapabilitiesDefaultProvider(HttpCapabilitiesDefaultProvider parent)
		{
			this.CacheTime = parent.CacheTime;
			this.ResultType = parent.ResultType;
			this.UserAgentCacheKeyLength = parent.UserAgentCacheKeyLength;
		}

		/// <summary>Adds an HTTP request string to use to parse browser capability information.</summary>
		/// <param name="variable">The string to use to parse browser capability information.</param>
		// Token: 0x06003DC8 RID: 15816 RVA: 0x00003A2B File Offset: 0x00001C2B
		public void AddDependency(string variable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a search string that modifies a browser definition.</summary>
		/// <param name="ruleList">The search string that modifies a browser definition.</param>
		// Token: 0x06003DC9 RID: 15817 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual void AddRuleList(ArrayList ruleList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the specified <see cref="T:System.Web.HttpRequest" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the <see cref="T:System.Web.HttpRequest" /> object that was passed in.</returns>
		/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> object.</param>
		// Token: 0x06003DCA RID: 15818 RVA: 0x000A1EA2 File Offset: 0x000A00A2
		public override HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request)
		{
			return new HttpBrowserCapabilities
			{
				capabilities = HttpCapabilitiesBase.GetConfigCapabilities(null, request).Capabilities
			};
		}
	}
}
