using System;

namespace System.Web.Configuration
{
	/// <summary>Used internally at run time by <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> and <see cref="T:System.Web.Configuration.BrowserCapabilitiesCodeGenerator" /> to parse request data and identify the browser.</summary>
	// Token: 0x020005CC RID: 1484
	public class RegexWorker
	{
		/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.RegexWorker" />.</summary>
		/// <param name="browserCaps">The <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> object to be configured.</param>
		// Token: 0x06004000 RID: 16384 RVA: 0x00002050 File Offset: 0x00000250
		public RegexWorker(HttpBrowserCapabilities browserCaps)
		{
		}

		/// <summary>Used internally at run time to determine whether the specified request-header value matches any of the capabilities of an internal collection of browsers.</summary>
		/// <returns>true if the specified request-header value matches any of the capabilities of an internal collection of browsers; otherwise, false. The default is false.</returns>
		/// <param name="target">The capabilities value from an internal collection of browsers.</param>
		/// <param name="regexExpression">The specified request-header value.</param>
		// Token: 0x06004001 RID: 16385 RVA: 0x00008A4D File Offset: 0x00006C4D
		[global::System.MonoTODO("Mono does not currently need this routine.  Always returns false.")]
		public bool ProcessRegex(string target, string regexExpression)
		{
			return false;
		}

		/// <summary>Accessor to this class.</summary>
		/// <returns>The internal value associated with the specified<paramref name="key" />.</returns>
		/// <param name="key">The specified key.</param>
		// Token: 0x17001446 RID: 5190
		[global::System.MonoTODO("Mono does not currently need this routine. Not implemented.")]
		public string this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
