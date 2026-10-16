using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Reserves a location on the Web page to display static text.</summary>
	// Token: 0x020003BF RID: 959
	[ControlBuilder(typeof(LiteralControlBuilder))]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[Designer("System.Web.UI.Design.WebControls.LiteralDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Literal : Control, ITextControl
	{
		/// <summary>Gets or sets an enumeration value that specifies how the content in the <see cref="T:System.Web.UI.WebControls.Literal" /> control is rendered.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LiteralMode" /> enumeration values. The default is Transform.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.LiteralMode" /> enumeration values. </exception>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000658F0 File Offset: 0x00063AF0
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x0006591B File Offset: 0x00063B1B
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(LiteralMode.Transform)]
		public LiteralMode Mode
		{
			get
			{
				if (this.ViewState["Mode"] != null)
				{
					return (LiteralMode)this.ViewState["Mode"];
				}
				return LiteralMode.Transform;
			}
			set
			{
				if (value < LiteralMode.Transform || value > LiteralMode.Encode)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.ViewState["Mode"] = value;
			}
		}

		/// <summary>Gets or sets the caption displayed in the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <returns>The caption displayed in the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</returns>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x0004759F File Offset: 0x0004579F
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x000475B6 File Offset: 0x000457B6
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[Localizable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return this.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		/// <summary>Set input focus to a control; the <see cref="M:System.Web.UI.WebControls.Literal.Focus" /> base control method is not supported on a <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.WebControls.Literal.Focus" /> was called on a <see cref="T:System.Web.UI.WebControls.Literal" />.</exception>
		// Token: 0x060027E0 RID: 10208 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the current instance of the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
		// Token: 0x060027E1 RID: 10209 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.WebControls.Literal" /> control that an XML or HTML element was parsed and adds that element to the <see cref="T:System.Web.UI.ControlCollection" /> of the control.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
		// Token: 0x060027E2 RID: 10210 RVA: 0x00065944 File Offset: 0x00063B44
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl != null)
			{
				this.Text = literalControl.Text;
				return;
			}
			throw new HttpException(global::Locale.GetText("'Literal' cannot have children of type '{0}'", new object[] { obj.GetType() }));
		}

		/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content. </param>
		// Token: 0x060027E3 RID: 10211 RVA: 0x00065986 File Offset: 0x00063B86
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Mode == LiteralMode.Encode)
			{
				writer.Write(HttpUtility.HtmlEncode(this.Text));
				return;
			}
			writer.Write(this.Text);
		}
	}
}
