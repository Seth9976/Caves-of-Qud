using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001AB RID: 427
	internal sealed class CodeRenderBuilder : CodeBuilder
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0002B64D File Offset: 0x0002984D
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x0002B655 File Offset: 0x00029855
		public bool HtmlEncode { get; private set; }

		// Token: 0x0600107A RID: 4218 RVA: 0x0002B65E File Offset: 0x0002985E
		public CodeRenderBuilder(string code, bool isAssign, ILocation location, bool doHtmlEncode)
			: base(code, isAssign, location)
		{
			this.HtmlEncode = doHtmlEncode;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002B671 File Offset: 0x00029871
		public CodeRenderBuilder(string code, bool isAssign, ILocation location)
			: base(code, isAssign, location)
		{
		}
	}
}
