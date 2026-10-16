using System;
using System.Collections.Specialized;

namespace System.Web.Configuration
{
	// Token: 0x0200055B RID: 1371
	internal class BrowserTree : OrderedDictionary
	{
		// Token: 0x06003B0B RID: 15115 RVA: 0x0009C6A4 File Offset: 0x0009A8A4
		internal BrowserTree()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}
	}
}
