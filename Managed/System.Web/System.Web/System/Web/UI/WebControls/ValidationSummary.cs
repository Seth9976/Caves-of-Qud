using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a summary of all validation errors inline on a Web page, in a message box, or both. </summary>
	// Token: 0x02000435 RID: 1077
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ValidationSummary : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> class.</summary>
		// Token: 0x06003196 RID: 12694 RVA: 0x0008298E File Offset: 0x00080B8E
		public ValidationSummary()
			: base(HtmlTextWriterTag.Div)
		{
			this.ForeColor = Color.Red;
		}

		/// <summary>Gets or sets the display mode of the validation summary.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ValidationSummaryDisplayMode" /> values. The default is BulletList.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The display mode is not one of the <see cref="T:System.Web.UI.WebControls.ValidationSummaryDisplayMode" /> values. </exception>
		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000829A4 File Offset: 0x00080BA4
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000829CD File Offset: 0x00080BCD
		[WebCategory("Appearance")]
		[DefaultValue(ValidationSummaryDisplayMode.BulletList)]
		[WebSysDescription("")]
		public ValidationSummaryDisplayMode DisplayMode
		{
			get
			{
				object obj = this.ViewState["DisplayMode"];
				if (obj != null)
				{
					return (ValidationSummaryDisplayMode)obj;
				}
				return ValidationSummaryDisplayMode.BulletList;
			}
			set
			{
				this.ViewState["DisplayMode"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control updates itself using client-side script.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control updates itself using client-side script; otherwise, false. The default is true.</returns>
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x00045FAB File Offset: 0x000441AB
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x00045FBE File Offset: 0x000441BE
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public bool EnableClientScript
		{
			get
			{
				return this.ViewState.GetBool("EnableClientScript", true);
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the control. The default is Red.</returns>
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000829E5 File Offset: 0x00080BE5
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x000829ED File Offset: 0x00080BED
		[DefaultValue(typeof(Color), "Red")]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the header text displayed at the top of the summary.</summary>
		/// <returns>The header text displayed at the top of the summary. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000829F6 File Offset: 0x00080BF6
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x00082A0D File Offset: 0x00080C0D
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Localizable(true)]
		[DefaultValue("")]
		public string HeaderText
		{
			get
			{
				return this.ViewState.GetString("HeaderText", string.Empty);
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the validation summary is displayed in a message box.</summary>
		/// <returns>true if the validation summary is to be displayed in a message box; otherwise, false. The default is false.</returns>
		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x00082A20 File Offset: 0x00080C20
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x00082A33 File Offset: 0x00080C33
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public bool ShowMessageBox
		{
			get
			{
				return this.ViewState.GetBool("ShowMessageBox", false);
			}
			set
			{
				this.ViewState["ShowMessageBox"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the validation summary is displayed inline.</summary>
		/// <returns>true if the validation summary is displayed inline; otherwise, false. The default is true.</returns>
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x00082A4B File Offset: 0x00080C4B
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x00082A5E File Offset: 0x00080C5E
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public bool ShowSummary
		{
			get
			{
				return this.ViewState.GetBool("ShowSummary", true);
			}
			set
			{
				this.ViewState["ShowSummary"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> object displays validation messages.</summary>
		/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> object displays validation messages.</returns>
		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x0003EF36 File Offset: 0x0003D136
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x0003EF4D File Offset: 0x0003D14D
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds attributes to the HTML tags generated for this control.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x060031A6 RID: 12710 RVA: 0x00082A78 File Offset: 0x00080C78
		[global::System.MonoTODO]
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this.EnableClientScript && this.pre_render_called && this.Page.AreValidatorsUplevel(this.ValidationGroup))
			{
				if (this.ID == null)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				}
				if (this.ValidationGroup != string.Empty)
				{
					this.RegisterExpandoAttribute(this.ClientID, "validationGroup", this.ValidationGroup);
				}
				if (this.HeaderText.Length > 0)
				{
					this.RegisterExpandoAttribute(this.ClientID, "headertext", this.HeaderText);
				}
				if (this.ShowMessageBox)
				{
					this.RegisterExpandoAttribute(this.ClientID, "showmessagebox", "True");
				}
				if (!this.ShowSummary)
				{
					this.RegisterExpandoAttribute(this.ClientID, "showsummary", "False");
				}
				if (this.DisplayMode != ValidationSummaryDisplayMode.BulletList)
				{
					this.RegisterExpandoAttribute(this.ClientID, "displaymode", this.DisplayMode.ToString());
				}
				if (!this.has_errors)
				{
					writer.AddStyleAttribute("display", "none");
				}
			}
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x00082B9E File Offset: 0x00080D9E
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, false);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00082BAA File Offset: 0x00080DAA
		internal void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			if (this.Page.ScriptManager != null)
			{
				this.Page.ScriptManager.RegisterExpandoAttributeExternal(this, controlId, attributeName, attributeValue, encode);
				return;
			}
			this.Page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x060031A9 RID: 12713 RVA: 0x00082BE5 File Offset: 0x00080DE5
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.RenderingCompatibilityLessThan40)
			{
				return;
			}
			if (this.ForeColor == Color.Empty)
			{
				this.ForeColor = Color.Red;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060031AA RID: 12714 RVA: 0x00082C14 File Offset: 0x00080E14
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.pre_render_called = true;
		}

		/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client.</param>
		// Token: 0x060031AB RID: 12715 RVA: 0x00082C24 File Offset: 0x00080E24
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.IsEnabled)
			{
				return;
			}
			ValidatorCollection validators = this.Page.GetValidators(this.ValidationGroup);
			ArrayList arrayList = new ArrayList(validators.Count);
			for (int i = 0; i < validators.Count; i++)
			{
				if (!validators[i].IsValid)
				{
					arrayList.Add(validators[i].ErrorMessage);
				}
			}
			this.has_errors = arrayList.Count > 0;
			if (this.EnableClientScript && this.pre_render_called && this.Page.AreValidatorsUplevel(this.ValidationGroup))
			{
				if (this.Page.ScriptManager != null)
				{
					this.Page.ScriptManager.RegisterArrayDeclarationExternal(this, "Page_ValidationSummaries", "document.getElementById ('" + this.ClientID + "')");
					this.Page.ScriptManager.RegisterStartupScriptExternal(this, typeof(BaseValidator), this.ClientID + "DisposeScript", string.Concat(new string[] { "\ndocument.getElementById('", this.ClientID, "').dispose = function() {\n\tArray.remove(Page_ValidationSummaries, document.getElementById('", this.ClientID, "'));\n}\n" }), true);
				}
				else
				{
					this.Page.ClientScript.RegisterArrayDeclaration("Page_ValidationSummaries", "document.getElementById ('" + this.ClientID + "')");
				}
			}
			if ((this.ShowSummary && this.has_errors) || (this.EnableClientScript && this.pre_render_called))
			{
				base.RenderBeginTag(writer);
			}
			if (this.ShowSummary && this.has_errors)
			{
				switch (this.DisplayMode)
				{
				case ValidationSummaryDisplayMode.List:
				{
					if (this.HeaderText.Length > 0)
					{
						writer.Write(this.HeaderText);
						writer.Write("<br />");
					}
					for (int j = 0; j < arrayList.Count; j++)
					{
						writer.Write(arrayList[j]);
						writer.Write("<br />");
					}
					break;
				}
				case ValidationSummaryDisplayMode.BulletList:
				{
					if (this.HeaderText.Length > 0)
					{
						writer.Write(this.HeaderText);
					}
					writer.Write("<ul>");
					for (int k = 0; k < arrayList.Count; k++)
					{
						writer.Write("<li>");
						writer.Write(arrayList[k]);
						writer.Write("</li>");
					}
					writer.Write("</ul>");
					break;
				}
				case ValidationSummaryDisplayMode.SingleParagraph:
				{
					if (this.HeaderText.Length > 0)
					{
						writer.Write(this.HeaderText);
						writer.Write(" ");
					}
					for (int l = 0; l < arrayList.Count; l++)
					{
						writer.Write(arrayList[l]);
						writer.Write(" ");
					}
					writer.Write("<br />");
					break;
				}
				}
			}
			if ((this.ShowSummary && this.has_errors) || (this.EnableClientScript && this.pre_render_called))
			{
				base.RenderEndTag(writer);
			}
		}

		/// <summary>Gets or sets a value that specifies whether model-state errors should be displayed.</summary>
		/// <returns>true if model state errors should be displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x00082F30 File Offset: 0x00081130
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool ShowModelStateErrors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the validation summary from validator controls should be displayed.</summary>
		/// <returns>true if the validation summary from validator controls should be displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x00082F4C File Offset: 0x0008114C
		// (set) Token: 0x060031AF RID: 12719 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool ShowValidationErrors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001C58 RID: 7256
		private bool pre_render_called;

		// Token: 0x04001C59 RID: 7257
		private bool has_errors;
	}
}
