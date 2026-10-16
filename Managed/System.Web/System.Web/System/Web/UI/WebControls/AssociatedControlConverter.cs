using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a type converter that retrieves a list of <see cref="T:System.Web.UI.WebControls.WebControl" /> controls in the current container.</summary>
	// Token: 0x0200032D RID: 813
	public class AssociatedControlConverter : ControlIDConverter
	{
		/// <summary>Indicates whether the provided control inherits from <see cref="T:System.Web.UI.WebControls.WebControl" />.</summary>
		/// <returns>true if the <paramref name="control" /> inherits from the <see cref="T:System.Web.UI.WebControls.WebControl" /> class; otherwise, false.</returns>
		/// <param name="control">The control instance to test whether it is a <see cref="T:System.Web.UI.WebControls.WebControl" />. </param>
		// Token: 0x06001C24 RID: 7204 RVA: 0x0004454E File Offset: 0x0004274E
		protected override bool FilterControl(Control control)
		{
			return control is WebControl;
		}
	}
}
