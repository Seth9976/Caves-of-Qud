using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000659 RID: 1625
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class PageBuildProvider : TemplateBuildProvider
	{
		// Token: 0x06004596 RID: 17814 RVA: 0x000BC647 File Offset: 0x000BA847
		protected override string MapPath(VirtualPath virtualPath)
		{
			if (virtualPath.IsFake)
			{
				return virtualPath.PhysicalPath;
			}
			return base.MapPath(virtualPath);
		}

		// Token: 0x06004597 RID: 17815 RVA: 0x000BC65F File Offset: 0x000BA85F
		protected override TextReader SpecialOpenReader(VirtualPath virtualPath, out string physicalPath)
		{
			if (virtualPath.IsFake)
			{
				physicalPath = virtualPath.PhysicalPath;
				return new StreamReader(physicalPath);
			}
			physicalPath = null;
			return base.SpecialOpenReader(virtualPath, out physicalPath);
		}

		// Token: 0x06004598 RID: 17816 RVA: 0x000BC684 File Offset: 0x000BA884
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new PageCompiler(parser as PageParser);
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x000B06C3 File Offset: 0x000AE8C3
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x0600459A RID: 17818 RVA: 0x000BC691 File Offset: 0x000BA891
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new PageParser(virtualPath, physicalPath, reader, context);
		}
	}
}
