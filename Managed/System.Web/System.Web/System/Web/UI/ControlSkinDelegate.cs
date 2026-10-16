using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Represents the method that applies the correct control skin to the specified control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> that was passed to the method, with a control skin applied.</returns>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to which to apply the theme skin.</param>
	// Token: 0x020001B6 RID: 438
	// (Invoke) Token: 0x060011BF RID: 4543
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public delegate Control ControlSkinDelegate(Control control);
}
