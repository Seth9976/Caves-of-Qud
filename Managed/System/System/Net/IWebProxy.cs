using System;

namespace System.Net
{
	/// <summary>Provides the base interface for implementation of proxy access for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200065E RID: 1630
	public interface IWebProxy
	{
		/// <summary>Returns the URI of a proxy.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the URI of the proxy used to contact <paramref name="destination" />.</returns>
		/// <param name="destination">A <see cref="T:System.Uri" /> that specifies the requested Internet resource. </param>
		// Token: 0x06003365 RID: 13157
		Uri GetProxy(Uri destination);

		/// <summary>Indicates that the proxy should not be used for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> of the host to check for proxy use. </param>
		// Token: 0x06003366 RID: 13158
		bool IsBypassed(Uri host);

		/// <summary>The credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials that are needed to authenticate a request to the proxy server.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06003367 RID: 13159
		// (set) Token: 0x06003368 RID: 13160
		ICredentials Credentials { get; set; }
	}
}
