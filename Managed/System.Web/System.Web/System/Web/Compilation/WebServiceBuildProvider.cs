using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200066F RID: 1647
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class WebServiceBuildProvider : SimpleBuildProvider
	{
		// Token: 0x06004691 RID: 18065 RVA: 0x000C3F06 File Offset: 0x000C2106
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new WebServiceParser(context, virtualPath, physicalPath, reader);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000C3F12 File Offset: 0x000C2112
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return new WebServiceParser(context, virtualPath, physicalPath, base.OpenReader(virtualPath.Original));
		}
	}
}
