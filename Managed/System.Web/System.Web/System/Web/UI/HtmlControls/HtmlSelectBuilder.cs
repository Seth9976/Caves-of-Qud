using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Interacts with the parser to build an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
	// Token: 0x0200026B RID: 619
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlSelectBuilder : ControlBuilder
	{
		/// <summary>Determines whether the white space literals in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control are to be processed or ignored.</summary>
		/// <returns>This method always returns false, indicating that white space in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control is ignored.</returns>
		// Token: 0x06001975 RID: 6517 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's child controls.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's specified child control.</returns>
		/// <param name="tagName">The tag name of the child control. </param>
		/// <param name="attribs">An array of attributes contained in the child control. </param>
		// Token: 0x06001976 RID: 6518 RVA: 0x00042348 File Offset: 0x00040548
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (string.Compare(tagName, "option", true, Helpers.InvariantCulture) != 0)
			{
				return null;
			}
			string text = attribs["selected"] as string;
			if (text != null && text.Length > 0 && string.Compare(text, "selected", true, Helpers.InvariantCulture) == 0)
			{
				attribs["selected"] = "true";
			}
			return typeof(ListItem);
		}
	}
}
