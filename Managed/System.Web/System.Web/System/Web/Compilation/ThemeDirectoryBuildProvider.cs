using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200066B RID: 1643
	internal class ThemeDirectoryBuildProvider : TemplateBuildProvider
	{
		// Token: 0x0600467D RID: 18045 RVA: 0x000C3B00 File Offset: 0x000C1D00
		protected override void OverrideAssemblyPrefix(TemplateParser parser, AssemblyBuilder assemblyBuilder)
		{
			if (parser == null || assemblyBuilder == null)
			{
				return;
			}
			string text = assemblyBuilder.OutputFilesPrefix + parser.ClassName + ".";
			assemblyBuilder.OutputFilesPrefix = text;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x000C3B32 File Offset: 0x000C1D32
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new PageThemeCompiler(parser as PageThemeParser);
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x000C3B3F File Offset: 0x000C1D3F
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			return this.CreateParser(virtualPath, inputFile, context);
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x000C3B4C File Offset: 0x000C1D4C
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string inputFile, HttpContext context)
		{
			string text = VirtualPathUtility.AppendTrailingSlash(virtualPath.Original);
			string physicalPath = virtualPath.PhysicalPath;
			if (!Directory.Exists(physicalPath))
			{
				throw new HttpException("Theme '" + virtualPath.Original + "' cannot be found in the application or global theme directories.");
			}
			PageThemeParser pageThemeParser = new PageThemeParser(virtualPath, context);
			string[] files = Directory.GetFiles(physicalPath, "*.css");
			string[] array = new string[files.Length];
			for (int i = 0; i < files.Length; i++)
			{
				array[i] = VirtualPathUtility.Combine(text, Path.GetFileName(files[i]));
				pageThemeParser.AddDependency(array[i]);
			}
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			pageThemeParser.LinkedStyleSheets = array;
			AspComponentFoundry aspComponentFoundry = new AspComponentFoundry();
			pageThemeParser.RootBuilder = new RootBuilder();
			foreach (string text2 in Directory.GetFiles(physicalPath, "*.skin"))
			{
				string text3 = VirtualPathUtility.Combine(text, Path.GetFileName(text2));
				PageThemeFileParser pageThemeFileParser = new PageThemeFileParser(new VirtualPath(text3), text2, context);
				pageThemeParser.AddDependency(text3);
				new AspGenerator(pageThemeFileParser, aspComponentFoundry).Parse();
				if (pageThemeFileParser.RootBuilder.Children != null)
				{
					foreach (object obj in pageThemeFileParser.RootBuilder.Children)
					{
						if (obj is ControlBuilder)
						{
							pageThemeParser.RootBuilder.AppendSubBuilder((ControlBuilder)obj);
						}
					}
				}
				foreach (string text4 in pageThemeFileParser.Assemblies)
				{
					if (!pageThemeParser.Assemblies.Contains(text4))
					{
						pageThemeParser.AddAssemblyByFileName(text4);
					}
				}
			}
			return pageThemeParser;
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override bool IsDirectoryBuilder
		{
			get
			{
				return true;
			}
		}
	}
}
