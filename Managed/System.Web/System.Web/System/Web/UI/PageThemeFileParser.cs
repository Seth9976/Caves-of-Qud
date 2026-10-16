using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000211 RID: 529
	internal sealed class PageThemeFileParser : UserControlParser
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x00038AC0 File Offset: 0x00036CC0
		internal PageThemeFileParser(VirtualPath virtualPath, string inputFile, HttpContext context)
			: base(virtualPath, inputFile, context, "System.Web.UI.PageTheme")
		{
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00003946 File Offset: 0x00001B46
		internal override void HandleOptions(object obj)
		{
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00038AD0 File Offset: 0x00036CD0
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare("Register", directive, StringComparison.OrdinalIgnoreCase) == 0)
			{
				base.AddDirective(directive, atts);
				return;
			}
			base.ThrowParseException("Unknown directive: " + directive, Array.Empty<object>());
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00038AFF File Offset: 0x00036CFF
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.UI.PageTheme";
			}
		}
	}
}
