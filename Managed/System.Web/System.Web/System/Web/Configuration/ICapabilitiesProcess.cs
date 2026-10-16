using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x020005AE RID: 1454
	internal interface ICapabilitiesProcess
	{
		// Token: 0x06003E63 RID: 15971
		CapabilitiesResult Process(string userAgent, IDictionary initialCapabilities);

		// Token: 0x06003E64 RID: 15972
		CapabilitiesResult Process(HttpRequest request, IDictionary initialCapabilities);

		// Token: 0x06003E65 RID: 15973
		CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities);
	}
}
