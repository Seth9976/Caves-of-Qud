using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000445 RID: 1093
	internal sealed class WizardLayoutContainer : WebControl
	{
		// Token: 0x060032B8 RID: 12984 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}
	}
}
