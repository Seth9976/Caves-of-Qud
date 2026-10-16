using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000657 RID: 1623
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class MasterPageBuildProvider : TemplateBuildProvider
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x000BC56C File Offset: 0x000BA76C
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new MasterPageCompiler(parser as MasterPageParser);
		}

		// Token: 0x06004591 RID: 17809 RVA: 0x000B06C3 File Offset: 0x000AE8C3
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x06004592 RID: 17810 RVA: 0x000BC579 File Offset: 0x000BA779
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new MasterPageParser(virtualPath, physicalPath, reader, context);
		}
	}
}
