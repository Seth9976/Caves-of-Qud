using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;

namespace System.Web.UI
{
	/// <summary>Represents the base class for a page theme, which is a collection of resources that are used to define a consistent look across pages and controls in a Web site. The page theme can be set through configuration or the page directive.</summary>
	// Token: 0x0200020F RID: 527
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class PageTheme
	{
		/// <summary>Creates a lookup key object for a particular control type and skin ID. </summary>
		/// <returns>An object that can be used as a lookup key in a dictionary-style collection, which contains the control type and skin ID information.</returns>
		/// <param name="controlType">The <see cref="T:System.Type" /> of control to which a control skin applies, which is passed typically from the <see cref="P:System.Web.UI.ControlBuilder.ControlType" />.</param>
		/// <param name="skinID">The name of the control skin for which to create a key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="controlType" /> is null.</exception>
		// Token: 0x060015AA RID: 5546 RVA: 0x000389E8 File Offset: 0x00036BE8
		public static object CreateSkinKey(Type controlType, string skinID)
		{
			return skinID + ":" + controlType;
		}

		/// <summary>Uses the <see cref="M:System.Web.UI.DataBinder.Eval(System.Object,System.String)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> property that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate a data-binding expression.</summary>
		/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
		/// <param name="expression">The navigation path from the container to the public property value. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
		// Token: 0x060015AB RID: 5547 RVA: 0x000389F6 File Offset: 0x00036BF6
		protected object Eval(string expression)
		{
			return this.Page.Eval(expression);
		}

		/// <summary>Uses the <see cref="M:System.Web.UI.DataBinder.Eval(System.Object,System.String,System.String)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> property that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate a data-binding expression.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="expression">The navigation path from the container to the public property value. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
		/// <param name="format">A .NET Framework format string. For details, see <see cref="T:System.Web.UI.DataBinder" />.</param>
		// Token: 0x060015AC RID: 5548 RVA: 0x00038A04 File Offset: 0x00036C04
		protected string Eval(string expression, string format)
		{
			return this.Page.Eval(expression, format);
		}

		/// <summary>Tests whether a device filter applies to the <see cref="T:System.Web.UI.Page" /> control that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with.</summary>
		/// <returns>true if <paramref name="deviceFilterName" /> applies to the page; otherwise; false.</returns>
		/// <param name="deviceFilterName">The string name of the device filter to check. </param>
		// Token: 0x060015AD RID: 5549 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public bool TestDeviceFilter(string deviceFilterName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Evaluates an XPath data-binding expression.</summary>
		/// <returns>An object that results from the evaluation of the data-binding <paramref name="xPathExpression" />.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		// Token: 0x060015AE RID: 5550 RVA: 0x00038A13 File Offset: 0x00036C13
		protected object XPath(string xPathExpression)
		{
			return this.Page.XPath(xPathExpression);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution. </summary>
		/// <returns>An object that results from the evaluation of the data-binding <paramref name="xPathExpression" />.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
		// Token: 0x060015AF RID: 5551 RVA: 0x00038A21 File Offset: 0x00036C21
		protected object XPath(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPath(xPathExpression, resolver);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified format string to display the result.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		/// <param name="format">A .NET Framework format string to apply to the result. </param>
		// Token: 0x060015B0 RID: 5552 RVA: 0x00038A30 File Offset: 0x00036C30
		protected string XPath(string xPathExpression, string format)
		{
			return this.Page.XPath(xPathExpression, format);
		}

		/// <summary>Uses the <see cref="M:System.Web.UI.TemplateControl.XPath(System.String,System.String,System.Xml.IXmlNamespaceResolver)" /> method of the <see cref="P:System.Web.UI.PageTheme.Page" /> control that the instance of the <see cref="T:System.Web.UI.PageTheme" /> class is associated with to evaluate an XPath data-binding expression.</summary>
		/// <returns>A string that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="format">A .NET Framework format string to apply to the result. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used for namespace resolution.</param>
		// Token: 0x060015B1 RID: 5553 RVA: 0x00038A3F File Offset: 0x00036C3F
		protected string XPath(string xPathExpression, string format, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPath(xPathExpression, format, resolver);
		}

		/// <summary>Evaluates an XPath data-binding expression and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />.</param>
		// Token: 0x060015B2 RID: 5554 RVA: 0x00038A4F File Offset: 0x00036C4F
		protected IEnumerable XPathSelect(string xPathExpression)
		{
			return this.Page.XPathSelect(xPathExpression);
		}

		/// <summary>Evaluates an XPath data-binding expression using the specified prefix and namespace mappings for namespace resolution and returns a node collection that implements the <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes. </returns>
		/// <param name="xPathExpression">The XPath expression to evaluate. For details, see <see cref="T:System.Web.UI.XPathBinder" />. </param>
		/// <param name="resolver">A set of prefix and namespace mappings used to for namespace resolution. </param>
		// Token: 0x060015B3 RID: 5555 RVA: 0x00038A5D File Offset: 0x00036C5D
		protected IEnumerable XPathSelect(string xPathExpression, IXmlNamespaceResolver resolver)
		{
			return this.Page.XPathSelect(xPathExpression, resolver);
		}

		/// <summary>When overridden a derived class, gets the relative URL of the directory for the <see cref="T:System.Web.UI.PageTheme" /> object.</summary>
		/// <returns>A string value containing the relative URL of the <see cref="T:System.Web.UI.PageTheme" /> directory.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060015B4 RID: 5556
		protected abstract string AppRelativeTemplateSourceDirectory { get; }

		/// <summary>When overridden in a derived class, gets an <see cref="T:System.Collections.IDictionary" /> interface of the names of all default skins that are available for the current page theme, indexed by control type.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that represents the control skins associated with the current page theme.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060015B5 RID: 5557
		protected abstract IDictionary ControlSkins { get; }

		/// <summary>When overridden in a derived class, gets an array of style sheets that are linked to this page.</summary>
		/// <returns>A string array of style sheets linked to this page.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060015B6 RID: 5558
		protected abstract string[] LinkedStyleSheets { get; }

		/// <summary>Gets the <see cref="T:System.Web.UI.Page" /> object that is associated with the instance of the <see cref="T:System.Web.UI.PageTheme" /> class.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> associated with the <see cref="T:System.Web.UI.PageTheme" />.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00038A6C File Offset: 0x00036C6C
		protected Page Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00038A74 File Offset: 0x00036C74
		internal void SetPage(Page page)
		{
			this._page = page;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00038A80 File Offset: 0x00036C80
		internal ControlSkin GetControlSkin(Type controlType, string skinID)
		{
			object obj = PageTheme.CreateSkinKey(controlType, skinID);
			return this.ControlSkins[obj] as ControlSkin;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00038AA6 File Offset: 0x00036CA6
		internal string[] GetStyleSheets()
		{
			return this.LinkedStyleSheets;
		}

		// Token: 0x04001535 RID: 5429
		private Page _page;
	}
}
