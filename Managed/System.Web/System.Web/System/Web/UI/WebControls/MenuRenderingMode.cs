using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies whether the <see cref="T:System.Web.UI.WebControls.Menu" /> control renders HTML table elements and inline styles, or listitem elements and cascading style sheet (CSS) styles.</summary>
	// Token: 0x020002E4 RID: 740
	public enum MenuRenderingMode
	{
		/// <summary>The <see cref="T:System.Web.UI.WebControls.Menu" /> control renders markup in the way it does by default for the version of ASP.NET indicated by the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property of the control. If the value of the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property is 3.5, this setting is equivalent to <see cref="F:System.Web.UI.WebControls.MenuRenderingMode.Table" />. If the value of the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property is 4.0 or greater, this setting is equivalent to <see cref="F:System.Web.UI.WebControls.MenuRenderingMode.List" />.</summary>
		// Token: 0x04001719 RID: 5913
		Default,
		/// <summary>The <see cref="T:System.Web.UI.WebControls.Menu" /> control renders markup by using table elements and inline styles. </summary>
		// Token: 0x0400171A RID: 5914
		Table,
		/// <summary>The <see cref="T:System.Web.UI.WebControls.Menu" /> control renders markup by using list item (li) elements and CSS styles. </summary>
		// Token: 0x0400171B RID: 5915
		List
	}
}
