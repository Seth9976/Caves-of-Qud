using System;

namespace System.Web.UI
{
	// Token: 0x02000210 RID: 528
	internal class PageThemeBuilder : UserControlControlBuilder
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x00038AAE File Offset: 0x00036CAE
		public override void AppendLiteralString(string s)
		{
			throw new HttpException(string.Format("Literal content ('{0}') not allowed within a skin file", s));
		}
	}
}
