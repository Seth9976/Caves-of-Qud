using System;
using System.IO;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x02000249 RID: 585
	internal class WebHandlerParser : SimpleWebHandlerParser
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x0003E3AE File Offset: 0x0003C5AE
		private WebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
			: base(context, virtualPath, physicalPath)
		{
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0003E3B9 File Offset: 0x0003C5B9
		internal WebHandlerParser(HttpContext context, VirtualPath virtualPath, TextReader reader)
			: this(context, virtualPath, null, reader)
		{
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0003E3C5 File Offset: 0x0003C5C5
		internal WebHandlerParser(HttpContext context, VirtualPath virtualPath, string physicalPath, TextReader reader)
			: base(context, virtualPath.Original, physicalPath, reader)
		{
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		public static Type GetCompiledType(HttpContext context, string virtualPath, string physicalPath)
		{
			WebHandlerParser webHandlerParser = new WebHandlerParser(context, virtualPath, physicalPath);
			Type compiledTypeFromCache = webHandlerParser.GetCompiledTypeFromCache();
			if (compiledTypeFromCache != null)
			{
				return compiledTypeFromCache;
			}
			return WebServiceCompiler.CompileIntoType(webHandlerParser);
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0003E406 File Offset: 0x0003C606
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webhandler";
			}
		}
	}
}
