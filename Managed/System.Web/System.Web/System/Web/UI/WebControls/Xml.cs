using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays an XML document without formatting or using Extensible Stylesheet Language Transformations (XSLT).</summary>
	// Token: 0x0200044C RID: 1100
	[PersistChildren(true)]
	[Designer("System.Web.UI.Design.WebControls.XmlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("DocumentSource")]
	[ControlBuilder(typeof(XmlBuilder))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Xml : Control
	{
		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.ClientID" /> property and returns the base server control identifier.</summary>
		/// <returns>The base server control identifier.</returns>
		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x0003002F File Offset: 0x0002E22F
		[global::System.MonoTODO("Anything else?")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.Controls" /> property and returns the base <see cref="T:System.Web.UI.ControlCollection" /> collection.</summary>
		/// <returns>The base <see cref="T:System.Web.UI.ControlCollection" /> collection.</returns>
		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x00030037 File Offset: 0x0002E237
		[global::System.MonoTODO("Anything else?")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlDocument" /> to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlDocument" /> to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x00086D00 File Offset: 0x00084F00
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x00086D08 File Offset: 0x00084F08
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use the XPathNavigator property instead by creating an XPathDocument and calling CreateNavigator().")]
		public XmlDocument Document
		{
			get
			{
				return this.xml_document;
			}
			set
			{
				this.xml_content = null;
				this.xml_file = null;
				this.xml_document = value;
			}
		}

		/// <summary>Sets a string that contains the XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>A string that contains the XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x00086D1F File Offset: 0x00084F1F
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x00086D35 File Offset: 0x00084F35
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DocumentContent
		{
			get
			{
				if (this.xml_content == null)
				{
					return "";
				}
				return this.xml_content;
			}
			set
			{
				this.xml_content = value;
				this.xml_file = null;
				this.xml_document = null;
			}
		}

		/// <summary>Gets or sets the path to an XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>The path to an XML document to display in the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x00086D4C File Offset: 0x00084F4C
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x00086D62 File Offset: 0x00084F62
		[WebSysDescription("")]
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		[Editor("System.Web.UI.Design.XmlUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[UrlProperty]
		public string DocumentSource
		{
			get
			{
				if (this.xml_file == null)
				{
					return "";
				}
				return this.xml_file;
			}
			set
			{
				this.xml_content = null;
				this.xml_file = value;
				this.xml_document = null;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.EnableTheming" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
		/// <returns>Always returns false. This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set the value of this property.</exception>
		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue(false)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.Control.SkinID" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
		/// <returns>Always returns an empty string (""). This property is not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set the value of this property.</exception>
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x00086D79 File Offset: 0x00084F79
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue("")]
		[Browsable(false)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException("SkinID is not supported on Xml control");
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Xsl.XslTransform" /> object that formats the XML document before it is written to the output stream.</summary>
		/// <returns>The <see cref="T:System.Xml.Xsl.XslTransform" /> that formats the XML document before it is written to the output stream.</returns>
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x00086D85 File Offset: 0x00084F85
		// (set) Token: 0x060032FF RID: 13055 RVA: 0x00086D8D File Offset: 0x00084F8D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public XslTransform Transform
		{
			get
			{
				return this.xsl_transform;
			}
			set
			{
				this.transform_file = null;
				this.xsl_transform = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Xml.Xsl.XsltArgumentList" /> that contains a list of optional arguments passed to the style sheet and used during the Extensible Stylesheet Language Transformation (XSLT).</summary>
		/// <returns>A <see cref="T:System.Xml.Xsl.XsltArgumentList" /> that contains a list of optional arguments passed to the style sheet and used during the XSL Transformation.</returns>
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x00086D9D File Offset: 0x00084F9D
		// (set) Token: 0x06003301 RID: 13057 RVA: 0x00086DA5 File Offset: 0x00084FA5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public XsltArgumentList TransformArgumentList
		{
			get
			{
				return this.transform_arguments;
			}
			set
			{
				this.transform_arguments = value;
			}
		}

		/// <summary>Gets or sets the path to an Extensible Stylesheet Language Transformation (XSLT) style sheet that formats the XML document before it is written to the output stream.</summary>
		/// <returns>The path to an XSL Transformation style sheet that formats the XML document before it is written to the output stream.</returns>
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x00086DAE File Offset: 0x00084FAE
		// (set) Token: 0x06003303 RID: 13059 RVA: 0x00086DC4 File Offset: 0x00084FC4
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.XslUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[global::System.MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
		public string TransformSource
		{
			get
			{
				if (this.transform_file == null)
				{
					return "";
				}
				return this.transform_file;
			}
			set
			{
				this.transform_file = value;
				this.xsl_transform = null;
			}
		}

		/// <summary>Gets or sets a cursor model for navigating and editing the XML data associated with the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x00086DD4 File Offset: 0x00084FD4
		// (set) Token: 0x06003305 RID: 13061 RVA: 0x00086DDC File Offset: 0x00084FDC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public XPathNavigator XPathNavigator
		{
			get
			{
				return this.xpath_navigator;
			}
			set
			{
				this.xpath_navigator = value;
			}
		}

		/// <summary>Searches the page naming container for the specified server control.</summary>
		/// <returns>The specified control; otherwise, null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found.</param>
		// Token: 0x06003306 RID: 13062 RVA: 0x00003BF6 File Offset: 0x00001DF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return null;
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.Focus" /> method. This method is not supported by the <see cref="T:System.Web.UI.WebControls.Xml" /> class.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to invoke this method.</exception>
		// Token: 0x06003307 RID: 13063 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Determines whether the server control contains any child controls.</summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x06003308 RID: 13064 RVA: 0x00008A4D File Offset: 0x00006C4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return false;
		}

		/// <summary>Renders the results to the output stream.</summary>
		/// <param name="output">The result of the output stream.</param>
		// Token: 0x06003309 RID: 13065 RVA: 0x00086DE8 File Offset: 0x00084FE8
		protected internal override void Render(HtmlTextWriter output)
		{
			XmlDocument xmlDocument = null;
			if (this.xpath_navigator == null)
			{
				if (this.xml_document != null)
				{
					xmlDocument = this.xml_document;
				}
				else if (this.xml_content != null)
				{
					xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(this.xml_content);
				}
				else
				{
					if (this.xml_file == null)
					{
						return;
					}
					xmlDocument = new XmlDocument();
					xmlDocument.Load(base.MapPathSecure(this.xml_file));
				}
			}
			XslTransform xslTransform = this.xsl_transform;
			if (this.transform_file != null)
			{
				xslTransform = new XslTransform();
				xslTransform.Load(base.MapPathSecure(this.transform_file));
			}
			if (xslTransform != null)
			{
				if (this.xpath_navigator != null)
				{
					xslTransform.Transform(this.xpath_navigator, this.transform_arguments, output);
					return;
				}
				xslTransform.Transform(xmlDocument, this.transform_arguments, output, null);
				return;
			}
			else
			{
				XmlTextWriter xmlTextWriter = new XmlTextWriter(output);
				xmlTextWriter.Formatting = Formatting.None;
				if (this.xpath_navigator != null)
				{
					xmlTextWriter.WriteStartDocument();
					this.xpath_navigator.WriteSubtree(xmlTextWriter);
					return;
				}
				xmlDocument.Save(xmlTextWriter);
				return;
			}
		}

		/// <param name="obj">An <see cref="T:System.Object" /> that represents the <see cref="T:System.Web.UI.LiteralControl" /> to add.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
		// Token: 0x0600330A RID: 13066 RVA: 0x00086ED8 File Offset: 0x000850D8
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.xml_document = new XmlDocument();
				this.xml_document.LoadXml(literalControl.Text);
				return;
			}
			throw new HttpException(string.Format("Objects of type {0} are not supported as children of the Xml control", obj.GetType()));
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.EmptyControlCollection" /> object.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x0600330B RID: 13067 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Gets design-time data for a control.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the <see cref="T:System.Web.UI.WebControls.Xml" /> control.</returns>
		// Token: 0x0600330C RID: 13068 RVA: 0x00003BF6 File Offset: 0x00001DF6
		[global::System.MonoTODO("Always returns null")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			return null;
		}

		// Token: 0x04001CB9 RID: 7353
		private XmlDocument xml_document;

		// Token: 0x04001CBA RID: 7354
		private XPathNavigator xpath_navigator;

		// Token: 0x04001CBB RID: 7355
		private string xml_content;

		// Token: 0x04001CBC RID: 7356
		private string xml_file;

		// Token: 0x04001CBD RID: 7357
		private XslTransform xsl_transform;

		// Token: 0x04001CBE RID: 7358
		private XsltArgumentList transform_arguments;

		// Token: 0x04001CBF RID: 7359
		private string transform_file;
	}
}
