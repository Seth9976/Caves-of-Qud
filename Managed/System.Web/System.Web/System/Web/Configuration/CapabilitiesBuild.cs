using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x02000587 RID: 1415
	internal abstract class CapabilitiesBuild : ICapabilitiesProcess
	{
		// Token: 0x06003BD0 RID: 15312
		protected abstract Collection<string> HeaderNames(Collection<string> list);

		// Token: 0x06003BD1 RID: 15313 RVA: 0x0009DD10 File Offset: 0x0009BF10
		public CapabilitiesResult Process(string userAgent, IDictionary initialCapabilities)
		{
			return this.Process(new NameValueCollection(1) { { "User-Agent", userAgent } }, initialCapabilities);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x0009DD38 File Offset: 0x0009BF38
		public CapabilitiesResult Process(HttpRequest request, IDictionary initialCapabilities)
		{
			if (request != null)
			{
				return this.Process(request.Headers, initialCapabilities);
			}
			return this.Process("", initialCapabilities);
		}

		// Token: 0x06003BD3 RID: 15315
		public abstract CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities);
	}
}
