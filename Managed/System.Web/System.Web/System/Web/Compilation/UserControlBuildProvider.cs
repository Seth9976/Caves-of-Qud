using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200066C RID: 1644
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class UserControlBuildProvider : TemplateBuildProvider
	{
		// Token: 0x06004683 RID: 18051 RVA: 0x000C3D38 File Offset: 0x000C1F38
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new UserControlCompiler(parser as UserControlParser);
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x000B06C3 File Offset: 0x000AE8C3
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x000C3D45 File Offset: 0x000C1F45
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new UserControlParser(virtualPath, physicalPath, reader, context);
		}
	}
}
