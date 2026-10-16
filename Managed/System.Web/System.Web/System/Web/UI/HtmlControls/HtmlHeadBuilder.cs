using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Interacts with the parser to build an <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</summary>
	// Token: 0x0200025A RID: 602
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlHeadBuilder : ControlBuilder
	{
		/// <summary>Determines whether the literal white space characters in the control must be processed or ignored.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x0600187A RID: 6266 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control's child controls. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the specified control's child control.</returns>
		/// <param name="tagName">The tag name of the child control.</param>
		/// <param name="attribs">An array of attributes contained in the child control.</param>
		// Token: 0x0600187B RID: 6267 RVA: 0x0003FFB4 File Offset: 0x0003E1B4
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			if (string.Compare(tagName, "title", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return typeof(HtmlTitle);
			}
			if (string.Compare(tagName, "link", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return typeof(HtmlLink);
			}
			if (string.Compare(tagName, "meta", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return typeof(HtmlMeta);
			}
			return null;
		}
	}
}
