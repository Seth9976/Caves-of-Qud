using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200066E RID: 1646
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class WebHandlerBuildProvider : SimpleBuildProvider
	{
		// Token: 0x0600468E RID: 18062 RVA: 0x000C3EE4 File Offset: 0x000C20E4
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new WebHandlerParser(context, virtualPath, physicalPath, reader);
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x000C3EF0 File Offset: 0x000C20F0
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return new WebHandlerParser(context, virtualPath, physicalPath, base.OpenReader(virtualPath.Original));
		}
	}
}
