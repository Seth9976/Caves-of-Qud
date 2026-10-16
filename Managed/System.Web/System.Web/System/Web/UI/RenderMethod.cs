using System;

namespace System.Web.UI
{
	/// <summary>Represents the method that renders the specified <see cref="T:System.Web.UI.Control" /> container to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
	/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to render content to.</param>
	/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to render.</param>
	// Token: 0x0200021B RID: 539
	// (Invoke) Token: 0x06001614 RID: 5652
	public delegate void RenderMethod(HtmlTextWriter output, Control container);
}
