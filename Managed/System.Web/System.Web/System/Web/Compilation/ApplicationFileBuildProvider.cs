using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000613 RID: 1555
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal class ApplicationFileBuildProvider : TemplateBuildProvider
	{
		// Token: 0x060042E6 RID: 17126 RVA: 0x000B06B6 File Offset: 0x000AE8B6
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new GlobalAsaxCompiler(parser as ApplicationFileParser);
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x000B06C3 File Offset: 0x000AE8C3
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x000B06DA File Offset: 0x000AE8DA
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new ApplicationFileParser(virtualPath, physicalPath, reader, context);
		}
	}
}
