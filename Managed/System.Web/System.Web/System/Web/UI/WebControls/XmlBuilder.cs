using System;
using System.Collections;
using System.Web.Compilation;
using System.Xml;

namespace System.Web.UI.WebControls
{
	/// <summary>Interacts with the parser to build the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
	// Token: 0x0200044D RID: 1101
	public class XmlBuilder : ControlBuilder
	{
		/// <summary>Adds literal content to the control.</summary>
		/// <param name="s">The literal content to add to the control.</param>
		// Token: 0x0600330D RID: 13069 RVA: 0x00003946 File Offset: 0x00001B46
		public override void AppendLiteralString(string s)
		{
		}

		/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.WebControls.Xml" /> control's specified child control.</summary>
		/// <returns>The <see cref="M:System.Web.UI.ControlBuilder.GetChildControlType(System.String,System.Collections.IDictionary)" /> method is overridden to always return null.</returns>
		/// <param name="tagName">The tag name of the child control.</param>
		/// <param name="attribs">An array of attributes contained in the child control.</param>
		// Token: 0x0600330E RID: 13070 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override Type GetChildControlType(string tagName, IDictionary attribs)
		{
			return null;
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.Xml" /> control needs to get its inner text.</summary>
		/// <returns>The <see cref="M:System.Web.UI.ControlBuilder.NeedsTagInnerText" /> method is overridden to always return true.</returns>
		// Token: 0x0600330F RID: 13071 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool NeedsTagInnerText()
		{
			return true;
		}

		/// <summary>Sets the <see cref="T:System.Web.UI.WebControls.Xml" /> control's inner text. </summary>
		/// <param name="text">The value to insert as the inner text. </param>
		/// <exception cref="T:System.Xml.XmlException">The <see cref="T:System.String" /> object passed in is not well-formed XML.</exception>
		// Token: 0x06003310 RID: 13072 RVA: 0x00086F24 File Offset: 0x00085124
		public override void SetTagInnerText(string text)
		{
			string text2 = text.Trim();
			if (text2 == "")
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.LoadXml(text);
			}
			catch (XmlException ex)
			{
				Location location = new Location(base.Location);
				if (ex.LineNumber >= 0)
				{
					location.BeginLine += ex.LineNumber - 1;
				}
				base.Location = location;
				throw;
			}
			base.AppendLiteralString(text2);
		}
	}
}
