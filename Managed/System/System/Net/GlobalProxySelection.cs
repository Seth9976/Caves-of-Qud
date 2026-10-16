using System;

namespace System.Net
{
	/// <summary>Contains a global default proxy instance for all HTTP requests.</summary>
	// Token: 0x020005D0 RID: 1488
	[Obsolete("This class has been deprecated. Please use WebRequest.DefaultWebProxy instead to access and set the global default proxy. Use 'null' instead of GetEmptyWebProxy. https://go.microsoft.com/fwlink/?linkid=14202")]
	public class GlobalProxySelection
	{
		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that every call to <see cref="M:System.Net.HttpWebRequest.GetResponse" /> uses.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation was null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation. </exception>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x000A5C38 File Offset: 0x000A3E38
		// (set) Token: 0x06003013 RID: 12307 RVA: 0x000A5C66 File Offset: 0x000A3E66
		public static IWebProxy Select
		{
			get
			{
				IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
				if (defaultWebProxy == null)
				{
					return GlobalProxySelection.GetEmptyWebProxy();
				}
				WebRequest.WebProxyWrapper webProxyWrapper = defaultWebProxy as WebRequest.WebProxyWrapper;
				if (webProxyWrapper != null)
				{
					return webProxyWrapper.WebProxy;
				}
				return defaultWebProxy;
			}
			set
			{
				WebRequest.DefaultWebProxy = value;
			}
		}

		/// <summary>Returns an empty proxy instance.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains no information.</returns>
		// Token: 0x06003014 RID: 12308 RVA: 0x000A5C6E File Offset: 0x000A3E6E
		public static IWebProxy GetEmptyWebProxy()
		{
			return new EmptyWebProxy();
		}
	}
}
